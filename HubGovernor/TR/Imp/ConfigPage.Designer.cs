namespace DGD.HubGovernor.TR.Imp
{
    partial class ConfigPage
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
            System.Windows.Forms.Label m_lblHeader;
            System.Windows.Forms.Label m_lblSeparator;
            System.Windows.Forms.Label m_lblShowLinesStart;
            System.Windows.Forms.Label m_lblShowLinesEnd;
            System.Windows.Forms.Label m_lblLinesToIgnoreEnd;
            System.Windows.Forms.Label m_lblLinesToIgnoreStart;
            System.Windows.Forms.GroupBox m_grpColumns;
            System.Windows.Forms.PictureBox m_pictureBox;
            this.m_btnMapColumn = new System.Windows.Forms.Button();
            this.m_lbColumns = new System.Windows.Forms.ListBox();
            this.m_tbSeparator = new System.Windows.Forms.TextBox();
            this.m_nudLinesToShow = new System.Windows.Forms.NumericUpDown();
            this.m_nudLinesToIgnore = new System.Windows.Forms.NumericUpDown();
            this.m_lvData = new System.Windows.Forms.ListView();
            m_lblHeader = new System.Windows.Forms.Label();
            m_lblSeparator = new System.Windows.Forms.Label();
            m_lblShowLinesStart = new System.Windows.Forms.Label();
            m_lblShowLinesEnd = new System.Windows.Forms.Label();
            m_lblLinesToIgnoreEnd = new System.Windows.Forms.Label();
            m_lblLinesToIgnoreStart = new System.Windows.Forms.Label();
            m_grpColumns = new System.Windows.Forms.GroupBox();
            m_pictureBox = new System.Windows.Forms.PictureBox();
            m_grpColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudLinesToShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudLinesToIgnore)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblHeader
            // 
            m_lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblHeader.Location = new System.Drawing.Point(87, 3);
            m_lblHeader.Name = "m_lblHeader";
            m_lblHeader.Size = new System.Drawing.Size(557, 64);
            m_lblHeader.TabIndex = 1;
            m_lblHeader.Text = "Vous êtes sur le point d’importer des données délimitées. Précisez le séparateur " +
    "de colonnes. Indiquez les colonnes à importer. Choisissez Suivant une fois prêts" +
    ".";
            m_lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblSeparator
            // 
            m_lblSeparator.AutoSize = true;
            m_lblSeparator.Location = new System.Drawing.Point(4, 83);
            m_lblSeparator.Name = "m_lblSeparator";
            m_lblSeparator.Size = new System.Drawing.Size(123, 13);
            m_lblSeparator.TabIndex = 2;
            m_lblSeparator.Text = "Séparateur de colonnes:";
            // 
            // m_lblShowLinesStart
            // 
            m_lblShowLinesStart.AutoSize = true;
            m_lblShowLinesStart.Location = new System.Drawing.Point(266, 82);
            m_lblShowLinesStart.Name = "m_lblShowLinesStart";
            m_lblShowLinesStart.Size = new System.Drawing.Size(80, 13);
            m_lblShowLinesStart.TabIndex = 4;
            m_lblShowLinesStart.Text = "Afficher au plus";
            // 
            // m_lblShowLinesEnd
            // 
            m_lblShowLinesEnd.AutoSize = true;
            m_lblShowLinesEnd.Location = new System.Drawing.Point(409, 82);
            m_lblShowLinesEnd.Name = "m_lblShowLinesEnd";
            m_lblShowLinesEnd.Size = new System.Drawing.Size(37, 13);
            m_lblShowLinesEnd.TabIndex = 6;
            m_lblShowLinesEnd.Text = "lignes.";
            // 
            // m_lblLinesToIgnoreEnd
            // 
            m_lblLinesToIgnoreEnd.AutoSize = true;
            m_lblLinesToIgnoreEnd.Location = new System.Drawing.Point(563, 82);
            m_lblLinesToIgnoreEnd.Name = "m_lblLinesToIgnoreEnd";
            m_lblLinesToIgnoreEnd.Size = new System.Drawing.Size(85, 13);
            m_lblLinesToIgnoreEnd.TabIndex = 9;
            m_lblLinesToIgnoreEnd.Text = "premières lignes.";
            // 
            // m_lblLinesToIgnoreStart
            // 
            m_lblLinesToIgnoreStart.AutoSize = true;
            m_lblLinesToIgnoreStart.Location = new System.Drawing.Point(463, 82);
            m_lblLinesToIgnoreStart.Name = "m_lblLinesToIgnoreStart";
            m_lblLinesToIgnoreStart.Size = new System.Drawing.Size(40, 13);
            m_lblLinesToIgnoreStart.TabIndex = 7;
            m_lblLinesToIgnoreStart.Text = "Ignorer";
            // 
            // m_grpColumns
            // 
            m_grpColumns.Controls.Add(this.m_btnMapColumn);
            m_grpColumns.Controls.Add(this.m_lbColumns);
            m_grpColumns.Location = new System.Drawing.Point(7, 116);
            m_grpColumns.Name = "m_grpColumns";
            m_grpColumns.Size = new System.Drawing.Size(161, 254);
            m_grpColumns.TabIndex = 10;
            m_grpColumns.TabStop = false;
            m_grpColumns.Text = " Colonnes ";
            // 
            // m_btnMapColumn
            // 
            this.m_btnMapColumn.Location = new System.Drawing.Point(7, 225);
            this.m_btnMapColumn.Name = "m_btnMapColumn";
            this.m_btnMapColumn.Size = new System.Drawing.Size(148, 23);
            this.m_btnMapColumn.TabIndex = 1;
            this.m_btnMapColumn.Text = "Définir";
            this.m_btnMapColumn.UseVisualStyleBackColor = true;
            this.m_btnMapColumn.Click += new System.EventHandler(this.MapColumn_Click);
            // 
            // m_lbColumns
            // 
            this.m_lbColumns.FormattingEnabled = true;
            this.m_lbColumns.Location = new System.Drawing.Point(7, 20);
            this.m_lbColumns.Name = "m_lbColumns";
            this.m_lbColumns.Size = new System.Drawing.Size(148, 199);
            this.m_lbColumns.Sorted = true;
            this.m_lbColumns.TabIndex = 0;
            this.m_lbColumns.DoubleClick += new System.EventHandler(this.ColumnsMapping_DoubleClick);
            // 
            // m_pictureBox
            // 
            m_pictureBox.Image = global::DGD.HubGovernor.Properties.Resources.Wizard_64;
            m_pictureBox.Location = new System.Drawing.Point(4, 3);
            m_pictureBox.Name = "m_pictureBox";
            m_pictureBox.Size = new System.Drawing.Size(64, 64);
            m_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pictureBox.TabIndex = 0;
            m_pictureBox.TabStop = false;
            // 
            // m_tbSeparator
            // 
            this.m_tbSeparator.Location = new System.Drawing.Point(133, 79);
            this.m_tbSeparator.MaxLength = 1;
            this.m_tbSeparator.Name = "m_tbSeparator";
            this.m_tbSeparator.Size = new System.Drawing.Size(35, 20);
            this.m_tbSeparator.TabIndex = 3;
            // 
            // m_nudLinesToShow
            // 
            this.m_nudLinesToShow.Location = new System.Drawing.Point(352, 78);
            this.m_nudLinesToShow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_nudLinesToShow.Name = "m_nudLinesToShow";
            this.m_nudLinesToShow.Size = new System.Drawing.Size(49, 20);
            this.m_nudLinesToShow.TabIndex = 5;
            this.m_nudLinesToShow.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // m_nudLinesToIgnore
            // 
            this.m_nudLinesToIgnore.Location = new System.Drawing.Point(508, 78);
            this.m_nudLinesToIgnore.Name = "m_nudLinesToIgnore";
            this.m_nudLinesToIgnore.Size = new System.Drawing.Size(49, 20);
            this.m_nudLinesToIgnore.TabIndex = 8;
            // 
            // m_lvData
            // 
            this.m_lvData.GridLines = true;
            this.m_lvData.Location = new System.Drawing.Point(175, 116);
            this.m_lvData.Name = "m_lvData";
            this.m_lvData.Size = new System.Drawing.Size(532, 248);
            this.m_lvData.TabIndex = 11;
            this.m_lvData.UseCompatibleStateImageBehavior = false;
            this.m_lvData.View = System.Windows.Forms.View.Details;
            this.m_lvData.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.Data_ColumnClick);
            // 
            // ConfigPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.m_lvData);
            this.Controls.Add(m_grpColumns);
            this.Controls.Add(m_lblLinesToIgnoreEnd);
            this.Controls.Add(this.m_nudLinesToIgnore);
            this.Controls.Add(m_lblLinesToIgnoreStart);
            this.Controls.Add(m_lblShowLinesEnd);
            this.Controls.Add(this.m_nudLinesToShow);
            this.Controls.Add(m_lblShowLinesStart);
            this.Controls.Add(this.m_tbSeparator);
            this.Controls.Add(m_lblSeparator);
            this.Controls.Add(m_lblHeader);
            this.Controls.Add(m_pictureBox);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "ConfigPage";
            this.Size = new System.Drawing.Size(710, 368);
            m_grpColumns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(m_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudLinesToShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_nudLinesToIgnore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbSeparator;
        private System.Windows.Forms.NumericUpDown m_nudLinesToShow;
        private System.Windows.Forms.NumericUpDown m_nudLinesToIgnore;
        private System.Windows.Forms.Button m_btnMapColumn;
        private System.Windows.Forms.ListBox m_lbColumns;
        private System.Windows.Forms.ListView m_lvData;
    }
}
