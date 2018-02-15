using System;
using System.Collections.Generic;
using DGD.HubGovernor.DB;
using easyLib.DB;

namespace DGD.HubGovernor.TR
{
    sealed class LabelMappingTable: FramedTable<LabelMapping>, ITableRelation
    {
        public LabelMappingTable(string filePath) :
            base(InternalTablesID.TR_LABEL_MAPPING , "Libellé Mapping (TR)" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => new[] { AppContext.TableManager.TRLabels.ID };


        //protected:
        protected override int DatumSize => LabelMapping.Size;
        
        protected override IDataColumn[] GetColumns()
        {
            return new[]
            {
                new IntegerColumn("ID Libellé TR"),
                new IntegerColumn("ID Texte Partagé")
            };
        }
    }
}
