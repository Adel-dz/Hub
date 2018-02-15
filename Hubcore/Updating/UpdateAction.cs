using DGD.HubCore.DB;
using easyLib;
using static System.Diagnostics.Debug;




namespace DGD.HubCore.Updating
{

    public enum ActionCode_t: byte
    {
        None,
        ResetTable,
        DeleteRow,
        ReplaceRow,
        AddRow
    }


    public interface IUpdateAction: IStorable
    {
        ActionCode_t Code { get; }
    }


    public class ResetTable: IUpdateAction
    {
        public ActionCode_t Code => ActionCode_t.ResetTable;

        public void Read(IReader reader)
        { }

        public void Write(IWriter writer)
        { }
    }


    public class DeleteRow: IUpdateAction
    {
        public DeleteRow(uint rowID = 0)
        {


            RowID = rowID;
        }


        public ActionCode_t Code => ActionCode_t.DeleteRow;
        public uint RowID { get; private set; }

        public void Read(IReader reader)
        {
            RowID = reader.ReadUInt();

            if (RowID == 0)
                throw new CorruptedStreamException();
        }

        public void Write(IWriter writer)
        {
            Assert(RowID > 0);
            writer.Write(RowID);
        }
    }


    public class ReplaceRow: IUpdateAction
    {
        public ReplaceRow(IDataRow datum )
        {
            Assert(datum != null);

            Datum = datum;
        }

        public ActionCode_t Code => ActionCode_t.ReplaceRow;
        public uint RowID => Datum.ID;
        public IDataRow Datum { get; }

        public void Read(IReader reader) => Datum.Read(reader);
        public void Write(IWriter writer) => Datum.Write(writer);
    }


    public class AddRow: IUpdateAction
    {
        public AddRow(IDataRow datum)
        {
            Assert(datum != null);

            Datum = datum;
        }


        public ActionCode_t Code => ActionCode_t.AddRow;
        public IDataRow Datum { get; }


        public void Read(IReader reader) => Datum.Read(reader);
        public void Write(IWriter writer) => Datum.Write(writer);
    }
}
