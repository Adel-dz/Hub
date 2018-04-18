namespace DGD.HubGovernor.Arch
{
    partial class ArchiveViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveViewer));
            this.m_lbData = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // m_lbData
            // 
            this.m_lbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lbData.FormattingEnabled = true;
            this.m_lbData.HorizontalScrollbar = true;
            this.m_lbData.Location = new System.Drawing.Point(0, 0);
            this.m_lbData.Name = "m_lbData";
            this.m_lbData.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.m_lbData.Size = new System.Drawing.Size(550, 386);
            this.m_lbData.TabIndex = 0;
            // 
            // ArchiveViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 386);
            this.Controls.Add(this.m_lbData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ArchiveViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox m_lbData;
    }
}