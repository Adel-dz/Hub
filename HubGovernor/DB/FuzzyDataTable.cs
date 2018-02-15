using easyLib;
using easyLib.DB;

namespace DGD.HubGovernor.DB
{
    interface IFuzzyDataTable: IDataTable
    {
        IDatumProvider RowProvider { get; }
        IDatumProvider DisabledRowProvider { get; }
    }
    


    abstract class FuzzyDataTable<T>: FuzzyTable<T>, IFuzzyDataTable
        where T : IStorable, IDatum, ITaggedRow, new()
    {
        protected FuzzyDataTable(uint id , string name , string filePath) : 
            base(id , name , filePath)
        { }


        public IDatumProvider RowProvider => 
            new DatumProvider(DataProvider , d => !(d as ITaggedRow).Disabled);

        public IDatumProvider DisabledRowProvider =>
            new DatumProvider(DataProvider , d => (d as ITaggedRow).Disabled);
    }
}
