using System;
using System.Collections.Generic;
using DGD.HubGovernor.DB;
using easyLib.DB;

namespace DGD.HubGovernor.Profiles
{
    class ProfileManagementModeTable: FramedTable<ProfileManagementMode>, ITableRelation
    {
        public ProfileManagementModeTable(string filePath) :
            base(InternalTablesID.PROFILE_MGMNT_MODE , "Gestion des profils" , filePath)
        { }

        public IEnumerable<uint> RelatedTables => new [] {InternalTablesID.USER_PROFILE };


        //protected:
        protected override int DatumSize => ProfileManagementMode.Size;

        protected override IDataColumn[] GetColumns()
        {
            return new IDataColumn[]
            {
                new IntegerColumn("ID"),
                new TextColumn("Mode")
            };
        }
    }
}
