namespace DGD.Hub.DummyView
{
    partial class DummyView
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
            System.Windows.Forms.PictureBox m_pbImage;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DummyView));
            System.Windows.Forms.Label m_lblTitle;
            m_pbImage = new System.Windows.Forms.PictureBox();
            m_lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(m_pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pbImage
            // 
            m_pbImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_pbImage.Image = ((System.Drawing.Image)(resources.GetObject("m_pbImage.Image")));
            m_pbImage.Location = new System.Drawing.Point(216, 170);
            m_pbImage.Name = "m_pbImage";
            m_pbImage.Size = new System.Drawing.Size(643, 362);
            m_pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            m_pbImage.TabIndex = 0;
            m_pbImage.TabStop = false;
            // 
            // m_lblTitle
            // 
            m_lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            m_lblTitle.AutoSize = true;
            m_lblTitle.Font = new System.Drawing.Font("Arial", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            m_lblTitle.Location = new System.Drawing.Point(238, 60);
            m_lblTitle.Name = "m_lblTitle";
            m_lblTitle.Size = new System.Drawing.Size(599, 61);
            m_lblTitle.TabIndex = 1;
            m_lblTitle.Text = "Module en construction!";
            // 
            // DummyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(m_lblTitle);
            this.Controls.Add(m_pbImage);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "DummyView";
            this.Size = new System.Drawing.Size(1075, 639);
            ((System.ComponentModel.ISupportInitialize)(m_pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
