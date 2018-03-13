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
        Message ProcessCloseMessage(Message msg)
        {
            EventLogger.Info("Réception d'une notification de fermeture.");

            Dbg.Assert(msg.MessageCode == Message_t.Close);

            m_lastCxnReqMsgID = msg.ID;

            var reader = new RawDataReader(new MemoryStream(msg.Data) , Encoding.UTF8);
            uint clID = reader.ReadUInt();

            var client = m_ndxerClients.Get(clID) as HubClient;

            if (client == null)
            {
                EventLogger.Warning("Réception d’une notification de démarrage " +
                    $"de la part d’un client inexistant ({clID}).");

                return null;
            }

            EventLogger.Info($"Réception d’une notification de fermeture de la part du client {client.ContactName}");

            //maj du dic des clients actifs
            DateTime dt = reader.ReadTime();

            EventLogger.Info($"client arrêté le {dt.ToShortDateString()} à {dt.ToLongTimeString()}");


            var clData = new ClientData(dt);
            string hubFilePath = AppPaths.GetLocalClientDilogPath(clID);

            //TODO: traiter le cas ou un client de meme ID est en execution
            m_runningClients.Remove(clID);

            ClientClosed?.Invoke(clID);

            return null;
        }

        Message ProcessStartMessage(Message msg)
        {
            EventLogger.Info("Réception d'une notification de démarrage.");

            Dbg.Assert(msg.MessageCode == Message_t.Start);

            m_lastCxnReqMsgID = msg.ID;

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

                AddUpload(srvDlgFile);
                return null;
            }

            EventLogger.Info("Réception d’une notification de démarrage " +
                    $"de la part du client {client.ContactName}");

            //verifier le statut du client
            var clStatus = m_ndxerClientsStatus.Get(clID) as ClientStatus;

            if (clStatus.Status != ClientStatus_t.Enabled)
            {
                EventLogger.Info("Tentative de démarrage d’un client non autorisé. Requête rejetée.");

                string dlgFile = AppPaths.GetLocalSrvDialogPath(clID);

                try
                {
                    ClientDialog clDlg = DialogEngin.ReadSrvDialog(dlgFile);
                    clDlg.ClientStatus = clStatus.Status;
                    DialogEngin.WriteSrvDialog(dlgFile , clDlg);
                }
                catch (Exception ex)
                {
                    EventLogger.Error(ex.Message);

                    var clDlg = new ClientDialog(clID , clStatus.Status , Enumerable.Empty<Message>());
                    DialogEngin.WriteSrvDialog(dlgFile , clDlg);
                }

                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , msg.Data);
            }



            EventLogger.Info($"Client démarré le {dtStart.Date.ToShortDateString()} à {dtStart.ToLongTimeString()}");

            //verifier si l'env du client a changé
            UpdateClientEnvironment(clID , clEnv);

            //ajouter client dans running liste
            var clData = new ClientData(dtStart);

            //reset dlg file                
            DialogEngin.WriteSrvDialog(AppPaths.GetLocalSrvDialogPath(clID) ,
                new ClientDialog(clID , clStatus.Status , Enumerable.Empty<Message>()));

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

            m_lastCxnReqMsgID = msg.ID;

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
            var curClient = GetProfileActiveClient(prf.ID) as HubClient;

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
                var curClStatus = new ClientStatus(curClient.ID , ClientStatus_t.Banned);
                int ndxCurClient = m_ndxerClientsStatus.IndexOf(curClStatus.ID);
                m_ndxerClientsStatus.Source.Replace(ndxCurClient , curClStatus);

                //maj du fichier distant g
                string curClFilePath = AppPaths.GetLocalSrvDialogPath(curClient.ID);
                try
                {
                    ClientDialog curClDlg = DialogEngin.ReadSrvDialog(curClFilePath);
                    curClDlg.ClientStatus = ClientStatus_t.Banned;
                    DialogEngin.WriteSrvDialog(curClFilePath , curClDlg);
                }
                catch (Exception ex)
                {
                    EventLogger.Error(ex.Message);

                    var curClDlg = new ClientDialog(curClient.ID , ClientStatus_t.Banned , Enumerable.Empty<Message>());
                    DialogEngin.WriteSrvDialog(curClFilePath , curClDlg);
                }

                AddUpload(Names.GetSrvDialogFile(curClient.ID));
            }


            //activation du client demandeur            

            //maj de la table des statuts
            var clStatus = new ClientStatus(clID , ClientStatus_t.Enabled);
            int ndxClient = m_ndxerClientsStatus.IndexOf(clID);
            m_ndxerClientsStatus.Source.Replace(ndxClient , clStatus);

            //maj du fichier g
            string clFilePath = AppPaths.GetLocalSrvDialogPath(clID);

            try
            {
                ClientDialog clDlg = DialogEngin.ReadSrvDialog(clFilePath);
                clDlg.ClientStatus = ClientStatus_t.Enabled;
                DialogEngin.WriteSrvDialog(clFilePath , clDlg);
            }
            catch (Exception ex)
            {
                EventLogger.Error(ex.Message);
                var clDlg = new ClientDialog(clID , ClientStatus_t.Enabled , Enumerable.Empty<Message>());
                DialogEngin.WriteSrvDialog(clFilePath , clDlg);
            }

            AddUpload(Names.GetSrvDialogFile(clID));

            //maj du dic des clients actifs
            var clData = new ClientData(DateTime.Now);
            string hubFilePath = AppPaths.GetLocalClientDilogPath(clID);

            try
            {
                IEnumerable<Message> msgs = DialogEngin.ReadHubDialog(hubFilePath , clID);

                if (msgs.Any())
                    clData.LastInMessageID = msgs.Max(m => m.ID);
            }
            catch (Exception ex)
            {
                DialogEngin.WriteHubDialog(hubFilePath , clID , Enumerable.Empty<Message>());
                EventLogger.Error(ex.Message);
            }

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

            byte[] data = BitConverter.GetBytes(clInfo.ClientID);
            var profile = m_ndxerProfiles.Get(clInfo.ProfileID) as UserProfile;

            string reqLog = $"Demande d’inscription émanant  de {clInfo.ContactName}" +
                $"(ID = {ClientStrID(clInfo.ClientID)}) pour " +
                (profile == null ? "un profil inexistant." :
                            $"le profil {profile.Name}.");

            EventLogger.Info(reqLog);

            m_lastCxnReqMsgID = msg.ID;

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
            var oldClient = GetProfileActiveClient(clInfo.ProfileID);

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

            //maj du dict des clients actifs
            var clData = new ClientData(DateTime.Now);
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
    }
}
