namespace DGD.HubGovernor
{
    partial class DatumView
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
            System.Windows.Forms.ToolStripButton m_tsbTrackNew;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripButton m_tsbAutoSizeColumns;
            System.Windows.Forms.ToolStripButton m_tsbOptions;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.StatusStrip m_statusStrip;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatumView));
            this.m_tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.m_tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.m_tslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lvData = new System.Windows.Forms.ListView();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbTrackNew = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbAutoSizeColumns = new System.Windows.Forms.ToolStripButton();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_statusStrip = new System.Windows.Forms.StatusStrip();
            m_toolStrip.SuspendLayout();
            m_statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbAdd,
            this.m_tsbDelete,
            toolStripSeparator1,
            m_tsbTrackNew,
            toolStripSeparator2,
            m_tsbAutoSizeColumns,
            m_tsbOptions,
            m_tsbHelp});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(876, 25);
            m_toolStrip.TabIndex = 0;
            // 
            // m_tsbAdd
            // 
            this.m_tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbAdd.Image = global::DGD.HubGovernor.Properties.Resources.new_row_16;
            this.m_tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbAdd.Name = "m_tsbAdd";
            this.m_tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.m_tsbAdd.Text = "Ajouter...";
            this.m_tsbAdd.Click += new System.EventHandler(this.Add_Click);
            // 
            // m_tsbDelete
            // 
            this.m_tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbDelete.Image = global::DGD.HubGovernor.Properties.Resources.delete_16;
            this.m_tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbDelete.Name = "m_tsbDelete";
            this.m_tsbDelete.Size = new System.Drawing.Size(23, 22);
            this.m_tsbDelete.Text = "Supprimer";
            this.m_tsbDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbTrackNew
            // 
            m_tsbTrackNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbTrackNew.Image = global::DGD.HubGovernor.Properties.Resources.auto_scroll_16;
            m_tsbTrackNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbTrackNew.Name = "m_tsbTrackNew";
            m_tsbTrackNew.Size = new System.Drawing.Size(23, 22);
            m_tsbTrackNew.Text = "Monter les nouveaux éléments";
            m_tsbTrackNew.Click += new System.EventHandler(this.TrackNew_Click);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbAutoSizeColumns
            // 
            m_tsbAutoSizeColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbAutoSizeColumns.Image = global::DGD.HubGovernor.Properties.Resources.auto_size_columns_16;
            m_tsbAutoSizeColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbAutoSizeColumns.Name = "m_tsbAutoSizeColumns";
            m_tsbAutoSizeColumns.Size = new System.Drawing.Size(23, 22);
            m_tsbAutoSizeColumns.Text = "Ajuster la largeur des colonnes";
            m_tsbAutoSizeColumns.Click += new System.EventHandler(this.AutoSizeColumns_Click);
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
            // m_statusStrip
            // 
            m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tslblStatus});
            m_statusStrip.Location = new System.Drawing.Point(0, 375);
            m_statusStrip.Name = "m_statusStrip";
            m_statusStrip.Size = new System.Drawing.Size(876, 22);
            m_statusStrip.TabIndex = 2;
            // 
            // m_tslblStatus
            // 
            this.m_tslblStatus.Name = "m_tslblStatus";
            this.m_tslblStatus.Size = new System.Drawing.Size(861, 17);
            this.m_tslblStatus.Spring = true;
            this.m_tslblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lvData
            // 
            this.m_lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvData.FullRowSelect = true;
            this.m_lvData.GridLines = true;
            this.m_lvData.Location = new System.Drawing.Point(0, 25);
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.Size = new System.Drawing.Size(876, 350);
            this.m_lvData.TabIndex = 1;
            this.m_lvData.UseCompatibleStateImageBehavior = false;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            this.m_lvData.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.DataView_ColumnClick);
            this.m_lvData.ItemActivate += new System.EventHandler(this.DataView_ItemActivate);
            // 
            // DatumView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(876, 397);
            this.Controls.Add(this.m_lvData);
            this.Controls.Add(m_statusStrip);
            this.Controls.Add(m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DatumView";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            m_statusStrip.ResumeLayout(false);
            m_statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbAdd;
        private System.Windows.Forms.ToolStripButton m_tsbDelete;
        private System.Windows.Forms.ListView m_lvData;
        private System.Windows.Forms.ToolStripStatusLabel m_tslblStatus;
    }
}