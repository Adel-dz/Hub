using DGD.HubCore;
using DGD.HubCore.DB;

namespace DGD.Hub.DB
{
    sealed class CountriesTable: DBTable<Country>
    {
        public CountriesTable(string filePath) :
            base(filePath , "countries" , TablesID.COUNTRY)
        { }
    }
}
