using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DGD.Hub.Log
{
    sealed partial class FlashBox: Form
    {
        public FlashBox(string msg)
        {
            InitializeComponent();

            m_lbMessage.Text = msg;
        }

        public void AutoMove()
        {
            Rectangle rcOwner = Owner.ClientRectangle;
            Rectangle rcThis = Bounds;
            Location = Owner.PointToScreen(new Point(rcOwner.Right - rcThis.Width - 2 , rcOwner.Bottom - rcThis.Height - 2));            
        }

        //protected:
        protected override void OnLoad(EventArgs e)
        {
            Size sz = m_lbMessage.Size;
            SuspendLayout();
            sz.Width += 8;
            sz.Height += 8;           
             
            ClientSize = sz;
            AutoMove();
            ResumeLayout();

            base.OnLoad(e);
        }

    }
}
