﻿using DGD.HubCore;
using DGD.HubCore.DB;
using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using DGD.HubGovernor.Profiles;
using easyLib.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


namespace DGD.HubGovernor.Clients
{
    sealed partial class ClientsManager: IDisposable
    {
        readonly Timer m_netTimer;
        readonly KeyIndexer m_ndxerProfiles;
        readonly KeyIndexer m_ndxerClients;
        readonly KeyIndexer m_ndxerClientsStatus;
        readonly KeyIndexer m_ndxerProfilesMgmnt;
        readonly List<string> m_pendingUploads = new List<string>();
        readonly List<string> m_pendingDownloads = new List<string>();
        readonly Dictionary<Message_t , Func<Message , uint , Message>> m_msgProcessors;
        readonly Dictionary<Message_t , Func<Message , Message>> m_cxnReqProcessors;
        uint m_lastCxnReqMsgID;
        uint m_lastCnxRespMsgID;
        bool m_initializationDone;

        public ClientsManager()
        {
            m_netTimer = new Timer(obj => ProcessTimer() , null , Timeout.Infinite , Timeout.Infinite);

            m_ndxerProfiles = new KeyIndexer(AppContext.TableManager.Profiles.DataProvider);
            m_ndxerProfiles.Connect();

            m_ndxerClients = new KeyIndexer(AppContext.TableManager.HubClients.DataProvider);
            m_ndxerClients.Connect();

            m_ndxerClientsStatus = new KeyIndexer(AppContext.TableManager.ClientsStatus.DataProvider);
            m_ndxerClientsStatus.Connect();

            m_ndxerProfilesMgmnt = new KeyIndexer(AppContext.TableManager.ProfileManagementMode.DataProvider);
            m_ndxerProfilesMgmnt.Connect();

            m_cxnReqProcessors = new Dictionary<Message_t , Func<Message , Message>>
            {
                {Message_t.NewConnection, ProcessNewConnectionReq },
                {Message_t.Resume, ProcessResumeConnectionReq }
            };

            RegisterHandlers();

            AddDownload(Names.ConnectionReqFile);
        }


        public bool IsDisposed { get; private set; }

        public IEnumerable<HubClient> Clients
        {
            get;
        }

        public IEnumerable<HubClient> ActiveClients
        {
            get;
        }

        public IEnumerable<HubClient> RunningClients
        {
            get;
        }

        public ClientStatus_t GetClientStatus(uint idClient)
        {
            var clStatus = m_ndxerClientsStatus.Get(idClient) as ClientStatus;
            Dbg.Assert(clStatus != null);

            return clStatus.Status;
        }

        public void SetClientStatus(HubClient client , ClientStatus_t status)
        {
            //basculer le mode de gestion des profil vers manuel
            SetProfileManagementMode(client.ProfileID , ManagementMode_t.Manual);

            string filePath = AppPaths.GetLocalSrvDialogPath(client.ID);
            ClientDialog clDlg = DialogEngin.ReadSrvDialog(filePath);

            //desactiver le client
            HubClient oldClient = GetProfileActiveClient(client.ProfileID);

            if (status == ClientStatus_t.Enabled && oldClient != null && oldClient.ID != client.ID)
            {
                EventLogger.Info($"Désactivation du client {oldClient.ContactName}...");
        
                //maj la table des status clients
                int ndx = m_ndxerClientsStatus.IndexOf(oldClient.ID);

                Dbg.Assert(ndx >= 0);

                string oldClFilePath = AppPaths.GetLocalSrvDialogPath(oldClient.ID);
                ClientDialog oldClDlg = DialogEngin.ReadSrvDialog(oldClFilePath);

                var oldClStatus = new ClientStatus(oldClient.ID , ClientStatus_t.Disabled);
                m_ndxerClientsStatus.Source.Replace(ndx , oldClStatus);

                oldClDlg.ClientStatus = ClientStatus_t.Disabled;
                DialogEngin.WriteSrvDialog(oldClFilePath , oldClDlg);
                AddUpload(Names.GetSrvDialogFile(oldClient.ID));
            }


            //maj la table des statuts clients
            int ndxStatus = m_ndxerClientsStatus.IndexOf(client.ID);

            Dbg.Assert(ndxStatus >= 0);
            var clStatus = new ClientStatus(client.ID , status);
            m_ndxerClientsStatus.Source.Replace(ndxStatus , clStatus);

            clDlg.ClientStatus = status;
            DialogEngin.WriteSrvDialog(filePath , clDlg);
            AddUpload(Names.GetSrvDialogFile(client.ID));
        }

        public IEnumerable<HubClient> GetProfileClients(uint idProfile)
        {
            return (from id in m_ndxerClients.Keys
                    let cl = m_ndxerClients.Get(id) as HubClient
                    where cl.ProfileID == idProfile
                    select cl).ToArray();
        }

        public HubClient GetProfileActiveClient(uint idProfile)
        {
            return (from hc in GetProfileClients(idProfile)
                    where (m_ndxerClientsStatus.Get(hc.ID) as ClientStatus).Status == ClientStatus_t.Enabled
                    select hc).SingleOrDefault();
        }

        public ManagementMode_t GetProfileManagementMode(uint idProfile)
        {
            var mgmntMode = m_ndxerProfilesMgmnt.Get(idProfile) as ProfileManagementMode;

            Dbg.Assert(mgmntMode != null);

            return mgmntMode.ManagementMode;
        }

        public void SetProfileManagementMode(uint idProfile , ManagementMode_t mode)
        {
            var mgmntMode = m_ndxerProfilesMgmnt.Get(idProfile) as ProfileManagementMode;

            Dbg.Assert(mgmntMode != null);

            if (mgmntMode.ManagementMode != mode)
            {
                mgmntMode.ManagementMode = mode;
                int ndx = m_ndxerProfilesMgmnt.IndexOf(idProfile);
                m_ndxerProfilesMgmnt.Source.Replace(ndx , mgmntMode);
            }
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                UnregisterHandlers();

                m_ndxerProfiles.Close();
                m_ndxerClients.Close();
                m_ndxerClientsStatus.Close();
                m_ndxerProfilesMgmnt.Close();
                m_netTimer.Dispose();

                IsDisposed = true;
            }
        }

        //private:        
        static string ClientStrID(uint clID) => clID.ToString("X");

        void Initialize()
        {
            EventLogger.Info("Réinitialisation des fichiers sur le serveur...");

            string reqFilePath = AppPaths.LocalConnectionReqPath;
            DialogEngin.WriteConnectionsReq(reqFilePath , Enumerable.Empty<Message>());

            string respFilePath = AppPaths.LocalConnectionRespPath;
            DialogEngin.WriteConnectionResp(respFilePath , Enumerable.Empty<Message>());


            try
            {
                var netEngin = new NetEngin(AppContext.Settings.AppSettings);
                netEngin.Upload(AppPaths.RemoteConnectionReqUri , reqFilePath);
                netEngin.Upload(AppPaths.RemoteConnectionRespUri , respFilePath);
                m_initializationDone = true;

                EventLogger.Info("Réinitialisation réussie.");
            }
            catch (Exception ex)
            {
                EventLogger.Error("Une erreur est survenue lors de l’initialisation du serveur: " +
                    ex.Message);
            }
        }

        void StartTimer()
        {
            if (!IsDisposed)
                lock (m_netTimer)
                {
                    int interval = m_initializationDone ? AppContext.Settings.AppSettings.DialogTimerInterval :
                        AppContext.Settings.AppSettings.DialogInitializationInterval;

                    m_netTimer.Change(interval , interval);
                }
        }

        void StopTimer()
        {
            if (!IsDisposed)
                lock (m_netTimer)
                    m_netTimer.Change(Timeout.Infinite , Timeout.Infinite);
        }

        void AddUpload(string fileName)
        {
            lock (m_pendingUploads)
                if (m_pendingUploads.FindIndex(s => string.Compare(s , fileName , true) == 0) == -1)
                    m_pendingUploads.Add(fileName);
        }

        void AddDownload(string fileName)
        {
            lock (m_pendingDownloads)
                if (m_pendingDownloads.FindIndex(s => string.Compare(s , fileName , true) == 0) == -1)
                    m_pendingDownloads.Add(fileName);
        }

        void ProcessProfilesChange()
        {
            EventLogger.Info("Mise à jour des profils au niveau du serveur...");

            string filePath = AppPaths.LocalProfilesPath;

            var seq = from UserProfile usrPro in m_ndxerProfiles.Source.Enumerate().Cast<ProfileRow>()
                      select new ProfileInfo(usrPro.ID , usrPro.Name , usrPro.Privilege);

            DialogEngin.WriteProfiles(filePath , seq);

            AddUpload(filePath);
        }

        void ProcessDialog(ClientDialog clientDialog)
        {
            throw new NotImplementedException();
        }

        void ProcessConnectionReq(IEnumerable<Message> messages)
        {
            EventLogger.Info("Traitement d’éventuelles requêtes d'inscription...");

            IEnumerable<Message> reqs = messages.Where(m => m.ID > m_lastCxnReqMsgID);

            var lst = new List<Message>();

            foreach (Message req in reqs)
            {
                Message resp = m_cxnReqProcessors[req.MessageCode](req);

                if (resp != null)
                    lst.Add(resp);
            }

            string respFile = AppPaths.LocalConnectionRespPath;
            DialogEngin.AppendConnectionResp(respFile , lst);
            AddUpload(respFile);
        }

        void RegisterHandlers()
        {
            StartTimer();

            m_ndxerProfiles.DatumInserted += Profiles_DatumInserted;
            m_ndxerProfiles.DatumReplaced += Profiles_DatumReplaced;
            m_ndxerProfiles.DatumDeleted += Profiles_DatumDeleted;
        }

        void UnregisterHandlers()
        {
            StopTimer();

            m_ndxerProfiles.DatumInserted -= Profiles_DatumInserted;
            m_ndxerProfiles.DatumReplaced -= Profiles_DatumReplaced;
            m_ndxerProfiles.DatumDeleted -= Profiles_DatumDeleted;
        }

        void ProcessUploads()
        {
            string[] files = null;

            lock (m_pendingUploads)
                if (m_pendingUploads.Count > 0)
                {
                    string dlgFolder = AppPaths.LocalDialogFolderPath;

                    files = (from file in m_pendingUploads
                             select Path.Combine(dlgFolder , file)).ToArray();
                    m_pendingUploads.Clear();
                }


            if (files != null)
                try
                {
                    EventLogger.Info($"Transfert de {files.Length} fichier(s) vers le serveur...");
                    new NetEngin(AppContext.Settings.AppSettings).Upload(AppPaths.RemoteDialogDirUri , files);
                }
                catch (Exception ex)
                {
                    EventLogger.Error(ex.Message);

                    foreach (string file in files)
                        AddUpload(file);
                }
        }

        void ProcessDownloads()
        {
            Dbg.Log("Processing downloads...");

            var netEngin = new NetEngin(AppContext.Settings.AppSettings);

            string[] files = null;


            lock (m_pendingDownloads)
                if (m_pendingDownloads.Count > 0)
                {
                    files = m_pendingDownloads.ToArray();
                    m_pendingDownloads.Clear();
                }

            if (files != null)
            {
                Uri remoteDlgDir = AppPaths.RemoteDialogDirUri;

                Uri[] uris = (from file in files
                              select new Uri(remoteDlgDir , file)).ToArray();

                try
                {
                    EventLogger.Info($"Réception de {files.Length} fichier(s) à partir du serveur");

                    netEngin.Download(AppPaths.LocalDialogFolderPath , uris);
                    string dlgFolderPath = AppPaths.LocalDialogFolderPath;

                    string cxnReqFile = Names.ConnectionReqFile;

                    foreach (string file in files)
                    {
                        if (string.Compare(file , cxnReqFile , true) == 0)
                            ProcessConnectionReq(DialogEngin.ReadConnectionsReq(AppPaths.LocalConnectionReqPath));
                        else
                            ProcessDialog(DialogEngin.ReadSrvDialog(Path.Combine(dlgFolderPath , file)));
                    }

                }
                catch (Exception ex)
                {
                    EventLogger.Error(ex.Message);

                    foreach (string file in files)
                        AddDownload(file);
                }
            }

            //allways need to be downlaoded
            AddDownload(Names.ConnectionReqFile);
        }

        void ProcessTimer()
        {
            StopTimer();

            if (m_initializationDone)
            {
                ProcessDownloads();
                ProcessUploads();
            }
            else
                Initialize();

            StartTimer();
        }

        void AddClient(ClientInfo clInfo)
        {
            //desactiver l'ancien client actif si il existe
            var oldClient = GetProfileActiveClient(clInfo.ProfileID);

            if (oldClient != null)
            {
                EventLogger.Info($"Désactivation du client {oldClient.ContactName}...");

                //maj la table des status clients                
                var oldClStatus = new ClientStatus(oldClient.ID , ClientStatus_t.Disabled);
                int ndx = m_ndxerClientsStatus.IndexOf(oldClStatus.ID);
                m_ndxerClientsStatus.Source.Replace(ndx , oldClStatus);

                //maj des fichiers de dialogue
                string filePath = AppPaths.GetLocalSrvDialogPath(oldClient.ID);

                ClientDialog clDlg = DialogEngin.ReadSrvDialog(filePath);
                clDlg.ClientStatus = ClientStatus_t.Disabled;
                DialogEngin.WriteSrvDialog(filePath , clDlg);
                AddUpload(Names.GetSrvDialogFile(oldClient.ID));
            }


            //maj la table des clients
            var hClient = new HubClient(clInfo.ClientID , clInfo.ProfileID)
            {
                ContaclEMail = clInfo.ContaclEMail ,
                ContactName = clInfo.ContactName ,
                ContactPhone = clInfo.ContactPhone ,
                MachineName = clInfo.MachineName ,
            };

            m_ndxerClients.Source.Insert(hClient);

            //maj du status client
            var clStatus = new ClientStatus(clInfo.ClientID , ClientStatus_t.Enabled);
            m_ndxerClientsStatus.Source.Insert(clStatus);

            //creer les fichier de dialogue 
            string srvDlgPath = AppPaths.GetLocalSrvDialogPath(clInfo.ClientID);
            DialogEngin.WriteSrvDialog(srvDlgPath , new ClientDialog(clInfo.ClientID ,
                 ClientStatus_t.Enabled , Enumerable.Empty<Message>()));

            string hubDlgPath = AppPaths.GetLocalClientDilogPath(clInfo.ClientID);
            DialogEngin.WriteHubDialog(hubDlgPath , clInfo.ClientID , Enumerable.Empty<Message>());

            new NetEngin(AppContext.Settings.AppSettings).Upload(AppPaths.RemoteDialogDirUri ,
                new string[] { srvDlgPath , hubDlgPath });
        }

        //handelrs:
        private void Profiles_DatumDeleted(IDataRow row) => ProcessProfilesChange();
        private void Profiles_DatumReplaced(IDataRow row) => ProcessProfilesChange();
        private void Profiles_DatumInserted(IDataRow row) => ProcessProfilesChange();
    }
}
