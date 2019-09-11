namespace Depressurizer.AutoCats {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Manual));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.splitMainBottom = new System.Windows.Forms.SplitContainer();
            this.groupRemove = new System.Windows.Forms.GroupBox();
            this.splitRemoveTop = new System.Windows.Forms.SplitContainer();
            this.clbRemoveSelected = new System.Windows.Forms.CheckedListBox();
            this.lstRemove = new System.Windows.Forms.ListView();
            this.columnRemoveCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRemoveCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextRemove = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nameascendingRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.namedescendingRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.countascendingRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.countdescendingRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.panelRemoveTop = new System.Windows.Forms.Panel();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.chkRemoveAll = new System.Windows.Forms.CheckBox();
            this.tblRemoveCheckAll = new System.Windows.Forms.TableLayoutPanel();
            this.btnRemoveUncheckAll = new System.Windows.Forms.Button();
            this.btnRemoveCheckAll = new System.Windows.Forms.Button();
            this.groupAdd = new System.Windows.Forms.GroupBox();
            this.splitAddTop = new System.Windows.Forms.SplitContainer();
            this.clbAddSelected = new System.Windows.Forms.CheckedListBox();
            this.lstAdd = new System.Windows.Forms.ListView();
            this.columnAddCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAddCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameascendingAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.namedescendingAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.countascendingAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.countdescendingAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.panelAddTop = new System.Windows.Forms.Panel();
            this.btnAddSelected = new System.Windows.Forms.Button();
            this.tblAddCheckAll = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddCheckAll = new System.Windows.Forms.Button();
            this.btnAddUncheckAll = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
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
            this.panelRemoveTop.SuspendLayout();
            this.tblRemoveCheckAll.SuspendLayout();
            this.groupAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAddTop)).BeginInit();
            this.splitAddTop.Panel1.SuspendLayout();
            this.splitAddTop.Panel2.SuspendLayout();
            this.splitAddTop.SuspendLayout();
            this.contextAdd.SuspendLayout();
            this.panelAddTop.SuspendLayout();
            this.tblAddCheckAll.SuspendLayout();
            this.panelTop.SuspendLayout();
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
            this.splitMainBottom.Panel1.Controls.Add(this.groupRemove);
            // 
            // splitMainBottom.Panel2
            // 
            this.splitMainBottom.Panel2.Controls.Add(this.groupAdd);
            // 
            // groupRemove
            // 
            this.groupRemove.Controls.Add(this.splitRemoveTop);
            this.groupRemove.Controls.Add(this.tblRemoveCheckAll);
            resources.ApplyResources(this.groupRemove, "groupRemove");
            this.groupRemove.Name = "groupRemove";
            this.groupRemove.TabStop = false;
            // 
            // splitRemoveTop
            // 
            resources.ApplyResources(this.splitRemoveTop, "splitRemoveTop");
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
            // 
            // clbRemoveSelected
            // 
            resources.ApplyResources(this.clbRemoveSelected, "clbRemoveSelected");
            this.clbRemoveSelected.FormattingEnabled = true;
            this.clbRemoveSelected.MultiColumn = true;
            this.clbRemoveSelected.Name = "clbRemoveSelected";
            this.clbRemoveSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbRemoveSelected_ItemCheck);
            // 
            // lstRemove
            // 
            this.lstRemove.CheckBoxes = true;
            this.lstRemove.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnRemoveCategory,
            this.columnRemoveCount});
            this.lstRemove.ContextMenuStrip = this.contextRemove;
            resources.ApplyResources(this.lstRemove, "lstRemove");
            this.lstRemove.HideSelection = false;
            this.lstRemove.Name = "lstRemove";
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
            resources.ApplyResources(this.contextRemove, "contextRemove");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascendingRemove,
            this.namedescendingRemove,
            this.countascendingRemove,
            this.countdescendingRemove});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // nameascendingRemove
            // 
            this.nameascendingRemove.Name = "nameascendingRemove";
            resources.ApplyResources(this.nameascendingRemove, "nameascendingRemove");
            this.nameascendingRemove.Click += new System.EventHandler(this.nameascendingRemove_Click);
            // 
            // namedescendingRemove
            // 
            this.namedescendingRemove.Name = "namedescendingRemove";
            resources.ApplyResources(this.namedescendingRemove, "namedescendingRemove");
            this.namedescendingRemove.Click += new System.EventHandler(this.namedescendingRemove_Click);
            // 
            // countascendingRemove
            // 
            this.countascendingRemove.Name = "countascendingRemove";
            resources.ApplyResources(this.countascendingRemove, "countascendingRemove");
            this.countascendingRemove.Click += new System.EventHandler(this.countascendingRemove_Click);
            // 
            // countdescendingRemove
            // 
            this.countdescendingRemove.Name = "countdescendingRemove";
            resources.ApplyResources(this.countdescendingRemove, "countdescendingRemove");
            this.countdescendingRemove.Click += new System.EventHandler(this.countdescendingRemove_Click);
            // 
            // panelRemoveTop
            // 
            resources.ApplyResources(this.panelRemoveTop, "panelRemoveTop");
            this.panelRemoveTop.Controls.Add(this.btnRemoveSelected);
            this.panelRemoveTop.Controls.Add(this.chkRemoveAll);
            this.panelRemoveTop.Name = "panelRemoveTop";
            // 
            // btnRemoveSelected
            // 
            resources.ApplyResources(this.btnRemoveSelected, "btnRemoveSelected");
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // chkRemoveAll
            // 
            resources.ApplyResources(this.chkRemoveAll, "chkRemoveAll");
            this.chkRemoveAll.Name = "chkRemoveAll";
            this.chkRemoveAll.UseVisualStyleBackColor = true;
            this.chkRemoveAll.CheckedChanged += new System.EventHandler(this.chkRemoveAll_CheckedChanged);
            // 
            // tblRemoveCheckAll
            // 
            resources.ApplyResources(this.tblRemoveCheckAll, "tblRemoveCheckAll");
            this.tblRemoveCheckAll.Controls.Add(this.btnRemoveUncheckAll, 1, 0);
            this.tblRemoveCheckAll.Controls.Add(this.btnRemoveCheckAll, 0, 0);
            this.tblRemoveCheckAll.Name = "tblRemoveCheckAll";
            // 
            // btnRemoveUncheckAll
            // 
            resources.ApplyResources(this.btnRemoveUncheckAll, "btnRemoveUncheckAll");
            this.btnRemoveUncheckAll.Name = "btnRemoveUncheckAll";
            this.btnRemoveUncheckAll.UseVisualStyleBackColor = true;
            this.btnRemoveUncheckAll.Click += new System.EventHandler(this.btnRemoveUncheckAll_Click);
            // 
            // btnRemoveCheckAll
            // 
            resources.ApplyResources(this.btnRemoveCheckAll, "btnRemoveCheckAll");
            this.btnRemoveCheckAll.Name = "btnRemoveCheckAll";
            this.btnRemoveCheckAll.UseVisualStyleBackColor = true;
            this.btnRemoveCheckAll.Click += new System.EventHandler(this.btnRemoveCheckAll_Click);
            // 
            // groupAdd
            // 
            this.groupAdd.Controls.Add(this.splitAddTop);
            this.groupAdd.Controls.Add(this.tblAddCheckAll);
            resources.ApplyResources(this.groupAdd, "groupAdd");
            this.groupAdd.Name = "groupAdd";
            this.groupAdd.TabStop = false;
            // 
            // splitAddTop
            // 
            resources.ApplyResources(this.splitAddTop, "splitAddTop");
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
            // 
            // clbAddSelected
            // 
            resources.ApplyResources(this.clbAddSelected, "clbAddSelected");
            this.clbAddSelected.FormattingEnabled = true;
            this.clbAddSelected.MultiColumn = true;
            this.clbAddSelected.Name = "clbAddSelected";
            this.clbAddSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbAddSelected_ItemCheck);
            // 
            // lstAdd
            // 
            this.lstAdd.CheckBoxes = true;
            this.lstAdd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnAddCategory,
            this.columnAddCount});
            this.lstAdd.ContextMenuStrip = this.contextAdd;
            resources.ApplyResources(this.lstAdd, "lstAdd");
            this.lstAdd.HideSelection = false;
            this.lstAdd.Name = "lstAdd";
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
            resources.ApplyResources(this.contextAdd, "contextAdd");
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameascendingAdd,
            this.namedescendingAdd,
            this.countascendingAdd,
            this.countdescendingAdd});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            resources.ApplyResources(this.sortToolStripMenuItem, "sortToolStripMenuItem");
            // 
            // nameascendingAdd
            // 
            this.nameascendingAdd.Name = "nameascendingAdd";
            resources.ApplyResources(this.nameascendingAdd, "nameascendingAdd");
            this.nameascendingAdd.Click += new System.EventHandler(this.nameascendingAdd_Click);
            // 
            // namedescendingAdd
            // 
            this.namedescendingAdd.Name = "namedescendingAdd";
            resources.ApplyResources(this.namedescendingAdd, "namedescendingAdd");
            this.namedescendingAdd.Click += new System.EventHandler(this.namedescendingAdd_Click);
            // 
            // countascendingAdd
            // 
            this.countascendingAdd.Name = "countascendingAdd";
            resources.ApplyResources(this.countascendingAdd, "countascendingAdd");
            this.countascendingAdd.Click += new System.EventHandler(this.countascendingAdd_Click);
            // 
            // countdescendingAdd
            // 
            this.countdescendingAdd.Name = "countdescendingAdd";
            resources.ApplyResources(this.countdescendingAdd, "countdescendingAdd");
            this.countdescendingAdd.Click += new System.EventHandler(this.countdescendingAdd_Click);
            // 
            // panelAddTop
            // 
            resources.ApplyResources(this.panelAddTop, "panelAddTop");
            this.panelAddTop.Controls.Add(this.btnAddSelected);
            this.panelAddTop.Name = "panelAddTop";
            // 
            // btnAddSelected
            // 
            resources.ApplyResources(this.btnAddSelected, "btnAddSelected");
            this.btnAddSelected.Name = "btnAddSelected";
            this.btnAddSelected.UseVisualStyleBackColor = true;
            this.btnAddSelected.Click += new System.EventHandler(this.btnAddSelected_Click);
            // 
            // tblAddCheckAll
            // 
            resources.ApplyResources(this.tblAddCheckAll, "tblAddCheckAll");
            this.tblAddCheckAll.Controls.Add(this.btnAddCheckAll, 0, 0);
            this.tblAddCheckAll.Controls.Add(this.btnAddUncheckAll, 1, 0);
            this.tblAddCheckAll.Name = "tblAddCheckAll";
            // 
            // btnAddCheckAll
            // 
            resources.ApplyResources(this.btnAddCheckAll, "btnAddCheckAll");
            this.btnAddCheckAll.Name = "btnAddCheckAll";
            this.btnAddCheckAll.UseVisualStyleBackColor = true;
            this.btnAddCheckAll.Click += new System.EventHandler(this.btnAddCheckAll_Click);
            // 
            // btnAddUncheckAll
            // 
            resources.ApplyResources(this.btnAddUncheckAll, "btnAddUncheckAll");
            this.btnAddUncheckAll.Name = "btnAddUncheckAll";
            this.btnAddUncheckAll.UseVisualStyleBackColor = true;
            this.btnAddUncheckAll.Click += new System.EventHandler(this.btnAddUncheckAll_Click);
            // 
            // panelTop
            // 
            resources.ApplyResources(this.panelTop, "panelTop");
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.lblPrefix);
            this.panelTop.Controls.Add(this.txtPrefix);
            this.panelTop.Controls.Add(this.helpPrefix);
            this.panelTop.Name = "panelTop";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // AutoCatConfigPanel_Manual
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Manual";
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
            this.panelRemoveTop.ResumeLayout(false);
            this.panelRemoveTop.PerformLayout();
            this.tblRemoveCheckAll.ResumeLayout(false);
            this.groupAdd.ResumeLayout(false);
            this.groupAdd.PerformLayout();
            this.splitAddTop.Panel1.ResumeLayout(false);
            this.splitAddTop.Panel2.ResumeLayout(false);
            this.splitAddTop.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAddTop)).EndInit();
            this.splitAddTop.ResumeLayout(false);
            this.contextAdd.ResumeLayout(false);
            this.panelAddTop.ResumeLayout(false);
            this.tblAddCheckAll.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
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
