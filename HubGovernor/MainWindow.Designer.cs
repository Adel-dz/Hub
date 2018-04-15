namespace DGD.HubGovernor
{
    partial class MainWindow
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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
            System.Windows.Forms.ToolStripButton m_tsbBackup;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.ToolStripMenuItem m_miCheckIntegrity;
            System.Windows.Forms.ToolStripButton m_tsbTR;
            System.Windows.Forms.ToolStripButton m_tsbUpdate;
            System.Windows.Forms.ToolStripButton m_tsbRepository;
            System.Windows.Forms.ToolStripButton m_tsbClientWindow;
            System.Windows.Forms.ToolStripButton m_tsbSysLog;
            System.Windows.Forms.ToolStripButton m_tsbSettings;
            this.m_toolStrip = new System.Windows.Forms.ToolStrip();
            this.m_tsbMainMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.afficherLeJournalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mpAdministration = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbLogView = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbBackup = new System.Windows.Forms.ToolStripButton();
            m_miCheckIntegrity = new System.Windows.Forms.ToolStripMenuItem();
            m_tsbTR = new System.Windows.Forms.ToolStripButton();
            m_tsbUpdate = new System.Windows.Forms.ToolStripButton();
            m_tsbRepository = new System.Windows.Forms.ToolStripButton();
            m_tsbClientWindow = new System.Windows.Forms.ToolStripButton();
            m_tsbSysLog = new System.Windows.Forms.ToolStripButton();
            m_tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // m_tsbBackup
            // 
            m_tsbBackup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbBackup.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbBackup.Image")));
            m_tsbBackup.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbBackup.Name = "m_tsbBackup";
            m_tsbBackup.Size = new System.Drawing.Size(36, 36);
            m_tsbBackup.Text = "Archiver...";
            m_tsbBackup.Click += new System.EventHandler(this.Backup_Click);
            // 
            // m_miCheckIntegrity
            // 
            m_miCheckIntegrity.Name = "m_miCheckIntegrity";
            m_miCheckIntegrity.Size = new System.Drawing.Size(231, 22);
            m_miCheckIntegrity.Text = "Vérifier l’intégrité des tables... ";
            m_miCheckIntegrity.Click += new System.EventHandler(this.CheckIntegrity_Click);
            // 
            // m_tsbTR
            // 
            m_tsbTR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbTR.Image = global::DGD.HubGovernor.Properties.Resources.tr_32;
            m_tsbTR.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbTR.Name = "m_tsbTR";
            m_tsbTR.Size = new System.Drawing.Size(36, 36);
            m_tsbTR.Text = "Thomson Reuters";
            m_tsbTR.Click += new System.EventHandler(this.TR_Click);
            // 
            // m_tsbUpdate
            // 
            m_tsbUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbUpdate.Image = global::DGD.HubGovernor.Properties.Resources.deploy_32;
            m_tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbUpdate.Name = "m_tsbUpdate";
            m_tsbUpdate.Size = new System.Drawing.Size(36, 36);
            m_tsbUpdate.Text = "MAJ";
            m_tsbUpdate.Click += new System.EventHandler(this.Update_Click);
            // 
            // m_tsbRepository
            // 
            m_tsbRepository.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbRepository.Image = global::DGD.HubGovernor.Properties.Resources.database_table_32;
            m_tsbRepository.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_tsbRepository.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbRepository.Name = "m_tsbRepository";
            m_tsbRepository.Size = new System.Drawing.Size(36, 36);
            m_tsbRepository.Text = "Tables";
            m_tsbRepository.Click += new System.EventHandler(this.Repository_Click);
            // 
            // m_tsbClientWindow
            // 
            m_tsbClientWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbClientWindow.Image = global::DGD.HubGovernor.Properties.Resources.profil_32;
            m_tsbClientWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbClientWindow.Name = "m_tsbClientWindow";
            m_tsbClientWindow.Size = new System.Drawing.Size(36, 36);
            m_tsbClientWindow.Text = "Gestionnaire des clients";
            m_tsbClientWindow.Click += new System.EventHandler(this.ClientWindow_Click);
            // 
            // m_tsbSysLog
            // 
            m_tsbSysLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbSysLog.Image = global::DGD.HubGovernor.Properties.Resources.syslog_32;
            m_tsbSysLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbSysLog.Name = "m_tsbSysLog";
            m_tsbSysLog.Size = new System.Drawing.Size(36, 36);
            m_tsbSysLog.Text = "Journal des événements système";
            m_tsbSysLog.Click += new System.EventHandler(this.SysLog_Click);
            // 
            // m_tsbSettings
            // 
            m_tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbSettings.Image = global::DGD.HubGovernor.Properties.Resources.settings_mixer_32;
            m_tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbSettings.Name = "m_tsbSettings";
            m_tsbSettings.Size = new System.Drawing.Size(36, 36);
            m_tsbSettings.Text = "Paramètres";
            m_tsbSettings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // m_toolStrip
            // 
            this.m_toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbMainMenu,
            m_tsbTR,
            m_tsbUpdate,
            m_tsbRepository,
            this.toolStripSeparator1,
            toolStripSeparator3,
            m_tsbClientWindow,
            toolStripSeparator2,
            m_tsbSysLog,
            this.m_tsbLogView,
            toolStripSeparator4,
            m_tsbBackup,
            toolStripSeparator5,
            m_tsbSettings});
            this.m_toolStrip.Location = new System.Drawing.Point(0, 0);
            this.m_toolStrip.Name = "m_toolStrip";
            this.m_toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.m_toolStrip.Size = new System.Drawing.Size(509, 39);
            this.m_toolStrip.TabIndex = 0;
            // 
            // m_tsbMainMenu
            // 
            this.m_tsbMainMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_tsbMainMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbMainMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.afficherLeJournalToolStripMenuItem,
            this.m_mpAdministration,
            this.toolStripMenuItem2,
            this.quitterToolStripMenuItem});
            this.m_tsbMainMenu.Enabled = false;
            this.m_tsbMainMenu.Image = global::DGD.HubGovernor.Properties.Resources.menu_32;
            this.m_tsbMainMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbMainMenu.Name = "m_tsbMainMenu";
            this.m_tsbMainMenu.Size = new System.Drawing.Size(45, 36);
            this.m_tsbMainMenu.Text = "Menu principal";
            // 
            // afficherLeJournalToolStripMenuItem
            // 
            this.afficherLeJournalToolStripMenuItem.Name = "afficherLeJournalToolStripMenuItem";
            this.afficherLeJournalToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.afficherLeJournalToolStripMenuItem.Text = "Afficher le journal";
            // 
            // m_mpAdministration
            // 
            this.m_mpAdministration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_miCheckIntegrity});
            this.m_mpAdministration.Name = "m_mpAdministration";
            this.m_mpAdministration.Size = new System.Drawing.Size(168, 22);
            this.m_mpAdministration.Text = "Administration";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 6);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // m_tsbLogView
            // 
            this.m_tsbLogView.Checked = true;
            this.m_tsbLogView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_tsbLogView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbLogView.Image = global::DGD.HubGovernor.Properties.Resources.logviewer_32;
            this.m_tsbLogView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbLogView.Name = "m_tsbLogView";
            this.m_tsbLogView.Size = new System.Drawing.Size(36, 36);
            this.m_tsbLogView.Text = "Afficher / Masquer la fenêtre du journal partial";
            this.m_tsbLogView.Click += new System.EventHandler(this.LogView_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 45);
            this.Controls.Add(this.m_toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Hub Governor";
            this.m_toolStrip.ResumeLayout(false);
            this.m_toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripDropDownButton m_tsbMainMenu;
        private System.Windows.Forms.ToolStrip m_toolStrip;
        private System.Windows.Forms.ToolStripMenuItem afficherLeJournalToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem m_mpAdministration;
        private System.Windows.Forms.ToolStripButton m_tsbLogView;
    }
}

