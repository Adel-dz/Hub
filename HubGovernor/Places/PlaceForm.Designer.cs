namespace DGD.HubGovernor.Places
{
    partial class PlaceForm
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
            System.Windows.Forms.ToolStrip m_toolStrip;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            System.Windows.Forms.ToolStripButton m_tsbOptions;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.Label m_lblName;
            System.Windows.Forms.Label m_lblCountry;
            System.Windows.Forms.GroupBox m_grpSep;
            System.Windows.Forms.PictureBox m_pictureBox;
            System.Windows.Forms.Label m_lbMsg;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaceForm));
            this.m_tsbSave = new System.Windows.Forms.ToolStripButton();
            this.m_tsbReload = new System.Windows.Forms.ToolStripButton();
            this.m_tsbAddCountry = new System.Windows.Forms.ToolStripButton();
            this.m_tbName = new System.Windows.Forms.TextBox();
            this.m_cbCountries = new System.Windows.Forms.ComboBox();
            this.m_errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_lblName = new System.Windows.Forms.Label();
            m_lblCountry = new System.Windows.Forms.Label();
            m_grpSep = new System.Windows.Forms.GroupBox();
            m_pictureBox = new System.Windows.Forms.PictureBox();
            m_lbMsg = new System.Windows.Forms.Label();
            m_toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbSave,
            toolStripSeparator1,
            this.m_tsbReload,
            toolStripSeparator2,
            this.m_tsbAddCountry,
            toolStripSeparator3,
            m_tsbOptions,
            m_tsbHelp});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(325, 25);
            m_toolStrip.TabIndex = 4;
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
            // m_tsbAddCountry
            // 
            this.m_tsbAddCountry.Enabled = false;
            this.m_tsbAddCountry.Image = global::DGD.HubGovernor.Properties.Resources.new_row_16;
            this.m_tsbAddCountry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbAddCountry.Name = "m_tsbAddCountry";
            this.m_tsbAddCountry.Size = new System.Drawing.Size(110, 22);
            this.m_tsbAddCountry.Text = "Ajouter un pays";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // m_lblName
            // 
            m_lblName.AutoSize = true;
            m_lblName.Location = new System.Drawing.Point(101, 45);
            m_lblName.Name = "m_lblName";
            m_lblName.Size = new System.Drawing.Size(34, 13);
            m_lblName.TabIndex = 0;
            m_lblName.Text = "Lieu*:";
            // 
            // m_lblCountry
            // 
            m_lblCountry.AutoSize = true;
            m_lblCountry.Location = new System.Drawing.Point(101, 96);
            m_lblCountry.Name = "m_lblCountry";
            m_lblCountry.Size = new System.Drawing.Size(37, 13);
            m_lblCountry.TabIndex = 2;
            m_lblCountry.Text = "Pays*:";
            // 
            // m_grpSep
            // 
            m_grpSep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_grpSep.Location = new System.Drawing.Point(13, 165);
            m_grpSep.Name = "m_grpSep";
            m_grpSep.Size = new System.Drawing.Size(299, 2);
            m_grpSep.TabIndex = 12;
            m_grpSep.TabStop = false;
            // 
            // m_pictureBox
            // 
            m_pictureBox.Image = global::DGD.HubGovernor.Properties.Resources.place_64;
            m_pictureBox.Location = new System.Drawing.Point(13, 46);
            m_pictureBox.Name = "m_pictureBox";
            m_pictureBox.Size = new System.Drawing.Size(64, 64);
            m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pictureBox.TabIndex = 2;
            m_pictureBox.TabStop = false;
            // 
            // m_lbMsg
            // 
            m_lbMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            m_lbMsg.AutoSize = true;
            m_lbMsg.Location = new System.Drawing.Point(13, 174);
            m_lbMsg.Name = "m_lbMsg";
            m_lbMsg.Size = new System.Drawing.Size(300, 13);
            m_lbMsg.TabIndex = 13;
            m_lbMsg.Text = "Les éléments marqués d’un astérisque (*)  doivent êtres servis.";
            // 
            // m_tbName
            // 
            this.m_tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbName.Location = new System.Drawing.Point(101, 61);
            this.m_tbName.Name = "m_tbName";
            this.m_tbName.Size = new System.Drawing.Size(193, 20);
            this.m_tbName.TabIndex = 1;
            // 
            // m_cbCountries
            // 
            this.m_cbCountries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cbCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbCountries.FormattingEnabled = true;
            this.m_errProvider.SetIconPadding(this.m_cbCountries, 2);
            this.m_cbCountries.Location = new System.Drawing.Point(101, 112);
            this.m_cbCountries.Name = "m_cbCountries";
            this.m_cbCountries.Size = new System.Drawing.Size(193, 21);
            this.m_cbCountries.TabIndex = 3;
            // 
            // m_errProvider
            // 
            this.m_errProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.m_errProvider.ContainerControl = this;
            this.m_errProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("m_errProvider.Icon")));
            // 
            // PlaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(325, 201);
            this.Controls.Add(m_lbMsg);
            this.Controls.Add(m_grpSep);
            this.Controls.Add(this.m_cbCountries);
            this.Controls.Add(m_lblCountry);
            this.Controls.Add(this.m_tbName);
            this.Controls.Add(m_lblName);
            this.Controls.Add(m_pictureBox);
            this.Controls.Add(m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(190, 213);
            this.Name = "PlaceForm";
            this.Text = "Lieu";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbSave;
        private System.Windows.Forms.ToolStripButton m_tsbReload;
        private System.Windows.Forms.ToolStripButton m_tsbAddCountry;
        private System.Windows.Forms.TextBox m_tbName;
        private System.Windows.Forms.ComboBox m_cbCountries;
        private System.Windows.Forms.ErrorProvider m_errProvider;
    }
}