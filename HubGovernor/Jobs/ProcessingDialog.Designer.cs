namespace DGD.HubGovernor.Jobs
{
    partial class ProcessingDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.PictureBox m_pbWorking;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessingDialog));
            this.m_lblMessage = new System.Windows.Forms.Label();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            m_pbWorking = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(m_pbWorking)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pbWorking
            // 
            m_pbWorking.Image = ((System.Drawing.Image)(resources.GetObject("m_pbWorking.Image")));
            m_pbWorking.Location = new System.Drawing.Point(12, 12);
            m_pbWorking.Name = "m_pbWorking";
            m_pbWorking.Size = new System.Drawing.Size(53, 57);
            m_pbWorking.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            m_pbWorking.TabIndex = 0;
            m_pbWorking.TabStop = false;
            // 
            // m_lblMessage
            // 
            this.m_lblMessage.AutoEllipsis = true;
            this.m_lblMessage.Location = new System.Drawing.Point(71, 31);
            this.m_lblMessage.Name = "m_lblMessage";
            this.m_lblMessage.Size = new System.Drawing.Size(415, 18);
            this.m_lblMessage.TabIndex = 1;
            this.m_lblMessage.Text = "Patientez svp";
            // 
            // m_timer
            // 
            this.m_timer.Interval = 500;
            this.m_timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // ProcessingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 81);
            this.ControlBox = false;
            this.Controls.Add(this.m_lblMessage);
            this.Controls.Add(m_pbWorking);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProcessingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Traitement en cours ...";
            ((System.ComponentModel.ISupportInitialize)(m_pbWorking)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_lblMessage;
        private System.Windows.Forms.Timer m_timer;
    }
}