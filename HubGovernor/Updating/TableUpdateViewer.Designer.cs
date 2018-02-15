namespace DGD.HubGovernor.Updating
{
    partial class TableUpdateViewer
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
            System.Windows.Forms.SplitContainer m_splitter;
            System.Windows.Forms.Panel m_topPanel;
            System.Windows.Forms.Label m_lblPreGeneration;
            System.Windows.Forms.Label m_lblPostGeneration;
            System.Windows.Forms.ColumnHeader m_colActionName;
            System.Windows.Forms.ColumnHeader m_colRowID;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableUpdateViewer));
            this.m_lbTables = new System.Windows.Forms.ListBox();
            this.m_lblPreGenerationValue = new System.Windows.Forms.Label();
            this.m_lblPostGenerationValue = new System.Windows.Forms.Label();
            this.m_lvActions = new System.Windows.Forms.ListView();
            m_splitter = new System.Windows.Forms.SplitContainer();
            m_topPanel = new System.Windows.Forms.Panel();
            m_lblPreGeneration = new System.Windows.Forms.Label();
            m_lblPostGeneration = new System.Windows.Forms.Label();
            m_colActionName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colRowID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).BeginInit();
            m_splitter.Panel1.SuspendLayout();
            m_splitter.Panel2.SuspendLayout();
            m_splitter.SuspendLayout();
            m_topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitter
            // 
            m_splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            m_splitter.Location = new System.Drawing.Point(0, 0);
            m_splitter.Name = "m_splitter";
            // 
            // m_splitter.Panel1
            // 
            m_splitter.Panel1.Controls.Add(this.m_lbTables);
            // 
            // m_splitter.Panel2
            // 
            m_splitter.Panel2.Controls.Add(this.m_lvActions);
            m_splitter.Panel2.Controls.Add(m_topPanel);
            m_splitter.Size = new System.Drawing.Size(517, 421);
            m_splitter.SplitterDistance = 199;
            m_splitter.TabIndex = 0;
            // 
            // m_lbTables
            // 
            this.m_lbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lbTables.FormattingEnabled = true;
            this.m_lbTables.IntegralHeight = false;
            this.m_lbTables.Location = new System.Drawing.Point(0, 0);
            this.m_lbTables.Name = "m_lbTables";
            this.m_lbTables.Size = new System.Drawing.Size(199, 421);
            this.m_lbTables.Sorted = true;
            this.m_lbTables.TabIndex = 0;
            this.m_lbTables.SelectedIndexChanged += new System.EventHandler(this.Tables_SelectedIndexChanged);
            // 
            // m_topPanel
            // 
            m_topPanel.AutoScroll = true;
            m_topPanel.BackColor = System.Drawing.SystemColors.Control;
            m_topPanel.Controls.Add(this.m_lblPostGenerationValue);
            m_topPanel.Controls.Add(m_lblPostGeneration);
            m_topPanel.Controls.Add(this.m_lblPreGenerationValue);
            m_topPanel.Controls.Add(m_lblPreGeneration);
            m_topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            m_topPanel.Location = new System.Drawing.Point(0, 0);
            m_topPanel.Name = "m_topPanel";
            m_topPanel.Size = new System.Drawing.Size(314, 64);
            m_topPanel.TabIndex = 0;
            // 
            // m_lblPreGeneration
            // 
            m_lblPreGeneration.AutoSize = true;
            m_lblPreGeneration.Location = new System.Drawing.Point(3, 9);
            m_lblPreGeneration.Name = "m_lblPreGeneration";
            m_lblPreGeneration.Size = new System.Drawing.Size(64, 13);
            m_lblPreGeneration.TabIndex = 0;
            m_lblPreGeneration.Text = "Pre-Version:";
            // 
            // m_lblPreGenerationValue
            // 
            this.m_lblPreGenerationValue.AutoSize = true;
            this.m_lblPreGenerationValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPreGenerationValue.Location = new System.Drawing.Point(85, 9);
            this.m_lblPreGenerationValue.Name = "m_lblPreGenerationValue";
            this.m_lblPreGenerationValue.Size = new System.Drawing.Size(11, 13);
            this.m_lblPreGenerationValue.TabIndex = 1;
            this.m_lblPreGenerationValue.Text = "-";
            // 
            // m_lblPostGeneration
            // 
            m_lblPostGeneration.AutoSize = true;
            m_lblPostGeneration.Location = new System.Drawing.Point(3, 37);
            m_lblPostGeneration.Name = "m_lblPostGeneration";
            m_lblPostGeneration.Size = new System.Drawing.Size(69, 13);
            m_lblPostGeneration.TabIndex = 2;
            m_lblPostGeneration.Text = "Post-Version:";
            // 
            // m_lblPostGenerationValue
            // 
            this.m_lblPostGenerationValue.AutoSize = true;
            this.m_lblPostGenerationValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblPostGenerationValue.Location = new System.Drawing.Point(85, 37);
            this.m_lblPostGenerationValue.Name = "m_lblPostGenerationValue";
            this.m_lblPostGenerationValue.Size = new System.Drawing.Size(11, 13);
            this.m_lblPostGenerationValue.TabIndex = 3;
            this.m_lblPostGenerationValue.Text = "-";
            // 
            // m_lvActions
            // 
            this.m_lvActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colActionName,
            m_colRowID});
            this.m_lvActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvActions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lvActions.Location = new System.Drawing.Point(0, 64);
            this.m_lvActions.Name = "m_lvActions";
            this.m_lvActions.Size = new System.Drawing.Size(314, 357);
            this.m_lvActions.TabIndex = 1;
            this.m_lvActions.UseCompatibleStateImageBehavior = false;
            this.m_lvActions.View = System.Windows.Forms.View.Details;
            // 
            // m_colActionName
            // 
            m_colActionName.Text = "Action";
            m_colActionName.Width = 193;
            // 
            // m_colRowID
            // 
            m_colRowID.Text = "ID Ligne";
            // 
            // TableUpdateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(517, 421);
            this.Controls.Add(m_splitter);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "TableUpdateView";
            this.Text = "Mises à jour";
            m_splitter.Panel1.ResumeLayout(false);
            m_splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).EndInit();
            m_splitter.ResumeLayout(false);
            m_topPanel.ResumeLayout(false);
            m_topPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox m_lbTables;
        private System.Windows.Forms.ListView m_lvActions;
        private System.Windows.Forms.Label m_lblPostGenerationValue;
        private System.Windows.Forms.Label m_lblPreGenerationValue;
    }
}