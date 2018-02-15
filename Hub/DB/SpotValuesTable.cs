namespace DGD.Hub.DB
{
    sealed class SpotValuesTable: HubCore.DB.DBTable<SpotValue>
    {
        public SpotValuesTable(string filePath) :
            base(filePath , "SpotValues" , HubCore.TablesID.SPOT_VALUE)
        { }
    }
}
