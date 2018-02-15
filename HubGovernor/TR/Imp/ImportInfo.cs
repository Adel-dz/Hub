using System.Collections.Generic;
using System.Linq;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.TR.Imp
{
    sealed class ImportInfo
    {
        public ImportInfo(string[][] data , IDictionary<ColumnKey_t , int> mapping)
        {
            Assert(data != null);
            Assert(mapping != null);
            Assert(data.All(row => row.Length >= ColumnMapping.COLUMNS_COUNT));


            ColumnsMapping = mapping;
            Data = data;
        }


        public IDictionary<ColumnKey_t , int> ColumnsMapping { get; }
        public string[][] Data { get; }
    }
}
