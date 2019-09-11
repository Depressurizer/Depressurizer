namespace Depressurizer.AutoCats {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_UserScore));
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
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // groupPresets
            // 
            resources.ApplyResources(this.groupPresets, "groupPresets");
            this.groupPresets.Controls.Add(this.cmbPresets);
            this.groupPresets.Controls.Add(this.cmdApplyPreset);
            this.groupPresets.Name = "groupPresets";
            this.groupPresets.TabStop = false;
            // 
            // cmbPresets
            // 
            this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresets.FormattingEnabled = true;
            resources.ApplyResources(this.cmbPresets, "cmbPresets");
            this.cmbPresets.Name = "cmbPresets";
            // 
            // cmdApplyPreset
            // 
            resources.ApplyResources(this.cmdApplyPreset, "cmdApplyPreset");
            this.cmdApplyPreset.Name = "cmdApplyPreset";
            this.cmdApplyPreset.UseVisualStyleBackColor = true;
            this.cmdApplyPreset.Click += new System.EventHandler(this.cmdApplyPreset_Click);
            // 
            // grpRules
            // 
            resources.ApplyResources(this.grpRules, "grpRules");
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
            this.grpRules.Name = "grpRules";
            this.grpRules.TabStop = false;
            // 
            // helpRules
            // 
            resources.ApplyResources(this.helpRules, "helpRules");
            this.helpRules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpRules.Name = "helpRules";
            // 
            // numRuleMaxReviews
            // 
            this.numRuleMaxReviews.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.numRuleMaxReviews, "numRuleMaxReviews");
            this.numRuleMaxReviews.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numRuleMaxReviews.Name = "numRuleMaxReviews";
            // 
            // lblRuleMaxReviews
            // 
            resources.ApplyResources(this.lblRuleMaxReviews, "lblRuleMaxReviews");
            this.lblRuleMaxReviews.Name = "lblRuleMaxReviews";
            // 
            // numRuleMinReviews
            // 
            this.numRuleMinReviews.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            resources.ApplyResources(this.numRuleMinReviews, "numRuleMinReviews");
            this.numRuleMinReviews.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numRuleMinReviews.Name = "numRuleMinReviews";
            // 
            // lblRuleMinReviews
            // 
            resources.ApplyResources(this.lblRuleMinReviews, "lblRuleMinReviews");
            this.lblRuleMinReviews.Name = "lblRuleMinReviews";
            // 
            // cmdRuleDown
            // 
            resources.ApplyResources(this.cmdRuleDown, "cmdRuleDown");
            this.cmdRuleDown.Name = "cmdRuleDown";
            this.cmdRuleDown.UseVisualStyleBackColor = true;
            this.cmdRuleDown.Click += new System.EventHandler(this.cmdRuleDown_Click);
            // 
            // cmdRuleUp
            // 
            resources.ApplyResources(this.cmdRuleUp, "cmdRuleUp");
            this.cmdRuleUp.Name = "cmdRuleUp";
            this.cmdRuleUp.UseVisualStyleBackColor = true;
            this.cmdRuleUp.Click += new System.EventHandler(this.cmdRuleUp_Click);
            // 
            // cmdRuleRemove
            // 
            resources.ApplyResources(this.cmdRuleRemove, "cmdRuleRemove");
            this.cmdRuleRemove.Name = "cmdRuleRemove";
            this.cmdRuleRemove.UseVisualStyleBackColor = true;
            this.cmdRuleRemove.Click += new System.EventHandler(this.cmdRuleRemove_Click);
            // 
            // cmdRuleAdd
            // 
            resources.ApplyResources(this.cmdRuleAdd, "cmdRuleAdd");
            this.cmdRuleAdd.Name = "cmdRuleAdd";
            this.cmdRuleAdd.UseVisualStyleBackColor = true;
            this.cmdRuleAdd.Click += new System.EventHandler(this.cmdRuleAdd_Click);
            // 
            // numRuleMinScore
            // 
            resources.ApplyResources(this.numRuleMinScore, "numRuleMinScore");
            this.numRuleMinScore.Name = "numRuleMinScore";
            // 
            // numRuleMaxScore
            // 
            resources.ApplyResources(this.numRuleMaxScore, "numRuleMaxScore");
            this.numRuleMaxScore.Name = "numRuleMaxScore";
            // 
            // txtRuleName
            // 
            resources.ApplyResources(this.txtRuleName, "txtRuleName");
            this.txtRuleName.Name = "txtRuleName";
            // 
            // lblRuleMinScore
            // 
            resources.ApplyResources(this.lblRuleMinScore, "lblRuleMinScore");
            this.lblRuleMinScore.Name = "lblRuleMinScore";
            // 
            // lblRuleName
            // 
            resources.ApplyResources(this.lblRuleName, "lblRuleName");
            this.lblRuleName.Name = "lblRuleName";
            // 
            // lblRuleMaxScore
            // 
            resources.ApplyResources(this.lblRuleMaxScore, "lblRuleMaxScore");
            this.lblRuleMaxScore.Name = "lblRuleMaxScore";
            // 
            // lstRules
            // 
            resources.ApplyResources(this.lstRules, "lstRules");
            this.lstRules.FormattingEnabled = true;
            this.lstRules.Name = "lstRules";
            this.lstRules.SelectedIndexChanged += new System.EventHandler(this.lstRules_SelectedIndexChanged);
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
            // helpUseWilsonScore
            // 
            resources.ApplyResources(this.helpUseWilsonScore, "helpUseWilsonScore");
            this.helpUseWilsonScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpUseWilsonScore.Name = "helpUseWilsonScore";
            // 
            // chkUseWilsonScore
            // 
            resources.ApplyResources(this.chkUseWilsonScore, "chkUseWilsonScore");
            this.chkUseWilsonScore.Name = "chkUseWilsonScore";
            this.chkUseWilsonScore.UseVisualStyleBackColor = true;
            // 
            // AutoCatConfigPanel_UserScore
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_UserScore";
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
