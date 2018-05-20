namespace DGD.HubGovernor.Opts
{
    partial class ConnectionSettingsPage
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
            System.Windows.Forms.Label m_lblPassword;
            System.Windows.Forms.Label m_lblUserName;
            System.Windows.Forms.Label m_lblServerURL;
            System.Windows.Forms.PictureBox m_pbLogo;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionSettingsPage));
            System.Windows.Forms.GroupBox m_groupBox;
            System.Windows.Forms.Label m_lblProxyPort;
            System.Windows.Forms.Label m_lblProxyHost;
            this.m_tbProxyHost = new System.Windows.Forms.TextBox();
            this.m_chkEnableProxy = new System.Windows.Forms.CheckBox();
            this.m_tbPassword = new System.Windows.Forms.TextBox();
            this.m_tbUserName = new System.Windows.Forms.TextBox();
            this.m_tbServerURL = new System.Windows.Forms.TextBox();
            this.m_nudProxyPort = new System.Windows.Forms.NumericUpDown();
            this.m_btnDetectProxy = new System.Windows.Forms.Button();
            m_lblPassword = new System.Windows.Forms.Label();
            m_lblUserName = new System.Windows.Forms.Label();
            m_lblServerURL = new System.Windows.Forms.Label();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            m_groupBox = new System.Windows.Forms.GroupBox();
            m_lblProxyPort = new System.Windows.Forms.Label();
            m_lblProxyHost = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            m_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudProxyPort)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblPassword
            // 
            m_lblPassword.AutoSize = true;
            m_lblPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblPassword.Location = new System.Drawing.Point(14, 170);
            m_lblPassword.Name = "m_lblPassword";
            m_lblPassword.Size = new System.Drawing.Size(75, 13);
            m_lblPassword.TabIndex = 13;
            m_lblPassword.Text = "Mot de Passe:";
            m_lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblUserName
            // 
            m_lblUserName.AutoSize = true;
            m_lblUserName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblUserName.Location = new System.Drawing.Point(14, 131);
            m_lblUserName.Name = "m_lblUserName";
            m_lblUserName.Size = new System.Drawing.Size(56, 13);
            m_lblUserName.TabIndex = 11;
            m_lblUserName.Text = "Utilisateur:";
            m_lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblServerURL
            // 
            m_lblServerURL.AutoSize = true;
            m_lblServerURL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblServerURL.Location = new System.Drawing.Point(14, 99);
            m_lblServerURL.Name = "m_lblServerURL";
            m_lblServerURL.Size = new System.Drawing.Size(47, 13);
            m_lblServerURL.TabIndex = 9;
            m_lblServerURL.Text = "Serveur:";
            m_lblServerURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_pbLogo
            // 
            m_pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            m_pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("m_pbLogo.Image")));
            m_pbLogo.Location = new System.Drawing.Point(273, 12);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(223, 52);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pbLogo.TabIndex = 15;
            m_pbLogo.TabStop = false;
            // 
            // m_groupBox
            // 
            m_groupBox.Controls.Add(this.m_btnDetectProxy);
            m_groupBox.Controls.Add(this.m_nudProxyPort);
            m_groupBox.Controls.Add(m_lblProxyPort);
            m_groupBox.Controls.Add(this.m_tbProxyHost);
            m_groupBox.Controls.Add(m_lblProxyHost);
            m_groupBox.Controls.Add(this.m_chkEnableProxy);
            m_groupBox.Location = new System.Drawing.Point(17, 214);
            m_groupBox.Name = "m_groupBox";
            m_groupBox.Size = new System.Drawing.Size(356, 159);
            m_groupBox.TabIndex = 16;
            m_groupBox.TabStop = false;
            m_groupBox.Text = " Serveur proxy ";
            // 
            // m_lblProxyPort
            // 
            m_lblProxyPort.AutoSize = true;
            m_lblProxyPort.Location = new System.Drawing.Point(3, 88);
            m_lblProxyPort.Name = "m_lblProxyPort";
            m_lblProxyPort.Size = new System.Drawing.Size(29, 13);
            m_lblProxyPort.TabIndex = 3;
            m_lblProxyPort.Text = "Port:";
            // 
            // m_tbProxyHost
            // 
            this.m_tbProxyHost.Enabled = false;
            this.m_tbProxyHost.Location = new System.Drawing.Point(58, 58);
            this.m_tbProxyHost.Name = "m_tbProxyHost";
            this.m_tbProxyHost.Size = new System.Drawing.Size(193, 20);
            this.m_tbProxyHost.TabIndex = 2;
            // 
            // m_lblProxyHost
            // 
            m_lblProxyHost.AutoSize = true;
            m_lblProxyHost.Location = new System.Drawing.Point(3, 62);
            m_lblProxyHost.Name = "m_lblProxyHost";
            m_lblProxyHost.Size = new System.Drawing.Size(48, 13);
            m_lblProxyHost.TabIndex = 1;
            m_lblProxyHost.Text = "Adresse:";
            // 
            // m_chkEnableProxy
            // 
            this.m_chkEnableProxy.AutoSize = true;
            this.m_chkEnableProxy.Location = new System.Drawing.Point(6, 33);
            this.m_chkEnableProxy.Name = "m_chkEnableProxy";
            this.m_chkEnableProxy.Size = new System.Drawing.Size(280, 17);
            this.m_chkEnableProxy.TabIndex = 0;
            this.m_chkEnableProxy.Text = "Je me connecte à Internet via un serveur proxy HTTP";
            this.m_chkEnableProxy.UseVisualStyleBackColor = true;
            this.m_chkEnableProxy.CheckedChanged += new System.EventHandler(this.EnableProxy_CheckedChanged);
            // 
            // m_tbPassword
            // 
            this.m_tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbPassword.Location = new System.Drawing.Point(99, 166);
            this.m_tbPassword.Name = "m_tbPassword";
            this.m_tbPassword.Size = new System.Drawing.Size(274, 20);
            this.m_tbPassword.TabIndex = 14;
            this.m_tbPassword.UseSystemPasswordChar = true;
            // 
            // m_tbUserName
            // 
            this.m_tbUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbUserName.Location = new System.Drawing.Point(99, 127);
            this.m_tbUserName.Name = "m_tbUserName";
            this.m_tbUserName.Size = new System.Drawing.Size(274, 20);
            this.m_tbUserName.TabIndex = 12;
            // 
            // m_tbServerURL
            // 
            this.m_tbServerURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbServerURL.Location = new System.Drawing.Point(99, 95);
            this.m_tbServerURL.Name = "m_tbServerURL";
            this.m_tbServerURL.Size = new System.Drawing.Size(360, 20);
            this.m_tbServerURL.TabIndex = 10;
            // 
            // m_nudProxyPort
            // 
            this.m_nudProxyPort.Enabled = false;
            this.m_nudProxyPort.Location = new System.Drawing.Point(58, 84);
            this.m_nudProxyPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.m_nudProxyPort.Name = "m_nudProxyPort";
            this.m_nudProxyPort.Size = new System.Drawing.Size(66, 20);
            this.m_nudProxyPort.TabIndex = 4;
            this.m_nudProxyPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // m_btnDetectProxy
            // 
            this.m_btnDetectProxy.Location = new System.Drawing.Point(166, 130);
            this.m_btnDetectProxy.Name = "m_btnDetectProxy";
            this.m_btnDetectProxy.Size = new System.Drawing.Size(180, 23);
            this.m_btnDetectProxy.TabIndex = 5;
            this.m_btnDetectProxy.Text = "Détecter les paramètres du proxy";
            this.m_btnDetectProxy.UseVisualStyleBackColor = true;
            this.m_btnDetectProxy.Click += new System.EventHandler(this.DetectProxy_Click);
            // 
            // ConnectionSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(m_groupBox);
            this.Controls.Add(m_pbLogo);
            this.Controls.Add(this.m_tbPassword);
            this.Controls.Add(m_lblPassword);
            this.Controls.Add(this.m_tbUserName);
            this.Controls.Add(m_lblUserName);
            this.Controls.Add(this.m_tbServerURL);
            this.Controls.Add(m_lblServerURL);
            this.Name = "ConnectionSettingsPage";
            this.Size = new System.Drawing.Size(499, 390);
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            m_groupBox.ResumeLayout(false);
            m_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudProxyPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbPassword;
        private System.Windows.Forms.TextBox m_tbUserName;
        private System.Windows.Forms.TextBox m_tbServerURL;
        private System.Windows.Forms.TextBox m_tbProxyHost;
        private System.Windows.Forms.CheckBox m_chkEnableProxy;
        private System.Windows.Forms.NumericUpDown m_nudProxyPort;
        private System.Windows.Forms.Button m_btnDetectProxy;
    }
}
