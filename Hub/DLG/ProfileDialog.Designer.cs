namespace DGD.Hub.DLG
{
    partial class ProfileDialog
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
            System.Windows.Forms.Label m_lblProfile;
            System.Windows.Forms.Label m_lblContact;
            System.Windows.Forms.Label m_lblEMail;
            System.Windows.Forms.Label m_lblPhone;
            System.Windows.Forms.Button m_btnCancel;
            System.Windows.Forms.PictureBox m_pbLogo;
            this.m_lblInfo = new System.Windows.Forms.Label();
            this.m_cbProfiles = new System.Windows.Forms.ComboBox();
            this.m_tbContact = new System.Windows.Forms.TextBox();
            this.m_tbEMail = new System.Windows.Forms.TextBox();
            this.m_tbPhone = new System.Windows.Forms.TextBox();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_lblHeader = new System.Windows.Forms.Label();
            m_lblProfile = new System.Windows.Forms.Label();
            m_lblContact = new System.Windows.Forms.Label();
            m_lblEMail = new System.Windows.Forms.Label();
            m_lblPhone = new System.Windows.Forms.Label();
            m_btnCancel = new System.Windows.Forms.Button();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblProfile
            // 
            m_lblProfile.AutoSize = true;
            m_lblProfile.Location = new System.Drawing.Point(226, 79);
            m_lblProfile.Name = "m_lblProfile";
            m_lblProfile.Size = new System.Drawing.Size(33, 13);
            m_lblProfile.TabIndex = 2;
            m_lblProfile.Text = "Profil:";
            // 
            // m_lblContact
            // 
            m_lblContact.AutoSize = true;
            m_lblContact.Location = new System.Drawing.Point(226, 110);
            m_lblContact.Name = "m_lblContact";
            m_lblContact.Size = new System.Drawing.Size(54, 13);
            m_lblContact.TabIndex = 4;
            m_lblContact.Text = "Utilsateur:";
            // 
            // m_lblEMail
            // 
            m_lblEMail.AutoSize = true;
            m_lblEMail.Location = new System.Drawing.Point(226, 170);
            m_lblEMail.Name = "m_lblEMail";
            m_lblEMail.Size = new System.Drawing.Size(38, 13);
            m_lblEMail.TabIndex = 6;
            m_lblEMail.Text = "e-Mail:";
            // 
            // m_lblPhone
            // 
            m_lblPhone.AutoSize = true;
            m_lblPhone.Location = new System.Drawing.Point(226, 140);
            m_lblPhone.Name = "m_lblPhone";
            m_lblPhone.Size = new System.Drawing.Size(61, 13);
            m_lblPhone.TabIndex = 8;
            m_lblPhone.Text = "Téléphone:";
            // 
            // m_btnCancel
            // 
            m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            m_btnCancel.Location = new System.Drawing.Point(414, 240);
            m_btnCancel.Name = "m_btnCancel";
            m_btnCancel.Size = new System.Drawing.Size(75, 23);
            m_btnCancel.TabIndex = 5;
            m_btnCancel.Text = "Annuler";
            m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_lblInfo
            // 
            this.m_lblInfo.Location = new System.Drawing.Point(13, 108);
            this.m_lblInfo.Name = "m_lblInfo";
            this.m_lblInfo.Size = new System.Drawing.Size(181, 155);
            this.m_lblInfo.TabIndex = 1;
            // 
            // m_cbProfiles
            // 
            this.m_cbProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbProfiles.FormattingEnabled = true;
            this.m_cbProfiles.Location = new System.Drawing.Point(290, 75);
            this.m_cbProfiles.Name = "m_cbProfiles";
            this.m_cbProfiles.Size = new System.Drawing.Size(231, 21);
            this.m_cbProfiles.TabIndex = 0;
            // 
            // m_tbContact
            // 
            this.m_tbContact.Location = new System.Drawing.Point(290, 106);
            this.m_tbContact.Name = "m_tbContact";
            this.m_tbContact.Size = new System.Drawing.Size(231, 20);
            this.m_tbContact.TabIndex = 1;
            // 
            // m_tbEMail
            // 
            this.m_tbEMail.Location = new System.Drawing.Point(290, 166);
            this.m_tbEMail.Name = "m_tbEMail";
            this.m_tbEMail.Size = new System.Drawing.Size(231, 20);
            this.m_tbEMail.TabIndex = 3;
            // 
            // m_tbPhone
            // 
            this.m_tbPhone.Location = new System.Drawing.Point(290, 136);
            this.m_tbPhone.Name = "m_tbPhone";
            this.m_tbPhone.Size = new System.Drawing.Size(231, 20);
            this.m_tbPhone.TabIndex = 2;
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Location = new System.Drawing.Point(316, 241);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 4;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.OK_Click);
            // 
            // m_lblHeader
            // 
            this.m_lblHeader.Location = new System.Drawing.Point(84, 9);
            this.m_lblHeader.Name = "m_lblHeader";
            this.m_lblHeader.Size = new System.Drawing.Size(437, 47);
            this.m_lblHeader.TabIndex = 9;
            this.m_lblHeader.Text = "Veuillez fournir les informations ci-dessous avant de poursuivre. Ces données ser" +
    "viront à vous enregistrer en tant qu’utilisateur au niveau de la sous-direction " +
    "de la valeur en douanes.";
            // 
            // m_pbLogo
            // 
            m_pbLogo.Image = global::DGD.Hub.Properties.Resources.profile_64;
            m_pbLogo.Location = new System.Drawing.Point(13, 28);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(64, 64);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pbLogo.TabIndex = 0;
            m_pbLogo.TabStop = false;
            // 
            // ProfileDialog
            // 
            this.AcceptButton = this.m_btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = m_btnCancel;
            this.ClientSize = new System.Drawing.Size(549, 282);
            this.Controls.Add(this.m_lblHeader);
            this.Controls.Add(m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_tbPhone);
            this.Controls.Add(m_lblPhone);
            this.Controls.Add(this.m_tbEMail);
            this.Controls.Add(m_lblEMail);
            this.Controls.Add(this.m_tbContact);
            this.Controls.Add(m_lblContact);
            this.Controls.Add(this.m_cbProfiles);
            this.Controls.Add(m_lblProfile);
            this.Controls.Add(this.m_lblInfo);
            this.Controls.Add(m_pbLogo);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enregistrement de l\'application";
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblInfo;
        private System.Windows.Forms.ComboBox m_cbProfiles;
        private System.Windows.Forms.TextBox m_tbContact;
        private System.Windows.Forms.TextBox m_tbEMail;
        private System.Windows.Forms.TextBox m_tbPhone;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Label m_lblHeader;
    }
}