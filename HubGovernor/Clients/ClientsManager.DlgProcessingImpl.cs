using DGD.HubCore;
using DGD.HubCore.DLG;
using DGD.HubCore.Net;
using DGD.HubGovernor.Profiles;
using easyLib;
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
                return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Rejected , msg.Data);
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

                //maj du fichier g
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

            //maj du fichier h + ajouter le client a la table des clients actifs
            var clData = new ClientData(DateTime.Now);
            string hubFilePath = AppPaths.GetLocalClientDilogPath(clID);

            try
            {
                
                new NetEngin(AppContext.Settings.AppSettings).Download(hubFilePath,                    
                    AppPaths.GetRemoteClientDialogUri(clID) ,
                    true);

                IEnumerable<Message> msgs = DialogEngin.ReadHubDialog(hubFilePath , clID);

                if (msgs.Any())
                    clData.LastHandledMessageID = msgs.Max(m => m.ID);
            }
            catch(Exception ex)
            {
                DialogEngin.WriteHubDialog(hubFilePath , clID , Enumerable.Empty<Message>());
                EventLogger.Error(ex.Message);
            }

            m_runningClients[clID] = clData;

            AddUpload(Names.GetSrvDialogFile(clID));            

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
                $"(ID = {ClientStrID(clInfo.ClientID)} pour " +
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

            //enregister le client 
            AddClient(clInfo);

            //maj du dict des clients actifs
            var clData = new ClientData(DateTime.Now);
            m_runningClients[clInfo.ClientID] = clData;

            EventLogger.Info("Inscription réussie. :-)");
            return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.Ok , data);
        }

        Message ProcessUnknownMsg(Message msg , uint clientID)
        {
            Dbg.Assert(msg.MessageCode == Message_t.UnknonwnMsg);
            Dbg.Log("Processing unknown message.");

            EventLogger.Warning($"Reception d'un msg inconnu en provenance du client {ClientStrID(clientID)}.");

            return msg.CreateResponse(++m_lastCnxRespMsgID , Message_t.UnknonwnMsg ,
                BitConverter.GetBytes(clientID));
        }
    }
}
