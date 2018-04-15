namespace GovDataGuard
{
    partial class BackupLastStagePage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.PictureBox m_pbLogo;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupLastStagePage));
            this.m_lblMessage = new System.Windows.Forms.Label();
            this.m_progressBar = new System.Windows.Forms.ProgressBar();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pbLogo
            // 
            m_pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            m_pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("m_pbLogo.Image")));
            m_pbLogo.Location = new System.Drawing.Point(200, 78);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(209, 177);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            m_pbLogo.TabIndex = 27;
            m_pbLogo.TabStop = false;
            // 
            // m_lblMessage
            // 
            this.m_lblMessage.AutoSize = true;
            this.m_lblMessage.Location = new System.Drawing.Point(19, 107);
            this.m_lblMessage.Name = "m_lblMessage";
            this.m_lblMessage.Size = new System.Drawing.Size(112, 13);
            this.m_lblMessage.TabIndex = 28;
            this.m_lblMessage.Text = "Creation de l\'archive...";
            // 
            // m_progressBar
            // 
            this.m_progressBar.Location = new System.Drawing.Point(19, 124);
            this.m_progressBar.Name = "m_progressBar";
            this.m_progressBar.Size = new System.Drawing.Size(375, 23);
            this.m_progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_progressBar.TabIndex = 29;
            // 
            // BackupLastStagePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_progressBar);
            this.Controls.Add(this.m_lblMessage);
            this.Controls.Add(m_pbLogo);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "BackupLastStagePage";
            this.Size = new System.Drawing.Size(412, 255);
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblMessage;
        private System.Windows.Forms.ProgressBar m_progressBar;
    }
}
