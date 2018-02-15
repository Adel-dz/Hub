namespace DGD.HubGovernor.Opts
{
    partial class SettingsWizard
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
            System.Windows.Forms.Button m_btnClose;
            System.Windows.Forms.ColumnHeader m_colSection;
            System.Windows.Forms.Panel m_mainPanel;
            this.m_pagePanel = new System.Windows.Forms.Panel();
            this.m_lvSections = new System.Windows.Forms.ListView();
            m_btnClose = new System.Windows.Forms.Button();
            m_colSection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_mainPanel = new System.Windows.Forms.Panel();
            m_mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnClose
            // 
            m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            m_btnClose.Location = new System.Drawing.Point(462, 422);
            m_btnClose.Name = "m_btnClose";
            m_btnClose.Size = new System.Drawing.Size(75, 23);
            m_btnClose.TabIndex = 2;
            m_btnClose.Text = "Fermer";
            m_btnClose.UseVisualStyleBackColor = true;
            // 
            // m_colSection
            // 
            m_colSection.Text = "";
            m_colSection.Width = 142;
            // 
            // m_mainPanel
            // 
            m_mainPanel.Controls.Add(this.m_pagePanel);
            m_mainPanel.Controls.Add(this.m_lvSections);
            m_mainPanel.Dock = System.Windows.Forms.DockStyle.Top;
            m_mainPanel.Location = new System.Drawing.Point(0, 0);
            m_mainPanel.Name = "m_mainPanel";
            m_mainPanel.Size = new System.Drawing.Size(549, 395);
            m_mainPanel.TabIndex = 1;
            // 
            // m_pagePanel
            // 
            this.m_pagePanel.AutoScroll = true;
            this.m_pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pagePanel.Location = new System.Drawing.Point(150, 0);
            this.m_pagePanel.Name = "m_pagePanel";
            this.m_pagePanel.Size = new System.Drawing.Size(399, 395);
            this.m_pagePanel.TabIndex = 1;
            // 
            // m_lvSections
            // 
            this.m_lvSections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colSection});
            this.m_lvSections.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_lvSections.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lvSections.Location = new System.Drawing.Point(0, 0);
            this.m_lvSections.MultiSelect = false;
            this.m_lvSections.Name = "m_lvSections";
            this.m_lvSections.Size = new System.Drawing.Size(150, 395);
            this.m_lvSections.TabIndex = 0;
            this.m_lvSections.UseCompatibleStateImageBehavior = false;
            this.m_lvSections.View = System.Windows.Forms.View.Details;
            this.m_lvSections.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.Sections_ItemSelectionChanged);
            // 
            // SettingsWizard
            // 
            this.AcceptButton = m_btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = m_btnClose;
            this.ClientSize = new System.Drawing.Size(549, 457);
            this.Controls.Add(m_btnClose);
            this.Controls.Add(m_mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paramètres";
            m_mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView m_lvSections;
        private System.Windows.Forms.Panel m_pagePanel;
    }
}