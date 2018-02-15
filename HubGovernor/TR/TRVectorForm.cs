using DGD.HubCore.DB;
using DGD.HubGovernor.Spots;
using easyLib.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.TR
{
    partial class TRVectorForm: Form
    {

        public TRVectorForm(IDatumProvider srcSpotValues, IDatum datum)
        {
            Assert(srcSpotValues != null);
            Assert(datum != null);
            Assert(datum is SpotValue);

            InitializeComponent();
        }
    }
}
