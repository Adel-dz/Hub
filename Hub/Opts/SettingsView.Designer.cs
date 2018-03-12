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
            System.Windows.Forms.Label m_lblContact;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label m_lblProfileLabel;
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.m_lblProfile = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            m_grpClientInfo = new System.Windows.Forms.GroupBox();
            m_lblContact = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            m_lblProfileLabel = new System.Windows.Forms.Label();
            m_grpClientInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_grpClientInfo
            // 
            m_grpClientInfo.Controls.Add(this.textBox3);
            m_grpClientInfo.Controls.Add(this.textBox2);
            m_grpClientInfo.Controls.Add(this.m_lblProfile);
            m_grpClientInfo.Controls.Add(this.textBox1);
            m_grpClientInfo.Controls.Add(m_lblProfileLabel);
            m_grpClientInfo.Controls.Add(label2);
            m_grpClientInfo.Controls.Add(label1);
            m_grpClientInfo.Controls.Add(m_lblContact);
            m_grpClientInfo.Location = new System.Drawing.Point(15, 14);
            m_grpClientInfo.Name = "m_grpClientInfo";
            m_grpClientInfo.Size = new System.Drawing.Size(368, 164);
            m_grpClientInfo.TabIndex = 0;
            m_grpClientInfo.TabStop = false;
            m_grpClientInfo.Text = " Infos Utilsateur ";
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 97);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(109, 13);
            label1.TabIndex = 1;
            label1.Text = "Numéro de téléphone";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 126);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 13);
            label2.TabIndex = 2;
            label2.Text = "E-mail";
            // 
            // m_lblProfileLabel
            // 
            m_lblProfileLabel.AutoSize = true;
            m_lblProfileLabel.Location = new System.Drawing.Point(6, 44);
            m_lblProfileLabel.Name = "m_lblProfileLabel";
            m_lblProfileLabel.Size = new System.Drawing.Size(30, 13);
            m_lblProfileLabel.TabIndex = 3;
            m_lblProfileLabel.Text = "Profil";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(121, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(213, 20);
            this.textBox1.TabIndex = 4;
            // 
            // m_lblProfile
            // 
            this.m_lblProfile.AutoSize = true;
            this.m_lblProfile.Location = new System.Drawing.Point(121, 44);
            this.m_lblProfile.Name = "m_lblProfile";
            this.m_lblProfile.Size = new System.Drawing.Size(10, 13);
            this.m_lblProfile.TabIndex = 5;
            this.m_lblProfile.Text = "-";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(121, 93);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(213, 20);
            this.textBox2.TabIndex = 6;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(121, 122);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(213, 20);
            this.textBox3.TabIndex = 7;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 200);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(255, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Utiliser les codes pays au lieu des noms de pays.";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(m_grpClientInfo);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(398, 270);
            m_grpClientInfo.ResumeLayout(false);
            m_grpClientInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label m_lblProfile;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
