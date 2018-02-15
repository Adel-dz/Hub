using System;
using DGD.HubGovernor.Countries;
using DGD.HubGovernor.Currencies;
using DGD.HubGovernor.Incoterms;
using DGD.HubGovernor.Places;
using DGD.HubGovernor.Products;
using DGD.HubGovernor.Units;
using DGD.HubGovernor.VContext;
using DGD.HubCore.DB;
using System.Collections.Generic;
using DGD.HubGovernor.DB;
using DGD.HubCore;

namespace DGD.HubGovernor.TR
{
    sealed class TRDatumBuilder
    {
        class TRDatum: ITRDatum
        {
            public Country Country { get; set; }
            public Currency Currency { get; set; }
            public Incoterm Incoterm { get; set; }
            public Place Place { get; set; }
            public Product Product { get; set; }
            public ProductMapping ProductMapping { get; set; }
            public SpotValue SpotValue { get; set; }
            public TRLabel TRLabel { get; set; }
            public Unit Unit { get; set; }
            public ValueContext ValueContext { get; set; }

            public string[] GetContent()
            {
                return new[]
                {
                    SpotValue.SessionNumber.ToString(),
                    Product.SubHeading.ToString(),
                    Product.Name,
                    TRLabel.Label,
                    SpotValue.Price.ToString(),
                    Currency.Name,
                    Incoterm?.Name ?? "",
                    Place?.Name ?? "",
                    SpotValue.Time.ToShortDateString(),
                    Unit.Name,
                    Country?.Name ?? "",
                    ProductMapping.ProductNumber.ToString()
                };
            }
        }




        public IEnumerable<ITRDatum> BuildAll()
        {
            foreach (uint id in AppContext.AccessPath.GetKeyIndexer(InternalTablesID.TR_SPOT_VALUE).Keys)
                yield return Build(id);
        }

        public ITRDatum Build(uint spotValueID)
        {
            DataAccessPath dataPath = AppContext.AccessPath;

            var datum = new TRDatum();

            datum.SpotValue = dataPath.GetKeyIndexer(InternalTablesID.TR_SPOT_VALUE).Get(spotValueID) as SpotValue;
            datum.TRLabel = dataPath.GetKeyIndexer(InternalTablesID.TR_LABEL).Get(datum.SpotValue.LabelID) as TRLabel;
            datum.ProductMapping = dataPath.GetKeyIndexer(InternalTablesID.TR_PRODUCT_MAPPING).Get(datum.SpotValue.ProductMappingID) as ProductMapping;
            datum.Product = dataPath.GetKeyIndexer(TablesID.PRODUCT).Get(datum.ProductMapping.ProductID) as Product;
            datum.ValueContext = dataPath.GetKeyIndexer(TablesID.VALUE_CONTEXT).Get(datum.ProductMapping.ContextID) as ValueContext;
            datum.Country = dataPath.GetKeyIndexer(TablesID.COUNTRY).Get(datum.ValueContext.OriginID) as Country;
            datum.Currency = dataPath.GetKeyIndexer(TablesID.CURRENCY).Get(datum.ValueContext.CurrencyID) as Currency;
            datum.Unit = dataPath.GetKeyIndexer(TablesID.UNIT).Get(datum.ValueContext.UnitID) as Unit;
            datum.Place = dataPath.GetKeyIndexer(TablesID.PLACE).Get(datum.ValueContext.PlaceID) as Place;
            datum.Incoterm = dataPath.GetKeyIndexer(TablesID.INCOTERM).Get(datum.ValueContext.IncotermID) as Incoterm;

            return datum;
        }
        
    }
}
