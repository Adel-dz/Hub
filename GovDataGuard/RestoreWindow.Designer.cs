namespace GovDataGuard
{
    partial class RestoreWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreWindow));
            this.m_lbMessage = new System.Windows.Forms.Label();
            this.m_progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // m_lbMessage
            // 
            this.m_lbMessage.AutoSize = true;
            this.m_lbMessage.BackColor = System.Drawing.Color.Transparent;
            this.m_lbMessage.Location = new System.Drawing.Point(15, 57);
            this.m_lbMessage.Name = "m_lbMessage";
            this.m_lbMessage.Size = new System.Drawing.Size(110, 13);
            this.m_lbMessage.TabIndex = 0;
            this.m_lbMessage.Text = "Lecture de l’archive...";
            // 
            // m_progressBar
            // 
            this.m_progressBar.Location = new System.Drawing.Point(15, 78);
            this.m_progressBar.Name = "m_progressBar";
            this.m_progressBar.Size = new System.Drawing.Size(389, 23);
            this.m_progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.m_progressBar.TabIndex = 1;
            // 
            // RestoreWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(425, 159);
            this.Controls.Add(this.m_progressBar);
            this.Controls.Add(this.m_lbMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RestoreWindow";
            this.Text = "Governor Data Guard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lbMessage;
        private System.Windows.Forms.ProgressBar m_progressBar;
    }
}