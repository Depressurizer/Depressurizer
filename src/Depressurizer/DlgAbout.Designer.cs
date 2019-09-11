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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgAbout));
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
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // flowLayout
            // 
            resources.ApplyResources(this.flowLayout, "flowLayout");
            this.flowLayout.Controls.Add(this.lblName);
            this.flowLayout.Controls.Add(this.lblVersion);
            this.flowLayout.Controls.Add(this.lnkHomepage);
            this.flowLayout.Controls.Add(this.lnkLicense);
            this.flowLayout.Controls.Add(this.lnkNDesk);
            this.flowLayout.Name = "flowLayout";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // lnkHomepage
            // 
            resources.ApplyResources(this.lnkHomepage, "lnkHomepage");
            this.lnkHomepage.Name = "lnkHomepage";
            this.lnkHomepage.UseCompatibleTextRendering = true;
            this.lnkHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHomepage_LinkClicked);
            // 
            // lnkLicense
            // 
            resources.ApplyResources(this.lnkLicense, "lnkLicense");
            this.lnkLicense.Name = "lnkLicense";
            this.lnkLicense.TabStop = true;
            this.lnkLicense.UseCompatibleTextRendering = true;
            this.lnkLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLicense_LinkClicked);
            // 
            // lnkNDesk
            // 
            resources.ApplyResources(this.lnkNDesk, "lnkNDesk");
            this.lnkNDesk.Name = "lnkNDesk";
            this.lnkNDesk.TabStop = true;
            this.lnkNDesk.UseCompatibleTextRendering = true;
            this.lnkNDesk.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNDesk_LinkClicked);
            // 
            // DlgAbout
            // 
            this.AcceptButton = this.cmdClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ControlBox = false;
            this.Controls.Add(this.flowLayout);
            this.Controls.Add(this.cmdClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DlgAbout";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.DlgAbout_Load);
            this.flowLayout.ResumeLayout(false);
            this.flowLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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