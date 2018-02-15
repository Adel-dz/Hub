using DGD.HubCore;
using DGD.HubCore.DB;
using DGD.HubGovernor.TR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.DB
{
    sealed class DataRowFactory: IDatumFactory
    {
        
        public DataRowFactory()
        {
            TablesDatum[InternalTablesID.TR_LABEL] = CreateEmptyDatum<TRLabel>;
            TablesDatum[InternalTablesID.TR_SPOT_VALUE] = CreateEmptyDatum<TR.SpotValue>;
            TablesDatum.Add(TablesID.COUNTRY , CreateEmptyDatum<CountryRow>);
            TablesDatum.Add(TablesID.CURRENCY , CreateEmptyDatum<CurrencyRow>);
            TablesDatum.Add(TablesID.INCOTERM , CreateEmptyDatum<IncotermRow>);
            TablesDatum.Add(TablesID.PLACE , CreateEmptyDatum<PlaceRow>);
            TablesDatum.Add(TablesID.PRODUCT , CreateEmptyDatum<ProductRow>);
            TablesDatum.Add(TablesID.SHARED_TEXT , CreateEmptyDatum<SharedTextRow>);
            TablesDatum.Add(TablesID.SPOT_VALUE , CreateEmptyDatum<SpotValueRow>);
            TablesDatum.Add(TablesID.SUPPLIER , CreateEmptyDatum<DataSupplierRow>);
            TablesDatum.Add(TablesID.UNIT , CreateEmptyDatum<UnitRow>);
            TablesDatum.Add(TablesID.VALUE_CONTEXT , CreateEmptyDatum<ValueContextRow>);
        }


        public IDataRow CreateDatum(uint tableID) => TablesDatum[tableID]();


        //private:
        IDictionary<uint , Func<IDataRow>> TablesDatum { get; } = new Dictionary<uint , Func<IDataRow>>();

        T CreateEmptyDatum<T>() where T : IDataRow, new() => new T();

    }
}
