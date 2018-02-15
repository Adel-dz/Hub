namespace DGD.Hub.DB
{
    sealed class SharedText : HubCore.DB.SharedTextRow
    {
        public SharedText()
        { }

        public SharedText(uint id, string text):
            base(id, text)
        { }
    }
}
