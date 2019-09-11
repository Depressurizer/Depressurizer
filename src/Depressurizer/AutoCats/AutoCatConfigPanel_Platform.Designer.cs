namespace Depressurizer.AutoCats {
    partial class AutoCatConfigPanel_Platform {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Platform));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.chkboxPlatforms = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblPlatforms = new System.Windows.Forms.Label();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.chkboxPlatforms);
            this.grpMain.Controls.Add(this.panel1);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // chkboxPlatforms
            // 
            this.chkboxPlatforms.CheckOnClick = true;
            resources.ApplyResources(this.chkboxPlatforms, "chkboxPlatforms");
            this.chkboxPlatforms.FormattingEnabled = true;
            this.chkboxPlatforms.Items.AddRange(new object[] {
            resources.GetString("chkboxPlatforms.Items"),
            resources.GetString("chkboxPlatforms.Items1"),
            resources.GetString("chkboxPlatforms.Items2"),
            resources.GetString("chkboxPlatforms.Items3")});
            this.chkboxPlatforms.Name = "chkboxPlatforms";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.txtPrefix);
            this.panel1.Controls.Add(this.lblPrefix);
            this.panel1.Controls.Add(this.helpPrefix);
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.lblPlatforms);
            this.panel1.Name = "panel1";
            // 
            // txtPrefix
            // 
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            // 
            // lblPrefix
            // 
            resources.ApplyResources(this.lblPrefix, "lblPrefix");
            this.lblPrefix.Name = "lblPrefix";
            // 
            // helpPrefix
            // 
            resources.ApplyResources(this.helpPrefix, "helpPrefix");
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Name = "helpPrefix";
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // lblPlatforms
            // 
            resources.ApplyResources(this.lblPlatforms, "lblPlatforms");
            this.lblPlatforms.Name = "lblPlatforms";
            // 
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // AutoCatConfigPanel_Platform
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Platform";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.CheckedListBox chkboxPlatforms;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPlatforms;
    }
}
