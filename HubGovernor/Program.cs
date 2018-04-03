using System;
using System.Reflection;
using System.Threading;
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
            Mutex mtx = null;

            bool mtxOwned;
            mtx = new Mutex(true , @"Global\GOVERNOR_BoumekouezKhaled" , out mtxOwned);

            if (!mtxOwned)
                return;
            try
            {
                AppPaths.CheckFolders();

                AppContext.Init();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                AppContext.LogManager.LogSysActivity("Démarrage du HUB Governor");
                AppContext.LogManager.LogSysActivity($"Version des données: {AppContext.Settings.AppSettings.DataGeneration}");
                AppContext.LogManager.LogSysActivity($"Hub Governor version: {Assembly.GetExecutingAssembly().GetName().Version}");

                Application.Run(new MainWindow());

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur fatale c’est produite:\n{ex.Message}\nCliquez sur OK pour fermer l'apliaction." ,
                    null , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
            finally
            {
                AppContext.Dispose();

                if (mtx != null)
                {
                    mtx.ReleaseMutex();
                    mtx.Dispose();
                }
            }
        }


    }
}
