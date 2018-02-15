using System;
using System.Windows.Forms;

namespace DGD.HubGovernor
{
    sealed partial class InputDialog: Form
    {
        public InputDialog()
        {
            InitializeComponent();
        }


        public string Input
        {
            get { return m_tbInput.Text; }
            set
            {
                m_tbInput.Text = value;
                m_btnOK.Enabled = value.Length > 0 && (AcceptEmptyString || !string.IsNullOrWhiteSpace(value));
            }
        }

        public string Message
        {
            get { return m_lblMessage.Text; }
            set { m_lblMessage.Text = value; }
        }
        
        public bool AcceptEmptyString { get; set; }

        public CharacterCasing InputCasing
        {
            get { return m_tbInput.CharacterCasing; }
            set { m_tbInput.CharacterCasing = value; }
        }

        //protected:
        protected override void OnLoad(EventArgs e)
        {
            m_tbInput.SelectAll();
            base.OnLoad(e);
        }

        //private:


        //handlers:
        private void Input_TextChanged(object sender , EventArgs e)
        {
            string txt = m_tbInput.Text;
            m_btnOK.Enabled = txt.Length > 0  && (AcceptEmptyString || !string.IsNullOrWhiteSpace(txt));
        }
    }
}
