namespace DGD.HubGovernor.Arch
{
    partial class BackupWindow
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
            System.Windows.Forms.Panel m_navPanel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupWindow));
            System.Windows.Forms.LinkLabel m_lblHelp;
            System.Windows.Forms.LinkLabel m_lblPlanning;
            System.Windows.Forms.LinkLabel m_lblRestore;
            System.Windows.Forms.LinkLabel m_lblBackup;
            this.m_pagesPanel = new System.Windows.Forms.Panel();
            m_navPanel = new System.Windows.Forms.Panel();
            m_lblHelp = new System.Windows.Forms.LinkLabel();
            m_lblPlanning = new System.Windows.Forms.LinkLabel();
            m_lblRestore = new System.Windows.Forms.LinkLabel();
            m_lblBackup = new System.Windows.Forms.LinkLabel();
            m_navPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_navPanel
            // 
            m_navPanel.AutoScroll = true;
            m_navPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_navPanel.BackgroundImage")));
            m_navPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            m_navPanel.Controls.Add(m_lblHelp);
            m_navPanel.Controls.Add(m_lblPlanning);
            m_navPanel.Controls.Add(m_lblRestore);
            m_navPanel.Controls.Add(m_lblBackup);
            m_navPanel.Dock = System.Windows.Forms.DockStyle.Left;
            m_navPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_navPanel.Location = new System.Drawing.Point(0, 0);
            m_navPanel.Name = "m_navPanel";
            m_navPanel.Size = new System.Drawing.Size(197, 355);
            m_navPanel.TabIndex = 0;
            // 
            // m_lblHelp
            // 
            m_lblHelp.ActiveLinkColor = System.Drawing.Color.Blue;
            m_lblHelp.BackColor = System.Drawing.Color.Transparent;
            m_lblHelp.Image = global::DGD.HubGovernor.Properties.Resources.help_32;
            m_lblHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblHelp.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            m_lblHelp.Location = new System.Drawing.Point(15, 280);
            m_lblHelp.Name = "m_lblHelp";
            m_lblHelp.Size = new System.Drawing.Size(70, 42);
            m_lblHelp.TabIndex = 6;
            m_lblHelp.TabStop = true;
            m_lblHelp.Text = "Aide";
            m_lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblPlanning
            // 
            m_lblPlanning.ActiveLinkColor = System.Drawing.Color.Blue;
            m_lblPlanning.BackColor = System.Drawing.Color.Transparent;
            m_lblPlanning.Enabled = false;
            m_lblPlanning.Image = global::DGD.HubGovernor.Properties.Resources.backup_timer_32;
            m_lblPlanning.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblPlanning.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            m_lblPlanning.Location = new System.Drawing.Point(12, 155);
            m_lblPlanning.Name = "m_lblPlanning";
            m_lblPlanning.Size = new System.Drawing.Size(171, 55);
            m_lblPlanning.TabIndex = 5;
            m_lblPlanning.TabStop = true;
            m_lblPlanning.Text = "Planification des sauvegardes";
            m_lblPlanning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_lblRestore
            // 
            m_lblRestore.ActiveLinkColor = System.Drawing.Color.Blue;
            m_lblRestore.BackColor = System.Drawing.Color.Transparent;
            m_lblRestore.Image = global::DGD.HubGovernor.Properties.Resources.restore_321;
            m_lblRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblRestore.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            m_lblRestore.Location = new System.Drawing.Point(12, 95);
            m_lblRestore.Name = "m_lblRestore";
            m_lblRestore.Size = new System.Drawing.Size(112, 30);
            m_lblRestore.TabIndex = 4;
            m_lblRestore.TabStop = true;
            m_lblRestore.Text = "Restauration";
            m_lblRestore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            m_lblRestore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Restore_LinkClicked);
            // 
            // m_lblBackup
            // 
            m_lblBackup.ActiveLinkColor = System.Drawing.Color.Blue;
            m_lblBackup.BackColor = System.Drawing.Color.Transparent;
            m_lblBackup.Image = ((System.Drawing.Image)(resources.GetObject("m_lblBackup.Image")));
            m_lblBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblBackup.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            m_lblBackup.Location = new System.Drawing.Point(12, 35);
            m_lblBackup.Name = "m_lblBackup";
            m_lblBackup.Size = new System.Drawing.Size(112, 30);
            m_lblBackup.TabIndex = 3;
            m_lblBackup.TabStop = true;
            m_lblBackup.Text = "Sauvegarde";
            m_lblBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            m_lblBackup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Backup_LinkClicked);
            // 
            // m_pagesPanel
            // 
            this.m_pagesPanel.AutoScroll = true;
            this.m_pagesPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.m_pagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pagesPanel.Location = new System.Drawing.Point(197, 0);
            this.m_pagesPanel.Name = "m_pagesPanel";
            this.m_pagesPanel.Size = new System.Drawing.Size(525, 355);
            this.m_pagesPanel.TabIndex = 1;
            // 
            // BackupWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(722, 355);
            this.Controls.Add(this.m_pagesPanel);
            this.Controls.Add(m_navPanel);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BackupWindow";
            this.Text = "Sauvegarde";
            m_navPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel m_pagesPanel;
    }
}