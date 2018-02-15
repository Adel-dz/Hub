using DGD.HubCore.DB;
using System;
using easyLib;
using static System.Diagnostics.Debug;
using DGD.HubCore.Updating;
using System.Collections.Generic;

namespace DGD.HubGovernor.Updating
{
    interface ITransaction: IDataRow
    {
        ActionCode_t Action { get; }
        uint TableID { get; }
        uint RowID { get; }
        DateTime ActionTime { get; }
    }


    sealed class Transaction: DataRow, ITransaction
    {
        static Dictionary<ActionCode_t , string> m_actionsName;


        static Transaction()
        {
            m_actionsName = new Dictionary<ActionCode_t , string>()
            {
                { ActionCode_t.AddRow, "Ajout" } ,
                { ActionCode_t.DeleteRow, "Suppression" },
                { ActionCode_t.ReplaceRow, "Modification" },
                { ActionCode_t.ResetTable, "RAZ" }
            };
        }

        public Transaction(uint id , uint idTable , uint idRow , ActionCode_t action , DateTime actionTime) :
            base(id)
        {
            Assert(idTable > 0);
            Assert(idRow > 0);

            TableID = idTable;
            RowID = idRow;
            Action = action;
            ActionTime = actionTime;
        }

        public Transaction(uint id , uint idTable , uint idRow , ActionCode_t action) :
            this(id , idTable , idRow , action , DateTime.Now)
        { }

        public Transaction()
        { }


        public ActionCode_t Action { get; private set; }
        public DateTime ActionTime { get; private set; }
        public uint RowID { get; private set; }
        public uint TableID { get; private set; }
        public static int Size => sizeof(uint) * 3 + sizeof(byte) + sizeof(long);

        public override string ToString() => $"(ID:{ID}, Action:{Action}, Table:{AppContext.TableManager.GetTable(TableID).Name}, " +
            $"ID Ligne:{RowID}, Date:{ActionTime})";


        //protected:
        protected override void DoRead(IReader reader)
        {
            byte action = reader.ReadByte();
            RowID = reader.ReadUInt();
            TableID = reader.ReadUInt();

            if (!Enum.IsDefined(typeof(ActionCode_t) , action) || action == (byte)ActionCode_t.None ||
                    RowID == 0 || TableID == 0)
                throw new CorruptedStreamException();

            Action = (ActionCode_t)action;
            ActionTime = reader.ReadTime();
        }

        protected override void DoWrite(IWriter writer)
        {
            Assert(Action != ActionCode_t.None);
            writer.Write((byte)Action);

            Assert(RowID > 0);
            writer.Write(RowID);

            Assert(TableID > 0);
            writer.Write(TableID);

            writer.Write(ActionTime);
        }

        protected override string[] GetContent()
        {
            return new[]
            {
                ID.ToString(),
                ActionTime.ToString(),
                m_actionsName[Action],
                AppContext.TableManager.GetTable(TableID).Name,
                RowID.ToString()
            };
        }
    }
}
