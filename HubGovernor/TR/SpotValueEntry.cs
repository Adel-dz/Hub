using System.Windows.Forms;

namespace DGD.HubGovernor.TR
{
    sealed class SpotValueEntry : ListViewItem
    {
        public SpotValueEntry(ITRVector vec) :
         base(vec.Content)
        {
            SpotValueID = vec.ValueID;
            ValueContextID = vec.ValueContextID;
            ProductID = vec.ProductID;
            OriginID = vec.OriginID;
            CurrencyID = vec.CurrencyID;
            UnitID = vec.UnitID;
            IncotermID = vec.IncotermID;
            PlaceID = vec.PlaceID;
        }


        public uint SpotValueID { get; }
        public uint ValueContextID { get; }
        public uint ProductID { get; }
        public uint OriginID { get; }
        public uint CurrencyID { get; }
        public uint UnitID { get; }
        public uint IncotermID { get; }
        public uint PlaceID { get; }
    }
}
