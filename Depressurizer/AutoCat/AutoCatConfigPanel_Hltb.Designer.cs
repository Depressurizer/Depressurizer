namespace Depressurizer {
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
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(517, 425);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit HLTB AutoCat";
            // 
            // helpUnknown
            // 
            this.helpUnknown.AutoSize = true;
            this.helpUnknown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpUnknown.Location = new System.Drawing.Point(144, 46);
            this.helpUnknown.Name = "helpUnknown";
            this.helpUnknown.Size = new System.Drawing.Size(15, 15);
            this.helpUnknown.TabIndex = 13;
            this.helpUnknown.Text = "?";
            // 
            // txtUnknownText
            // 
            this.txtUnknownText.Location = new System.Drawing.Point(130, 68);
            this.txtUnknownText.Name = "txtUnknownText";
            this.txtUnknownText.Size = new System.Drawing.Size(159, 20);
            this.txtUnknownText.TabIndex = 12;
            // 
            // lblUnknownText
            // 
            this.lblUnknownText.AutoSize = true;
            this.lblUnknownText.Location = new System.Drawing.Point(48, 71);
            this.lblUnknownText.Name = "lblUnknownText";
            this.lblUnknownText.Size = new System.Drawing.Size(76, 13);
            this.lblUnknownText.TabIndex = 11;
            this.lblUnknownText.Text = "Unknown text:";
            // 
            // chkIncludeUnknown
            // 
            this.chkIncludeUnknown.AutoSize = true;
            this.chkIncludeUnknown.Location = new System.Drawing.Point(28, 45);
            this.chkIncludeUnknown.Name = "chkIncludeUnknown";
            this.chkIncludeUnknown.Size = new System.Drawing.Size(110, 17);
            this.chkIncludeUnknown.TabIndex = 10;
            this.chkIncludeUnknown.Text = "Include Unknown";
            this.chkIncludeUnknown.UseVisualStyleBackColor = true;
            // 
            // grpRules
            // 
            this.grpRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.grpRules.Location = new System.Drawing.Point(6, 95);
            this.grpRules.Name = "grpRules";
            this.grpRules.Size = new System.Drawing.Size(505, 324);
            this.grpRules.TabIndex = 4;
            this.grpRules.TabStop = false;
            this.grpRules.Text = "Rules";
            // 
            // lblRuleTimeType
            // 
            this.lblRuleTimeType.AutoSize = true;
            this.lblRuleTimeType.Location = new System.Drawing.Point(186, 48);
            this.lblRuleTimeType.Name = "lblRuleTimeType";
            this.lblRuleTimeType.Size = new System.Drawing.Size(34, 13);
            this.lblRuleTimeType.TabIndex = 13;
            this.lblRuleTimeType.Text = "Type:";
            // 
            // cmbTimeType
            // 
            this.cmbTimeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeType.Location = new System.Drawing.Point(248, 45);
            this.cmbTimeType.Name = "cmbTimeType";
            this.cmbTimeType.Size = new System.Drawing.Size(130, 21);
            this.cmbTimeType.TabIndex = 12;
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
            // cmdRuleDown
            // 
            this.cmdRuleDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRuleDown.Location = new System.Drawing.Point(94, 295);
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
            this.cmdRuleUp.Location = new System.Drawing.Point(6, 295);
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
            this.cmdRuleRemove.Location = new System.Drawing.Point(6, 270);
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
            this.cmdRuleAdd.Location = new System.Drawing.Point(6, 245);
            this.cmdRuleAdd.Name = "cmdRuleAdd";
            this.cmdRuleAdd.Size = new System.Drawing.Size(174, 23);
            this.cmdRuleAdd.TabIndex = 2;
            this.cmdRuleAdd.Text = "Add Rule";
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
            this.numRuleMinTime.Location = new System.Drawing.Point(319, 74);
            this.numRuleMinTime.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numRuleMinTime.Name = "numRuleMinTime";
            this.numRuleMinTime.Size = new System.Drawing.Size(59, 20);
            this.numRuleMinTime.TabIndex = 9;
            // 
            // numRuleMaxTime
            // 
            this.numRuleMaxTime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numRuleMaxTime.Location = new System.Drawing.Point(319, 100);
            this.numRuleMaxTime.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numRuleMaxTime.Name = "numRuleMaxTime";
            this.numRuleMaxTime.Size = new System.Drawing.Size(59, 20);
            this.numRuleMaxTime.TabIndex = 11;
            // 
            // txtRuleName
            // 
            this.txtRuleName.Location = new System.Drawing.Point(248, 19);
            this.txtRuleName.Name = "txtRuleName";
            this.txtRuleName.Size = new System.Drawing.Size(130, 20);
            this.txtRuleName.TabIndex = 7;
            // 
            // lblRuleMinTime
            // 
            this.lblRuleMinTime.AutoSize = true;
            this.lblRuleMinTime.Location = new System.Drawing.Point(186, 76);
            this.lblRuleMinTime.Name = "lblRuleMinTime";
            this.lblRuleMinTime.Size = new System.Drawing.Size(58, 13);
            this.lblRuleMinTime.TabIndex = 8;
            this.lblRuleMinTime.Text = "Min Hours:";
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
            // lblRuleMaxTime
            // 
            this.lblRuleMaxTime.AutoSize = true;
            this.lblRuleMaxTime.Location = new System.Drawing.Point(186, 102);
            this.lblRuleMaxTime.Name = "lblRuleMaxTime";
            this.lblRuleMaxTime.Size = new System.Drawing.Size(78, 26);
            this.lblRuleMaxTime.TabIndex = 10;
            this.lblRuleMaxTime.Text = "Max Hours:\n(0 for unlimited)";
            // 
            // lstRules
            // 
            this.lstRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstRules.FormattingEnabled = true;
            this.lstRules.IntegralHeight = false;
            this.lstRules.Location = new System.Drawing.Point(6, 19);
            this.lstRules.Name = "lstRules";
            this.lstRules.Size = new System.Drawing.Size(174, 224);
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
            // AutoCatConfigPanel_Hltb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Hltb";
            this.Size = new System.Drawing.Size(517, 425);
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
