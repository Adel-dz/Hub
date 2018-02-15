namespace DGD.HubGovernor.TR.Imp
{
    partial class ImportWizardDialog
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
            System.Windows.Forms.Panel m_navPanel;
            System.Windows.Forms.Button m_btnHelp;
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnPrevious = new System.Windows.Forms.Button();
            this.m_btnNext = new System.Windows.Forms.Button();
            this.m_clientPanel = new System.Windows.Forms.Panel();
            m_navPanel = new System.Windows.Forms.Panel();
            m_btnHelp = new System.Windows.Forms.Button();
            m_navPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_navPanel
            // 
            m_navPanel.Controls.Add(m_btnHelp);
            m_navPanel.Controls.Add(this.m_btnCancel);
            m_navPanel.Controls.Add(this.m_btnPrevious);
            m_navPanel.Controls.Add(this.m_btnNext);
            m_navPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            m_navPanel.Location = new System.Drawing.Point(0, 369);
            m_navPanel.Name = "m_navPanel";
            m_navPanel.Size = new System.Drawing.Size(711, 46);
            m_navPanel.TabIndex = 0;
            // 
            // m_btnHelp
            // 
            m_btnHelp.Location = new System.Drawing.Point(341, 12);
            m_btnHelp.Name = "m_btnHelp";
            m_btnHelp.Size = new System.Drawing.Size(75, 23);
            m_btnHelp.TabIndex = 3;
            m_btnHelp.Text = "Aide";
            m_btnHelp.UseVisualStyleBackColor = true;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(624, 12);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 2;
            this.m_btnCancel.Text = "Annuler";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_btnPrevious
            // 
            this.m_btnPrevious.Enabled = false;
            this.m_btnPrevious.Location = new System.Drawing.Point(443, 12);
            this.m_btnPrevious.Name = "m_btnPrevious";
            this.m_btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.m_btnPrevious.TabIndex = 1;
            this.m_btnPrevious.Text = "Précédent";
            this.m_btnPrevious.UseVisualStyleBackColor = true;
            this.m_btnPrevious.Click += new System.EventHandler(this.Previous_Click);
            // 
            // m_btnNext
            // 
            this.m_btnNext.Location = new System.Drawing.Point(524, 12);
            this.m_btnNext.Name = "m_btnNext";
            this.m_btnNext.Size = new System.Drawing.Size(75, 23);
            this.m_btnNext.TabIndex = 0;
            this.m_btnNext.Text = "Suivant";
            this.m_btnNext.UseVisualStyleBackColor = true;
            this.m_btnNext.Click += new System.EventHandler(this.Next_Click);
            // 
            // m_clientPanel
            // 
            this.m_clientPanel.BackColor = System.Drawing.SystemColors.Window;
            this.m_clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_clientPanel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_clientPanel.Location = new System.Drawing.Point(0, 0);
            this.m_clientPanel.Name = "m_clientPanel";
            this.m_clientPanel.Size = new System.Drawing.Size(711, 369);
            this.m_clientPanel.TabIndex = 1;
            // 
            // ImportWizardDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(711, 415);
            this.Controls.Add(this.m_clientPanel);
            this.Controls.Add(m_navPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportWizardDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importation";
            m_navPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_btnPrevious;
        private System.Windows.Forms.Button m_btnNext;
        private System.Windows.Forms.Panel m_clientPanel;
        private System.Windows.Forms.Button m_btnCancel;
    }
}