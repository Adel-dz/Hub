using DGD.HubCore;
using DGD.HubCore.DB;

namespace DGD.Hub.DB
{
    sealed class PlacesTable: DBTable<Place>
    {
        public PlacesTable(string filePath) :
            base(filePath , "Places" , TablesID.PLACE)
        { }
    }
}
