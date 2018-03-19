namespace DGD.Hub.SpotView
{
    partial class SpotView
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
            System.Windows.Forms.ToolStrip m_toolStrip;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.Label m_lblOrigin;
            System.Windows.Forms.Label m_lblIncoterm;
            System.Windows.Forms.Label m_lblDate;
            System.Windows.Forms.Label m_lblSubHeading;
            System.Windows.Forms.Label m_lblInfos;
            this.m_tsbSearch = new System.Windows.Forms.ToolStripButton();
            this.m_tsbToggleDetails = new System.Windows.Forms.ToolStripButton();
            this.m_tsbToggleView = new System.Windows.Forms.ToolStripButton();
            this.m_tsbTogglePanel = new System.Windows.Forms.ToolStripButton();
            this.m_searchPanel = new System.Windows.Forms.Panel();
            this.m_lblCountryInfo = new System.Windows.Forms.Label();
            this.m_cbOrigin = new System.Windows.Forms.ComboBox();
            this.m_cbIncoterm = new System.Windows.Forms.ComboBox();
            this.m_dtpSpotDate = new System.Windows.Forms.DateTimePicker();
            this.m_tbSubHeading = new System.Windows.Forms.TextBox();
            this.m_lvSearchResult = new System.Windows.Forms.ListView();
            m_toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_lblOrigin = new System.Windows.Forms.Label();
            m_lblIncoterm = new System.Windows.Forms.Label();
            m_lblDate = new System.Windows.Forms.Label();
            m_lblSubHeading = new System.Windows.Forms.Label();
            m_lblInfos = new System.Windows.Forms.Label();
            m_toolStrip.SuspendLayout();
            this.m_searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_toolStrip
            // 
            m_toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbSearch,
            toolStripSeparator1,
            this.m_tsbToggleDetails,
            this.m_tsbToggleView,
            toolStripSeparator2,
            this.m_tsbTogglePanel});
            m_toolStrip.Location = new System.Drawing.Point(0, 0);
            m_toolStrip.Name = "m_toolStrip";
            m_toolStrip.Size = new System.Drawing.Size(866, 39);
            m_toolStrip.TabIndex = 2;
            // 
            // m_tsbSearch
            // 
            this.m_tsbSearch.AutoToolTip = false;
            this.m_tsbSearch.Image = global::DGD.Hub.Properties.Resources.search_32;
            this.m_tsbSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbSearch.Name = "m_tsbSearch";
            this.m_tsbSearch.Size = new System.Drawing.Size(102, 36);
            this.m_tsbSearch.Text = "Rechercher";
            this.m_tsbSearch.ToolTipText = "Lancer la recherche des prix spot selon les critères introduits.";
            this.m_tsbSearch.Click += new System.EventHandler(this.Search_Click);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // m_tsbToggleDetails
            // 
            this.m_tsbToggleDetails.AutoToolTip = false;
            this.m_tsbToggleDetails.Image = global::DGD.Hub.Properties.Resources.toggle_details_32;
            this.m_tsbToggleDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbToggleDetails.Name = "m_tsbToggleDetails";
            this.m_tsbToggleDetails.Size = new System.Drawing.Size(147, 36);
            this.m_tsbToggleDetails.Text = "Afficher plus détails";
            this.m_tsbToggleDetails.ToolTipText = "Basculer entre un affichage étendu ou réduit des informations relatives aux les p" +
    "rix spots.";
            this.m_tsbToggleDetails.Click += new System.EventHandler(this.ToggleDetails_Click);
            // 
            // m_tsbToggleView
            // 
            this.m_tsbToggleView.AutoToolTip = false;
            this.m_tsbToggleView.Image = global::DGD.Hub.Properties.Resources.toggle_view_32;
            this.m_tsbToggleView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbToggleView.Name = "m_tsbToggleView";
            this.m_tsbToggleView.Size = new System.Drawing.Size(95, 36);
            this.m_tsbToggleView.Text = "Mosaïque";
            this.m_tsbToggleView.ToolTipText = "Basculer l’affichage entre une vue en mosaïque et une vue en liste. ";
            this.m_tsbToggleView.Click += new System.EventHandler(this.ToggleView_Click);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // m_tsbTogglePanel
            // 
            this.m_tsbTogglePanel.Image = global::DGD.Hub.Properties.Resources.Collapse_32;
            this.m_tsbTogglePanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbTogglePanel.Name = "m_tsbTogglePanel";
            this.m_tsbTogglePanel.Size = new System.Drawing.Size(159, 36);
            this.m_tsbTogglePanel.Text = "Masquer le formulaire";
            this.m_tsbTogglePanel.ToolTipText = "Affiche ou masque le panneau de saisie vous permettant ainsi d’avoir plus d’espac" +
    "e pour visualiser les résultats de la recherche.";
            this.m_tsbTogglePanel.Click += new System.EventHandler(this.TogglePanel_Click);
            // 
            // m_lblOrigin
            // 
            m_lblOrigin.AutoSize = true;
            m_lblOrigin.Location = new System.Drawing.Point(358, 108);
            m_lblOrigin.Name = "m_lblOrigin";
            m_lblOrigin.Size = new System.Drawing.Size(40, 13);
            m_lblOrigin.TabIndex = 9;
            m_lblOrigin.Text = "Origine";
            // 
            // m_lblIncoterm
            // 
            m_lblIncoterm.AutoSize = true;
            m_lblIncoterm.Location = new System.Drawing.Point(358, 80);
            m_lblIncoterm.Name = "m_lblIncoterm";
            m_lblIncoterm.Size = new System.Drawing.Size(51, 13);
            m_lblIncoterm.TabIndex = 7;
            m_lblIncoterm.Text = "Incoterm:";
            // 
            // m_lblDate
            // 
            m_lblDate.AutoSize = true;
            m_lblDate.Location = new System.Drawing.Point(15, 108);
            m_lblDate.Name = "m_lblDate";
            m_lblDate.Size = new System.Drawing.Size(86, 13);
            m_lblDate.TabIndex = 5;
            m_lblDate.Text = "Date facturation:";
            // 
            // m_lblSubHeading
            // 
            m_lblSubHeading.AutoSize = true;
            m_lblSubHeading.Location = new System.Drawing.Point(15, 80);
            m_lblSubHeading.Name = "m_lblSubHeading";
            m_lblSubHeading.Size = new System.Drawing.Size(110, 13);
            m_lblSubHeading.TabIndex = 3;
            m_lblSubHeading.Text = "Sous-position tarifaire:";
            // 
            // m_lblInfos
            // 
            m_lblInfos.AutoSize = true;
            m_lblInfos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_lblInfos.Location = new System.Drawing.Point(15, 20);
            m_lblInfos.Name = "m_lblInfos";
            m_lblInfos.Size = new System.Drawing.Size(848, 13);
            m_lblInfos.TabIndex = 17;
            m_lblInfos.Text = "Recherchez les prix spots par sous-positions tarifaires. Vous devez, obligatoirem" +
    "ent, fournir la sous-position tarifaire ainsi que la date de facturation.";
            // 
            // m_searchPanel
            // 
            this.m_searchPanel.Controls.Add(m_lblInfos);
            this.m_searchPanel.Controls.Add(this.m_lblCountryInfo);
            this.m_searchPanel.Controls.Add(this.m_cbOrigin);
            this.m_searchPanel.Controls.Add(this.m_cbIncoterm);
            this.m_searchPanel.Controls.Add(this.m_dtpSpotDate);
            this.m_searchPanel.Controls.Add(m_lblOrigin);
            this.m_searchPanel.Controls.Add(m_lblIncoterm);
            this.m_searchPanel.Controls.Add(m_lblDate);
            this.m_searchPanel.Controls.Add(this.m_tbSubHeading);
            this.m_searchPanel.Controls.Add(m_lblSubHeading);
            this.m_searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_searchPanel.Location = new System.Drawing.Point(0, 39);
            this.m_searchPanel.Name = "m_searchPanel";
            this.m_searchPanel.Size = new System.Drawing.Size(866, 157);
            this.m_searchPanel.TabIndex = 0;
            // 
            // m_lblCountryInfo
            // 
            this.m_lblCountryInfo.AutoSize = true;
            this.m_lblCountryInfo.Location = new System.Drawing.Point(505, 109);
            this.m_lblCountryInfo.Name = "m_lblCountryInfo";
            this.m_lblCountryInfo.Size = new System.Drawing.Size(10, 13);
            this.m_lblCountryInfo.TabIndex = 16;
            this.m_lblCountryInfo.Text = "-";
            // 
            // m_cbOrigin
            // 
            this.m_cbOrigin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbOrigin.FormattingEnabled = true;
            this.m_cbOrigin.Location = new System.Drawing.Point(414, 104);
            this.m_cbOrigin.Name = "m_cbOrigin";
            this.m_cbOrigin.Size = new System.Drawing.Size(77, 21);
            this.m_cbOrigin.TabIndex = 3;
            this.m_cbOrigin.SelectedIndexChanged += new System.EventHandler(this.Origin_SelectedIndexChanged);
            // 
            // m_cbIncoterm
            // 
            this.m_cbIncoterm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cbIncoterm.FormattingEnabled = true;
            this.m_cbIncoterm.Location = new System.Drawing.Point(414, 76);
            this.m_cbIncoterm.Name = "m_cbIncoterm";
            this.m_cbIncoterm.Size = new System.Drawing.Size(121, 21);
            this.m_cbIncoterm.TabIndex = 2;
            // 
            // m_dtpSpotDate
            // 
            this.m_dtpSpotDate.Location = new System.Drawing.Point(132, 108);
            this.m_dtpSpotDate.Name = "m_dtpSpotDate";
            this.m_dtpSpotDate.Size = new System.Drawing.Size(168, 20);
            this.m_dtpSpotDate.TabIndex = 1;
            // 
            // m_tbSubHeading
            // 
            this.m_tbSubHeading.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.m_tbSubHeading.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.m_tbSubHeading.Location = new System.Drawing.Point(132, 76);
            this.m_tbSubHeading.Name = "m_tbSubHeading";
            this.m_tbSubHeading.Size = new System.Drawing.Size(168, 20);
            this.m_tbSubHeading.TabIndex = 0;
            // 
            // m_lvSearchResult
            // 
            this.m_lvSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lvSearchResult.FullRowSelect = true;
            this.m_lvSearchResult.Location = new System.Drawing.Point(0, 196);
            this.m_lvSearchResult.Name = "m_lvSearchResult";
            this.m_lvSearchResult.Size = new System.Drawing.Size(866, 317);
            this.m_lvSearchResult.TabIndex = 1;
            this.m_lvSearchResult.UseCompatibleStateImageBehavior = false;
            this.m_lvSearchResult.View = System.Windows.Forms.View.Details;
            this.m_lvSearchResult.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.SearchResult_ColumnClick);
            // 
            // SpotView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_lvSearchResult);
            this.Controls.Add(this.m_searchPanel);
            this.Controls.Add(m_toolStrip);
            this.Name = "SpotView";
            this.Size = new System.Drawing.Size(866, 513);
            m_toolStrip.ResumeLayout(false);
            m_toolStrip.PerformLayout();
            this.m_searchPanel.ResumeLayout(false);
            this.m_searchPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel m_searchPanel;
        private System.Windows.Forms.TextBox m_tbSubHeading;
        private System.Windows.Forms.ToolStripButton m_tsbSearch;
        private System.Windows.Forms.ToolStripButton m_tsbTogglePanel;
        private System.Windows.Forms.ComboBox m_cbIncoterm;
        private System.Windows.Forms.DateTimePicker m_dtpSpotDate;
        private System.Windows.Forms.ListView m_lvSearchResult;
        private System.Windows.Forms.ComboBox m_cbOrigin;
        private System.Windows.Forms.Label m_lblCountryInfo;
        private System.Windows.Forms.ToolStripButton m_tsbToggleDetails;
        private System.Windows.Forms.ToolStripButton m_tsbToggleView;
    }
}
