namespace DGD.Hub.DB
{
    sealed class Unit: HubCore.DB.UnitRow
    {
        public Unit()
        { }
        
        public Unit(uint id , string name , string descr) :
            base(id, name, descr)
        { }

    }
}
