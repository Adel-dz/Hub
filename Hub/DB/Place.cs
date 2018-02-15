using DGD.HubCore;
using static System.Diagnostics.Debug;


namespace DGD.Hub.DB
{
    sealed class Place : HubCore.DB.PlaceRow
    {
        public Place()
        { }

        public Place(uint id , string name , uint countryID):
            base(id, name, countryID)
        { }


        public Country Country
        {
            get
            {
                return CountryID == 0 ? null : 
                    Program.TablesManager.GetKeyIndexer(TablesID.COUNTRY).Get(CountryID) as Country;
            }
        }
    }
}
