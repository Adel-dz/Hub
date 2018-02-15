using DGD.HubGovernor.DB;
using System.Collections.Generic;
using System.Linq;
using easyLib.DB;

namespace DGD.HubGovernor.Profiles
{
    sealed class UserProfileTable : FuzzyTable<UserProfile>, ITableRelation
    {
        public UserProfileTable(string filePath) :
            base(InternalTablesID.USER_PROFILE , "Profiles" , filePath)
        { }


        public IEnumerable<uint> RelatedTables => Enumerable.Empty<uint>();

        //protected:
        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TextColumn("Profile"),
                new TextColumn("Privilège")
            };
        }
    }
}
