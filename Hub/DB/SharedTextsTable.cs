namespace DGD.Hub.DB
{
    sealed class SharedTextsTable: HubCore.DB.DBTable<SharedText>
    {
        public SharedTextsTable(string filePath) :
            base(filePath , "Strings" , HubCore.TablesID.SHARED_TEXT)
        { }
    }
}
