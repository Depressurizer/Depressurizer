namespace Depressurizer.AutoCats {
    partial class AutoCatConfigPanel_Name {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Name));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtGroupNonEnglishCharactersText = new System.Windows.Forms.TextBox();
            this.chkgroupNonEnglishCharacters = new System.Windows.Forms.CheckBox();
            this.cbGroupNumbers = new System.Windows.Forms.CheckBox();
            this.cbSkipThe = new System.Windows.Forms.CheckBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.lblDescription);
            this.grpMain.Controls.Add(this.txtGroupNonEnglishCharactersText);
            this.grpMain.Controls.Add(this.chkgroupNonEnglishCharacters);
            this.grpMain.Controls.Add(this.cbGroupNumbers);
            this.grpMain.Controls.Add(this.cbSkipThe);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.txtPrefix);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // txtGroupNonEnglishCharactersText
            // 
            resources.ApplyResources(this.txtGroupNonEnglishCharactersText, "txtGroupNonEnglishCharactersText");
            this.txtGroupNonEnglishCharactersText.Name = "txtGroupNonEnglishCharactersText";
            // 
            // chkgroupNonEnglishCharacters
            // 
            resources.ApplyResources(this.chkgroupNonEnglishCharacters, "chkgroupNonEnglishCharacters");
            this.chkgroupNonEnglishCharacters.Checked = true;
            this.chkgroupNonEnglishCharacters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkgroupNonEnglishCharacters.Name = "chkgroupNonEnglishCharacters";
            this.chkgroupNonEnglishCharacters.UseVisualStyleBackColor = true;
            // 
            // cbGroupNumbers
            // 
            resources.ApplyResources(this.cbGroupNumbers, "cbGroupNumbers");
            this.cbGroupNumbers.Checked = true;
            this.cbGroupNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGroupNumbers.Name = "cbGroupNumbers";
            this.cbGroupNumbers.UseVisualStyleBackColor = true;
            // 
            // cbSkipThe
            // 
            resources.ApplyResources(this.cbSkipThe, "cbSkipThe");
            this.cbSkipThe.Checked = true;
            this.cbSkipThe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSkipThe.Name = "cbSkipThe";
            this.cbSkipThe.UseVisualStyleBackColor = true;
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
            // AutoCatConfigPanel_Name
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Name";
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
        private System.Windows.Forms.CheckBox cbGroupNumbers;
        private System.Windows.Forms.CheckBox cbSkipThe;
        private System.Windows.Forms.CheckBox chkgroupNonEnglishCharacters;
        private System.Windows.Forms.TextBox txtGroupNonEnglishCharactersText;
        private System.Windows.Forms.Label lblDescription;
    }
}
