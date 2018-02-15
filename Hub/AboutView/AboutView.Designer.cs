namespace DGD.Hub.AboutView
{
    partial class AboutView
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
            System.Windows.Forms.PictureBox pictureBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutView));
            System.Windows.Forms.PictureBox m_pbLogo;
            System.Windows.Forms.PictureBox m_pbTitle;
            this.m_lblVersion = new System.Windows.Forms.Label();
            this.m_lblDataGeneration = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            m_pbTitle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(m_pbTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            pictureBox1.Location = new System.Drawing.Point(127, 113);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(182, 50);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // m_pbLogo
            // 
            m_pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            m_pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("m_pbLogo.Image")));
            m_pbLogo.InitialImage = global::DGD.Hub.Properties.Resources.logo_douane_529_754;
            m_pbLogo.Location = new System.Drawing.Point(115, 173);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(618, 328);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            m_pbLogo.TabIndex = 3;
            m_pbLogo.TabStop = false;
            // 
            // m_pbTitle
            // 
            m_pbTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            m_pbTitle.Image = global::DGD.Hub.Properties.Resources.hub_title;
            m_pbTitle.Location = new System.Drawing.Point(115, 29);
            m_pbTitle.Name = "m_pbTitle";
            m_pbTitle.Size = new System.Drawing.Size(618, 78);
            m_pbTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pbTitle.TabIndex = 0;
            m_pbTitle.TabStop = false;
            // 
            // m_lblVersion
            // 
            this.m_lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_lblVersion.ForeColor = System.Drawing.Color.Blue;
            this.m_lblVersion.Location = new System.Drawing.Point(115, 110);
            this.m_lblVersion.Name = "m_lblVersion";
            this.m_lblVersion.Size = new System.Drawing.Size(618, 23);
            this.m_lblVersion.TabIndex = 1;
            this.m_lblVersion.Text = "Version: ";
            this.m_lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblDataGeneration
            // 
            this.m_lblDataGeneration.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_lblDataGeneration.ForeColor = System.Drawing.Color.Blue;
            this.m_lblDataGeneration.Location = new System.Drawing.Point(124, 133);
            this.m_lblDataGeneration.Name = "m_lblDataGeneration";
            this.m_lblDataGeneration.Size = new System.Drawing.Size(609, 23);
            this.m_lblDataGeneration.TabIndex = 2;
            this.m_lblDataGeneration.Text = "Version des données:";
            this.m_lblDataGeneration.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // AboutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(pictureBox1);
            this.Controls.Add(m_pbLogo);
            this.Controls.Add(this.m_lblDataGeneration);
            this.Controls.Add(this.m_lblVersion);
            this.Controls.Add(m_pbTitle);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "AboutView";
            this.Size = new System.Drawing.Size(848, 531);
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(m_pbTitle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblVersion;
        private System.Windows.Forms.Label m_lblDataGeneration;
    }
}
