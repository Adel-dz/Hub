namespace DGD.HubGovernor.Clients
{
    partial class ClientsWindow
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
            System.Windows.Forms.SplitContainer m_splitter;
            System.Windows.Forms.ImageList m_ilSmall;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsWindow));
            System.Windows.Forms.GroupBox m_grpClientInfo;
            System.Windows.Forms.TableLayoutPanel m_tblPanelClient;
            System.Windows.Forms.Label m_lblStatusLabel;
            System.Windows.Forms.Label m_lblLastActivityLabel;
            System.Windows.Forms.Label m_lblCreationTimeLabel;
            System.Windows.Forms.Label m_lblEMailLabel;
            System.Windows.Forms.Label m_lblPhoneLabel;
            System.Windows.Forms.Label m_lblContactNameLabel;
            System.Windows.Forms.Label m_lblMachineNameLabel;
            this.m_tvClients = new System.Windows.Forms.TreeView();
            this.m_lblStatus = new System.Windows.Forms.Label();
            this.m_lblLastActivity = new System.Windows.Forms.Label();
            this.m_lblCreationTime = new System.Windows.Forms.Label();
            this.m_toolStrip = new System.Windows.Forms.ToolStrip();
            this.m_tsbEnableClient = new System.Windows.Forms.ToolStripButton();
            this.m_tsbDisableClient = new System.Windows.Forms.ToolStripButton();
            this.m_tsbBanishClient = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.m_lblEMail = new System.Windows.Forms.Label();
            this.m_lblPhone = new System.Windows.Forms.Label();
            this.m_lblContactName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_lblMachineName = new System.Windows.Forms.Label();
            m_splitter = new System.Windows.Forms.SplitContainer();
            m_ilSmall = new System.Windows.Forms.ImageList(this.components);
            m_grpClientInfo = new System.Windows.Forms.GroupBox();
            m_tblPanelClient = new System.Windows.Forms.TableLayoutPanel();
            m_lblStatusLabel = new System.Windows.Forms.Label();
            m_lblLastActivityLabel = new System.Windows.Forms.Label();
            m_lblCreationTimeLabel = new System.Windows.Forms.Label();
            m_lblEMailLabel = new System.Windows.Forms.Label();
            m_lblPhoneLabel = new System.Windows.Forms.Label();
            m_lblContactNameLabel = new System.Windows.Forms.Label();
            m_lblMachineNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).BeginInit();
            m_splitter.Panel1.SuspendLayout();
            m_splitter.Panel2.SuspendLayout();
            m_splitter.SuspendLayout();
            m_grpClientInfo.SuspendLayout();
            m_tblPanelClient.SuspendLayout();
            this.m_toolStrip.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitter
            // 
            m_splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            m_splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            m_splitter.Location = new System.Drawing.Point(0, 25);
            m_splitter.Name = "m_splitter";
            // 
            // m_splitter.Panel1
            // 
            m_splitter.Panel1.Controls.Add(this.m_tvClients);
            // 
            // m_splitter.Panel2
            // 
            m_splitter.Panel2.Controls.Add(this.groupBox2);
            m_splitter.Panel2.Controls.Add(m_grpClientInfo);
            m_splitter.Size = new System.Drawing.Size(602, 277);
            m_splitter.SplitterDistance = 157;
            m_splitter.TabIndex = 2;
            // 
            // m_tvClients
            // 
            this.m_tvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tvClients.HideSelection = false;
            this.m_tvClients.ImageIndex = 0;
            this.m_tvClients.ImageList = m_ilSmall;
            this.m_tvClients.Location = new System.Drawing.Point(0, 0);
            this.m_tvClients.Name = "m_tvClients";
            this.m_tvClients.SelectedImageIndex = 0;
            this.m_tvClients.ShowNodeToolTips = true;
            this.m_tvClients.Size = new System.Drawing.Size(157, 277);
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
            // m_grpClientInfo
            // 
            m_grpClientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_grpClientInfo.Controls.Add(m_tblPanelClient);
            m_grpClientInfo.Location = new System.Drawing.Point(14, 16);
            m_grpClientInfo.Name = "m_grpClientInfo";
            m_grpClientInfo.Size = new System.Drawing.Size(409, 95);
            m_grpClientInfo.TabIndex = 1;
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
            m_tblPanelClient.Size = new System.Drawing.Size(397, 62);
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
            this.m_lblStatus.AutoSize = true;
            this.m_lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblStatus.Location = new System.Drawing.Point(119, 43);
            this.m_lblStatus.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(275, 16);
            this.m_lblStatus.TabIndex = 13;
            this.m_lblStatus.Text = "-";
            this.m_lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblLastActivity
            // 
            this.m_lblLastActivity.AutoSize = true;
            this.m_lblLastActivity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblLastActivity.Location = new System.Drawing.Point(119, 23);
            this.m_lblLastActivity.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblLastActivity.Name = "m_lblLastActivity";
            this.m_lblLastActivity.Size = new System.Drawing.Size(275, 14);
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
            m_lblLastActivityLabel.Text = "Dernière activité le:";
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
            this.m_lblCreationTime.AutoSize = true;
            this.m_lblCreationTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblCreationTime.Location = new System.Drawing.Point(119, 3);
            this.m_lblCreationTime.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblCreationTime.Name = "m_lblCreationTime";
            this.m_lblCreationTime.Size = new System.Drawing.Size(275, 14);
            this.m_lblCreationTime.TabIndex = 9;
            this.m_lblCreationTime.Text = "-";
            this.m_lblCreationTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_toolStrip
            // 
            this.m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbEnableClient,
            this.m_tsbDisableClient,
            this.m_tsbBanishClient,
            this.toolStripButton4});
            this.m_toolStrip.Location = new System.Drawing.Point(0, 0);
            this.m_toolStrip.Name = "m_toolStrip";
            this.m_toolStrip.Size = new System.Drawing.Size(602, 25);
            this.m_toolStrip.TabIndex = 0;
            // 
            // m_tsbEnableClient
            // 
            this.m_tsbEnableClient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbEnableClient.Enabled = false;
            this.m_tsbEnableClient.Image = global::DGD.HubGovernor.Properties.Resources.enable_client_16;
            this.m_tsbEnableClient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbEnableClient.Name = "m_tsbEnableClient";
            this.m_tsbEnableClient.Size = new System.Drawing.Size(23, 22);
            this.m_tsbEnableClient.Text = "Activee le client";
            this.m_tsbEnableClient.Click += new System.EventHandler(this.EnableClient_Click);
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
            this.m_tsbDisableClient.Click += new System.EventHandler(this.DisableClient_Click);
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
            this.m_tsbBanishClient.Click += new System.EventHandler(this.BanishClient_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::DGD.HubGovernor.Properties.Resources.help_16;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(m_lblContactNameLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.m_lblContactName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(m_lblPhoneLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.m_lblPhone, 1, 1);
            this.tableLayoutPanel2.Controls.Add(m_lblEMailLabel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.m_lblEMail, 1, 2);
            this.tableLayoutPanel2.Controls.Add(m_lblMachineNameLabel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.m_lblMachineName, 1, 3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(394, 86);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // m_lblEMail
            // 
            this.m_lblEMail.AutoSize = true;
            this.m_lblEMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblEMail.Location = new System.Drawing.Point(119, 43);
            this.m_lblEMail.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblEMail.Name = "m_lblEMail";
            this.m_lblEMail.Size = new System.Drawing.Size(272, 14);
            this.m_lblEMail.TabIndex = 5;
            this.m_lblEMail.Text = "-";
            this.m_lblEMail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblEMailLabel
            // 
            m_lblEMailLabel.AutoSize = true;
            m_lblEMailLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblEMailLabel.Location = new System.Drawing.Point(3, 43);
            m_lblEMailLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblEMailLabel.Name = "m_lblEMailLabel";
            m_lblEMailLabel.Size = new System.Drawing.Size(110, 14);
            m_lblEMailLabel.TabIndex = 4;
            m_lblEMailLabel.Text = "e-mail:";
            m_lblEMailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblPhone
            // 
            this.m_lblPhone.AutoSize = true;
            this.m_lblPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblPhone.Location = new System.Drawing.Point(119, 23);
            this.m_lblPhone.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblPhone.Name = "m_lblPhone";
            this.m_lblPhone.Size = new System.Drawing.Size(272, 14);
            this.m_lblPhone.TabIndex = 3;
            this.m_lblPhone.Text = "-";
            this.m_lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // m_lblContactName
            // 
            this.m_lblContactName.AutoSize = true;
            this.m_lblContactName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblContactName.Location = new System.Drawing.Point(119, 3);
            this.m_lblContactName.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblContactName.Name = "m_lblContactName";
            this.m_lblContactName.Size = new System.Drawing.Size(272, 14);
            this.m_lblContactName.TabIndex = 1;
            this.m_lblContactName.Text = "-";
            this.m_lblContactName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblContactNameLabel
            // 
            m_lblContactNameLabel.AutoSize = true;
            m_lblContactNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblContactNameLabel.Location = new System.Drawing.Point(3, 3);
            m_lblContactNameLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblContactNameLabel.Name = "m_lblContactNameLabel";
            m_lblContactNameLabel.Size = new System.Drawing.Size(110, 14);
            m_lblContactNameLabel.TabIndex = 0;
            m_lblContactNameLabel.Text = "Nom:";
            m_lblContactNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(14, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 118);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Contact ";
            // 
            // m_lblMachineName
            // 
            this.m_lblMachineName.AutoSize = true;
            this.m_lblMachineName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblMachineName.Location = new System.Drawing.Point(119, 63);
            this.m_lblMachineName.Margin = new System.Windows.Forms.Padding(3);
            this.m_lblMachineName.Name = "m_lblMachineName";
            this.m_lblMachineName.Size = new System.Drawing.Size(272, 20);
            this.m_lblMachineName.TabIndex = 7;
            this.m_lblMachineName.Text = "-";
            this.m_lblMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblMachineNameLabel
            // 
            m_lblMachineNameLabel.AutoSize = true;
            m_lblMachineNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblMachineNameLabel.Location = new System.Drawing.Point(3, 63);
            m_lblMachineNameLabel.Margin = new System.Windows.Forms.Padding(3);
            m_lblMachineNameLabel.Name = "m_lblMachineNameLabel";
            m_lblMachineNameLabel.Size = new System.Drawing.Size(110, 20);
            m_lblMachineNameLabel.TabIndex = 6;
            m_lblMachineNameLabel.Text = "Nom de la machine:";
            m_lblMachineNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ClientsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(602, 302);
            this.Controls.Add(m_splitter);
            this.Controls.Add(this.m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClientsWindow";
            this.Text = "Clients";
            m_splitter.Panel1.ResumeLayout(false);
            m_splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).EndInit();
            m_splitter.ResumeLayout(false);
            m_grpClientInfo.ResumeLayout(false);
            m_tblPanelClient.ResumeLayout(false);
            m_tblPanelClient.PerformLayout();
            this.m_toolStrip.ResumeLayout(false);
            this.m_toolStrip.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip m_toolStrip;
        private System.Windows.Forms.TreeView m_tvClients;
        private System.Windows.Forms.Label m_lblStatus;
        private System.Windows.Forms.Label m_lblLastActivity;
        private System.Windows.Forms.Label m_lblCreationTime;
        private System.Windows.Forms.ToolStripButton m_tsbEnableClient;
        private System.Windows.Forms.ToolStripButton m_tsbDisableClient;
        private System.Windows.Forms.ToolStripButton m_tsbBanishClient;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label m_lblContactName;
        private System.Windows.Forms.Label m_lblPhone;
        private System.Windows.Forms.Label m_lblEMail;
        private System.Windows.Forms.Label m_lblMachineName;
    }
}