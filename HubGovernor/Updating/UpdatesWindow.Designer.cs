namespace DGD.HubGovernor.Updating
{
    partial class UpdatesWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdatesWindow));
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripButton m_tsbOptions;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.ColumnHeader m_colDataGeneration;
            System.Windows.Forms.ColumnHeader m_colCreationTime;
            System.Windows.Forms.ColumnHeader m_colDeployTime;
            System.Windows.Forms.ColumnHeader m_colIncID;
            this.m_tsbBuildUpdate = new System.Windows.Forms.ToolStripButton();
            this.m_tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.m_lvUpdates = new System.Windows.Forms.ListView();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_colDataGeneration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colCreationTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colDeployTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colIncID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbBuildUpdate,
            this.m_tsbUpload,
            toolStripSeparator1,
            m_tsbOptions,
            m_tsbHelp});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(673, 25);
            m_toolStrip.TabIndex = 0;
            // 
            // m_tsbBuildUpdate
            // 
            this.m_tsbBuildUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbBuildUpdate.Enabled = false;
            this.m_tsbBuildUpdate.Image = global::DGD.HubGovernor.Properties.Resources.build_update_16;
            this.m_tsbBuildUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbBuildUpdate.Name = "m_tsbBuildUpdate";
            this.m_tsbBuildUpdate.Size = new System.Drawing.Size(23, 22);
            this.m_tsbBuildUpdate.Text = "Construire la MAJ";
            this.m_tsbBuildUpdate.Click += new System.EventHandler(this.BuildUpdate_Click);
            // 
            // m_tsbUpload
            // 
            this.m_tsbUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbUpload.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbUpload.Image")));
            this.m_tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbUpload.Name = "m_tsbUpload";
            this.m_tsbUpload.Size = new System.Drawing.Size(23, 22);
            this.m_tsbUpload.Text = "Publier la MAJ";
            this.m_tsbUpload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbOptions
            // 
            m_tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbOptions.Image = global::DGD.HubGovernor.Properties.Resources.option_16;
            m_tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbOptions.Name = "m_tsbOptions";
            m_tsbOptions.Size = new System.Drawing.Size(23, 22);
            m_tsbOptions.Text = "Options";
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
            // m_colDataGeneration
            // 
            m_colDataGeneration.Text = "Version des données";
            m_colDataGeneration.Width = 144;
            // 
            // m_colCreationTime
            // 
            m_colCreationTime.Text = "Créer le";
            m_colCreationTime.Width = 156;
            // 
            // m_colDeployTime
            // 
            m_colDeployTime.Text = "Publier le";
            m_colDeployTime.Width = 228;
            // 
            // m_colIncID
            // 
            m_colIncID.Text = "ID";
            // 
            // m_lvUpdates
            // 
            this.m_lvUpdates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colIncID,
            m_colDataGeneration,
            m_colCreationTime,
            m_colDeployTime});
            this.m_lvUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvUpdates.FullRowSelect = true;
            this.m_lvUpdates.Location = new System.Drawing.Point(0, 25);
            this.m_lvUpdates.MultiSelect = false;
            this.m_lvUpdates.Name = "m_lvUpdates";
            this.m_lvUpdates.Size = new System.Drawing.Size(673, 284);
            this.m_lvUpdates.TabIndex = 1;
            this.m_lvUpdates.UseCompatibleStateImageBehavior = false;
            this.m_lvUpdates.View = System.Windows.Forms.View.Details;
            this.m_lvUpdates.ItemActivate += new System.EventHandler(this.Updates_ItemActivate);
            // 
            // UpdatesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(673, 309);
            this.Controls.Add(this.m_lvUpdates);
            this.Controls.Add(m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdatesWindow";
            this.Text = "Gestionnaire des MAJ";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbUpload;
        private System.Windows.Forms.ListView m_lvUpdates;
        private System.Windows.Forms.ToolStripButton m_tsbBuildUpdate;
    }
}