using DGD.HubCore.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easyLib;

namespace DGD.HubGovernor.Log
{
    enum EventType_t : byte
    {
        Activity,
        Error
    }

    interface IEventLog: IDataRow
    {
        EventType_t EventType { get; }
        DateTime Time { get; }
        string Text { get; }
    }


    sealed class EventLog: DataRow, IEventLog
    {

        public EventLog(uint id, string txt, EventType_t evType, DateTime tm):
            base(id)
        {
            EventType = evType;
            Time = tm;
            Text = txt ?? "";
        }

        public EventLog(uint id, string txt, EventType_t evType = EventType_t.Activity):
            this(id, txt, evType, DateTime.Now)
        { }

        public EventLog()
        {
            Text = "";
        }


        public EventType_t EventType { get; private set; }
        public string Text { get; private set; }
        public DateTime Time { get; private set; }

        public static string GetEventTypeName(EventType_t evType)
        {
            if (evType == EventType_t.Error)
                return "Erreur";

            return "Activité";
        }

        //protected:
        protected override void DoRead(IReader reader)
        {
            byte evType = reader.ReadByte();
            
            if(!Enum.IsDefined(typeof(EventType_t), evType))            
                throw new CorruptedStreamException();

            EventType = (EventType_t)evType;
            Time = reader.ReadTime();
            Text = reader.ReadString();
        }

        protected override void DoWrite(IWriter writer)
        {
            writer.Write((byte)EventType);
            writer.Write(Time);
            writer.Write(Text);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                Time.ToString(),
                GetEventTypeName(EventType),
                Text
            };
        }
    }
}
