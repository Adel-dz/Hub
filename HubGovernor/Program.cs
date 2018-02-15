using System;
using System.Windows.Forms;

namespace DGD.HubGovernor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppPaths.CheckFolders();

            AppContext.Init();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());

            AppContext.Dispose();
        }

       
    }
}
