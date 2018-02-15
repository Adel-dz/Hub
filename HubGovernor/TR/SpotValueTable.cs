using System.Collections.Generic;
using DGD.HubGovernor.DB;
using easyLib.DB;

namespace DGD.HubGovernor.TR
{
    sealed class SpotValueTable: FramedTable<SpotValue>, ITableRelation
    {
        public SpotValueTable(string filePath) : 
            base(InternalTablesID.TR_SPOT_VALUE , "Prix Spot (TR)", filePath)
        { }


        public IEnumerable<uint> RelatedTables
        {
            get
            {
                return new[]
                {
                    AppContext.TableManager.TRProductsMapping.ID,
                    AppContext.TableManager.TRLabels.ID
                };
            }
        }


        //protected:
        protected override int DatumSize => SpotValue.Size;

        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new IntegerColumn("N° Session"),
                new FloatColumn("Prix"),
                new TimeColumn("date et Heure"),
                new IntegerColumn("ID Mapping Produit (TR)"),
                new IntegerColumn("ID Libellé TR"),
                new IntegerColumn("ID Valeur publiée")
            };
        }
    }
}
