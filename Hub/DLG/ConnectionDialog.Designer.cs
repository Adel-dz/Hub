namespace DGD.Hub.DLG
{
    partial class ConnectionDialog
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.PictureBox m_pbLogo;
            this.m_lbl_ProgressInfo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.m_timer = new System.Windows.Forms.Timer(this.components);
            m_pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pbLogo
            // 
            m_pbLogo.Image = global::DGD.Hub.Properties.Resources.spinner_32;
            m_pbLogo.Location = new System.Drawing.Point(13, 13);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(32, 32);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pbLogo.TabIndex = 0;
            m_pbLogo.TabStop = false;
            // 
            // m_lbl_ProgressInfo
            // 
            this.m_lbl_ProgressInfo.AutoSize = true;
            this.m_lbl_ProgressInfo.Location = new System.Drawing.Point(64, 23);
            this.m_lbl_ProgressInfo.Name = "m_lbl_ProgressInfo";
            this.m_lbl_ProgressInfo.Size = new System.Drawing.Size(189, 13);
            this.m_lbl_ProgressInfo.TabIndex = 1;
            this.m_lbl_ProgressInfo.Text = "Merci de patienter, transfert en cours...";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(286, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Annuler";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // m_timer
            // 
            this.m_timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // ConnectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(380, 62);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_lbl_ProgressInfo);
            this.Controls.Add(m_pbLogo);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enregistrement en cours...";
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer m_timer;
        private System.Windows.Forms.Label m_lbl_ProgressInfo;
    }
}