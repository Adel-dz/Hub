namespace DGD.HubGovernor.Opts
{
    partial class InputSettingsPage
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
            System.Windows.Forms.Label m_lblInputInfo;
            System.Windows.Forms.PictureBox m_pbLogo;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputSettingsPage));
            this.m_rbPreserveCase = new System.Windows.Forms.RadioButton();
            this.m_rbUpperCase = new System.Windows.Forms.RadioButton();
            this.m_rbLowerCase = new System.Windows.Forms.RadioButton();
            m_lblInputInfo = new System.Windows.Forms.Label();
            m_pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblInputInfo
            // 
            m_lblInputInfo.AutoSize = true;
            m_lblInputInfo.Location = new System.Drawing.Point(14, 76);
            m_lblInputInfo.Name = "m_lblInputInfo";
            m_lblInputInfo.Size = new System.Drawing.Size(152, 13);
            m_lblInputInfo.TabIndex = 0;
            m_lblInputInfo.Text = "Lors de la saisie des données :";
            // 
            // m_pbLogo
            // 
            m_pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            m_pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("m_pbLogo.Image")));
            m_pbLogo.Location = new System.Drawing.Point(178, 3);
            m_pbLogo.Name = "m_pbLogo";
            m_pbLogo.Size = new System.Drawing.Size(258, 52);
            m_pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            m_pbLogo.TabIndex = 4;
            m_pbLogo.TabStop = false;
            // 
            // m_rbPreserveCase
            // 
            this.m_rbPreserveCase.AutoSize = true;
            this.m_rbPreserveCase.Checked = true;
            this.m_rbPreserveCase.Location = new System.Drawing.Point(45, 102);
            this.m_rbPreserveCase.Name = "m_rbPreserveCase";
            this.m_rbPreserveCase.Size = new System.Drawing.Size(85, 17);
            this.m_rbPreserveCase.TabIndex = 1;
            this.m_rbPreserveCase.TabStop = true;
            this.m_rbPreserveCase.Text = "Ne rien faire.";
            this.m_rbPreserveCase.UseVisualStyleBackColor = true;
            // 
            // m_rbUpperCase
            // 
            this.m_rbUpperCase.AutoSize = true;
            this.m_rbUpperCase.Location = new System.Drawing.Point(45, 125);
            this.m_rbUpperCase.Name = "m_rbUpperCase";
            this.m_rbUpperCase.Size = new System.Drawing.Size(186, 17);
            this.m_rbUpperCase.TabIndex = 2;
            this.m_rbUpperCase.Text = "Transformer le texte en majuscule.";
            this.m_rbUpperCase.UseVisualStyleBackColor = true;
            // 
            // m_rbLowerCase
            // 
            this.m_rbLowerCase.AutoSize = true;
            this.m_rbLowerCase.Location = new System.Drawing.Point(45, 148);
            this.m_rbLowerCase.Name = "m_rbLowerCase";
            this.m_rbLowerCase.Size = new System.Drawing.Size(182, 17);
            this.m_rbLowerCase.TabIndex = 3;
            this.m_rbLowerCase.Text = "Transformer le texte en miniscule.";
            this.m_rbLowerCase.UseVisualStyleBackColor = true;
            // 
            // InputSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(m_pbLogo);
            this.Controls.Add(this.m_rbLowerCase);
            this.Controls.Add(this.m_rbUpperCase);
            this.Controls.Add(this.m_rbPreserveCase);
            this.Controls.Add(m_lblInputInfo);
            this.Name = "InputSettingsPage";
            this.Size = new System.Drawing.Size(439, 182);
            ((System.ComponentModel.ISupportInitialize)(m_pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton m_rbPreserveCase;
        private System.Windows.Forms.RadioButton m_rbUpperCase;
        private System.Windows.Forms.RadioButton m_rbLowerCase;
    }
}
