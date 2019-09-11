namespace Depressurizer.AutoCats {
    partial class AutoCatConfigPanel_Hltb {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Hltb));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.helpUnknown = new System.Windows.Forms.Label();
            this.txtUnknownText = new System.Windows.Forms.TextBox();
            this.lblUnknownText = new System.Windows.Forms.Label();
            this.chkIncludeUnknown = new System.Windows.Forms.CheckBox();
            this.grpRules = new System.Windows.Forms.GroupBox();
            this.lblRuleTimeType = new System.Windows.Forms.Label();
            this.cmbTimeType = new System.Windows.Forms.ComboBox();
            this.helpRules = new System.Windows.Forms.Label();
            this.cmdRuleDown = new System.Windows.Forms.Button();
            this.cmdRuleUp = new System.Windows.Forms.Button();
            this.cmdRuleRemove = new System.Windows.Forms.Button();
            this.cmdRuleAdd = new System.Windows.Forms.Button();
            this.numRuleMinTime = new System.Windows.Forms.NumericUpDown();
            this.numRuleMaxTime = new System.Windows.Forms.NumericUpDown();
            this.txtRuleName = new System.Windows.Forms.TextBox();
            this.lblRuleMinTime = new System.Windows.Forms.Label();
            this.lblRuleName = new System.Windows.Forms.Label();
            this.lblRuleMaxTime = new System.Windows.Forms.Label();
            this.lstRules = new System.Windows.Forms.ListBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.grpRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxTime)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.helpUnknown);
            this.grpMain.Controls.Add(this.txtUnknownText);
            this.grpMain.Controls.Add(this.lblUnknownText);
            this.grpMain.Controls.Add(this.chkIncludeUnknown);
            this.grpMain.Controls.Add(this.grpRules);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.txtPrefix);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
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
            // grpRules
            // 
            resources.ApplyResources(this.grpRules, "grpRules");
            this.grpRules.Controls.Add(this.lblRuleTimeType);
            this.grpRules.Controls.Add(this.cmbTimeType);
            this.grpRules.Controls.Add(this.helpRules);
            this.grpRules.Controls.Add(this.cmdRuleDown);
            this.grpRules.Controls.Add(this.cmdRuleUp);
            this.grpRules.Controls.Add(this.cmdRuleRemove);
            this.grpRules.Controls.Add(this.cmdRuleAdd);
            this.grpRules.Controls.Add(this.numRuleMinTime);
            this.grpRules.Controls.Add(this.numRuleMaxTime);
            this.grpRules.Controls.Add(this.txtRuleName);
            this.grpRules.Controls.Add(this.lblRuleMinTime);
            this.grpRules.Controls.Add(this.lblRuleName);
            this.grpRules.Controls.Add(this.lblRuleMaxTime);
            this.grpRules.Controls.Add(this.lstRules);
            this.grpRules.Name = "grpRules";
            this.grpRules.TabStop = false;
            // 
            // lblRuleTimeType
            // 
            resources.ApplyResources(this.lblRuleTimeType, "lblRuleTimeType");
            this.lblRuleTimeType.Name = "lblRuleTimeType";
            // 
            // cmbTimeType
            // 
            this.cmbTimeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbTimeType, "cmbTimeType");
            this.cmbTimeType.Name = "cmbTimeType";
            // 
            // helpRules
            // 
            resources.ApplyResources(this.helpRules, "helpRules");
            this.helpRules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpRules.Name = "helpRules";
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
            // numRuleMinTime
            // 
            this.numRuleMinTime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            resources.ApplyResources(this.numRuleMinTime, "numRuleMinTime");
            this.numRuleMinTime.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numRuleMinTime.Name = "numRuleMinTime";
            // 
            // numRuleMaxTime
            // 
            this.numRuleMaxTime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            resources.ApplyResources(this.numRuleMaxTime, "numRuleMaxTime");
            this.numRuleMaxTime.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numRuleMaxTime.Name = "numRuleMaxTime";
            // 
            // txtRuleName
            // 
            resources.ApplyResources(this.txtRuleName, "txtRuleName");
            this.txtRuleName.Name = "txtRuleName";
            // 
            // lblRuleMinTime
            // 
            resources.ApplyResources(this.lblRuleMinTime, "lblRuleMinTime");
            this.lblRuleMinTime.Name = "lblRuleMinTime";
            // 
            // lblRuleName
            // 
            resources.ApplyResources(this.lblRuleName, "lblRuleName");
            this.lblRuleName.Name = "lblRuleName";
            // 
            // lblRuleMaxTime
            // 
            resources.ApplyResources(this.lblRuleMaxTime, "lblRuleMaxTime");
            this.lblRuleMaxTime.Name = "lblRuleMaxTime";
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
            // AutoCatConfigPanel_Hltb
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Hltb";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpRules.ResumeLayout(false);
            this.grpRules.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.GroupBox grpRules;
        private System.Windows.Forms.Button cmdRuleDown;
        private System.Windows.Forms.Button cmdRuleUp;
        private System.Windows.Forms.Button cmdRuleRemove;
        private System.Windows.Forms.Button cmdRuleAdd;
        private System.Windows.Forms.NumericUpDown numRuleMinTime;
        private System.Windows.Forms.NumericUpDown numRuleMaxTime;
        private System.Windows.Forms.TextBox txtRuleName;
        private System.Windows.Forms.Label lblRuleMinTime;
        private System.Windows.Forms.Label lblRuleName;
        private System.Windows.Forms.Label lblRuleMaxTime;
        private System.Windows.Forms.ListBox lstRules;
        private System.Windows.Forms.Label helpRules;
        private System.Windows.Forms.Label lblRuleTimeType;
        private System.Windows.Forms.ComboBox cmbTimeType;
        private System.Windows.Forms.Label helpUnknown;
        private System.Windows.Forms.TextBox txtUnknownText;
        private System.Windows.Forms.Label lblUnknownText;
        private System.Windows.Forms.CheckBox chkIncludeUnknown;
    }
}
