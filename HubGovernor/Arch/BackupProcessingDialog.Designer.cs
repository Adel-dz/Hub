namespace DGD.HubGovernor.Arch
{
    partial class BackupProcessingDialog
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
            System.Windows.Forms.GroupBox m_grpFrame;
            this.m_lblProcessingData = new System.Windows.Forms.Label();
            this.m_pbData = new System.Windows.Forms.ProgressBar();
            this.m_pbLogs = new System.Windows.Forms.ProgressBar();
            this.m_lblProcessingLogs = new System.Windows.Forms.Label();
            this.m_pbUpdates = new System.Windows.Forms.ProgressBar();
            this.m_lblProcessingUpdates = new System.Windows.Forms.Label();
            this.m_pbSysFiles = new System.Windows.Forms.ProgressBar();
            this.m_lblProcessingSysFile = new System.Windows.Forms.Label();
            this.Annuler = new System.Windows.Forms.Button();
            m_grpFrame = new System.Windows.Forms.GroupBox();
            m_grpFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_grpFrame
            // 
            m_grpFrame.Controls.Add(this.Annuler);
            m_grpFrame.Controls.Add(this.m_pbSysFiles);
            m_grpFrame.Controls.Add(this.m_lblProcessingSysFile);
            m_grpFrame.Controls.Add(this.m_pbUpdates);
            m_grpFrame.Controls.Add(this.m_lblProcessingUpdates);
            m_grpFrame.Controls.Add(this.m_pbLogs);
            m_grpFrame.Controls.Add(this.m_lblProcessingLogs);
            m_grpFrame.Controls.Add(this.m_pbData);
            m_grpFrame.Controls.Add(this.m_lblProcessingData);
            m_grpFrame.Location = new System.Drawing.Point(0, -6);
            m_grpFrame.Margin = new System.Windows.Forms.Padding(1);
            m_grpFrame.Name = "m_grpFrame";
            m_grpFrame.Size = new System.Drawing.Size(413, 280);
            m_grpFrame.TabIndex = 0;
            m_grpFrame.TabStop = false;
            // 
            // m_lblProcessingData
            // 
            this.m_lblProcessingData.AutoSize = true;
            this.m_lblProcessingData.Location = new System.Drawing.Point(12, 16);
            this.m_lblProcessingData.Name = "m_lblProcessingData";
            this.m_lblProcessingData.Size = new System.Drawing.Size(256, 13);
            this.m_lblProcessingData.TabIndex = 0;
            this.m_lblProcessingData.Text = "Préparation de la sauvegarde des bases de données";
            // 
            // m_pbData
            // 
            this.m_pbData.Location = new System.Drawing.Point(12, 32);
            this.m_pbData.Name = "m_pbData";
            this.m_pbData.Size = new System.Drawing.Size(383, 19);
            this.m_pbData.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_pbData.TabIndex = 1;
            // 
            // m_pbLogs
            // 
            this.m_pbLogs.Location = new System.Drawing.Point(12, 77);
            this.m_pbLogs.Name = "m_pbLogs";
            this.m_pbLogs.Size = new System.Drawing.Size(383, 19);
            this.m_pbLogs.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_pbLogs.TabIndex = 3;
            // 
            // m_lblProcessingLogs
            // 
            this.m_lblProcessingLogs.AutoSize = true;
            this.m_lblProcessingLogs.Location = new System.Drawing.Point(12, 61);
            this.m_lblProcessingLogs.Name = "m_lblProcessingLogs";
            this.m_lblProcessingLogs.Size = new System.Drawing.Size(255, 13);
            this.m_lblProcessingLogs.TabIndex = 2;
            this.m_lblProcessingLogs.Text = "Préparation de la sauvegarde des bases de journaux";
            // 
            // m_pbUpdates
            // 
            this.m_pbUpdates.Location = new System.Drawing.Point(12, 129);
            this.m_pbUpdates.Name = "m_pbUpdates";
            this.m_pbUpdates.Size = new System.Drawing.Size(383, 19);
            this.m_pbUpdates.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_pbUpdates.TabIndex = 5;
            // 
            // m_lblProcessingUpdates
            // 
            this.m_lblProcessingUpdates.AutoSize = true;
            this.m_lblProcessingUpdates.Location = new System.Drawing.Point(12, 113);
            this.m_lblProcessingUpdates.Name = "m_lblProcessingUpdates";
            this.m_lblProcessingUpdates.Size = new System.Drawing.Size(224, 13);
            this.m_lblProcessingUpdates.TabIndex = 4;
            this.m_lblProcessingUpdates.Text = "Préparation de la sauvegarde des mises à jour";
            // 
            // m_pbSysFiles
            // 
            this.m_pbSysFiles.Location = new System.Drawing.Point(12, 184);
            this.m_pbSysFiles.Name = "m_pbSysFiles";
            this.m_pbSysFiles.Size = new System.Drawing.Size(383, 19);
            this.m_pbSysFiles.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.m_pbSysFiles.TabIndex = 7;
            // 
            // m_lblProcessingSysFile
            // 
            this.m_lblProcessingSysFile.AutoSize = true;
            this.m_lblProcessingSysFile.Location = new System.Drawing.Point(12, 168);
            this.m_lblProcessingSysFile.Name = "m_lblProcessingSysFile";
            this.m_lblProcessingSysFile.Size = new System.Drawing.Size(248, 13);
            this.m_lblProcessingSysFile.TabIndex = 6;
            this.m_lblProcessingSysFile.Text = "Préparation de la sauvegarde des fichiers systèmes";
            // 
            // Annuler
            // 
            this.Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Annuler.Location = new System.Drawing.Point(306, 235);
            this.Annuler.Name = "Annuler";
            this.Annuler.Size = new System.Drawing.Size(89, 23);
            this.Annuler.TabIndex = 8;
            this.Annuler.Text = "Annuler";
            this.Annuler.UseVisualStyleBackColor = true;
            // 
            // BackupProcessingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(414, 277);
            this.Controls.Add(m_grpFrame);
            this.ForeColor = System.Drawing.Color.SkyBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BackupProcessingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Préparation de l\'archivage";
            m_grpFrame.ResumeLayout(false);
            m_grpFrame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar m_pbData;
        private System.Windows.Forms.Button Annuler;
        private System.Windows.Forms.ProgressBar m_pbSysFiles;
        private System.Windows.Forms.ProgressBar m_pbUpdates;
        private System.Windows.Forms.ProgressBar m_pbLogs;
        private System.Windows.Forms.Label m_lblProcessingSysFile;
        private System.Windows.Forms.Label m_lblProcessingUpdates;
        private System.Windows.Forms.Label m_lblProcessingLogs;
        private System.Windows.Forms.Label m_lblProcessingData;
    }
}