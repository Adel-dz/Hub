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
            m_lblCaption = new System.Windows.Forms.Label();
            m_btnStart = new System.Windows.Forms.Button();
            m_lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_lblCaption
            // 
            m_lblCaption.Anchor = System.Windows.Forms.AnchorStyles.Top;
            m_lblCaption.AutoSize = true;
            m_lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblCaption.ForeColor = System.Drawing.Color.SteelBlue;
            m_lblCaption.Location = new System.Drawing.Point(164, 12);
            m_lblCaption.Name = "m_lblCaption";
            m_lblCaption.Size = new System.Drawing.Size(138, 25);
            m_lblCaption.TabIndex = 1;
            m_lblCaption.Text = "Sauvegarde";
            // 
            // m_btnStart
            // 
            m_btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            m_btnStart.Location = new System.Drawing.Point(126, 244);
            m_btnStart.Name = "m_btnStart";
            m_btnStart.Size = new System.Drawing.Size(215, 34);
            m_btnStart.TabIndex = 8;
            m_btnStart.Text = "Démarrer la sauvegarde";
            m_btnStart.UseVisualStyleBackColor = true;
            m_btnStart.Click += new System.EventHandler(this.Start_Click);
            // 
            // m_lblInfo
            // 
            m_lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblInfo.Location = new System.Drawing.Point(15, 66);
            m_lblInfo.Name = "m_lblInfo";
            m_lblInfo.Size = new System.Drawing.Size(443, 126);
            m_lblInfo.TabIndex = 9;
            m_lblInfo.Text = resources.GetString("m_lblInfo.Text");
            // 
            // BackupPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(m_lblInfo);
            this.Controls.Add(m_btnStart);
            this.Controls.Add(m_lblCaption);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "BackupPage";
            this.Size = new System.Drawing.Size(467, 309);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
