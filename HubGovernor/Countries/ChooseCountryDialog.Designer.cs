namespace DGD.HubGovernor.Countries
{
    partial class ChooseCountryDialog
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
            System.Windows.Forms.Label m_lblInfo;
            System.Windows.Forms.PictureBox m_pbLogo;
            System.Windows.Forms.Button m_btnAdd;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseCountryDialog));
            System.Windows.Forms.Button m_btnCancel;
            this.m_cbCountries = new System.Windows.Forms.ComboBox();
            this.m_btnOK = new System.Windows.Forms.Button();
            m_lblInfo = new System.Windows.Forms.Label();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            m_btnAdd = new System.Windows.Forms.Button();
            m_btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblInfo
            // 
            m_lblInfo.AutoSize = true;
            m_lblInfo.Location = new System.Drawing.Point(13, 13);
            m_lblInfo.Name = "m_lblInfo";
            m_lblInfo.Size = new System.Drawing.Size(354, 13);
            m_lblInfo.TabIndex = 0;
            m_lblInfo.Text = "Sélectionnez un pays parmi ceux déjà existants, ou créez-en un nouveau.";
            // 
            // m_pbLogo
            // 
            m_pbLogo.Image = global::DGD.HubGovernor.Properties.Resources.country_64;
            m_pbLogo.Location = new System.Drawing.Point(16, 43);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(64, 64);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pbLogo.TabIndex = 1;
            m_pbLogo.TabStop = false;
            // 
            // m_btnAdd
            // 
            m_btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAdd.Image")));
            m_btnAdd.Location = new System.Drawing.Point(319, 55);
            m_btnAdd.Name = "m_btnAdd";
            m_btnAdd.Size = new System.Drawing.Size(48, 35);
            m_btnAdd.TabIndex = 3;
            m_btnAdd.UseVisualStyleBackColor = true;
            m_btnAdd.Click += new System.EventHandler(this.Add_Click);
            // 
            // m_btnCancel
            // 
            m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            m_btnCancel.Location = new System.Drawing.Point(203, 132);
            m_btnCancel.Name = "m_btnCancel";
            m_btnCancel.Size = new System.Drawing.Size(75, 23);
            m_btnCancel.TabIndex = 5;
            m_btnCancel.Text = "Annuler";
            m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_cbCountries
            // 
            this.m_cbCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbCountries.FormattingEnabled = true;
            this.m_cbCountries.Location = new System.Drawing.Point(97, 62);
            this.m_cbCountries.Name = "m_cbCountries";
            this.m_cbCountries.Size = new System.Drawing.Size(207, 21);
            this.m_cbCountries.Sorted = true;
            this.m_cbCountries.TabIndex = 2;
            this.m_cbCountries.SelectedIndexChanged += new System.EventHandler(this.Countries_SelectedIndexChanged);
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Enabled = false;
            this.m_btnOK.Location = new System.Drawing.Point(106, 132);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 4;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // ChooseCountryDialog
            // 
            this.AcceptButton = this.m_btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = m_btnCancel;
            this.ClientSize = new System.Drawing.Size(385, 167);
            this.Controls.Add(m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(m_btnAdd);
            this.Controls.Add(this.m_cbCountries);
            this.Controls.Add(m_pbLogo);
            this.Controls.Add(m_lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseCountryDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choisir un pays";
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox m_cbCountries;
        private System.Windows.Forms.Button m_btnOK;
    }
}