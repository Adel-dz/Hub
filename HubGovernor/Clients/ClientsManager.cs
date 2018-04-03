using DGD.HubCore;
using DGD.HubCore.DB;
using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using DGD.HubGovernor.Log;
using DGD.HubGovernor.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


namespace DGD.HubGovernor.Clients
{
    sealed partial class ClientsManager: IDisposable
    {
        const int TTL_DIE = -6;

        readonly ActiveClientsQueue m_onlineClients;
        readonly Timer m_netTimer;
        readonly KeyIndexer m_ndxerProfiles;
        readonly KeyIndexer m_ndxerClients;
        readonly KeyIndexer m_ndxerClientsStatus;
        readonly KeyIndexer m_ndxerProfilesMgmnt;
        readonly KeyIndexer m_ndxerClientsEnv;
        readonly List<string> m_pendingUploads = new List<string>();
        readonly List<string> m_pendingDownloads = new List<string>();
        readonly Dictionary<Message_t , Func<Message , uint , Message>> m_msgProcessors;
        readonly Dictionary<Message_t , Func<Message , Message>> m_cxnReqProcessors;
        uint m_lastCxnReqMsgID;
        uint m_lastCnxRespMsgID;
        bool m_initializationDone;

        public event Action<uint> ClientStarted;
        public event Action<uint> ClientClosed;



        public ClientsManager()
        {
            m_netTimer = new Timer(obj => ProcessTimer() , null , Timeout.Infinite , Timeout.Infinite);

            m_ndxerProfiles = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.USER_PROFILE);
            m_ndxerClients = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.HUB_CLIENT);
            m_ndxerClientsStatus = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.CLIENT_STATUS);
            m_ndxerProfilesMgmnt = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.PROFILE_MGMNT_MODE);
            m_ndxerClientsEnv = AppContext.AccessPath.GetKeyIndexer(InternalTablesID.CLIENT_ENV);


            m_onlineClients = new ActiveClientsQueue();

            m_cxnReqProcessors = new Dictionary<Message_t , Func<Message , Message>>
            {
                { Message_t.NewConnection, ProcessNewConnectionReq },
                { Message_t.Resume, ProcessResumeConnectionReq },
                { Message_t.Start, ProcessStartMessage },
                {Message_t.Sync, ProcessSyncMessage }
            };

            m_msgProcessors = new Dictionary<Message_t , Func<Message , uint , Message>>
            {
                { Message_t.Close, ProcessCloseMessage },
                {Message_t.Null, ProcessNullMessage },
                {Message_t.SetInfo, ProcessSetInfoMessage },
                { Message_t.Log, ProcessLogMessage }
            };

            RegisterHandlers();

            AddDownload(Names.ConnectionReqFile);
        }


        public bool IsDisposed { get; private set; }

        public IEnumerable<HubClient> EnabledClients
        {
            get
            {
                var clients = from ClientStatus clStatus in m_ndxerClientsStatus.Source.Enumerate()
                              where clStatus.Status == ClientStatus_t.Enabled
                              select m_ndxerClients.Get(clStatus.ClientID) as HubClient;

                return clients;
            }
        }

        public IEnumerable<HubClient> RunningClients
        {
            get
            {
                lock (m_onlineClients)
                {
                    var clients = from clID in m_onlineClients.ClientsID
                                  select m_ndxerClients.Get(clID) as HubClient;

                    return clients.ToArray();
                }
            }
        }

        public IEnumerable<HubClient> BannedClients
        {
            get
            {

                var banneds = from cl in AllClients
                              let status = m_ndxerClientsStatus.Get(cl.ID) as ClientStatus
                              where status.Status == ClientStatus_t.Banned
                              select cl;

                return banneds;
            }
        }

        public IEnumerable<HubClient> DiasbledClients
        {
            get
            {
                var disableds = from cl in AllClients
                                let status = m_ndxerClientsStatus.Get(cl.ID) as ClientStatus
                                where status.Status == ClientStatus_t.Disabled
                                select cl;

                return disableds;
            }
        }

        public IEnumerable<HubClient> AllClients
        {
            get
            {
                var clients = from HubClient cl in m_ndxerClients.Source.Enumerate()
                              select cl;

                return clients;
            }
        }

        public bool IsClientRunning(uint clID) => m_onlineClients.Contains(clID);

        public ClientStatus_t GetClientStatus(uint idClient)
        {
            var clStatus = m_ndxerClientsStatus.Get(idClient) as ClientStatus;
            Dbg.Assert(clStatus != null);

            return clStatus.Status;
        }

        public void SetClientStatus(HubClient client , ClientStatus_t status)
        {
            //basculer le mode de gestion des profil vers manuel
            //SetProfileManagementMode(client.ProfileID , ManagementMode_t.Manual);

            //desactiver le client
            HubClient oldClient = GetProfileEnabledClient(client.ProfileID);

            if (status == ClientStatus_t.Enabled && oldClient != null && oldClient.ID != client.ID)
            {
                AppContext.LogManager.LogSysActivity($"Désactivation du client {ClientStrID(oldClient.ID)}" , true);

                //maj la table des status clients
                var oldClStatus = m_ndxerClientsStatus.Get(oldClient.ID) as ClientStatus;
                int ndx = m_ndxerClientsStatus.IndexOf(oldClient.ID);

                oldClStatus.Status = ClientStatus_t.Disabled;
                m_ndxerClientsStatus.Source.Replace(ndx , oldClStatus);

                string oldClFilePath = AppPaths.GetLocalSrvDialogPath(oldClient.ID);

                try
                {
                    ClientDialog oldClDlg = DialogEngin.ReadSrvDialog(oldClFilePath);
                    oldClDlg.ClientStatus = ClientStatus_t.Disabled;
                    DialogEngin.WriteSrvDialog(oldClFilePath , oldClDlg);
                }
                catch (Exception ex)
                {
                    AppContext.LogManager.LogSysError($"Lecture du fichier dialogue du client {ClientStrID(oldClient.ID)}" +
                        ex.Message);

                    DialogEngin.WriteSrvDialog(oldClFilePath ,
                        new ClientDialog(oldClient.ID , ClientStatus_t.Disabled , Enumerable.Empty<Message>()));
                }
                finally
                {
                    AddUpload(Names.GetSrvDialogFile(oldClient.ID));
                }
            }


            //maj la table des statuts clients
            int ndxStatus = m_ndxerClientsStatus.IndexOf(client.ID);
            var clStatus = m_ndxerClientsStatus.Get(client.ID) as ClientStatus;
            clStatus.Status = status;
            m_ndxerClientsStatus.Source.Replace(ndxStatus , clStatus);

            string filePath = AppPaths.GetLocalSrvDialogPath(client.ID);
            try
            {
                ClientDialog clDlg = DialogEngin.ReadSrvDialog(filePath);
                clDlg.ClientStatus = status;
                DialogEngin.WriteSrvDialog(filePath , clDlg);
            }
            catch (Exception ex)
            {
                AppContext.LogManager.LogSysError($"Lecture du fichier dialogue du client {ClientStrID(client.ID)}" +
                        ex.Message);

                DialogEngin.WriteSrvDialog(filePath ,
                    new ClientDialog(client.ID , status , Enumerable.Empty<Message>()));
            }
            finally
            {
                AddUpload(Names.GetSrvDialogFile(client.ID));
            }
        }

        public IEnumerable<HubClient> GetProfileClients(uint idProfile)
        {
            return (from id in m_ndxerClients.Keys
                    let cl = m_ndxerClients.Get(id) as HubClient
                    where cl.ProfileID == idProfile
                    select cl).ToArray();
        }

        public HubClient GetProfileEnabledClient(uint idProfile)
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
                AppContext.LogManager.LogSysActivity($"Mode de gestion du profil {idProfile} changé vers " +
                    $"{ProfileManagementMode.GetManagementModeName(mode)}" , true);

                mgmntMode.ManagementMode = mode;
                int ndx = m_ndxerProfilesMgmnt.IndexOf(idProfile);
                m_ndxerProfilesMgmnt.Source.Replace(ndx , mgmntMode);
            }
        }

        public IEnumerable<IEventLog> GetClientLog(uint clID)
        {
            Dbg.Assert(IsClientRunning(clID) == true);

            ActiveClientsQueue.IClientData clData = m_onlineClients.Get(clID);

            if (clData != null)
                return clData.ClientLogs.ToArray();

            return Enumerable.Empty<IEventLog>();
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                UnregisterHandlers();
                m_netTimer.Dispose();
                IsDisposed = true;
            }
        }

        public static string ClientStrID(uint clID) => clID.ToString("X");


        //private:                        
        void Initialize()
        {
            AppContext.LogManager.LogSysActivity("Démarrage de la réinitialisation des fichiers sur le serveur" , true);

            string reqFilePath = AppPaths.LocalConnectionReqPath;
            DialogEngin.WriteConnectionsReq(reqFilePath , Enumerable.Empty<Message>());

            string respFilePath = AppPaths.LocalConnectionRespPath;
            DialogEngin.WriteConnectionsResp(respFilePath , Enumerable.Empty<Message>());


            try
            {
                var netEngin = new NetEngin(AppContext.Settings.AppSettings);
                netEngin.Upload(AppPaths.RemoteConnectionReqUri , reqFilePath);
                netEngin.Upload(AppPaths.RemoteConnectionRespUri , respFilePath);
                m_initializationDone = true;
                AppContext.LogManager.LogSysActivity("Réinitialisation des fichiers sur le serveur terminée" , true);
            }
            catch (Exception ex)
            {
                AppContext.LogManager.LogSysError("Une erreur est survenue lors de l’initialisation du serveur: " +
                    ex.Message , true);
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
            AppContext.LogManager.LogSysActivity("Lancement de la mise à jour des profils sur serveur" , true);

            string filePath = AppPaths.LocalProfilesPath;

            var seq = from UserProfile usrPro in m_ndxerProfiles.Source.Enumerate().Cast<ProfileRow>()
                      select new ProfileInfo(usrPro.ID , usrPro.Name , usrPro.Privilege);

            DialogEngin.WriteProfiles(filePath , seq);

            AddUpload(Names.ProfilesFile);
        }

        void ProcessDialog(uint clientID , IEnumerable<Message> msgs)
        {
            ActiveClientsQueue.IClientData clData = m_onlineClients.Get(clientID);

            if (clData == null)
                return;


            uint lastClMsgID = clData.LastClientMessageID;

            var seq = from msg in msgs
                      where msg.ID > lastClMsgID
                      select msg;


            if (seq.Any())
            {
                uint lastSrvMsgID = clData.LastSrvMessageID;
                var respList = new List<Message>(seq.Count());

                var clStatus = m_ndxerClientsStatus.Get(clientID) as ClientStatus;
                clStatus.LastSeen = DateTime.Now;
                clData.TimeToLive = ActiveClientsQueue.InitTimeToLive;
                clData.LastClientMessageID = seq.Max(m => m.ID);

                foreach (Message msg in seq)
                {
                    Dbg.Log($"Processing dialog msg {msg.ID}: {msg.MessageCode}...");

                    if (m_msgProcessors.ContainsKey(msg.MessageCode))
                    {
                        Message resp = m_msgProcessors[msg.MessageCode](msg , clientID);

                        if (resp != null)
                            respList.Add(resp);
                    }
                }


                if (respList.Count > 0)
                {
                    DialogEngin.AppendSrvDialog(AppPaths.GetLocalSrvDialogPath(clientID) , respList);
                    AddUpload(Names.GetSrvDialogFile(clientID));
                }



                Dbg.Assert(clData.LastClientMessageID >= lastClMsgID);
                clStatus.SentMsgCount += clData.LastClientMessageID - lastClMsgID;

                Dbg.Assert(clData.LastSrvMessageID >= lastSrvMsgID);
                clStatus.ReceivedMsgCount += clData.LastSrvMessageID - lastSrvMsgID;

                m_ndxerClientsStatus.Source.Replace(m_ndxerClientsStatus.IndexOf(clientID) , clStatus);
            }
        }

        void ProcessConnectionReq(IEnumerable<Message> messages)
        {
            uint id = m_lastCxnReqMsgID;
            IEnumerable<Message> reqs = messages.Where(m => m.ID > id);

            if (reqs.Any())
            {
                var respList = new List<Message>();

                m_lastCxnReqMsgID = reqs.Max(m => m.ID);

                foreach (Message req in reqs)
                {
                    Dbg.Log($"Processing connection msg {req.ID} ...");
                    Message resp = m_cxnReqProcessors[req.MessageCode](req);

                    if (resp != null)
                        respList.Add(resp);
                }

                string respFile = AppPaths.LocalConnectionRespPath;
                DialogEngin.AppendConnectionsResp(respFile , respList);
                AddUpload(Names.ConnectionRespFile);
            }
        }

        void RegisterHandlers()
        {
            StartTimer();

            m_ndxerProfiles.DatumInserted += Profiles_DatumInserted;
            m_ndxerProfiles.DatumReplaced += Profiles_DatumReplaced;
            m_ndxerProfiles.DatumDeleted += Profiles_DatumDeleted;
            AppContext.LogManager.ClientLogAdded += LogManager_ClientLogAdded;
        }

        void UnregisterHandlers()
        {
            StopTimer();

            m_ndxerProfiles.DatumInserted -= Profiles_DatumInserted;
            m_ndxerProfiles.DatumReplaced -= Profiles_DatumReplaced;
            m_ndxerProfiles.DatumDeleted -= Profiles_DatumDeleted;
            AppContext.LogManager.ClientLogAdded -= LogManager_ClientLogAdded;
        }

        void ProcessUploads()
        {
            List<string> files = null;

            lock (m_pendingUploads)
                if (m_pendingUploads.Count > 0)
                {
                    files = m_pendingUploads.ToList();
                    m_pendingUploads.Clear();
                }


            if (files != null)
            {
                string dlgFolder = AppPaths.LocalDialogFolderPath;
                var netEngin = new NetEngin(AppContext.Settings.AppSettings);

                for (int i = files.Count - 1; i >= 0; --i)
                {
                    string localDlgDir = AppPaths.LocalDialogFolderPath;
                    string fileName = files[i];
                    string srcPath = Path.Combine(localDlgDir , fileName);
                    var destURI = new Uri(AppPaths.RemoteDialogDirUri , fileName);


                    try
                    {
                        netEngin.Upload(destURI , srcPath);
                        files.RemoveAt(i);
                    }
                    catch (Exception ex)
                    {
                        AppContext.LogManager.LogSysError("Traitement des transferts vers le serveur: " + ex.Message , true);
                        continue;
                    }
                }

                foreach (string file in files)
                    AddUpload(file);
            }
        }

        void ProcessDownloads()
        {
            List<string> files = null;

            lock (m_pendingDownloads)
                if (m_pendingDownloads.Count > 0)
                {
                    files = m_pendingDownloads.ToList();
                    m_pendingDownloads.Clear();
                }

            if (files != null)
            {
                Uri remoteDlgDir = AppPaths.RemoteDialogDirUri;
                string localDlgFolder = AppPaths.LocalDialogFolderPath;
                string cxnReqFile = Names.ConnectionReqFile;
                var netEngin = new NetEngin(AppContext.Settings.AppSettings);

                for (int i = files.Count - 1; i >= 0; --i)
                {
                    string fileName = files[i];
                    string destPath = Path.Combine(localDlgFolder , fileName);
                    var srcURI = new Uri(remoteDlgDir , fileName);


                    try
                    {
                        netEngin.Download(destPath , srcURI);
                    }
                    catch (Exception ex)
                    {
                        AppContext.LogManager.LogSysError("Traitement des transferts à partir du serveur: " +
                            ex.Message , true);
                        continue;
                    }

                    if (string.Compare(fileName , cxnReqFile , true) == 0)

                        try
                        {
                            ProcessConnectionReq(DialogEngin.ReadConnectionsReq(AppPaths.LocalConnectionReqPath));
                        }
                        catch (Exception ex)
                        {
                            TextLogger.Warning(ex.Message);
                        }
                    else
                    {
                        uint clID = uint.Parse(Path.GetFileNameWithoutExtension(fileName) ,
                            System.Globalization.NumberStyles.AllowHexSpecifier);

                        try
                        {
                            ProcessDialog(clID , DialogEngin.ReadHubDialog(Path.Combine(localDlgFolder , fileName) , clID));
                        }
                        catch (Exception ex)
                        {
                            AppContext.LogManager.LogSysError("Traitement des transferts à partir du serveur: " +
                                ex.Message , true);
                            continue;
                        }
                    }

                    files.RemoveAt(i);
                }


                foreach (string file in files)
                    AddDownload(file);
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
                ProcessRunningClients();
                ProcessUploads();
            }
            else
                Initialize();

            StartTimer();
        }

        void ProcessRunningClients()
        {
            var deadClients = new List<uint>();


            foreach (uint clID in m_onlineClients.ClientsID)
            {
                ActiveClientsQueue.IClientData clData = m_onlineClients.Get(clID);

                if (clData == null)
                    continue;

                if (--clData.TimeToLive <= TTL_DIE)
                    deadClients.Add(clID);
                else if (clData.TimeToLive <= 0)
                {
                    AppContext.LogManager.LogSysActivity($"Envoi d'un message de synchronisation au client {ClientStrID(clID)}" , true);
                    var msg = new Message(++clData.LastSrvMessageID , 0 , Message_t.Sync); //delegate status update to processdialog method
                    DialogEngin.AppendSrvDialog(AppPaths.GetLocalSrvDialogPath(clID) , msg);
                    AddUpload(Names.GetSrvDialogFile(clID));
                }
            }

            foreach (uint id in deadClients)
            {
                AppContext.LogManager.LogSysActivity($"Client {ClientStrID(id)} présumé déconnecté" , true);

                m_onlineClients.Remove(id);
                AppContext.LogManager.CloseLogger(id);
                ClientClosed?.Invoke(id);
            }


            foreach (uint clID in m_onlineClients.ClientsID)
                AddDownload(Names.GetClientDialogFile(clID));
        }

        void UpdateClientEnvironment(uint clID , ClientEnvironment clEnv)
        {
            IEnumerable<HubClientEnvironment> seq =
                from HubClientEnvironment env in m_ndxerClientsEnv.Source.Enumerate()
                where env.ClientID == clID
                select env;

            if (seq.Any())
            {
                HubClientEnvironment hubEnv = seq.First();

                foreach (HubClientEnvironment hce in seq.Skip(1))
                    if (hubEnv.CreationTime < hce.CreationTime)
                        hubEnv = hce;

                if (hubEnv.HubArchitecture == clEnv.HubArchitecture &&
                        hubEnv.HubVersion == clEnv.HubVersion &&
                        hubEnv.Is64BitOperatingSystem == clEnv.Is64BitOperatingSystem &&
                        hubEnv.MachineName == clEnv.MachineName &&
                        hubEnv.OSVersion == clEnv.OSVersion &&
                        hubEnv.UserName == clEnv.UserName)
                    return;
            }

            var newHubEnv = new HubClientEnvironment(AppContext.TableManager.ClientsEnvironment.CreateUniqID() ,
                clID);

            newHubEnv.HubArchitecture = clEnv.HubArchitecture;
            newHubEnv.HubVersion = clEnv.HubVersion;
            newHubEnv.Is64BitOperatingSystem = clEnv.Is64BitOperatingSystem;
            newHubEnv.MachineName = clEnv.MachineName;
            newHubEnv.OSVersion = clEnv.OSVersion;
            newHubEnv.UserName = clEnv.UserName;

            AppContext.LogManager.LogSysActivity("Mise à jour de l'environnement du client " +
                $"{ClientStrID(clID)}, valeur = {newHubEnv}");

            m_ndxerClientsEnv.Source.Insert(newHubEnv);
        }

        //handelrs:        
        private void Profiles_DatumDeleted(IDataRow row) => ProcessProfilesChange();
        private void Profiles_DatumReplaced(IDataRow row) => ProcessProfilesChange();
        private void Profiles_DatumInserted(IDataRow row) => ProcessProfilesChange();

        private void LogManager_ClientLogAdded(uint clID , IEventLog log)
        {
            ActiveClientsQueue.IClientData clData = m_onlineClients.Get(clID);

            if (clData != null)
                clData.AddLog(log);
        }
    }
}
