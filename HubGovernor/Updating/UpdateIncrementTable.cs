using easyLib.DB;

namespace DGD.HubGovernor.Updating
{
    sealed class UpdateIncrementTable: FramedTable<UpdateIncrement>
    {
        public UpdateIncrementTable(string filePath) :
            base(InternalTablesID.INCREMENT , "MAJ de Données" , filePath)
        { }


        //protected
        protected override int DatumSize => UpdateIncrement.Size;

        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                    new IntegerColumn("ID"),
                    new IntegerColumn("Pré-Version des données"),
                    new TimeColumn("Crée le"),
                    new TimeColumn("Publier le")
            };

        }
    }
}
