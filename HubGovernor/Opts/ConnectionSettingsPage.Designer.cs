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
            this.m_tbPassword = new System.Windows.Forms.TextBox();
            this.m_tbUserName = new System.Windows.Forms.TextBox();
            this.m_tbServerURL = new System.Windows.Forms.TextBox();
            m_lblPassword = new System.Windows.Forms.Label();
            m_lblUserName = new System.Windows.Forms.Label();
            m_lblServerURL = new System.Windows.Forms.Label();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblPassword
            // 
            m_lblPassword.Image = global::DGD.HubGovernor.Properties.Resources.password;
            m_lblPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblPassword.Location = new System.Drawing.Point(14, 188);
            m_lblPassword.Name = "m_lblPassword";
            m_lblPassword.Size = new System.Drawing.Size(115, 32);
            m_lblPassword.TabIndex = 13;
            m_lblPassword.Text = "Mot de Passe:";
            m_lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblUserName
            // 
            m_lblUserName.Image = global::DGD.HubGovernor.Properties.Resources.Users;
            m_lblUserName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblUserName.Location = new System.Drawing.Point(14, 138);
            m_lblUserName.Name = "m_lblUserName";
            m_lblUserName.Size = new System.Drawing.Size(97, 32);
            m_lblUserName.TabIndex = 11;
            m_lblUserName.Text = "Utilisateur:";
            m_lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblServerURL
            // 
            m_lblServerURL.Image = global::DGD.HubGovernor.Properties.Resources.url;
            m_lblServerURL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblServerURL.Location = new System.Drawing.Point(14, 88);
            m_lblServerURL.Name = "m_lblServerURL";
            m_lblServerURL.Size = new System.Drawing.Size(88, 32);
            m_lblServerURL.TabIndex = 9;
            m_lblServerURL.Text = "Serveur:";
            m_lblServerURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_pbLogo
            // 
            m_pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            m_pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("m_pbLogo.Image")));
            m_pbLogo.Location = new System.Drawing.Point(274, 12);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(223, 52);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pbLogo.TabIndex = 15;
            m_pbLogo.TabStop = false;
            // 
            // m_tbPassword
            // 
            this.m_tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbPassword.Location = new System.Drawing.Point(136, 195);
            this.m_tbPassword.Name = "m_tbPassword";
            this.m_tbPassword.Size = new System.Drawing.Size(326, 20);
            this.m_tbPassword.TabIndex = 14;
            this.m_tbPassword.UseSystemPasswordChar = true;
            // 
            // m_tbUserName
            // 
            this.m_tbUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbUserName.Location = new System.Drawing.Point(136, 145);
            this.m_tbUserName.Name = "m_tbUserName";
            this.m_tbUserName.Size = new System.Drawing.Size(326, 20);
            this.m_tbUserName.TabIndex = 12;
            // 
            // m_tbServerURL
            // 
            this.m_tbServerURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tbServerURL.Location = new System.Drawing.Point(136, 95);
            this.m_tbServerURL.Name = "m_tbServerURL";
            this.m_tbServerURL.Size = new System.Drawing.Size(361, 20);
            this.m_tbServerURL.TabIndex = 10;
            // 
            // ConnexionSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(m_pbLogo);
            this.Controls.Add(this.m_tbPassword);
            this.Controls.Add(m_lblPassword);
            this.Controls.Add(this.m_tbUserName);
            this.Controls.Add(m_lblUserName);
            this.Controls.Add(this.m_tbServerURL);
            this.Controls.Add(m_lblServerURL);
            this.Name = "ConnexionSettingsPage";
            this.Size = new System.Drawing.Size(500, 256);
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbPassword;
        private System.Windows.Forms.TextBox m_tbUserName;
        private System.Windows.Forms.TextBox m_tbServerURL;
    }
}
