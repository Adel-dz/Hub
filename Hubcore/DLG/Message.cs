using easyLib;
using System;
using static System.Diagnostics.Debug;


namespace DGD.HubCore.DLG
{
    public enum Message_t : byte
    {
        UnknonwnMsg = 0,
        NewConnection,  //data = ClientInfo, resp = {ok, rejected, InvalidID}
        Resume, //data = ClientID, resp = {OK, Rejected}
        Ok, //data = ClientID pour cxn
        InvalidID,  //data = ClientID                
        InvalidProfile, //data = ClientID
        Rejected,   //data = ClientID        
        SendInfo,    //data = ClientID
        SetInfo,    //data = ClientInfo
        Start,  //data = clientID + clientEnv + start time , resp = ok
        Close,  //data = ClientID + close time, no resp
        Sync,    //data = client id in cxn nothing in dlg, resp = ok
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
