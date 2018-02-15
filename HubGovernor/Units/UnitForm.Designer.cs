namespace DGD.HubGovernor.Units
{
    partial class UnitForm
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
            System.Windows.Forms.PictureBox m_pictureBox;
            System.Windows.Forms.ToolStripButton m_tsbOptions;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.Label m_lbl_Name;
            System.Windows.Forms.Label m_lbMsg;
            System.Windows.Forms.GroupBox m_grpSep;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitForm));
            this.m_tsbSave = new System.Windows.Forms.ToolStripButton();
            this.m_tsbReload = new System.Windows.Forms.ToolStripButton();
            this.m_tbName = new System.Windows.Forms.TextBox();
            this.m_lblDescription = new System.Windows.Forms.Label();
            this.m_tbDescription = new System.Windows.Forms.TextBox();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_pictureBox = new System.Windows.Forms.PictureBox();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_lbl_Name = new System.Windows.Forms.Label();
            m_lbMsg = new System.Windows.Forms.Label();
            m_grpSep = new System.Windows.Forms.GroupBox();
            m_toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbSave,
            toolStripSeparator1,
            this.m_tsbReload,
            toolStripSeparator2,
            m_tsbOptions,
            m_tsbHelp});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(457, 25);
            m_toolStrip.TabIndex = 7;
            m_toolStrip.TabStop = true;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_pictureBox
            // 
            m_pictureBox.Image = global::DGD.HubGovernor.Properties.Resources.unit_64;
            m_pictureBox.Location = new System.Drawing.Point(12, 53);
            m_pictureBox.Name = "m_pictureBox";
            m_pictureBox.Size = new System.Drawing.Size(64, 64);
            m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pictureBox.TabIndex = 8;
            m_pictureBox.TabStop = false;
            // 
            // m_tsbSave
            // 
            this.m_tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbSave.Enabled = false;
            this.m_tsbSave.Image = global::DGD.HubGovernor.Properties.Resources.save_16;
            this.m_tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbSave.Name = "m_tsbSave";
            this.m_tsbSave.Size = new System.Drawing.Size(23, 22);
            this.m_tsbSave.Text = "Enregistrer";
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
            // m_tsbOptions
            // 
            m_tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbOptions.Image = global::DGD.HubGovernor.Properties.Resources.option_16;
            m_tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbOptions.Name = "m_tsbOptions";
            m_tsbOptions.Size = new System.Drawing.Size(23, 22);
            m_tsbOptions.Text = "Options...";
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
            // m_lbl_Name
            // 
            m_lbl_Name.AutoSize = true;
            m_lbl_Name.Location = new System.Drawing.Point(98, 57);
            m_lbl_Name.Name = "m_lbl_Name";
            m_lbl_Name.Size = new System.Drawing.Size(39, 13);
            m_lbl_Name.TabIndex = 9;
            m_lbl_Name.Text = "Unité*:";
            // 
            // m_tbName
            // 
            this.m_tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbName.Location = new System.Drawing.Point(169, 53);
            this.m_tbName.MaximumSize = new System.Drawing.Size(144, 20);
            this.m_tbName.MinimumSize = new System.Drawing.Size(89, 20);
            this.m_tbName.Name = "m_tbName";
            this.m_tbName.Size = new System.Drawing.Size(144, 20);
            this.m_tbName.TabIndex = 10;
            // 
            // m_lblDescription
            // 
            this.m_lblDescription.AutoSize = true;
            this.m_lblDescription.Location = new System.Drawing.Point(98, 95);
            this.m_lblDescription.Name = "m_lblDescription";
            this.m_lblDescription.Size = new System.Drawing.Size(63, 13);
            this.m_lblDescription.TabIndex = 11;
            this.m_lblDescription.Text = "Description:";
            // 
            // m_tbDescription
            // 
            this.m_tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbDescription.Location = new System.Drawing.Point(169, 91);
            this.m_tbDescription.MinimumSize = new System.Drawing.Size(89, 20);
            this.m_tbDescription.Name = "m_tbDescription";
            this.m_tbDescription.Size = new System.Drawing.Size(268, 20);
            this.m_tbDescription.TabIndex = 12;
            // 
            // m_lbMsg
            // 
            m_lbMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            m_lbMsg.AutoSize = true;
            m_lbMsg.Location = new System.Drawing.Point(9, 158);
            m_lbMsg.Name = "m_lbMsg";
            m_lbMsg.Size = new System.Drawing.Size(300, 13);
            m_lbMsg.TabIndex = 19;
            m_lbMsg.Text = "Les éléments marqués d’un astérisque (*)  doivent êtres servis.";
            // 
            // m_grpSep
            // 
            m_grpSep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_grpSep.Location = new System.Drawing.Point(12, 153);
            m_grpSep.Name = "m_grpSep";
            m_grpSep.Size = new System.Drawing.Size(427, 2);
            m_grpSep.TabIndex = 18;
            m_grpSep.TabStop = false;
            // 
            // UnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(457, 186);
            this.Controls.Add(m_lbMsg);
            this.Controls.Add(m_grpSep);
            this.Controls.Add(this.m_tbDescription);
            this.Controls.Add(this.m_lblDescription);
            this.Controls.Add(this.m_tbName);
            this.Controls.Add(m_lbl_Name);
            this.Controls.Add(m_pictureBox);
            this.Controls.Add(m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(294, 196);
            this.Name = "UnitForm";
            this.Text = "Unité de mesure";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbSave;
        private System.Windows.Forms.ToolStripButton m_tsbReload;
        private System.Windows.Forms.TextBox m_tbName;
        private System.Windows.Forms.Label m_lblDescription;
        private System.Windows.Forms.TextBox m_tbDescription;
    }
}