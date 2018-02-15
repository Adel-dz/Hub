using System.Windows.Forms;
using static System.Diagnostics.Debug;


namespace DGD.HubGovernor.Waits
{
    sealed class WaitClue
    {
        WaitBox m_waitBox;


        public WaitClue(Control client)
        {
            Assert(client != null);

            Client = client;
        }


        public Control Client { get; }


        public void EnterWaitMode()
        {
            Assert(!Client.InvokeRequired);

            if (m_waitBox != null)
                return;

            Client.SuspendLayout();
            foreach (Control ctrl in Client.Controls)
                ctrl.Visible = false;
            

            m_waitBox = new WaitBox();
            Client.Controls.Add(m_waitBox);
            m_waitBox.Dock = DockStyle.Fill;
            m_waitBox.BackColor = Client.BackColor;

            m_waitBox.Show();

            Client.ResumeLayout();
        }

        public void LeaveWaitMode()
        {
            Assert(!Client.InvokeRequired);


            if (m_waitBox == null)
                return;

            Client.SuspendLayout();

            Client.Controls.Remove(m_waitBox);
            foreach (Control ctrl in Client.Controls)
                ctrl.Visible = true;
                        
            m_waitBox.Dispose();
            Client.ResumeLayout();
            m_waitBox = null;            
        }
    }
}
