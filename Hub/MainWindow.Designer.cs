namespace DGD.Hub
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
            System.Windows.Forms.MenuStrip m_mainMenu;
            System.Windows.Forms.ToolStripMenuItem m_mpFile;
            System.Windows.Forms.ToolStripMenuItem m_miQuit;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.m_tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.m_tsbJetSki = new System.Windows.Forms.ToolStripButton();
            this.m_tsbRangeValue = new System.Windows.Forms.ToolStripButton();
            this.m_tsbQuad = new System.Windows.Forms.ToolStripButton();
            this.m_tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.m_mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.m_tsbSpotView = new System.Windows.Forms.ToolStripButton();
            this.m_tsbMachinery = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.m_viewsPanel = new System.Windows.Forms.Panel();
            m_mainMenu = new System.Windows.Forms.MenuStrip();
            m_mpFile = new System.Windows.Forms.ToolStripMenuItem();
            m_miQuit = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            m_mainMenu.SuspendLayout();
            this.m_mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_mainMenu
            // 
            m_mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_mpFile});
            m_mainMenu.Location = new System.Drawing.Point(0, 0);
            m_mainMenu.Name = "m_mainMenu";
            m_mainMenu.Size = new System.Drawing.Size(801, 24);
            m_mainMenu.TabIndex = 0;
            // 
            // m_mpFile
            // 
            m_mpFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_miQuit});
            m_mpFile.Name = "m_mpFile";
            m_mpFile.Size = new System.Drawing.Size(54, 20);
            m_mpFile.Text = "&Fichier";
            // 
            // m_miQuit
            // 
            m_miQuit.Name = "m_miQuit";
            m_miQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            m_miQuit.Size = new System.Drawing.Size(157, 22);
            m_miQuit.Text = "&Quitter";
            m_miQuit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(50, 6);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(50, 6);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(50, 6);
            // 
            // m_tsbHelp
            // 
            this.m_tsbHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbHelp.Image = global::DGD.Hub.Properties.Resources.help_48;
            this.m_tsbHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbHelp.Name = "m_tsbHelp";
            this.m_tsbHelp.Size = new System.Drawing.Size(50, 52);
            this.m_tsbHelp.Text = "Aide";
            this.m_tsbHelp.Click += new System.EventHandler(this.SetView_Handler);
            // 
            // m_tsbSettings
            // 
            this.m_tsbSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.m_tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbSettings.Image = global::DGD.Hub.Properties.Resources.setting_48;
            this.m_tsbSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbSettings.Name = "m_tsbSettings";
            this.m_tsbSettings.Size = new System.Drawing.Size(50, 52);
            this.m_tsbSettings.Text = "Réglages";
            this.m_tsbSettings.Click += new System.EventHandler(this.SetView_Handler);
            // 
            // m_tsbJetSki
            // 
            this.m_tsbJetSki.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbJetSki.Image = global::DGD.Hub.Properties.Resources.jet_ski_48;
            this.m_tsbJetSki.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbJetSki.Name = "m_tsbJetSki";
            this.m_tsbJetSki.Size = new System.Drawing.Size(50, 52);
            this.m_tsbJetSki.Text = "Jet-ski";
            this.m_tsbJetSki.Click += new System.EventHandler(this.SetView_Handler);
            // 
            // m_tsbRangeValue
            // 
            this.m_tsbRangeValue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbRangeValue.Image = global::DGD.Hub.Properties.Resources.range_val_48;
            this.m_tsbRangeValue.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tsbRangeValue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbRangeValue.Name = "m_tsbRangeValue";
            this.m_tsbRangeValue.Size = new System.Drawing.Size(50, 52);
            this.m_tsbRangeValue.Text = "Parcourir les fourchettes de valeurs";
            this.m_tsbRangeValue.Click += new System.EventHandler(this.SetView_Handler);
            // 
            // m_tsbQuad
            // 
            this.m_tsbQuad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbQuad.Image = global::DGD.Hub.Properties.Resources.quad_48;
            this.m_tsbQuad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbQuad.Name = "m_tsbQuad";
            this.m_tsbQuad.Size = new System.Drawing.Size(50, 52);
            this.m_tsbQuad.Text = "Quads";
            this.m_tsbQuad.Click += new System.EventHandler(this.SetView_Handler);
            // 
            // m_tsbAbout
            // 
            this.m_tsbAbout.Checked = true;
            this.m_tsbAbout.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbAbout.Image = global::DGD.Hub.Properties.Resources.about_hub_48;
            this.m_tsbAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbAbout.Name = "m_tsbAbout";
            this.m_tsbAbout.Size = new System.Drawing.Size(50, 52);
            this.m_tsbAbout.Text = "A props de cette application";
            this.m_tsbAbout.Click += new System.EventHandler(this.SetView_Handler);
            // 
            // m_mainToolStrip
            // 
            this.m_mainToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_mainToolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.m_mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbAbout,
            toolStripSeparator1,
            this.m_tsbSpotView,
            this.m_tsbRangeValue,
            toolStripSeparator2,
            this.m_tsbMachinery,
            this.toolStripButton2,
            this.m_tsbQuad,
            this.m_tsbJetSki,
            toolStripSeparator3,
            this.m_tsbHelp,
            this.m_tsbSettings});
            this.m_mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.m_mainToolStrip.Name = "m_mainToolStrip";
            this.m_mainToolStrip.Size = new System.Drawing.Size(53, 470);
            this.m_mainToolStrip.TabIndex = 1;
            // 
            // m_tsbSpotView
            // 
            this.m_tsbSpotView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbSpotView.Image = global::DGD.Hub.Properties.Resources.spot_value_48;
            this.m_tsbSpotView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tsbSpotView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbSpotView.Name = "m_tsbSpotView";
            this.m_tsbSpotView.Size = new System.Drawing.Size(50, 52);
            this.m_tsbSpotView.Text = "Trouver une valeur boursière";
            this.m_tsbSpotView.Click += new System.EventHandler(this.SetView_Handler);
            // 
            // m_tsbMachinery
            // 
            this.m_tsbMachinery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbMachinery.Image = global::DGD.Hub.Properties.Resources.btp_48;
            this.m_tsbMachinery.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tsbMachinery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbMachinery.Name = "m_tsbMachinery";
            this.m_tsbMachinery.Size = new System.Drawing.Size(50, 52);
            this.m_tsbMachinery.Text = "Engins BTP";
            this.m_tsbMachinery.Click += new System.EventHandler(this.SetView_Handler);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(50, 4);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // m_viewsPanel
            // 
            this.m_viewsPanel.AutoScroll = true;
            this.m_viewsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_viewsPanel.Location = new System.Drawing.Point(53, 24);
            this.m_viewsPanel.Name = "m_viewsPanel";
            this.m_viewsPanel.Size = new System.Drawing.Size(748, 470);
            this.m_viewsPanel.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(801, 494);
            this.Controls.Add(this.m_viewsPanel);
            this.Controls.Add(this.m_mainToolStrip);
            this.Controls.Add(m_mainMenu);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = m_mainMenu;
            this.Name = "MainWindow";
            this.Text = "Hub de la valeur en douane";
            m_mainMenu.ResumeLayout(false);
            m_mainMenu.PerformLayout();
            this.m_mainToolStrip.ResumeLayout(false);
            this.m_mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbMachinery;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton m_tsbSpotView;
        private System.Windows.Forms.Panel m_viewsPanel;
        private System.Windows.Forms.ToolStrip m_mainToolStrip;
        private System.Windows.Forms.ToolStripButton m_tsbAbout;
        private System.Windows.Forms.ToolStripButton m_tsbRangeValue;
        private System.Windows.Forms.ToolStripButton m_tsbQuad;
        private System.Windows.Forms.ToolStripButton m_tsbJetSki;
        private System.Windows.Forms.ToolStripButton m_tsbHelp;
        private System.Windows.Forms.ToolStripButton m_tsbSettings;
    }
}

