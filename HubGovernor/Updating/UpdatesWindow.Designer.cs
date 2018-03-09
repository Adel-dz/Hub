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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripButton m_tsbDataUpdateOptions;
            System.Windows.Forms.ToolStripButton m_tsbDataUpdateHelp;
            System.Windows.Forms.ColumnHeader m_colDataGeneration;
            System.Windows.Forms.ColumnHeader m_colCreationTime;
            System.Windows.Forms.ColumnHeader m_colDeployTime;
            System.Windows.Forms.ColumnHeader m_colIncID;
            System.Windows.Forms.StatusStrip m_statusStrip;
            System.Windows.Forms.TabControl m_tabControl;
            System.Windows.Forms.TabPage m_datapPage;
            System.Windows.Forms.TabPage m_hubPage;
            System.Windows.Forms.ColumnHeader m_colID;
            System.Windows.Forms.ColumnHeader m_colVer;
            System.Windows.Forms.ColumnHeader m_colSystem;
            System.Windows.Forms.ColumnHeader m_colCreationDate;
            System.Windows.Forms.ColumnHeader m_colPublishDate;
            System.Windows.Forms.ToolStrip m_toolStripHub;
            System.Windows.Forms.ToolStripButton m_tsbAddApp;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripButton m_tsbAppUpdateOption;
            System.Windows.Forms.ToolStripButton m_tsbAppUpdateHelp;
            System.Windows.Forms.ImageList m_ilSmall;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdatesWindow));
            this.m_tsbBuildUpdate = new System.Windows.Forms.ToolStripButton();
            this.m_tsbUploadDataUpdates = new System.Windows.Forms.ToolStripButton();
            this.m_sslUpdateKey = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lvDataUpdates = new System.Windows.Forms.ListView();
            this.m_lvAppUpdates = new System.Windows.Forms.ListView();
            this.m_tsbUploadAppUpdates = new System.Windows.Forms.ToolStripButton();
            m_toolStripData = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbDataUpdateOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbDataUpdateHelp = new System.Windows.Forms.ToolStripButton();
            m_colDataGeneration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colCreationTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colDeployTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colIncID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_statusStrip = new System.Windows.Forms.StatusStrip();
            m_tabControl = new System.Windows.Forms.TabControl();
            m_datapPage = new System.Windows.Forms.TabPage();
            m_hubPage = new System.Windows.Forms.TabPage();
            m_colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colVer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colSystem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colCreationDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colPublishDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_toolStripHub = new System.Windows.Forms.ToolStrip();
            m_tsbAddApp = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbAppUpdateOption = new System.Windows.Forms.ToolStripButton();
            m_tsbAppUpdateHelp = new System.Windows.Forms.ToolStripButton();
            m_ilSmall = new System.Windows.Forms.ImageList(this.components);
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
            this.m_tsbUploadDataUpdates,
            toolStripSeparator1,
            m_tsbDataUpdateOptions,
            m_tsbDataUpdateHelp});
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
            this.m_tsbBuildUpdate.Click += new System.EventHandler(this.BuildDataUpdate_Click);
            // 
            // m_tsbUploadDataUpdates
            // 
            this.m_tsbUploadDataUpdates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbUploadDataUpdates.Image = global::DGD.HubGovernor.Properties.Resources.upload_16;
            this.m_tsbUploadDataUpdates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbUploadDataUpdates.Name = "m_tsbUploadDataUpdates";
            this.m_tsbUploadDataUpdates.Size = new System.Drawing.Size(23, 22);
            this.m_tsbUploadDataUpdates.Text = "Publier la MAJ";
            this.m_tsbUploadDataUpdates.Click += new System.EventHandler(this.UploadDataUpdates_Click);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbDataUpdateOptions
            // 
            m_tsbDataUpdateOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbDataUpdateOptions.Image = global::DGD.HubGovernor.Properties.Resources.option_16;
            m_tsbDataUpdateOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbDataUpdateOptions.Name = "m_tsbDataUpdateOptions";
            m_tsbDataUpdateOptions.Size = new System.Drawing.Size(23, 22);
            m_tsbDataUpdateOptions.Text = "Options";
            // 
            // m_tsbDataUpdateHelp
            // 
            m_tsbDataUpdateHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            m_tsbDataUpdateHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbDataUpdateHelp.Image = global::DGD.HubGovernor.Properties.Resources.help_16;
            m_tsbDataUpdateHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbDataUpdateHelp.Name = "m_tsbDataUpdateHelp";
            m_tsbDataUpdateHelp.Size = new System.Drawing.Size(23, 22);
            m_tsbDataUpdateHelp.Text = "Aide";
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
            m_datapPage.Controls.Add(this.m_lvDataUpdates);
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
            // m_lvDataUpdates
            // 
            this.m_lvDataUpdates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colIncID,
            m_colDataGeneration,
            m_colCreationTime,
            m_colDeployTime});
            this.m_lvDataUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvDataUpdates.FullRowSelect = true;
            this.m_lvDataUpdates.HideSelection = false;
            this.m_lvDataUpdates.Location = new System.Drawing.Point(3, 28);
            this.m_lvDataUpdates.MultiSelect = false;
            this.m_lvDataUpdates.Name = "m_lvDataUpdates";
            this.m_lvDataUpdates.Size = new System.Drawing.Size(659, 226);
            this.m_lvDataUpdates.TabIndex = 1;
            this.m_lvDataUpdates.UseCompatibleStateImageBehavior = false;
            this.m_lvDataUpdates.View = System.Windows.Forms.View.Details;
            this.m_lvDataUpdates.ItemActivate += new System.EventHandler(this.Updates_DataItemActivate);
            // 
            // m_hubPage
            // 
            m_hubPage.Controls.Add(this.m_lvAppUpdates);
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
            // m_lvAppUpdates
            // 
            this.m_lvAppUpdates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colID,
            m_colVer,
            m_colSystem,
            m_colCreationDate,
            m_colPublishDate});
            this.m_lvAppUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvAppUpdates.FullRowSelect = true;
            this.m_lvAppUpdates.HideSelection = false;
            this.m_lvAppUpdates.Location = new System.Drawing.Point(3, 28);
            this.m_lvAppUpdates.Name = "m_lvAppUpdates";
            this.m_lvAppUpdates.Size = new System.Drawing.Size(659, 226);
            this.m_lvAppUpdates.TabIndex = 1;
            this.m_lvAppUpdates.UseCompatibleStateImageBehavior = false;
            this.m_lvAppUpdates.View = System.Windows.Forms.View.Details;
            // 
            // m_colID
            // 
            m_colID.Text = "ID";
            // 
            // m_colVer
            // 
            m_colVer.Text = "Version";
            m_colVer.Width = 91;
            // 
            // m_colSystem
            // 
            m_colSystem.Text = "Système requis";
            m_colSystem.Width = 152;
            // 
            // m_colCreationDate
            // 
            m_colCreationDate.Text = "Crée le";
            m_colCreationDate.Width = 159;
            // 
            // m_colPublishDate
            // 
            m_colPublishDate.Text = "Publié le";
            m_colPublishDate.Width = 188;
            // 
            // m_toolStripHub
            // 
            m_toolStripHub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbAddApp,
            this.m_tsbUploadAppUpdates,
            toolStripSeparator2,
            m_tsbAppUpdateOption,
            m_tsbAppUpdateHelp});
            m_toolStripHub.Location = new System.Drawing.Point(3, 3);
            m_toolStripHub.Name = "m_toolStripHub";
            m_toolStripHub.Size = new System.Drawing.Size(659, 25);
            m_toolStripHub.TabIndex = 0;
            // 
            // m_tsbAddApp
            // 
            m_tsbAddApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbAddApp.Image = global::DGD.HubGovernor.Properties.Resources.add_app_16;
            m_tsbAddApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbAddApp.Name = "m_tsbAddApp";
            m_tsbAddApp.Size = new System.Drawing.Size(23, 22);
            m_tsbAddApp.Text = "Ajouter une mise à jour...";
            m_tsbAddApp.Click += new System.EventHandler(this.AddPackage_Click);
            // 
            // m_tsbUploadAppUpdates
            // 
            this.m_tsbUploadAppUpdates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbUploadAppUpdates.Image = global::DGD.HubGovernor.Properties.Resources.upload_16;
            this.m_tsbUploadAppUpdates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbUploadAppUpdates.Name = "m_tsbUploadAppUpdates";
            this.m_tsbUploadAppUpdates.Size = new System.Drawing.Size(23, 22);
            this.m_tsbUploadAppUpdates.Text = "Publier";
            this.m_tsbUploadAppUpdates.Click += new System.EventHandler(this.UploadAppUpdates_Click);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbAppUpdateOption
            // 
            m_tsbAppUpdateOption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbAppUpdateOption.Image = global::DGD.HubGovernor.Properties.Resources.option_16;
            m_tsbAppUpdateOption.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbAppUpdateOption.Name = "m_tsbAppUpdateOption";
            m_tsbAppUpdateOption.Size = new System.Drawing.Size(23, 22);
            m_tsbAppUpdateOption.Text = "Paramètres";
            // 
            // m_tsbAppUpdateHelp
            // 
            m_tsbAppUpdateHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            m_tsbAppUpdateHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbAppUpdateHelp.Image = global::DGD.HubGovernor.Properties.Resources.help_16;
            m_tsbAppUpdateHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbAppUpdateHelp.Name = "m_tsbAppUpdateHelp";
            m_tsbAppUpdateHelp.Size = new System.Drawing.Size(23, 22);
            m_tsbAppUpdateHelp.Text = "Aide";
            // 
            // m_ilSmall
            // 
            m_ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ilSmall.ImageStream")));
            m_ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            m_ilSmall.Images.SetKeyName(0, "hub_app_16.png");
            m_ilSmall.Images.SetKeyName(1, "database_16.png");
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

        private System.Windows.Forms.ToolStripButton m_tsbUploadDataUpdates;
        private System.Windows.Forms.ListView m_lvDataUpdates;
        private System.Windows.Forms.ToolStripButton m_tsbBuildUpdate;
        private System.Windows.Forms.ToolStripStatusLabel m_sslUpdateKey;
        private System.Windows.Forms.ToolStripButton m_tsbUploadAppUpdates;
        private System.Windows.Forms.ListView m_lvAppUpdates;
    }
}