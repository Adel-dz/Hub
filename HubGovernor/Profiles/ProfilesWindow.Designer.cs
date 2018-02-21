namespace DGD.HubGovernor.Profiles
{
    partial class ProfilesWindow
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
            System.Windows.Forms.ToolStripButton m_tsbNewProfile;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
            System.Windows.Forms.ToolStripButton m_tsbAdjustColumns;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.ColumnHeader m_colID;
            System.Windows.Forms.ColumnHeader m_colProfileName;
            System.Windows.Forms.ColumnHeader m_colProfilePrivilege;
            System.Windows.Forms.ColumnHeader m_colProfileStatus;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfilesWindow));
            this.m_tsbDeleteProfile = new System.Windows.Forms.ToolStripButton();
            this.m_tsbAutoManagement = new System.Windows.Forms.ToolStripButton();
            this.m_tsbClients = new System.Windows.Forms.ToolStripButton();
            this.m_colMgmntMode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_lvData = new System.Windows.Forms.ListView();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            m_tsbNewProfile = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbAdjustColumns = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colProfileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colProfilePrivilege = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colProfileStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbNewProfile,
            this.m_tsbDeleteProfile,
            toolStripSeparator3,
            this.m_tsbAutoManagement,
            toolStripSeparator1,
            this.m_tsbClients,
            toolStripSeparator4,
            m_tsbAdjustColumns,
            toolStripSeparator2,
            m_tsbHelp});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(629, 25);
            m_toolStrip.TabIndex = 0;
            // 
            // m_tsbNewProfile
            // 
            m_tsbNewProfile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbNewProfile.Image = global::DGD.HubGovernor.Properties.Resources.new_row_16;
            m_tsbNewProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbNewProfile.Name = "m_tsbNewProfile";
            m_tsbNewProfile.Size = new System.Drawing.Size(23, 22);
            m_tsbNewProfile.Text = "Nouveau profile";
            m_tsbNewProfile.Click += new System.EventHandler(this.NewProfile_Click);
            // 
            // m_tsbDeleteProfile
            // 
            this.m_tsbDeleteProfile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbDeleteProfile.Enabled = false;
            this.m_tsbDeleteProfile.Image = global::DGD.HubGovernor.Properties.Resources.delete_16;
            this.m_tsbDeleteProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbDeleteProfile.Name = "m_tsbDeleteProfile";
            this.m_tsbDeleteProfile.Size = new System.Drawing.Size(23, 22);
            this.m_tsbDeleteProfile.Text = "Supprimer le(s) profile(s) sélectionné(s)";
            this.m_tsbDeleteProfile.Click += new System.EventHandler(this.DeleteProfile_Click);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbAutoManagement
            // 
            this.m_tsbAutoManagement.Checked = true;
            this.m_tsbAutoManagement.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_tsbAutoManagement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbAutoManagement.Enabled = false;
            this.m_tsbAutoManagement.Image = global::DGD.HubGovernor.Properties.Resources.auto_mgmnt_16;
            this.m_tsbAutoManagement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbAutoManagement.Name = "m_tsbAutoManagement";
            this.m_tsbAutoManagement.Size = new System.Drawing.Size(23, 22);
            this.m_tsbAutoManagement.Text = "Gestion automatique / manuelle des profils";
            this.m_tsbAutoManagement.Click += new System.EventHandler(this.AutoManagement_Click);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbClients
            // 
            this.m_tsbClients.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbClients.Enabled = false;
            this.m_tsbClients.Image = global::DGD.HubGovernor.Properties.Resources.profile_clients_16;
            this.m_tsbClients.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbClients.Name = "m_tsbClients";
            this.m_tsbClients.Size = new System.Drawing.Size(23, 22);
            this.m_tsbClients.Text = "Liste des clients";
            this.m_tsbClients.Click += new System.EventHandler(this.Clients_Click);
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbAdjustColumns
            // 
            m_tsbAdjustColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbAdjustColumns.Image = global::DGD.HubGovernor.Properties.Resources.auto_size_columns_16;
            m_tsbAdjustColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbAdjustColumns.Name = "m_tsbAdjustColumns";
            m_tsbAdjustColumns.Size = new System.Drawing.Size(23, 22);
            m_tsbAdjustColumns.Text = "Ajuster les colonnes";
            m_tsbAdjustColumns.Click += new System.EventHandler(this.AdjustColumns_Click);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // m_colID
            // 
            m_colID.Tag = easyLib.DB.ColumnDataType_t.Integer;
            m_colID.Text = "ID";
            // 
            // m_colProfileName
            // 
            m_colProfileName.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colProfileName.Text = "Profile";
            m_colProfileName.Width = 126;
            // 
            // m_colProfilePrivilege
            // 
            m_colProfilePrivilege.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colProfilePrivilege.Text = "Privilège";
            m_colProfilePrivilege.Width = 145;
            // 
            // m_colProfileStatus
            // 
            m_colProfileStatus.Tag = easyLib.DB.ColumnDataType_t.Text;
            m_colProfileStatus.Text = "Status";
            m_colProfileStatus.Width = 136;
            // 
            // m_colMgmntMode
            // 
            this.m_colMgmntMode.Tag = easyLib.DB.ColumnDataType_t.Text;
            this.m_colMgmntMode.Text = "Mode de gestion";
            this.m_colMgmntMode.Width = 153;
            // 
            // m_lvData
            // 
            this.m_lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colID,
            m_colProfileName,
            m_colProfilePrivilege,
            m_colProfileStatus,
            this.m_colMgmntMode});
            this.m_lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvData.FullRowSelect = true;
            this.m_lvData.GridLines = true;
            this.m_lvData.Location = new System.Drawing.Point(0, 25);
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.Size = new System.Drawing.Size(629, 371);
            this.m_lvData.TabIndex = 1;
            this.m_lvData.UseCompatibleStateImageBehavior = false;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            this.m_lvData.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.Data_ColumnClick);
            this.m_lvData.ItemActivate += new System.EventHandler(this.Data_ItemActivate);
            this.m_lvData.SelectedIndexChanged += new System.EventHandler(this.Data_SelectedIndexChanged);
            // 
            // ProfilesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 396);
            this.Controls.Add(this.m_lvData);
            this.Controls.Add(m_toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProfilesWindow";
            this.Text = "Gestionnaire des profiles";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbDeleteProfile;
        private System.Windows.Forms.ListView m_lvData;
        private System.Windows.Forms.ToolStripButton m_tsbAutoManagement;
        private System.Windows.Forms.ColumnHeader m_colMgmntMode;
        private System.Windows.Forms.ToolStripButton m_tsbClients;
    }
}