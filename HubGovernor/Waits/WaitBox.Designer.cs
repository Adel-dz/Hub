namespace DGD.HubGovernor.Waits
{
    partial class WaitBox
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
            System.Windows.Forms.PictureBox m_pictureBox;
            m_pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pictureBox
            // 
            m_pictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            m_pictureBox.Image = global::DGD.HubGovernor.Properties.Resources.animated_loading;
            m_pictureBox.Location = new System.Drawing.Point(129, 52);
            m_pictureBox.Name = "m_pictureBox";
            m_pictureBox.Size = new System.Drawing.Size(228, 144);
            m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            m_pictureBox.TabIndex = 0;
            m_pictureBox.TabStop = false;
            // 
            // WaitBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(m_pictureBox);
            this.Name = "WaitBox";
            this.Size = new System.Drawing.Size(486, 248);
            this.UseWaitCursor = true;
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
