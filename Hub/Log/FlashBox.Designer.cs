namespace DGD.Hub.Log
{
    partial class FlashBox
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
            this.m_lbMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_lbMessage
            // 
            this.m_lbMessage.AutoSize = true;
            this.m_lbMessage.ForeColor = System.Drawing.Color.SteelBlue;
            this.m_lbMessage.Location = new System.Drawing.Point(4, 4);
            this.m_lbMessage.Name = "m_lbMessage";
            this.m_lbMessage.Size = new System.Drawing.Size(35, 13);
            this.m_lbMessage.TabIndex = 0;
            this.m_lbMessage.Text = "label1";
            this.m_lbMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FlashBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(494, 39);
            this.ControlBox = false;
            this.Controls.Add(this.m_lbMessage);
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FlashBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Message Flash";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lbMessage;
    }
}