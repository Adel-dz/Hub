using System.Collections.Generic;
using DGD.HubCore;
using DGD.HubGovernor.DB;
using easyLib.DB;
using System.Linq;

namespace DGD.HubGovernor.FilesGen
{
    sealed class FileGenerationTable: FramedTable<FileGeneration>, ITableRelation
    {
        public FileGenerationTable(string filePath) : 
            base(TablesID.FILE_GENERATION , "Générations de fichiers" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => Enumerable.Empty<uint>();


        //protected:
        protected override int DatumSize => FileGeneration.Size;

        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID Table"),
                new TextColumn("Table"),
                new IntegerColumn("Génération")
            };
        }
    }
}
