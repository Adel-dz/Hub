using System;
using System.Drawing;
using System.Windows.Forms;

namespace DGD.Hub.Log
{
    sealed partial class LogBox: UserControl
    {
        public LogBox()
        {
            InitializeComponent();
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }


        public string Message
        {
            get { return m_lbMessage.Text; }
            set
            {
                m_lbMessage.Text = value;
                AdjustSize();
                AutoMove();
            }
        }


       //private:
        private void AdjustSize()
        {
            Size sz = m_lbMessage.Size;
            SuspendLayout();
            sz.Width += 8;
            sz.Height += 8;
            ClientSize = sz;
            ResumeLayout();
        }

        void AutoMove()
        {
            Rectangle rcOwner = Parent.ClientRectangle;
            Rectangle rcThis = Bounds;
            Location = new Point(rcOwner.Right - rcThis.Width - 2 , rcOwner.Bottom - rcThis.Height - 2);
        }

    }
}
