namespace Depressurizer {
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
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(504, 374);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit Platform AutoCat";
            // 
            // chkboxPlatforms
            // 
            this.chkboxPlatforms.CheckOnClick = true;
            this.chkboxPlatforms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkboxPlatforms.FormattingEnabled = true;
            this.chkboxPlatforms.Items.AddRange(new object[] {
            "Windows",
            "Mac",
            "Linux",
            "SteamOS"});
            this.chkboxPlatforms.Location = new System.Drawing.Point(3, 100);
            this.chkboxPlatforms.Name = "chkboxPlatforms";
            this.chkboxPlatforms.Size = new System.Drawing.Size(498, 271);
            this.chkboxPlatforms.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.txtPrefix);
            this.panel1.Controls.Add(this.lblPrefix);
            this.panel1.Controls.Add(this.helpPrefix);
            this.panel1.Controls.Add(this.lblDescription);
            this.panel1.Controls.Add(this.lblPlatforms);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel1.Size = new System.Drawing.Size(498, 84);
            this.panel1.TabIndex = 12;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(68, 36);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.txtPrefix.TabIndex = 4;
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(26, 39);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.lblPrefix.TabIndex = 3;
            this.lblPrefix.Text = "Prefix:";
            // 
            // helpPrefix
            // 
            this.helpPrefix.AutoSize = true;
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Location = new System.Drawing.Point(239, 39);
            this.helpPrefix.Name = "helpPrefix";
            this.helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.helpPrefix.TabIndex = 5;
            this.helpPrefix.Text = "?";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblDescription.Location = new System.Drawing.Point(80, 16);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(282, 15);
            this.lblDescription.TabIndex = 10;
            this.lblDescription.Text = "Categorizes games based on supported platforms.";
            // 
            // lblPlatforms
            // 
            this.lblPlatforms.AutoSize = true;
            this.lblPlatforms.Location = new System.Drawing.Point(3, 68);
            this.lblPlatforms.Name = "lblPlatforms";
            this.lblPlatforms.Size = new System.Drawing.Size(97, 13);
            this.lblPlatforms.TabIndex = 11;
            this.lblPlatforms.Text = "Included Platforms:";
            // 
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // AutoCatConfigPanel_Platform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Platform";
            this.Size = new System.Drawing.Size(504, 374);
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
