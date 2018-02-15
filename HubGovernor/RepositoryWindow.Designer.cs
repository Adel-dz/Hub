namespace DGD.HubGovernor
{
    partial class RepositoryWindow
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.SplitContainer m_splitter;
            System.Windows.Forms.ColumnHeader m_columnHeader;
            System.Windows.Forms.ImageList m_ilSmall;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepositoryWindow));
            System.Windows.Forms.TableLayoutPanel m_tablePanel;
            System.Windows.Forms.Label m_lblFileSizeLabel;
            System.Windows.Forms.Label m_lblFileNameLabel;
            System.Windows.Forms.Label m_lblGenerationLabel;
            System.Windows.Forms.Label m_lblRowCountLabel;
            System.Windows.Forms.Label m_lblAccessTimeLabel;
            System.Windows.Forms.Label m_lblWriteTimeLabel;
            System.Windows.Forms.Label m_lblCreateTimeLabel;
            System.Windows.Forms.ToolStrip m_toolStrip;
            System.Windows.Forms.ToolStripButton m_tsbHelp;
            this.m_lvTables = new System.Windows.Forms.ListView();
            this.m_lblFileSize = new System.Windows.Forms.Label();
            this.m_lblFileName = new System.Windows.Forms.Label();
            this.m_lblGeneration = new System.Windows.Forms.Label();
            this.m_lblWriteTime = new System.Windows.Forms.Label();
            this.m_lblCreateTime = new System.Windows.Forms.Label();
            this.m_lblAccessTime = new System.Windows.Forms.Label();
            this.m_lblRowCount = new System.Windows.Forms.Label();
            m_splitter = new System.Windows.Forms.SplitContainer();
            m_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_ilSmall = new System.Windows.Forms.ImageList(this.components);
            m_tablePanel = new System.Windows.Forms.TableLayoutPanel();
            m_lblFileSizeLabel = new System.Windows.Forms.Label();
            m_lblFileNameLabel = new System.Windows.Forms.Label();
            m_lblGenerationLabel = new System.Windows.Forms.Label();
            m_lblRowCountLabel = new System.Windows.Forms.Label();
            m_lblAccessTimeLabel = new System.Windows.Forms.Label();
            m_lblWriteTimeLabel = new System.Windows.Forms.Label();
            m_lblCreateTimeLabel = new System.Windows.Forms.Label();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            m_tsbHelp = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).BeginInit();
            m_splitter.Panel1.SuspendLayout();
            m_splitter.Panel2.SuspendLayout();
            m_splitter.SuspendLayout();
            m_tablePanel.SuspendLayout();
            m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitter
            // 
            m_splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            m_splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            m_splitter.Location = new System.Drawing.Point(0, 25);
            m_splitter.Name = "m_splitter";
            // 
            // m_splitter.Panel1
            // 
            m_splitter.Panel1.Controls.Add(this.m_lvTables);
            // 
            // m_splitter.Panel2
            // 
            m_splitter.Panel2.AutoScroll = true;
            m_splitter.Panel2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            m_splitter.Panel2.Controls.Add(m_tablePanel);
            m_splitter.Size = new System.Drawing.Size(655, 271);
            m_splitter.SplitterDistance = 342;
            m_splitter.TabIndex = 0;
            // 
            // m_lvTables
            // 
            this.m_lvTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_columnHeader});
            this.m_lvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvTables.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_lvTables.Location = new System.Drawing.Point(0, 0);
            this.m_lvTables.Name = "m_lvTables";
            this.m_lvTables.Size = new System.Drawing.Size(342, 271);
            this.m_lvTables.SmallImageList = m_ilSmall;
            this.m_lvTables.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.m_lvTables.TabIndex = 0;
            this.m_lvTables.UseCompatibleStateImageBehavior = false;
            this.m_lvTables.View = System.Windows.Forms.View.Details;
            this.m_lvTables.ItemActivate += new System.EventHandler(this.Tables_ItemActivate);
            this.m_lvTables.SelectedIndexChanged += new System.EventHandler(this.Tables_SelectedIndexChanged);
            // 
            // m_columnHeader
            // 
            m_columnHeader.Text = "-";
            // 
            // m_ilSmall
            // 
            m_ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ilSmall.ImageStream")));
            m_ilSmall.TransparentColor = System.Drawing.Color.Transparent;
            m_ilSmall.Images.SetKeyName(0, "repository_32.png");
            // 
            // m_tablePanel
            // 
            m_tablePanel.AutoSize = true;
            m_tablePanel.ColumnCount = 2;
            m_tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            m_tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            m_tablePanel.Controls.Add(this.m_lblFileSize, 1, 6);
            m_tablePanel.Controls.Add(m_lblFileSizeLabel, 0, 6);
            m_tablePanel.Controls.Add(this.m_lblFileName, 1, 5);
            m_tablePanel.Controls.Add(m_lblFileNameLabel, 0, 5);
            m_tablePanel.Controls.Add(this.m_lblGeneration, 1, 4);
            m_tablePanel.Controls.Add(m_lblGenerationLabel, 0, 4);
            m_tablePanel.Controls.Add(m_lblRowCountLabel, 0, 3);
            m_tablePanel.Controls.Add(m_lblAccessTimeLabel, 0, 2);
            m_tablePanel.Controls.Add(this.m_lblWriteTime, 1, 1);
            m_tablePanel.Controls.Add(m_lblWriteTimeLabel, 0, 1);
            m_tablePanel.Controls.Add(m_lblCreateTimeLabel, 0, 0);
            m_tablePanel.Controls.Add(this.m_lblCreateTime, 1, 0);
            m_tablePanel.Controls.Add(this.m_lblAccessTime, 1, 2);
            m_tablePanel.Controls.Add(this.m_lblRowCount, 1, 3);
            m_tablePanel.Location = new System.Drawing.Point(3, 3);
            m_tablePanel.Name = "m_tablePanel";
            m_tablePanel.RowCount = 7;
            m_tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            m_tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            m_tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            m_tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            m_tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            m_tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            m_tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            m_tablePanel.Size = new System.Drawing.Size(155, 99);
            m_tablePanel.TabIndex = 0;
            // 
            // m_lblFileSize
            // 
            this.m_lblFileSize.AutoSize = true;
            this.m_lblFileSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblFileSize.Location = new System.Drawing.Point(142, 78);
            this.m_lblFileSize.Name = "m_lblFileSize";
            this.m_lblFileSize.Size = new System.Drawing.Size(10, 21);
            this.m_lblFileSize.TabIndex = 13;
            this.m_lblFileSize.Text = "-";
            this.m_lblFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblFileSizeLabel
            // 
            m_lblFileSizeLabel.AutoSize = true;
            m_lblFileSizeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblFileSizeLabel.Location = new System.Drawing.Point(3, 78);
            m_lblFileSizeLabel.Name = "m_lblFileSizeLabel";
            m_lblFileSizeLabel.Size = new System.Drawing.Size(133, 21);
            m_lblFileSizeLabel.TabIndex = 12;
            m_lblFileSizeLabel.Text = "Taille:";
            m_lblFileSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblFileName
            // 
            this.m_lblFileName.AutoSize = true;
            this.m_lblFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblFileName.Location = new System.Drawing.Point(142, 65);
            this.m_lblFileName.Name = "m_lblFileName";
            this.m_lblFileName.Size = new System.Drawing.Size(10, 13);
            this.m_lblFileName.TabIndex = 11;
            this.m_lblFileName.Text = "-";
            this.m_lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblFileNameLabel
            // 
            m_lblFileNameLabel.AutoSize = true;
            m_lblFileNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblFileNameLabel.Location = new System.Drawing.Point(3, 65);
            m_lblFileNameLabel.Name = "m_lblFileNameLabel";
            m_lblFileNameLabel.Size = new System.Drawing.Size(133, 13);
            m_lblFileNameLabel.TabIndex = 10;
            m_lblFileNameLabel.Text = "Fichier:";
            m_lblFileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblGeneration
            // 
            this.m_lblGeneration.AutoSize = true;
            this.m_lblGeneration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblGeneration.Location = new System.Drawing.Point(142, 52);
            this.m_lblGeneration.Name = "m_lblGeneration";
            this.m_lblGeneration.Size = new System.Drawing.Size(10, 13);
            this.m_lblGeneration.TabIndex = 9;
            this.m_lblGeneration.Text = "-";
            this.m_lblGeneration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblGenerationLabel
            // 
            m_lblGenerationLabel.AutoSize = true;
            m_lblGenerationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblGenerationLabel.Location = new System.Drawing.Point(3, 52);
            m_lblGenerationLabel.Name = "m_lblGenerationLabel";
            m_lblGenerationLabel.Size = new System.Drawing.Size(133, 13);
            m_lblGenerationLabel.TabIndex = 8;
            m_lblGenerationLabel.Text = "Génération:";
            m_lblGenerationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblRowCountLabel
            // 
            m_lblRowCountLabel.AutoSize = true;
            m_lblRowCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblRowCountLabel.Location = new System.Drawing.Point(3, 39);
            m_lblRowCountLabel.Name = "m_lblRowCountLabel";
            m_lblRowCountLabel.Size = new System.Drawing.Size(133, 13);
            m_lblRowCountLabel.TabIndex = 6;
            m_lblRowCountLabel.Text = "Nombre d’enregistrements:";
            m_lblRowCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblAccessTimeLabel
            // 
            m_lblAccessTimeLabel.AutoSize = true;
            m_lblAccessTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblAccessTimeLabel.Location = new System.Drawing.Point(3, 26);
            m_lblAccessTimeLabel.Name = "m_lblAccessTimeLabel";
            m_lblAccessTimeLabel.Size = new System.Drawing.Size(133, 13);
            m_lblAccessTimeLabel.TabIndex = 4;
            m_lblAccessTimeLabel.Text = "Dernier accès le:";
            m_lblAccessTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblWriteTime
            // 
            this.m_lblWriteTime.AutoSize = true;
            this.m_lblWriteTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblWriteTime.Location = new System.Drawing.Point(142, 13);
            this.m_lblWriteTime.Name = "m_lblWriteTime";
            this.m_lblWriteTime.Size = new System.Drawing.Size(10, 13);
            this.m_lblWriteTime.TabIndex = 3;
            this.m_lblWriteTime.Text = "-";
            this.m_lblWriteTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblWriteTimeLabel
            // 
            m_lblWriteTimeLabel.AutoSize = true;
            m_lblWriteTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblWriteTimeLabel.Location = new System.Drawing.Point(3, 13);
            m_lblWriteTimeLabel.Name = "m_lblWriteTimeLabel";
            m_lblWriteTimeLabel.Size = new System.Drawing.Size(133, 13);
            m_lblWriteTimeLabel.TabIndex = 2;
            m_lblWriteTimeLabel.Text = "Modifiée le:";
            m_lblWriteTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCreateTimeLabel
            // 
            m_lblCreateTimeLabel.AutoSize = true;
            m_lblCreateTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_lblCreateTimeLabel.Location = new System.Drawing.Point(3, 0);
            m_lblCreateTimeLabel.Name = "m_lblCreateTimeLabel";
            m_lblCreateTimeLabel.Size = new System.Drawing.Size(133, 13);
            m_lblCreateTimeLabel.TabIndex = 0;
            m_lblCreateTimeLabel.Text = "Créée le:";
            m_lblCreateTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblCreateTime
            // 
            this.m_lblCreateTime.AutoSize = true;
            this.m_lblCreateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblCreateTime.Location = new System.Drawing.Point(142, 0);
            this.m_lblCreateTime.Name = "m_lblCreateTime";
            this.m_lblCreateTime.Size = new System.Drawing.Size(10, 13);
            this.m_lblCreateTime.TabIndex = 1;
            this.m_lblCreateTime.Text = "-";
            this.m_lblCreateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblAccessTime
            // 
            this.m_lblAccessTime.AutoSize = true;
            this.m_lblAccessTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblAccessTime.Location = new System.Drawing.Point(142, 26);
            this.m_lblAccessTime.Name = "m_lblAccessTime";
            this.m_lblAccessTime.Size = new System.Drawing.Size(10, 13);
            this.m_lblAccessTime.TabIndex = 5;
            this.m_lblAccessTime.Text = "-";
            this.m_lblAccessTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblRowCount
            // 
            this.m_lblRowCount.AutoSize = true;
            this.m_lblRowCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblRowCount.Location = new System.Drawing.Point(142, 39);
            this.m_lblRowCount.Name = "m_lblRowCount";
            this.m_lblRowCount.Size = new System.Drawing.Size(10, 13);
            this.m_lblRowCount.TabIndex = 7;
            this.m_lblRowCount.Text = "-";
            this.m_lblRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_toolStrip
            // 
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbHelp});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(655, 25);
            m_toolStrip.TabIndex = 1;
            // 
            // m_tsbHelp
            // 
            m_tsbHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            m_tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbHelp.Image = global::DGD.HubGovernor.Properties.Resources.help_16;
            m_tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbHelp.Name = "m_tsbHelp";
            m_tsbHelp.Size = new System.Drawing.Size(23, 22);
            m_tsbHelp.Text = "Aide";
            // 
            // RepositoryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(655, 296);
            this.Controls.Add(m_splitter);
            this.Controls.Add(m_toolStrip);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RepositoryWindow";
            this.Text = "Dépot de tables";
            m_splitter.Panel1.ResumeLayout(false);
            m_splitter.Panel2.ResumeLayout(false);
            m_splitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).EndInit();
            m_splitter.ResumeLayout(false);
            m_tablePanel.ResumeLayout(false);
            m_tablePanel.PerformLayout();
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView m_lvTables;
        private System.Windows.Forms.Label m_lblWriteTime;
        private System.Windows.Forms.Label m_lblCreateTime;
        private System.Windows.Forms.Label m_lblAccessTime;
        private System.Windows.Forms.Label m_lblRowCount;
        private System.Windows.Forms.Label m_lblFileSize;
        private System.Windows.Forms.Label m_lblFileName;
        private System.Windows.Forms.Label m_lblGeneration;
    }
}