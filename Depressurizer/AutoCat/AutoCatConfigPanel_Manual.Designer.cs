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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.splitMainBottom = new System.Windows.Forms.SplitContainer();
            this.groupRemove = new System.Windows.Forms.GroupBox();
            this.splitRemoveMain = new System.Windows.Forms.SplitContainer();
            this.splitRemoveTop = new System.Windows.Forms.SplitContainer();
            this.clbRemoveSelected = new System.Windows.Forms.CheckedListBox();
            this.splitRemoveRight = new System.Windows.Forms.SplitContainer();
            this.chkRemoveAll = new System.Windows.Forms.CheckBox();
            this.lstRemove = new System.Windows.Forms.ListView();
            this.tblIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.btnRemoveUncheckAll = new System.Windows.Forms.Button();
            this.btnRemoveCheckAll = new System.Windows.Forms.Button();
            this.groupAdd = new System.Windows.Forms.GroupBox();
            this.splitAddMain = new System.Windows.Forms.SplitContainer();
            this.splitAddTop = new System.Windows.Forms.SplitContainer();
            this.clbAddSelected = new System.Windows.Forms.CheckedListBox();
            this.lstAdd = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddCheckAll = new System.Windows.Forms.Button();
            this.btnAddUncheckAll = new System.Windows.Forms.Button();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMainBottom)).BeginInit();
            this.splitMainBottom.Panel1.SuspendLayout();
            this.splitMainBottom.Panel2.SuspendLayout();
            this.splitMainBottom.SuspendLayout();
            this.groupRemove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRemoveMain)).BeginInit();
            this.splitRemoveMain.Panel1.SuspendLayout();
            this.splitRemoveMain.Panel2.SuspendLayout();
            this.splitRemoveMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRemoveTop)).BeginInit();
            this.splitRemoveTop.Panel1.SuspendLayout();
            this.splitRemoveTop.Panel2.SuspendLayout();
            this.splitRemoveTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRemoveRight)).BeginInit();
            this.splitRemoveRight.Panel1.SuspendLayout();
            this.splitRemoveRight.Panel2.SuspendLayout();
            this.splitRemoveRight.SuspendLayout();
            this.tblIgnore.SuspendLayout();
            this.groupAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAddMain)).BeginInit();
            this.splitAddMain.Panel1.SuspendLayout();
            this.splitAddMain.Panel2.SuspendLayout();
            this.splitAddMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAddTop)).BeginInit();
            this.splitAddTop.Panel1.SuspendLayout();
            this.splitAddTop.Panel2.SuspendLayout();
            this.splitAddTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.splitContainer1);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(610, 406);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit Manual AutoCat";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblPrefix);
            this.splitContainer1.Panel1.Controls.Add(this.txtPrefix);
            this.splitContainer1.Panel1.Controls.Add(this.helpPrefix);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitMainBottom);
            this.splitContainer1.Size = new System.Drawing.Size(604, 387);
            this.splitContainer1.SplitterDistance = 29;
            this.splitContainer1.TabIndex = 12;
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
            this.splitMainBottom.Location = new System.Drawing.Point(0, 0);
            this.splitMainBottom.Name = "splitMainBottom";
            // 
            // splitMainBottom.Panel1
            // 
            this.splitMainBottom.Panel1.Controls.Add(this.groupRemove);
            // 
            // splitMainBottom.Panel2
            // 
            this.splitMainBottom.Panel2.Controls.Add(this.groupAdd);
            this.splitMainBottom.Size = new System.Drawing.Size(604, 354);
            this.splitMainBottom.SplitterDistance = 291;
            this.splitMainBottom.TabIndex = 0;
            // 
            // groupRemove
            // 
            this.groupRemove.Controls.Add(this.splitRemoveMain);
            this.groupRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupRemove.Location = new System.Drawing.Point(0, 0);
            this.groupRemove.Name = "groupRemove";
            this.groupRemove.Size = new System.Drawing.Size(291, 354);
            this.groupRemove.TabIndex = 14;
            this.groupRemove.TabStop = false;
            this.groupRemove.Text = "Remove";
            // 
            // splitRemoveMain
            // 
            this.splitRemoveMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRemoveMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitRemoveMain.Location = new System.Drawing.Point(3, 16);
            this.splitRemoveMain.Name = "splitRemoveMain";
            this.splitRemoveMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitRemoveMain.Panel1
            // 
            this.splitRemoveMain.Panel1.Controls.Add(this.splitRemoveTop);
            // 
            // splitRemoveMain.Panel2
            // 
            this.splitRemoveMain.Panel2.Controls.Add(this.tblIgnore);
            this.splitRemoveMain.Size = new System.Drawing.Size(285, 335);
            this.splitRemoveMain.SplitterDistance = 301;
            this.splitRemoveMain.TabIndex = 0;
            // 
            // splitRemoveTop
            // 
            this.splitRemoveTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRemoveTop.Location = new System.Drawing.Point(0, 0);
            this.splitRemoveTop.Name = "splitRemoveTop";
            // 
            // splitRemoveTop.Panel1
            // 
            this.splitRemoveTop.Panel1.Controls.Add(this.clbRemoveSelected);
            // 
            // splitRemoveTop.Panel2
            // 
            this.splitRemoveTop.Panel2.Controls.Add(this.splitRemoveRight);
            this.splitRemoveTop.Size = new System.Drawing.Size(285, 301);
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
            this.clbRemoveSelected.Size = new System.Drawing.Size(95, 301);
            this.clbRemoveSelected.TabIndex = 13;
            this.clbRemoveSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbRemoveSelected_ItemCheck);
            // 
            // splitRemoveRight
            // 
            this.splitRemoveRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRemoveRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitRemoveRight.Location = new System.Drawing.Point(0, 0);
            this.splitRemoveRight.Name = "splitRemoveRight";
            this.splitRemoveRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitRemoveRight.Panel1
            // 
            this.splitRemoveRight.Panel1.Controls.Add(this.chkRemoveAll);
            // 
            // splitRemoveRight.Panel2
            // 
            this.splitRemoveRight.Panel2.Controls.Add(this.lstRemove);
            this.splitRemoveRight.Size = new System.Drawing.Size(186, 301);
            this.splitRemoveRight.SplitterDistance = 25;
            this.splitRemoveRight.TabIndex = 0;
            // 
            // chkRemoveAll
            // 
            this.chkRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRemoveAll.AutoSize = true;
            this.chkRemoveAll.Location = new System.Drawing.Point(104, 3);
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
            this.lstRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRemove.Location = new System.Drawing.Point(0, 0);
            this.lstRemove.Name = "lstRemove";
            this.lstRemove.Size = new System.Drawing.Size(186, 272);
            this.lstRemove.TabIndex = 10;
            this.lstRemove.UseCompatibleStateImageBehavior = false;
            this.lstRemove.View = System.Windows.Forms.View.List;
            this.lstRemove.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstRemove_ItemChecked);
            // 
            // tblIgnore
            // 
            this.tblIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblIgnore.ColumnCount = 2;
            this.tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.Controls.Add(this.btnRemoveUncheckAll, 1, 0);
            this.tblIgnore.Controls.Add(this.btnRemoveCheckAll, 0, 0);
            this.tblIgnore.Location = new System.Drawing.Point(0, 0);
            this.tblIgnore.Name = "tblIgnore";
            this.tblIgnore.RowCount = 1;
            this.tblIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.Size = new System.Drawing.Size(285, 30);
            this.tblIgnore.TabIndex = 11;
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
            this.groupAdd.Controls.Add(this.splitAddMain);
            this.groupAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAdd.Location = new System.Drawing.Point(0, 0);
            this.groupAdd.Name = "groupAdd";
            this.groupAdd.Size = new System.Drawing.Size(309, 354);
            this.groupAdd.TabIndex = 16;
            this.groupAdd.TabStop = false;
            this.groupAdd.Text = "Add";
            // 
            // splitAddMain
            // 
            this.splitAddMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitAddMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitAddMain.Location = new System.Drawing.Point(3, 16);
            this.splitAddMain.Name = "splitAddMain";
            this.splitAddMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitAddMain.Panel1
            // 
            this.splitAddMain.Panel1.Controls.Add(this.splitAddTop);
            // 
            // splitAddMain.Panel2
            // 
            this.splitAddMain.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitAddMain.Size = new System.Drawing.Size(303, 335);
            this.splitAddMain.SplitterDistance = 301;
            this.splitAddMain.TabIndex = 0;
            // 
            // splitAddTop
            // 
            this.splitAddTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitAddTop.Location = new System.Drawing.Point(0, 0);
            this.splitAddTop.Name = "splitAddTop";
            // 
            // splitAddTop.Panel1
            // 
            this.splitAddTop.Panel1.Controls.Add(this.clbAddSelected);
            // 
            // splitAddTop.Panel2
            // 
            this.splitAddTop.Panel2.Controls.Add(this.lstAdd);
            this.splitAddTop.Size = new System.Drawing.Size(303, 301);
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
            this.clbAddSelected.Size = new System.Drawing.Size(101, 301);
            this.clbAddSelected.TabIndex = 15;
            this.clbAddSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbAddSelected_ItemCheck);
            // 
            // lstAdd
            // 
            this.lstAdd.CheckBoxes = true;
            this.lstAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAdd.Location = new System.Drawing.Point(0, 0);
            this.lstAdd.Name = "lstAdd";
            this.lstAdd.Size = new System.Drawing.Size(198, 301);
            this.lstAdd.TabIndex = 13;
            this.lstAdd.UseCompatibleStateImageBehavior = false;
            this.lstAdd.View = System.Windows.Forms.View.List;
            this.lstAdd.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstAdd_ItemChecked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddCheckAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddUncheckAll, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(303, 30);
            this.tableLayoutPanel1.TabIndex = 14;
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
            // AutoCatConfigPanel_Manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Manual";
            this.Size = new System.Drawing.Size(610, 406);
            this.grpMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitMainBottom.Panel1.ResumeLayout(false);
            this.splitMainBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMainBottom)).EndInit();
            this.splitMainBottom.ResumeLayout(false);
            this.groupRemove.ResumeLayout(false);
            this.splitRemoveMain.Panel1.ResumeLayout(false);
            this.splitRemoveMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRemoveMain)).EndInit();
            this.splitRemoveMain.ResumeLayout(false);
            this.splitRemoveTop.Panel1.ResumeLayout(false);
            this.splitRemoveTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRemoveTop)).EndInit();
            this.splitRemoveTop.ResumeLayout(false);
            this.splitRemoveRight.Panel1.ResumeLayout(false);
            this.splitRemoveRight.Panel1.PerformLayout();
            this.splitRemoveRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRemoveRight)).EndInit();
            this.splitRemoveRight.ResumeLayout(false);
            this.tblIgnore.ResumeLayout(false);
            this.groupAdd.ResumeLayout(false);
            this.splitAddMain.Panel1.ResumeLayout(false);
            this.splitAddMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitAddMain)).EndInit();
            this.splitAddMain.ResumeLayout(false);
            this.splitAddTop.Panel1.ResumeLayout(false);
            this.splitAddTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitAddTop)).EndInit();
            this.splitAddTop.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TableLayoutPanel tblIgnore;
        private System.Windows.Forms.Button btnRemoveUncheckAll;
        private System.Windows.Forms.Button btnRemoveCheckAll;
        private System.Windows.Forms.ListView lstRemove;
        private System.Windows.Forms.CheckBox chkRemoveAll;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitMainBottom;
        private System.Windows.Forms.ListView lstAdd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAddUncheckAll;
        private System.Windows.Forms.Button btnAddCheckAll;
        private System.Windows.Forms.CheckedListBox clbRemoveSelected;
        private System.Windows.Forms.CheckedListBox clbAddSelected;
        private System.Windows.Forms.GroupBox groupRemove;
        private System.Windows.Forms.SplitContainer splitRemoveMain;
        private System.Windows.Forms.SplitContainer splitRemoveTop;
        private System.Windows.Forms.SplitContainer splitRemoveRight;
        private System.Windows.Forms.GroupBox groupAdd;
        private System.Windows.Forms.SplitContainer splitAddMain;
        private System.Windows.Forms.SplitContainer splitAddTop;
    }
}
