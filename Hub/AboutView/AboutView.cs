using System.Windows.Forms;
using System.Reflection;
using static System.Diagnostics.Debug;
using System;

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
            m_lblUpdateKey.Text = $"Clé de mise à jour: {Program.Settings.UpdateKey}";

            AutoUpdater.DataUpdated += AutoUpdater_DataUpdated;

            Show();
        }

        public void Deactivate(Control parent)
        {
            Assert(parent != null);
            AutoUpdater.DataUpdated -= AutoUpdater_DataUpdated;
            parent.Controls.Remove(this);
            Hide();
        }

        //private:
        private void AutoUpdater_DataUpdated()
        {
            if (InvokeRequired)
                Invoke(new Action(AutoUpdater_DataUpdated));
            else
                m_lblDataGeneration.Text = $"Version des données: {Program.Settings.DataGeneration}";
        }

    }
}
