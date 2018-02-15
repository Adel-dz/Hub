using DGD.HubCore;
using static System.Diagnostics.Debug;



namespace DGD.Hub.DB
{
    sealed class ValueContext: HubCore.DB.ValueContextRow
    {
        public ValueContext()
        { }

        public ValueContext(uint id , uint currencyID , uint unitID , uint originID ,
                uint incotermID , uint placeID):
            base(id, currencyID, unitID, originID, incotermID, placeID)
        { }


        public Currency Currency
        {
            get
            {
                Assert(CurrencyID > 0);

                return Program.TablesManager.GetKeyIndexer(TablesID.CURRENCY).Get(CurrencyID) as Currency;
            }
        }

        public Unit Unit
        {
            get
            {
                Assert(UnitID > 0);

                return Program.TablesManager.GetKeyIndexer(TablesID.UNIT).Get(UnitID) as Unit;
            }
        }

        public Country Origin => OriginID == 0 ? null :
            Program.TablesManager.GetKeyIndexer(TablesID.COUNTRY).Get(OriginID) as Country;

        public Incoterm Incoterm => IncotermID == 0 ? null :
            Program.TablesManager.GetKeyIndexer(TablesID.INCOTERM).Get(IncotermID) as Incoterm;

        public Place Place => PlaceID == 0 ? null :
            Program.TablesManager.GetKeyIndexer(TablesID.PLACE).Get(PlaceID) as Place;
    }
}
