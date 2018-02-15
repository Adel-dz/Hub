using System.Windows.Forms;
using System.Reflection;
using static System.Diagnostics.Debug;


namespace DGD.Hub.AboutView
{
    public partial class AboutView: UserControl, IView
    {
        public AboutView()
        {
            InitializeComponent();

            m_lblVersion.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";

            System.Diagnostics.Trace.WriteLine($"Hub version: {Assembly.GetExecutingAssembly().GetName().Version}");
        }

        public void Activate(Control parent)
        {
            Assert(parent != null);

            parent.Controls.Add(this);
            Dock = DockStyle.Fill;
            m_lblDataGeneration.Text = $"Version des données: {Program.Settings.DataGeneration}";
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
