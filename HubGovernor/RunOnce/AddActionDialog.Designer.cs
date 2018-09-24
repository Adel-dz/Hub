namespace DGD.HubGovernor.RunOnce
{
    partial class AddActionDialog
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
            System.Windows.Forms.GroupBox m_grpClients;
            System.Windows.Forms.Label m_lblClients;
            System.Windows.Forms.GroupBox m_grpActions;
            System.Windows.Forms.Label m_lblActions;
            System.Windows.Forms.Button m_btnCancel;
            this.m_lbClients = new System.Windows.Forms.CheckedListBox();
            this.m_lbActions = new System.Windows.Forms.CheckedListBox();
            this.m_btnAdd = new System.Windows.Forms.Button();
            this.m_btnToggleSelection = new System.Windows.Forms.Button();
            m_grpClients = new System.Windows.Forms.GroupBox();
            m_lblClients = new System.Windows.Forms.Label();
            m_grpActions = new System.Windows.Forms.GroupBox();
            m_lblActions = new System.Windows.Forms.Label();
            m_btnCancel = new System.Windows.Forms.Button();
            m_grpClients.SuspendLayout();
            m_grpActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_grpClients
            // 
            m_grpClients.Controls.Add(this.m_btnToggleSelection);
            m_grpClients.Controls.Add(this.m_lbClients);
            m_grpClients.Controls.Add(m_lblClients);
            m_grpClients.Location = new System.Drawing.Point(12, 12);
            m_grpClients.Name = "m_grpClients";
            m_grpClients.Size = new System.Drawing.Size(198, 290);
            m_grpClients.TabIndex = 0;
            m_grpClients.TabStop = false;
            m_grpClients.Text = " Clients ";
            // 
            // m_lbClients
            // 
            this.m_lbClients.CheckOnClick = true;
            this.m_lbClients.FormatString = "X";
            this.m_lbClients.FormattingEnabled = true;
            this.m_lbClients.Location = new System.Drawing.Point(10, 56);
            this.m_lbClients.Name = "m_lbClients";
            this.m_lbClients.Size = new System.Drawing.Size(182, 199);
            this.m_lbClients.Sorted = true;
            this.m_lbClients.TabIndex = 1;
            // 
            // m_lblClients
            // 
            m_lblClients.Location = new System.Drawing.Point(7, 20);
            m_lblClients.Name = "m_lblClients";
            m_lblClients.Size = new System.Drawing.Size(185, 32);
            m_lblClients.TabIndex = 0;
            m_lblClients.Text = "Cochez l’ensemble des clients à qui vous désirez appliquer des actions.";
            // 
            // m_grpActions
            // 
            m_grpActions.Controls.Add(this.m_lbActions);
            m_grpActions.Controls.Add(m_lblActions);
            m_grpActions.Location = new System.Drawing.Point(216, 12);
            m_grpActions.Name = "m_grpActions";
            m_grpActions.Size = new System.Drawing.Size(200, 290);
            m_grpActions.TabIndex = 1;
            m_grpActions.TabStop = false;
            m_grpActions.Text = " Actions ";
            // 
            // m_lbActions
            // 
            this.m_lbActions.FormattingEnabled = true;
            this.m_lbActions.Location = new System.Drawing.Point(10, 56);
            this.m_lbActions.Name = "m_lbActions";
            this.m_lbActions.Size = new System.Drawing.Size(184, 214);
            this.m_lbActions.TabIndex = 1;
            // 
            // m_lblActions
            // 
            m_lblActions.AutoSize = true;
            m_lblActions.Location = new System.Drawing.Point(7, 20);
            m_lblActions.Name = "m_lblActions";
            m_lblActions.Size = new System.Drawing.Size(154, 13);
            m_lblActions.TabIndex = 0;
            m_lblActions.Text = "Cochez les actions à appliquer.";
            // 
            // m_btnCancel
            // 
            m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            m_btnCancel.Location = new System.Drawing.Point(341, 318);
            m_btnCancel.Name = "m_btnCancel";
            m_btnCancel.Size = new System.Drawing.Size(75, 23);
            m_btnCancel.TabIndex = 3;
            m_btnCancel.Text = "Annuler";
            m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnAdd.Enabled = false;
            this.m_btnAdd.Location = new System.Drawing.Point(259, 318);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(75, 23);
            this.m_btnAdd.TabIndex = 2;
            this.m_btnAdd.Text = "Ajouter";
            this.m_btnAdd.UseVisualStyleBackColor = true;
            // 
            // m_btnToggleSelection
            // 
            this.m_btnToggleSelection.Location = new System.Drawing.Point(40, 261);
            this.m_btnToggleSelection.Name = "m_btnToggleSelection";
            this.m_btnToggleSelection.Size = new System.Drawing.Size(118, 23);
            this.m_btnToggleSelection.TabIndex = 2;
            this.m_btnToggleSelection.Text = "Inverser la sélection";
            this.m_btnToggleSelection.UseVisualStyleBackColor = true;
            // 
            // AddActionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 353);
            this.Controls.Add(m_btnCancel);
            this.Controls.Add(this.m_btnAdd);
            this.Controls.Add(m_grpActions);
            this.Controls.Add(m_grpClients);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddActionDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajouter des actions";
            m_grpClients.ResumeLayout(false);
            m_grpActions.ResumeLayout(false);
            m_grpActions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox m_lbClients;
        private System.Windows.Forms.CheckedListBox m_lbActions;
        private System.Windows.Forms.Button m_btnAdd;
        private System.Windows.Forms.Button m_btnToggleSelection;
    }
}