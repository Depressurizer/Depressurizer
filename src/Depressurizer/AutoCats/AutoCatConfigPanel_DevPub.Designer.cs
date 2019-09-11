namespace Depressurizer.AutoCats {
    partial class AutoCatConfigPanel_DevPub
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_DevPub));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.splitMainBottom = new System.Windows.Forms.SplitContainer();
            this.groupDevelopers = new System.Windows.Forms.GroupBox();
            this.splitDevTop = new System.Windows.Forms.SplitContainer();
            this.clbDevelopersSelected = new System.Windows.Forms.CheckedListBox();
            this.lstDevelopers = new System.Windows.Forms.ListView();
            this.columnDeveloper = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDevCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextDev = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameascendingDev = new System.Windows.Forms.ToolStripMenuItem();
            this.namedescendingDev = new System.Windows.Forms.ToolStripMenuItem();
            this.countascendingDev = new System.Windows.Forms.ToolStripMenuItem();
            this.countdescendingDev = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelDevTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnDevSelected = new System.Windows.Forms.Button();
            this.chkAllDevelopers = new System.Windows.Forms.CheckBox();
            this.tblIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.btnDevUncheckAll = new System.Windows.Forms.Button();
            this.btnDevCheckAll = new System.Windows.Forms.Button();
            this.groupPublishers = new System.Windows.Forms.GroupBox();
            this.splitPubTop = new System.Windows.Forms.SplitContainer();
            this.clbPublishersSelected = new System.Windows.Forms.CheckedListBox();
            this.lstPublishers = new System.Windows.Forms.ListView();
            this.columnPublisher = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPubCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextPub = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nameascendingPub = new System.Windows.Forms.ToolStripMenuItem();
            this.namedescendingPub = new System.Windows.Forms.ToolStripMenuItem();
            this.countascendingPub = new System.Windows.Forms.ToolStripMenuItem();
            this.countdescendingPub = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelPublisherTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnPubSelected = new System.Windows.Forms.Button();
            this.chkAllPublishers = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPubCheckAll = new System.Windows.Forms.Button();
            this.btnPubUncheckAll = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.cmdListRebuild = new System.Windows.Forms.Button();
            this.list_helpOwnedOnly = new System.Windows.Forms.Label();
            this.chkOwnedOnly = new System.Windows.Forms.CheckBox();
            this.list_helpScore = new System.Windows.Forms.Label();
            this.list_numScore = new System.Windows.Forms.NumericUpDown();
            this.lblMinScore = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMainBottom)).BeginInit();
            this.splitMainBottom.Panel1.SuspendLayout();
            this.splitMainBottom.Panel2.SuspendLayout();
            this.splitMainBottom.SuspendLayout();
            this.groupDevelopers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDevTop)).BeginInit();
            this.splitDevTop.Panel1.SuspendLayout();
            this.splitDevTop.Panel2.SuspendLayout();
            this.splitDevTop.SuspendLayout();
            this.contextDev.SuspendLayout();
            this.tableLayoutPanelDevTop.SuspendLayout();
            this.tblIgnore.SuspendLayout();
            this.groupPublishers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPubTop)).BeginInit();
            this.splitPubTop.Panel1.SuspendLayout();
            this.splitPubTop.Panel2.SuspendLayout();
            this.splitPubTop.SuspendLayout();
            this.contextPub.SuspendLayout();
            this.tableLayoutPanelPublisherTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_numScore)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.splitMainBottom);
            this.grpMain.Controls.Add(this.panelTop);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // splitMainBottom
            // 
            resources.ApplyResources(this.splitMainBottom, "splitMainBottom");
            this.splitMainBottom.Name = "splitMainBottom";
            // 
            // splitMainBottom.Panel1
            // 
            this.splitMainBottom.Panel1.Controls.Add(this.groupDevelopers);
            // 
            // splitMainBottom.Panel2
            // 
            this.splitMainBottom.Panel2.Controls.Add(this.groupPublishers);
            // 
            // groupDevelopers
            // 
            this.groupDevelopers.Controls.Add(this.splitDevTop);
            this.groupDevelopers.Controls.Add(this.tblIgnore);
            resources.ApplyResources(this.groupDevelopers, "groupDevelopers");
            this.groupDevelopers.Name = "groupDevelopers";
            this.groupDevelopers.TabStop = false;
            // 
            // splitDevTop
            // 
            resources.ApplyResources(this.splitDevTop, "splitDevTop");
            this.splitDevTop.Name = "splitDevTop";
            // 
            // splitDevTop.Panel1
            // 
            this.splitDevTop.Panel1.Controls.Add(this.clbDevelopersSelected);
            this.splitDevTop.Panel1Collapsed = true;
            // 
            // splitDevTop.Panel2
            // 
            this.splitDevTop.Panel2.Controls.Add(this.lstDevelopers);
            this.splitDevTop.Panel2.Controls.Add(this.tableLayoutPanelDevTop);
            // 
            // clbDevelopersSelected
            // 
            resources.ApplyResources(this.clbDevelopersSelected, "clbDevelopersSelected");
            this.clbDevelopersSelected.FormattingEnabled = true;
            this.clbDevelopersSelected.MultiColumn = true;
            this.clbDevelopersSelected.Name = "clbDevelopersSelected";
            this.clbDevelopersSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDevelopersSelected_ItemCheck);
            // 
            // lstDevelopers
            // 
            this.lstDevelopers.CheckBoxes = true;
            this.lstDevelopers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDeveloper,
            this.columnDevCount});
            this.lstDevelopers.ContextMenuStrip = this.contextDev;
            resources.ApplyResources(this.lstDevelopers, "lstDevelopers");
            this.lstDevelopers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstDevelopers.HideSelection = false;
            this.lstDevelopers.Name = "lstDevelopers";
            this.lstDevelopers.UseCompatibleStateImageBehavior = false;
            this.lstDevelopers.View = System.Windows.Forms.View.List;
            this.lstDevelopers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstDevelopers_ItemChecked);
            // 
            // columnDeveloper
            // 
            resources.ApplyResources(this.columnDeveloper, "columnDeveloper");
            // 
            // contextDev
            // 
            this.contextDev.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortToolStripMenuItem});
            this.contextDev.Name = "contextCat";
            this.contextDev.ShowImageMargin = false;
            resources.ApplyResources(this.contextDev, "contextDev");
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascendingDev,
            this.namedescendingDev,
            this.countascendingDev,
            this.countdescendingDev});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            resources.ApplyResources(this.sortToolStripMenuItem, "sortToolStripMenuItem");
            // 
            // nameascendingDev
            // 
            this.nameascendingDev.Name = "nameascendingDev";
            resources.ApplyResources(this.nameascendingDev, "nameascendingDev");
            this.nameascendingDev.Click += new System.EventHandler(this.nameascendingDev_Click);
            // 
            // namedescendingDev
            // 
            this.namedescendingDev.Name = "namedescendingDev";
            resources.ApplyResources(this.namedescendingDev, "namedescendingDev");
            this.namedescendingDev.Click += new System.EventHandler(this.namedescendingDev_Click);
            // 
            // countascendingDev
            // 
            this.countascendingDev.Name = "countascendingDev";
            resources.ApplyResources(this.countascendingDev, "countascendingDev");
            this.countascendingDev.Click += new System.EventHandler(this.countascendingDev_Click);
            // 
            // countdescendingDev
            // 
            this.countdescendingDev.Name = "countdescendingDev";
            resources.ApplyResources(this.countdescendingDev, "countdescendingDev");
            this.countdescendingDev.Click += new System.EventHandler(this.countdescendingDev_Click);
            // 
            // tableLayoutPanelDevTop
            // 
            resources.ApplyResources(this.tableLayoutPanelDevTop, "tableLayoutPanelDevTop");
            this.tableLayoutPanelDevTop.Controls.Add(this.btnDevSelected, 0, 0);
            this.tableLayoutPanelDevTop.Controls.Add(this.chkAllDevelopers, 1, 0);
            this.tableLayoutPanelDevTop.Name = "tableLayoutPanelDevTop";
            // 
            // btnDevSelected
            // 
            resources.ApplyResources(this.btnDevSelected, "btnDevSelected");
            this.btnDevSelected.Name = "btnDevSelected";
            this.btnDevSelected.UseVisualStyleBackColor = true;
            this.btnDevSelected.Click += new System.EventHandler(this.btnDevSelected_Click);
            // 
            // chkAllDevelopers
            // 
            resources.ApplyResources(this.chkAllDevelopers, "chkAllDevelopers");
            this.chkAllDevelopers.Name = "chkAllDevelopers";
            this.chkAllDevelopers.UseVisualStyleBackColor = true;
            this.chkAllDevelopers.CheckedChanged += new System.EventHandler(this.chkAllDevelopers_CheckedChanged);
            // 
            // tblIgnore
            // 
            resources.ApplyResources(this.tblIgnore, "tblIgnore");
            this.tblIgnore.Controls.Add(this.btnDevUncheckAll, 1, 0);
            this.tblIgnore.Controls.Add(this.btnDevCheckAll, 0, 0);
            this.tblIgnore.Name = "tblIgnore";
            // 
            // btnDevUncheckAll
            // 
            resources.ApplyResources(this.btnDevUncheckAll, "btnDevUncheckAll");
            this.btnDevUncheckAll.Name = "btnDevUncheckAll";
            this.btnDevUncheckAll.UseVisualStyleBackColor = true;
            this.btnDevUncheckAll.Click += new System.EventHandler(this.btnDevUncheckAll_Click);
            // 
            // btnDevCheckAll
            // 
            resources.ApplyResources(this.btnDevCheckAll, "btnDevCheckAll");
            this.btnDevCheckAll.Name = "btnDevCheckAll";
            this.btnDevCheckAll.UseVisualStyleBackColor = true;
            this.btnDevCheckAll.Click += new System.EventHandler(this.btnDevCheckAll_Click);
            // 
            // groupPublishers
            // 
            this.groupPublishers.Controls.Add(this.splitPubTop);
            this.groupPublishers.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.groupPublishers, "groupPublishers");
            this.groupPublishers.Name = "groupPublishers";
            this.groupPublishers.TabStop = false;
            // 
            // splitPubTop
            // 
            resources.ApplyResources(this.splitPubTop, "splitPubTop");
            this.splitPubTop.Name = "splitPubTop";
            // 
            // splitPubTop.Panel1
            // 
            this.splitPubTop.Panel1.Controls.Add(this.clbPublishersSelected);
            this.splitPubTop.Panel1Collapsed = true;
            // 
            // splitPubTop.Panel2
            // 
            this.splitPubTop.Panel2.Controls.Add(this.lstPublishers);
            this.splitPubTop.Panel2.Controls.Add(this.tableLayoutPanelPublisherTop);
            // 
            // clbPublishersSelected
            // 
            resources.ApplyResources(this.clbPublishersSelected, "clbPublishersSelected");
            this.clbPublishersSelected.FormattingEnabled = true;
            this.clbPublishersSelected.MultiColumn = true;
            this.clbPublishersSelected.Name = "clbPublishersSelected";
            this.clbPublishersSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPublishersSelected_ItemCheck);
            // 
            // lstPublishers
            // 
            this.lstPublishers.CheckBoxes = true;
            this.lstPublishers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPublisher,
            this.columnPubCount});
            this.lstPublishers.ContextMenuStrip = this.contextPub;
            resources.ApplyResources(this.lstPublishers, "lstPublishers");
            this.lstPublishers.HideSelection = false;
            this.lstPublishers.Name = "lstPublishers";
            this.lstPublishers.UseCompatibleStateImageBehavior = false;
            this.lstPublishers.View = System.Windows.Forms.View.List;
            this.lstPublishers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstPublishers_ItemChecked);
            // 
            // columnPublisher
            // 
            resources.ApplyResources(this.columnPublisher, "columnPublisher");
            // 
            // contextPub
            // 
            this.contextPub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextPub.Name = "contextCat";
            this.contextPub.ShowImageMargin = false;
            resources.ApplyResources(this.contextPub, "contextPub");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascendingPub,
            this.namedescendingPub,
            this.countascendingPub,
            this.countdescendingPub});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // nameascendingPub
            // 
            this.nameascendingPub.Name = "nameascendingPub";
            resources.ApplyResources(this.nameascendingPub, "nameascendingPub");
            this.nameascendingPub.Click += new System.EventHandler(this.nameascendingPub_Click);
            // 
            // namedescendingPub
            // 
            this.namedescendingPub.Name = "namedescendingPub";
            resources.ApplyResources(this.namedescendingPub, "namedescendingPub");
            this.namedescendingPub.Click += new System.EventHandler(this.namedescendingPub_Click);
            // 
            // countascendingPub
            // 
            this.countascendingPub.Name = "countascendingPub";
            resources.ApplyResources(this.countascendingPub, "countascendingPub");
            this.countascendingPub.Click += new System.EventHandler(this.countascendingPub_Click);
            // 
            // countdescendingPub
            // 
            this.countdescendingPub.Name = "countdescendingPub";
            resources.ApplyResources(this.countdescendingPub, "countdescendingPub");
            this.countdescendingPub.Click += new System.EventHandler(this.countdescendingPub_Click);
            // 
            // tableLayoutPanelPublisherTop
            // 
            resources.ApplyResources(this.tableLayoutPanelPublisherTop, "tableLayoutPanelPublisherTop");
            this.tableLayoutPanelPublisherTop.Controls.Add(this.btnPubSelected, 0, 0);
            this.tableLayoutPanelPublisherTop.Controls.Add(this.chkAllPublishers, 1, 0);
            this.tableLayoutPanelPublisherTop.Name = "tableLayoutPanelPublisherTop";
            // 
            // btnPubSelected
            // 
            resources.ApplyResources(this.btnPubSelected, "btnPubSelected");
            this.btnPubSelected.Name = "btnPubSelected";
            this.btnPubSelected.UseVisualStyleBackColor = true;
            this.btnPubSelected.Click += new System.EventHandler(this.btnPubSelected_Click);
            // 
            // chkAllPublishers
            // 
            resources.ApplyResources(this.chkAllPublishers, "chkAllPublishers");
            this.chkAllPublishers.Name = "chkAllPublishers";
            this.chkAllPublishers.UseVisualStyleBackColor = true;
            this.chkAllPublishers.CheckedChanged += new System.EventHandler(this.chkAllPublishers_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnPubCheckAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPubUncheckAll, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // btnPubCheckAll
            // 
            resources.ApplyResources(this.btnPubCheckAll, "btnPubCheckAll");
            this.btnPubCheckAll.Name = "btnPubCheckAll";
            this.btnPubCheckAll.UseVisualStyleBackColor = true;
            this.btnPubCheckAll.Click += new System.EventHandler(this.btnPubCheckAll_Click);
            // 
            // btnPubUncheckAll
            // 
            resources.ApplyResources(this.btnPubUncheckAll, "btnPubUncheckAll");
            this.btnPubUncheckAll.Name = "btnPubUncheckAll";
            this.btnPubUncheckAll.UseVisualStyleBackColor = true;
            this.btnPubUncheckAll.Click += new System.EventHandler(this.btnPubUncheckAll_Click);
            // 
            // panelTop
            // 
            resources.ApplyResources(this.panelTop, "panelTop");
            this.panelTop.Controls.Add(this.cmdListRebuild);
            this.panelTop.Controls.Add(this.list_helpOwnedOnly);
            this.panelTop.Controls.Add(this.chkOwnedOnly);
            this.panelTop.Controls.Add(this.list_helpScore);
            this.panelTop.Controls.Add(this.list_numScore);
            this.panelTop.Controls.Add(this.lblMinScore);
            this.panelTop.Controls.Add(this.lblPrefix);
            this.panelTop.Controls.Add(this.txtPrefix);
            this.panelTop.Controls.Add(this.helpPrefix);
            this.panelTop.Name = "panelTop";
            // 
            // cmdListRebuild
            // 
            resources.ApplyResources(this.cmdListRebuild, "cmdListRebuild");
            this.cmdListRebuild.Name = "cmdListRebuild";
            this.cmdListRebuild.UseVisualStyleBackColor = true;
            this.cmdListRebuild.Click += new System.EventHandler(this.cmdListRebuild_Click);
            // 
            // list_helpOwnedOnly
            // 
            resources.ApplyResources(this.list_helpOwnedOnly, "list_helpOwnedOnly");
            this.list_helpOwnedOnly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_helpOwnedOnly.Name = "list_helpOwnedOnly";
            // 
            // chkOwnedOnly
            // 
            resources.ApplyResources(this.chkOwnedOnly, "chkOwnedOnly");
            this.chkOwnedOnly.Name = "chkOwnedOnly";
            this.chkOwnedOnly.UseVisualStyleBackColor = true;
            // 
            // list_helpScore
            // 
            resources.ApplyResources(this.list_helpScore, "list_helpScore");
            this.list_helpScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_helpScore.Name = "list_helpScore";
            // 
            // list_numScore
            // 
            resources.ApplyResources(this.list_numScore, "list_numScore");
            this.list_numScore.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.list_numScore.Name = "list_numScore";
            // 
            // lblMinScore
            // 
            resources.ApplyResources(this.lblMinScore, "lblMinScore");
            this.lblMinScore.Name = "lblMinScore";
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
            // helpPrefix
            // 
            resources.ApplyResources(this.helpPrefix, "helpPrefix");
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Name = "helpPrefix";
            // 
            // AutoCatConfigPanel_DevPub
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_DevPub";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.splitMainBottom.Panel1.ResumeLayout(false);
            this.splitMainBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMainBottom)).EndInit();
            this.splitMainBottom.ResumeLayout(false);
            this.groupDevelopers.ResumeLayout(false);
            this.splitDevTop.Panel1.ResumeLayout(false);
            this.splitDevTop.Panel2.ResumeLayout(false);
            this.splitDevTop.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDevTop)).EndInit();
            this.splitDevTop.ResumeLayout(false);
            this.contextDev.ResumeLayout(false);
            this.tableLayoutPanelDevTop.ResumeLayout(false);
            this.tableLayoutPanelDevTop.PerformLayout();
            this.tblIgnore.ResumeLayout(false);
            this.groupPublishers.ResumeLayout(false);
            this.splitPubTop.Panel1.ResumeLayout(false);
            this.splitPubTop.Panel2.ResumeLayout(false);
            this.splitPubTop.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPubTop)).EndInit();
            this.splitPubTop.ResumeLayout(false);
            this.contextPub.ResumeLayout(false);
            this.tableLayoutPanelPublisherTop.ResumeLayout(false);
            this.tableLayoutPanelPublisherTop.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.list_numScore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TableLayoutPanel tblIgnore;
        private System.Windows.Forms.Button btnDevUncheckAll;
        private System.Windows.Forms.Button btnDevCheckAll;
        private System.Windows.Forms.ListView lstDevelopers;
        private System.Windows.Forms.CheckBox chkAllDevelopers;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.SplitContainer splitMainBottom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnPubUncheckAll;
        private System.Windows.Forms.Button btnPubCheckAll;
        private System.Windows.Forms.CheckedListBox clbDevelopersSelected;
        private System.Windows.Forms.CheckedListBox clbPublishersSelected;
        private System.Windows.Forms.GroupBox groupDevelopers;
        private System.Windows.Forms.SplitContainer splitDevTop;
        private System.Windows.Forms.GroupBox groupPublishers;
        private System.Windows.Forms.SplitContainer splitPubTop;
        private System.Windows.Forms.CheckBox chkAllPublishers;
        private System.Windows.Forms.ListView lstPublishers;
        private System.Windows.Forms.Label list_helpScore;
        private System.Windows.Forms.NumericUpDown list_numScore;
        private System.Windows.Forms.Label lblMinScore;
        private System.Windows.Forms.Button btnDevSelected;
        private System.Windows.Forms.Button btnPubSelected;
        private System.Windows.Forms.Label list_helpOwnedOnly;
        private System.Windows.Forms.CheckBox chkOwnedOnly;
        private System.Windows.Forms.Button cmdListRebuild;
        private System.Windows.Forms.ColumnHeader columnDeveloper;
        private System.Windows.Forms.ColumnHeader columnDevCount;
        private System.Windows.Forms.ColumnHeader columnPublisher;
        private System.Windows.Forms.ColumnHeader columnPubCount;
        private System.Windows.Forms.ContextMenuStrip contextDev;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameascendingDev;
        private System.Windows.Forms.ToolStripMenuItem namedescendingDev;
        private System.Windows.Forms.ToolStripMenuItem countascendingDev;
        private System.Windows.Forms.ToolStripMenuItem countdescendingDev;
        private System.Windows.Forms.ContextMenuStrip contextPub;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nameascendingPub;
        private System.Windows.Forms.ToolStripMenuItem namedescendingPub;
        private System.Windows.Forms.ToolStripMenuItem countascendingPub;
        private System.Windows.Forms.ToolStripMenuItem countdescendingPub;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDevTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPublisherTop;
    }
}
