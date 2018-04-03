namespace DGD.HubGovernor.Log
{
    partial class SysLogWindow
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
            System.Windows.Forms.ToolStripButton m_tsbSortAscending;
            System.Windows.Forms.ToolStripButton m_tsbSortDescending;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripButton m_tsbOptions;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysLogWindow));
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbFilterDate = new System.Windows.Forms.ToolStripButton();
            this.m_rtbData = new System.Windows.Forms.RichTextBox();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            m_tsbSortAscending = new System.Windows.Forms.ToolStripButton();
            m_tsbSortDescending = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            m_tsbOptions = new System.Windows.Forms.ToolStripButton();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbSortAscending,
            m_tsbSortDescending,
            this.toolStripSeparator2,
            this.m_tsbFilterDate,
            toolStripSeparator1,
            m_tsbOptions,
            m_tsbHelp});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(602, 25);
            m_toolStrip.TabIndex = 0;
            // 
            // m_tsbSortAscending
            // 
            m_tsbSortAscending.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbSortAscending.Image = global::DGD.HubGovernor.Properties.Resources.SortAscending_16;
            m_tsbSortAscending.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbSortAscending.Name = "m_tsbSortAscending";
            m_tsbSortAscending.Size = new System.Drawing.Size(23, 22);
            m_tsbSortAscending.Text = "Trier du plus ancien au plus récent";
            m_tsbSortAscending.Click += new System.EventHandler(this.SortAscending_Click);
            // 
            // m_tsbSortDescending
            // 
            m_tsbSortDescending.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbSortDescending.Image = global::DGD.HubGovernor.Properties.Resources.SortDescending_16;
            m_tsbSortDescending.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbSortDescending.Name = "m_tsbSortDescending";
            m_tsbSortDescending.Size = new System.Drawing.Size(23, 22);
            m_tsbSortDescending.Text = "Trier du récent au plus ancien";
            m_tsbSortDescending.Click += new System.EventHandler(this.SortDescending_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbFilterDate
            // 
            this.m_tsbFilterDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbFilterDate.Enabled = false;
            this.m_tsbFilterDate.Image = global::DGD.HubGovernor.Properties.Resources.filter_16;
            this.m_tsbFilterDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbFilterDate.Name = "m_tsbFilterDate";
            this.m_tsbFilterDate.Size = new System.Drawing.Size(23, 22);
            this.m_tsbFilterDate.Text = "Filter par date";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsbOptions
            // 
            m_tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbOptions.Enabled = false;
            m_tsbOptions.Image = global::DGD.HubGovernor.Properties.Resources.option_16;
            m_tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbOptions.Name = "m_tsbOptions";
            m_tsbOptions.Size = new System.Drawing.Size(23, 22);
            m_tsbOptions.Text = "Paramètres";
            // 
            // m_tsbHelp
            // 
            m_tsbHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            m_tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbHelp.Enabled = false;
            m_tsbHelp.Image = global::DGD.HubGovernor.Properties.Resources.help_16;
            m_tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbHelp.Name = "m_tsbHelp";
            m_tsbHelp.Size = new System.Drawing.Size(23, 22);
            m_tsbHelp.Text = "Aide";
            // 
            // m_rtbData
            // 
            this.m_rtbData.BackColor = System.Drawing.SystemColors.Window;
            this.m_rtbData.BulletIndent = 4;
            this.m_rtbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_rtbData.Location = new System.Drawing.Point(0, 25);
            this.m_rtbData.Name = "m_rtbData";
            this.m_rtbData.ReadOnly = true;
            this.m_rtbData.ShortcutsEnabled = false;
            this.m_rtbData.Size = new System.Drawing.Size(602, 340);
            this.m_rtbData.TabIndex = 1;
            this.m_rtbData.Text = "";
            // 
            // SysLogWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 365);
            this.Controls.Add(this.m_rtbData);
            this.Controls.Add(m_toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SysLogWindow";
            this.Text = "Journal système";
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton m_tsbFilterDate;
        private System.Windows.Forms.RichTextBox m_rtbData;
    }
}