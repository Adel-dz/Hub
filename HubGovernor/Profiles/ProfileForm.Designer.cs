namespace DGD.HubGovernor.Profiles
{
    partial class ProfileForm
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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripButton m_tsbSettings;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.Windows.Forms.Label m_lblName;
            System.Windows.Forms.Label m_lblPrivilege;
            System.Windows.Forms.Label m_lbMsg;
            System.Windows.Forms.GroupBox m_grpSep;
            System.Windows.Forms.PictureBox m_pbLogo;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileForm));
            this.m_tsbSave = new System.Windows.Forms.ToolStripButton();
            this.m_tsbReload = new System.Windows.Forms.ToolStripButton();
            this.m_tbProfileName = new System.Windows.Forms.TextBox();
            this.m_cbPrivilege = new System.Windows.Forms.ComboBox();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbSettings = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_lblName = new System.Windows.Forms.Label();
            m_lblPrivilege = new System.Windows.Forms.Label();
            m_lbMsg = new System.Windows.Forms.Label();
            m_grpSep = new System.Windows.Forms.GroupBox();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            m_toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbSave,
            toolStripSeparator1,
            this.m_tsbReload,
            toolStripSeparator2,
            m_tsbSettings,
            m_tsbHelp});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(399, 25);
            m_toolStrip.TabIndex = 0;
            // 
            // m_tsbSave
            // 
            this.m_tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbSave.Image = global::DGD.HubGovernor.Properties.Resources.save_16;
            this.m_tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbSave.Name = "m_tsbSave";
            this.m_tsbSave.Size = new System.Drawing.Size(23, 22);
            this.m_tsbSave.Text = "Enregistrer";
            this.m_tsbSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbReload
            // 
            this.m_tsbReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbReload.Image = global::DGD.HubGovernor.Properties.Resources.refresh_16;
            this.m_tsbReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbReload.Name = "m_tsbReload";
            this.m_tsbReload.Size = new System.Drawing.Size(23, 22);
            this.m_tsbReload.Text = "Recharger";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbSettings
            // 
            m_tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbSettings.Image = global::DGD.HubGovernor.Properties.Resources.option_16;
            m_tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbSettings.Name = "m_tsbSettings";
            m_tsbSettings.Size = new System.Drawing.Size(23, 22);
            m_tsbSettings.Text = "Paramètres";
            m_tsbSettings.Click += new System.EventHandler(this.Settings_Click);
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
            // m_lblName
            // 
            m_lblName.AutoSize = true;
            m_lblName.Location = new System.Drawing.Point(96, 44);
            m_lblName.Name = "m_lblName";
            m_lblName.Size = new System.Drawing.Size(82, 13);
            m_lblName.TabIndex = 2;
            m_lblName.Text = "Nom du profile:*";
            // 
            // m_lblPrivilege
            // 
            m_lblPrivilege.AutoSize = true;
            m_lblPrivilege.Location = new System.Drawing.Point(96, 80);
            m_lblPrivilege.Name = "m_lblPrivilege";
            m_lblPrivilege.Size = new System.Drawing.Size(54, 13);
            m_lblPrivilege.TabIndex = 4;
            m_lblPrivilege.Text = "Privilège:*";
            // 
            // m_lbMsg
            // 
            m_lbMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            m_lbMsg.AutoSize = true;
            m_lbMsg.Location = new System.Drawing.Point(19, 143);
            m_lbMsg.Name = "m_lbMsg";
            m_lbMsg.Size = new System.Drawing.Size(300, 13);
            m_lbMsg.TabIndex = 11;
            m_lbMsg.Text = "Les éléments marqués d’un astérisque (*)  doivent êtres servis.";
            // 
            // m_grpSep
            // 
            m_grpSep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_grpSep.Location = new System.Drawing.Point(18, 134);
            m_grpSep.Name = "m_grpSep";
            m_grpSep.Size = new System.Drawing.Size(367, 2);
            m_grpSep.TabIndex = 10;
            m_grpSep.TabStop = false;
            // 
            // m_pbLogo
            // 
            m_pbLogo.Image = global::DGD.HubGovernor.Properties.Resources.profile_64;
            m_pbLogo.Location = new System.Drawing.Point(12, 44);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(64, 64);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pbLogo.TabIndex = 1;
            m_pbLogo.TabStop = false;
            // 
            // m_tbProfileName
            // 
            this.m_tbProfileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbProfileName.Location = new System.Drawing.Point(185, 40);
            this.m_tbProfileName.Name = "m_tbProfileName";
            this.m_tbProfileName.Size = new System.Drawing.Size(192, 20);
            this.m_tbProfileName.TabIndex = 3;
            this.m_tbProfileName.TextChanged += new System.EventHandler(this.ProfileName_TextChanged);
            // 
            // m_cbPrivilege
            // 
            this.m_cbPrivilege.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cbPrivilege.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbPrivilege.FormattingEnabled = true;
            this.m_cbPrivilege.Location = new System.Drawing.Point(185, 76);
            this.m_cbPrivilege.Name = "m_cbPrivilege";
            this.m_cbPrivilege.Size = new System.Drawing.Size(192, 21);
            this.m_cbPrivilege.Sorted = true;
            this.m_cbPrivilege.TabIndex = 5;
            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 168);
            this.Controls.Add(m_lbMsg);
            this.Controls.Add(m_grpSep);
            this.Controls.Add(this.m_cbPrivilege);
            this.Controls.Add(m_lblPrivilege);
            this.Controls.Add(this.m_tbProfileName);
            this.Controls.Add(m_lblName);
            this.Controls.Add(m_pbLogo);
            this.Controls.Add(m_toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(345, 187);
            this.Name = "ProfileForm";
            this.Text = "Profile";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton m_tsbSave;
        private System.Windows.Forms.ToolStripButton m_tsbReload;
        private System.Windows.Forms.TextBox m_tbProfileName;
        private System.Windows.Forms.ComboBox m_cbPrivilege;
    }
}