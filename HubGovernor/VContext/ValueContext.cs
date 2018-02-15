using DGD.HubCore.DB;

namespace DGD.HubGovernor.VContext
{
    sealed class ValueContext: ValueContextRow
    {
        public ValueContext()
        { }

        public ValueContext(uint id , uint currencyID , uint unitID , uint originID = 0 ,
                uint incotermID = 0 , uint placeID = 0):
            base(id,currencyID,unitID,originID,incotermID,placeID)
        { }


        public static int Size => sizeof(uint) * 6;

        public override string ToString() => $"(ID:{ID}, ID Monnaie:{CurrencyID},ID Unité:{UnitID}, ID Origine:{OriginID} " +
            $"ID Incoterm:{IncotermID}, ID Lieu:{PlaceID}";
    }
}
