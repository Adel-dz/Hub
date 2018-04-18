using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GovDataGuard
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                int action = int.Parse(args[0]);

                if (action == 0)
                    Application.Run(new BackupWindow(args[1] , args[2]));
                else if (action == 1)
                    Application.Run(new RestoreWindow(args[1] , args[2]));
            }
            catch(Exception ex)
            {
                File.WriteAllText("ArchLog.txt" , ex.Message + "\n\n" + ex.StackTrace);
            }

            System.Diagnostics.Process.Start(Path.Combine(@".\" , "HubGovernor.exe"));
        }
    }
}
