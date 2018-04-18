namespace DGD.HubGovernor.Arch
{
    partial class RestorePage
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
            System.Windows.Forms.ToolStrip m_toolStrip;
            System.Windows.Forms.ToolStripButton m_tsbOpen;
            System.Windows.Forms.ToolStripButton m_tsbSettings;
            System.Windows.Forms.Label m_lblCaption;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestorePage));
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_lblSource = new System.Windows.Forms.Label();
            this.m_lblArchiveInfo = new System.Windows.Forms.Label();
            this.m_btnStart = new System.Windows.Forms.Button();
            this.m_pbLogo = new System.Windows.Forms.PictureBox();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            m_tsbOpen = new System.Windows.Forms.ToolStripButton();
            m_tsbSettings = new System.Windows.Forms.ToolStripButton();
            m_lblCaption = new System.Windows.Forms.Label();
            m_toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbOpen,
            this.toolStripSeparator1,
            this.m_tsbPreview,
            this.toolStripSeparator2,
            m_tsbSettings});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(501, 25);
            m_toolStrip.TabIndex = 7;
            // 
            // m_tsbOpen
            // 
            m_tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbOpen.Image = global::DGD.HubGovernor.Properties.Resources.folder_Open_16;
            m_tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbOpen.Name = "m_tsbOpen";
            m_tsbOpen.Size = new System.Drawing.Size(23, 22);
            m_tsbOpen.Text = "Ouvrir une archive";
            m_tsbOpen.Click += new System.EventHandler(this.Open_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbPreview
            // 
            this.m_tsbPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbPreview.Enabled = false;
            this.m_tsbPreview.Image = global::DGD.HubGovernor.Properties.Resources.show_data_preview_16;
            this.m_tsbPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbPreview.Name = "m_tsbPreview";
            this.m_tsbPreview.Size = new System.Drawing.Size(23, 22);
            this.m_tsbPreview.Text = "Aperçu du contenu de l\'archive";
            this.m_tsbPreview.Click += new System.EventHandler(this.Preview_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbSettings
            // 
            m_tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbSettings.Image = global::DGD.HubGovernor.Properties.Resources.option_16;
            m_tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbSettings.Name = "m_tsbSettings";
            m_tsbSettings.Size = new System.Drawing.Size(23, 22);
            m_tsbSettings.Text = "Paramètres";
            // 
            // m_lblCaption
            // 
            m_lblCaption.Anchor = System.Windows.Forms.AnchorStyles.Top;
            m_lblCaption.AutoSize = true;
            m_lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblCaption.ForeColor = System.Drawing.Color.SteelBlue;
            m_lblCaption.Location = new System.Drawing.Point(177, 36);
            m_lblCaption.Name = "m_lblCaption";
            m_lblCaption.Size = new System.Drawing.Size(146, 25);
            m_lblCaption.TabIndex = 8;
            m_lblCaption.Text = "Restauration";
            // 
            // m_lblSource
            // 
            this.m_lblSource.AutoEllipsis = true;
            this.m_lblSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblSource.Location = new System.Drawing.Point(21, 92);
            this.m_lblSource.Name = "m_lblSource";
            this.m_lblSource.Size = new System.Drawing.Size(435, 51);
            this.m_lblSource.TabIndex = 1;
            this.m_lblSource.Text = "Cliquez sur ouvrir pour choisir une archive à partir de laquelle vous voulez rest" +
    "aurer vos données.";
            // 
            // m_lblArchiveInfo
            // 
            this.m_lblArchiveInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblArchiveInfo.BackColor = System.Drawing.Color.Transparent;
            this.m_lblArchiveInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblArchiveInfo.Location = new System.Drawing.Point(21, 143);
            this.m_lblArchiveInfo.Name = "m_lblArchiveInfo";
            this.m_lblArchiveInfo.Size = new System.Drawing.Size(463, 46);
            this.m_lblArchiveInfo.TabIndex = 4;
            // 
            // m_btnStart
            // 
            this.m_btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.m_btnStart.Enabled = false;
            this.m_btnStart.Location = new System.Drawing.Point(185, 261);
            this.m_btnStart.Name = "m_btnStart";
            this.m_btnStart.Size = new System.Drawing.Size(130, 31);
            this.m_btnStart.TabIndex = 5;
            this.m_btnStart.Text = "Restaurer";
            this.m_btnStart.UseVisualStyleBackColor = true;
            this.m_btnStart.Click += new System.EventHandler(this.Start_Click);
            // 
            // m_pbLogo
            // 
            this.m_pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("m_pbLogo.Image")));
            this.m_pbLogo.Location = new System.Drawing.Point(353, 192);
            this.m_pbLogo.Name = "m_pbLogo";
            this.m_pbLogo.Size = new System.Drawing.Size(148, 131);
            this.m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_pbLogo.TabIndex = 0;
            this.m_pbLogo.TabStop = false;
            // 
            // RestorePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(m_lblCaption);
            this.Controls.Add(m_toolStrip);
            this.Controls.Add(this.m_btnStart);
            this.Controls.Add(this.m_lblArchiveInfo);
            this.Controls.Add(this.m_lblSource);
            this.Controls.Add(this.m_pbLogo);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "RestorePage";
            this.Size = new System.Drawing.Size(501, 323);
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_pbLogo;
        private System.Windows.Forms.Label m_lblArchiveInfo;
        private System.Windows.Forms.Button m_btnStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton m_tsbPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label m_lblSource;
    }
}
