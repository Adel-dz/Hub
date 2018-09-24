namespace DGD.HubGovernor.RunOnce
{
    partial class RunOnceWindow
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
            System.Windows.Forms.ToolStrip m_toolstrip;
            System.Windows.Forms.ToolStripButton m_tsbAddActions;
            System.Windows.Forms.ToolStripButton m_tsbClear;
            System.Windows.Forms.StatusStrip m_statusStrip;
            System.Windows.Forms.SplitContainer m_splitter;
            System.Windows.Forms.ColumnHeader m_colClientID;
            System.Windows.Forms.ColumnHeader m_colStatus;
            System.Windows.Forms.ColumnHeader m_colAction;
            System.Windows.Forms.ColumnHeader m_colDate;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunOnceWindow));
            this.m_tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.m_sslblActionCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_lvClients = new System.Windows.Forms.ListView();
            this.m_lvActions = new System.Windows.Forms.ListView();
            m_toolstrip = new System.Windows.Forms.ToolStrip();
            m_tsbAddActions = new System.Windows.Forms.ToolStripButton();
            m_tsbClear = new System.Windows.Forms.ToolStripButton();
            m_statusStrip = new System.Windows.Forms.StatusStrip();
            m_splitter = new System.Windows.Forms.SplitContainer();
            m_colClientID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_toolstrip.SuspendLayout();
            m_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).BeginInit();
            m_splitter.Panel1.SuspendLayout();
            m_splitter.Panel2.SuspendLayout();
            m_splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolstrip
            // 
            m_toolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbAddActions,
            this.m_tsbDelete,
            m_tsbClear});
            m_toolstrip.Location = new System.Drawing.Point(0, 0);
            m_toolstrip.Name = "m_toolstrip";
            m_toolstrip.Size = new System.Drawing.Size(569, 25);
            m_toolstrip.TabIndex = 0;
            // 
            // m_tsbAddActions
            // 
            m_tsbAddActions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbAddActions.Image = global::DGD.HubGovernor.Properties.Resources.add_actions_16;
            m_tsbAddActions.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbAddActions.Name = "m_tsbAddActions";
            m_tsbAddActions.Size = new System.Drawing.Size(23, 22);
            m_tsbAddActions.Text = "Ajouter des actions..";
            m_tsbAddActions.Click += new System.EventHandler(this.AddActions_Click);
            // 
            // m_tsbDelete
            // 
            this.m_tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbDelete.Enabled = false;
            this.m_tsbDelete.Image = global::DGD.HubGovernor.Properties.Resources.delete_16;
            this.m_tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbDelete.Name = "m_tsbDelete";
            this.m_tsbDelete.Size = new System.Drawing.Size(23, 22);
            this.m_tsbDelete.Text = "Supprimer l\'action sélectionnée";
            // 
            // m_tsbClear
            // 
            m_tsbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbClear.Image = global::DGD.HubGovernor.Properties.Resources.clear_16;
            m_tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbClear.Name = "m_tsbClear";
            m_tsbClear.Size = new System.Drawing.Size(23, 22);
            m_tsbClear.Text = "Effacer toutes les actions en attente du client";
            // 
            // m_statusStrip
            // 
            m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_sslblActionCount});
            m_statusStrip.Location = new System.Drawing.Point(0, 367);
            m_statusStrip.Name = "m_statusStrip";
            m_statusStrip.Size = new System.Drawing.Size(569, 22);
            m_statusStrip.TabIndex = 1;
            // 
            // m_sslblActionCount
            // 
            this.m_sslblActionCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_sslblActionCount.Name = "m_sslblActionCount";
            this.m_sslblActionCount.Size = new System.Drawing.Size(554, 17);
            this.m_sslblActionCount.Spring = true;
            this.m_sslblActionCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_splitter
            // 
            m_splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            m_splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            m_splitter.Location = new System.Drawing.Point(0, 25);
            m_splitter.Name = "m_splitter";
            // 
            // m_splitter.Panel1
            // 
            m_splitter.Panel1.Controls.Add(this.m_lvClients);
            // 
            // m_splitter.Panel2
            // 
            m_splitter.Panel2.Controls.Add(this.m_lvActions);
            m_splitter.Size = new System.Drawing.Size(569, 342);
            m_splitter.SplitterDistance = 189;
            m_splitter.TabIndex = 2;
            // 
            // m_lvClients
            // 
            this.m_lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colClientID,
            m_colStatus});
            this.m_lvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvClients.FullRowSelect = true;
            this.m_lvClients.Location = new System.Drawing.Point(0, 0);
            this.m_lvClients.Name = "m_lvClients";
            this.m_lvClients.Size = new System.Drawing.Size(189, 342);
            this.m_lvClients.TabIndex = 0;
            this.m_lvClients.UseCompatibleStateImageBehavior = false;
            this.m_lvClients.View = System.Windows.Forms.View.Details;
            // 
            // m_colClientID
            // 
            m_colClientID.Text = "Client";
            m_colClientID.Width = 85;
            // 
            // m_colStatus
            // 
            m_colStatus.Text = "Statut";
            // 
            // m_lvActions
            // 
            this.m_lvActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_colAction,
            m_colDate});
            this.m_lvActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvActions.FullRowSelect = true;
            this.m_lvActions.Location = new System.Drawing.Point(0, 0);
            this.m_lvActions.Name = "m_lvActions";
            this.m_lvActions.Size = new System.Drawing.Size(376, 342);
            this.m_lvActions.TabIndex = 0;
            this.m_lvActions.UseCompatibleStateImageBehavior = false;
            this.m_lvActions.View = System.Windows.Forms.View.Details;
            // 
            // m_colAction
            // 
            m_colAction.Text = "Action";
            m_colAction.Width = 119;
            // 
            // m_colDate
            // 
            m_colDate.Text = "Ajoutée le";
            m_colDate.Width = 213;
            // 
            // RunOnceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 389);
            this.Controls.Add(m_splitter);
            this.Controls.Add(m_statusStrip);
            this.Controls.Add(m_toolstrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RunOnceWindow";
            this.Text = "Gestionnaire RunOnce";
            m_toolstrip.ResumeLayout(false);
            m_toolstrip.PerformLayout();
            m_statusStrip.ResumeLayout(false);
            m_statusStrip.PerformLayout();
            m_splitter.Panel1.ResumeLayout(false);
            m_splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_splitter)).EndInit();
            m_splitter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripStatusLabel m_sslblActionCount;
        private System.Windows.Forms.ToolStripButton m_tsbDelete;
        private System.Windows.Forms.ListView m_lvClients;
        private System.Windows.Forms.ListView m_lvActions;
    }
}