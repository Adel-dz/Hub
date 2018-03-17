namespace DGD.HubGovernor.Updating
{
    partial class AppUpdateViewer
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
            System.Windows.Forms.ColumnHeader m_colKey;
            System.Windows.Forms.ColumnHeader m_colValue;
            System.Windows.Forms.ToolStrip m_toolStrip;
            System.Windows.Forms.ToolStripButton m_tsbExtract;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppUpdateViewer));
            this.m_lvData = new System.Windows.Forms.ListView();
            m_colKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            m_tsbExtract = new System.Windows.Forms.ToolStripButton();
            m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_colKey
            // 
            m_colKey.Text = "";
            m_colKey.Width = 132;
            // 
            // m_colValue
            // 
            m_colValue.Text = "";
            m_colValue.Width = 303;
            // 
            // m_lvData
            // 
            this.m_lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colKey,
            m_colValue});
            this.m_lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvData.FullRowSelect = true;
            this.m_lvData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lvData.Location = new System.Drawing.Point(0, 25);
            this.m_lvData.MultiSelect = false;
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.ShowItemToolTips = true;
            this.m_lvData.Size = new System.Drawing.Size(447, 341);
            this.m_lvData.TabIndex = 0;
            this.m_lvData.UseCompatibleStateImageBehavior = false;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbExtract});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(447, 25);
            m_toolStrip.TabIndex = 1;
            // 
            // m_tsbExtract
            // 
            m_tsbExtract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbExtract.Image = global::DGD.HubGovernor.Properties.Resources.Extract_16;
            m_tsbExtract.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbExtract.Name = "m_tsbExtract";
            m_tsbExtract.Size = new System.Drawing.Size(23, 22);
            m_tsbExtract.Text = "Extraire...";
            m_tsbExtract.Click += new System.EventHandler(this.Extract_Click);
            // 
            // AppUpdateViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 366);
            this.Controls.Add(this.m_lvData);
            this.Controls.Add(m_toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AppUpdateViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView m_lvData;
    }
}