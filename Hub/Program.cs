using DGD.Hub.DB;
using System;
using System.Windows.Forms;

namespace DGD.Hub
{
    static class Program
    {        
        static TablesManager m_tblManager;
        static SettingsManager m_settings;
        static DLG.DialogManager m_dlgManager;

        public static TablesManager TablesManager => m_tblManager;
        public static SettingsManager Settings => m_settings;
        public static DLG.DialogManager DialogManager => m_dlgManager;


        //private:

        //Entry:
        [STAThread]
        static void Main()
        {
            using(m_settings = new SettingsManager())
            using (m_tblManager = new TablesManager())
            using (m_dlgManager = new DLG.DialogManager())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new MainWindow());
            }

            Log.LogEngin.Dispose();
        }

    }
}
