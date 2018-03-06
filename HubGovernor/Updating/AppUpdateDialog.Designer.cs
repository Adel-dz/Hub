namespace DGD.HubGovernor.Updating
{
    partial class AppUpdateDialog
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
            System.Windows.Forms.Label m_lblFiles;
            System.Windows.Forms.ColumnHeader m_colFileName;
            System.Windows.Forms.ColumnHeader m_colDestFolder;
            System.Windows.Forms.Label m_lblVersion;
            System.Windows.Forms.Label m_lblSystem;
            System.Windows.Forms.Button m_btnInsert;
            this.m_lvFiles = new System.Windows.Forms.ListView();
            this.m_btnDelete = new System.Windows.Forms.Button();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_tbVersion = new System.Windows.Forms.TextBox();
            this.m_cmbSystem = new System.Windows.Forms.ComboBox();
            m_lblFiles = new System.Windows.Forms.Label();
            m_colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colDestFolder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_lblVersion = new System.Windows.Forms.Label();
            m_lblSystem = new System.Windows.Forms.Label();
            m_btnInsert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblFiles
            // 
            m_lblFiles.AutoSize = true;
            m_lblFiles.Location = new System.Drawing.Point(13, 13);
            m_lblFiles.Name = "m_lblFiles";
            m_lblFiles.Size = new System.Drawing.Size(43, 13);
            m_lblFiles.TabIndex = 7;
            m_lblFiles.Text = "Fichiers";
            // 
            // m_lvFiles
            // 
            this.m_lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colFileName,
            m_colDestFolder});
            this.m_lvFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvFiles.HideSelection = false;
            this.m_lvFiles.Location = new System.Drawing.Point(16, 29);
            this.m_lvFiles.Name = "m_lvFiles";
            this.m_lvFiles.ShowItemToolTips = true;
            this.m_lvFiles.Size = new System.Drawing.Size(376, 248);
            this.m_lvFiles.TabIndex = 6;
            this.m_lvFiles.UseCompatibleStateImageBehavior = false;
            this.m_lvFiles.View = System.Windows.Forms.View.Details;
            // 
            // m_colFileName
            // 
            m_colFileName.Text = "Fichier";
            m_colFileName.Width = 131;
            // 
            // m_colDestFolder
            // 
            m_colDestFolder.Text = "Sous-dossier de destination";
            m_colDestFolder.Width = 235;
            // 
            // m_btnDelete
            // 
            this.m_btnDelete.Enabled = false;
            this.m_btnDelete.Location = new System.Drawing.Point(398, 107);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Size = new System.Drawing.Size(87, 23);
            this.m_btnDelete.TabIndex = 1;
            this.m_btnDelete.Text = "Retirer";
            this.m_btnDelete.UseVisualStyleBackColor = true;
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Enabled = false;
            this.m_btnOK.Location = new System.Drawing.Point(398, 289);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(87, 23);
            this.m_btnOK.TabIndex = 4;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(398, 329);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(87, 23);
            this.m_btnCancel.TabIndex = 5;
            this.m_btnCancel.Text = "Annuler";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_lblVersion
            // 
            m_lblVersion.AutoSize = true;
            m_lblVersion.Location = new System.Drawing.Point(13, 293);
            m_lblVersion.Name = "m_lblVersion";
            m_lblVersion.Size = new System.Drawing.Size(45, 13);
            m_lblVersion.TabIndex = 8;
            m_lblVersion.Text = "Version:";
            // 
            // m_tbVersion
            // 
            this.m_tbVersion.Location = new System.Drawing.Point(65, 289);
            this.m_tbVersion.Name = "m_tbVersion";
            this.m_tbVersion.Size = new System.Drawing.Size(177, 20);
            this.m_tbVersion.TabIndex = 2;
            this.m_tbVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // m_lblSystem
            // 
            m_lblSystem.AutoSize = true;
            m_lblSystem.Location = new System.Drawing.Point(13, 328);
            m_lblSystem.Name = "m_lblSystem";
            m_lblSystem.Size = new System.Drawing.Size(50, 13);
            m_lblSystem.TabIndex = 9;
            m_lblSystem.Text = "Système:";
            // 
            // m_cmbSystem
            // 
            this.m_cmbSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbSystem.FormattingEnabled = true;
            this.m_cmbSystem.Items.AddRange(new object[] {
            "Microsoft Windows 7 SP1"});
            this.m_cmbSystem.Location = new System.Drawing.Point(65, 324);
            this.m_cmbSystem.Name = "m_cmbSystem";
            this.m_cmbSystem.Size = new System.Drawing.Size(177, 21);
            this.m_cmbSystem.TabIndex = 3;
            // 
            // m_btnInsert
            // 
            m_btnInsert.Image = global::DGD.HubGovernor.Properties.Resources.folder_Open_16;
            m_btnInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            m_btnInsert.Location = new System.Drawing.Point(398, 63);
            m_btnInsert.Name = "m_btnInsert";
            m_btnInsert.Size = new System.Drawing.Size(87, 23);
            m_btnInsert.TabIndex = 0;
            m_btnInsert.Text = "   Insérer...";
            m_btnInsert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            m_btnInsert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            m_btnInsert.UseVisualStyleBackColor = true;
            // 
            // AppUpdateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 368);
            this.ControlBox = false;
            this.Controls.Add(this.m_cmbSystem);
            this.Controls.Add(m_lblSystem);
            this.Controls.Add(this.m_tbVersion);
            this.Controls.Add(m_lblVersion);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_btnDelete);
            this.Controls.Add(m_btnInsert);
            this.Controls.Add(this.m_lvFiles);
            this.Controls.Add(m_lblFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppUpdateDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAJ d\'application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView m_lvFiles;
        private System.Windows.Forms.Button m_btnDelete;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.TextBox m_tbVersion;
        private System.Windows.Forms.ComboBox m_cmbSystem;
    }
}