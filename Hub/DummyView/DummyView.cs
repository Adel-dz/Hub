using System.Windows.Forms;
using static System.Diagnostics.Debug;

namespace DGD.Hub.DummyView
{
    sealed partial class DummyView: UserControl, IView
    {
        public DummyView()
        {
            InitializeComponent();
        }

        public void Activate(Control parent)
        {
            Assert(parent != null);

            parent.Controls.Add(this);
            Dock = DockStyle.Fill;

            Show();
            
        }

        public void Deactivate(Control parent)
        {
            Assert(parent != null);

            parent.Controls.Remove(this);
            Hide();
        }
    }
}
