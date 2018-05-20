namespace DGD.Hub.Jobs
{
    partial class SplashScreen
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
            System.Windows.Forms.Label m_lblAppName;
            System.Windows.Forms.Label m_lblHeader;
            System.Windows.Forms.Label m_lblFooter;
            System.Windows.Forms.ProgressBar m_progressBar;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.m_lblVersion = new System.Windows.Forms.Label();
            this.m_lblMessage = new System.Windows.Forms.Label();
            m_lblAppName = new System.Windows.Forms.Label();
            m_lblHeader = new System.Windows.Forms.Label();
            m_lblFooter = new System.Windows.Forms.Label();
            m_progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // m_lblAppName
            // 
            m_lblAppName.AutoSize = true;
            m_lblAppName.BackColor = System.Drawing.Color.Transparent;
            m_lblAppName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblAppName.ForeColor = System.Drawing.Color.White;
            m_lblAppName.Location = new System.Drawing.Point(57, 88);
            m_lblAppName.Name = "m_lblAppName";
            m_lblAppName.Size = new System.Drawing.Size(284, 23);
            m_lblAppName.TabIndex = 0;
            m_lblAppName.Text = "HUB de la valeur en douanes";
            // 
            // m_lblHeader
            // 
            m_lblHeader.AutoSize = true;
            m_lblHeader.BackColor = System.Drawing.Color.Transparent;
            m_lblHeader.ForeColor = System.Drawing.Color.LightGray;
            m_lblHeader.Location = new System.Drawing.Point(48, 9);
            m_lblHeader.Name = "m_lblHeader";
            m_lblHeader.Size = new System.Drawing.Size(303, 39);
            m_lblHeader.TabIndex = 2;
            m_lblHeader.Text = "Direction de la fiscalité et du recouvrement\r\nSous-direction de la valeur en doua" +
    "nes\r\nBureau de l’analyse, de la diffusion des données et du contrôle";
            m_lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblFooter
            // 
            m_lblFooter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            m_lblFooter.AutoSize = true;
            m_lblFooter.BackColor = System.Drawing.Color.Transparent;
            m_lblFooter.ForeColor = System.Drawing.Color.Silver;
            m_lblFooter.Location = new System.Drawing.Point(6, 280);
            m_lblFooter.Name = "m_lblFooter";
            m_lblFooter.Size = new System.Drawing.Size(387, 13);
            m_lblFooter.TabIndex = 3;
            m_lblFooter.Text = "Copyright © 2018. Ce logiciel est la propriété exclusive des douanes algériennes." +
    "";
            // 
            // m_progressBar
            // 
            m_progressBar.Location = new System.Drawing.Point(19, 244);
            m_progressBar.Name = "m_progressBar";
            m_progressBar.Size = new System.Drawing.Size(361, 15);
            m_progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            m_progressBar.TabIndex = 4;
            // 
            // m_lblVersion
            // 
            this.m_lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.m_lblVersion.ForeColor = System.Drawing.Color.White;
            this.m_lblVersion.Location = new System.Drawing.Point(57, 115);
            this.m_lblVersion.Name = "m_lblVersion";
            this.m_lblVersion.Size = new System.Drawing.Size(284, 13);
            this.m_lblVersion.TabIndex = 1;
            this.m_lblVersion.Text = "Version: 1";
            this.m_lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblMessage
            // 
            this.m_lblMessage.AutoEllipsis = true;
            this.m_lblMessage.AutoSize = true;
            this.m_lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.m_lblMessage.ForeColor = System.Drawing.Color.SteelBlue;
            this.m_lblMessage.Location = new System.Drawing.Point(19, 228);
            this.m_lblMessage.Name = "m_lblMessage";
            this.m_lblMessage.Size = new System.Drawing.Size(70, 13);
            this.m_lblMessage.TabIndex = 5;
            this.m_lblMessage.Text = "Initialisation...";
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(399, 302);
            this.Controls.Add(this.m_lblMessage);
            this.Controls.Add(m_progressBar);
            this.Controls.Add(m_lblFooter);
            this.Controls.Add(m_lblHeader);
            this.Controls.Add(this.m_lblVersion);
            this.Controls.Add(m_lblAppName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HUB de la valeur en douanes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblVersion;
        private System.Windows.Forms.Label m_lblMessage;
    }
}