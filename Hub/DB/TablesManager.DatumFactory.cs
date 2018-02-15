using DGD.HubCore;
using DGD.HubCore.DB;
using System;
using System.Collections.Generic;

namespace DGD.Hub.DB
{

    partial class TablesManager
    {
        class DatumFactory: IDatumFactory
        {
            public DatumFactory()
            {
                TablesDatum.Add(TablesID.COUNTRY , CreateEmptyDatum<Country>);
                TablesDatum.Add(TablesID.CURRENCY , CreateEmptyDatum<Currency>);
                TablesDatum.Add(TablesID.INCOTERM , CreateEmptyDatum<Incoterm>);
                TablesDatum.Add(TablesID.PLACE , CreateEmptyDatum<Place>);
                TablesDatum.Add(TablesID.PRODUCT , CreateEmptyDatum<Product>);
                TablesDatum.Add(TablesID.SHARED_TEXT , CreateEmptyDatum<SharedText>);
                TablesDatum.Add(TablesID.SPOT_VALUE , CreateEmptyDatum<SpotValue>);
                TablesDatum.Add(TablesID.SUPPLIER , CreateEmptyDatum<DataSupplier>);
                TablesDatum.Add(TablesID.UNIT , CreateEmptyDatum<Unit>);
                TablesDatum.Add(TablesID.VALUE_CONTEXT , CreateEmptyDatum<ValueContext>);
            }


            public IDataRow CreateDatum(uint tableID) => TablesDatum[tableID]();


            //private:
            IDictionary<uint , Func<IDataRow>> TablesDatum { get; } = new Dictionary<uint , Func<IDataRow>>();

            T CreateEmptyDatum<T>() where T : IDataRow, new() => new T();

        }
    }
}
