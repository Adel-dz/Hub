namespace DGD.HubCore.Updating
{
    public sealed class UpdateURI
    {
        public UpdateURI(string uriFile, uint preGeneration, uint postGeneration)
        {
            DataPreGeneration = preGeneration;
            DataPostGeneration = postGeneration;
            FileURI = uriFile;
        }

        public UpdateURI(string uriFile, uint preGeneration):
            this(uriFile, preGeneration, preGeneration + 1)
        { }


        public uint DataPreGeneration { get; }
        public uint DataPostGeneration { get; }
        public string FileURI { get; }
    }
}
