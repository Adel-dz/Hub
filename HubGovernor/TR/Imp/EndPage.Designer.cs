namespace DGD.HubGovernor.TR.Imp
{
    partial class EndPage
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
            System.Windows.Forms.Label m_lblHeader;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndPage));
            System.Windows.Forms.PictureBox m_pictureBox;
            this.m_tbBadRows = new System.Windows.Forms.TextBox();
            this.m_btnSave = new System.Windows.Forms.Button();
            m_lblHeader = new System.Windows.Forms.Label();
            m_pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblHeader
            // 
            m_lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblHeader.Location = new System.Drawing.Point(89, 9);
            m_lblHeader.Name = "m_lblHeader";
            m_lblHeader.Size = new System.Drawing.Size(557, 64);
            m_lblHeader.TabIndex = 3;
            m_lblHeader.Text = resources.GetString("m_lblHeader.Text");
            m_lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_pictureBox
            // 
            m_pictureBox.Image = global::DGD.HubGovernor.Properties.Resources.Wizard_64;
            m_pictureBox.Location = new System.Drawing.Point(6, 9);
            m_pictureBox.Name = "m_pictureBox";
            m_pictureBox.Size = new System.Drawing.Size(64, 64);
            m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pictureBox.TabIndex = 2;
            m_pictureBox.TabStop = false;
            // 
            // m_tbBadRows
            // 
            this.m_tbBadRows.BackColor = System.Drawing.SystemColors.Window;
            this.m_tbBadRows.Location = new System.Drawing.Point(17, 89);
            this.m_tbBadRows.Multiline = true;
            this.m_tbBadRows.Name = "m_tbBadRows";
            this.m_tbBadRows.ReadOnly = true;
            this.m_tbBadRows.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_tbBadRows.Size = new System.Drawing.Size(677, 227);
            this.m_tbBadRows.TabIndex = 4;
            // 
            // m_btnSave
            // 
            this.m_btnSave.Location = new System.Drawing.Point(604, 322);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(90, 23);
            this.m_btnSave.TabIndex = 5;
            this.m_btnSave.Text = "Enregistrer...";
            this.m_btnSave.UseVisualStyleBackColor = true;
            this.m_btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // EndPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.m_btnSave);
            this.Controls.Add(this.m_tbBadRows);
            this.Controls.Add(m_lblHeader);
            this.Controls.Add(m_pictureBox);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "EndPage";
            this.Size = new System.Drawing.Size(710, 368);
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbBadRows;
        private System.Windows.Forms.Button m_btnSave;
    }
}
