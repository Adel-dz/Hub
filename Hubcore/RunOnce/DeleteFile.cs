using easyLib;

namespace DGD.HubCore.RunOnce
{
    public class DeleteFile: IStorable
    {
        public DeleteFile(string filePath = "")
        {
            FilePath = filePath;
        }
                
        public string FilePath { get; private set; }

        public void Read(IReader reader)
        {
            string filePath = reader.ReadString();

            if (string.IsNullOrWhiteSpace(filePath))
                throw new CorruptedStreamException();

            FilePath = filePath;
        }

        public void Write(IWriter writer)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrWhiteSpace(FilePath));
            writer.Write(FilePath);
        }
    }
}
