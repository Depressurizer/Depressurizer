namespace Depressurizer {
    partial class AutoCatConfigPanel_UserScore {
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
            this.groupPresets = new System.Windows.Forms.GroupBox();
            this.cmbPresets = new System.Windows.Forms.ComboBox();
            this.cmdApplyPreset = new System.Windows.Forms.Button();
            this.grpRules = new System.Windows.Forms.GroupBox();
            this.helpRules = new System.Windows.Forms.Label();
            this.numRuleMaxReviews = new System.Windows.Forms.NumericUpDown();
            this.lblRuleMaxReviews = new System.Windows.Forms.Label();
            this.numRuleMinReviews = new System.Windows.Forms.NumericUpDown();
            this.lblRuleMinReviews = new System.Windows.Forms.Label();
            this.cmdRuleDown = new System.Windows.Forms.Button();
            this.cmdRuleUp = new System.Windows.Forms.Button();
            this.cmdRuleRemove = new System.Windows.Forms.Button();
            this.cmdRuleAdd = new System.Windows.Forms.Button();
            this.numRuleMinScore = new System.Windows.Forms.NumericUpDown();
            this.numRuleMaxScore = new System.Windows.Forms.NumericUpDown();
            this.txtRuleName = new System.Windows.Forms.TextBox();
            this.lblRuleMinScore = new System.Windows.Forms.Label();
            this.lblRuleName = new System.Windows.Forms.Label();
            this.lblRuleMaxScore = new System.Windows.Forms.Label();
            this.lstRules = new System.Windows.Forms.ListBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.helpUseWilsonScore = new System.Windows.Forms.Label();
            this.chkUseWilsonScore = new System.Windows.Forms.CheckBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.groupPresets.SuspendLayout();
            this.grpRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxReviews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinReviews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxScore)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.groupPresets);
            this.grpMain.Controls.Add(this.grpRules);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.txtPrefix);
            this.grpMain.Controls.Add(this.helpUseWilsonScore);
            this.grpMain.Controls.Add(this.chkUseWilsonScore);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(517, 425);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit UserScore AutoCat";
            // 
            // groupPresets
            // 
            this.groupPresets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPresets.Controls.Add(this.cmbPresets);
            this.groupPresets.Controls.Add(this.cmdApplyPreset);
            this.groupPresets.Location = new System.Drawing.Point(6, 366);
            this.groupPresets.Name = "groupPresets";
            this.groupPresets.Size = new System.Drawing.Size(505, 53);
            this.groupPresets.TabIndex = 5;
            this.groupPresets.TabStop = false;
            this.groupPresets.Text = "Rule Presets";
            // 
            // cmbPresets
            // 
            this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresets.FormattingEnabled = true;
            this.cmbPresets.Location = new System.Drawing.Point(6, 19);
            this.cmbPresets.Name = "cmbPresets";
            this.cmbPresets.Size = new System.Drawing.Size(280, 21);
            this.cmbPresets.TabIndex = 0;
            // 
            // cmdApplyPreset
            // 
            this.cmdApplyPreset.Location = new System.Drawing.Point(292, 18);
            this.cmdApplyPreset.Name = "cmdApplyPreset";
            this.cmdApplyPreset.Size = new System.Drawing.Size(86, 23);
            this.cmdApplyPreset.TabIndex = 1;
            this.cmdApplyPreset.Text = "Apply Preset";
            this.cmdApplyPreset.UseVisualStyleBackColor = true;
            this.cmdApplyPreset.Click += new System.EventHandler(this.cmdApplyPreset_Click);
            // 
            // grpRules
            // 
            this.grpRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRules.Controls.Add(this.helpRules);
            this.grpRules.Controls.Add(this.numRuleMaxReviews);
            this.grpRules.Controls.Add(this.lblRuleMaxReviews);
            this.grpRules.Controls.Add(this.numRuleMinReviews);
            this.grpRules.Controls.Add(this.lblRuleMinReviews);
            this.grpRules.Controls.Add(this.cmdRuleDown);
            this.grpRules.Controls.Add(this.cmdRuleUp);
            this.grpRules.Controls.Add(this.cmdRuleRemove);
            this.grpRules.Controls.Add(this.cmdRuleAdd);
            this.grpRules.Controls.Add(this.numRuleMinScore);
            this.grpRules.Controls.Add(this.numRuleMaxScore);
            this.grpRules.Controls.Add(this.txtRuleName);
            this.grpRules.Controls.Add(this.lblRuleMinScore);
            this.grpRules.Controls.Add(this.lblRuleName);
            this.grpRules.Controls.Add(this.lblRuleMaxScore);
            this.grpRules.Controls.Add(this.lstRules);
            this.grpRules.Location = new System.Drawing.Point(6, 45);
            this.grpRules.Name = "grpRules";
            this.grpRules.Size = new System.Drawing.Size(505, 315);
            this.grpRules.TabIndex = 4;
            this.grpRules.TabStop = false;
            this.grpRules.Text = "Rules";
            // 
            // helpRules
            // 
            this.helpRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpRules.AutoSize = true;
            this.helpRules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpRules.Location = new System.Drawing.Point(484, 0);
            this.helpRules.Name = "helpRules";
            this.helpRules.Size = new System.Drawing.Size(15, 15);
            this.helpRules.TabIndex = 0;
            this.helpRules.Text = "?";
            // 
            // numRuleMaxReviews
            // 
            this.numRuleMaxReviews.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRuleMaxReviews.Location = new System.Drawing.Point(319, 123);
            this.numRuleMaxReviews.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numRuleMaxReviews.Name = "numRuleMaxReviews";
            this.numRuleMaxReviews.Size = new System.Drawing.Size(59, 20);
            this.numRuleMaxReviews.TabIndex = 15;
            this.numRuleMaxReviews.ThousandsSeparator = true;
            // 
            // lblRuleMaxReviews
            // 
            this.lblRuleMaxReviews.AutoSize = true;
            this.lblRuleMaxReviews.Location = new System.Drawing.Point(186, 125);
            this.lblRuleMaxReviews.Name = "lblRuleMaxReviews";
            this.lblRuleMaxReviews.Size = new System.Drawing.Size(78, 26);
            this.lblRuleMaxReviews.TabIndex = 14;
            this.lblRuleMaxReviews.Text = "Max Reviews:\r\n(0 for unlimited)";
            // 
            // numRuleMinReviews
            // 
            this.numRuleMinReviews.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRuleMinReviews.Location = new System.Drawing.Point(319, 97);
            this.numRuleMinReviews.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numRuleMinReviews.Name = "numRuleMinReviews";
            this.numRuleMinReviews.Size = new System.Drawing.Size(59, 20);
            this.numRuleMinReviews.TabIndex = 13;
            this.numRuleMinReviews.ThousandsSeparator = true;
            // 
            // lblRuleMinReviews
            // 
            this.lblRuleMinReviews.AutoSize = true;
            this.lblRuleMinReviews.Location = new System.Drawing.Point(186, 99);
            this.lblRuleMinReviews.Name = "lblRuleMinReviews";
            this.lblRuleMinReviews.Size = new System.Drawing.Size(71, 13);
            this.lblRuleMinReviews.TabIndex = 12;
            this.lblRuleMinReviews.Text = "Min Reviews:";
            // 
            // cmdRuleDown
            // 
            this.cmdRuleDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRuleDown.Location = new System.Drawing.Point(94, 286);
            this.cmdRuleDown.Name = "cmdRuleDown";
            this.cmdRuleDown.Size = new System.Drawing.Size(86, 23);
            this.cmdRuleDown.TabIndex = 5;
            this.cmdRuleDown.Text = "Down";
            this.cmdRuleDown.UseVisualStyleBackColor = true;
            this.cmdRuleDown.Click += new System.EventHandler(this.cmdRuleDown_Click);
            // 
            // cmdRuleUp
            // 
            this.cmdRuleUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRuleUp.Location = new System.Drawing.Point(6, 286);
            this.cmdRuleUp.Name = "cmdRuleUp";
            this.cmdRuleUp.Size = new System.Drawing.Size(86, 23);
            this.cmdRuleUp.TabIndex = 4;
            this.cmdRuleUp.Text = "Up";
            this.cmdRuleUp.UseVisualStyleBackColor = true;
            this.cmdRuleUp.Click += new System.EventHandler(this.cmdRuleUp_Click);
            // 
            // cmdRuleRemove
            // 
            this.cmdRuleRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRuleRemove.Location = new System.Drawing.Point(6, 261);
            this.cmdRuleRemove.Name = "cmdRuleRemove";
            this.cmdRuleRemove.Size = new System.Drawing.Size(174, 23);
            this.cmdRuleRemove.TabIndex = 3;
            this.cmdRuleRemove.Text = "Remove Rule";
            this.cmdRuleRemove.UseVisualStyleBackColor = true;
            this.cmdRuleRemove.Click += new System.EventHandler(this.cmdRuleRemove_Click);
            // 
            // cmdRuleAdd
            // 
            this.cmdRuleAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRuleAdd.Location = new System.Drawing.Point(6, 236);
            this.cmdRuleAdd.Name = "cmdRuleAdd";
            this.cmdRuleAdd.Size = new System.Drawing.Size(174, 23);
            this.cmdRuleAdd.TabIndex = 2;
            this.cmdRuleAdd.Text = "Add Rule";
            this.cmdRuleAdd.UseVisualStyleBackColor = true;
            this.cmdRuleAdd.Click += new System.EventHandler(this.cmdRuleAdd_Click);
            // 
            // numRuleMinScore
            // 
            this.numRuleMinScore.Location = new System.Drawing.Point(319, 45);
            this.numRuleMinScore.Name = "numRuleMinScore";
            this.numRuleMinScore.Size = new System.Drawing.Size(59, 20);
            this.numRuleMinScore.TabIndex = 9;
            // 
            // numRuleMaxScore
            // 
            this.numRuleMaxScore.Location = new System.Drawing.Point(319, 71);
            this.numRuleMaxScore.Name = "numRuleMaxScore";
            this.numRuleMaxScore.Size = new System.Drawing.Size(59, 20);
            this.numRuleMaxScore.TabIndex = 11;
            // 
            // txtRuleName
            // 
            this.txtRuleName.Location = new System.Drawing.Point(248, 19);
            this.txtRuleName.Name = "txtRuleName";
            this.txtRuleName.Size = new System.Drawing.Size(130, 20);
            this.txtRuleName.TabIndex = 7;
            // 
            // lblRuleMinScore
            // 
            this.lblRuleMinScore.AutoSize = true;
            this.lblRuleMinScore.Location = new System.Drawing.Point(186, 47);
            this.lblRuleMinScore.Name = "lblRuleMinScore";
            this.lblRuleMinScore.Size = new System.Drawing.Size(58, 13);
            this.lblRuleMinScore.TabIndex = 8;
            this.lblRuleMinScore.Text = "Min Score:";
            // 
            // lblRuleName
            // 
            this.lblRuleName.AutoSize = true;
            this.lblRuleName.Location = new System.Drawing.Point(186, 22);
            this.lblRuleName.Name = "lblRuleName";
            this.lblRuleName.Size = new System.Drawing.Size(38, 13);
            this.lblRuleName.TabIndex = 6;
            this.lblRuleName.Text = "Name:";
            // 
            // lblRuleMaxScore
            // 
            this.lblRuleMaxScore.AutoSize = true;
            this.lblRuleMaxScore.Location = new System.Drawing.Point(186, 73);
            this.lblRuleMaxScore.Name = "lblRuleMaxScore";
            this.lblRuleMaxScore.Size = new System.Drawing.Size(61, 13);
            this.lblRuleMaxScore.TabIndex = 10;
            this.lblRuleMaxScore.Text = "Max Score:";
            // 
            // lstRules
            // 
            this.lstRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstRules.FormattingEnabled = true;
            this.lstRules.IntegralHeight = false;
            this.lstRules.Location = new System.Drawing.Point(6, 19);
            this.lstRules.Name = "lstRules";
            this.lstRules.Size = new System.Drawing.Size(174, 215);
            this.lstRules.TabIndex = 1;
            this.lstRules.SelectedIndexChanged += new System.EventHandler(this.lstRules_SelectedIndexChanged);
            // 
            // helpPrefix
            // 
            this.helpPrefix.AutoSize = true;
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Location = new System.Drawing.Point(238, 22);
            this.helpPrefix.Name = "helpPrefix";
            this.helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.helpPrefix.TabIndex = 2;
            this.helpPrefix.Text = "?";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.lblPrefix.TabIndex = 0;
            this.lblPrefix.Text = "Prefix:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(67, 19);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.txtPrefix.TabIndex = 1;
            // 
            // helpUseWilsonScore
            // 
            this.helpUseWilsonScore.AutoSize = true;
            this.helpUseWilsonScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpUseWilsonScore.Location = new System.Drawing.Point(391, 22);
            this.helpUseWilsonScore.Name = "helpUseWilsonScore";
            this.helpUseWilsonScore.Size = new System.Drawing.Size(15, 15);
            this.helpUseWilsonScore.TabIndex = 7;
            this.helpUseWilsonScore.Text = "?";
            // 
            // chkUseWilsonScore
            // 
            this.chkUseWilsonScore.AutoSize = true;
            this.chkUseWilsonScore.Location = new System.Drawing.Point(274, 21);
            this.chkUseWilsonScore.Name = "chkUseWilsonScore";
            this.chkUseWilsonScore.Size = new System.Drawing.Size(111, 17);
            this.chkUseWilsonScore.TabIndex = 6;
            this.chkUseWilsonScore.Text = "Use Wilson Score";
            this.chkUseWilsonScore.UseVisualStyleBackColor = true;
            // 
            // AutoCatConfigPanel_UserScore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_UserScore";
            this.Size = new System.Drawing.Size(517, 425);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.groupPresets.ResumeLayout(false);
            this.grpRules.ResumeLayout(false);
            this.grpRules.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxReviews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinReviews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxScore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label helpUseWilsonScore;
        private System.Windows.Forms.CheckBox chkUseWilsonScore;
        private System.Windows.Forms.GroupBox grpRules;
        private System.Windows.Forms.Button cmdRuleDown;
        private System.Windows.Forms.Button cmdRuleUp;
        private System.Windows.Forms.Button cmdRuleRemove;
        private System.Windows.Forms.Button cmdRuleAdd;
        private System.Windows.Forms.NumericUpDown numRuleMinScore;
        private System.Windows.Forms.NumericUpDown numRuleMaxScore;
        private System.Windows.Forms.TextBox txtRuleName;
        private System.Windows.Forms.Label lblRuleMinScore;
        private System.Windows.Forms.Label lblRuleName;
        private System.Windows.Forms.Label lblRuleMaxScore;
        private System.Windows.Forms.ListBox lstRules;
        private System.Windows.Forms.NumericUpDown numRuleMaxReviews;
        private System.Windows.Forms.Label lblRuleMaxReviews;
        private System.Windows.Forms.NumericUpDown numRuleMinReviews;
        private System.Windows.Forms.Label lblRuleMinReviews;
        private System.Windows.Forms.GroupBox groupPresets;
        private System.Windows.Forms.ComboBox cmbPresets;
        private System.Windows.Forms.Button cmdApplyPreset;
        private System.Windows.Forms.Label helpRules;
    }
}
