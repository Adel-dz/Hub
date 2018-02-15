namespace DGD.HubCore.Updating
{
    public sealed class UpdateManifest
    {
        public UpdateManifest(uint dataGeneration, uint appGeneration)
        {
            DataGeneration = dataGeneration;
            AppGeneration = appGeneration;
        }


        public uint DataGeneration { get; }
        public uint AppGeneration { get; }
    }
}
