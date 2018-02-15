namespace DGD.HubGovernor.TR.Imp
{
    partial class PreviewPage
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label m_lblHeader;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewPage));
            System.Windows.Forms.PictureBox m_pictureBox;
            System.Windows.Forms.SplitContainer m_splitter;
            System.Windows.Forms.ColumnHeader m_colTables;
            System.Windows.Forms.ImageList m_ilSmall;
            this.m_lvTables = new System.Windows.Forms.ListView();
            this.m_lvData = new System.Windows.Forms.ListView();
            this.m_lblImportStatus = new System.Windows.Forms.Label();
            m_lblHeader = new System.Windows.Forms.Label();
            m_pictureBox = new System.Windows.Forms.PictureBox();
            m_splitter = new System.Windows.Forms.SplitContainer();
            m_colTables = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_ilSmall = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).BeginInit();
            m_splitter.Panel1.SuspendLayout();
            m_splitter.Panel2.SuspendLayout();
            m_splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lblHeader
            // 
            m_lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblHeader.Location = new System.Drawing.Point(83, 3);
            m_lblHeader.Name = "m_lblHeader";
            m_lblHeader.Size = new System.Drawing.Size(566, 64);
            m_lblHeader.TabIndex = 3;
            m_lblHeader.Text = resources.GetString("m_lblHeader.Text");
            m_lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_pictureBox
            // 
            m_pictureBox.Image = global::DGD.HubGovernor.Properties.Resources.Wizard_64;
            m_pictureBox.Location = new System.Drawing.Point(3, 3);
            m_pictureBox.Name = "m_pictureBox";
            m_pictureBox.Size = new System.Drawing.Size(64, 64);
            m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pictureBox.TabIndex = 2;
            m_pictureBox.TabStop = false;
            // 
            // m_splitter
            // 
            m_splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            m_splitter.Location = new System.Drawing.Point(3, 87);
            m_splitter.Name = "m_splitter";
            // 
            // m_splitter.Panel1
            // 
            m_splitter.Panel1.Controls.Add(this.m_lvTables);
            // 
            // m_splitter.Panel2
            // 
            m_splitter.Panel2.Controls.Add(this.m_lvData);
            m_splitter.Size = new System.Drawing.Size(704, 255);
            m_splitter.SplitterDistance = 154;
            m_splitter.TabIndex = 4;
            // 
            // m_lvTables
            // 
            this.m_lvTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colTables});
            this.m_lvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvTables.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvTables.Location = new System.Drawing.Point(0, 0);
            this.m_lvTables.MultiSelect = false;
            this.m_lvTables.Name = "m_lvTables";
            this.m_lvTables.ShowItemToolTips = true;
            this.m_lvTables.Size = new System.Drawing.Size(154, 255);
            this.m_lvTables.SmallImageList = m_ilSmall;
            this.m_lvTables.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.m_lvTables.TabIndex = 0;
            this.m_lvTables.UseCompatibleStateImageBehavior = false;
            this.m_lvTables.View = System.Windows.Forms.View.Details;
            this.m_lvTables.SelectedIndexChanged += new System.EventHandler(this.Tables_SelectedIndexChanged);
            // 
            // m_colTables
            // 
            m_colTables.Text = "Tables";
            m_colTables.Width = 142;
            // 
            // m_ilSmall
            // 
            m_ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ilSmall.ImageStream")));
            m_ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            m_ilSmall.Images.SetKeyName(0, "warn_error_provider_16.ico");
            // 
            // m_lvData
            // 
            this.m_lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvData.FullRowSelect = true;
            this.m_lvData.GridLines = true;
            this.m_lvData.Location = new System.Drawing.Point(0, 0);
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.Size = new System.Drawing.Size(546, 255);
            this.m_lvData.SmallImageList = m_ilSmall;
            this.m_lvData.TabIndex = 0;
            this.m_lvData.UseCompatibleStateImageBehavior = false;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            this.m_lvData.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.Data_ColumnClick);
            this.m_lvData.ItemActivate += new System.EventHandler(this.Data_ItemActivate);
            // 
            // m_lblImportStatus
            // 
            this.m_lblImportStatus.AutoSize = true;
            this.m_lblImportStatus.Location = new System.Drawing.Point(3, 345);
            this.m_lblImportStatus.Name = "m_lblImportStatus";
            this.m_lblImportStatus.Size = new System.Drawing.Size(10, 13);
            this.m_lblImportStatus.TabIndex = 5;
            this.m_lblImportStatus.Text = "-";
            // 
            // PreviewPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.m_lblImportStatus);
            this.Controls.Add(m_splitter);
            this.Controls.Add(m_lblHeader);
            this.Controls.Add(m_pictureBox);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "PreviewPage";
            this.Size = new System.Drawing.Size(710, 368);
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).EndInit();
            m_splitter.Panel1.ResumeLayout(false);
            m_splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).EndInit();
            m_splitter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView m_lvData;
        private System.Windows.Forms.Label m_lblImportStatus;
        private System.Windows.Forms.ListView m_lvTables;
    }
}
