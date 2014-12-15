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
            this.grpRules = new System.Windows.Forms.GroupBox();
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
            this.lblRuleMinReviews = new System.Windows.Forms.Label();
            this.numRuleMinReviews = new System.Windows.Forms.NumericUpDown();
            this.lblRuleMaxReviews = new System.Windows.Forms.Label();
            this.numRuleMaxReviews = new System.Windows.Forms.NumericUpDown();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.grpRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinReviews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxReviews)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMain
            // 
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
            this.grpMain.Text = "Edit UserScore AutoCat";
            // 
            // grpRules
            // 
            this.grpRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.grpRules.Size = new System.Drawing.Size(505, 374);
            this.grpRules.TabIndex = 3;
            this.grpRules.TabStop = false;
            this.grpRules.Text = "Rules";
            // 
            // cmdRuleDown
            // 
            this.cmdRuleDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRuleDown.Location = new System.Drawing.Point(95, 345);
            this.cmdRuleDown.Name = "cmdRuleDown";
            this.cmdRuleDown.Size = new System.Drawing.Size(85, 23);
            this.cmdRuleDown.TabIndex = 4;
            this.cmdRuleDown.Text = "Down";
            this.cmdRuleDown.UseVisualStyleBackColor = true;
            this.cmdRuleDown.Click += new System.EventHandler(this.cmdRuleDown_Click);
            // 
            // cmdRuleUp
            // 
            this.cmdRuleUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRuleUp.Location = new System.Drawing.Point(6, 345);
            this.cmdRuleUp.Name = "cmdRuleUp";
            this.cmdRuleUp.Size = new System.Drawing.Size(85, 23);
            this.cmdRuleUp.TabIndex = 3;
            this.cmdRuleUp.Text = "Up";
            this.cmdRuleUp.UseVisualStyleBackColor = true;
            this.cmdRuleUp.Click += new System.EventHandler(this.cmdRuleUp_Click);
            // 
            // cmdRuleRemove
            // 
            this.cmdRuleRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRuleRemove.Location = new System.Drawing.Point(6, 320);
            this.cmdRuleRemove.Name = "cmdRuleRemove";
            this.cmdRuleRemove.Size = new System.Drawing.Size(174, 23);
            this.cmdRuleRemove.TabIndex = 2;
            this.cmdRuleRemove.Text = "Remove Rule";
            this.cmdRuleRemove.UseVisualStyleBackColor = true;
            this.cmdRuleRemove.Click += new System.EventHandler(this.cmdRuleRemove_Click);
            // 
            // cmdRuleAdd
            // 
            this.cmdRuleAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRuleAdd.Location = new System.Drawing.Point(6, 294);
            this.cmdRuleAdd.Name = "cmdRuleAdd";
            this.cmdRuleAdd.Size = new System.Drawing.Size(174, 23);
            this.cmdRuleAdd.TabIndex = 1;
            this.cmdRuleAdd.Text = "Add Rule";
            this.cmdRuleAdd.UseVisualStyleBackColor = true;
            this.cmdRuleAdd.Click += new System.EventHandler(this.cmdRuleAdd_Click);
            // 
            // numRuleMinScore
            // 
            this.numRuleMinScore.Location = new System.Drawing.Point(319, 45);
            this.numRuleMinScore.Name = "numRuleMinScore";
            this.numRuleMinScore.Size = new System.Drawing.Size(59, 20);
            this.numRuleMinScore.TabIndex = 8;
            // 
            // numRuleMaxScore
            // 
            this.numRuleMaxScore.Location = new System.Drawing.Point(319, 71);
            this.numRuleMaxScore.Name = "numRuleMaxScore";
            this.numRuleMaxScore.Size = new System.Drawing.Size(59, 20);
            this.numRuleMaxScore.TabIndex = 10;
            // 
            // txtRuleName
            // 
            this.txtRuleName.Location = new System.Drawing.Point(248, 19);
            this.txtRuleName.Name = "txtRuleName";
            this.txtRuleName.Size = new System.Drawing.Size(130, 20);
            this.txtRuleName.TabIndex = 6;
            // 
            // lblRuleMinScore
            // 
            this.lblRuleMinScore.AutoSize = true;
            this.lblRuleMinScore.Location = new System.Drawing.Point(186, 47);
            this.lblRuleMinScore.Name = "lblRuleMinScore";
            this.lblRuleMinScore.Size = new System.Drawing.Size(58, 13);
            this.lblRuleMinScore.TabIndex = 7;
            this.lblRuleMinScore.Text = "Min Score:";
            // 
            // lblRuleName
            // 
            this.lblRuleName.AutoSize = true;
            this.lblRuleName.Location = new System.Drawing.Point(186, 22);
            this.lblRuleName.Name = "lblRuleName";
            this.lblRuleName.Size = new System.Drawing.Size(38, 13);
            this.lblRuleName.TabIndex = 5;
            this.lblRuleName.Text = "Name:";
            // 
            // lblRuleMaxScore
            // 
            this.lblRuleMaxScore.AutoSize = true;
            this.lblRuleMaxScore.Location = new System.Drawing.Point(186, 73);
            this.lblRuleMaxScore.Name = "lblRuleMaxScore";
            this.lblRuleMaxScore.Size = new System.Drawing.Size(61, 13);
            this.lblRuleMaxScore.TabIndex = 9;
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
            this.lstRules.Size = new System.Drawing.Size(174, 269);
            this.lstRules.TabIndex = 0;
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
            // lblRuleMinReviews
            // 
            this.lblRuleMinReviews.AutoSize = true;
            this.lblRuleMinReviews.Location = new System.Drawing.Point(186, 99);
            this.lblRuleMinReviews.Name = "lblRuleMinReviews";
            this.lblRuleMinReviews.Size = new System.Drawing.Size(71, 13);
            this.lblRuleMinReviews.TabIndex = 11;
            this.lblRuleMinReviews.Text = "Min Reviews:";
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
            this.numRuleMinReviews.TabIndex = 12;
            this.numRuleMinReviews.ThousandsSeparator = true;
            // 
            // lblRuleMaxReviews
            // 
            this.lblRuleMaxReviews.AutoSize = true;
            this.lblRuleMaxReviews.Location = new System.Drawing.Point(186, 125);
            this.lblRuleMaxReviews.Name = "lblRuleMaxReviews";
            this.lblRuleMaxReviews.Size = new System.Drawing.Size(78, 26);
            this.lblRuleMaxReviews.TabIndex = 13;
            this.lblRuleMaxReviews.Text = "Max Reviews:\r\n(0 for unlimited)";
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
            this.numRuleMaxReviews.TabIndex = 14;
            this.numRuleMaxReviews.ThousandsSeparator = true;
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
            this.grpRules.ResumeLayout(false);
            this.grpRules.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMinReviews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuleMaxReviews)).EndInit();
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
    }
}
