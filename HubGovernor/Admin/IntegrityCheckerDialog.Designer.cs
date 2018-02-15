namespace DGD.HubGovernor.Admin
{
    partial class IntegrityCheckerDialog
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
            System.Windows.Forms.PictureBox m_pbLogo;
            System.Windows.Forms.Label m_lblHeader;
            System.Windows.Forms.Button m_btnCancel;
            System.Windows.Forms.Button m_btnCheckAll;
            System.Windows.Forms.Button m_btnToggleCheck;
            this.m_lbTables = new System.Windows.Forms.CheckedListBox();
            this.m_btnOK = new System.Windows.Forms.Button();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            m_lblHeader = new System.Windows.Forms.Label();
            m_btnCancel = new System.Windows.Forms.Button();
            m_btnCheckAll = new System.Windows.Forms.Button();
            m_btnToggleCheck = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pbLogo
            // 
            m_pbLogo.Image = global::DGD.HubGovernor.Properties.Resources.integrity_checker;
            m_pbLogo.Location = new System.Drawing.Point(12, 12);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(196, 153);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pbLogo.TabIndex = 0;
            m_pbLogo.TabStop = false;
            // 
            // m_lblHeader
            // 
            m_lblHeader.AutoSize = true;
            m_lblHeader.Location = new System.Drawing.Point(224, 12);
            m_lblHeader.Name = "m_lblHeader";
            m_lblHeader.Size = new System.Drawing.Size(313, 13);
            m_lblHeader.TabIndex = 1;
            m_lblHeader.Text = "Cochez l’ensemble des tables dont vous voulez vérifier l’intégrité.";
            // 
            // m_btnCancel
            // 
            m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            m_btnCancel.Location = new System.Drawing.Point(290, 180);
            m_btnCancel.Name = "m_btnCancel";
            m_btnCancel.Size = new System.Drawing.Size(117, 23);
            m_btnCancel.TabIndex = 3;
            m_btnCancel.Text = "Annuler";
            m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_lbTables
            // 
            this.m_lbTables.CheckOnClick = true;
            this.m_lbTables.FormattingEnabled = true;
            this.m_lbTables.IntegralHeight = false;
            this.m_lbTables.Location = new System.Drawing.Point(227, 41);
            this.m_lbTables.Name = "m_lbTables";
            this.m_lbTables.Size = new System.Drawing.Size(228, 124);
            this.m_lbTables.Sorted = true;
            this.m_lbTables.TabIndex = 2;
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Location = new System.Drawing.Point(144, 180);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(117, 23);
            this.m_btnOK.TabIndex = 4;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // m_btnCheckAll
            // 
            m_btnCheckAll.Location = new System.Drawing.Point(462, 41);
            m_btnCheckAll.Name = "m_btnCheckAll";
            m_btnCheckAll.Size = new System.Drawing.Size(75, 47);
            m_btnCheckAll.TabIndex = 5;
            m_btnCheckAll.Text = "Sélectionner tous";
            m_btnCheckAll.UseVisualStyleBackColor = true;
            m_btnCheckAll.Click += new System.EventHandler(this.CheckAll_Click);
            // 
            // m_btnToggleCheck
            // 
            m_btnToggleCheck.Location = new System.Drawing.Point(462, 94);
            m_btnToggleCheck.Name = "m_btnToggleCheck";
            m_btnToggleCheck.Size = new System.Drawing.Size(75, 47);
            m_btnToggleCheck.TabIndex = 6;
            m_btnToggleCheck.Text = "Inverser la sélection";
            m_btnToggleCheck.UseVisualStyleBackColor = true;
            m_btnToggleCheck.Click += new System.EventHandler(this.ToggleCheck_Click);
            // 
            // IntegrityCheckerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 216);
            this.Controls.Add(m_btnToggleCheck);
            this.Controls.Add(m_btnCheckAll);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(m_btnCancel);
            this.Controls.Add(this.m_lbTables);
            this.Controls.Add(m_lblHeader);
            this.Controls.Add(m_pbLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntegrityCheckerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analyse d\'intégrtié";
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox m_lbTables;
        private System.Windows.Forms.Button m_btnOK;
    }
}