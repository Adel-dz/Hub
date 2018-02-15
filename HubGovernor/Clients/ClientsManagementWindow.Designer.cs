namespace DGD.HubGovernor.Clients
{
    partial class ClientsManagementWindow
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
            System.Windows.Forms.SplitContainer m_leftSplitter;
            System.Windows.Forms.ColumnHeader m_colActiveClient;
            System.Windows.Forms.SplitContainer m_rightSplitter;
            System.Windows.Forms.ColumnHeader m_colTime;
            System.Windows.Forms.ColumnHeader m_colActivity;
            System.Windows.Forms.ColumnHeader m_colActivityData;
            System.Windows.Forms.Panel m_infoPanel;
            System.Windows.Forms.ToolStrip m_toolstrip;
            System.Windows.Forms.ToolStripButton m_tsbProfiles;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsManagementWindow));
            this.m_lvActiveClient = new System.Windows.Forms.ListView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.m_lblClientInfo = new System.Windows.Forms.Label();
            this.m_tsbClients = new System.Windows.Forms.ToolStripButton();
            m_leftSplitter = new System.Windows.Forms.SplitContainer();
            m_colActiveClient = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_rightSplitter = new System.Windows.Forms.SplitContainer();
            m_colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colActivity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colActivityData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_infoPanel = new System.Windows.Forms.Panel();
            m_toolstrip = new System.Windows.Forms.ToolStrip();
            m_tsbProfiles = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(m_leftSplitter)).BeginInit();
            m_leftSplitter.Panel1.SuspendLayout();
            m_leftSplitter.Panel2.SuspendLayout();
            m_leftSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_rightSplitter)).BeginInit();
            m_rightSplitter.Panel1.SuspendLayout();
            m_rightSplitter.SuspendLayout();
            m_infoPanel.SuspendLayout();
            m_toolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_leftSplitter
            // 
            m_leftSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            m_leftSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            m_leftSplitter.Location = new System.Drawing.Point(0, 25);
            m_leftSplitter.Name = "m_leftSplitter";
            // 
            // m_leftSplitter.Panel1
            // 
            m_leftSplitter.Panel1.Controls.Add(this.m_lvActiveClient);
            // 
            // m_leftSplitter.Panel2
            // 
            m_leftSplitter.Panel2.Controls.Add(m_rightSplitter);
            m_leftSplitter.Size = new System.Drawing.Size(972, 417);
            m_leftSplitter.SplitterDistance = 248;
            m_leftSplitter.TabIndex = 0;
            // 
            // m_lvActiveClient
            // 
            this.m_lvActiveClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colActiveClient});
            this.m_lvActiveClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvActiveClient.Location = new System.Drawing.Point(0, 0);
            this.m_lvActiveClient.Name = "m_lvActiveClient";
            this.m_lvActiveClient.Size = new System.Drawing.Size(248, 417);
            this.m_lvActiveClient.TabIndex = 0;
            this.m_lvActiveClient.UseCompatibleStateImageBehavior = false;
            this.m_lvActiveClient.View = System.Windows.Forms.View.Details;
            // 
            // m_colActiveClient
            // 
            m_colActiveClient.Text = "Actifs Clients";
            m_colActiveClient.Width = 232;
            // 
            // m_rightSplitter
            // 
            m_rightSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            m_rightSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            m_rightSplitter.Location = new System.Drawing.Point(0, 0);
            m_rightSplitter.Name = "m_rightSplitter";
            // 
            // m_rightSplitter.Panel1
            // 
            m_rightSplitter.Panel1.Controls.Add(this.listView1);
            m_rightSplitter.Panel1.Controls.Add(m_infoPanel);
            m_rightSplitter.Size = new System.Drawing.Size(720, 417);
            m_rightSplitter.SplitterDistance = 504;
            m_rightSplitter.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colTime,
            m_colActivity,
            m_colActivityData});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 100);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(504, 317);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // m_colTime
            // 
            m_colTime.Text = "Date";
            m_colTime.Width = 119;
            // 
            // m_colActivity
            // 
            m_colActivity.Text = "Activité";
            m_colActivity.Width = 109;
            // 
            // m_colActivityData
            // 
            m_colActivityData.Text = "Données";
            m_colActivityData.Width = 262;
            // 
            // m_infoPanel
            // 
            m_infoPanel.AutoScroll = true;
            m_infoPanel.Controls.Add(this.m_lblClientInfo);
            m_infoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            m_infoPanel.Location = new System.Drawing.Point(0, 0);
            m_infoPanel.Name = "m_infoPanel";
            m_infoPanel.Size = new System.Drawing.Size(504, 100);
            m_infoPanel.TabIndex = 1;
            // 
            // m_lblClientInfo
            // 
            this.m_lblClientInfo.AutoSize = true;
            this.m_lblClientInfo.Location = new System.Drawing.Point(4, 4);
            this.m_lblClientInfo.Name = "m_lblClientInfo";
            this.m_lblClientInfo.Size = new System.Drawing.Size(62, 13);
            this.m_lblClientInfo.TabIndex = 0;
            this.m_lblClientInfo.Text = "Infos. Client";
            // 
            // m_toolstrip
            // 
            m_toolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbProfiles,
            this.m_tsbClients});
            m_toolstrip.Location = new System.Drawing.Point(0, 0);
            m_toolstrip.Name = "m_toolstrip";
            m_toolstrip.Size = new System.Drawing.Size(972, 25);
            m_toolstrip.TabIndex = 1;
            // 
            // m_tsbProfiles
            // 
            m_tsbProfiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbProfiles.Image = global::DGD.HubGovernor.Properties.Resources.profiles_16;
            m_tsbProfiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbProfiles.Name = "m_tsbProfiles";
            m_tsbProfiles.Size = new System.Drawing.Size(23, 22);
            m_tsbProfiles.Text = "Profiles";
            m_tsbProfiles.Click += new System.EventHandler(this.Profiles_Click);
            // 
            // m_tsbClients
            // 
            this.m_tsbClients.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbClients.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbClients.Image")));
            this.m_tsbClients.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbClients.Name = "m_tsbClients";
            this.m_tsbClients.Size = new System.Drawing.Size(23, 22);
            this.m_tsbClients.Text = "Liste des clients";
            this.m_tsbClients.Click += new System.EventHandler(this.Clients_Click);
            // 
            // ClientsManagementWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 442);
            this.Controls.Add(m_leftSplitter);
            this.Controls.Add(m_toolstrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClientsManagementWindow";
            this.Text = "Gestionnaire des clients";
            m_leftSplitter.Panel1.ResumeLayout(false);
            m_leftSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_leftSplitter)).EndInit();
            m_leftSplitter.ResumeLayout(false);
            m_rightSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_rightSplitter)).EndInit();
            m_rightSplitter.ResumeLayout(false);
            m_infoPanel.ResumeLayout(false);
            m_infoPanel.PerformLayout();
            m_toolstrip.ResumeLayout(false);
            m_toolstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView m_lvActiveClient;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label m_lblClientInfo;
        private System.Windows.Forms.ToolStripButton m_tsbClients;
    }
}