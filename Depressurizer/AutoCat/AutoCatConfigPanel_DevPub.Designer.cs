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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.splitMainBottom = new System.Windows.Forms.SplitContainer();
            this.groupDevelopers = new System.Windows.Forms.GroupBox();
            this.splitRemoveMain = new System.Windows.Forms.SplitContainer();
            this.splitRemoveTop = new System.Windows.Forms.SplitContainer();
            this.clbDevelopersSelected = new System.Windows.Forms.CheckedListBox();
            this.splitRemoveRight = new System.Windows.Forms.SplitContainer();
            this.chkAllDevelopers = new System.Windows.Forms.CheckBox();
            this.lstDevelopers = new System.Windows.Forms.ListView();
            this.tblIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.btnDevUncheckAll = new System.Windows.Forms.Button();
            this.btnDevCheckAll = new System.Windows.Forms.Button();
            this.groupPublishers = new System.Windows.Forms.GroupBox();
            this.splitAddMain = new System.Windows.Forms.SplitContainer();
            this.splitAddTop = new System.Windows.Forms.SplitContainer();
            this.clbPublishersSelected = new System.Windows.Forms.CheckedListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chkAllPublishers = new System.Windows.Forms.CheckBox();
            this.lstPublishers = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPubCheckAll = new System.Windows.Forms.Button();
            this.btnPubUncheckAll = new System.Windows.Forms.Button();
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
            this.groupDevelopers.SuspendLayout();
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
            this.groupPublishers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAddMain)).BeginInit();
            this.splitAddMain.Panel1.SuspendLayout();
            this.splitAddMain.Panel2.SuspendLayout();
            this.splitAddMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAddTop)).BeginInit();
            this.splitAddTop.Panel1.SuspendLayout();
            this.splitAddTop.Panel2.SuspendLayout();
            this.splitAddTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.grpMain.Text = "Edit DevPub AutoCat";
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
            this.splitMainBottom.Panel1.Controls.Add(this.groupDevelopers);
            // 
            // splitMainBottom.Panel2
            // 
            this.splitMainBottom.Panel2.Controls.Add(this.groupPublishers);
            this.splitMainBottom.Size = new System.Drawing.Size(604, 354);
            this.splitMainBottom.SplitterDistance = 291;
            this.splitMainBottom.TabIndex = 0;
            // 
            // groupDevelopers
            // 
            this.groupDevelopers.Controls.Add(this.splitRemoveMain);
            this.groupDevelopers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDevelopers.Location = new System.Drawing.Point(0, 0);
            this.groupDevelopers.Name = "groupDevelopers";
            this.groupDevelopers.Size = new System.Drawing.Size(291, 354);
            this.groupDevelopers.TabIndex = 14;
            this.groupDevelopers.TabStop = false;
            this.groupDevelopers.Text = "Developers";
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
            this.splitRemoveTop.Panel1.Controls.Add(this.clbDevelopersSelected);
            // 
            // splitRemoveTop.Panel2
            // 
            this.splitRemoveTop.Panel2.Controls.Add(this.splitRemoveRight);
            this.splitRemoveTop.Size = new System.Drawing.Size(285, 301);
            this.splitRemoveTop.SplitterDistance = 95;
            this.splitRemoveTop.TabIndex = 0;
            // 
            // clbDevelopersSelected
            // 
            this.clbDevelopersSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbDevelopersSelected.FormattingEnabled = true;
            this.clbDevelopersSelected.Location = new System.Drawing.Point(0, 0);
            this.clbDevelopersSelected.MultiColumn = true;
            this.clbDevelopersSelected.Name = "clbDevelopersSelected";
            this.clbDevelopersSelected.Size = new System.Drawing.Size(95, 301);
            this.clbDevelopersSelected.TabIndex = 13;
            this.clbDevelopersSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDevelopersSelected_ItemCheck);
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
            this.splitRemoveRight.Panel1.Controls.Add(this.chkAllDevelopers);
            // 
            // splitRemoveRight.Panel2
            // 
            this.splitRemoveRight.Panel2.Controls.Add(this.lstDevelopers);
            this.splitRemoveRight.Size = new System.Drawing.Size(186, 301);
            this.splitRemoveRight.SplitterDistance = 25;
            this.splitRemoveRight.TabIndex = 0;
            // 
            // chkAllDevelopers
            // 
            this.chkAllDevelopers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllDevelopers.AutoSize = true;
            this.chkAllDevelopers.Location = new System.Drawing.Point(89, 3);
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
            this.lstDevelopers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDevelopers.Location = new System.Drawing.Point(0, 0);
            this.lstDevelopers.Name = "lstDevelopers";
            this.lstDevelopers.Size = new System.Drawing.Size(186, 272);
            this.lstDevelopers.TabIndex = 10;
            this.lstDevelopers.UseCompatibleStateImageBehavior = false;
            this.lstDevelopers.View = System.Windows.Forms.View.List;
            this.lstDevelopers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstDevelopers_ItemChecked);
            // 
            // tblIgnore
            // 
            this.tblIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblIgnore.ColumnCount = 2;
            this.tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.Controls.Add(this.btnDevUncheckAll, 1, 0);
            this.tblIgnore.Controls.Add(this.btnDevCheckAll, 0, 0);
            this.tblIgnore.Location = new System.Drawing.Point(0, 0);
            this.tblIgnore.Name = "tblIgnore";
            this.tblIgnore.RowCount = 1;
            this.tblIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.Size = new System.Drawing.Size(285, 30);
            this.tblIgnore.TabIndex = 11;
            // 
            // btnDevUncheckAll
            // 
            this.btnDevUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDevUncheckAll.Location = new System.Drawing.Point(145, 3);
            this.btnDevUncheckAll.Name = "btnDevUncheckAll";
            this.btnDevUncheckAll.Size = new System.Drawing.Size(137, 23);
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
            this.btnDevCheckAll.Size = new System.Drawing.Size(136, 23);
            this.btnDevCheckAll.TabIndex = 0;
            this.btnDevCheckAll.Text = "Check All";
            this.btnDevCheckAll.UseVisualStyleBackColor = true;
            this.btnDevCheckAll.Click += new System.EventHandler(this.btnDevCheckAll_Click);
            // 
            // groupPublishers
            // 
            this.groupPublishers.Controls.Add(this.splitAddMain);
            this.groupPublishers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPublishers.Location = new System.Drawing.Point(0, 0);
            this.groupPublishers.Name = "groupPublishers";
            this.groupPublishers.Size = new System.Drawing.Size(309, 354);
            this.groupPublishers.TabIndex = 16;
            this.groupPublishers.TabStop = false;
            this.groupPublishers.Text = "Publishers";
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
            this.splitAddTop.Panel1.Controls.Add(this.clbPublishersSelected);
            // 
            // splitAddTop.Panel2
            // 
            this.splitAddTop.Panel2.Controls.Add(this.splitContainer2);
            this.splitAddTop.Size = new System.Drawing.Size(303, 301);
            this.splitAddTop.SplitterDistance = 101;
            this.splitAddTop.TabIndex = 0;
            // 
            // clbPublishersSelected
            // 
            this.clbPublishersSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbPublishersSelected.FormattingEnabled = true;
            this.clbPublishersSelected.Location = new System.Drawing.Point(0, 0);
            this.clbPublishersSelected.MultiColumn = true;
            this.clbPublishersSelected.Name = "clbPublishersSelected";
            this.clbPublishersSelected.Size = new System.Drawing.Size(101, 301);
            this.clbPublishersSelected.TabIndex = 15;
            this.clbPublishersSelected.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPublishersSelected_ItemCheck);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chkAllPublishers);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lstPublishers);
            this.splitContainer2.Size = new System.Drawing.Size(198, 301);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 1;
            // 
            // chkAllPublishers
            // 
            this.chkAllPublishers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllPublishers.AutoSize = true;
            this.chkAllPublishers.Location = new System.Drawing.Point(107, 3);
            this.chkAllPublishers.Name = "chkAllPublishers";
            this.chkAllPublishers.Size = new System.Drawing.Size(88, 17);
            this.chkAllPublishers.TabIndex = 5;
            this.chkAllPublishers.Text = "All Publishers";
            this.chkAllPublishers.UseVisualStyleBackColor = true;
            this.chkAllPublishers.CheckedChanged += new System.EventHandler(this.chkAllPublishers_CheckedChanged);
            // 
            // lstPublishers
            // 
            this.lstPublishers.CheckBoxes = true;
            this.lstPublishers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPublishers.Location = new System.Drawing.Point(0, 0);
            this.lstPublishers.Name = "lstPublishers";
            this.lstPublishers.Size = new System.Drawing.Size(198, 272);
            this.lstPublishers.TabIndex = 10;
            this.lstPublishers.UseCompatibleStateImageBehavior = false;
            this.lstPublishers.View = System.Windows.Forms.View.List;
            this.lstPublishers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstPublishers_ItemChecked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnPubCheckAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPubUncheckAll, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(303, 30);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // btnPubCheckAll
            // 
            this.btnPubCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPubCheckAll.Location = new System.Drawing.Point(3, 3);
            this.btnPubCheckAll.Name = "btnPubCheckAll";
            this.btnPubCheckAll.Size = new System.Drawing.Size(145, 23);
            this.btnPubCheckAll.TabIndex = 0;
            this.btnPubCheckAll.Text = "Check All";
            this.btnPubCheckAll.UseVisualStyleBackColor = true;
            this.btnPubCheckAll.Click += new System.EventHandler(this.btnPubCheckAll_Click);
            // 
            // btnPubUncheckAll
            // 
            this.btnPubUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPubUncheckAll.Location = new System.Drawing.Point(154, 3);
            this.btnPubUncheckAll.Name = "btnPubUncheckAll";
            this.btnPubUncheckAll.Size = new System.Drawing.Size(146, 23);
            this.btnPubUncheckAll.TabIndex = 1;
            this.btnPubUncheckAll.Text = "Uncheck All";
            this.btnPubUncheckAll.UseVisualStyleBackColor = true;
            this.btnPubUncheckAll.Click += new System.EventHandler(this.btnPubUncheckAll_Click);
            // 
            // AutoCatConfigPanel_DevPub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_DevPub";
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
            this.groupDevelopers.ResumeLayout(false);
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
            this.groupPublishers.ResumeLayout(false);
            this.splitAddMain.Panel1.ResumeLayout(false);
            this.splitAddMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitAddMain)).EndInit();
            this.splitAddMain.ResumeLayout(false);
            this.splitAddTop.Panel1.ResumeLayout(false);
            this.splitAddTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitAddTop)).EndInit();
            this.splitAddTop.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitMainBottom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnPubUncheckAll;
        private System.Windows.Forms.Button btnPubCheckAll;
        private System.Windows.Forms.CheckedListBox clbDevelopersSelected;
        private System.Windows.Forms.CheckedListBox clbPublishersSelected;
        private System.Windows.Forms.GroupBox groupDevelopers;
        private System.Windows.Forms.SplitContainer splitRemoveMain;
        private System.Windows.Forms.SplitContainer splitRemoveTop;
        private System.Windows.Forms.SplitContainer splitRemoveRight;
        private System.Windows.Forms.GroupBox groupPublishers;
        private System.Windows.Forms.SplitContainer splitAddMain;
        private System.Windows.Forms.SplitContainer splitAddTop;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox chkAllPublishers;
        private System.Windows.Forms.ListView lstPublishers;
    }
}
