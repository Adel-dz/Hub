namespace DGD.HubGovernor.Arch
{
    partial class BackupPage
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
            System.Windows.Forms.Label m_lblCaption;
            System.Windows.Forms.Button m_btnStart;
            System.Windows.Forms.Label m_lblInfo;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupPage));
            this.m_pbLogo = new System.Windows.Forms.PictureBox();
            m_lblCaption = new System.Windows.Forms.Label();
            m_btnStart = new System.Windows.Forms.Button();
            m_lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblCaption
            // 
            m_lblCaption.Anchor = System.Windows.Forms.AnchorStyles.Top;
            m_lblCaption.AutoSize = true;
            m_lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblCaption.ForeColor = System.Drawing.Color.SteelBlue;
            m_lblCaption.Location = new System.Drawing.Point(182, 12);
            m_lblCaption.Name = "m_lblCaption";
            m_lblCaption.Size = new System.Drawing.Size(138, 25);
            m_lblCaption.TabIndex = 1;
            m_lblCaption.Text = "Sauvegarde";
            // 
            // m_btnStart
            // 
            m_btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            m_btnStart.Location = new System.Drawing.Point(144, 244);
            m_btnStart.Name = "m_btnStart";
            m_btnStart.Size = new System.Drawing.Size(176, 34);
            m_btnStart.TabIndex = 8;
            m_btnStart.Text = "Démarrer la sauvegarde";
            m_btnStart.UseVisualStyleBackColor = true;
            m_btnStart.Click += new System.EventHandler(this.Start_Click);
            // 
            // m_lblInfo
            // 
            m_lblInfo.BackColor = System.Drawing.Color.Transparent;
            m_lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblInfo.Location = new System.Drawing.Point(15, 66);
            m_lblInfo.Name = "m_lblInfo";
            m_lblInfo.Size = new System.Drawing.Size(443, 126);
            m_lblInfo.TabIndex = 9;
            m_lblInfo.Text = resources.GetString("m_lblInfo.Text");
            // 
            // m_pbLogo
            // 
            this.m_pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("m_pbLogo.Image")));
            this.m_pbLogo.Location = new System.Drawing.Point(355, 175);
            this.m_pbLogo.Name = "m_pbLogo";
            this.m_pbLogo.Size = new System.Drawing.Size(148, 131);
            this.m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_pbLogo.TabIndex = 10;
            this.m_pbLogo.TabStop = false;
            // 
            // BackupPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(m_btnStart);
            this.Controls.Add(m_lblInfo);
            this.Controls.Add(m_lblCaption);
            this.Controls.Add(this.m_pbLogo);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "BackupPage";
            this.Size = new System.Drawing.Size(503, 309);
            ((System.ComponentModel.ISupportInitialize)(this.m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_pbLogo;
    }
}
