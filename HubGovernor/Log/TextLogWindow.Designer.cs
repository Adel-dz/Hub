namespace DGD.HubGovernor.Log
{
    partial class TextLogWindow
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
            System.Windows.Forms.ToolStrip m_toolStrip;
            System.Windows.Forms.ToolStripButton m_tsbClear;
            this.m_rtbLogBox = new System.Windows.Forms.RichTextBox();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            m_tsbClear = new System.Windows.Forms.ToolStripButton();
            m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbClear});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(218, 25);
            m_toolStrip.TabIndex = 0;
            // 
            // m_tsbClear
            // 
            m_tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbClear.Image = global::DGD.HubGovernor.Properties.Resources.clear_16;
            m_tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbClear.Name = "m_tsbClear";
            m_tsbClear.Size = new System.Drawing.Size(23, 22);
            m_tsbClear.Text = "Effacer tout";
            m_tsbClear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // m_rtbLogBox
            // 
            this.m_rtbLogBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_rtbLogBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbLogBox.Font = new System.Drawing.Font("Consolas", 7.25F);
            this.m_rtbLogBox.Location = new System.Drawing.Point(0, 25);
            this.m_rtbLogBox.Name = "m_rtbLogBox";
            this.m_rtbLogBox.ReadOnly = true;
            this.m_rtbLogBox.Size = new System.Drawing.Size(218, 237);
            this.m_rtbLogBox.TabIndex = 1;
            this.m_rtbLogBox.Text = "";
            // 
            // LogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 262);
            this.Controls.Add(this.m_rtbLogBox);
            this.Controls.Add(m_toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "LogWindow";
            this.ShowInTaskbar = false;
            this.Text = "Journal";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox m_rtbLogBox;
    }
}