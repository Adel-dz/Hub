namespace DGD.HubGovernor.Clients
{
    partial class ClientsManagmentWindow
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
            System.Windows.Forms.SplitContainer m_leftSplitter;
            System.Windows.Forms.ColumnHeader m_colID;
            System.Windows.Forms.ImageList m_ilSmall;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsManagmentWindow));
            System.Windows.Forms.Label m_lblStatusLabel;
            System.Windows.Forms.Label l_lblProfileLabel;
            System.Windows.Forms.Label m_lblContactLabel;
            System.Windows.Forms.ToolStrip m_toolstrip;
            System.Windows.Forms.ToolStripButton m_tsbProfiles;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripLabel m_tslClientType;
            this.m_lvClients = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_lblStatus = new System.Windows.Forms.Label();
            this.m_lblProfil = new System.Windows.Forms.Label();
            this.m_lblContact = new System.Windows.Forms.Label();
            this.m_tsbClients = new System.Windows.Forms.ToolStripButton();
            this.m_tscbClientsType = new System.Windows.Forms.ToolStripComboBox();
            m_leftSplitter = new System.Windows.Forms.SplitContainer();
            m_colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_ilSmall = new System.Windows.Forms.ImageList(this.components);
            m_lblStatusLabel = new System.Windows.Forms.Label();
            l_lblProfileLabel = new System.Windows.Forms.Label();
            m_lblContactLabel = new System.Windows.Forms.Label();
            m_toolstrip = new System.Windows.Forms.ToolStrip();
            m_tsbProfiles = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            m_tslClientType = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(m_leftSplitter)).BeginInit();
            m_leftSplitter.Panel1.SuspendLayout();
            m_leftSplitter.SuspendLayout();
            this.panel1.SuspendLayout();
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
            m_leftSplitter.Panel1.Controls.Add(this.m_lvClients);
            m_leftSplitter.Panel1.Controls.Add(this.panel1);
            m_leftSplitter.Size = new System.Drawing.Size(682, 350);
            m_leftSplitter.SplitterDistance = 153;
            m_leftSplitter.TabIndex = 0;
            // 
            // m_lvClients
            // 
            this.m_lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colID});
            this.m_lvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvClients.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lvClients.Location = new System.Drawing.Point(0, 0);
            this.m_lvClients.MultiSelect = false;
            this.m_lvClients.Name = "m_lvClients";
            this.m_lvClients.Size = new System.Drawing.Size(153, 284);
            this.m_lvClients.SmallImageList = m_ilSmall;
            this.m_lvClients.TabIndex = 0;
            this.m_lvClients.UseCompatibleStateImageBehavior = false;
            this.m_lvClients.View = System.Windows.Forms.View.Details;
            this.m_lvClients.SelectedIndexChanged += new System.EventHandler(this.Clients_SelectedIndexChanged);
            // 
            // m_colID
            // 
            m_colID.Text = "ID";
            m_colID.Width = 110;
            // 
            // m_ilSmall
            // 
            m_ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ilSmall.ImageStream")));
            m_ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            m_ilSmall.Images.SetKeyName(0, "led-green_16.png");
            m_ilSmall.Images.SetKeyName(1, "white-led_16.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_lblStatus);
            this.panel1.Controls.Add(this.m_lblProfil);
            this.panel1.Controls.Add(this.m_lblContact);
            this.panel1.Controls.Add(m_lblStatusLabel);
            this.panel1.Controls.Add(l_lblProfileLabel);
            this.panel1.Controls.Add(m_lblContactLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 284);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 66);
            this.panel1.TabIndex = 0;
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.AutoSize = true;
            this.m_lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblStatus.Location = new System.Drawing.Point(58, 49);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(11, 13);
            this.m_lblStatus.TabIndex = 5;
            this.m_lblStatus.Text = "-";
            // 
            // m_lblProfil
            // 
            this.m_lblProfil.AutoSize = true;
            this.m_lblProfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblProfil.Location = new System.Drawing.Point(58, 25);
            this.m_lblProfil.Name = "m_lblProfil";
            this.m_lblProfil.Size = new System.Drawing.Size(11, 13);
            this.m_lblProfil.TabIndex = 4;
            this.m_lblProfil.Text = "-";
            // 
            // m_lblContact
            // 
            this.m_lblContact.AutoSize = true;
            this.m_lblContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblContact.Location = new System.Drawing.Point(58, 3);
            this.m_lblContact.Name = "m_lblContact";
            this.m_lblContact.Size = new System.Drawing.Size(11, 13);
            this.m_lblContact.TabIndex = 3;
            this.m_lblContact.Text = "-";
            // 
            // m_lblStatusLabel
            // 
            m_lblStatusLabel.AutoSize = true;
            m_lblStatusLabel.Location = new System.Drawing.Point(12, 49);
            m_lblStatusLabel.Name = "m_lblStatusLabel";
            m_lblStatusLabel.Size = new System.Drawing.Size(38, 13);
            m_lblStatusLabel.TabIndex = 2;
            m_lblStatusLabel.Text = "Statut:";
            // 
            // l_lblProfileLabel
            // 
            l_lblProfileLabel.AutoSize = true;
            l_lblProfileLabel.Location = new System.Drawing.Point(12, 25);
            l_lblProfileLabel.Name = "l_lblProfileLabel";
            l_lblProfileLabel.Size = new System.Drawing.Size(33, 13);
            l_lblProfileLabel.TabIndex = 1;
            l_lblProfileLabel.Text = "Profil:";
            // 
            // m_lblContactLabel
            // 
            m_lblContactLabel.AutoSize = true;
            m_lblContactLabel.Location = new System.Drawing.Point(12, 3);
            m_lblContactLabel.Name = "m_lblContactLabel";
            m_lblContactLabel.Size = new System.Drawing.Size(47, 13);
            m_lblContactLabel.TabIndex = 0;
            m_lblContactLabel.Text = "Contact:";
            // 
            // m_toolstrip
            // 
            m_toolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbProfiles,
            this.m_tsbClients,
            toolStripSeparator1,
            m_tslClientType,
            this.m_tscbClientsType});
            m_toolstrip.Location = new System.Drawing.Point(0, 0);
            m_toolstrip.Name = "m_toolstrip";
            m_toolstrip.Size = new System.Drawing.Size(682, 25);
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
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tslClientType
            // 
            m_tslClientType.Name = "m_tslClientType";
            m_tslClientType.Size = new System.Drawing.Size(106, 22);
            m_tslClientType.Text = "Afficher les clients ";
            // 
            // m_tscbClientsType
            // 
            this.m_tscbClientsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_tscbClientsType.Items.AddRange(new object[] {
            "En cours d’exécution",
            "Autorisés",
            "Tous"});
            this.m_tscbClientsType.Name = "m_tscbClientsType";
            this.m_tscbClientsType.Size = new System.Drawing.Size(160, 25);
            this.m_tscbClientsType.SelectedIndexChanged += new System.EventHandler(this.ClientsType_SelectedIndexChanged);
            // 
            // ClientsManagmentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 375);
            this.Controls.Add(m_leftSplitter);
            this.Controls.Add(m_toolstrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClientsManagmentWindow";
            this.Text = "Gestionnaire des clients";
            m_leftSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_leftSplitter)).EndInit();
            m_leftSplitter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            m_toolstrip.ResumeLayout(false);
            m_toolstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView m_lvClients;
        private System.Windows.Forms.ToolStripButton m_tsbClients;
        private System.Windows.Forms.ToolStripComboBox m_tscbClientsType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label m_lblStatus;
        private System.Windows.Forms.Label m_lblProfil;
        private System.Windows.Forms.Label m_lblContact;
    }
}