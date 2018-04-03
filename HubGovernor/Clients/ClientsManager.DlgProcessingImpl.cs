using DGD.HubCore;
using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using DGD.HubGovernor.Log;
using DGD.HubGovernor.Profiles;
using easyLib;
using System;
using System.IO;
using System.Linq;
using System.Text;


namespace DGD.HubGovernor.Clients
{
    partial class ClientsManager
    {
        Message ProcessCloseMessage(Message msg , uint clID)
        {            
            Dbg.Assert(msg.MessageCode == Message_t.Close);

            AppContext.LogManager.LogSysActivity($"Réception d'une notification de déconnexion {ClientStrID(clID)}", true);

            DateTime dt = DateTime.Now;
            AppContext.LogManager.LogSysActivity($"client {ClientStrID(clID)} arrêté le {dt.ToShortDateString()} à {dt.ToLongTimeString()}", true);

            AppContext.LogManager.LogClientActivity(clID , "Déconnexion");
            m_onlineClients.Remove(clID);            
            AppContext.LogManager.CloseLogger(clID);
            ClientClosed?.Invoke(clID);

            return null;
        }

        Message ProcessStartMessage(Message msg)
        {
            Dbg.Assert(msg.MessageCode == Message_t.Start);

            var reader = new RawDataReader(new MemoryStream(msg.Data) , Encoding.UTF8);
            uint clID = reader.ReadUInt();
            ClientEnvironment clEnv = ClientEnvironment.Load(reader);
            DateTime dtStart = reader.ReadTime();

            var client = m_ndxerClients.Get(clID) as HubClient;

            if (client == null)
            {
                AppContext.LogManager.LogSysActivity("Réception d’une notification de démarrage " +
                  $"de la part d’un client inexistant ({ClientStrID(clID)}). Bannissement du client", true);

                //maj du fichier gov
                string srvDlgFile = AppPaths.GetLocalSrvDialogPath(clID);

                //le client n'existe pas => son fichier gov n'existe pas
                var clDlg = new ClientDialog(clID , ClientStatus_t.Banned , Enumerable.Empty<Message>());
                DialogEngin.WriteSrvDialog(srvDlgFile , clDlg);

                AddUpload(Path.GetFileName(srvDlgFile));
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , msg.Data);
            }


            AppContext.LogManager.LogSysActivity("Réception d’une notification de démarrage " +
                    $"de la part du client {ClientStrID(clID)}", true);

            //verifier le statut du client
            var clStatus = m_ndxerClientsStatus.Get(clID) as ClientStatus;

            if (clStatus.Status != ClientStatus_t.Enabled)
            {
                AppContext.LogManager.LogSysActivity($"Démmarage du Client { ClientStrID(clID)} non autorisé. Requête rejetée", true);
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
                AppContext.LogManager.LogSysError($"Traitement de la requête de démarrage du client {ClientStrID(clID)}: " +
                    $"{ ex.Message}. demande ignorée, laisser le client reformuler la requête.", true);

                return null;    // let the cleint retry req.
            }


            AppContext.LogManager.LogSysActivity($"Client {ClientStrID(clID)} démarré le { dtStart.Date.ToShortDateString()} " +
                $"à { dtStart.ToLongTimeString()}", true);

            //verifier si l'env du client a changé
            UpdateClientEnvironment(clID , clEnv);

            //ajouter client dans running liste
            m_onlineClients.Add(clID , dtStart);
            AppContext.LogManager.StartLogger(clID);
            AppContext.LogManager.LogClientActivity(clID , "Démarrage");

            //maj du last seen
            clStatus.LastSeen = dtStart;
            m_ndxerClientsStatus.Source.Replace(m_ndxerClientsStatus.IndexOf(clID) , clStatus);
            ClientStarted?.Invoke(clID);

            return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Ok , msg.Data);
        }

        Message ProcessResumeConnectionReq(Message msg)
        {
            Dbg.Assert(msg.MessageCode == Message_t.Resume);


            //verfier que le client existe
            uint clID = BitConverter.ToUInt32(msg.Data , 0);
            var client = m_ndxerClients.Get(clID) as HubClient;

            if (client == null)
            {
                AppContext.LogManager.LogSysActivity("Réception d’une requête de reprise émanant d’un client non enregistré. Requête rejetée.", true);
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , BitConverter.GetBytes(clID));
            }

            var prf = m_ndxerProfiles.Get(client.ProfileID) as UserProfile;

            AppContext.LogManager.LogSysActivity($"Requête de reprise émanant de {ClientStrID(clID)} pour le profil " +
                $"{prf.Name}, inscrit le {client.CreationTime}", true);


            //verifier que le profil est en mode auto
            if ((m_ndxerProfilesMgmnt.Get(prf.ID) as ProfileManagementMode).ManagementMode == ManagementMode_t.Manual)
            {
                AppContext.LogManager.LogSysActivity($"Le profil {prf.Name} est en gestion manuelle. Requête de reprise rejetée", true);
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
                if (curClient.CreationTime <= client.CreationTime)
                {
                    AppContext.LogManager.LogSysActivity($"Le client actif {ClientStrID(curClient.ID)} inscrit le " +
                        $"{curClient.CreationTime}. Le client actif est plus ancien. Requête de reprise rejetée", true);

                    return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , msg.Data);
                }

                //bannissemnt du client actif
                AppContext.LogManager.LogSysActivity($"Le client actif {ClientStrID(curClient.ID)} inscrit le " +
                        $"{curClient.CreationTime}. Le client demandeur de reprise est plus ancien. Bannissement du client actif", true);

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
                AppContext.LogManager.LogSysError($"Traitement de la requête de reprise du client {ClientStrID(clID)}: " +
                        $"{ ex.Message}. demande ignorée, laisser le client reformuler la requête", true);

                return null;    // let the cleint retry req.
            }


            //maj de la table des statuts
            var clStatus = m_ndxerClientsStatus.Get(clID) as ClientStatus;
            clStatus.Status = ClientStatus_t.Enabled;

            int ndxClient = m_ndxerClientsStatus.IndexOf(clID);
            m_ndxerClientsStatus.Source.Replace(ndxClient , clStatus);

            //maj du dic des clients actifs
            m_onlineClients.Add(clID);
            AppContext.LogManager.StartLogger(clID);
            AppContext.LogManager.LogClientActivity(clID , "Reprise.");
            ClientStarted?.Invoke(clID);

            AppContext.LogManager.LogSysActivity($"Demande de reprise du client {ClientStrID(clID)} acceptée", true);
            return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Ok , msg.Data);
        }

        Message ProcessNewConnectionReq(Message msg)
        {
            Dbg.Assert(msg.MessageCode == Message_t.NewConnection);

            //TextLogger.Info("Réception d’une nouvelle requête  d’inscription.");

            var ms = new MemoryStream(msg.Data);
            var reader = new RawDataReader(ms , Encoding.UTF8);
            ClientInfo clInfo = ClientInfo.LoadClientInfo(reader);
            ClientEnvironment clEnv = ClientEnvironment.Load(reader);

            byte[] data = BitConverter.GetBytes(clInfo.ClientID);
            var profile = m_ndxerProfiles.Get(clInfo.ProfileID) as UserProfile;

            string reqLog = $"Réception d'une demande d’inscription émanant  de {clInfo.ContactName}" +
                $"(ID = {ClientStrID(clInfo.ClientID)}) pour " +
                (profile == null ? "un profil inexistant." :
                            $"le profil {profile.Name}.");

            AppContext.LogManager.LogSysActivity(reqLog, true);


            //verifier que le profil existe
            if (profile == null)
            {
                AppContext.LogManager.LogSysActivity("Lancement de la procédure d’actualisation  " +
                    "de la liste des profils sur le serveur", true);

                ProcessProfilesChange();
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.InvalidProfile , data);
            }


            //verifier que ClientID n'existe pas
            var clSameID = m_ndxerClients.Get(clInfo.ClientID) as HubClient;

            if (clSameID != null)
            {
                AppContext.LogManager.LogSysActivity("Collision d’identifiants: " +
                    $"un client portant le même ID est déjà enregistré ({clSameID.ContactName}). " +
                    "Exiger au client de reformuler son inscription avec un nouvel ID", true);
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.InvalidID , data);
            }


            //verifier que le profile est en mode auto 
            ManagementMode_t prfMgmntMode = GetProfileManagementMode(clInfo.ProfileID);

            if (prfMgmntMode == ManagementMode_t.Manual)
            {
                AppContext.LogManager.LogSysActivity("Profil en gestion manuelle, inscription rejetée.", true);
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , data);
            }


            TextLogger.Info("Profil en gestion automatique.");
            TextLogger.Info("Enregistrement du client...");


            //desactiver l'ancien client actif si il existe
            var oldClient = GetProfileEnabledClient(clInfo.ProfileID);

            if (oldClient != null)
            {
                if (IsClientRunning(oldClient.ID))
                {
                    //rejeter l'inscription
            
                    AppContext.LogManager.LogSysActivity($"Un client pour le profil {profile.Name} est déjà en cours d’exécution. " +
                        "Inscription rejetée", true);

                    return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , data);
                }


                AppContext.LogManager.LogSysActivity($"Désactivation du client {ClientStrID(oldClient.ID)}", true);


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
                AppContext.LogManager.LogSysError($"Traitement de la requête d'inscription du client {ClientStrID(clInfo.ClientID)}: " +
                    $"{ ex.Message}. demande ignorée, laisser le client reformuler la requête", true);
                                
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
            m_onlineClients.Add(clInfo.ClientID);
            AppContext.LogManager.StartLogger(clInfo.ClientID);
            AppContext.LogManager.LogClientActivity(clInfo.ClientID , "Inscription");
            ClientStarted?.Invoke(clInfo.ClientID);

            
            AppContext.LogManager.LogSysActivity($"Inscription du client {clInfo.ClientID} terminée", true);
            TextLogger.Info("Inscription réussie. :-)");
            return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Ok , data);
        }

        Message ProcessUnknownMsg(Message msg , uint clientID)

        {
            Dbg.Assert(msg.MessageCode == Message_t.UnknonwnMsg);
            Dbg.Log("Processing unknown message.");

            AppContext.LogManager.LogSysActivity($"Reception d'un message inconnu en provenance du client {ClientStrID(clientID)}. Message ignoré.", true);

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

            AppContext.LogManager.LogSysActivity($"Reception d'un message de synchronisation du client {ClientStrID(clID)}", true);

            ActiveClientsQueue.IClientData clData = m_onlineClients.Get(clID);

            if(clData != null)
            {
                clData.TimeToLive = ActiveClientsQueue.InitTimeToLive;
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
                AppContext.LogManager.LogSysActivity("Réception d’une demande de synchronisation " +
                        $"de la part d’un client inexistant ({ClientStrID(clID)}). Bannissement du client", true);

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
                ActiveClientsQueue.IClientData clientData = m_onlineClients.Add(clID);                
                clientData.LastClientMessageID = clMsgId;
                clientData.LastSrvMessageID = srvMsgID;

                AppContext.LogManager.StartLogger(clID);
            }

            return null;
        }

        Message ProcessSetInfoMessage(Message msg, uint clID)
        {
            Dbg.Assert(msg.MessageCode == Message_t.SetInfo);

            AppContext.LogManager.LogSysActivity($"Réception d’une mise à jour des informations Utilisateur du client {ClientStrID(clID)}", true);

            int ndx = m_ndxerClients.IndexOf(clID);

            if(ndx < 0)
            {
                AppContext.LogManager.LogSysActivity("Mise à jour des information utilisateur d'un client inexistant.  Requête ignorée.", true);
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


            ActiveClientsQueue.IClientData clData = m_onlineClients.Get(clID);
            uint msgID = clData == null ? 1 : ++clData.LastClientMessageID;

            return msg.CreateResponse(msgID , Message_t.Ok);
        }

        Message ProcessLogMessage(Message msg , uint clID)
        {
            Dbg.Assert(msg.MessageCode == Message_t.Log);

            var ms = new MemoryStream(msg.Data);
            var reader = new RawDataReader(ms , Encoding.UTF8);

            DateTime tm = reader.ReadTime();
            bool isErr = reader.ReadBoolean();
            string txt = reader.ReadString();

            if (isErr)
            {
                AppContext.LogManager.LogSysActivity($"Réception log d'erreur du client {ClientStrID(clID)}");
                AppContext.LogManager.LogClientError(clID , txt , tm);
            }
            else
            {
                AppContext.LogManager.LogSysActivity($"Réception d'un log d'activité du client {ClientStrID(clID)}");
                AppContext.LogManager.LogClientActivity(clID , txt , tm);
            }

            return null;
        }
    }
}
