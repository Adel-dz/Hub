using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.Updating
{
    sealed partial class AppUpdateDialog: Form
    {
        public AppUpdateDialog()
        {
            InitializeComponent();

            m_cmbSystem.SelectedIndex = 0;
        }
    }
}
