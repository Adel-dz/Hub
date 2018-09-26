using easyLib;
using System;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DLG
{
    public enum Message_t : byte
    {
        UnknonwnMsg = 0,
        NewConnection,  //in cnx, data = ClientInfo + ClientEnv, resp = {ok, rejected, InvalidID}
        Resume, //in cnx, data = ClientID, resp = {OK, Rejected}
        Ok, //in cnx, data = ClientID, no resp
        InvalidID,  //in cnx, data = ClientID, no resp                
        InvalidProfile, //in cnx, data = ClientID, no resp
        Rejected,   //in cnx, data = ClientID, no resp        
        SendInfo,    //in dlg, no data, resp = setInfo
        SetInfo,    //in dlg, data = ClientInfo, no resp
        Start,  //in cnx, data = clientID + clientEnv + start time , resp = {ok, rejected}
        Close,  //in dlg, no data, no resp
        Sync,    //in cnx dlg, data = clientid + srvMsgID + clientMsgID in cxn no data in dlg, resp = null in dlg
        Null, //in cnx dlg, no data, no resp,
        Log, //in dlg, data = time + is error + msg, no resp
        RunOnce, //in dlg, data = runonce action code, resp = null    
    }


    public sealed class Message : IStorable
    {
        public const uint NULL_ID = 0;

        Message()
        { }

        public Message(uint msgId , uint reqID , Message_t msg, byte[] data = null):
            this(msgId, reqID, msg, data, DateTime.Now)
        { }

        public Message(uint msgId, uint reqID, Message_t msg, byte[] data, DateTime timestamp)
        {
            ID = msgId;
            ReqID = reqID;
            MessageCode = msg;
            Timestamp = timestamp;
            Data = data;
        }

        public uint ID { get; private set; }
        public uint ReqID { get; private set; }
        public Message_t MessageCode { get; private set; }
        public DateTime Timestamp { get; private set; }
        public byte[] Data { get; private set; }

        public void Read(IReader reader)
        {
            ID = reader.ReadUInt();
            ReqID = reader.ReadUInt();
            byte msgCode = reader.ReadByte();            

            if (ID == 0 || msgCode == 0 || 
                    !Enum.IsDefined(typeof(Message_t) , msgCode))
                throw new CorruptedStreamException();

            MessageCode = (Message_t)msgCode;
            Timestamp = reader.ReadTime();
            int count = reader.ReadInt();
            Data = reader.ReadBytes(count);
        }

        public void Write(IWriter writer)
        {
            Assert(ID > 0);            
            Assert(MessageCode != Message_t.UnknonwnMsg);

            writer.Write(ID);
            writer.Write(ReqID);
            writer.Write((byte)MessageCode);
            writer.Write(Timestamp);           

            if(Data != null)
            {
                writer.Write(Data.Length);
                writer.Write(Data);
            }
            else
                writer.Write(0);
        }

        public Message CreateResponse(uint idMsg, Message_t respCode, byte[] data = null) => 
            new Message(idMsg , ID , respCode, data);

        public static Message LoadMessage(IReader reader)
        {
            var cxnMsg = new Message();
            cxnMsg.Read(reader);

            return cxnMsg;
        }        
    }
}
