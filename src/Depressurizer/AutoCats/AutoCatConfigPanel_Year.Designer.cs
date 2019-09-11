namespace Depressurizer.AutoCats {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Year));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.grpGrouping = new System.Windows.Forms.GroupBox();
            this.radGroupHalf = new System.Windows.Forms.RadioButton();
            this.radGroupDec = new System.Windows.Forms.RadioButton();
            this.radGroupNone = new System.Windows.Forms.RadioButton();
            this.helpUnknown = new System.Windows.Forms.Label();
            this.txtUnknownText = new System.Windows.Forms.TextBox();
            this.lblUnknownText = new System.Windows.Forms.Label();
            this.chkIncludeUnknown = new System.Windows.Forms.CheckBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.grpGrouping.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.grpGrouping);
            this.grpMain.Controls.Add(this.helpUnknown);
            this.grpMain.Controls.Add(this.txtUnknownText);
            this.grpMain.Controls.Add(this.lblUnknownText);
            this.grpMain.Controls.Add(this.chkIncludeUnknown);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.txtPrefix);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // grpGrouping
            // 
            resources.ApplyResources(this.grpGrouping, "grpGrouping");
            this.grpGrouping.Controls.Add(this.radGroupHalf);
            this.grpGrouping.Controls.Add(this.radGroupDec);
            this.grpGrouping.Controls.Add(this.radGroupNone);
            this.grpGrouping.Name = "grpGrouping";
            this.grpGrouping.TabStop = false;
            // 
            // radGroupHalf
            // 
            resources.ApplyResources(this.radGroupHalf, "radGroupHalf");
            this.radGroupHalf.Name = "radGroupHalf";
            this.radGroupHalf.TabStop = true;
            this.radGroupHalf.UseVisualStyleBackColor = true;
            // 
            // radGroupDec
            // 
            resources.ApplyResources(this.radGroupDec, "radGroupDec");
            this.radGroupDec.Name = "radGroupDec";
            this.radGroupDec.TabStop = true;
            this.radGroupDec.UseVisualStyleBackColor = true;
            // 
            // radGroupNone
            // 
            resources.ApplyResources(this.radGroupNone, "radGroupNone");
            this.radGroupNone.Name = "radGroupNone";
            this.radGroupNone.TabStop = true;
            this.radGroupNone.UseVisualStyleBackColor = true;
            // 
            // helpUnknown
            // 
            resources.ApplyResources(this.helpUnknown, "helpUnknown");
            this.helpUnknown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpUnknown.Name = "helpUnknown";
            // 
            // txtUnknownText
            // 
            resources.ApplyResources(this.txtUnknownText, "txtUnknownText");
            this.txtUnknownText.Name = "txtUnknownText";
            // 
            // lblUnknownText
            // 
            resources.ApplyResources(this.lblUnknownText, "lblUnknownText");
            this.lblUnknownText.Name = "lblUnknownText";
            // 
            // chkIncludeUnknown
            // 
            resources.ApplyResources(this.chkIncludeUnknown, "chkIncludeUnknown");
            this.chkIncludeUnknown.Name = "chkIncludeUnknown";
            this.chkIncludeUnknown.UseVisualStyleBackColor = true;
            // 
            // helpPrefix
            // 
            resources.ApplyResources(this.helpPrefix, "helpPrefix");
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Name = "helpPrefix";
            // 
            // lblPrefix
            // 
            resources.ApplyResources(this.lblPrefix, "lblPrefix");
            this.lblPrefix.Name = "lblPrefix";
            // 
            // txtPrefix
            // 
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            // 
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // AutoCatConfigPanel_Year
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Year";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpGrouping.ResumeLayout(false);
            this.grpGrouping.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpGrouping;
        private System.Windows.Forms.RadioButton radGroupHalf;
        private System.Windows.Forms.RadioButton radGroupDec;
        private System.Windows.Forms.RadioButton radGroupNone;
    }
}
