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
            System.Windows.Forms.SplitContainer m_mainSplitter;
            System.Windows.Forms.ImageList m_ilSmall;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsManagmentWindow));
            System.Windows.Forms.SplitContainer m_detailSplitter;
            System.Windows.Forms.GroupBox m_grpEnvironment;
            System.Windows.Forms.Label m_lblHubArchitectureLable;
            System.Windows.Forms.Label m_lblHubVersionLabel;
            System.Windows.Forms.Label m_lblUserNameLabel;
            System.Windows.Forms.Label m_lblMachineNameLabel;
            System.Windows.Forms.Label m_lblOSVersionLabel;
            System.Windows.Forms.Label m_lblOSArchitectureLabel;
            System.Windows.Forms.GroupBox m_grpContact;
            System.Windows.Forms.Label m_lblContactLabel;
            System.Windows.Forms.Label m_lblPhoneLabel;
            System.Windows.Forms.Label m_lblEMailLabel;
            System.Windows.Forms.GroupBox m_grpClientInfo;
            System.Windows.Forms.TableLayoutPanel m_tblPanelClient;
            System.Windows.Forms.Label m_lblStatusLabel;
            System.Windows.Forms.Label m_lblLastActivityLabel;
            System.Windows.Forms.Label m_lblCreationTimeLabel;
            System.Windows.Forms.ToolStrip m_toolstrip;
            System.Windows.Forms.ToolStripButton m_tsbProfiles;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
            System.Windows.Forms.ToolStripButton m_tsbOptions;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.StatusStrip m_statusStrip;
            this.m_tvClients = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_lblHubArchitecture = new System.Windows.Forms.Label();
            this.m_lblHubVersion = new System.Windows.Forms.Label();
            this.m_lblUserName = new System.Windows.Forms.Label();
            this.m_lblMachineName = new System.Windows.Forms.Label();
            this.m_lblOSVersion = new System.Windows.Forms.Label();
            this.m_lblOSArchitecture = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.m_lblContact = new System.Windows.Forms.Label();
            this.m_lblPhone = new System.Windows.Forms.Label();
            this.m_lblEMail = new System.Windows.Forms.Label();
            this.m_lblStatus = new System.Windows.Forms.Label();
            this.m_lblLastActivity = new System.Windows.Forms.Label();
            this.m_lblCreationTime = new System.Windows.Forms.Label();
            this.m_tbActivity = new System.Windows.Forms.TextBox();
            this.m_tsbRunningClientsOnly = new System.Windows.Forms.ToolStripButton();
            this.m_tsbEnableClient = new System.Windows.Forms.ToolStripButton();
            this.m_tsbDisableClient = new System.Windows.Forms.ToolStripButton();
            this.m_tsbBanishClient = new System.Windows.Forms.ToolStripButton();
            this.m_tsbShowActivityHistory = new System.Windows.Forms.ToolStripButton();
            this.m_tsbChat = new System.Windows.Forms.ToolStripButton();
            this.m_sslClientsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_sslRunningClientsCount = new System.Windows.Forms.ToolStripStatusLabel();
            m_mainSplitter = new System.Windows.Forms.SplitContainer();
            m_ilSmall = new System.Windows.Forms.ImageList(this.components);
            m_detailSplitter = new System.Windows.Forms.SplitContainer();
            m_grpEnvironment = new System.Windows.Forms.GroupBox();
            m_lblHubArchitectureLable = new System.Windows.Forms.Label();
            m_lblHubVersionLabel = new System.Windows.Forms.Label();
            m_lblUserNameLabel = new System.Windows.Forms.Label();
            m_lblMachineNameLabel = new System.Windows.Forms.Label();
            m_lblOSVersionLabel = new System.Windows.Forms.Label();
            m_lblOSArchitectureLabel = new System.Windows.Forms.Label();
            m_grpContact = new System.Windows.Forms.GroupBox();
            m_lblContactLabel = new System.Windows.Forms.Label();
            m_lblPhoneLabel = new System.Windows.Forms.Label();
            m_lblEMailLabel = new System.Windows.Forms.Label();
            m_grpClientInfo = new System.Windows.Forms.GroupBox();
            m_tblPanelClient = new System.Windows.Forms.TableLayoutPanel();
            m_lblStatusLabel = new System.Windows.Forms.Label();
            m_lblLastActivityLabel = new System.Windows.Forms.Label();
            m_lblCreationTimeLabel = new System.Windows.Forms.Label();
            m_toolstrip = new System.Windows.Forms.ToolStrip();
            m_tsbProfiles = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_statusStrip = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(m_mainSplitter)).BeginInit();
            m_mainSplitter.Panel1.SuspendLayout();
            m_mainSplitter.Panel2.SuspendLayout();
            m_mainSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_detailSplitter)).BeginInit();
            m_detailSplitter.Panel1.SuspendLayout();
            m_detailSplitter.Panel2.SuspendLayout();
            m_detailSplitter.SuspendLayout();
            m_grpEnvironment.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            m_grpContact.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            m_grpClientInfo.SuspendLayout();
            m_tblPanelClient.SuspendLayout();
            m_toolstrip.SuspendLayout();
            m_statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_mainSplitter
            // 
            m_mainSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            m_mainSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            m_mainSplitter.Location = new System.Drawing.Point(0, 25);
            m_mainSplitter.Name = "m_mainSplitter";
            // 
            // m_mainSplitter.Panel1
            // 
            m_mainSplitter.Panel1.Controls.Add(this.m_tvClients);
            // 
            // m_mainSplitter.Panel2
            // 
            m_mainSplitter.Panel2.Controls.Add(m_detailSplitter);
            m_mainSplitter.Size = new System.Drawing.Size(857, 381);
            m_mainSplitter.SplitterDistance = 153;
            m_mainSplitter.TabIndex = 0;
            // 
            // m_tvClients
            // 
            this.m_tvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tvClients.FullRowSelect = true;
            this.m_tvClients.HideSelection = false;
            this.m_tvClients.ImageIndex = 0;
            this.m_tvClients.ImageList = m_ilSmall;
            this.m_tvClients.Location = new System.Drawing.Point(0, 0);
            this.m_tvClients.Name = "m_tvClients";
            this.m_tvClients.SelectedImageIndex = 0;
            this.m_tvClients.ShowNodeToolTips = true;
            this.m_tvClients.Size = new System.Drawing.Size(153, 381);
            this.m_tvClients.TabIndex = 0;
            this.m_tvClients.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Clients_AfterSelect);
            // 
            // m_ilSmall
            // 
            m_ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ilSmall.ImageStream")));
            m_ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            m_ilSmall.Images.SetKeyName(0, "profile_16.png");
            m_ilSmall.Images.SetKeyName(1, "client_16.png");
            // 
            // m_detailSplitter
            // 
            m_detailSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            m_detailSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            m_detailSplitter.Location = new System.Drawing.Point(0, 0);
            m_detailSplitter.Name = "m_detailSplitter";
            m_detailSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_detailSplitter.Panel1
            // 
            m_detailSplitter.Panel1.AutoScroll = true;
            m_detailSplitter.Panel1.BackColor = System.Drawing.SystemColors.Window;
            m_detailSplitter.Panel1.Controls.Add(m_grpEnvironment);
            m_detailSplitter.Panel1.Controls.Add(m_grpContact);
            m_detailSplitter.Panel1.Controls.Add(m_grpClientInfo);
            m_detailSplitter.Panel1.ForeColor = System.Drawing.SystemColors.WindowText;
            // 
            // m_detailSplitter.Panel2
            // 
            m_detailSplitter.Panel2.Controls.Add(this.m_tbActivity);
            m_detailSplitter.Size = new System.Drawing.Size(700, 381);
            m_detailSplitter.SplitterDistance = 239;
            m_detailSplitter.TabIndex = 0;
            // 
            // m_grpEnvironment
            // 
            m_grpEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_grpEnvironment.Controls.Add(this.tableLayoutPanel1);
            m_grpEnvironment.Location = new System.Drawing.Point(296, 16);
            m_grpEnvironment.MinimumSize = new System.Drawing.Size(275, 152);
            m_grpEnvironment.Name = "m_grpEnvironment";
            m_grpEnvironment.Size = new System.Drawing.Size(392, 152);
            m_grpEnvironment.TabIndex = 4;
            m_grpEnvironment.TabStop = false;
            m_grpEnvironment.Text = " Environnement ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(m_lblHubArchitectureLable, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.m_lblHubArchitecture, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.m_lblHubVersion, 1, 4);
            this.tableLayoutPanel1.Controls.Add(m_lblHubVersionLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(m_lblUserNameLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_lblUserName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(m_lblMachineNameLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_lblMachineName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(m_lblOSVersionLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_lblOSVersion, 1, 2);
            this.tableLayoutPanel1.Controls.Add(m_lblOSArchitectureLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.m_lblOSArchitecture, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(377, 127);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // m_lblHubArchitectureLable
            // 
            m_lblHubArchitectureLable.AutoSize = true;
            m_lblHubArchitectureLable.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblHubArchitectureLable.Location = new System.Drawing.Point(3, 103);
            m_lblHubArchitectureLable.Margin = new System.Windows.Forms.Padding(3);
            m_lblHubArchitectureLable.Name = "m_lblHubArchitectureLable";
            m_lblHubArchitectureLable.Size = new System.Drawing.Size(110, 21);
            m_lblHubArchitectureLable.TabIndex = 12;
            m_lblHubArchitectureLable.Text = "Architecture du Hub:";
            m_lblHubArchitectureLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblHubArchitecture
            // 
            this.m_lblHubArchitecture.AutoEllipsis = true;
            this.m_lblHubArchitecture.AutoSize = true;
            this.m_lblHubArchitecture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblHubArchitecture.Location = new System.Drawing.Point(119, 103);
            this.m_lblHubArchitecture.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblHubArchitecture.Name = "m_lblHubArchitecture";
            this.m_lblHubArchitecture.Size = new System.Drawing.Size(255, 21);
            this.m_lblHubArchitecture.TabIndex = 11;
            this.m_lblHubArchitecture.Text = "-";
            this.m_lblHubArchitecture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblHubVersion
            // 
            this.m_lblHubVersion.AutoEllipsis = true;
            this.m_lblHubVersion.AutoSize = true;
            this.m_lblHubVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblHubVersion.Location = new System.Drawing.Point(119, 83);
            this.m_lblHubVersion.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblHubVersion.Name = "m_lblHubVersion";
            this.m_lblHubVersion.Size = new System.Drawing.Size(255, 14);
            this.m_lblHubVersion.TabIndex = 9;
            this.m_lblHubVersion.Text = "-";
            this.m_lblHubVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblHubVersionLabel
            // 
            m_lblHubVersionLabel.AutoSize = true;
            m_lblHubVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblHubVersionLabel.Location = new System.Drawing.Point(3, 83);
            m_lblHubVersionLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblHubVersionLabel.Name = "m_lblHubVersionLabel";
            m_lblHubVersionLabel.Size = new System.Drawing.Size(110, 14);
            m_lblHubVersionLabel.TabIndex = 8;
            m_lblHubVersionLabel.Text = "Version du Hub:";
            m_lblHubVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblUserNameLabel
            // 
            m_lblUserNameLabel.AutoSize = true;
            m_lblUserNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblUserNameLabel.Location = new System.Drawing.Point(3, 3);
            m_lblUserNameLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblUserNameLabel.Name = "m_lblUserNameLabel";
            m_lblUserNameLabel.Size = new System.Drawing.Size(110, 14);
            m_lblUserNameLabel.TabIndex = 0;
            m_lblUserNameLabel.Text = "Utilsateur:";
            m_lblUserNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblUserName
            // 
            this.m_lblUserName.AutoEllipsis = true;
            this.m_lblUserName.AutoSize = true;
            this.m_lblUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblUserName.Location = new System.Drawing.Point(119, 3);
            this.m_lblUserName.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblUserName.Name = "m_lblUserName";
            this.m_lblUserName.Size = new System.Drawing.Size(255, 14);
            this.m_lblUserName.TabIndex = 1;
            this.m_lblUserName.Text = "-";
            this.m_lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblMachineNameLabel
            // 
            m_lblMachineNameLabel.AutoSize = true;
            m_lblMachineNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblMachineNameLabel.Location = new System.Drawing.Point(3, 23);
            m_lblMachineNameLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblMachineNameLabel.Name = "m_lblMachineNameLabel";
            m_lblMachineNameLabel.Size = new System.Drawing.Size(110, 14);
            m_lblMachineNameLabel.TabIndex = 2;
            m_lblMachineNameLabel.Text = "Nom de la station:";
            m_lblMachineNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblMachineName
            // 
            this.m_lblMachineName.AutoEllipsis = true;
            this.m_lblMachineName.AutoSize = true;
            this.m_lblMachineName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblMachineName.Location = new System.Drawing.Point(119, 23);
            this.m_lblMachineName.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblMachineName.Name = "m_lblMachineName";
            this.m_lblMachineName.Size = new System.Drawing.Size(255, 14);
            this.m_lblMachineName.TabIndex = 3;
            this.m_lblMachineName.Text = "-";
            this.m_lblMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblOSVersionLabel
            // 
            m_lblOSVersionLabel.AutoSize = true;
            m_lblOSVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblOSVersionLabel.Location = new System.Drawing.Point(3, 43);
            m_lblOSVersionLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblOSVersionLabel.Name = "m_lblOSVersionLabel";
            m_lblOSVersionLabel.Size = new System.Drawing.Size(110, 14);
            m_lblOSVersionLabel.TabIndex = 4;
            m_lblOSVersionLabel.Text = "OS Version:";
            m_lblOSVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblOSVersion
            // 
            this.m_lblOSVersion.AutoEllipsis = true;
            this.m_lblOSVersion.AutoSize = true;
            this.m_lblOSVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblOSVersion.Location = new System.Drawing.Point(119, 43);
            this.m_lblOSVersion.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblOSVersion.Name = "m_lblOSVersion";
            this.m_lblOSVersion.Size = new System.Drawing.Size(255, 14);
            this.m_lblOSVersion.TabIndex = 5;
            this.m_lblOSVersion.Text = "-";
            this.m_lblOSVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblOSArchitectureLabel
            // 
            m_lblOSArchitectureLabel.AutoSize = true;
            m_lblOSArchitectureLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblOSArchitectureLabel.Location = new System.Drawing.Point(3, 63);
            m_lblOSArchitectureLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblOSArchitectureLabel.Name = "m_lblOSArchitectureLabel";
            m_lblOSArchitectureLabel.Size = new System.Drawing.Size(110, 14);
            m_lblOSArchitectureLabel.TabIndex = 6;
            m_lblOSArchitectureLabel.Text = "OS Architecture:";
            m_lblOSArchitectureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblOSArchitecture
            // 
            this.m_lblOSArchitecture.AutoEllipsis = true;
            this.m_lblOSArchitecture.AutoSize = true;
            this.m_lblOSArchitecture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblOSArchitecture.Location = new System.Drawing.Point(119, 63);
            this.m_lblOSArchitecture.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblOSArchitecture.Name = "m_lblOSArchitecture";
            this.m_lblOSArchitecture.Size = new System.Drawing.Size(255, 14);
            this.m_lblOSArchitecture.TabIndex = 7;
            this.m_lblOSArchitecture.Text = "-";
            this.m_lblOSArchitecture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_grpContact
            // 
            m_grpContact.Controls.Add(this.tableLayoutPanel2);
            m_grpContact.Location = new System.Drawing.Point(15, 117);
            m_grpContact.Name = "m_grpContact";
            m_grpContact.Size = new System.Drawing.Size(272, 88);
            m_grpContact.TabIndex = 3;
            m_grpContact.TabStop = false;
            m_grpContact.Text = " Contact ";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(m_lblContactLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.m_lblContact, 1, 0);
            this.tableLayoutPanel2.Controls.Add(m_lblPhoneLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.m_lblPhone, 1, 1);
            this.tableLayoutPanel2.Controls.Add(m_lblEMailLabel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.m_lblEMail, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(257, 62);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // m_lblContactLabel
            // 
            m_lblContactLabel.AutoSize = true;
            m_lblContactLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblContactLabel.Location = new System.Drawing.Point(3, 3);
            m_lblContactLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblContactLabel.Name = "m_lblContactLabel";
            m_lblContactLabel.Size = new System.Drawing.Size(110, 14);
            m_lblContactLabel.TabIndex = 0;
            m_lblContactLabel.Text = "Nom:";
            m_lblContactLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblContact
            // 
            this.m_lblContact.AutoEllipsis = true;
            this.m_lblContact.AutoSize = true;
            this.m_lblContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblContact.Location = new System.Drawing.Point(119, 3);
            this.m_lblContact.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblContact.Name = "m_lblContact";
            this.m_lblContact.Size = new System.Drawing.Size(135, 14);
            this.m_lblContact.TabIndex = 1;
            this.m_lblContact.Text = "-";
            this.m_lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblPhoneLabel
            // 
            m_lblPhoneLabel.AutoSize = true;
            m_lblPhoneLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblPhoneLabel.Location = new System.Drawing.Point(3, 23);
            m_lblPhoneLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblPhoneLabel.Name = "m_lblPhoneLabel";
            m_lblPhoneLabel.Size = new System.Drawing.Size(110, 14);
            m_lblPhoneLabel.TabIndex = 2;
            m_lblPhoneLabel.Text = "Tél.:";
            m_lblPhoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblPhone
            // 
            this.m_lblPhone.AutoEllipsis = true;
            this.m_lblPhone.AutoSize = true;
            this.m_lblPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblPhone.Location = new System.Drawing.Point(119, 23);
            this.m_lblPhone.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblPhone.Name = "m_lblPhone";
            this.m_lblPhone.Size = new System.Drawing.Size(135, 14);
            this.m_lblPhone.TabIndex = 3;
            this.m_lblPhone.Text = "-";
            this.m_lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblEMailLabel
            // 
            m_lblEMailLabel.AutoSize = true;
            m_lblEMailLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblEMailLabel.Location = new System.Drawing.Point(3, 43);
            m_lblEMailLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblEMailLabel.Name = "m_lblEMailLabel";
            m_lblEMailLabel.Size = new System.Drawing.Size(110, 16);
            m_lblEMailLabel.TabIndex = 4;
            m_lblEMailLabel.Text = "E-mail:";
            m_lblEMailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblEMail
            // 
            this.m_lblEMail.AutoEllipsis = true;
            this.m_lblEMail.AutoSize = true;
            this.m_lblEMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblEMail.Location = new System.Drawing.Point(119, 43);
            this.m_lblEMail.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblEMail.Name = "m_lblEMail";
            this.m_lblEMail.Size = new System.Drawing.Size(135, 16);
            this.m_lblEMail.TabIndex = 5;
            this.m_lblEMail.Text = "-";
            this.m_lblEMail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_grpClientInfo
            // 
            m_grpClientInfo.Controls.Add(m_tblPanelClient);
            m_grpClientInfo.Location = new System.Drawing.Point(15, 16);
            m_grpClientInfo.Name = "m_grpClientInfo";
            m_grpClientInfo.Size = new System.Drawing.Size(272, 95);
            m_grpClientInfo.TabIndex = 2;
            m_grpClientInfo.TabStop = false;
            m_grpClientInfo.Text = " Client ";
            // 
            // m_tblPanelClient
            // 
            m_tblPanelClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_tblPanelClient.ColumnCount = 2;
            m_tblPanelClient.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            m_tblPanelClient.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            m_tblPanelClient.Controls.Add(m_lblStatusLabel, 0, 2);
            m_tblPanelClient.Controls.Add(this.m_lblStatus, 0, 2);
            m_tblPanelClient.Controls.Add(this.m_lblLastActivity, 0, 1);
            m_tblPanelClient.Controls.Add(m_lblLastActivityLabel, 0, 1);
            m_tblPanelClient.Controls.Add(m_lblCreationTimeLabel, 0, 0);
            m_tblPanelClient.Controls.Add(this.m_lblCreationTime, 0, 0);
            m_tblPanelClient.Location = new System.Drawing.Point(6, 16);
            m_tblPanelClient.Name = "m_tblPanelClient";
            m_tblPanelClient.RowCount = 3;
            m_tblPanelClient.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            m_tblPanelClient.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            m_tblPanelClient.RowStyles.Add(new System.Windows.Forms.RowStyle());
            m_tblPanelClient.Size = new System.Drawing.Size(260, 62);
            m_tblPanelClient.TabIndex = 0;
            // 
            // m_lblStatusLabel
            // 
            m_lblStatusLabel.AutoSize = true;
            m_lblStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblStatusLabel.Location = new System.Drawing.Point(3, 43);
            m_lblStatusLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblStatusLabel.Name = "m_lblStatusLabel";
            m_lblStatusLabel.Size = new System.Drawing.Size(110, 16);
            m_lblStatusLabel.TabIndex = 14;
            m_lblStatusLabel.Text = "Statut:";
            m_lblStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.AutoEllipsis = true;
            this.m_lblStatus.AutoSize = true;
            this.m_lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblStatus.Location = new System.Drawing.Point(119, 43);
            this.m_lblStatus.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(138, 16);
            this.m_lblStatus.TabIndex = 13;
            this.m_lblStatus.Text = "-";
            this.m_lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblLastActivity
            // 
            this.m_lblLastActivity.AutoEllipsis = true;
            this.m_lblLastActivity.AutoSize = true;
            this.m_lblLastActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblLastActivity.Location = new System.Drawing.Point(119, 23);
            this.m_lblLastActivity.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblLastActivity.Name = "m_lblLastActivity";
            this.m_lblLastActivity.Size = new System.Drawing.Size(138, 14);
            this.m_lblLastActivity.TabIndex = 12;
            this.m_lblLastActivity.Text = "-";
            this.m_lblLastActivity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblLastActivityLabel
            // 
            m_lblLastActivityLabel.AutoSize = true;
            m_lblLastActivityLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblLastActivityLabel.Location = new System.Drawing.Point(3, 23);
            m_lblLastActivityLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblLastActivityLabel.Name = "m_lblLastActivityLabel";
            m_lblLastActivityLabel.Size = new System.Drawing.Size(110, 14);
            m_lblLastActivityLabel.TabIndex = 11;
            m_lblLastActivityLabel.Text = "Dernière activité:";
            m_lblLastActivityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblCreationTimeLabel
            // 
            m_lblCreationTimeLabel.AutoSize = true;
            m_lblCreationTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblCreationTimeLabel.Location = new System.Drawing.Point(3, 3);
            m_lblCreationTimeLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblCreationTimeLabel.Name = "m_lblCreationTimeLabel";
            m_lblCreationTimeLabel.Size = new System.Drawing.Size(110, 14);
            m_lblCreationTimeLabel.TabIndex = 10;
            m_lblCreationTimeLabel.Text = "Inscrit le:";
            m_lblCreationTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblCreationTime
            // 
            this.m_lblCreationTime.AutoEllipsis = true;
            this.m_lblCreationTime.AutoSize = true;
            this.m_lblCreationTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblCreationTime.Location = new System.Drawing.Point(119, 3);
            this.m_lblCreationTime.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblCreationTime.Name = "m_lblCreationTime";
            this.m_lblCreationTime.Size = new System.Drawing.Size(138, 14);
            this.m_lblCreationTime.TabIndex = 9;
            this.m_lblCreationTime.Text = "-";
            this.m_lblCreationTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_tbActivity
            // 
            this.m_tbActivity.BackColor = System.Drawing.SystemColors.Window;
            this.m_tbActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbActivity.Location = new System.Drawing.Point(0, 0);
            this.m_tbActivity.Multiline = true;
            this.m_tbActivity.Name = "m_tbActivity";
            this.m_tbActivity.ReadOnly = true;
            this.m_tbActivity.Size = new System.Drawing.Size(700, 138);
            this.m_tbActivity.TabIndex = 0;
            // 
            // m_toolstrip
            // 
            m_toolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbProfiles,
            toolStripSeparator1,
            this.m_tsbRunningClientsOnly,
            toolStripSeparator2,
            this.m_tsbEnableClient,
            this.m_tsbDisableClient,
            this.m_tsbBanishClient,
            toolStripSeparator3,
            this.m_tsbShowActivityHistory,
            toolStripSeparator4,
            this.m_tsbChat,
            toolStripSeparator5,
            m_tsbOptions,
            m_tsbHelp});
            m_toolstrip.Location = new System.Drawing.Point(0, 0);
            m_toolstrip.Name = "m_toolstrip";
            m_toolstrip.Size = new System.Drawing.Size(857, 25);
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
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbRunningClientsOnly
            // 
            this.m_tsbRunningClientsOnly.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbRunningClientsOnly.Enabled = false;
            this.m_tsbRunningClientsOnly.Image = global::DGD.HubGovernor.Properties.Resources.profile_clients_16;
            this.m_tsbRunningClientsOnly.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbRunningClientsOnly.Name = "m_tsbRunningClientsOnly";
            this.m_tsbRunningClientsOnly.Size = new System.Drawing.Size(23, 22);
            this.m_tsbRunningClientsOnly.Text = "Afficher uniquement les clients en ligne";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbEnableClient
            // 
            this.m_tsbEnableClient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbEnableClient.Enabled = false;
            this.m_tsbEnableClient.Image = global::DGD.HubGovernor.Properties.Resources.enable_client_16;
            this.m_tsbEnableClient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbEnableClient.Name = "m_tsbEnableClient";
            this.m_tsbEnableClient.Size = new System.Drawing.Size(23, 22);
            this.m_tsbEnableClient.Text = "Définir comme client actif";
            // 
            // m_tsbDisableClient
            // 
            this.m_tsbDisableClient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbDisableClient.Enabled = false;
            this.m_tsbDisableClient.Image = global::DGD.HubGovernor.Properties.Resources.disabled_client_16;
            this.m_tsbDisableClient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbDisableClient.Name = "m_tsbDisableClient";
            this.m_tsbDisableClient.Size = new System.Drawing.Size(23, 22);
            this.m_tsbDisableClient.Text = "Désactiver le client";
            // 
            // m_tsbBanishClient
            // 
            this.m_tsbBanishClient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbBanishClient.Enabled = false;
            this.m_tsbBanishClient.Image = global::DGD.HubGovernor.Properties.Resources.banned_client_16;
            this.m_tsbBanishClient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbBanishClient.Name = "m_tsbBanishClient";
            this.m_tsbBanishClient.Size = new System.Drawing.Size(23, 22);
            this.m_tsbBanishClient.Text = "Bannir le client";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbShowActivityHistory
            // 
            this.m_tsbShowActivityHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbShowActivityHistory.Enabled = false;
            this.m_tsbShowActivityHistory.Image = global::DGD.HubGovernor.Properties.Resources.history_16;
            this.m_tsbShowActivityHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbShowActivityHistory.Name = "m_tsbShowActivityHistory";
            this.m_tsbShowActivityHistory.Size = new System.Drawing.Size(23, 22);
            this.m_tsbShowActivityHistory.Text = "Afficher l’historique des activités";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbChat
            // 
            this.m_tsbChat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbChat.Enabled = false;
            this.m_tsbChat.Image = global::DGD.HubGovernor.Properties.Resources.chat_16;
            this.m_tsbChat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbChat.Name = "m_tsbChat";
            this.m_tsbChat.Size = new System.Drawing.Size(23, 22);
            this.m_tsbChat.Text = "Communiquer avec le client";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbOptions
            // 
            m_tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbOptions.Enabled = false;
            m_tsbOptions.Image = global::DGD.HubGovernor.Properties.Resources.option_16;
            m_tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbOptions.Name = "m_tsbOptions";
            m_tsbOptions.Size = new System.Drawing.Size(23, 22);
            m_tsbOptions.Text = "Paramètres";
            // 
            // m_tsbHelp
            // 
            m_tsbHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            m_tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbHelp.Enabled = false;
            m_tsbHelp.Image = global::DGD.HubGovernor.Properties.Resources.help_16;
            m_tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbHelp.Name = "m_tsbHelp";
            m_tsbHelp.Size = new System.Drawing.Size(23, 22);
            m_tsbHelp.Text = "Aide";
            // 
            // m_statusStrip
            // 
            m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_sslClientsCount,
            this.m_sslRunningClientsCount});
            m_statusStrip.Location = new System.Drawing.Point(0, 406);
            m_statusStrip.Name = "m_statusStrip";
            m_statusStrip.Size = new System.Drawing.Size(857, 22);
            m_statusStrip.TabIndex = 0;
            // 
            // m_sslClientsCount
            // 
            this.m_sslClientsCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.m_sslClientsCount.Name = "m_sslClientsCount";
            this.m_sslClientsCount.Size = new System.Drawing.Size(4, 17);
            // 
            // m_sslRunningClientsCount
            // 
            this.m_sslRunningClientsCount.Name = "m_sslRunningClientsCount";
            this.m_sslRunningClientsCount.Size = new System.Drawing.Size(0, 17);
            // 
            // ClientsManagmentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 428);
            this.Controls.Add(m_mainSplitter);
            this.Controls.Add(m_toolstrip);
            this.Controls.Add(m_statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClientsManagmentWindow";
            this.Text = "Gestionnaire des clients";
            m_mainSplitter.Panel1.ResumeLayout(false);
            m_mainSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_mainSplitter)).EndInit();
            m_mainSplitter.ResumeLayout(false);
            m_detailSplitter.Panel1.ResumeLayout(false);
            m_detailSplitter.Panel2.ResumeLayout(false);
            m_detailSplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(m_detailSplitter)).EndInit();
            m_detailSplitter.ResumeLayout(false);
            m_grpEnvironment.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            m_grpContact.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            m_grpClientInfo.ResumeLayout(false);
            m_tblPanelClient.ResumeLayout(false);
            m_tblPanelClient.PerformLayout();
            m_toolstrip.ResumeLayout(false);
            m_toolstrip.PerformLayout();
            m_statusStrip.ResumeLayout(false);
            m_statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView m_tvClients;
        private System.Windows.Forms.ToolStripButton m_tsbEnableClient;
        private System.Windows.Forms.ToolStripButton m_tsbDisableClient;
        private System.Windows.Forms.ToolStripButton m_tsbBanishClient;
        private System.Windows.Forms.ToolStripButton m_tsbRunningClientsOnly;
        private System.Windows.Forms.ToolStripButton m_tsbShowActivityHistory;
        private System.Windows.Forms.ToolStripButton m_tsbChat;
        private System.Windows.Forms.ToolStripStatusLabel m_sslClientsCount;
        private System.Windows.Forms.ToolStripStatusLabel m_sslRunningClientsCount;
        private System.Windows.Forms.TextBox m_tbActivity;
        private System.Windows.Forms.Label m_lblStatus;
        private System.Windows.Forms.Label m_lblLastActivity;
        private System.Windows.Forms.Label m_lblCreationTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label m_lblContact;
        private System.Windows.Forms.Label m_lblPhone;
        private System.Windows.Forms.Label m_lblEMail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label m_lblUserName;
        private System.Windows.Forms.Label m_lblMachineName;
        private System.Windows.Forms.Label m_lblOSVersion;
        private System.Windows.Forms.Label m_lblOSArchitecture;
        private System.Windows.Forms.Label m_lblHubArchitecture;
        private System.Windows.Forms.Label m_lblHubVersion;
    }
}