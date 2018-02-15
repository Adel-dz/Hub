namespace DGD.Hub.DB
{
    sealed class Incoterm : HubCore.DB.IncotermRow
    {
        public Incoterm()
        { }

        public Incoterm(uint id, string name):
            base(id, name)
        { }
    }
}
