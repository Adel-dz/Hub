namespace DGD.HubGovernor.Countries
{
    partial class CountryForm
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
            System.Windows.Forms.ToolStrip m_toolStrip;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripButton m_tsbOption;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.Label m_lblName;
            System.Windows.Forms.Label m_lblInternalCode;
            System.Windows.Forms.Label m_lblIsoCode;
            System.Windows.Forms.GroupBox m_grpSep;
            System.Windows.Forms.PictureBox m_pictureBox;
            System.Windows.Forms.Label m_lbMsg;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountryForm));
            this.m_tsbSave = new System.Windows.Forms.ToolStripButton();
            this.m_tsbReload = new System.Windows.Forms.ToolStripButton();
            this.m_tbName = new System.Windows.Forms.TextBox();
            this.m_nudInternalCode = new System.Windows.Forms.NumericUpDown();
            this.m_tbIsoCode = new System.Windows.Forms.TextBox();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbOption = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_lblName = new System.Windows.Forms.Label();
            m_lblInternalCode = new System.Windows.Forms.Label();
            m_lblIsoCode = new System.Windows.Forms.Label();
            m_grpSep = new System.Windows.Forms.GroupBox();
            m_pictureBox = new System.Windows.Forms.PictureBox();
            m_lbMsg = new System.Windows.Forms.Label();
            m_toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudInternalCode)).BeginInit();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbSave,
            toolStripSeparator1,
            this.m_tsbReload,
            toolStripSeparator2,
            m_tsbOption,
            m_tsbHelp});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(390, 25);
            m_toolStrip.TabIndex = 3;
            m_toolStrip.TabStop = true;
            // 
            // m_tsbSave
            // 
            this.m_tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbSave.Enabled = false;
            this.m_tsbSave.Image = global::DGD.HubGovernor.Properties.Resources.save_16;
            this.m_tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbSave.Name = "m_tsbSave";
            this.m_tsbSave.Size = new System.Drawing.Size(23, 22);
            this.m_tsbSave.Text = "Enregister";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbReload
            // 
            this.m_tsbReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbReload.Enabled = false;
            this.m_tsbReload.Image = global::DGD.HubGovernor.Properties.Resources.refresh_16;
            this.m_tsbReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbReload.Name = "m_tsbReload";
            this.m_tsbReload.Size = new System.Drawing.Size(23, 22);
            this.m_tsbReload.Text = "Recharger";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbOption
            // 
            m_tsbOption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbOption.Image = global::DGD.HubGovernor.Properties.Resources.option_16;
            m_tsbOption.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbOption.Name = "m_tsbOption";
            m_tsbOption.Size = new System.Drawing.Size(23, 22);
            m_tsbOption.Text = "Options";
            // 
            // m_tsbHelp
            // 
            m_tsbHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            m_tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbHelp.Image = global::DGD.HubGovernor.Properties.Resources.help_16;
            m_tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbHelp.Name = "m_tsbHelp";
            m_tsbHelp.Size = new System.Drawing.Size(23, 22);
            m_tsbHelp.Text = "Aide";
            // 
            // m_lblName
            // 
            m_lblName.AutoSize = true;
            m_lblName.Location = new System.Drawing.Point(100, 41);
            m_lblName.Name = "m_lblName";
            m_lblName.Size = new System.Drawing.Size(37, 13);
            m_lblName.TabIndex = 2;
            m_lblName.Text = "Pays*:";
            // 
            // m_lblInternalCode
            // 
            m_lblInternalCode.AutoSize = true;
            m_lblInternalCode.Location = new System.Drawing.Point(100, 77);
            m_lblInternalCode.Name = "m_lblInternalCode";
            m_lblInternalCode.Size = new System.Drawing.Size(64, 13);
            m_lblInternalCode.TabIndex = 4;
            m_lblInternalCode.Text = "Code pays*:";
            // 
            // m_lblIsoCode
            // 
            m_lblIsoCode.AutoSize = true;
            m_lblIsoCode.Location = new System.Drawing.Point(100, 113);
            m_lblIsoCode.Name = "m_lblIsoCode";
            m_lblIsoCode.Size = new System.Drawing.Size(92, 13);
            m_lblIsoCode.TabIndex = 6;
            m_lblIsoCode.Text = "Code ISO 3166-1:";
            // 
            // m_grpSep
            // 
            m_grpSep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_grpSep.Location = new System.Drawing.Point(12, 134);
            m_grpSep.Name = "m_grpSep";
            m_grpSep.Size = new System.Drawing.Size(366, 2);
            m_grpSep.TabIndex = 8;
            m_grpSep.TabStop = false;
            // 
            // m_pictureBox
            // 
            m_pictureBox.Image = global::DGD.HubGovernor.Properties.Resources.country_64;
            m_pictureBox.Location = new System.Drawing.Point(12, 44);
            m_pictureBox.Name = "m_pictureBox";
            m_pictureBox.Size = new System.Drawing.Size(64, 64);
            m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pictureBox.TabIndex = 1;
            m_pictureBox.TabStop = false;
            // 
            // m_lbMsg
            // 
            m_lbMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            m_lbMsg.AutoSize = true;
            m_lbMsg.Location = new System.Drawing.Point(13, 143);
            m_lbMsg.Name = "m_lbMsg";
            m_lbMsg.Size = new System.Drawing.Size(300, 13);
            m_lbMsg.TabIndex = 9;
            m_lbMsg.Text = "Les éléments marqués d’un astérisque (*)  doivent êtres servis.";
            // 
            // m_tbName
            // 
            this.m_tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbName.Location = new System.Drawing.Point(198, 37);
            this.m_tbName.Name = "m_tbName";
            this.m_tbName.Size = new System.Drawing.Size(180, 20);
            this.m_tbName.TabIndex = 0;
            // 
            // m_nudInternalCode
            // 
            this.m_nudInternalCode.Location = new System.Drawing.Point(198, 73);
            this.m_nudInternalCode.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_nudInternalCode.Name = "m_nudInternalCode";
            this.m_nudInternalCode.Size = new System.Drawing.Size(77, 20);
            this.m_nudInternalCode.TabIndex = 1;
            this.m_nudInternalCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_tbIsoCode
            // 
            this.m_tbIsoCode.Location = new System.Drawing.Point(198, 109);
            this.m_tbIsoCode.Name = "m_tbIsoCode";
            this.m_tbIsoCode.Size = new System.Drawing.Size(77, 20);
            this.m_tbIsoCode.TabIndex = 2;
            // 
            // CountryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(390, 166);
            this.Controls.Add(m_lbMsg);
            this.Controls.Add(m_grpSep);
            this.Controls.Add(this.m_tbIsoCode);
            this.Controls.Add(m_lblIsoCode);
            this.Controls.Add(this.m_nudInternalCode);
            this.Controls.Add(m_lblInternalCode);
            this.Controls.Add(this.m_tbName);
            this.Controls.Add(m_lblName);
            this.Controls.Add(m_pictureBox);
            this.Controls.Add(m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(304, 204);
            this.Name = "CountryForm";
            this.Text = "Pays";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudInternalCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbSave;
        private System.Windows.Forms.ToolStripButton m_tsbReload;
        private System.Windows.Forms.TextBox m_tbName;
        private System.Windows.Forms.NumericUpDown m_nudInternalCode;
        private System.Windows.Forms.TextBox m_tbIsoCode;
    }
}