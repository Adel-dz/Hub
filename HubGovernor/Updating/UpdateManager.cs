using DGD.HubCore.Updating;
using DGD.HubGovernor.Updating;
using easyLib.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGD.HubGovernor.Updating
{
    sealed class UpdateManager
    {
        public uint DataGeneration
        {
            get { return AppContext.Settings.AppSettings.DataGeneration; }
            set { AppContext.Settings.AppSettings.DataGeneration = value; }
        }

        public IEnumerable<UpdateIncrement> Updates
        {
            get
            {
                IDatumProvider dp = AppContext.AccessPath.GetDataProvider(InternalTablesID.INCREMENT);

                if (dp.Count == 0)
                    return Enumerable.Empty<UpdateIncrement>();

                return dp.Enumerate().Cast<UpdateIncrement>();
            }
        }

        public IEnumerable<UpdateIncrement> UnPublishedUpdates
        {
            get
            {
                IDatumProvider dp = AppContext.AccessPath.GetDataProvider(InternalTablesID.INCREMENT);

                if (dp.Count == 0)
                    return Enumerable.Empty<UpdateIncrement>();

                return dp.Enumerate().Cast<UpdateIncrement>().Where(inc => !inc.IsDeployed);
            }
        }


    }
}
