namespace Depressurizer {
    partial class AutoCatConfigPanel_Manual {
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
            this.label2 = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.splitMainBottom = new System.Windows.Forms.SplitContainer();
            this.groupRemove = new System.Windows.Forms.GroupBox();
            this.splitRemoveTop = new System.Windows.Forms.SplitContainer();
            this.clbRemoveSelected = new System.Windows.Forms.CheckedListBox();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.chkRemoveAll = new System.Windows.Forms.CheckBox();
            this.lstRemove = new System.Windows.Forms.ListView();
            this.columnRemoveCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRemoveCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextRemove = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nameascendingRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.namedescendingRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.countascendingRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.countdescendingRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tblRemoveCheckAll = new System.Windows.Forms.TableLayoutPanel();
            this.btnRemoveUncheckAll = new System.Windows.Forms.Button();
            this.btnRemoveCheckAll = new System.Windows.Forms.Button();
            this.groupAdd = new System.Windows.Forms.GroupBox();
            this.splitAddTop = new System.Windows.Forms.SplitContainer();
            this.clbAddSelected = new System.Windows.Forms.CheckedListBox();
            this.btnAddSelected = new System.Windows.Forms.Button();
            this.lstAdd = new System.Windows.Forms.ListView();
            this.columnAddCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAddCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameascendingAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.namedescendingAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.countascendingAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.countdescendingAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tblAddCheckAll = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddCheckAll = new System.Windows.Forms.Button();
            this.btnAddUncheckAll = new System.Windows.Forms.Button();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelRemoveTop = new System.Windows.Forms.Panel();
            this.panelAddTop = new System.Windows.Forms.Panel();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMainBottom)).BeginInit();
            this.splitMainBottom.Panel1.SuspendLayout();
            this.splitMainBottom.Panel2.SuspendLayout();
            this.splitMainBottom.SuspendLayout();
            this.groupRemove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRemoveTop)).BeginInit();
            this.splitRemoveTop.Panel1.SuspendLayout();
            this.splitRemoveTop.Panel2.SuspendLayout();
            this.splitRemoveTop.SuspendLayout();
            this.contextRemove.SuspendLayout();
            this.tblRemoveCheckAll.SuspendLayout();
            this.groupAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAddTop)).BeginInit();
            this.splitAddTop.Panel1.SuspendLayout();
            this.splitAddTop.Panel2.SuspendLayout();
            this.splitAddTop.SuspendLayout();
            this.contextAdd.SuspendLayout();
            this.tblAddCheckAll.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelRemoveTop.SuspendLayout();
            this.panelAddTop.SuspendLayout();
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
            this.grpMain.Text = "Edit Manual AutoCat";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(459, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Right-click in lists for Sorting";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(3, 9);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.lblPrefix.TabIndex = 0;
            this.lblPrefix.Text = "Prefix:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(45, 6);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.txtPrefix.TabIndex = 1;
            // 
            // helpPrefix
            // 
            this.helpPrefix.AutoSize = true;
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Location = new System.Drawing.Point(216, 9);
            this.helpPrefix.Name = "helpPrefix";
            this.helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.helpPrefix.TabIndex = 2;
            this.helpPrefix.Text = "?";
            // 
            // splitMainBottom
            // 
            this.splitMainBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMainBottom.Location = new System.Drawing.Point(3, 45);
            this.splitMainBottom.Name = "splitMainBottom";
            // 
            // splitMainBottom.Panel1
            // 
            this.splitMainBottom.Panel1.Controls.Add(this.groupRemove);
            // 
            // splitMainBottom.Panel2
            // 
            this.splitMainBottom.Panel2.Controls.Add(this.groupAdd);
            this.splitMainBottom.Size = new System.Drawing.Size(604, 358);
            this.splitMainBottom.SplitterDistance = 291;
            this.splitMainBottom.TabIndex = 0;
            // 
            // groupRemove
            // 
            this.groupRemove.Controls.Add(this.splitRemoveTop);
            this.groupRemove.Controls.Add(this.tblRemoveCheckAll);
            this.groupRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupRemove.Location = new System.Drawing.Point(0, 0);
            this.groupRemove.Name = "groupRemove";
            this.groupRemove.Size = new System.Drawing.Size(291, 358);
            this.groupRemove.TabIndex = 14;
            this.groupRemove.TabStop = false;
            this.groupRemove.Text = "Remove";
            // 
            // splitRemoveTop
            // 
            this.splitRemoveTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRemoveTop.Location = new System.Drawing.Point(3, 16);
            this.splitRemoveTop.Name = "splitRemoveTop";
            // 
            // splitRemoveTop.Panel1
            // 
            this.splitRemoveTop.Panel1.Controls.Add(this.clbRemoveSelected);
            this.splitRemoveTop.Panel1Collapsed = true;
            // 
            // splitRemoveTop.Panel2
            // 
            this.splitRemoveTop.Panel2.Controls.Add(this.lstRemove);
            this.splitRemoveTop.Panel2.Controls.Add(this.panelRemoveTop);
            this.splitRemoveTop.Size = new System.Drawing.Size(285, 310);
            this.splitRemoveTop.SplitterDistance = 95;
            this.splitRemoveTop.TabIndex = 0;
            // 
            // clbRemoveSelected
            // 
            this.clbRemoveSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbRemoveSelected.FormattingEnabled = true;
            this.clbRemoveSelected.Location = new System.Drawing.Point(0, 0);
            this.clbRemoveSelected.MultiColumn = true;
            this.clbRemoveSelected.Name = "clbRemoveSelected";
            this.clbRemoveSelected.Size = new System.Drawing.Size(95, 100);
            this.clbRemoveSelected.TabIndex = 13;
            this.clbRemoveSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbRemoveSelected_ItemCheck);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(0, 0);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(23, 23);
            this.btnRemoveSelected.TabIndex = 8;
            this.btnRemoveSelected.Text = ">";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // chkRemoveAll
            // 
            this.chkRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRemoveAll.AutoSize = true;
            this.chkRemoveAll.Location = new System.Drawing.Point(203, 3);
            this.chkRemoveAll.Name = "chkRemoveAll";
            this.chkRemoveAll.Size = new System.Drawing.Size(79, 17);
            this.chkRemoveAll.TabIndex = 5;
            this.chkRemoveAll.Text = "Remove all";
            this.chkRemoveAll.UseVisualStyleBackColor = true;
            this.chkRemoveAll.CheckedChanged += new System.EventHandler(this.chkRemoveAll_CheckedChanged);
            // 
            // lstRemove
            // 
            this.lstRemove.CheckBoxes = true;
            this.lstRemove.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnRemoveCategory,
            this.columnRemoveCount});
            this.lstRemove.ContextMenuStrip = this.contextRemove;
            this.lstRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRemove.Location = new System.Drawing.Point(0, 26);
            this.lstRemove.Name = "lstRemove";
            this.lstRemove.Size = new System.Drawing.Size(285, 284);
            this.lstRemove.TabIndex = 10;
            this.lstRemove.UseCompatibleStateImageBehavior = false;
            this.lstRemove.View = System.Windows.Forms.View.List;
            this.lstRemove.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstRemove_ItemChecked);
            // 
            // contextRemove
            // 
            this.contextRemove.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextRemove.Name = "contextCat";
            this.contextRemove.ShowImageMargin = false;
            this.contextRemove.Size = new System.Drawing.Size(71, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascendingRemove,
            this.namedescendingRemove,
            this.countascendingRemove,
            this.countdescendingRemove});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(70, 22);
            this.toolStripMenuItem1.Text = "Sort";
            // 
            // nameascendingRemove
            // 
            this.nameascendingRemove.Name = "nameascendingRemove";
            this.nameascendingRemove.Size = new System.Drawing.Size(179, 22);
            this.nameascendingRemove.Text = "Name (ascending)";
            this.nameascendingRemove.Click += new System.EventHandler(this.nameascendingRemove_Click);
            // 
            // namedescendingRemove
            // 
            this.namedescendingRemove.Name = "namedescendingRemove";
            this.namedescendingRemove.Size = new System.Drawing.Size(179, 22);
            this.namedescendingRemove.Text = "Name (descending)";
            this.namedescendingRemove.Click += new System.EventHandler(this.namedescendingRemove_Click);
            // 
            // countascendingRemove
            // 
            this.countascendingRemove.Name = "countascendingRemove";
            this.countascendingRemove.Size = new System.Drawing.Size(179, 22);
            this.countascendingRemove.Text = "Count (ascending)";
            this.countascendingRemove.Click += new System.EventHandler(this.countascendingRemove_Click);
            // 
            // countdescendingRemove
            // 
            this.countdescendingRemove.Name = "countdescendingRemove";
            this.countdescendingRemove.Size = new System.Drawing.Size(179, 22);
            this.countdescendingRemove.Text = "Count (descending)";
            this.countdescendingRemove.Click += new System.EventHandler(this.countdescendingRemove_Click);
            // 
            // tblRemoveCheckAll
            // 
            this.tblRemoveCheckAll.AutoSize = true;
            this.tblRemoveCheckAll.ColumnCount = 2;
            this.tblRemoveCheckAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRemoveCheckAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRemoveCheckAll.Controls.Add(this.btnRemoveUncheckAll, 1, 0);
            this.tblRemoveCheckAll.Controls.Add(this.btnRemoveCheckAll, 0, 0);
            this.tblRemoveCheckAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblRemoveCheckAll.Location = new System.Drawing.Point(3, 326);
            this.tblRemoveCheckAll.Name = "tblRemoveCheckAll";
            this.tblRemoveCheckAll.RowCount = 1;
            this.tblRemoveCheckAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRemoveCheckAll.Size = new System.Drawing.Size(285, 29);
            this.tblRemoveCheckAll.TabIndex = 11;
            // 
            // btnRemoveUncheckAll
            // 
            this.btnRemoveUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveUncheckAll.Location = new System.Drawing.Point(145, 3);
            this.btnRemoveUncheckAll.Name = "btnRemoveUncheckAll";
            this.btnRemoveUncheckAll.Size = new System.Drawing.Size(137, 23);
            this.btnRemoveUncheckAll.TabIndex = 1;
            this.btnRemoveUncheckAll.Text = "Uncheck All";
            this.btnRemoveUncheckAll.UseVisualStyleBackColor = true;
            this.btnRemoveUncheckAll.Click += new System.EventHandler(this.btnRemoveUncheckAll_Click);
            // 
            // btnRemoveCheckAll
            // 
            this.btnRemoveCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveCheckAll.Location = new System.Drawing.Point(3, 3);
            this.btnRemoveCheckAll.Name = "btnRemoveCheckAll";
            this.btnRemoveCheckAll.Size = new System.Drawing.Size(136, 23);
            this.btnRemoveCheckAll.TabIndex = 0;
            this.btnRemoveCheckAll.Text = "Check All";
            this.btnRemoveCheckAll.UseVisualStyleBackColor = true;
            this.btnRemoveCheckAll.Click += new System.EventHandler(this.btnRemoveCheckAll_Click);
            // 
            // groupAdd
            // 
            this.groupAdd.Controls.Add(this.splitAddTop);
            this.groupAdd.Controls.Add(this.tblAddCheckAll);
            this.groupAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAdd.Location = new System.Drawing.Point(0, 0);
            this.groupAdd.Name = "groupAdd";
            this.groupAdd.Size = new System.Drawing.Size(309, 358);
            this.groupAdd.TabIndex = 16;
            this.groupAdd.TabStop = false;
            this.groupAdd.Text = "Add";
            // 
            // splitAddTop
            // 
            this.splitAddTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitAddTop.Location = new System.Drawing.Point(3, 16);
            this.splitAddTop.Name = "splitAddTop";
            // 
            // splitAddTop.Panel1
            // 
            this.splitAddTop.Panel1.Controls.Add(this.clbAddSelected);
            this.splitAddTop.Panel1Collapsed = true;
            // 
            // splitAddTop.Panel2
            // 
            this.splitAddTop.Panel2.Controls.Add(this.lstAdd);
            this.splitAddTop.Panel2.Controls.Add(this.panelAddTop);
            this.splitAddTop.Size = new System.Drawing.Size(303, 310);
            this.splitAddTop.SplitterDistance = 101;
            this.splitAddTop.TabIndex = 0;
            // 
            // clbAddSelected
            // 
            this.clbAddSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbAddSelected.FormattingEnabled = true;
            this.clbAddSelected.Location = new System.Drawing.Point(0, 0);
            this.clbAddSelected.MultiColumn = true;
            this.clbAddSelected.Name = "clbAddSelected";
            this.clbAddSelected.Size = new System.Drawing.Size(101, 100);
            this.clbAddSelected.TabIndex = 15;
            this.clbAddSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbAddSelected_ItemCheck);
            // 
            // btnAddSelected
            // 
            this.btnAddSelected.Location = new System.Drawing.Point(0, 0);
            this.btnAddSelected.Name = "btnAddSelected";
            this.btnAddSelected.Size = new System.Drawing.Size(23, 23);
            this.btnAddSelected.TabIndex = 8;
            this.btnAddSelected.Text = ">";
            this.btnAddSelected.UseVisualStyleBackColor = true;
            this.btnAddSelected.Click += new System.EventHandler(this.btnAddSelected_Click);
            // 
            // lstAdd
            // 
            this.lstAdd.CheckBoxes = true;
            this.lstAdd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnAddCategory,
            this.columnAddCount});
            this.lstAdd.ContextMenuStrip = this.contextAdd;
            this.lstAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAdd.Location = new System.Drawing.Point(0, 26);
            this.lstAdd.Name = "lstAdd";
            this.lstAdd.Size = new System.Drawing.Size(303, 284);
            this.lstAdd.TabIndex = 13;
            this.lstAdd.UseCompatibleStateImageBehavior = false;
            this.lstAdd.View = System.Windows.Forms.View.List;
            this.lstAdd.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstAdd_ItemChecked);
            // 
            // contextAdd
            // 
            this.contextAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortToolStripMenuItem});
            this.contextAdd.Name = "contextCat";
            this.contextAdd.ShowImageMargin = false;
            this.contextAdd.Size = new System.Drawing.Size(71, 26);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascendingAdd,
            this.namedescendingAdd,
            this.countascendingAdd,
            this.countdescendingAdd});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(70, 22);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // nameascendingAdd
            // 
            this.nameascendingAdd.Name = "nameascendingAdd";
            this.nameascendingAdd.Size = new System.Drawing.Size(179, 22);
            this.nameascendingAdd.Text = "Name (ascending)";
            this.nameascendingAdd.Click += new System.EventHandler(this.nameascendingAdd_Click);
            // 
            // namedescendingAdd
            // 
            this.namedescendingAdd.Name = "namedescendingAdd";
            this.namedescendingAdd.Size = new System.Drawing.Size(179, 22);
            this.namedescendingAdd.Text = "Name (descending)";
            this.namedescendingAdd.Click += new System.EventHandler(this.namedescendingAdd_Click);
            // 
            // countascendingAdd
            // 
            this.countascendingAdd.Name = "countascendingAdd";
            this.countascendingAdd.Size = new System.Drawing.Size(179, 22);
            this.countascendingAdd.Text = "Count (ascending)";
            this.countascendingAdd.Click += new System.EventHandler(this.countascendingAdd_Click);
            // 
            // countdescendingAdd
            // 
            this.countdescendingAdd.Name = "countdescendingAdd";
            this.countdescendingAdd.Size = new System.Drawing.Size(179, 22);
            this.countdescendingAdd.Text = "Count (descending)";
            this.countdescendingAdd.Click += new System.EventHandler(this.countdescendingAdd_Click);
            // 
            // tblAddCheckAll
            // 
            this.tblAddCheckAll.AutoSize = true;
            this.tblAddCheckAll.ColumnCount = 2;
            this.tblAddCheckAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAddCheckAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAddCheckAll.Controls.Add(this.btnAddCheckAll, 0, 0);
            this.tblAddCheckAll.Controls.Add(this.btnAddUncheckAll, 1, 0);
            this.tblAddCheckAll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblAddCheckAll.Location = new System.Drawing.Point(3, 326);
            this.tblAddCheckAll.Name = "tblAddCheckAll";
            this.tblAddCheckAll.RowCount = 1;
            this.tblAddCheckAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAddCheckAll.Size = new System.Drawing.Size(303, 29);
            this.tblAddCheckAll.TabIndex = 14;
            // 
            // btnAddCheckAll
            // 
            this.btnAddCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCheckAll.Location = new System.Drawing.Point(3, 3);
            this.btnAddCheckAll.Name = "btnAddCheckAll";
            this.btnAddCheckAll.Size = new System.Drawing.Size(145, 23);
            this.btnAddCheckAll.TabIndex = 0;
            this.btnAddCheckAll.Text = "Check All";
            this.btnAddCheckAll.UseVisualStyleBackColor = true;
            this.btnAddCheckAll.Click += new System.EventHandler(this.btnAddCheckAll_Click);
            // 
            // btnAddUncheckAll
            // 
            this.btnAddUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddUncheckAll.Location = new System.Drawing.Point(154, 3);
            this.btnAddUncheckAll.Name = "btnAddUncheckAll";
            this.btnAddUncheckAll.Size = new System.Drawing.Size(146, 23);
            this.btnAddUncheckAll.TabIndex = 1;
            this.btnAddUncheckAll.Text = "Uncheck All";
            this.btnAddUncheckAll.UseVisualStyleBackColor = true;
            this.btnAddUncheckAll.Click += new System.EventHandler(this.btnAddUncheckAll_Click);
            // 
            // panelTop
            // 
            this.panelTop.AutoSize = true;
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.lblPrefix);
            this.panelTop.Controls.Add(this.txtPrefix);
            this.panelTop.Controls.Add(this.helpPrefix);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(3, 16);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(604, 29);
            this.panelTop.TabIndex = 4;
            // 
            // panelRemoveTop
            // 
            this.panelRemoveTop.AutoSize = true;
            this.panelRemoveTop.Controls.Add(this.btnRemoveSelected);
            this.panelRemoveTop.Controls.Add(this.chkRemoveAll);
            this.panelRemoveTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRemoveTop.Location = new System.Drawing.Point(0, 0);
            this.panelRemoveTop.Name = "panelRemoveTop";
            this.panelRemoveTop.Size = new System.Drawing.Size(285, 26);
            this.panelRemoveTop.TabIndex = 9;
            // 
            // panelAddTop
            // 
            this.panelAddTop.AutoSize = true;
            this.panelAddTop.Controls.Add(this.btnAddSelected);
            this.panelAddTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAddTop.Location = new System.Drawing.Point(0, 0);
            this.panelAddTop.Name = "panelAddTop";
            this.panelAddTop.Size = new System.Drawing.Size(303, 26);
            this.panelAddTop.TabIndex = 9;
            // 
            // AutoCatConfigPanel_Manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Manual";
            this.Size = new System.Drawing.Size(610, 406);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.splitMainBottom.Panel1.ResumeLayout(false);
            this.splitMainBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMainBottom)).EndInit();
            this.splitMainBottom.ResumeLayout(false);
            this.groupRemove.ResumeLayout(false);
            this.groupRemove.PerformLayout();
            this.splitRemoveTop.Panel1.ResumeLayout(false);
            this.splitRemoveTop.Panel2.ResumeLayout(false);
            this.splitRemoveTop.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRemoveTop)).EndInit();
            this.splitRemoveTop.ResumeLayout(false);
            this.contextRemove.ResumeLayout(false);
            this.tblRemoveCheckAll.ResumeLayout(false);
            this.groupAdd.ResumeLayout(false);
            this.groupAdd.PerformLayout();
            this.splitAddTop.Panel1.ResumeLayout(false);
            this.splitAddTop.Panel2.ResumeLayout(false);
            this.splitAddTop.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAddTop)).EndInit();
            this.splitAddTop.ResumeLayout(false);
            this.contextAdd.ResumeLayout(false);
            this.tblAddCheckAll.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelRemoveTop.ResumeLayout(false);
            this.panelRemoveTop.PerformLayout();
            this.panelAddTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TableLayoutPanel tblRemoveCheckAll;
        private System.Windows.Forms.Button btnRemoveUncheckAll;
        private System.Windows.Forms.Button btnRemoveCheckAll;
        private System.Windows.Forms.ListView lstRemove;
        private System.Windows.Forms.CheckBox chkRemoveAll;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.SplitContainer splitMainBottom;
        private System.Windows.Forms.ListView lstAdd;
        private System.Windows.Forms.TableLayoutPanel tblAddCheckAll;
        private System.Windows.Forms.Button btnAddUncheckAll;
        private System.Windows.Forms.Button btnAddCheckAll;
        private System.Windows.Forms.CheckedListBox clbRemoveSelected;
        private System.Windows.Forms.CheckedListBox clbAddSelected;
        private System.Windows.Forms.GroupBox groupRemove;
        private System.Windows.Forms.SplitContainer splitRemoveTop;
        private System.Windows.Forms.GroupBox groupAdd;
        private System.Windows.Forms.SplitContainer splitAddTop;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnAddSelected;
        private System.Windows.Forms.ColumnHeader columnRemoveCategory;
        private System.Windows.Forms.ColumnHeader columnRemoveCount;
        private System.Windows.Forms.ColumnHeader columnAddCategory;
        private System.Windows.Forms.ColumnHeader columnAddCount;
        private System.Windows.Forms.ContextMenuStrip contextRemove;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nameascendingRemove;
        private System.Windows.Forms.ToolStripMenuItem namedescendingRemove;
        private System.Windows.Forms.ToolStripMenuItem countascendingRemove;
        private System.Windows.Forms.ToolStripMenuItem countdescendingRemove;
        private System.Windows.Forms.ContextMenuStrip contextAdd;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameascendingAdd;
        private System.Windows.Forms.ToolStripMenuItem namedescendingAdd;
        private System.Windows.Forms.ToolStripMenuItem countascendingAdd;
        private System.Windows.Forms.ToolStripMenuItem countdescendingAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelRemoveTop;
        private System.Windows.Forms.Panel panelAddTop;
    }
}
