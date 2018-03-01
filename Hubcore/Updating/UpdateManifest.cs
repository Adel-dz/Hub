namespace DGD.HubCore.Updating
{
    public sealed class UpdateManifest
    {
        public UpdateManifest(uint updateKey, uint dataGeneration, uint appGeneration)
        {
            UpdateKey = updateKey;
            DataGeneration = dataGeneration;
            AppGeneration = appGeneration;
        }


        public uint UpdateKey { get; }
        public uint DataGeneration { get; }
        public uint AppGeneration { get; }
    }
}
