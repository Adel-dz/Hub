using DGD.HubCore;
using easyLib;
using System;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.TR
{
    interface ITRVector
    {
        uint ValueID { get; }
        uint ProductID { get; }
        uint ValueContextID { get; }
        uint OriginID { get; }
        uint IncotermID { get; }
        uint PlaceID { get; }
        uint CurrencyID { get; }
        uint UnitID { get; }
        SubHeading SubHeading { get; }
        string Product { get; }
        string Origin { get; }
        DateTime SpotTime { get; }
        double Price { get; }
        string Currency { get; }
        string Unit { get; }
        string Incoterm { get; }
        string Place { get; }
        string TRLabel { get; }
        bool IsStable { get; }
        string[] Content { get; }
}


    sealed class TRVector: ITRVector
    {
        public string Currency { get; private set; }
        public string Incoterm { get; private set; }
        public string Origin { get; private set; }
        public string Place { get; private set; }
        public double Price { get; private set; }
        public string Product { get; private set; }
        public DateTime SpotTime { get; private set; }
        public SubHeading SubHeading { get; private set; }
        public string Unit { get; private set; }
        public string TRLabel { get; private set; }
        public bool IsStable { get; private set; }
        public uint ValueID { get; private set; }
        public uint ProductID { get; private set; }
        public uint ValueContextID { get; private set; }
        public uint OriginID { get; private set; }
        public uint IncotermID { get; private set; }
        public uint PlaceID { get; private set; }
        public uint CurrencyID { get; private set; }
        public uint UnitID { get; private set; }
        public string[] Content => new[]
        {
            SubHeading.ToString(),
            Product,
            Origin,
            SpotTime.ToShortDateString(),
            Price.ToString(),
            Currency,
            Unit,
            Incoterm,
            Place
        };

        public static TRVector BuildVector(uint valID, DataAccesPath accPath)
        {
            //Assert(accPath != null);

            //var vec = new TRVector();
            //vec.ValueID = valID;
            //vec.IsStable = true;

            //try
            //{
            //    var spotVal = accPath.Values.Get(valID) as Spots.SpotValue;

            //    vec.Price = spotVal.Price;
            //    vec.SpotTime = spotVal.SpotTime;
            //    vec.ProductID = spotVal.ProductID;
            //    vec.ValueContextID = spotVal.ValueContextID;

            //    var prod = accPath.Products.Get(spotVal.ProductID) as Products.Product;
            //    vec.Product = prod.Name;
            //    vec.SubHeading = prod.SubHeading;
            //    vec.IsStable &= !prod.Disabled;

            //    var valContext = accPath.ValuesContext.Get(spotVal.ValueContextID) as VContext.ValueContext;
            //    vec.OriginID = valContext.OriginID;
            //    vec.IncotermID = valContext.IncotermID;
            //    vec.PlaceID = valContext.PlaceID;
            //    vec.CurrencyID = valContext.CurrencyID;
            //    vec.UnitID = valContext.UnitID;

            //    if (valContext.OriginID == 0)
            //        vec.Origin = "";
            //    else
            //    {
            //        var ctry = accPath.Countries.Get(valContext.OriginID) as Countries.Country;
            //        vec.Origin = ctry.Name;
            //        vec.IsStable &= !ctry.Disabled;
            //    }


            //    if (valContext.IncotermID == 0)
            //        vec.Incoterm = "";
            //    else
            //    {
            //        var ict = accPath.Incoterms.Get(valContext.IncotermID) as Incoterms.Incoterm;
            //        vec.Incoterm = ict.Name;
            //        vec.IsStable &= !ict.Disabled;
            //    }


            //    if (valContext.PlaceID == 0)
            //        vec.Place = "";
            //    else
            //    {
            //        var place = accPath.Places.Get(valContext.PlaceID) as Places.Place;
            //        vec.Place = place.Name;
            //        vec.IsStable &= !place.Disabled;
            //    }

            //    var cncy = accPath.Currencies.Get(valContext.CurrencyID) as Currencies.Currency;
            //    vec.Currency = cncy.Name;
            //    vec.IsStable &= !cncy.Disabled;

            //    var unit = accPath.Units.Get(valContext.UnitID) as Units.Unit;
            //    vec.Unit = unit.Name;
            //    vec.IsStable &= !unit.Disabled;

            //    var mapping = (from Mappings.TRMapping m in accPath.MappingProductsID.Get(prod.ID)
            //                   where m.ValueContextID == valContext.ID
            //                   select m).Single();

            //    vec.TRLabel = mapping.Description;
            //}
            //catch (Exception ex)
            //{
            //    EventLogger.Error(ex.Message);

            //    throw new CorruptedStreamException(innerException: ex);
            //}

            //return vec;

            throw null;
        }
    }
}
