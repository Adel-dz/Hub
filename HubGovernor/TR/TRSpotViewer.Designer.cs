using easyLib.DB;

namespace DGD.HubGovernor.TR
{
    partial class TRSpotViewer
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
            System.Windows.Forms.ToolStripButton m_tsbAddRow;
            System.Windows.Forms.ToolStripButton m_tsbImportData;
            System.Windows.Forms.ToolStripButton m_tsbAdjustColumns;
            System.Windows.Forms.ToolStripButton m_tsbOptions;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.ToolStripComboBox m_tscbSessions;
            System.Windows.Forms.StatusStrip m_statuStrip;
            System.Windows.Forms.ColumnHeader m_colSession;
            System.Windows.Forms.ColumnHeader m_colSubHeading;
            System.Windows.Forms.ColumnHeader m_colLabel;
            System.Windows.Forms.ColumnHeader m_colTRLabel;
            System.Windows.Forms.ColumnHeader m_colPrice;
            System.Windows.Forms.ColumnHeader m_colCurrency;
            System.Windows.Forms.ColumnHeader m_colIncoterm;
            System.Windows.Forms.ColumnHeader m_colPlace;
            System.Windows.Forms.ColumnHeader m_colTime;
            System.Windows.Forms.ColumnHeader m_colUnit;
            System.Windows.Forms.ColumnHeader m_colOrigin;
            System.Windows.Forms.ColumnHeader m_colPruductNber;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TRSpotViewer));
            this.m_tsbDeleteRow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_slRowCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lvData = new System.Windows.Forms.ListView();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            m_tsbAddRow = new System.Windows.Forms.ToolStripButton();
            m_tsbImportData = new System.Windows.Forms.ToolStripButton();
            m_tsbAdjustColumns = new System.Windows.Forms.ToolStripButton();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_tscbSessions = new System.Windows.Forms.ToolStripComboBox();
            m_statuStrip = new System.Windows.Forms.StatusStrip();
            m_colSession = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colSubHeading = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colTRLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colCurrency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colIncoterm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colPlace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colUnit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colOrigin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colPruductNber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_toolStrip.SuspendLayout();
            m_statuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbAddRow,
            this.m_tsbDeleteRow,
            this.toolStripSeparator1,
            m_tsbImportData,
            this.toolStripSeparator3,
            m_tsbAdjustColumns,
            m_tsbOptions,
            m_tsbHelp,
            m_tscbSessions});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(798, 25);
            m_toolStrip.TabIndex = 0;
            // 
            // m_tsbAddRow
            // 
            m_tsbAddRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbAddRow.Enabled = false;
            m_tsbAddRow.Image = global::DGD.HubGovernor.Properties.Resources.new_row_16;
            m_tsbAddRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbAddRow.Name = "m_tsbAddRow";
            m_tsbAddRow.Size = new System.Drawing.Size(23, 22);
            m_tsbAddRow.Text = "Nouvelle donnée...";
            // 
            // m_tsbDeleteRow
            // 
            this.m_tsbDeleteRow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbDeleteRow.Enabled = false;
            this.m_tsbDeleteRow.Image = global::DGD.HubGovernor.Properties.Resources.delete_16;
            this.m_tsbDeleteRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbDeleteRow.Name = "m_tsbDeleteRow";
            this.m_tsbDeleteRow.Size = new System.Drawing.Size(23, 22);
            this.m_tsbDeleteRow.Text = "Supprimer les données sélectionnées...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbImportData
            // 
            m_tsbImportData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbImportData.Image = global::DGD.HubGovernor.Properties.Resources.import_16;
            m_tsbImportData.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbImportData.Name = "m_tsbImportData";
            m_tsbImportData.Size = new System.Drawing.Size(23, 22);
            m_tsbImportData.Text = "Importer...";
            m_tsbImportData.Click += new System.EventHandler(this.ImportData_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbAdjustColumns
            // 
            m_tsbAdjustColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbAdjustColumns.Image = global::DGD.HubGovernor.Properties.Resources.auto_size_columns_16;
            m_tsbAdjustColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbAdjustColumns.Name = "m_tsbAdjustColumns";
            m_tsbAdjustColumns.Size = new System.Drawing.Size(23, 22);
            m_tsbAdjustColumns.Text = "Ajuster les colonnes";
            // 
            // m_tsbOptions
            // 
            m_tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbOptions.Enabled = false;
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
            m_tsbHelp.Text = "toolStripButton5";
            // 
            // m_tscbSessions
            // 
            m_tscbSessions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            m_tscbSessions.IntegralHeight = false;
            m_tscbSessions.Name = "m_tscbSessions";
            m_tscbSessions.Size = new System.Drawing.Size(121, 25);
            m_tscbSessions.Sorted = true;
            // 
            // m_statuStrip
            // 
            m_statuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_slRowCount});
            m_statuStrip.Location = new System.Drawing.Point(0, 301);
            m_statuStrip.Name = "m_statuStrip";
            m_statuStrip.Size = new System.Drawing.Size(798, 22);
            m_statuStrip.TabIndex = 2;
            // 
            // m_slRowCount
            // 
            this.m_slRowCount.BackColor = System.Drawing.Color.Transparent;
            this.m_slRowCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_slRowCount.Name = "m_slRowCount";
            this.m_slRowCount.Size = new System.Drawing.Size(112, 17);
            this.m_slRowCount.Text = "0 enregistrement(s )";
            this.m_slRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_colSession
            // 
            m_colSession.Tag = ColumnDataType_t.Integer;
            m_colSession.Text = "N° Session";
            m_colSession.Width = 86;
            // 
            // m_colSubHeading
            // 
            m_colSubHeading.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colSubHeading.Text = "SPT10";
            // 
            // m_colLabel
            // 
            m_colLabel.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colLabel.Text = "Libellé";
            // 
            // m_colTRLabel
            // 
            m_colTRLabel.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colTRLabel.Text = "Libellé TR";
            // 
            // m_colPrice
            // 
            m_colPrice.Tag = easyLib.DB.ColumnDataType_t.Float;
            m_colPrice.Text = "Prix";
            // 
            // m_colCurrency
            // 
            m_colCurrency.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colCurrency.Text = "Monnaie";
            // 
            // m_colIncoterm
            // 
            m_colIncoterm.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colIncoterm.Text = "Incoterm";
            // 
            // m_colPlace
            // 
            m_colPlace.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colPlace.Text = "Lieu";
            // 
            // m_colTime
            // 
            m_colTime.Tag = easyLib.DB.ColumnDataType_t.Time;
            m_colTime.Text = "Date";
            // 
            // m_colUnit
            // 
            m_colUnit.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colUnit.Text = "Unité";
            // 
            // m_colOrigin
            // 
            m_colOrigin.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colOrigin.Text = "Origine";
            // 
            // m_colPruductNber
            // 
            m_colPruductNber.Text = "N° Produit";
            // 
            // m_lvData
            // 
            this.m_lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colSession,
            m_colSubHeading,
            m_colLabel,
            m_colTRLabel,
            m_colPrice,
            m_colCurrency,
            m_colIncoterm,
            m_colPlace,
            m_colTime,
            m_colUnit,
            m_colOrigin,
            m_colPruductNber});
            this.m_lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvData.FullRowSelect = true;
            this.m_lvData.GridLines = true;
            this.m_lvData.HideSelection = false;
            this.m_lvData.Location = new System.Drawing.Point(0, 25);
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.Size = new System.Drawing.Size(798, 276);
            this.m_lvData.TabIndex = 3;
            this.m_lvData.UseCompatibleStateImageBehavior = false;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            this.m_lvData.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.View_ColumnClick);
            // 
            // TRSpotViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(798, 323);
            this.Controls.Add(this.m_lvData);
            this.Controls.Add(m_statuStrip);
            this.Controls.Add(m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TRSpotViewer";
            this.Text = "Valeurs Spots (TR)";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            m_statuStrip.ResumeLayout(false);
            m_statuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton m_tsbDeleteRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ListView m_lvData;
        private System.Windows.Forms.ToolStripStatusLabel m_slRowCount;
    }
}