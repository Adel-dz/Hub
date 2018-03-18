using DGD.HubCore;
using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using DGD.HubGovernor.Profiles;
using easyLib;
using easyLib.DB;
using easyLib.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace DGD.HubGovernor.Clients
{
    partial class ClientsManager
    {
        Message ProcessCloseMessage(Message msg , uint clID)
        {
            EventLogger.Info("Réception d'une notification de fermeture.");

            Dbg.Assert(msg.MessageCode == Message_t.Close);

            EventLogger.Info($"Réception d’une notification de fermeture de la part du client {clID:X}");


            DateTime dt = DateTime.Now;
            EventLogger.Info($"client arrêté le {dt.ToShortDateString()} à {dt.ToLongTimeString()}");

            lock (m_runningClients)
                m_runningClients.Remove(clID);

            ClientClosed?.Invoke(clID);

            return null;
        }

        Message ProcessStartMessage(Message msg)
        {
            EventLogger.Info("Réception d'une notification de démarrage.");

            Dbg.Assert(msg.MessageCode == Message_t.Start);

            var reader = new RawDataReader(new MemoryStream(msg.Data) , Encoding.UTF8);
            uint clID = reader.ReadUInt();
            ClientEnvironment clEnv = ClientEnvironment.Load(reader);
            DateTime dtStart = reader.ReadTime();

            var client = m_ndxerClients.Get(clID) as HubClient;

            if (client == null)
            {
                EventLogger.Warning("Réception d’une notification de démarrage " +
                    $"de la part d’un client inexistant ({clID}). Bannissement du client.");

                //maj du fichier gov
                string srvDlgFile = AppPaths.GetLocalSrvDialogPath(clID);

                //le client n'existe pas => son fichier gov n'existe pas
                var clDlg = new ClientDialog(clID , ClientStatus_t.Banned , Enumerable.Empty<Message>());
                DialogEngin.WriteSrvDialog(srvDlgFile , clDlg);

                AddUpload(Path.GetFileName(srvDlgFile));
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , msg.Data);
            }


            EventLogger.Info("Réception d’une notification de démarrage " +
                    $"de la part du client {client.ContactName}");

            //verifier le statut du client
            var clStatus = m_ndxerClientsStatus.Get(clID) as ClientStatus;

            if (clStatus.Status != ClientStatus_t.Enabled)
            {
                EventLogger.Info("Tentative de démarrage d’un client non autorisé. Requête rejetée.");
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , msg.Data);
            }


            //reset dlg files                
            DialogEngin.WriteHubDialog(AppPaths.GetLocalClientDilogPath(clID) , clID , Enumerable.Empty<Message>());
            DialogEngin.WriteSrvDialog(AppPaths.GetLocalSrvDialogPath(clID) ,
                new ClientDialog(clID , clStatus.Status , Enumerable.Empty<Message>()));

            try
            {
                new NetEngin(AppContext.Settings.AppSettings).Upload(AppPaths.RemoteDialogDirUri ,
                    new string[] { AppPaths.GetLocalClientDilogPath(clID) , AppPaths.GetLocalSrvDialogPath(clID) });
            }
            catch (Exception ex)
            {
                EventLogger.Error(ex.Message);
                return null;    // let the cleint retry req.
            }


            EventLogger.Info($"Client démarré le {dtStart.Date.ToShortDateString()} à {dtStart.ToLongTimeString()}");

            //verifier si l'env du client a changé
            UpdateClientEnvironment(clID , clEnv);

            //ajouter client dans running liste
            var clData = new ClientData(dtStart);

            lock (m_runningClients)
                m_runningClients[clID] = clData;

            //maj du last seen
            clStatus.LastSeen = dtStart;
            m_ndxerClientsStatus.Source.Replace(m_ndxerClientsStatus.IndexOf(clID) , clStatus);
            ClientStarted?.Invoke(clID);

            return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Ok , msg.Data);
        }

        Message ProcessResumeConnectionReq(Message msg)
        {
            Dbg.Assert(msg.MessageCode == Message_t.Resume);

            EventLogger.Info("Réception d’une requête de reprise.");

            //verfier que le client existe
            uint clID = BitConverter.ToUInt32(msg.Data , 0);
            var client = m_ndxerClients.Get(clID) as HubClient;

            if (client == null)
            {
                EventLogger.Info("Réception d’une requête de reprise émanant d’un client non enregistré. Requête rejetée.");
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , BitConverter.GetBytes(clID));
            }

            var prf = m_ndxerProfiles.Get(client.ProfileID) as UserProfile;
            EventLogger.Info($"Requête de reprise émanant de {client.ContactName} pour le profil {prf.Name}, " +
                $"inscrit le {client.CreationTime}.");

            //verifier que le profil est en mode auto
            if ((m_ndxerProfilesMgmnt.Get(prf.ID) as ProfileManagementMode).ManagementMode == ManagementMode_t.Manual)
            {
                EventLogger.Info($"Le profil {prf.Name} est en gestion manuelle. Requête rejetée.");
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , msg.Data);
            }

            //appliquer l'alog. de selction du client
            // si date inscription client actif <= date inscription client en pause
            //      rejeter le demande
            //sinon
            //      bannir le client actif
            //      acvtiver le client en pause
            var curClient = GetProfileEnabledClient(prf.ID) as HubClient;

            if (curClient != null)
            {
                EventLogger.Info($"Le client actif {curClient.ContactName} inscrit le {curClient.CreationTime}.");

                if (curClient.CreationTime <= client.CreationTime)
                {
                    EventLogger.Info("Le client actif est plus ancien. Requête rejetée.");
                    return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , msg.Data);
                }

                //bannissemnt du client actif
                EventLogger.Info($"Le client demandeur est plus ancien. Bannissement du client actif ({curClient.ContactName})...");

                //maj de la table des statuts
                var curClStatus = m_ndxerClientsStatus.Get(curClient.ID) as ClientStatus;
                curClStatus.Status = ClientStatus_t.Banned;
                int ndxCurClient = m_ndxerClientsStatus.IndexOf(curClStatus.ID);
                m_ndxerClientsStatus.Source.Replace(ndxCurClient , curClStatus);

                //maj du fichier distant g
                string curClFilePath = AppPaths.GetLocalSrvDialogPath(curClient.ID);
                var curClDlg = new ClientDialog(curClient.ID , ClientStatus_t.Banned , Enumerable.Empty<Message>());
                DialogEngin.WriteSrvDialog(curClFilePath , curClDlg);
                AddUpload(Names.GetSrvDialogFile(curClient.ID));
            }



            //activation du client demandeur            

            //reset dlg files
            DialogEngin.WriteHubDialog(AppPaths.GetLocalClientDilogPath(clID) , clID , Enumerable.Empty<Message>());
            DialogEngin.WriteSrvDialog(AppPaths.GetLocalSrvDialogPath(clID) ,
                new ClientDialog(clID , ClientStatus_t.Enabled , Enumerable.Empty<Message>()));

            try
            {
                new NetEngin(AppContext.Settings.AppSettings).Upload(AppPaths.RemoteDialogDirUri ,
                    new string[] { AppPaths.GetLocalClientDilogPath(clID) , AppPaths.GetLocalSrvDialogPath(clID) });
            }
            catch (Exception ex)
            {
                EventLogger.Error(ex.Message);
                return null;    // let the cleint retry req.
            }


            //maj de la table des statuts
            var clStatus = m_ndxerClientsStatus.Get(clID) as ClientStatus;
            clStatus.Status = ClientStatus_t.Enabled;

            int ndxClient = m_ndxerClientsStatus.IndexOf(clID);
            m_ndxerClientsStatus.Source.Replace(ndxClient , clStatus);

            //maj du dic des clients actifs
            var clData = new ClientData(DateTime.Now);

            lock (m_runningClients)
                m_runningClients[clID] = clData;

            ClientStarted?.Invoke(clID);

            EventLogger.Info("Requête acceptée. :-)");
            return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Ok , msg.Data);
        }

        Message ProcessNewConnectionReq(Message msg)
        {
            Dbg.Assert(msg.MessageCode == Message_t.NewConnection);

            EventLogger.Info("Réception d’une nouvelle requête  d’inscription.");

            var ms = new MemoryStream(msg.Data);
            var reader = new RawDataReader(ms , Encoding.UTF8);
            ClientInfo clInfo = ClientInfo.LoadClientInfo(reader);
            ClientEnvironment clEnv = ClientEnvironment.Load(reader);

            byte[] data = BitConverter.GetBytes(clInfo.ClientID);
            var profile = m_ndxerProfiles.Get(clInfo.ProfileID) as UserProfile;

            string reqLog = $"Demande d’inscription émanant  de {clInfo.ContactName}" +
                $"(ID = {ClientStrID(clInfo.ClientID)}) pour " +
                (profile == null ? "un profil inexistant." :
                            $"le profil {profile.Name}.");

            EventLogger.Info(reqLog);


            //verifier que le profil existe
            if (profile == null)
            {
                EventLogger.Info("Lancement de la procédure d’actualisation  " +
                    "de la liste des profils sur le serveur");

                ProcessProfilesChange();
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.InvalidProfile , data);
            }


            //verifier que ClientID n'existe pas
            var clSameID = m_ndxerClients.Get(clInfo.ClientID) as HubClient;

            if (clSameID != null)
            {
                EventLogger.Info("Collision d’identifiants: " +
                    $"un client portant le même ID est déjà enregistré ({clSameID.ContactName}). " +
                    "Demande au client de reformuler son inscription avec un nouvel ID.");

                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.InvalidID , data);
            }


            //verifier que le profile est en mode auto 
            ManagementMode_t prfMgmntMode = GetProfileManagementMode(clInfo.ProfileID);

            if (prfMgmntMode == ManagementMode_t.Manual)
            {
                EventLogger.Info("Profil en gestion manuelle, inscription rejetée.");
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , data);
            }


            EventLogger.Info("Profil en gestion automatique.");
            EventLogger.Info("Enregistrement du client...");


            //desactiver l'ancien client actif si il existe
            var oldClient = GetProfileEnabledClient(clInfo.ProfileID);

            if (oldClient != null)
            {
                if (IsClientRunning(oldClient.ID))
                {
                    //rejeter l'inscription
                    EventLogger.Info($"Un client pour le profil {profile.Name} est déjà en cours d’exécution. " +
                        "requête rejetée.");

                    return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , data);
                }


                EventLogger.Info($"Désactivation du client {oldClient.ContactName}...");


                //maj la table des status clients                
                var oldClStatus = m_ndxerClientsStatus.Get(oldClient.ID) as ClientStatus;
                oldClStatus.Status = ClientStatus_t.Disabled;
                int ndx = m_ndxerClientsStatus.IndexOf(oldClStatus.ID);
                m_ndxerClientsStatus.Source.Replace(ndx , oldClStatus);


                //maj des fichiers de dialogue de old client
                string filePath = AppPaths.GetLocalSrvDialogPath(oldClient.ID);
                ClientDialog clDlg = new ClientDialog(oldClient.ID , ClientStatus_t.Disabled , Enumerable.Empty<Message>());
                DialogEngin.WriteSrvDialog(filePath , clDlg);
                AddUpload(Names.GetSrvDialogFile(oldClient.ID));
            }


            //creer le fichier dialogue 
            string srvDlgPath = AppPaths.GetLocalSrvDialogPath(clInfo.ClientID);
            DialogEngin.WriteSrvDialog(srvDlgPath , new ClientDialog(clInfo.ClientID ,
                 ClientStatus_t.Enabled , Enumerable.Empty<Message>()));
            DialogEngin.WriteHubDialog(AppPaths.GetLocalClientDilogPath(clInfo.ClientID) ,
                clInfo.ClientID , Enumerable.Empty<Message>());

            try
            {
                new NetEngin(AppContext.Settings.AppSettings).Upload(AppPaths.RemoteDialogDirUri ,
                    new string[] { AppPaths.GetLocalClientDilogPath(clInfo.ClientID) , AppPaths.GetLocalSrvDialogPath(clInfo.ClientID) });
            }
            catch (Exception ex)
            {
                EventLogger.Error(ex.Message);
                return null;    // let the cleint retry req.
            }


            //maj la table des clients
            var hClient = new HubClient(clInfo.ClientID , clInfo.ProfileID)
            {
                ContaclEMail = clInfo.ContaclEMail ,
                ContactName = clInfo.ContactName ,
                ContactPhone = clInfo.ContactPhone ,
            };

            m_ndxerClients.Source.Insert(hClient);


            //maj du status client
            var clStatus = new ClientStatus(clInfo.ClientID , ClientStatus_t.Enabled);
            m_ndxerClientsStatus.Source.Insert(clStatus);

            //maj du client env
            UpdateClientEnvironment(clInfo.ClientID , clEnv);


            //maj du dict des clients actifs
            var clData = new ClientData(DateTime.Now);

            lock (m_runningClients)
                m_runningClients[clInfo.ClientID] = clData;

            ClientStarted?.Invoke(clInfo.ClientID);

            EventLogger.Info("Inscription réussie. :-)");
            return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Ok , data);
        }

        Message ProcessUnknownMsg(Message msg , uint clientID)

        {
            Dbg.Assert(msg.MessageCode == Message_t.UnknonwnMsg);
            Dbg.Log("Processing unknown message.");

            EventLogger.Warning($"Reception d'un msg inconnu en provenance du client {ClientStrID(clientID)}.");

            return null;
        }

        Message ProcessNullMessage(Message msg , uint clientID) => null;

        Message ProcessSyncMessage(Message msg)
        {
            Dbg.Assert(msg.MessageCode == Message_t.Sync);

            var ms = new MemoryStream(msg.Data);
            var reader = new RawDataReader(ms , Encoding.UTF8);
            uint clID = reader.ReadUInt();
            uint srvMsgID = reader.ReadUInt();
            uint clMsgId = reader.ReadUInt();

            EventLogger.Info($"Reception d'un message de synchronisation du client {clID:X}.");

            ClientData clData;

            lock (m_runningClients)
                m_runningClients.TryGetValue(clID , out clData);

            if(clData != null)
            {
                clData.LiveTimeout = MAX_TTL;
                var resp = new Message(++clData.LastSrvMessageID , 0 , Message_t.Null);

                DialogEngin.AppendSrvDialog(AppPaths.GetLocalSrvDialogPath(clID) , resp);
                AddUpload(Names.GetSrvDialogFile(clID));

                //maj status
                var clStatus = m_ndxerClientsStatus.Get(clID) as ClientStatus;
                clStatus.LastSeen = DateTime.Now;
                ++clStatus.ReceivedMsgCount;
                m_ndxerClientsStatus.Source.Replace(m_ndxerClientsStatus.IndexOf(clID) , clStatus);

                return null;
            }

            var client = m_ndxerClients.Get(clID) as HubClient;

            if(client == null)
            {
                EventLogger.Warning("Réception d’une demande de synchronisation " +
                    $"de la part d’un client inexistant ({clID}). Bannissement du client.");

                //maj du fichier gov
                string srvDlgFile = AppPaths.GetLocalSrvDialogPath(clID);

                //le client n'existe pas => son fichier gov n'existe pas
                var clDlg = new ClientDialog(clID , ClientStatus_t.Banned , Enumerable.Empty<Message>());
                DialogEngin.WriteSrvDialog(srvDlgFile , clDlg);

                AddUpload(Path.GetFileName(srvDlgFile));
                return null;
            }


            var status = m_ndxerClientsStatus.Get(clID) as ClientStatus;                        
            var respMsg = new Message(++srvMsgID , 0 , Message_t.Null);
            var dlg = new ClientDialog(clID , status.Status , new Message[] { respMsg });
            DialogEngin.WriteSrvDialog(AppPaths.GetLocalSrvDialogPath(clID) , dlg); 
            AddUpload(Names.GetSrvDialogFile(clID));

            if(status.Status == ClientStatus_t.Enabled)
            {
                clData = new ClientData(DateTime.Now);
                clData.LastClientMessageID = clMsgId;
                clData.LastSrvMessageID = srvMsgID;

                lock (m_runningClients)
                    m_runningClients[clID] = clData;
            }

            return null;
        }

        Message ProcessSetInfoMessage(Message msg, uint clID)
        {
            Dbg.Assert(msg.MessageCode == Message_t.SetInfo);
            EventLogger.Info($"Réception d’une mise à jour des infos. Utilisateur du client {clID:X}.");

            int ndx = m_ndxerClients.IndexOf(clID);

            if(ndx < 0)
            {
                EventLogger.Warning("Client inexistant.  Requête ignorée.");
                return null;
            }

            //maj le client
            ClientInfo clInfo = new ClientInfo();
            clInfo.SetBytes(msg.Data);

            var client = new HubClient(clInfo.ClientID , clInfo.ProfileID)
            {
                ContaclEMail = clInfo.ContaclEMail ,
                ContactName = clInfo.ContactName ,
                ContactPhone = clInfo.ContactPhone ,
            };


            m_ndxerClients.Source.Replace(ndx , client);

            
            ClientData clData;
            lock (m_runningClients)
                m_runningClients.TryGetValue(clID , out clData);

            uint msgID = clData == null ? 1 : ++clData.LastClientMessageID;

            return msg.CreateResponse(msgID , Message_t.Ok);
        }
    }
}
