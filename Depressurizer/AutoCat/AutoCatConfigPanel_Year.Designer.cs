namespace Depressurizer {
    partial class AutoCatConfigPanel_Year {
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
            this.helpPrefix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.chkIncludeUnknown = new System.Windows.Forms.CheckBox();
            this.lblUnknownText = new System.Windows.Forms.Label();
            this.txtUnknownText = new System.Windows.Forms.TextBox();
            this.helpUnknown = new System.Windows.Forms.Label();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.helpUnknown);
            this.grpMain.Controls.Add(this.txtUnknownText);
            this.grpMain.Controls.Add(this.lblUnknownText);
            this.grpMain.Controls.Add(this.chkIncludeUnknown);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.txtPrefix);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(504, 374);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit Year AutoCat";
            // 
            // helpPrefix
            // 
            this.helpPrefix.AutoSize = true;
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Location = new System.Drawing.Point(238, 22);
            this.helpPrefix.Name = "helpPrefix";
            this.helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.helpPrefix.TabIndex = 5;
            this.helpPrefix.Text = "?";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.lblPrefix.TabIndex = 3;
            this.lblPrefix.Text = "Prefix:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(67, 19);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.txtPrefix.TabIndex = 4;
            // 
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // chkIncludeUnknown
            // 
            this.chkIncludeUnknown.AutoSize = true;
            this.chkIncludeUnknown.Location = new System.Drawing.Point(28, 60);
            this.chkIncludeUnknown.Name = "chkIncludeUnknown";
            this.chkIncludeUnknown.Size = new System.Drawing.Size(110, 17);
            this.chkIncludeUnknown.TabIndex = 6;
            this.chkIncludeUnknown.Text = "Include Unknown";
            this.chkIncludeUnknown.UseVisualStyleBackColor = true;
            // 
            // lblUnknownText
            // 
            this.lblUnknownText.AutoSize = true;
            this.lblUnknownText.Location = new System.Drawing.Point(48, 86);
            this.lblUnknownText.Name = "lblUnknownText";
            this.lblUnknownText.Size = new System.Drawing.Size(76, 13);
            this.lblUnknownText.TabIndex = 7;
            this.lblUnknownText.Text = "Unknown text:";
            // 
            // txtUnknownText
            // 
            this.txtUnknownText.Location = new System.Drawing.Point(130, 83);
            this.txtUnknownText.Name = "txtUnknownText";
            this.txtUnknownText.Size = new System.Drawing.Size(159, 20);
            this.txtUnknownText.TabIndex = 8;
            // 
            // helpUnknown
            // 
            this.helpUnknown.AutoSize = true;
            this.helpUnknown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpUnknown.Location = new System.Drawing.Point(144, 61);
            this.helpUnknown.Name = "helpUnknown";
            this.helpUnknown.Size = new System.Drawing.Size(15, 15);
            this.helpUnknown.TabIndex = 9;
            this.helpUnknown.Text = "?";
            // 
            // AutoCatConfigPanel_Year
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Year";
            this.Size = new System.Drawing.Size(504, 374);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Label helpUnknown;
        private System.Windows.Forms.TextBox txtUnknownText;
        private System.Windows.Forms.Label lblUnknownText;
        private System.Windows.Forms.CheckBox chkIncludeUnknown;
    }
}
