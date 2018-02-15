namespace DGD.Hub
{
    partial class BusyDialog
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
            System.Windows.Forms.GroupBox m_groupBox;
            this.m_pbSpinner = new System.Windows.Forms.PictureBox();
            this.m_lblMessage = new System.Windows.Forms.Label();
            m_groupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // m_groupBox
            // 
            m_groupBox.Location = new System.Drawing.Point(3, -3);
            m_groupBox.Name = "m_groupBox";
            m_groupBox.Size = new System.Drawing.Size(352, 62);
            m_groupBox.TabIndex = 2;
            m_groupBox.TabStop = false;
            // 
            // m_pbSpinner
            // 
            this.m_pbSpinner.Image = global::DGD.Hub.Properties.Resources.spinner_32;
            this.m_pbSpinner.Location = new System.Drawing.Point(12, 13);
            this.m_pbSpinner.Name = "m_pbSpinner";
            this.m_pbSpinner.Size = new System.Drawing.Size(32, 32);
            this.m_pbSpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.m_pbSpinner.TabIndex = 0;
            this.m_pbSpinner.TabStop = false;
            // 
            // m_lblMessage
            // 
            this.m_lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_lblMessage.AutoSize = true;
            this.m_lblMessage.Location = new System.Drawing.Point(50, 24);
            this.m_lblMessage.Name = "m_lblMessage";
            this.m_lblMessage.Size = new System.Drawing.Size(110, 13);
            this.m_lblMessage.TabIndex = 1;
            this.m_lblMessage.Text = "Traitement en cours...";
            this.m_lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BusyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(358, 60);
            this.Controls.Add(this.m_lblMessage);
            this.Controls.Add(this.m_pbSpinner);
            this.Controls.Add(m_groupBox);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BusyDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BusyDialog";
            ((System.ComponentModel.ISupportInitialize)(this.m_pbSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblMessage;
        private System.Windows.Forms.PictureBox m_pbSpinner;
    }
}