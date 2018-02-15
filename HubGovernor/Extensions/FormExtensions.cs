using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Extensions
{
    static class FormExtensions
    {
        public static void ShowError(this Form form, string msg)
        {
            Assert(form != null);
            Assert(!form.InvokeRequired);

            MessageBox.Show(form , msg , null , MessageBoxButtons.OK , MessageBoxIcon.Error);
        }

        public static void ShowWarning(this Form form , string msg)
        {
            Assert(form != null);
            Assert(!form.InvokeRequired);

            MessageBox.Show(form , msg , form.Text , MessageBoxButtons.OK , MessageBoxIcon.Warning);
        }
    }
}
