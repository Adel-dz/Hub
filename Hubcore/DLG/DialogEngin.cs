using easyLib;
using easyLib.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DLG
{
    public static class DialogEngin
    {
        const string PROFILE_SIGNATURE = "USERPROF1";
        const string SRV_CXN_SIGNATURE = "CXNSRV1";
        const string CLIENT_CXN_SIGNATURE = "CXNCLIENT1";
        const string SRV_MSG_QUEUE_SIGNATURE = "SRVMSG1";
        const string SRV_DIALOG_SIGNATURE = "GMSG1";
        const string HUB_DIALOG_SIGNATURE = "HMSG1";


        public static void WriteProfiles(string filePath , IEnumerable<ProfileInfo> profiles)
        {
            Assert(profiles != null);

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.Create(filePath))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);

                writer.Write(ProfileSignature);

                writer.Write(profiles.Count());

                foreach (ProfileInfo prInfo in profiles)
                    prInfo.Write(writer);
            }
        }

        public static IEnumerable<ProfileInfo> ReadProfiles(string filePath)
        {
            byte[] sign = ProfileSignature;

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);

                foreach (byte b in sign)
                    if (reader.ReadByte() != b)
                        throw new CorruptedFileException(filePath);

                int count = reader.ReadInt();
                var prInfos = new List<ProfileInfo>(count);

                for (int i = 0; i < count; ++i)
                    prInfos.Add(ProfileInfo.LoadProfileInfo(reader));

                return prInfos;
            }
        }
        
        public static IEnumerable<Message> ReadConnectionsReq(string filePath)
        {
            byte[] sign = HubCxnSignature;

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);

                foreach (byte b in sign)
                    if (reader.ReadByte() != b)
                        throw new CorruptedFileException(filePath);

                int count = reader.ReadInt();
                var cxns = new List<Message>(count);

                for (int i = 0; i < count; ++i)
                    cxns.Add(Message.LoadMessage(reader));

                return cxns;
            }
        }
        
        public static void WriteConnectionsReq(string filePath , IEnumerable<Message> messages)
        {
            Assert(messages != null);

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.Create(filePath))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);

                writer.Write(HubCxnSignature);

                writer.Write(messages.Count());

                foreach (Message msg in messages)
                    msg.Write(writer);
            }
        }

        public static void AppendConnectionsReq(string filePath , IEnumerable<Message> messages)
        {
            Assert(messages != null);

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.Open(filePath , FileMode.Open , FileAccess.ReadWrite))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);
                int countPos = HubCxnSignature.Length; ;
                fs.Position = countPos;

                int msgCount = reader.ReadInt();

                var writer = new RawDataWriter(fs , Encoding.UTF8);
                fs.Seek(0 , SeekOrigin.End);

                foreach (Message msg in messages)
                {
                    msg.Write(writer);
                    ++msgCount;
                }

                fs.Position = countPos;
                writer.Write(msgCount);
            }
        }

        public static void AppendConnectionsResp(string filePath , IEnumerable<Message> messages)
        {
            Assert(messages != null);

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.Open(filePath , FileMode.Open , FileAccess.ReadWrite))
            {

                var reader = new RawDataReader(fs , Encoding.UTF8);
                int countPos = SrvCxnSignature.Length; ;
                fs.Position = countPos;

                int msgCount = reader.ReadInt();

                var writer = new RawDataWriter(fs , Encoding.UTF8);
                fs.Seek(0 , SeekOrigin.End);

                foreach (Message msg in messages)
                {
                    msg.Write(writer);
                    ++msgCount;
                }

                fs.Position = countPos;
                writer.Write(msgCount);
            }
        }

        public static IEnumerable<Message> ReadConnectionsResp(string filePath)
        {
            byte[] sign = SrvCxnSignature;

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);

                foreach (byte b in sign)
                    if (reader.ReadByte() != b)
                        throw new CorruptedFileException(filePath);

                int count = reader.ReadInt();
                var cxns = new List<Message>(count);

                for (int i = 0; i < count; ++i)
                    cxns.Add(Message.LoadMessage(reader));

                return cxns;
            }
        }

        public static void WriteConnectionsResp(string filePath , IEnumerable<Message> messages)
        {
            Assert(messages != null);

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.Create(filePath))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);

                writer.Write(SrvCxnSignature);

                writer.Write(messages.Count());

                foreach (Message msg in messages)
                    msg.Write(writer);
            }
        }

        public static void WriteHubDialog(string filePath , uint clID, IEnumerable<Message> messages)
        {
            Assert(messages != null);

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.Create(filePath))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);

                writer.Write(HubDialogSignature);
                writer.Write(clID);

                writer.Write(messages.Count());

                foreach (Message msg in messages)
                    msg.Write(writer);
            }
        }

        public static void AppendHubDialog(string filePath, uint clID, IEnumerable<Message> msgs)
        {
            Assert(msgs != null);

            using (FileLocker.Lock(filePath))
            {
                IEnumerable<Message> prevMsgs = ReadHubDialog(filePath , clID);
                WriteHubDialog(filePath , clID , prevMsgs.Concat(msgs));
            }

        }

        public static void AppendHubDialog(string filePath , uint clID , Message msg)
        {
            Assert(msg != null);

            using (FileLocker.Lock(filePath))
            {
                IEnumerable<Message> prevMsgs = ReadHubDialog(filePath , clID);
                WriteHubDialog(filePath , clID , prevMsgs.Add(msg));
            }

        }

        public static IEnumerable<Message> ReadHubDialog(string filePath, uint clID)
        {
            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);
                byte[] sign = HubDialogSignature;

                foreach (byte b in sign)
                    if (reader.ReadByte() != b)
                        throw new CorruptedFileException(filePath);

                uint id = reader.ReadUInt();

                if (id != clID)
                    throw new CorruptedFileException(filePath);

                int count = reader.ReadInt();
                var msgs = new List<Message>(count);

                for (int i = 0; i < count; ++i)
                    msgs.Add(Message.LoadMessage(reader));

                return msgs;
            }
        }

        public static void WriteSrvDialog(string filePath , ClientDialog clDlg)
        {
            Assert(clDlg != null);

            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.Create(filePath))
            {
                var writer = new RawDataWriter(fs , Encoding.UTF8);

                writer.Write(SrvDialogSignature);
                clDlg.Write(writer);
            }
        }

        public static void AppendSrvDialog(string filePath, Message msg)
        {
            Assert(msg != null);

            using (FileLocker.Lock(filePath))
            {
                ClientDialog clDlg = ReadSrvDialog(filePath);
                var newDlg = new ClientDialog(clDlg.ClientID , clDlg.ClientStatus , clDlg.Messages.Add(msg));

                WriteSrvDialog(filePath , newDlg);
            }
        }

        public static void AppendSrvDialog(string filePath , IEnumerable<Message> msgs)
        {
            Assert(msgs != null);

            using (FileLocker.Lock(filePath))
            {
                ClientDialog clDlg = ReadSrvDialog(filePath);
                var newDlg = new ClientDialog(clDlg.ClientID , clDlg.ClientStatus , clDlg.Messages.Concat(msgs));

                WriteSrvDialog(filePath , newDlg);
            }
        }

        public static ClientDialog ReadSrvDialog(string filePath)
        {
            using (FileLocker.Lock(filePath))
            using (FileStream fs = File.OpenRead(filePath))
            {
                var reader = new RawDataReader(fs , Encoding.UTF8);
                byte[] sign = SrvDialogSignature;

                foreach (byte b in sign)
                    if (reader.ReadByte() != b)
                        throw new CorruptedFileException(filePath);

                return ClientDialog.LoadClientDialog(reader);
            }
        }
                
        
        //private:
        static byte[] ProfileSignature => Encoding.UTF8.GetBytes(PROFILE_SIGNATURE);
        static byte[] HubCxnSignature => Encoding.UTF8.GetBytes(CLIENT_CXN_SIGNATURE);
        static byte[] SrvCxnSignature => Encoding.UTF8.GetBytes(SRV_CXN_SIGNATURE);
        static byte[] SrvDialogSignature => Encoding.UTF8.GetBytes(SRV_DIALOG_SIGNATURE);
        static byte[] HubDialogSignature => Encoding.UTF8.GetBytes(HUB_DIALOG_SIGNATURE);
    }
}
