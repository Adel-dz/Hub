namespace DGD.HubGovernor
{
    partial class CredentialDialog
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
            System.Windows.Forms.Label m_lblPassword;
            System.Windows.Forms.Label m_lblUserName;
            System.Windows.Forms.Label m_lblServerURL;
            System.Windows.Forms.Button m_btnOK;
            System.Windows.Forms.Button m_btnCancel;
            this.m_bServerURL = new System.Windows.Forms.TextBox();
            this.m_tbUserName = new System.Windows.Forms.TextBox();
            this.m_tbPassword = new System.Windows.Forms.TextBox();
            this.m_chkSavePassword = new System.Windows.Forms.CheckBox();
            m_lblPassword = new System.Windows.Forms.Label();
            m_lblUserName = new System.Windows.Forms.Label();
            m_lblServerURL = new System.Windows.Forms.Label();
            m_btnOK = new System.Windows.Forms.Button();
            m_btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblPassword
            // 
            m_lblPassword.Image = global::DGD.HubGovernor.Properties.Resources.password;
            m_lblPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblPassword.Location = new System.Drawing.Point(12, 124);
            m_lblPassword.Name = "m_lblPassword";
            m_lblPassword.Size = new System.Drawing.Size(115, 32);
            m_lblPassword.TabIndex = 7;
            m_lblPassword.Text = "Mot de Passe:";
            m_lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblUserName
            // 
            m_lblUserName.Image = global::DGD.HubGovernor.Properties.Resources.Users;
            m_lblUserName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblUserName.Location = new System.Drawing.Point(12, 74);
            m_lblUserName.Name = "m_lblUserName";
            m_lblUserName.Size = new System.Drawing.Size(97, 32);
            m_lblUserName.TabIndex = 5;
            m_lblUserName.Text = "Utilisateur:";
            m_lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblServerURL
            // 
            m_lblServerURL.Image = global::DGD.HubGovernor.Properties.Resources.url;
            m_lblServerURL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_lblServerURL.Location = new System.Drawing.Point(12, 24);
            m_lblServerURL.Name = "m_lblServerURL";
            m_lblServerURL.Size = new System.Drawing.Size(88, 32);
            m_lblServerURL.TabIndex = 3;
            m_lblServerURL.Text = "Serveur:";
            m_lblServerURL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_btnOK
            // 
            m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            m_btnOK.Location = new System.Drawing.Point(148, 216);
            m_btnOK.Name = "m_btnOK";
            m_btnOK.Size = new System.Drawing.Size(75, 23);
            m_btnOK.TabIndex = 9;
            m_btnOK.Text = "OK";
            m_btnOK.UseVisualStyleBackColor = true;
            // 
            // m_btnCancel
            // 
            m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            m_btnCancel.Location = new System.Drawing.Point(248, 216);
            m_btnCancel.Name = "m_btnCancel";
            m_btnCancel.Size = new System.Drawing.Size(75, 23);
            m_btnCancel.TabIndex = 10;
            m_btnCancel.Text = "Annuler";
            m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_bServerURL
            // 
            this.m_bServerURL.Location = new System.Drawing.Point(134, 31);
            this.m_bServerURL.Name = "m_bServerURL";
            this.m_bServerURL.Size = new System.Drawing.Size(325, 20);
            this.m_bServerURL.TabIndex = 4;
            // 
            // m_tbUserName
            // 
            this.m_tbUserName.Location = new System.Drawing.Point(134, 81);
            this.m_tbUserName.Name = "m_tbUserName";
            this.m_tbUserName.Size = new System.Drawing.Size(203, 20);
            this.m_tbUserName.TabIndex = 6;
            // 
            // m_tbPassword
            // 
            this.m_tbPassword.Location = new System.Drawing.Point(134, 131);
            this.m_tbPassword.Name = "m_tbPassword";
            this.m_tbPassword.Size = new System.Drawing.Size(203, 20);
            this.m_tbPassword.TabIndex = 8;
            this.m_tbPassword.UseSystemPasswordChar = true;
            // 
            // m_chkSavePassword
            // 
            this.m_chkSavePassword.AutoSize = true;
            this.m_chkSavePassword.Location = new System.Drawing.Point(15, 173);
            this.m_chkSavePassword.Name = "m_chkSavePassword";
            this.m_chkSavePassword.Size = new System.Drawing.Size(156, 17);
            this.m_chkSavePassword.TabIndex = 11;
            this.m_chkSavePassword.Text = "Enregistrer le mot de passe.";
            this.m_chkSavePassword.UseVisualStyleBackColor = true;
            // 
            // CredentialDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 255);
            this.Controls.Add(this.m_chkSavePassword);
            this.Controls.Add(m_btnCancel);
            this.Controls.Add(m_btnOK);
            this.Controls.Add(this.m_tbPassword);
            this.Controls.Add(m_lblPassword);
            this.Controls.Add(this.m_tbUserName);
            this.Controls.Add(m_lblUserName);
            this.Controls.Add(this.m_bServerURL);
            this.Controls.Add(m_lblServerURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CredentialDialog";
            this.Text = "Paramètres de connexion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox m_bServerURL;
        private System.Windows.Forms.TextBox m_tbUserName;
        private System.Windows.Forms.TextBox m_tbPassword;
        private System.Windows.Forms.CheckBox m_chkSavePassword;
    }
}