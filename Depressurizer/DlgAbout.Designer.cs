namespace Depressurizer {
    partial class DlgAbout {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cmdClose = new System.Windows.Forms.Button();
            this.flowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lnkHomepage = new System.Windows.Forms.LinkLabel();
            this.lnkLicense = new System.Windows.Forms.LinkLabel();
            this.lnkNDesk = new System.Windows.Forms.LinkLabel();
            this.flowLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(7, 146);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(444, 23);
            this.cmdClose.TabIndex = 0;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // flowLayout
            // 
            this.flowLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayout.AutoScroll = true;
            this.flowLayout.Controls.Add(this.lblName);
            this.flowLayout.Controls.Add(this.lblVersion);
            this.flowLayout.Controls.Add(this.lnkHomepage);
            this.flowLayout.Controls.Add(this.lnkLicense);
            this.flowLayout.Controls.Add(this.lnkNDesk);
            this.flowLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayout.Location = new System.Drawing.Point(7, 7);
            this.flowLayout.Name = "flowLayout";
            this.flowLayout.Size = new System.Drawing.Size(444, 133);
            this.flowLayout.TabIndex = 5;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(71, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Depressurizer";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(3, 17);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(13, 13);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "v";
            // 
            // lnkHomepage
            // 
            this.lnkHomepage.AutoSize = true;
            this.lnkHomepage.LinkArea = new System.Windows.Forms.LinkArea(18, 40);
            this.lnkHomepage.Location = new System.Drawing.Point(3, 38);
            this.lnkHomepage.Margin = new System.Windows.Forms.Padding(3, 0, 3, 12);
            this.lnkHomepage.Name = "lnkHomepage";
            this.lnkHomepage.Size = new System.Drawing.Size(99, 17);
            this.lnkHomepage.TabIndex = 2;
            this.lnkHomepage.Text = "Project homepage:";
            this.lnkHomepage.UseCompatibleTextRendering = true;
            this.lnkHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHomepage_LinkClicked);
            // 
            // lnkLicense
            // 
            this.lnkLicense.AutoSize = true;
            this.lnkLicense.LinkArea = new System.Windows.Forms.LinkArea(52, 26);
            this.lnkLicense.Location = new System.Drawing.Point(3, 67);
            this.lnkLicense.Margin = new System.Windows.Forms.Padding(3, 0, 3, 12);
            this.lnkLicense.Name = "lnkLicense";
            this.lnkLicense.Size = new System.Drawing.Size(404, 17);
            this.lnkLicense.TabIndex = 3;
            this.lnkLicense.TabStop = true;
            this.lnkLicense.Text = "Depressurizer is distributed under version 3 of the GNU General Public License.";
            this.lnkLicense.UseCompatibleTextRendering = true;
            this.lnkLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLicense_LinkClicked);
            // 
            // lnkNDesk
            // 
            this.lnkNDesk.AutoSize = true;
            this.lnkNDesk.LinkArea = new System.Windows.Forms.LinkArea(79, 21);
            this.lnkNDesk.Location = new System.Drawing.Point(3, 96);
            this.lnkNDesk.Name = "lnkNDesk";
            this.lnkNDesk.Size = new System.Drawing.Size(411, 30);
            this.lnkNDesk.TabIndex = 4;
            this.lnkNDesk.TabStop = true;
            this.lnkNDesk.Text = "This program incorporates the NDesk Options module, copyright (C) 2008 Novell (ht" +
    "tp://www.novell.com)";
            this.lnkNDesk.UseCompatibleTextRendering = true;
            this.lnkNDesk.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNDesk_LinkClicked);
            // 
            // DlgAbout
            // 
            this.AcceptButton = this.cmdClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(458, 176);
            this.ControlBox = false;
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.flowLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DlgAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Depressurizer";
            this.Load += new System.EventHandler(this.DlgAbout_Load);
            this.flowLayout.ResumeLayout(false);
            this.flowLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.FlowLayoutPanel flowLayout;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.LinkLabel lnkHomepage;
        private System.Windows.Forms.LinkLabel lnkLicense;
        private System.Windows.Forms.LinkLabel lnkNDesk;
    }
}