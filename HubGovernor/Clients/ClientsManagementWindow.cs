using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.HubGovernor.Clients
{
    public partial class ClientsManagementWindow: Form
    {
        public ClientsManagementWindow()
        {
            InitializeComponent();
        }

        //private:

        //handlers
        private void Profiles_Click(object sender , EventArgs e)
        {
            var wind = new Profiles.ProfilesWindow();
            wind.Show(Owner);
        }

        private void Clients_Click(object sender , EventArgs e)
        {
            var wind = new ClientsWindow();
            wind.Show(Owner);
        }
    }
}
