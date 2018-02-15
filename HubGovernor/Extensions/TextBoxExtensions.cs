using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Extensions
{
    static class TextBoxExtensions
    {
        public static string GetInputText(this TextBox txtBox)
        {
            Assert(txtBox != null);
            Assert(!txtBox.InvokeRequired);

            Opts.StringTransform_t optStr = AppContext.Settings.AppSettings.InputTransform;

            return (optStr == Opts.StringTransform_t.UpperCase ? txtBox.Text.ToUpper() :
                optStr == Opts.StringTransform_t.LowerCase ? txtBox.Text.ToLower() :
                txtBox.Text).Trim();
        }
    }
}
