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
            System.Windows.Forms.StatusStrip m_statusStrip;
            System.Windows.Forms.ColumnHeader m_colAction;
            System.Windows.Forms.ColumnHeader m_colDate;
            System.Windows.Forms.ToolStripButton m_tsbAddActions;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunOnceWindow));
            this.listView1 = new System.Windows.Forms.ListView();
            this.m_collient = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_sslblActionCount = new System.Windows.Forms.ToolStripStatusLabel();
            m_toolstrip = new System.Windows.Forms.ToolStrip();
            m_statusStrip = new System.Windows.Forms.StatusStrip();
            m_colAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            m_tsbAddActions = new System.Windows.Forms.ToolStripButton();
            m_toolstrip.SuspendLayout();
            m_statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolstrip
            // 
            m_toolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            m_tsbAddActions});
            m_toolstrip.Location = new System.Drawing.Point(0, 0);
            m_toolstrip.Name = "m_toolstrip";
            m_toolstrip.Size = new System.Drawing.Size(569, 25);
            m_toolstrip.TabIndex = 0;
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
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_collient,
            m_colAction,
            m_colDate});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(569, 342);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // m_collient
            // 
            this.m_collient.Text = "Client";
            this.m_collient.Width = 97;
            // 
            // m_colAction
            // 
            m_colAction.Text = "Action";
            m_colAction.Width = 193;
            // 
            // m_colDate
            // 
            m_colDate.Text = "Date";
            m_colDate.Width = 242;
            // 
            // m_tsbAddActions
            // 
            m_tsbAddActions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            m_tsbAddActions.Image = global::DGD.HubGovernor.Properties.Resources.add_actions_16;
            m_tsbAddActions.ImageTransparentColor = System.Drawing.Color.Magenta;
            m_tsbAddActions.Name = "m_tsbAddActions";
            m_tsbAddActions.Size = new System.Drawing.Size(23, 22);
            m_tsbAddActions.Text = "Ajouter des actions..";
            // 
            // m_sslblActionCount
            // 
            this.m_sslblActionCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_sslblActionCount.Name = "m_sslblActionCount";
            this.m_sslblActionCount.Size = new System.Drawing.Size(554, 17);
            this.m_sslblActionCount.Spring = true;
            this.m_sslblActionCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RunOnceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 389);
            this.Controls.Add(this.listView1);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader m_collient;
        private System.Windows.Forms.ToolStripStatusLabel m_sslblActionCount;
    }
}