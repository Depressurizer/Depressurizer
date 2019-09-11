namespace Depressurizer.AutoCats {
    partial class AutoCatConfigPanel_Tags {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Tags));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.splitTags = new System.Windows.Forms.SplitContainer();
            this.clbTags = new System.Windows.Forms.CheckedListBox();
            this.lstIncluded = new System.Windows.Forms.ListView();
            this.columnTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextTags = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nameascendingTags = new System.Windows.Forms.ToolStripMenuItem();
            this.namedescendingTags = new System.Windows.Forms.ToolStripMenuItem();
            this.countascendingTags = new System.Windows.Forms.ToolStripMenuItem();
            this.countdescendingTags = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTagSelected = new System.Windows.Forms.Button();
            this.lblIncluded = new System.Windows.Forms.Label();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.grpListOpts = new System.Windows.Forms.GroupBox();
            this.helpExcludeGenres = new System.Windows.Forms.Label();
            this.list_chkExcludeGenres = new System.Windows.Forms.CheckBox();
            this.lblExplain = new System.Windows.Forms.Label();
            this.list_lblWeightFactor = new System.Windows.Forms.Label();
            this.list_numWeightFactor = new System.Windows.Forms.NumericUpDown();
            this.list_helpOwnedOnly = new System.Windows.Forms.Label();
            this.helpWeightFactor = new System.Windows.Forms.Label();
            this.helpTagsPerGame = new System.Windows.Forms.Label();
            this.list_helpMinScore = new System.Windows.Forms.Label();
            this.list_lblTagsPerGame = new System.Windows.Forms.Label();
            this.list_numTagsPerGame = new System.Windows.Forms.NumericUpDown();
            this.list_chkOwnedOnly = new System.Windows.Forms.CheckBox();
            this.list_numMinScore = new System.Windows.Forms.NumericUpDown();
            this.lblMinScore = new System.Windows.Forms.Label();
            this.cmdListRebuild = new System.Windows.Forms.Button();
            this.cmdCheckAll = new System.Windows.Forms.Button();
            this.cmdUncheckAll = new System.Windows.Forms.Button();
            this.numMaxTags = new System.Windows.Forms.NumericUpDown();
            this.lblMaxTags = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitTags)).BeginInit();
            this.splitTags.Panel1.SuspendLayout();
            this.splitTags.Panel2.SuspendLayout();
            this.splitTags.SuspendLayout();
            this.contextTags.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpListOpts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_numWeightFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.list_numTagsPerGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.list_numMinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTags)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.splitTags);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.grpListOpts);
            this.grpMain.Controls.Add(this.cmdListRebuild);
            this.grpMain.Controls.Add(this.cmdCheckAll);
            this.grpMain.Controls.Add(this.cmdUncheckAll);
            this.grpMain.Controls.Add(this.numMaxTags);
            this.grpMain.Controls.Add(this.lblMaxTags);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.txtPrefix);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // splitTags
            // 
            resources.ApplyResources(this.splitTags, "splitTags");
            this.splitTags.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitTags.Name = "splitTags";
            // 
            // splitTags.Panel1
            // 
            this.splitTags.Panel1.Controls.Add(this.clbTags);
            this.splitTags.Panel1Collapsed = true;
            // 
            // splitTags.Panel2
            // 
            this.splitTags.Panel2.Controls.Add(this.lstIncluded);
            this.splitTags.Panel2.Controls.Add(this.tableLayoutPanel1);
            // 
            // clbTags
            // 
            resources.ApplyResources(this.clbTags, "clbTags");
            this.clbTags.FormattingEnabled = true;
            this.clbTags.MultiColumn = true;
            this.clbTags.Name = "clbTags";
            this.clbTags.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbTags_ItemCheck);
            // 
            // lstIncluded
            // 
            this.lstIncluded.CheckBoxes = true;
            this.lstIncluded.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnTag,
            this.columnCount});
            this.lstIncluded.ContextMenuStrip = this.contextTags;
            resources.ApplyResources(this.lstIncluded, "lstIncluded");
            this.lstIncluded.HideSelection = false;
            this.lstIncluded.Name = "lstIncluded";
            this.lstIncluded.UseCompatibleStateImageBehavior = false;
            this.lstIncluded.View = System.Windows.Forms.View.List;
            this.lstIncluded.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstIncluded_ItemChecked);
            // 
            // columnTag
            // 
            resources.ApplyResources(this.columnTag, "columnTag");
            // 
            // contextTags
            // 
            this.contextTags.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextTags.Name = "contextCat";
            this.contextTags.ShowImageMargin = false;
            resources.ApplyResources(this.contextTags, "contextTags");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascendingTags,
            this.namedescendingTags,
            this.countascendingTags,
            this.countdescendingTags});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // nameascendingTags
            // 
            this.nameascendingTags.Name = "nameascendingTags";
            resources.ApplyResources(this.nameascendingTags, "nameascendingTags");
            this.nameascendingTags.Click += new System.EventHandler(this.nameascendingTags_Click);
            // 
            // namedescendingTags
            // 
            this.namedescendingTags.Name = "namedescendingTags";
            resources.ApplyResources(this.namedescendingTags, "namedescendingTags");
            this.namedescendingTags.Click += new System.EventHandler(this.namedescendingTags_Click);
            // 
            // countascendingTags
            // 
            this.countascendingTags.Name = "countascendingTags";
            resources.ApplyResources(this.countascendingTags, "countascendingTags");
            this.countascendingTags.Click += new System.EventHandler(this.countascendingTags_Click);
            // 
            // countdescendingTags
            // 
            this.countdescendingTags.Name = "countdescendingTags";
            resources.ApplyResources(this.countdescendingTags, "countdescendingTags");
            this.countdescendingTags.Click += new System.EventHandler(this.countdescendingTags_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTagSelected, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblIncluded, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnTagSelected
            // 
            resources.ApplyResources(this.btnTagSelected, "btnTagSelected");
            this.btnTagSelected.Name = "btnTagSelected";
            this.btnTagSelected.UseVisualStyleBackColor = true;
            this.btnTagSelected.Click += new System.EventHandler(this.btnTagSelected_Click);
            // 
            // lblIncluded
            // 
            resources.ApplyResources(this.lblIncluded, "lblIncluded");
            this.lblIncluded.Name = "lblIncluded";
            // 
            // helpPrefix
            // 
            resources.ApplyResources(this.helpPrefix, "helpPrefix");
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Name = "helpPrefix";
            // 
            // grpListOpts
            // 
            resources.ApplyResources(this.grpListOpts, "grpListOpts");
            this.grpListOpts.Controls.Add(this.helpExcludeGenres);
            this.grpListOpts.Controls.Add(this.list_chkExcludeGenres);
            this.grpListOpts.Controls.Add(this.lblExplain);
            this.grpListOpts.Controls.Add(this.list_lblWeightFactor);
            this.grpListOpts.Controls.Add(this.list_numWeightFactor);
            this.grpListOpts.Controls.Add(this.list_helpOwnedOnly);
            this.grpListOpts.Controls.Add(this.helpWeightFactor);
            this.grpListOpts.Controls.Add(this.helpTagsPerGame);
            this.grpListOpts.Controls.Add(this.list_helpMinScore);
            this.grpListOpts.Controls.Add(this.list_lblTagsPerGame);
            this.grpListOpts.Controls.Add(this.list_numTagsPerGame);
            this.grpListOpts.Controls.Add(this.list_chkOwnedOnly);
            this.grpListOpts.Controls.Add(this.list_numMinScore);
            this.grpListOpts.Controls.Add(this.lblMinScore);
            this.grpListOpts.Name = "grpListOpts";
            this.grpListOpts.TabStop = false;
            // 
            // helpExcludeGenres
            // 
            resources.ApplyResources(this.helpExcludeGenres, "helpExcludeGenres");
            this.helpExcludeGenres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpExcludeGenres.Name = "helpExcludeGenres";
            // 
            // list_chkExcludeGenres
            // 
            resources.ApplyResources(this.list_chkExcludeGenres, "list_chkExcludeGenres");
            this.list_chkExcludeGenres.Name = "list_chkExcludeGenres";
            this.list_chkExcludeGenres.UseVisualStyleBackColor = true;
            // 
            // lblExplain
            // 
            resources.ApplyResources(this.lblExplain, "lblExplain");
            this.lblExplain.Name = "lblExplain";
            // 
            // list_lblWeightFactor
            // 
            resources.ApplyResources(this.list_lblWeightFactor, "list_lblWeightFactor");
            this.list_lblWeightFactor.Name = "list_lblWeightFactor";
            // 
            // list_numWeightFactor
            // 
            this.list_numWeightFactor.DecimalPlaces = 1;
            this.list_numWeightFactor.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            resources.ApplyResources(this.list_numWeightFactor, "list_numWeightFactor");
            this.list_numWeightFactor.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.list_numWeightFactor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.list_numWeightFactor.Name = "list_numWeightFactor";
            this.list_numWeightFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // list_helpOwnedOnly
            // 
            resources.ApplyResources(this.list_helpOwnedOnly, "list_helpOwnedOnly");
            this.list_helpOwnedOnly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_helpOwnedOnly.Name = "list_helpOwnedOnly";
            // 
            // helpWeightFactor
            // 
            resources.ApplyResources(this.helpWeightFactor, "helpWeightFactor");
            this.helpWeightFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpWeightFactor.Name = "helpWeightFactor";
            // 
            // helpTagsPerGame
            // 
            resources.ApplyResources(this.helpTagsPerGame, "helpTagsPerGame");
            this.helpTagsPerGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpTagsPerGame.Name = "helpTagsPerGame";
            // 
            // list_helpMinScore
            // 
            resources.ApplyResources(this.list_helpMinScore, "list_helpMinScore");
            this.list_helpMinScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_helpMinScore.Name = "list_helpMinScore";
            // 
            // list_lblTagsPerGame
            // 
            resources.ApplyResources(this.list_lblTagsPerGame, "list_lblTagsPerGame");
            this.list_lblTagsPerGame.Name = "list_lblTagsPerGame";
            // 
            // list_numTagsPerGame
            // 
            resources.ApplyResources(this.list_numTagsPerGame, "list_numTagsPerGame");
            this.list_numTagsPerGame.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.list_numTagsPerGame.Name = "list_numTagsPerGame";
            // 
            // list_chkOwnedOnly
            // 
            resources.ApplyResources(this.list_chkOwnedOnly, "list_chkOwnedOnly");
            this.list_chkOwnedOnly.Name = "list_chkOwnedOnly";
            this.list_chkOwnedOnly.UseVisualStyleBackColor = true;
            // 
            // list_numMinScore
            // 
            resources.ApplyResources(this.list_numMinScore, "list_numMinScore");
            this.list_numMinScore.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.list_numMinScore.Name = "list_numMinScore";
            // 
            // lblMinScore
            // 
            resources.ApplyResources(this.lblMinScore, "lblMinScore");
            this.lblMinScore.Name = "lblMinScore";
            // 
            // cmdListRebuild
            // 
            resources.ApplyResources(this.cmdListRebuild, "cmdListRebuild");
            this.cmdListRebuild.Name = "cmdListRebuild";
            this.cmdListRebuild.UseVisualStyleBackColor = true;
            this.cmdListRebuild.Click += new System.EventHandler(this.cmdListRebuild_Click);
            // 
            // cmdCheckAll
            // 
            resources.ApplyResources(this.cmdCheckAll, "cmdCheckAll");
            this.cmdCheckAll.Name = "cmdCheckAll";
            this.cmdCheckAll.UseVisualStyleBackColor = true;
            this.cmdCheckAll.Click += new System.EventHandler(this.cmdCheckAll_Click);
            // 
            // cmdUncheckAll
            // 
            resources.ApplyResources(this.cmdUncheckAll, "cmdUncheckAll");
            this.cmdUncheckAll.Name = "cmdUncheckAll";
            this.cmdUncheckAll.UseVisualStyleBackColor = true;
            this.cmdUncheckAll.Click += new System.EventHandler(this.cmdUncheckAll_Click);
            // 
            // numMaxTags
            // 
            resources.ApplyResources(this.numMaxTags, "numMaxTags");
            this.numMaxTags.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numMaxTags.Name = "numMaxTags";
            // 
            // lblMaxTags
            // 
            resources.ApplyResources(this.lblMaxTags, "lblMaxTags");
            this.lblMaxTags.Name = "lblMaxTags";
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
            // AutoCatConfigPanel_Tags
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Tags";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.splitTags.Panel1.ResumeLayout(false);
            this.splitTags.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitTags)).EndInit();
            this.splitTags.ResumeLayout(false);
            this.contextTags.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpListOpts.ResumeLayout(false);
            this.grpListOpts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_numWeightFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.list_numTagsPerGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.list_numMinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.GroupBox grpListOpts;
        private System.Windows.Forms.Label helpExcludeGenres;
        private System.Windows.Forms.CheckBox list_chkExcludeGenres;
        private System.Windows.Forms.Label lblExplain;
        private System.Windows.Forms.Label list_lblWeightFactor;
        private System.Windows.Forms.NumericUpDown list_numWeightFactor;
        private System.Windows.Forms.Label list_helpOwnedOnly;
        private System.Windows.Forms.Label helpWeightFactor;
        private System.Windows.Forms.Label helpTagsPerGame;
        private System.Windows.Forms.Label list_helpMinScore;
        private System.Windows.Forms.Label list_lblTagsPerGame;
        private System.Windows.Forms.NumericUpDown list_numTagsPerGame;
        private System.Windows.Forms.CheckBox list_chkOwnedOnly;
        private System.Windows.Forms.NumericUpDown list_numMinScore;
        private System.Windows.Forms.Label lblMinScore;
        private System.Windows.Forms.Label lblIncluded;
        private System.Windows.Forms.Button cmdListRebuild;
        private System.Windows.Forms.Button cmdCheckAll;
        private System.Windows.Forms.Button cmdUncheckAll;
        private System.Windows.Forms.NumericUpDown numMaxTags;
        private System.Windows.Forms.Label lblMaxTags;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.ListView lstIncluded;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.SplitContainer splitTags;
        private System.Windows.Forms.CheckedListBox clbTags;
        private System.Windows.Forms.Button btnTagSelected;
        private System.Windows.Forms.ColumnHeader columnTag;
        private System.Windows.Forms.ColumnHeader columnCount;
        private System.Windows.Forms.ContextMenuStrip contextTags;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nameascendingTags;
        private System.Windows.Forms.ToolStripMenuItem namedescendingTags;
        private System.Windows.Forms.ToolStripMenuItem countascendingTags;
        private System.Windows.Forms.ToolStripMenuItem countdescendingTags;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
