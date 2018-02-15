using DGD.HubCore.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easyLib;
using DGD.HubCore;
using DGD.HubGovernor.Products;
using DGD.HubGovernor.VContext;
using DGD.HubGovernor.Countries;
using DGD.HubGovernor.Currencies;
using DGD.HubGovernor.Units;
using DGD.HubGovernor.Places;
using DGD.HubGovernor.Incoterms;

namespace DGD.HubGovernor.TR
{
    interface ITRDatum
    {        
        SpotValue SpotValue { get; }
        ProductMapping ProductMapping { get; }
        TRLabel TRLabel { get; }
        Product Product { get; }
        ValueContext ValueContext { get; }
        Country Country { get; }
        Currency Currency { get; }
        Unit Unit { get; }
        Place Place { get; }
        Incoterm Incoterm { get; }

        string[] GetContent();
    }
}
