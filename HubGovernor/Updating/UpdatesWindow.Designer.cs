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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStrip m_toolStripData;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdatesWindow));
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripButton m_tsbOptions;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.ColumnHeader m_colDataGeneration;
            System.Windows.Forms.ColumnHeader m_colCreationTime;
            System.Windows.Forms.ColumnHeader m_colDeployTime;
            System.Windows.Forms.ColumnHeader m_colIncID;
            System.Windows.Forms.StatusStrip m_statusStrip;
            System.Windows.Forms.TabControl m_tabControl;
            System.Windows.Forms.TabPage m_datapPage;
            System.Windows.Forms.TabPage m_hubPage;
            System.Windows.Forms.ImageList m_ilSmall;
            System.Windows.Forms.ToolStrip m_toolStripHub;
            this.m_tsbBuildUpdate = new System.Windows.Forms.ToolStripButton();
            this.m_tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.m_sslUpdateKey = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lvUpdates = new System.Windows.Forms.ListView();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            m_toolStripData = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_colDataGeneration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colCreationTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colDeployTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colIncID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_statusStrip = new System.Windows.Forms.StatusStrip();
            m_tabControl = new System.Windows.Forms.TabControl();
            m_datapPage = new System.Windows.Forms.TabPage();
            m_hubPage = new System.Windows.Forms.TabPage();
            m_ilSmall = new System.Windows.Forms.ImageList(this.components);
            m_toolStripHub = new System.Windows.Forms.ToolStrip();
            m_toolStripData.SuspendLayout();
            m_statusStrip.SuspendLayout();
            m_tabControl.SuspendLayout();
            m_datapPage.SuspendLayout();
            m_hubPage.SuspendLayout();
            m_toolStripHub.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolStripData
            // 
            m_toolStripData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbBuildUpdate,
            this.m_tsbUpload,
            toolStripSeparator1,
            m_tsbOptions,
            m_tsbHelp});
            m_toolStripData.Location = new System.Drawing.Point(3, 3);
            m_toolStripData.Name = "m_toolStripData";
            m_toolStripData.Size = new System.Drawing.Size(659, 25);
            m_toolStripData.TabIndex = 0;
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
            m_colDataGeneration.Text = "Version des données requise";
            m_colDataGeneration.Width = 167;
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
            // m_statusStrip
            // 
            m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_sslUpdateKey});
            m_statusStrip.Location = new System.Drawing.Point(0, 287);
            m_statusStrip.Name = "m_statusStrip";
            m_statusStrip.Size = new System.Drawing.Size(673, 22);
            m_statusStrip.TabIndex = 2;
            // 
            // m_sslUpdateKey
            // 
            this.m_sslUpdateKey.Name = "m_sslUpdateKey";
            this.m_sslUpdateKey.Size = new System.Drawing.Size(0, 17);
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
            this.m_lvUpdates.Location = new System.Drawing.Point(3, 28);
            this.m_lvUpdates.MultiSelect = false;
            this.m_lvUpdates.Name = "m_lvUpdates";
            this.m_lvUpdates.Size = new System.Drawing.Size(659, 226);
            this.m_lvUpdates.TabIndex = 1;
            this.m_lvUpdates.UseCompatibleStateImageBehavior = false;
            this.m_lvUpdates.View = System.Windows.Forms.View.Details;
            this.m_lvUpdates.ItemActivate += new System.EventHandler(this.Updates_ItemActivate);
            // 
            // m_tabControl
            // 
            m_tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            m_tabControl.Controls.Add(m_datapPage);
            m_tabControl.Controls.Add(m_hubPage);
            m_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            m_tabControl.ImageList = m_ilSmall;
            m_tabControl.Location = new System.Drawing.Point(0, 0);
            m_tabControl.Name = "m_tabControl";
            m_tabControl.SelectedIndex = 0;
            m_tabControl.Size = new System.Drawing.Size(673, 287);
            m_tabControl.TabIndex = 0;
            // 
            // m_datapPage
            // 
            m_datapPage.Controls.Add(this.m_lvUpdates);
            m_datapPage.Controls.Add(m_toolStripData);
            m_datapPage.ImageIndex = 1;
            m_datapPage.Location = new System.Drawing.Point(4, 26);
            m_datapPage.Name = "m_datapPage";
            m_datapPage.Padding = new System.Windows.Forms.Padding(3);
            m_datapPage.Size = new System.Drawing.Size(665, 257);
            m_datapPage.TabIndex = 0;
            m_datapPage.Text = "Données";
            m_datapPage.UseVisualStyleBackColor = true;
            // 
            // m_hubPage
            // 
            m_hubPage.Controls.Add(m_toolStripHub);
            m_hubPage.ImageIndex = 0;
            m_hubPage.Location = new System.Drawing.Point(4, 26);
            m_hubPage.Name = "m_hubPage";
            m_hubPage.Padding = new System.Windows.Forms.Padding(3);
            m_hubPage.Size = new System.Drawing.Size(665, 257);
            m_hubPage.TabIndex = 1;
            m_hubPage.Text = "Hub";
            m_hubPage.UseVisualStyleBackColor = true;
            // 
            // m_ilSmall
            // 
            m_ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ilSmall.ImageStream")));
            m_ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            m_ilSmall.Images.SetKeyName(0, "hub_app_16.png");
            m_ilSmall.Images.SetKeyName(1, "database_16.png");
            // 
            // m_toolStripHub
            // 
            m_toolStripHub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            m_toolStripHub.Location = new System.Drawing.Point(3, 3);
            m_toolStripHub.Name = "m_toolStripHub";
            m_toolStripHub.Size = new System.Drawing.Size(659, 25);
            m_toolStripHub.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::DGD.HubGovernor.Properties.Resources.add_app_16;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // UpdatesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(673, 309);
            this.Controls.Add(m_tabControl);
            this.Controls.Add(m_statusStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdatesWindow";
            this.Text = "Gestionnaire des MAJ";
            m_toolStripData.ResumeLayout(false);
            m_toolStripData.PerformLayout();
            m_statusStrip.ResumeLayout(false);
            m_statusStrip.PerformLayout();
            m_tabControl.ResumeLayout(false);
            m_datapPage.ResumeLayout(false);
            m_datapPage.PerformLayout();
            m_hubPage.ResumeLayout(false);
            m_hubPage.PerformLayout();
            m_toolStripHub.ResumeLayout(false);
            m_toolStripHub.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbUpload;
        private System.Windows.Forms.ListView m_lvUpdates;
        private System.Windows.Forms.ToolStripButton m_tsbBuildUpdate;
        private System.Windows.Forms.ToolStripStatusLabel m_sslUpdateKey;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}