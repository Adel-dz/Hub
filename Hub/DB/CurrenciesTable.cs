using DGD.HubCore;
using DGD.HubCore.DB;

namespace DGD.Hub.DB
{
    sealed class CurrenciesTable: DBTable<Currency>
    {
        public CurrenciesTable(string filePath) :
            base(filePath , "Currencies" , TablesID.CURRENCY)
        { }
    }
}
