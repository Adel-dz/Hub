using easyLib.DB;

namespace DGD.HubGovernor.Log
{
    sealed class EventLogTable: FuzzyTable<EventLog>
    {
        public EventLogTable(string filePath) :
            base(InternalTablesID.LOG_EVENT , "log" , filePath)
        { }


        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TimeColumn("Date"),
                new TextColumn("Type d’événement"),
                new TextColumn("Evénement")
            };
        }
    }
}
