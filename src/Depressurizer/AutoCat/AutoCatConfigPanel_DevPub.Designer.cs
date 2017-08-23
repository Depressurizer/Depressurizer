namespace Depressurizer {
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.splitMainBottom = new System.Windows.Forms.SplitContainer();
            this.groupDevelopers = new System.Windows.Forms.GroupBox();
            this.splitDevTop = new System.Windows.Forms.SplitContainer();
            this.clbDevelopersSelected = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanelDevTop = new System.Windows.Forms.TableLayoutPanel();
            this.btnDevSelected = new System.Windows.Forms.Button();
            this.chkAllDevelopers = new System.Windows.Forms.CheckBox();
            this.lstDevelopers = new System.Windows.Forms.ListView();
            this.columnDeveloper = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDevCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextDev = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameascendingDev = new System.Windows.Forms.ToolStripMenuItem();
            this.namedescendingDev = new System.Windows.Forms.ToolStripMenuItem();
            this.countascendingDev = new System.Windows.Forms.ToolStripMenuItem();
            this.countdescendingDev = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tableLayoutPanelDevTop.SuspendLayout();
            this.contextDev.SuspendLayout();
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
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(610, 406);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit DevPub AutoCat";
            // 
            // splitMainBottom
            // 
            this.splitMainBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMainBottom.Location = new System.Drawing.Point(3, 72);
            this.splitMainBottom.Name = "splitMainBottom";
            // 
            // splitMainBottom.Panel1
            // 
            this.splitMainBottom.Panel1.Controls.Add(this.groupDevelopers);
            // 
            // splitMainBottom.Panel2
            // 
            this.splitMainBottom.Panel2.Controls.Add(this.groupPublishers);
            this.splitMainBottom.Size = new System.Drawing.Size(604, 331);
            this.splitMainBottom.SplitterDistance = 297;
            this.splitMainBottom.TabIndex = 0;
            // 
            // groupDevelopers
            // 
            this.groupDevelopers.Controls.Add(this.splitDevTop);
            this.groupDevelopers.Controls.Add(this.tblIgnore);
            this.groupDevelopers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDevelopers.Location = new System.Drawing.Point(0, 0);
            this.groupDevelopers.Name = "groupDevelopers";
            this.groupDevelopers.Size = new System.Drawing.Size(297, 331);
            this.groupDevelopers.TabIndex = 14;
            this.groupDevelopers.TabStop = false;
            this.groupDevelopers.Text = "Developers (right-click to Sort)";
            // 
            // splitDevTop
            // 
            this.splitDevTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDevTop.Location = new System.Drawing.Point(3, 16);
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
            this.splitDevTop.Size = new System.Drawing.Size(291, 282);
            this.splitDevTop.SplitterDistance = 95;
            this.splitDevTop.TabIndex = 0;
            // 
            // clbDevelopersSelected
            // 
            this.clbDevelopersSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbDevelopersSelected.FormattingEnabled = true;
            this.clbDevelopersSelected.Location = new System.Drawing.Point(0, 0);
            this.clbDevelopersSelected.MultiColumn = true;
            this.clbDevelopersSelected.Name = "clbDevelopersSelected";
            this.clbDevelopersSelected.Size = new System.Drawing.Size(95, 100);
            this.clbDevelopersSelected.TabIndex = 13;
            this.clbDevelopersSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDevelopersSelected_ItemCheck);
            // 
            // tableLayoutPanelDevTop
            // 
            this.tableLayoutPanelDevTop.AutoSize = true;
            this.tableLayoutPanelDevTop.ColumnCount = 2;
            this.tableLayoutPanelDevTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDevTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDevTop.Controls.Add(this.btnDevSelected, 0, 0);
            this.tableLayoutPanelDevTop.Controls.Add(this.chkAllDevelopers, 1, 0);
            this.tableLayoutPanelDevTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelDevTop.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelDevTop.Name = "tableLayoutPanelDevTop";
            this.tableLayoutPanelDevTop.RowCount = 1;
            this.tableLayoutPanelDevTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDevTop.Size = new System.Drawing.Size(291, 29);
            this.tableLayoutPanelDevTop.TabIndex = 7;
            // 
            // btnDevSelected
            // 
            this.btnDevSelected.Location = new System.Drawing.Point(3, 3);
            this.btnDevSelected.Name = "btnDevSelected";
            this.btnDevSelected.Size = new System.Drawing.Size(23, 23);
            this.btnDevSelected.TabIndex = 6;
            this.btnDevSelected.Text = ">";
            this.btnDevSelected.UseVisualStyleBackColor = true;
            this.btnDevSelected.Click += new System.EventHandler(this.btnDevSelected_Click);
            // 
            // chkAllDevelopers
            // 
            this.chkAllDevelopers.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkAllDevelopers.AutoSize = true;
            this.chkAllDevelopers.Location = new System.Drawing.Point(194, 6);
            this.chkAllDevelopers.Name = "chkAllDevelopers";
            this.chkAllDevelopers.Size = new System.Drawing.Size(94, 17);
            this.chkAllDevelopers.TabIndex = 5;
            this.chkAllDevelopers.Text = "All Developers";
            this.chkAllDevelopers.UseVisualStyleBackColor = true;
            this.chkAllDevelopers.CheckedChanged += new System.EventHandler(this.chkAllDevelopers_CheckedChanged);
            // 
            // lstDevelopers
            // 
            this.lstDevelopers.CheckBoxes = true;
            this.lstDevelopers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDeveloper,
            this.columnDevCount});
            this.lstDevelopers.ContextMenuStrip = this.contextDev;
            this.lstDevelopers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDevelopers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstDevelopers.Location = new System.Drawing.Point(0, 29);
            this.lstDevelopers.Name = "lstDevelopers";
            this.lstDevelopers.Size = new System.Drawing.Size(291, 253);
            this.lstDevelopers.TabIndex = 10;
            this.lstDevelopers.UseCompatibleStateImageBehavior = false;
            this.lstDevelopers.View = System.Windows.Forms.View.List;
            this.lstDevelopers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstDevelopers_ItemChecked);
            // 
            // columnDeveloper
            // 
            this.columnDeveloper.Width = -1;
            // 
            // contextDev
            // 
            this.contextDev.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortToolStripMenuItem});
            this.contextDev.Name = "contextCat";
            this.contextDev.ShowImageMargin = false;
            this.contextDev.Size = new System.Drawing.Size(71, 26);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascendingDev,
            this.namedescendingDev,
            this.countascendingDev,
            this.countdescendingDev});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(70, 22);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // nameascendingDev
            // 
            this.nameascendingDev.Name = "nameascendingDev";
            this.nameascendingDev.Size = new System.Drawing.Size(179, 22);
            this.nameascendingDev.Text = "Name (ascending)";
            this.nameascendingDev.Click += new System.EventHandler(this.nameascendingDev_Click);
            // 
            // namedescendingDev
            // 
            this.namedescendingDev.Name = "namedescendingDev";
            this.namedescendingDev.Size = new System.Drawing.Size(179, 22);
            this.namedescendingDev.Text = "Name (descending)";
            this.namedescendingDev.Click += new System.EventHandler(this.namedescendingDev_Click);
            // 
            // countascendingDev
            // 
            this.countascendingDev.Name = "countascendingDev";
            this.countascendingDev.Size = new System.Drawing.Size(179, 22);
            this.countascendingDev.Text = "Count (ascending)";
            this.countascendingDev.Click += new System.EventHandler(this.countascendingDev_Click);
            // 
            // countdescendingDev
            // 
            this.countdescendingDev.Name = "countdescendingDev";
            this.countdescendingDev.Size = new System.Drawing.Size(179, 22);
            this.countdescendingDev.Text = "Count (descending)";
            this.countdescendingDev.Click += new System.EventHandler(this.countdescendingDev_Click);
            // 
            // tblIgnore
            // 
            this.tblIgnore.ColumnCount = 2;
            this.tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.Controls.Add(this.btnDevUncheckAll, 1, 0);
            this.tblIgnore.Controls.Add(this.btnDevCheckAll, 0, 0);
            this.tblIgnore.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblIgnore.Location = new System.Drawing.Point(3, 298);
            this.tblIgnore.Name = "tblIgnore";
            this.tblIgnore.RowCount = 1;
            this.tblIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.Size = new System.Drawing.Size(291, 30);
            this.tblIgnore.TabIndex = 11;
            // 
            // btnDevUncheckAll
            // 
            this.btnDevUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDevUncheckAll.Location = new System.Drawing.Point(148, 3);
            this.btnDevUncheckAll.Name = "btnDevUncheckAll";
            this.btnDevUncheckAll.Size = new System.Drawing.Size(140, 23);
            this.btnDevUncheckAll.TabIndex = 1;
            this.btnDevUncheckAll.Text = "Uncheck All";
            this.btnDevUncheckAll.UseVisualStyleBackColor = true;
            this.btnDevUncheckAll.Click += new System.EventHandler(this.btnDevUncheckAll_Click);
            // 
            // btnDevCheckAll
            // 
            this.btnDevCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDevCheckAll.Location = new System.Drawing.Point(3, 3);
            this.btnDevCheckAll.Name = "btnDevCheckAll";
            this.btnDevCheckAll.Size = new System.Drawing.Size(139, 23);
            this.btnDevCheckAll.TabIndex = 0;
            this.btnDevCheckAll.Text = "Check All";
            this.btnDevCheckAll.UseVisualStyleBackColor = true;
            this.btnDevCheckAll.Click += new System.EventHandler(this.btnDevCheckAll_Click);
            // 
            // groupPublishers
            // 
            this.groupPublishers.Controls.Add(this.splitPubTop);
            this.groupPublishers.Controls.Add(this.tableLayoutPanel1);
            this.groupPublishers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPublishers.Location = new System.Drawing.Point(0, 0);
            this.groupPublishers.Name = "groupPublishers";
            this.groupPublishers.Size = new System.Drawing.Size(303, 331);
            this.groupPublishers.TabIndex = 16;
            this.groupPublishers.TabStop = false;
            this.groupPublishers.Text = "Publishers (right-click to Sort)";
            // 
            // splitPubTop
            // 
            this.splitPubTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPubTop.Location = new System.Drawing.Point(3, 16);
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
            this.splitPubTop.Size = new System.Drawing.Size(297, 282);
            this.splitPubTop.SplitterDistance = 101;
            this.splitPubTop.TabIndex = 0;
            // 
            // clbPublishersSelected
            // 
            this.clbPublishersSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbPublishersSelected.FormattingEnabled = true;
            this.clbPublishersSelected.Location = new System.Drawing.Point(0, 0);
            this.clbPublishersSelected.MultiColumn = true;
            this.clbPublishersSelected.Name = "clbPublishersSelected";
            this.clbPublishersSelected.Size = new System.Drawing.Size(101, 100);
            this.clbPublishersSelected.TabIndex = 15;
            this.clbPublishersSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPublishersSelected_ItemCheck);
            // 
            // lstPublishers
            // 
            this.lstPublishers.CheckBoxes = true;
            this.lstPublishers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPublisher,
            this.columnPubCount});
            this.lstPublishers.ContextMenuStrip = this.contextPub;
            this.lstPublishers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPublishers.Location = new System.Drawing.Point(0, 29);
            this.lstPublishers.Name = "lstPublishers";
            this.lstPublishers.Size = new System.Drawing.Size(297, 253);
            this.lstPublishers.TabIndex = 10;
            this.lstPublishers.UseCompatibleStateImageBehavior = false;
            this.lstPublishers.View = System.Windows.Forms.View.List;
            this.lstPublishers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstPublishers_ItemChecked);
            // 
            // columnPublisher
            // 
            this.columnPublisher.Width = -1;
            // 
            // contextPub
            // 
            this.contextPub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextPub.Name = "contextCat";
            this.contextPub.ShowImageMargin = false;
            this.contextPub.Size = new System.Drawing.Size(71, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascendingPub,
            this.namedescendingPub,
            this.countascendingPub,
            this.countdescendingPub});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(70, 22);
            this.toolStripMenuItem1.Text = "Sort";
            // 
            // nameascendingPub
            // 
            this.nameascendingPub.Name = "nameascendingPub";
            this.nameascendingPub.Size = new System.Drawing.Size(179, 22);
            this.nameascendingPub.Text = "Name (ascending)";
            this.nameascendingPub.Click += new System.EventHandler(this.nameascendingPub_Click);
            // 
            // namedescendingPub
            // 
            this.namedescendingPub.Name = "namedescendingPub";
            this.namedescendingPub.Size = new System.Drawing.Size(179, 22);
            this.namedescendingPub.Text = "Name (descending)";
            this.namedescendingPub.Click += new System.EventHandler(this.namedescendingPub_Click);
            // 
            // countascendingPub
            // 
            this.countascendingPub.Name = "countascendingPub";
            this.countascendingPub.Size = new System.Drawing.Size(179, 22);
            this.countascendingPub.Text = "Count (ascending)";
            this.countascendingPub.Click += new System.EventHandler(this.countascendingPub_Click);
            // 
            // countdescendingPub
            // 
            this.countdescendingPub.Name = "countdescendingPub";
            this.countdescendingPub.Size = new System.Drawing.Size(179, 22);
            this.countdescendingPub.Text = "Count (descending)";
            this.countdescendingPub.Click += new System.EventHandler(this.countdescendingPub_Click);
            // 
            // tableLayoutPanelPublisherTop
            // 
            this.tableLayoutPanelPublisherTop.AutoSize = true;
            this.tableLayoutPanelPublisherTop.ColumnCount = 2;
            this.tableLayoutPanelPublisherTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelPublisherTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPublisherTop.Controls.Add(this.btnPubSelected, 0, 0);
            this.tableLayoutPanelPublisherTop.Controls.Add(this.chkAllPublishers, 1, 0);
            this.tableLayoutPanelPublisherTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelPublisherTop.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelPublisherTop.Name = "tableLayoutPanelPublisherTop";
            this.tableLayoutPanelPublisherTop.RowCount = 1;
            this.tableLayoutPanelPublisherTop.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelPublisherTop.Size = new System.Drawing.Size(297, 29);
            this.tableLayoutPanelPublisherTop.TabIndex = 8;
            // 
            // btnPubSelected
            // 
            this.btnPubSelected.Location = new System.Drawing.Point(3, 3);
            this.btnPubSelected.Name = "btnPubSelected";
            this.btnPubSelected.Size = new System.Drawing.Size(23, 23);
            this.btnPubSelected.TabIndex = 7;
            this.btnPubSelected.Text = ">";
            this.btnPubSelected.UseVisualStyleBackColor = true;
            this.btnPubSelected.Click += new System.EventHandler(this.btnPubSelected_Click);
            // 
            // chkAllPublishers
            // 
            this.chkAllPublishers.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkAllPublishers.AutoSize = true;
            this.chkAllPublishers.Location = new System.Drawing.Point(206, 6);
            this.chkAllPublishers.Name = "chkAllPublishers";
            this.chkAllPublishers.Size = new System.Drawing.Size(88, 17);
            this.chkAllPublishers.TabIndex = 5;
            this.chkAllPublishers.Text = "All Publishers";
            this.chkAllPublishers.UseVisualStyleBackColor = true;
            this.chkAllPublishers.CheckedChanged += new System.EventHandler(this.chkAllPublishers_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnPubCheckAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPubUncheckAll, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 298);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(297, 30);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // btnPubCheckAll
            // 
            this.btnPubCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPubCheckAll.Location = new System.Drawing.Point(3, 3);
            this.btnPubCheckAll.Name = "btnPubCheckAll";
            this.btnPubCheckAll.Size = new System.Drawing.Size(142, 23);
            this.btnPubCheckAll.TabIndex = 0;
            this.btnPubCheckAll.Text = "Check All";
            this.btnPubCheckAll.UseVisualStyleBackColor = true;
            this.btnPubCheckAll.Click += new System.EventHandler(this.btnPubCheckAll_Click);
            // 
            // btnPubUncheckAll
            // 
            this.btnPubUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPubUncheckAll.Location = new System.Drawing.Point(151, 3);
            this.btnPubUncheckAll.Name = "btnPubUncheckAll";
            this.btnPubUncheckAll.Size = new System.Drawing.Size(143, 23);
            this.btnPubUncheckAll.TabIndex = 1;
            this.btnPubUncheckAll.Text = "Uncheck All";
            this.btnPubUncheckAll.UseVisualStyleBackColor = true;
            this.btnPubUncheckAll.Click += new System.EventHandler(this.btnPubUncheckAll_Click);
            // 
            // panelTop
            // 
            this.panelTop.AutoSize = true;
            this.panelTop.Controls.Add(this.cmdListRebuild);
            this.panelTop.Controls.Add(this.list_helpOwnedOnly);
            this.panelTop.Controls.Add(this.chkOwnedOnly);
            this.panelTop.Controls.Add(this.list_helpScore);
            this.panelTop.Controls.Add(this.list_numScore);
            this.panelTop.Controls.Add(this.lblMinScore);
            this.panelTop.Controls.Add(this.lblPrefix);
            this.panelTop.Controls.Add(this.txtPrefix);
            this.panelTop.Controls.Add(this.helpPrefix);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(3, 16);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(604, 56);
            this.panelTop.TabIndex = 19;
            // 
            // cmdListRebuild
            // 
            this.cmdListRebuild.Location = new System.Drawing.Point(262, 7);
            this.cmdListRebuild.Name = "cmdListRebuild";
            this.cmdListRebuild.Size = new System.Drawing.Size(75, 42);
            this.cmdListRebuild.TabIndex = 18;
            this.cmdListRebuild.Text = "Rebuild List";
            this.cmdListRebuild.UseVisualStyleBackColor = true;
            this.cmdListRebuild.Click += new System.EventHandler(this.cmdListRebuild_Click);
            // 
            // list_helpOwnedOnly
            // 
            this.list_helpOwnedOnly.AutoSize = true;
            this.list_helpOwnedOnly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_helpOwnedOnly.Location = new System.Drawing.Point(241, 34);
            this.list_helpOwnedOnly.Name = "list_helpOwnedOnly";
            this.list_helpOwnedOnly.Size = new System.Drawing.Size(15, 15);
            this.list_helpOwnedOnly.TabIndex = 17;
            this.list_helpOwnedOnly.Text = "?";
            // 
            // chkOwnedOnly
            // 
            this.chkOwnedOnly.AutoSize = true;
            this.chkOwnedOnly.Location = new System.Drawing.Point(162, 33);
            this.chkOwnedOnly.Name = "chkOwnedOnly";
            this.chkOwnedOnly.Size = new System.Drawing.Size(82, 17);
            this.chkOwnedOnly.TabIndex = 16;
            this.chkOwnedOnly.Text = "Owned only";
            this.chkOwnedOnly.UseVisualStyleBackColor = true;
            // 
            // list_helpScore
            // 
            this.list_helpScore.AutoSize = true;
            this.list_helpScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_helpScore.Location = new System.Drawing.Point(129, 34);
            this.list_helpScore.Name = "list_helpScore";
            this.list_helpScore.Size = new System.Drawing.Size(15, 15);
            this.list_helpScore.TabIndex = 6;
            this.list_helpScore.Text = "?";
            // 
            // list_numScore
            // 
            this.list_numScore.Location = new System.Drawing.Point(8, 33);
            this.list_numScore.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.list_numScore.Name = "list_numScore";
            this.list_numScore.Size = new System.Drawing.Size(38, 20);
            this.list_numScore.TabIndex = 4;
            // 
            // lblMinScore
            // 
            this.lblMinScore.AutoSize = true;
            this.lblMinScore.Location = new System.Drawing.Point(52, 35);
            this.lblMinScore.Name = "lblMinScore";
            this.lblMinScore.Size = new System.Drawing.Size(71, 13);
            this.lblMinScore.TabIndex = 5;
            this.lblMinScore.Text = "Game count?";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(6, 8);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.lblPrefix.TabIndex = 0;
            this.lblPrefix.Text = "Prefix:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(42, 5);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(193, 20);
            this.txtPrefix.TabIndex = 1;
            // 
            // helpPrefix
            // 
            this.helpPrefix.AutoSize = true;
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Location = new System.Drawing.Point(241, 8);
            this.helpPrefix.Name = "helpPrefix";
            this.helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.helpPrefix.TabIndex = 2;
            this.helpPrefix.Text = "?";
            // 
            // AutoCatConfigPanel_DevPub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_DevPub";
            this.Size = new System.Drawing.Size(610, 406);
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
            this.tableLayoutPanelDevTop.ResumeLayout(false);
            this.tableLayoutPanelDevTop.PerformLayout();
            this.contextDev.ResumeLayout(false);
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
