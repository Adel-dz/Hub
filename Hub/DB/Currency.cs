using DGD.HubCore;

namespace DGD.Hub.DB
{
    sealed class Currency : HubCore.DB.CurrencyRow
    {
        public Currency()
        { }

        public Currency(uint id , string name , uint countryID , string descr) :
            base(id, name, countryID, descr)
        { }


        public Country Country
        {
            get
            {
                if (CountryID > 0)
                    return Program.TablesManager.GetKeyIndexer(TablesID.COUNTRY).Get(CountryID) as Country;

                return null;
            }
        }
    }
}
