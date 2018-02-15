namespace DGD.HubGovernor
{
    partial class InputDialog
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
            System.Windows.Forms.Button m_btnCancel;
            this.m_lblMessage = new System.Windows.Forms.Label();
            this.m_tbInput = new System.Windows.Forms.TextBox();
            this.m_btnOK = new System.Windows.Forms.Button();
            m_btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_btnCancel
            // 
            m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            m_btnCancel.Location = new System.Drawing.Point(181, 78);
            m_btnCancel.Name = "m_btnCancel";
            m_btnCancel.Size = new System.Drawing.Size(75, 23);
            m_btnCancel.TabIndex = 2;
            m_btnCancel.Text = "Annuler";
            m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_lblMessage
            // 
            this.m_lblMessage.AutoSize = true;
            this.m_lblMessage.Location = new System.Drawing.Point(13, 13);
            this.m_lblMessage.Name = "m_lblMessage";
            this.m_lblMessage.Size = new System.Drawing.Size(0, 13);
            this.m_lblMessage.TabIndex = 0;
            // 
            // m_tbInput
            // 
            this.m_tbInput.Location = new System.Drawing.Point(16, 38);
            this.m_tbInput.Name = "m_tbInput";
            this.m_tbInput.Size = new System.Drawing.Size(316, 20);
            this.m_tbInput.TabIndex = 1;
            this.m_tbInput.TextChanged += new System.EventHandler(this.Input_TextChanged);
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Enabled = false;
            this.m_btnOK.Location = new System.Drawing.Point(88, 78);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 3;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // InputDialog
            // 
            this.AcceptButton = this.m_btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = m_btnCancel;
            this.ClientSize = new System.Drawing.Size(344, 113);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(m_btnCancel);
            this.Controls.Add(this.m_tbInput);
            this.Controls.Add(this.m_lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hub Governor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblMessage;
        private System.Windows.Forms.TextBox m_tbInput;
        private System.Windows.Forms.Button m_btnOK;
    }
}