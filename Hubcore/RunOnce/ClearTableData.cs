using easyLib;

namespace DGD.HubCore.RunOnce
{
    public class ClearTableData: IStorable
    {
        public ClearTableData(string tblPath = "")
        {
            TablePath = tblPath;
        }
                
        public string TablePath { get; private set; }

        public void Read(IReader reader)
        {
            string tblPath = reader.ReadString();

            if (string.IsNullOrWhiteSpace(tblPath))
                throw new CorruptedStreamException();

            TablePath = tblPath;
        }

        public void Write(IWriter writer)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(TablePath));
            writer.Write(TablePath);
        }
    }
}
