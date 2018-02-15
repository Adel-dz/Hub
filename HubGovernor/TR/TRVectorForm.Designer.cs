namespace DGD.HubGovernor.TR
{
    partial class TRVectorForm
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
            System.Windows.Forms.Label m_lblProduct;
            System.Windows.Forms.Button m_btnPickProduct;
            System.Windows.Forms.PictureBox m_pictureBox;
            System.Windows.Forms.Label m_lblPrice;
            System.Windows.Forms.Label m_lblDate;
            System.Windows.Forms.GroupBox m_grpProdContext;
            System.Windows.Forms.Label m_lblUnit;
            System.Windows.Forms.Label m_lblCurrency;
            System.Windows.Forms.Label m_lblPlace;
            System.Windows.Forms.Label m_lblIncoterms;
            System.Windows.Forms.Label m_lblOrigin;
            System.Windows.Forms.Label m_lbMsg;
            System.Windows.Forms.GroupBox m_grpSep;
            System.Windows.Forms.Label m_lblSession;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TRVectorForm));
            this.m_tsbSave = new System.Windows.Forms.ToolStripButton();
            this.m_tsbReload = new System.Windows.Forms.ToolStripButton();
            this.m_cbUnits = new System.Windows.Forms.ComboBox();
            this.m_cbPlaces = new System.Windows.Forms.ComboBox();
            this.m_cbCurrencies = new System.Windows.Forms.ComboBox();
            this.m_cbOrign = new System.Windows.Forms.ComboBox();
            this.m_cbIncoterms = new System.Windows.Forms.ComboBox();
            this.m_tbProduct = new System.Windows.Forms.TextBox();
            this.m_nupPrice = new System.Windows.Forms.NumericUpDown();
            this.m_dtpSpotTime = new System.Windows.Forms.DateTimePicker();
            this.m_lblSupplier = new System.Windows.Forms.Label();
            this.m_nudSession = new System.Windows.Forms.NumericUpDown();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_lblProduct = new System.Windows.Forms.Label();
            m_btnPickProduct = new System.Windows.Forms.Button();
            m_pictureBox = new System.Windows.Forms.PictureBox();
            m_lblPrice = new System.Windows.Forms.Label();
            m_lblDate = new System.Windows.Forms.Label();
            m_grpProdContext = new System.Windows.Forms.GroupBox();
            m_lblUnit = new System.Windows.Forms.Label();
            m_lblCurrency = new System.Windows.Forms.Label();
            m_lblPlace = new System.Windows.Forms.Label();
            m_lblIncoterms = new System.Windows.Forms.Label();
            m_lblOrigin = new System.Windows.Forms.Label();
            m_lbMsg = new System.Windows.Forms.Label();
            m_grpSep = new System.Windows.Forms.GroupBox();
            m_lblSession = new System.Windows.Forms.Label();
            m_toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).BeginInit();
            m_grpProdContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudSession)).BeginInit();
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
            m_toolStrip.Size = new System.Drawing.Size(471, 25);
            m_toolStrip.TabIndex = 6;
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
            // m_lblProduct
            // 
            m_lblProduct.AutoSize = true;
            m_lblProduct.Location = new System.Drawing.Point(96, 63);
            m_lblProduct.Name = "m_lblProduct";
            m_lblProduct.Size = new System.Drawing.Size(47, 13);
            m_lblProduct.TabIndex = 8;
            m_lblProduct.Text = "Produit:*";
            // 
            // m_btnPickProduct
            // 
            m_btnPickProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            m_btnPickProduct.Image = global::DGD.HubGovernor.Properties.Resources.pick_16;
            m_btnPickProduct.Location = new System.Drawing.Point(418, 77);
            m_btnPickProduct.Name = "m_btnPickProduct";
            m_btnPickProduct.Size = new System.Drawing.Size(24, 23);
            m_btnPickProduct.TabIndex = 10;
            m_btnPickProduct.UseVisualStyleBackColor = true;
            // 
            // m_pictureBox
            // 
            m_pictureBox.Image = global::DGD.HubGovernor.Properties.Resources.spot_value_64;
            m_pictureBox.Location = new System.Drawing.Point(12, 43);
            m_pictureBox.Name = "m_pictureBox";
            m_pictureBox.Size = new System.Drawing.Size(64, 64);
            m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pictureBox.TabIndex = 7;
            m_pictureBox.TabStop = false;
            // 
            // m_lblPrice
            // 
            m_lblPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            m_lblPrice.AutoSize = true;
            m_lblPrice.Location = new System.Drawing.Point(315, 113);
            m_lblPrice.Name = "m_lblPrice";
            m_lblPrice.Size = new System.Drawing.Size(31, 13);
            m_lblPrice.TabIndex = 11;
            m_lblPrice.Text = "Prix:*";
            // 
            // m_lblDate
            // 
            m_lblDate.AutoSize = true;
            m_lblDate.Location = new System.Drawing.Point(102, 113);
            m_lblDate.Name = "m_lblDate";
            m_lblDate.Size = new System.Drawing.Size(37, 13);
            m_lblDate.TabIndex = 13;
            m_lblDate.Text = "Date:*";
            // 
            // m_grpProdContext
            // 
            m_grpProdContext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_grpProdContext.Controls.Add(this.m_cbUnits);
            m_grpProdContext.Controls.Add(m_lblUnit);
            m_grpProdContext.Controls.Add(this.m_cbPlaces);
            m_grpProdContext.Controls.Add(this.m_cbCurrencies);
            m_grpProdContext.Controls.Add(this.m_cbOrign);
            m_grpProdContext.Controls.Add(m_lblCurrency);
            m_grpProdContext.Controls.Add(m_lblPlace);
            m_grpProdContext.Controls.Add(this.m_cbIncoterms);
            m_grpProdContext.Controls.Add(m_lblIncoterms);
            m_grpProdContext.Controls.Add(m_lblOrigin);
            m_grpProdContext.Location = new System.Drawing.Point(12, 134);
            m_grpProdContext.Name = "m_grpProdContext";
            m_grpProdContext.Size = new System.Drawing.Size(430, 170);
            m_grpProdContext.TabIndex = 15;
            m_grpProdContext.TabStop = false;
            m_grpProdContext.Text = " Contexte de produit ";
            // 
            // m_cbUnits
            // 
            this.m_cbUnits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cbUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbUnits.FormattingEnabled = true;
            this.m_cbUnits.Location = new System.Drawing.Point(274, 108);
            this.m_cbUnits.Name = "m_cbUnits";
            this.m_cbUnits.Size = new System.Drawing.Size(141, 21);
            this.m_cbUnits.Sorted = true;
            this.m_cbUnits.TabIndex = 18;
            // 
            // m_lblUnit
            // 
            m_lblUnit.AutoSize = true;
            m_lblUnit.Location = new System.Drawing.Point(274, 92);
            m_lblUnit.Name = "m_lblUnit";
            m_lblUnit.Size = new System.Drawing.Size(39, 13);
            m_lblUnit.TabIndex = 17;
            m_lblUnit.Text = "Unité:*";
            // 
            // m_cbPlaces
            // 
            this.m_cbPlaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbPlaces.FormattingEnabled = true;
            this.m_cbPlaces.Location = new System.Drawing.Point(6, 129);
            this.m_cbPlaces.Name = "m_cbPlaces";
            this.m_cbPlaces.Size = new System.Drawing.Size(230, 21);
            this.m_cbPlaces.Sorted = true;
            this.m_cbPlaces.TabIndex = 18;
            // 
            // m_cbCurrencies
            // 
            this.m_cbCurrencies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cbCurrencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbCurrencies.FormattingEnabled = true;
            this.m_cbCurrencies.Location = new System.Drawing.Point(274, 60);
            this.m_cbCurrencies.Name = "m_cbCurrencies";
            this.m_cbCurrencies.Size = new System.Drawing.Size(141, 21);
            this.m_cbCurrencies.Sorted = true;
            this.m_cbCurrencies.TabIndex = 16;
            // 
            // m_cbOrign
            // 
            this.m_cbOrign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbOrign.FormattingEnabled = true;
            this.m_cbOrign.Location = new System.Drawing.Point(6, 39);
            this.m_cbOrign.Name = "m_cbOrign";
            this.m_cbOrign.Size = new System.Drawing.Size(230, 21);
            this.m_cbOrign.Sorted = true;
            this.m_cbOrign.TabIndex = 17;
            // 
            // m_lblCurrency
            // 
            m_lblCurrency.AutoSize = true;
            m_lblCurrency.Location = new System.Drawing.Point(274, 44);
            m_lblCurrency.Name = "m_lblCurrency";
            m_lblCurrency.Size = new System.Drawing.Size(55, 13);
            m_lblCurrency.TabIndex = 0;
            m_lblCurrency.Text = "Monnaie:*";
            // 
            // m_lblPlace
            // 
            m_lblPlace.AutoSize = true;
            m_lblPlace.Location = new System.Drawing.Point(6, 113);
            m_lblPlace.Name = "m_lblPlace";
            m_lblPlace.Size = new System.Drawing.Size(30, 13);
            m_lblPlace.TabIndex = 16;
            m_lblPlace.Text = "Lieu:";
            // 
            // m_cbIncoterms
            // 
            this.m_cbIncoterms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbIncoterms.FormattingEnabled = true;
            this.m_cbIncoterms.Location = new System.Drawing.Point(6, 84);
            this.m_cbIncoterms.Name = "m_cbIncoterms";
            this.m_cbIncoterms.Size = new System.Drawing.Size(91, 21);
            this.m_cbIncoterms.Sorted = true;
            this.m_cbIncoterms.TabIndex = 15;
            // 
            // m_lblIncoterms
            // 
            m_lblIncoterms.AutoSize = true;
            m_lblIncoterms.Location = new System.Drawing.Point(6, 68);
            m_lblIncoterms.Name = "m_lblIncoterms";
            m_lblIncoterms.Size = new System.Drawing.Size(56, 13);
            m_lblIncoterms.TabIndex = 14;
            m_lblIncoterms.Text = "Incoterms:";
            // 
            // m_lblOrigin
            // 
            m_lblOrigin.AutoSize = true;
            m_lblOrigin.Location = new System.Drawing.Point(6, 23);
            m_lblOrigin.Name = "m_lblOrigin";
            m_lblOrigin.Size = new System.Drawing.Size(47, 13);
            m_lblOrigin.TabIndex = 11;
            m_lblOrigin.Text = "Origine:*";
            // 
            // m_lbMsg
            // 
            m_lbMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            m_lbMsg.AutoSize = true;
            m_lbMsg.Location = new System.Drawing.Point(9, 343);
            m_lbMsg.Name = "m_lbMsg";
            m_lbMsg.Size = new System.Drawing.Size(300, 13);
            m_lbMsg.TabIndex = 19;
            m_lbMsg.Text = "Les éléments marqués d’un astérisque (*)  doivent êtres servis.";
            // 
            // m_grpSep
            // 
            m_grpSep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_grpSep.Location = new System.Drawing.Point(12, 338);
            m_grpSep.Name = "m_grpSep";
            m_grpSep.Size = new System.Drawing.Size(441, 2);
            m_grpSep.TabIndex = 18;
            m_grpSep.TabStop = false;
            // 
            // m_tbProduct
            // 
            this.m_tbProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbProduct.BackColor = System.Drawing.SystemColors.Info;
            this.m_tbProduct.Location = new System.Drawing.Point(99, 80);
            this.m_tbProduct.Name = "m_tbProduct";
            this.m_tbProduct.ReadOnly = true;
            this.m_tbProduct.Size = new System.Drawing.Size(313, 20);
            this.m_tbProduct.TabIndex = 9;
            // 
            // m_nupPrice
            // 
            this.m_nupPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_nupPrice.Location = new System.Drawing.Point(354, 109);
            this.m_nupPrice.Name = "m_nupPrice";
            this.m_nupPrice.Size = new System.Drawing.Size(88, 20);
            this.m_nupPrice.TabIndex = 12;
            // 
            // m_dtpSpotTime
            // 
            this.m_dtpSpotTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dtpSpotTime.Location = new System.Drawing.Point(147, 109);
            this.m_dtpSpotTime.Name = "m_dtpSpotTime";
            this.m_dtpSpotTime.Size = new System.Drawing.Size(116, 20);
            this.m_dtpSpotTime.TabIndex = 14;
            // 
            // m_lblSupplier
            // 
            this.m_lblSupplier.AutoSize = true;
            this.m_lblSupplier.Location = new System.Drawing.Point(18, 307);
            this.m_lblSupplier.Name = "m_lblSupplier";
            this.m_lblSupplier.Size = new System.Drawing.Size(10, 13);
            this.m_lblSupplier.TabIndex = 17;
            this.m_lblSupplier.Text = "-";
            // 
            // m_lblSession
            // 
            m_lblSession.AutoSize = true;
            m_lblSession.Location = new System.Drawing.Point(96, 43);
            m_lblSession.Name = "m_lblSession";
            m_lblSession.Size = new System.Drawing.Size(51, 13);
            m_lblSession.TabIndex = 20;
            m_lblSession.Text = "Session:*";
            // 
            // m_nudSession
            // 
            this.m_nudSession.Location = new System.Drawing.Point(153, 41);
            this.m_nudSession.Name = "m_nudSession";
            this.m_nudSession.Size = new System.Drawing.Size(95, 20);
            this.m_nudSession.TabIndex = 21;
            // 
            // TRVectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(471, 366);
            this.Controls.Add(this.m_nudSession);
            this.Controls.Add(m_lblSession);
            this.Controls.Add(m_lbMsg);
            this.Controls.Add(m_grpSep);
            this.Controls.Add(this.m_lblSupplier);
            this.Controls.Add(m_grpProdContext);
            this.Controls.Add(this.m_dtpSpotTime);
            this.Controls.Add(m_lblDate);
            this.Controls.Add(this.m_nupPrice);
            this.Controls.Add(m_lblPrice);
            this.Controls.Add(m_btnPickProduct);
            this.Controls.Add(this.m_tbProduct);
            this.Controls.Add(m_lblProduct);
            this.Controls.Add(m_pictureBox);
            this.Controls.Add(m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(474, 398);
            this.Name = "TRVectorForm";
            this.Text = "Valeur Spot";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).EndInit();
            m_grpProdContext.ResumeLayout(false);
            m_grpProdContext.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudSession)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbSave;
        private System.Windows.Forms.ToolStripButton m_tsbReload;
        private System.Windows.Forms.TextBox m_tbProduct;
        private System.Windows.Forms.NumericUpDown m_nupPrice;
        private System.Windows.Forms.DateTimePicker m_dtpSpotTime;
        private System.Windows.Forms.ComboBox m_cbPlaces;
        private System.Windows.Forms.ComboBox m_cbOrign;
        private System.Windows.Forms.ComboBox m_cbIncoterms;
        private System.Windows.Forms.ComboBox m_cbUnits;
        private System.Windows.Forms.ComboBox m_cbCurrencies;
        private System.Windows.Forms.Label m_lblSupplier;
        private System.Windows.Forms.NumericUpDown m_nudSession;
    }
}