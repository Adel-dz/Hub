namespace DGD.Hub.Opts
{
    partial class SettingsView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox m_grpClientInfo;
            System.Windows.Forms.Label m_lblProfileLabel;
            System.Windows.Forms.Label m_lblEmail;
            System.Windows.Forms.Label m_lblPhone;
            System.Windows.Forms.Label m_lblContact;
            System.Windows.Forms.PictureBox m_pbLogo;
            System.Windows.Forms.GroupBox m_grpProxy;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label m_lblHost;
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_tbEmail = new System.Windows.Forms.TextBox();
            this.m_tbPhone = new System.Windows.Forms.TextBox();
            this.m_lblProfile = new System.Windows.Forms.Label();
            this.m_tbContact = new System.Windows.Forms.TextBox();
            this.m_nudPort = new System.Windows.Forms.NumericUpDown();
            this.m_tbHost = new System.Windows.Forms.TextBox();
            this.m_chkEnableProxy = new System.Windows.Forms.CheckBox();
            this.m_chkDetectProxy = new System.Windows.Forms.CheckBox();
            this.m_chkUseInternalCode = new System.Windows.Forms.CheckBox();
            this.m_grpOtherOpt = new System.Windows.Forms.GroupBox();
            m_grpClientInfo = new System.Windows.Forms.GroupBox();
            m_lblProfileLabel = new System.Windows.Forms.Label();
            m_lblEmail = new System.Windows.Forms.Label();
            m_lblPhone = new System.Windows.Forms.Label();
            m_lblContact = new System.Windows.Forms.Label();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            m_grpProxy = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            m_lblHost = new System.Windows.Forms.Label();
            m_grpClientInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            m_grpProxy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudPort)).BeginInit();
            this.m_grpOtherOpt.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_grpClientInfo
            // 
            m_grpClientInfo.Controls.Add(this.m_btnSave);
            m_grpClientInfo.Controls.Add(this.m_tbEmail);
            m_grpClientInfo.Controls.Add(this.m_tbPhone);
            m_grpClientInfo.Controls.Add(this.m_lblProfile);
            m_grpClientInfo.Controls.Add(this.m_tbContact);
            m_grpClientInfo.Controls.Add(m_lblProfileLabel);
            m_grpClientInfo.Controls.Add(m_lblEmail);
            m_grpClientInfo.Controls.Add(m_lblPhone);
            m_grpClientInfo.Controls.Add(m_lblContact);
            m_grpClientInfo.Location = new System.Drawing.Point(15, 14);
            m_grpClientInfo.Name = "m_grpClientInfo";
            m_grpClientInfo.Size = new System.Drawing.Size(368, 206);
            m_grpClientInfo.TabIndex = 0;
            m_grpClientInfo.TabStop = false;
            m_grpClientInfo.Text = " Infos Utilisateur ";
            // 
            // m_btnSave
            // 
            this.m_btnSave.Enabled = false;
            this.m_btnSave.ForeColor = System.Drawing.Color.SteelBlue;
            this.m_btnSave.Location = new System.Drawing.Point(194, 168);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(140, 23);
            this.m_btnSave.TabIndex = 3;
            this.m_btnSave.Text = "Transmettre au serveur...";
            this.m_btnSave.UseVisualStyleBackColor = true;
            this.m_btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // m_tbEmail
            // 
            this.m_tbEmail.Location = new System.Drawing.Point(121, 122);
            this.m_tbEmail.Name = "m_tbEmail";
            this.m_tbEmail.Size = new System.Drawing.Size(213, 20);
            this.m_tbEmail.TabIndex = 7;
            this.m_tbEmail.TextChanged += new System.EventHandler(this.Input_TextChanged);
            // 
            // m_tbPhone
            // 
            this.m_tbPhone.Location = new System.Drawing.Point(121, 93);
            this.m_tbPhone.Name = "m_tbPhone";
            this.m_tbPhone.Size = new System.Drawing.Size(213, 20);
            this.m_tbPhone.TabIndex = 6;
            this.m_tbPhone.TextChanged += new System.EventHandler(this.Input_TextChanged);
            // 
            // m_lblProfile
            // 
            this.m_lblProfile.AutoSize = true;
            this.m_lblProfile.Location = new System.Drawing.Point(121, 40);
            this.m_lblProfile.Name = "m_lblProfile";
            this.m_lblProfile.Size = new System.Drawing.Size(10, 13);
            this.m_lblProfile.TabIndex = 5;
            this.m_lblProfile.Text = "-";
            // 
            // m_tbContact
            // 
            this.m_tbContact.Location = new System.Drawing.Point(121, 64);
            this.m_tbContact.Name = "m_tbContact";
            this.m_tbContact.Size = new System.Drawing.Size(213, 20);
            this.m_tbContact.TabIndex = 4;
            this.m_tbContact.TextChanged += new System.EventHandler(this.Input_TextChanged);
            // 
            // m_lblProfileLabel
            // 
            m_lblProfileLabel.AutoSize = true;
            m_lblProfileLabel.Location = new System.Drawing.Point(6, 40);
            m_lblProfileLabel.Name = "m_lblProfileLabel";
            m_lblProfileLabel.Size = new System.Drawing.Size(30, 13);
            m_lblProfileLabel.TabIndex = 3;
            m_lblProfileLabel.Text = "Profil";
            // 
            // m_lblEmail
            // 
            m_lblEmail.AutoSize = true;
            m_lblEmail.Location = new System.Drawing.Point(6, 126);
            m_lblEmail.Name = "m_lblEmail";
            m_lblEmail.Size = new System.Drawing.Size(35, 13);
            m_lblEmail.TabIndex = 2;
            m_lblEmail.Text = "E-mail";
            // 
            // m_lblPhone
            // 
            m_lblPhone.AutoSize = true;
            m_lblPhone.Location = new System.Drawing.Point(6, 97);
            m_lblPhone.Name = "m_lblPhone";
            m_lblPhone.Size = new System.Drawing.Size(109, 13);
            m_lblPhone.TabIndex = 1;
            m_lblPhone.Text = "Numéro de téléphone";
            // 
            // m_lblContact
            // 
            m_lblContact.AutoSize = true;
            m_lblContact.Location = new System.Drawing.Point(6, 68);
            m_lblContact.Name = "m_lblContact";
            m_lblContact.Size = new System.Drawing.Size(44, 13);
            m_lblContact.TabIndex = 0;
            m_lblContact.Text = "Contact";
            // 
            // m_pbLogo
            // 
            m_pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_pbLogo.Image = global::DGD.Hub.Properties.Resources.settings_256;
            m_pbLogo.Location = new System.Drawing.Point(390, 3);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(522, 481);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            m_pbLogo.TabIndex = 2;
            m_pbLogo.TabStop = false;
            // 
            // m_grpProxy
            // 
            m_grpProxy.Controls.Add(this.m_nudPort);
            m_grpProxy.Controls.Add(label1);
            m_grpProxy.Controls.Add(this.m_tbHost);
            m_grpProxy.Controls.Add(m_lblHost);
            m_grpProxy.Controls.Add(this.m_chkEnableProxy);
            m_grpProxy.Controls.Add(this.m_chkDetectProxy);
            m_grpProxy.Location = new System.Drawing.Point(15, 226);
            m_grpProxy.Name = "m_grpProxy";
            m_grpProxy.Size = new System.Drawing.Size(368, 156);
            m_grpProxy.TabIndex = 4;
            m_grpProxy.TabStop = false;
            m_grpProxy.Text = " Proxy ";
            // 
            // m_nudPort
            // 
            this.m_nudPort.Enabled = false;
            this.m_nudPort.Location = new System.Drawing.Point(90, 117);
            this.m_nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.m_nudPort.Name = "m_nudPort";
            this.m_nudPort.Size = new System.Drawing.Size(67, 20);
            this.m_nudPort.TabIndex = 5;
            this.m_nudPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(7, 121);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(29, 13);
            label1.TabIndex = 4;
            label1.Text = "Port:";
            // 
            // m_tbHost
            // 
            this.m_tbHost.Enabled = false;
            this.m_tbHost.Location = new System.Drawing.Point(90, 84);
            this.m_tbHost.Name = "m_tbHost";
            this.m_tbHost.Size = new System.Drawing.Size(189, 20);
            this.m_tbHost.TabIndex = 3;
            // 
            // m_lblHost
            // 
            m_lblHost.AutoSize = true;
            m_lblHost.Location = new System.Drawing.Point(7, 88);
            m_lblHost.Name = "m_lblHost";
            m_lblHost.Size = new System.Drawing.Size(76, 13);
            m_lblHost.TabIndex = 2;
            m_lblHost.Text = "Adresse proxy:";
            // 
            // m_chkEnableProxy
            // 
            this.m_chkEnableProxy.AutoSize = true;
            this.m_chkEnableProxy.Location = new System.Drawing.Point(7, 60);
            this.m_chkEnableProxy.Name = "m_chkEnableProxy";
            this.m_chkEnableProxy.Size = new System.Drawing.Size(280, 17);
            this.m_chkEnableProxy.TabIndex = 1;
            this.m_chkEnableProxy.Text = "Je me connecte à Internet via un serveur proxy HTTP";
            this.m_chkEnableProxy.UseVisualStyleBackColor = true;
            this.m_chkEnableProxy.CheckedChanged += new System.EventHandler(this.EnableProxy_CheckedChanged);
            // 
            // m_chkDetectProxy
            // 
            this.m_chkDetectProxy.AutoSize = true;
            this.m_chkDetectProxy.Checked = true;
            this.m_chkDetectProxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkDetectProxy.Location = new System.Drawing.Point(7, 20);
            this.m_chkDetectProxy.Name = "m_chkDetectProxy";
            this.m_chkDetectProxy.Size = new System.Drawing.Size(287, 17);
            this.m_chkDetectProxy.TabIndex = 0;
            this.m_chkDetectProxy.Text = "Détecter les paramètres du serveur proxy au démarrage";
            this.m_chkDetectProxy.UseVisualStyleBackColor = true;
            // 
            // m_chkUseInternalCode
            // 
            this.m_chkUseInternalCode.AutoSize = true;
            this.m_chkUseInternalCode.Location = new System.Drawing.Point(9, 29);
            this.m_chkUseInternalCode.Name = "m_chkUseInternalCode";
            this.m_chkUseInternalCode.Size = new System.Drawing.Size(255, 17);
            this.m_chkUseInternalCode.TabIndex = 1;
            this.m_chkUseInternalCode.Text = "Utiliser les codes pays au lieu des noms de pays.";
            this.m_chkUseInternalCode.UseVisualStyleBackColor = true;
            // 
            // m_grpOtherOpt
            // 
            this.m_grpOtherOpt.Controls.Add(this.m_chkUseInternalCode);
            this.m_grpOtherOpt.Location = new System.Drawing.Point(15, 392);
            this.m_grpOtherOpt.Name = "m_grpOtherOpt";
            this.m_grpOtherOpt.Size = new System.Drawing.Size(368, 63);
            this.m_grpOtherOpt.TabIndex = 3;
            this.m_grpOtherOpt.TabStop = false;
            this.m_grpOtherOpt.Text = " Autres paramètres ";
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(m_grpProxy);
            this.Controls.Add(this.m_grpOtherOpt);
            this.Controls.Add(m_pbLogo);
            this.Controls.Add(m_grpClientInfo);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(915, 487);
            m_grpClientInfo.ResumeLayout(false);
            m_grpClientInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            m_grpProxy.ResumeLayout(false);
            m_grpProxy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudPort)).EndInit();
            this.m_grpOtherOpt.ResumeLayout(false);
            this.m_grpOtherOpt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbEmail;
        private System.Windows.Forms.TextBox m_tbPhone;
        private System.Windows.Forms.Label m_lblProfile;
        private System.Windows.Forms.TextBox m_tbContact;
        private System.Windows.Forms.CheckBox m_chkUseInternalCode;
        private System.Windows.Forms.Button m_btnSave;
        private System.Windows.Forms.GroupBox m_grpOtherOpt;
        private System.Windows.Forms.NumericUpDown m_nudPort;
        private System.Windows.Forms.TextBox m_tbHost;
        private System.Windows.Forms.CheckBox m_chkEnableProxy;
        private System.Windows.Forms.CheckBox m_chkDetectProxy;
    }
}
