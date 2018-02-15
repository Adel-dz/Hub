using DGD.HubCore;
using DGD.HubGovernor.DB;
using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.Spots
{
    sealed class SpotValueTable: FramedTable<SpotValue>, ITableRelation
    {
        public SpotValueTable(string filePath) : 
            base(TablesID.SPOT_VALUE , "Valeurs spots" , filePath)
        { }

        public IEnumerable<uint> RelatedTables
        {
            get
            {
                yield return TablesID.VALUE_CONTEXT;
                yield return TablesID.PRODUCT;
                yield return TablesID.SUPPLIER;
                yield return TablesID.SHARED_TEXT;
            }
        }

        //protected:
        protected override int DatumSize => SpotValue.Size;

        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TimeColumn("Date spot"),
                new FloatColumn("Prix"),
                new IntegerColumn("ID Contexte de valeur"),
                new IntegerColumn("ID Produit"),
                new IntegerColumn("ID Source de données")
            };
        }
    }
}
