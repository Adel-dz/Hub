using DGD.HubCore.DB;
using System;

namespace DGD.HubGovernor.Spots
{
    sealed class SpotValue : SpotValueRow
    {
        public SpotValue()
        { }


        public SpotValue(uint id , double price , DateTime spotTime , uint productID , 
                uint valContextID , uint supplierID, uint descrID) : 
            base(id , price , spotTime , productID , valContextID , supplierID, descrID)
        { }


        public static int Size => (sizeof(uint) * 5) + sizeof(double) + sizeof(long);

        public override string ToString() => $"(ID:{ID}, Prix:{Price}, Date:{SpotTime}, ID Produit:{ProductID}, " +
            $"ID Contexte de valeur:{ValueContextID}, ID Fournisseur:{SupplierID}, ID Description:{DescriptionID})";
    }
}
