namespace Depressurizer {
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.cbGroupNumbers = new System.Windows.Forms.CheckBox();
            this.cbSkipThe = new System.Windows.Forms.CheckBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.chkgroupNonEnglishCharacters = new System.Windows.Forms.CheckBox();
            this.txtGroupNonEnglishCharactersText = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
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
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(504, 374);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit Name AutoCat";
            // 
            // cbGroupNumbers
            // 
            this.cbGroupNumbers.AutoSize = true;
            this.cbGroupNumbers.Checked = true;
            this.cbGroupNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGroupNumbers.Location = new System.Drawing.Point(29, 85);
            this.cbGroupNumbers.Name = "cbGroupNumbers";
            this.cbGroupNumbers.Size = new System.Drawing.Size(143, 17);
            this.cbGroupNumbers.TabIndex = 7;
            this.cbGroupNumbers.Text = "Group all numbers to \"#\"";
            this.cbGroupNumbers.UseVisualStyleBackColor = true;
            // 
            // cbSkipThe
            // 
            this.cbSkipThe.AutoSize = true;
            this.cbSkipThe.Checked = true;
            this.cbSkipThe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSkipThe.Location = new System.Drawing.Point(29, 62);
            this.cbSkipThe.Name = "cbSkipThe";
            this.cbSkipThe.Size = new System.Drawing.Size(79, 17);
            this.cbSkipThe.TabIndex = 6;
            this.cbSkipThe.Text = "Skip \"The\"";
            this.cbSkipThe.UseVisualStyleBackColor = true;
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
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(26, 39);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.lblPrefix.TabIndex = 3;
            this.lblPrefix.Text = "Prefix:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(68, 36);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.txtPrefix.TabIndex = 4;
            // 
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // chkgroupNonEnglishCharacters
            // 
            this.chkgroupNonEnglishCharacters.AutoSize = true;
            this.chkgroupNonEnglishCharacters.Checked = true;
            this.chkgroupNonEnglishCharacters.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkgroupNonEnglishCharacters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.chkgroupNonEnglishCharacters.Location = new System.Drawing.Point(29, 108);
            this.chkgroupNonEnglishCharacters.Name = "chkgroupNonEnglishCharacters";
            this.chkgroupNonEnglishCharacters.Size = new System.Drawing.Size(320, 17);
            this.chkgroupNonEnglishCharacters.TabIndex = 8;
            this.chkgroupNonEnglishCharacters.Text = "Group all non-English alphabet, non-numeric characters under ";
            this.chkgroupNonEnglishCharacters.UseVisualStyleBackColor = true;
            // 
            // txtGroupNonEnglishCharactersText
            // 
            this.txtGroupNonEnglishCharactersText.Location = new System.Drawing.Point(345, 105);
            this.txtGroupNonEnglishCharactersText.Name = "txtGroupNonEnglishCharactersText";
            this.txtGroupNonEnglishCharactersText.Size = new System.Drawing.Size(50, 20);
            this.txtGroupNonEnglishCharactersText.TabIndex = 9;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblDescription.Location = new System.Drawing.Point(80, 16);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(270, 15);
            this.lblDescription.TabIndex = 10;
            this.lblDescription.Text = "Categorizes games based on their starting letter.";
            // 
            // AutoCatConfigPanel_Name
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Name";
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
        private System.Windows.Forms.CheckBox cbGroupNumbers;
        private System.Windows.Forms.CheckBox cbSkipThe;
        private System.Windows.Forms.CheckBox chkgroupNonEnglishCharacters;
        private System.Windows.Forms.TextBox txtGroupNonEnglishCharactersText;
        private System.Windows.Forms.Label lblDescription;
    }
}
