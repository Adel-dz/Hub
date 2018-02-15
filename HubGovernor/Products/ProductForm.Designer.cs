namespace DGD.HubGovernor.Products
{
    partial class ProductForm
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
            System.Windows.Forms.ToolStripButton m_tsbOptions;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.PictureBox m_pictureBox;
            System.Windows.Forms.Label m_lblSubHeading;
            System.Windows.Forms.Label m_lblName;
            System.Windows.Forms.GroupBox m_grpSep;
            System.Windows.Forms.Label m_lbMsg;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductForm));
            this.m_tsbSave = new System.Windows.Forms.ToolStripButton();
            this.m_tsbReload = new System.Windows.Forms.ToolStripButton();
            this.m_tbSubHeading = new System.Windows.Forms.TextBox();
            this.m_tbName = new System.Windows.Forms.TextBox();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_pictureBox = new System.Windows.Forms.PictureBox();
            m_lblSubHeading = new System.Windows.Forms.Label();
            m_lblName = new System.Windows.Forms.Label();
            m_grpSep = new System.Windows.Forms.GroupBox();
            m_lbMsg = new System.Windows.Forms.Label();
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
            m_toolStrip.Size = new System.Drawing.Size(361, 25);
            m_toolStrip.TabIndex = 5;
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
            this.m_tsbSave.Text = "Enregistrer";
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
            // m_pictureBox
            // 
            m_pictureBox.Image = global::DGD.HubGovernor.Properties.Resources.product_64;
            m_pictureBox.Location = new System.Drawing.Point(13, 42);
            m_pictureBox.Name = "m_pictureBox";
            m_pictureBox.Size = new System.Drawing.Size(64, 64);
            m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pictureBox.TabIndex = 6;
            m_pictureBox.TabStop = false;
            // 
            // m_lblSubHeading
            // 
            m_lblSubHeading.AutoSize = true;
            m_lblSubHeading.Location = new System.Drawing.Point(84, 46);
            m_lblSubHeading.Name = "m_lblSubHeading";
            m_lblSubHeading.Size = new System.Drawing.Size(53, 13);
            m_lblSubHeading.TabIndex = 7;
            m_lblSubHeading.Text = "SPTF10*:";
            // 
            // m_lblName
            // 
            m_lblName.AutoSize = true;
            m_lblName.Location = new System.Drawing.Point(84, 82);
            m_lblName.Name = "m_lblName";
            m_lblName.Size = new System.Drawing.Size(47, 13);
            m_lblName.TabIndex = 9;
            m_lblName.Text = "Produit*:";
            // 
            // m_grpSep
            // 
            m_grpSep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_grpSep.Location = new System.Drawing.Point(13, 145);
            m_grpSep.Name = "m_grpSep";
            m_grpSep.Size = new System.Drawing.Size(337, 2);
            m_grpSep.TabIndex = 14;
            m_grpSep.TabStop = false;
            // 
            // m_tbSubHeading
            // 
            this.m_tbSubHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbSubHeading.Location = new System.Drawing.Point(142, 42);
            this.m_tbSubHeading.MaximumSize = new System.Drawing.Size(168, 20);
            this.m_tbSubHeading.MinimumSize = new System.Drawing.Size(49, 20);
            this.m_tbSubHeading.Name = "m_tbSubHeading";
            this.m_tbSubHeading.Size = new System.Drawing.Size(101, 20);
            this.m_tbSubHeading.TabIndex = 8;
            // 
            // m_tbName
            // 
            this.m_tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbName.Location = new System.Drawing.Point(142, 78);
            this.m_tbName.MinimumSize = new System.Drawing.Size(49, 20);
            this.m_tbName.Name = "m_tbName";
            this.m_tbName.Size = new System.Drawing.Size(208, 20);
            this.m_tbName.TabIndex = 10;
            // 
            // m_lbMsg
            // 
            m_lbMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            m_lbMsg.AutoSize = true;
            m_lbMsg.Location = new System.Drawing.Point(10, 150);
            m_lbMsg.Name = "m_lbMsg";
            m_lbMsg.Size = new System.Drawing.Size(300, 13);
            m_lbMsg.TabIndex = 15;
            m_lbMsg.Text = "Les éléments marqués d’un astérisque (*)  doivent êtres servis.";
            // 
            // ProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(361, 173);
            this.Controls.Add(m_lbMsg);
            this.Controls.Add(m_grpSep);
            this.Controls.Add(this.m_tbName);
            this.Controls.Add(m_lblName);
            this.Controls.Add(this.m_tbSubHeading);
            this.Controls.Add(m_lblSubHeading);
            this.Controls.Add(m_pictureBox);
            this.Controls.Add(m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(225, 180);
            this.Name = "ProductForm";
            this.Text = "Produit";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbSave;
        private System.Windows.Forms.ToolStripButton m_tsbReload;
        private System.Windows.Forms.TextBox m_tbSubHeading;
        private System.Windows.Forms.TextBox m_tbName;
    }
}