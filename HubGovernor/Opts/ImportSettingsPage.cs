using System;
using System.Windows.Forms;

namespace DGD.HubGovernor.Opts
{
    sealed partial class ImportSettingsPage: UserControl, ISettingsPage
    {
        public ImportSettingsPage()
        {
            InitializeComponent();
        }

        public void Apply()
        {
            StringTransform_t optInput = m_rbLowerCase.Checked ? StringTransform_t.LowerCase :
                m_rbPreserveCase.Checked ? StringTransform_t.None : StringTransform_t.UpperCase;

            AppContext.Settings.AppSettings.ImportTransform = optInput;

        }

        //protected:
        protected override void OnLoad(EventArgs e)
        {
            StringTransform_t optInput = AppContext.Settings.AppSettings.ImportTransform;

            if (optInput == StringTransform_t.LowerCase)
                m_rbLowerCase.Checked = true;
            else if (optInput == StringTransform_t.UpperCase)
                m_rbUpperCase.Checked = true;
            else
                m_rbPreserveCase.Checked = true;

            base.OnLoad(e);
        }
    }
}
