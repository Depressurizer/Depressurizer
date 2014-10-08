namespace Depressurizer {
    partial class DlgAutoCat {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lstAutoCats = new System.Windows.Forms.ListBox();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdRename = new System.Windows.Forms.Button();
            this.cmdCreate = new System.Windows.Forms.Button();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.panGenre = new System.Windows.Forms.Panel();
            this.genre_grpMain = new System.Windows.Forms.GroupBox();
            this.genre_helpRemoveExisting = new System.Windows.Forms.Label();
            this.genre_helpPrefix = new System.Windows.Forms.Label();
            this.genre_lblPrefix = new System.Windows.Forms.Label();
            this.genre_txtPrefix = new System.Windows.Forms.TextBox();
            this.genre_tblIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.genre_cmdUncheckAll = new System.Windows.Forms.Button();
            this.genre_cmdCheckAll = new System.Windows.Forms.Button();
            this.genre_lstIgnore = new System.Windows.Forms.ListView();
            this.genre_lblIgnore = new System.Windows.Forms.Label();
            this.genre_chkRemoveExisting = new System.Windows.Forms.CheckBox();
            this.genre_lblMaxCats = new System.Windows.Forms.Label();
            this.genre_numMaxCats = new System.Windows.Forms.NumericUpDown();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.panFlags = new System.Windows.Forms.Panel();
            this.flags_grpMain = new System.Windows.Forms.GroupBox();
            this.flags_helpPrefix = new System.Windows.Forms.Label();
            this.flags_tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.flags_cmdCheckAll = new System.Windows.Forms.Button();
            this.flags_cmdUncheckAll = new System.Windows.Forms.Button();
            this.flags_lblInclude = new System.Windows.Forms.Label();
            this.flags_lstIncluded = new System.Windows.Forms.ListView();
            this.flags_txtPrefix = new System.Windows.Forms.TextBox();
            this.flags_lblPrefix = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panTags = new System.Windows.Forms.Panel();
            this.tags_grpMain = new System.Windows.Forms.GroupBox();
            this.tags_helpPrefix = new System.Windows.Forms.Label();
            this.tags_grpListOpts = new System.Windows.Forms.GroupBox();
            this.tags_list_helpScoreSort = new System.Windows.Forms.Label();
            this.tags_list_helpExcludeGenres = new System.Windows.Forms.Label();
            this.tags_list_chkScoreSort = new System.Windows.Forms.CheckBox();
            this.tags_list_chkExcludeGenres = new System.Windows.Forms.CheckBox();
            this.tags_list_lblExplain = new System.Windows.Forms.Label();
            this.tags_list_lblWeightFactor = new System.Windows.Forms.Label();
            this.tags_list_numWeightFactor = new System.Windows.Forms.NumericUpDown();
            this.tags_list_helpOwnedOnly = new System.Windows.Forms.Label();
            this.tags_list_helpWeightFactor = new System.Windows.Forms.Label();
            this.tags_list_helpTagsPerGame = new System.Windows.Forms.Label();
            this.tags_list_helpMinScore = new System.Windows.Forms.Label();
            this.tags_list_lblTagsPerGame = new System.Windows.Forms.Label();
            this.tags_list_numTagsPerGame = new System.Windows.Forms.NumericUpDown();
            this.tags_list_chkOwnedOnly = new System.Windows.Forms.CheckBox();
            this.tags_list_numMinScore = new System.Windows.Forms.NumericUpDown();
            this.tags_list_lblMinScore = new System.Windows.Forms.Label();
            this.tags_lblIncluded = new System.Windows.Forms.Label();
            this.tags_cmdListRebuild = new System.Windows.Forms.Button();
            this.tags_cmdCheckAll = new System.Windows.Forms.Button();
            this.tags_cmdUncheckAll = new System.Windows.Forms.Button();
            this.tags_numMaxTags = new System.Windows.Forms.NumericUpDown();
            this.tags_lblMaxTags = new System.Windows.Forms.Label();
            this.tags_lblPrefix = new System.Windows.Forms.Label();
            this.tags_txtPrefix = new System.Windows.Forms.TextBox();
            this.tags_lstIncluded = new System.Windows.Forms.ListView();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpList.SuspendLayout();
            this.panGenre.SuspendLayout();
            this.genre_grpMain.SuspendLayout();
            this.genre_tblIgnore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.genre_numMaxCats)).BeginInit();
            this.panFlags.SuspendLayout();
            this.flags_grpMain.SuspendLayout();
            this.flags_tblButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panTags.SuspendLayout();
            this.tags_grpMain.SuspendLayout();
            this.tags_grpListOpts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numWeightFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numTagsPerGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numMinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_numMaxTags)).BeginInit();
            this.SuspendLayout();
            // 
            // lstAutoCats
            // 
            this.lstAutoCats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAutoCats.FormattingEnabled = true;
            this.lstAutoCats.HorizontalScrollbar = true;
            this.lstAutoCats.IntegralHeight = false;
            this.lstAutoCats.Location = new System.Drawing.Point(6, 19);
            this.lstAutoCats.Name = "lstAutoCats";
            this.lstAutoCats.Size = new System.Drawing.Size(171, 223);
            this.lstAutoCats.TabIndex = 0;
            this.lstAutoCats.SelectedIndexChanged += new System.EventHandler(this.lstAutoCats_SelectedIndexChanged);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Location = new System.Drawing.Point(6, 299);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(171, 23);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdRename
            // 
            this.cmdRename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRename.Location = new System.Drawing.Point(6, 273);
            this.cmdRename.Name = "cmdRename";
            this.cmdRename.Size = new System.Drawing.Size(171, 23);
            this.cmdRename.TabIndex = 2;
            this.cmdRename.Text = "Rename";
            this.cmdRename.UseVisualStyleBackColor = true;
            this.cmdRename.Click += new System.EventHandler(this.cmdRename_Click);
            // 
            // cmdCreate
            // 
            this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreate.Location = new System.Drawing.Point(6, 247);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(171, 23);
            this.cmdCreate.TabIndex = 1;
            this.cmdCreate.Text = "Create";
            this.cmdCreate.UseVisualStyleBackColor = true;
            this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.cmdCreate);
            this.grpList.Controls.Add(this.lstAutoCats);
            this.grpList.Controls.Add(this.cmdRename);
            this.grpList.Controls.Add(this.cmdDelete);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Location = new System.Drawing.Point(0, 0);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(184, 329);
            this.grpList.TabIndex = 0;
            this.grpList.TabStop = false;
            this.grpList.Text = "AutoCat List";
            // 
            // panGenre
            // 
            this.panGenre.Controls.Add(this.genre_grpMain);
            this.panGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panGenre.Location = new System.Drawing.Point(0, 0);
            this.panGenre.Name = "panGenre";
            this.panGenre.Size = new System.Drawing.Size(402, 329);
            this.panGenre.TabIndex = 1;
            // 
            // genre_grpMain
            // 
            this.genre_grpMain.Controls.Add(this.genre_helpRemoveExisting);
            this.genre_grpMain.Controls.Add(this.genre_helpPrefix);
            this.genre_grpMain.Controls.Add(this.genre_lblPrefix);
            this.genre_grpMain.Controls.Add(this.genre_txtPrefix);
            this.genre_grpMain.Controls.Add(this.genre_tblIgnore);
            this.genre_grpMain.Controls.Add(this.genre_lstIgnore);
            this.genre_grpMain.Controls.Add(this.genre_lblIgnore);
            this.genre_grpMain.Controls.Add(this.genre_chkRemoveExisting);
            this.genre_grpMain.Controls.Add(this.genre_lblMaxCats);
            this.genre_grpMain.Controls.Add(this.genre_numMaxCats);
            this.genre_grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.genre_grpMain.Location = new System.Drawing.Point(0, 0);
            this.genre_grpMain.Name = "genre_grpMain";
            this.genre_grpMain.Size = new System.Drawing.Size(402, 329);
            this.genre_grpMain.TabIndex = 0;
            this.genre_grpMain.TabStop = false;
            this.genre_grpMain.Text = "Edit Genre AutoCat";
            // 
            // genre_helpRemoveExisting
            // 
            this.genre_helpRemoveExisting.AutoSize = true;
            this.genre_helpRemoveExisting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.genre_helpRemoveExisting.Location = new System.Drawing.Point(238, 82);
            this.genre_helpRemoveExisting.Name = "genre_helpRemoveExisting";
            this.genre_helpRemoveExisting.Size = new System.Drawing.Size(15, 15);
            this.genre_helpRemoveExisting.TabIndex = 1;
            this.genre_helpRemoveExisting.Text = "?";
            // 
            // genre_helpPrefix
            // 
            this.genre_helpPrefix.AutoSize = true;
            this.genre_helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.genre_helpPrefix.Location = new System.Drawing.Point(238, 22);
            this.genre_helpPrefix.Name = "genre_helpPrefix";
            this.genre_helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.genre_helpPrefix.TabIndex = 11;
            this.genre_helpPrefix.Text = "?";
            // 
            // genre_lblPrefix
            // 
            this.genre_lblPrefix.AutoSize = true;
            this.genre_lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.genre_lblPrefix.Name = "genre_lblPrefix";
            this.genre_lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.genre_lblPrefix.TabIndex = 0;
            this.genre_lblPrefix.Text = "Prefix:";
            // 
            // genre_txtPrefix
            // 
            this.genre_txtPrefix.Location = new System.Drawing.Point(67, 19);
            this.genre_txtPrefix.Name = "genre_txtPrefix";
            this.genre_txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.genre_txtPrefix.TabIndex = 1;
            // 
            // genre_tblIgnore
            // 
            this.genre_tblIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genre_tblIgnore.ColumnCount = 2;
            this.genre_tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genre_tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genre_tblIgnore.Controls.Add(this.genre_cmdUncheckAll, 1, 0);
            this.genre_tblIgnore.Controls.Add(this.genre_cmdCheckAll, 0, 0);
            this.genre_tblIgnore.Location = new System.Drawing.Point(6, 295);
            this.genre_tblIgnore.Name = "genre_tblIgnore";
            this.genre_tblIgnore.RowCount = 1;
            this.genre_tblIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genre_tblIgnore.Size = new System.Drawing.Size(394, 30);
            this.genre_tblIgnore.TabIndex = 7;
            // 
            // genre_cmdUncheckAll
            // 
            this.genre_cmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genre_cmdUncheckAll.Location = new System.Drawing.Point(200, 3);
            this.genre_cmdUncheckAll.Name = "genre_cmdUncheckAll";
            this.genre_cmdUncheckAll.Size = new System.Drawing.Size(191, 23);
            this.genre_cmdUncheckAll.TabIndex = 7;
            this.genre_cmdUncheckAll.Text = "Uncheck All";
            this.genre_cmdUncheckAll.UseVisualStyleBackColor = true;
            this.genre_cmdUncheckAll.Click += new System.EventHandler(this.genreCmdUncheckAll_Click);
            // 
            // genre_cmdCheckAll
            // 
            this.genre_cmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genre_cmdCheckAll.Location = new System.Drawing.Point(3, 3);
            this.genre_cmdCheckAll.Name = "genre_cmdCheckAll";
            this.genre_cmdCheckAll.Size = new System.Drawing.Size(191, 23);
            this.genre_cmdCheckAll.TabIndex = 6;
            this.genre_cmdCheckAll.Text = "Check All";
            this.genre_cmdCheckAll.UseVisualStyleBackColor = true;
            this.genre_cmdCheckAll.Click += new System.EventHandler(this.genreCmdCheckAll_Click);
            // 
            // genre_lstIgnore
            // 
            this.genre_lstIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genre_lstIgnore.CheckBoxes = true;
            this.genre_lstIgnore.Location = new System.Drawing.Point(9, 130);
            this.genre_lstIgnore.Name = "genre_lstIgnore";
            this.genre_lstIgnore.Size = new System.Drawing.Size(389, 166);
            this.genre_lstIgnore.TabIndex = 3;
            this.genre_lstIgnore.UseCompatibleStateImageBehavior = false;
            this.genre_lstIgnore.View = System.Windows.Forms.View.List;
            // 
            // genre_lblIgnore
            // 
            this.genre_lblIgnore.AutoSize = true;
            this.genre_lblIgnore.Location = new System.Drawing.Point(6, 114);
            this.genre_lblIgnore.Name = "genre_lblIgnore";
            this.genre_lblIgnore.Size = new System.Drawing.Size(104, 13);
            this.genre_lblIgnore.TabIndex = 2;
            this.genre_lblIgnore.Text = "Categories to ignore:";
            // 
            // genre_chkRemoveExisting
            // 
            this.genre_chkRemoveExisting.AutoSize = true;
            this.genre_chkRemoveExisting.Location = new System.Drawing.Point(46, 82);
            this.genre_chkRemoveExisting.Name = "genre_chkRemoveExisting";
            this.genre_chkRemoveExisting.Size = new System.Drawing.Size(186, 17);
            this.genre_chkRemoveExisting.TabIndex = 0;
            this.genre_chkRemoveExisting.Text = "Remove existing genre categories";
            this.genre_chkRemoveExisting.UseVisualStyleBackColor = true;
            // 
            // genre_lblMaxCats
            // 
            this.genre_lblMaxCats.AutoSize = true;
            this.genre_lblMaxCats.Location = new System.Drawing.Point(64, 47);
            this.genre_lblMaxCats.Name = "genre_lblMaxCats";
            this.genre_lblMaxCats.Size = new System.Drawing.Size(148, 26);
            this.genre_lblMaxCats.TabIndex = 3;
            this.genre_lblMaxCats.Text = "Maximum categories to assign\r\n(0 for unlimited)";
            // 
            // genre_numMaxCats
            // 
            this.genre_numMaxCats.Location = new System.Drawing.Point(6, 50);
            this.genre_numMaxCats.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.genre_numMaxCats.Name = "genre_numMaxCats";
            this.genre_numMaxCats.Size = new System.Drawing.Size(52, 20);
            this.genre_numMaxCats.TabIndex = 2;
            this.genre_numMaxCats.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.genre_numMaxCats.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdSave.Location = new System.Drawing.Point(448, 337);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(141, 23);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Save All Changes";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(352, 337);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(90, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // panFlags
            // 
            this.panFlags.Controls.Add(this.flags_grpMain);
            this.panFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panFlags.Location = new System.Drawing.Point(0, 0);
            this.panFlags.Name = "panFlags";
            this.panFlags.Size = new System.Drawing.Size(402, 329);
            this.panFlags.TabIndex = 1;
            // 
            // flags_grpMain
            // 
            this.flags_grpMain.Controls.Add(this.flags_helpPrefix);
            this.flags_grpMain.Controls.Add(this.flags_tblButtons);
            this.flags_grpMain.Controls.Add(this.flags_lblInclude);
            this.flags_grpMain.Controls.Add(this.flags_lstIncluded);
            this.flags_grpMain.Controls.Add(this.flags_txtPrefix);
            this.flags_grpMain.Controls.Add(this.flags_lblPrefix);
            this.flags_grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flags_grpMain.Location = new System.Drawing.Point(0, 0);
            this.flags_grpMain.Name = "flags_grpMain";
            this.flags_grpMain.Size = new System.Drawing.Size(402, 329);
            this.flags_grpMain.TabIndex = 0;
            this.flags_grpMain.TabStop = false;
            this.flags_grpMain.Text = "Edit Flag Autocat";
            // 
            // flags_helpPrefix
            // 
            this.flags_helpPrefix.AutoSize = true;
            this.flags_helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flags_helpPrefix.Location = new System.Drawing.Point(238, 22);
            this.flags_helpPrefix.Name = "flags_helpPrefix";
            this.flags_helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.flags_helpPrefix.TabIndex = 6;
            this.flags_helpPrefix.Text = "?";
            // 
            // flags_tblButtons
            // 
            this.flags_tblButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flags_tblButtons.ColumnCount = 2;
            this.flags_tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.flags_tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.flags_tblButtons.Controls.Add(this.flags_cmdCheckAll, 0, 0);
            this.flags_tblButtons.Controls.Add(this.flags_cmdUncheckAll, 1, 0);
            this.flags_tblButtons.Location = new System.Drawing.Point(3, 295);
            this.flags_tblButtons.Name = "flags_tblButtons";
            this.flags_tblButtons.RowCount = 1;
            this.flags_tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.flags_tblButtons.Size = new System.Drawing.Size(396, 30);
            this.flags_tblButtons.TabIndex = 4;
            // 
            // flags_cmdCheckAll
            // 
            this.flags_cmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flags_cmdCheckAll.Location = new System.Drawing.Point(3, 3);
            this.flags_cmdCheckAll.Name = "flags_cmdCheckAll";
            this.flags_cmdCheckAll.Size = new System.Drawing.Size(192, 23);
            this.flags_cmdCheckAll.TabIndex = 0;
            this.flags_cmdCheckAll.Text = "Check All";
            this.flags_cmdCheckAll.UseVisualStyleBackColor = true;
            this.flags_cmdCheckAll.Click += new System.EventHandler(this.flagsCmdCheckAll_Click);
            // 
            // flags_cmdUncheckAll
            // 
            this.flags_cmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flags_cmdUncheckAll.Location = new System.Drawing.Point(201, 3);
            this.flags_cmdUncheckAll.Name = "flags_cmdUncheckAll";
            this.flags_cmdUncheckAll.Size = new System.Drawing.Size(192, 23);
            this.flags_cmdUncheckAll.TabIndex = 1;
            this.flags_cmdUncheckAll.Text = "Uncheck All";
            this.flags_cmdUncheckAll.UseVisualStyleBackColor = true;
            this.flags_cmdUncheckAll.Click += new System.EventHandler(this.flagsCmdUncheckAll_Click);
            // 
            // flags_lblInclude
            // 
            this.flags_lblInclude.AutoSize = true;
            this.flags_lblInclude.Location = new System.Drawing.Point(3, 69);
            this.flags_lblInclude.Name = "flags_lblInclude";
            this.flags_lblInclude.Size = new System.Drawing.Size(79, 13);
            this.flags_lblInclude.TabIndex = 0;
            this.flags_lblInclude.Text = "Included Flags:";
            // 
            // flags_lstIncluded
            // 
            this.flags_lstIncluded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flags_lstIncluded.CheckBoxes = true;
            this.flags_lstIncluded.Location = new System.Drawing.Point(6, 85);
            this.flags_lstIncluded.Name = "flags_lstIncluded";
            this.flags_lstIncluded.Size = new System.Drawing.Size(390, 208);
            this.flags_lstIncluded.TabIndex = 3;
            this.flags_lstIncluded.UseCompatibleStateImageBehavior = false;
            this.flags_lstIncluded.View = System.Windows.Forms.View.List;
            // 
            // flags_txtPrefix
            // 
            this.flags_txtPrefix.Location = new System.Drawing.Point(67, 19);
            this.flags_txtPrefix.Name = "flags_txtPrefix";
            this.flags_txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.flags_txtPrefix.TabIndex = 1;
            // 
            // flags_lblPrefix
            // 
            this.flags_lblPrefix.AutoSize = true;
            this.flags_lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.flags_lblPrefix.Name = "flags_lblPrefix";
            this.flags_lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.flags_lblPrefix.TabIndex = 0;
            this.flags_lblPrefix.Text = "Prefix:";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(4, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpList);
            this.splitContainer.Panel1MinSize = 125;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panTags);
            this.splitContainer.Panel2.Controls.Add(this.panGenre);
            this.splitContainer.Panel2.Controls.Add(this.panFlags);
            this.splitContainer.Panel2MinSize = 400;
            this.splitContainer.Size = new System.Drawing.Size(590, 329);
            this.splitContainer.SplitterDistance = 184;
            this.splitContainer.TabIndex = 7;
            // 
            // panTags
            // 
            this.panTags.Controls.Add(this.tags_grpMain);
            this.panTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panTags.Location = new System.Drawing.Point(0, 0);
            this.panTags.Name = "panTags";
            this.panTags.Size = new System.Drawing.Size(402, 329);
            this.panTags.TabIndex = 2;
            // 
            // tags_grpMain
            // 
            this.tags_grpMain.Controls.Add(this.tags_helpPrefix);
            this.tags_grpMain.Controls.Add(this.tags_grpListOpts);
            this.tags_grpMain.Controls.Add(this.tags_lblIncluded);
            this.tags_grpMain.Controls.Add(this.tags_cmdListRebuild);
            this.tags_grpMain.Controls.Add(this.tags_cmdCheckAll);
            this.tags_grpMain.Controls.Add(this.tags_cmdUncheckAll);
            this.tags_grpMain.Controls.Add(this.tags_numMaxTags);
            this.tags_grpMain.Controls.Add(this.tags_lblMaxTags);
            this.tags_grpMain.Controls.Add(this.tags_lblPrefix);
            this.tags_grpMain.Controls.Add(this.tags_txtPrefix);
            this.tags_grpMain.Controls.Add(this.tags_lstIncluded);
            this.tags_grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tags_grpMain.Location = new System.Drawing.Point(0, 0);
            this.tags_grpMain.Name = "tags_grpMain";
            this.tags_grpMain.Size = new System.Drawing.Size(402, 329);
            this.tags_grpMain.TabIndex = 0;
            this.tags_grpMain.TabStop = false;
            this.tags_grpMain.Text = "Edit Tag Autocat";
            // 
            // tags_helpPrefix
            // 
            this.tags_helpPrefix.AutoSize = true;
            this.tags_helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_helpPrefix.Location = new System.Drawing.Point(238, 22);
            this.tags_helpPrefix.Name = "tags_helpPrefix";
            this.tags_helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.tags_helpPrefix.TabIndex = 2;
            this.tags_helpPrefix.Text = "?";
            // 
            // tags_grpListOpts
            // 
            this.tags_grpListOpts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tags_grpListOpts.Controls.Add(this.tags_list_helpScoreSort);
            this.tags_grpListOpts.Controls.Add(this.tags_list_helpExcludeGenres);
            this.tags_grpListOpts.Controls.Add(this.tags_list_chkScoreSort);
            this.tags_grpListOpts.Controls.Add(this.tags_list_chkExcludeGenres);
            this.tags_grpListOpts.Controls.Add(this.tags_list_lblExplain);
            this.tags_grpListOpts.Controls.Add(this.tags_list_lblWeightFactor);
            this.tags_grpListOpts.Controls.Add(this.tags_list_numWeightFactor);
            this.tags_grpListOpts.Controls.Add(this.tags_list_helpOwnedOnly);
            this.tags_grpListOpts.Controls.Add(this.tags_list_helpWeightFactor);
            this.tags_grpListOpts.Controls.Add(this.tags_list_helpTagsPerGame);
            this.tags_grpListOpts.Controls.Add(this.tags_list_helpMinScore);
            this.tags_grpListOpts.Controls.Add(this.tags_list_lblTagsPerGame);
            this.tags_grpListOpts.Controls.Add(this.tags_list_numTagsPerGame);
            this.tags_grpListOpts.Controls.Add(this.tags_list_chkOwnedOnly);
            this.tags_grpListOpts.Controls.Add(this.tags_list_numMinScore);
            this.tags_grpListOpts.Controls.Add(this.tags_list_lblMinScore);
            this.tags_grpListOpts.Location = new System.Drawing.Point(6, 203);
            this.tags_grpListOpts.Name = "tags_grpListOpts";
            this.tags_grpListOpts.Size = new System.Drawing.Size(389, 119);
            this.tags_grpListOpts.TabIndex = 10;
            this.tags_grpListOpts.TabStop = false;
            this.tags_grpListOpts.Text = "Tag List Options";
            // 
            // tags_list_helpScoreSort
            // 
            this.tags_list_helpScoreSort.AutoSize = true;
            this.tags_list_helpScoreSort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpScoreSort.Location = new System.Drawing.Point(283, 73);
            this.tags_list_helpScoreSort.Name = "tags_list_helpScoreSort";
            this.tags_list_helpScoreSort.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpScoreSort.TabIndex = 11;
            this.tags_list_helpScoreSort.Text = "?";
            // 
            // tags_list_helpExcludeGenres
            // 
            this.tags_list_helpExcludeGenres.AutoSize = true;
            this.tags_list_helpExcludeGenres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpExcludeGenres.Location = new System.Drawing.Point(113, 98);
            this.tags_list_helpExcludeGenres.Name = "tags_list_helpExcludeGenres";
            this.tags_list_helpExcludeGenres.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpExcludeGenres.TabIndex = 13;
            this.tags_list_helpExcludeGenres.Text = "?";
            // 
            // tags_list_chkScoreSort
            // 
            this.tags_list_chkScoreSort.AutoSize = true;
            this.tags_list_chkScoreSort.Location = new System.Drawing.Point(195, 73);
            this.tags_list_chkScoreSort.Name = "tags_list_chkScoreSort";
            this.tags_list_chkScoreSort.Size = new System.Drawing.Size(88, 17);
            this.tags_list_chkScoreSort.TabIndex = 10;
            this.tags_list_chkScoreSort.Text = "Sort by score";
            this.tags_list_chkScoreSort.UseVisualStyleBackColor = true;
            // 
            // tags_list_chkExcludeGenres
            // 
            this.tags_list_chkExcludeGenres.AutoSize = true;
            this.tags_list_chkExcludeGenres.Location = new System.Drawing.Point(6, 97);
            this.tags_list_chkExcludeGenres.Name = "tags_list_chkExcludeGenres";
            this.tags_list_chkExcludeGenres.Size = new System.Drawing.Size(99, 17);
            this.tags_list_chkExcludeGenres.TabIndex = 12;
            this.tags_list_chkExcludeGenres.Text = "Exclude genres";
            this.tags_list_chkExcludeGenres.UseVisualStyleBackColor = true;
            // 
            // tags_list_lblExplain
            // 
            this.tags_list_lblExplain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tags_list_lblExplain.Location = new System.Drawing.Point(7, 16);
            this.tags_list_lblExplain.Name = "tags_list_lblExplain";
            this.tags_list_lblExplain.Size = new System.Drawing.Size(376, 26);
            this.tags_list_lblExplain.TabIndex = 0;
            this.tags_list_lblExplain.Text = "These options determine which tags show up in the list above. Click \"Rebuild List" +
    "\" to apply these settings.";
            // 
            // tags_list_lblWeightFactor
            // 
            this.tags_list_lblWeightFactor.AutoSize = true;
            this.tags_list_lblWeightFactor.Location = new System.Drawing.Point(239, 52);
            this.tags_list_lblWeightFactor.Name = "tags_list_lblWeightFactor";
            this.tags_list_lblWeightFactor.Size = new System.Drawing.Size(85, 13);
            this.tags_list_lblWeightFactor.TabIndex = 5;
            this.tags_list_lblWeightFactor.Text = "Weighting factor";
            // 
            // tags_list_numWeightFactor
            // 
            this.tags_list_numWeightFactor.DecimalPlaces = 1;
            this.tags_list_numWeightFactor.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.tags_list_numWeightFactor.Location = new System.Drawing.Point(195, 48);
            this.tags_list_numWeightFactor.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tags_list_numWeightFactor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tags_list_numWeightFactor.Name = "tags_list_numWeightFactor";
            this.tags_list_numWeightFactor.Size = new System.Drawing.Size(38, 20);
            this.tags_list_numWeightFactor.TabIndex = 4;
            this.tags_list_numWeightFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tags_list_helpOwnedOnly
            // 
            this.tags_list_helpOwnedOnly.AutoSize = true;
            this.tags_list_helpOwnedOnly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpOwnedOnly.Location = new System.Drawing.Point(283, 97);
            this.tags_list_helpOwnedOnly.Name = "tags_list_helpOwnedOnly";
            this.tags_list_helpOwnedOnly.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpOwnedOnly.TabIndex = 15;
            this.tags_list_helpOwnedOnly.Text = "?";
            // 
            // tags_list_helpWeightFactor
            // 
            this.tags_list_helpWeightFactor.AutoSize = true;
            this.tags_list_helpWeightFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpWeightFactor.Location = new System.Drawing.Point(330, 52);
            this.tags_list_helpWeightFactor.Name = "tags_list_helpWeightFactor";
            this.tags_list_helpWeightFactor.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpWeightFactor.TabIndex = 6;
            this.tags_list_helpWeightFactor.Text = "?";
            // 
            // tags_list_helpTagsPerGame
            // 
            this.tags_list_helpTagsPerGame.AutoSize = true;
            this.tags_list_helpTagsPerGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpTagsPerGame.Location = new System.Drawing.Point(134, 75);
            this.tags_list_helpTagsPerGame.Name = "tags_list_helpTagsPerGame";
            this.tags_list_helpTagsPerGame.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpTagsPerGame.TabIndex = 9;
            this.tags_list_helpTagsPerGame.Text = "?";
            // 
            // tags_list_helpMinScore
            // 
            this.tags_list_helpMinScore.AutoSize = true;
            this.tags_list_helpMinScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpMinScore.Location = new System.Drawing.Point(134, 52);
            this.tags_list_helpMinScore.Name = "tags_list_helpMinScore";
            this.tags_list_helpMinScore.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpMinScore.TabIndex = 3;
            this.tags_list_helpMinScore.Text = "?";
            // 
            // tags_list_lblTagsPerGame
            // 
            this.tags_list_lblTagsPerGame.AutoSize = true;
            this.tags_list_lblTagsPerGame.Location = new System.Drawing.Point(50, 75);
            this.tags_list_lblTagsPerGame.Name = "tags_list_lblTagsPerGame";
            this.tags_list_lblTagsPerGame.Size = new System.Drawing.Size(78, 13);
            this.tags_list_lblTagsPerGame.TabIndex = 8;
            this.tags_list_lblTagsPerGame.Text = "Tags per game";
            // 
            // tags_list_numTagsPerGame
            // 
            this.tags_list_numTagsPerGame.Location = new System.Drawing.Point(6, 72);
            this.tags_list_numTagsPerGame.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.tags_list_numTagsPerGame.Name = "tags_list_numTagsPerGame";
            this.tags_list_numTagsPerGame.Size = new System.Drawing.Size(38, 20);
            this.tags_list_numTagsPerGame.TabIndex = 7;
            // 
            // tags_list_chkOwnedOnly
            // 
            this.tags_list_chkOwnedOnly.AutoSize = true;
            this.tags_list_chkOwnedOnly.Location = new System.Drawing.Point(195, 96);
            this.tags_list_chkOwnedOnly.Name = "tags_list_chkOwnedOnly";
            this.tags_list_chkOwnedOnly.Size = new System.Drawing.Size(82, 17);
            this.tags_list_chkOwnedOnly.TabIndex = 14;
            this.tags_list_chkOwnedOnly.Text = "Owned only";
            this.tags_list_chkOwnedOnly.UseVisualStyleBackColor = true;
            // 
            // tags_list_numMinScore
            // 
            this.tags_list_numMinScore.Location = new System.Drawing.Point(6, 48);
            this.tags_list_numMinScore.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.tags_list_numMinScore.Name = "tags_list_numMinScore";
            this.tags_list_numMinScore.Size = new System.Drawing.Size(38, 20);
            this.tags_list_numMinScore.TabIndex = 1;
            // 
            // tags_list_lblMinScore
            // 
            this.tags_list_lblMinScore.AutoSize = true;
            this.tags_list_lblMinScore.Location = new System.Drawing.Point(50, 52);
            this.tags_list_lblMinScore.Name = "tags_list_lblMinScore";
            this.tags_list_lblMinScore.Size = new System.Drawing.Size(71, 13);
            this.tags_list_lblMinScore.TabIndex = 2;
            this.tags_list_lblMinScore.Text = "Min tag score";
            // 
            // tags_lblIncluded
            // 
            this.tags_lblIncluded.AutoSize = true;
            this.tags_lblIncluded.Location = new System.Drawing.Point(6, 82);
            this.tags_lblIncluded.Name = "tags_lblIncluded";
            this.tags_lblIncluded.Size = new System.Drawing.Size(74, 13);
            this.tags_lblIncluded.TabIndex = 5;
            this.tags_lblIncluded.Text = "Included tags:";
            // 
            // tags_cmdListRebuild
            // 
            this.tags_cmdListRebuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tags_cmdListRebuild.Location = new System.Drawing.Point(11, 174);
            this.tags_cmdListRebuild.Name = "tags_cmdListRebuild";
            this.tags_cmdListRebuild.Size = new System.Drawing.Size(75, 23);
            this.tags_cmdListRebuild.TabIndex = 7;
            this.tags_cmdListRebuild.Text = "Rebuild List";
            this.tags_cmdListRebuild.UseVisualStyleBackColor = true;
            this.tags_cmdListRebuild.Click += new System.EventHandler(this.tags_cmdListRebuild_Click);
            // 
            // tags_cmdCheckAll
            // 
            this.tags_cmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tags_cmdCheckAll.Location = new System.Drawing.Point(239, 174);
            this.tags_cmdCheckAll.Name = "tags_cmdCheckAll";
            this.tags_cmdCheckAll.Size = new System.Drawing.Size(75, 23);
            this.tags_cmdCheckAll.TabIndex = 8;
            this.tags_cmdCheckAll.Text = "Check All";
            this.tags_cmdCheckAll.UseVisualStyleBackColor = true;
            this.tags_cmdCheckAll.Click += new System.EventHandler(this.tags_cmdCheckAll_Click);
            // 
            // tags_cmdUncheckAll
            // 
            this.tags_cmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tags_cmdUncheckAll.Location = new System.Drawing.Point(320, 174);
            this.tags_cmdUncheckAll.Name = "tags_cmdUncheckAll";
            this.tags_cmdUncheckAll.Size = new System.Drawing.Size(75, 23);
            this.tags_cmdUncheckAll.TabIndex = 9;
            this.tags_cmdUncheckAll.Text = "Uncheck All";
            this.tags_cmdUncheckAll.UseVisualStyleBackColor = true;
            this.tags_cmdUncheckAll.Click += new System.EventHandler(this.tags_cmdUncheckAll_Click);
            // 
            // tags_numMaxTags
            // 
            this.tags_numMaxTags.Location = new System.Drawing.Point(6, 50);
            this.tags_numMaxTags.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.tags_numMaxTags.Name = "tags_numMaxTags";
            this.tags_numMaxTags.Size = new System.Drawing.Size(52, 20);
            this.tags_numMaxTags.TabIndex = 3;
            // 
            // tags_lblMaxTags
            // 
            this.tags_lblMaxTags.AutoSize = true;
            this.tags_lblMaxTags.Location = new System.Drawing.Point(64, 47);
            this.tags_lblMaxTags.Name = "tags_lblMaxTags";
            this.tags_lblMaxTags.Size = new System.Drawing.Size(97, 26);
            this.tags_lblMaxTags.TabIndex = 4;
            this.tags_lblMaxTags.Text = "Max tags per game\r\n(0 for unlimited)";
            // 
            // tags_lblPrefix
            // 
            this.tags_lblPrefix.AutoSize = true;
            this.tags_lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.tags_lblPrefix.Name = "tags_lblPrefix";
            this.tags_lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.tags_lblPrefix.TabIndex = 0;
            this.tags_lblPrefix.Text = "Prefix:";
            // 
            // tags_txtPrefix
            // 
            this.tags_txtPrefix.Location = new System.Drawing.Point(67, 19);
            this.tags_txtPrefix.Name = "tags_txtPrefix";
            this.tags_txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.tags_txtPrefix.TabIndex = 1;
            // 
            // tags_lstIncluded
            // 
            this.tags_lstIncluded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tags_lstIncluded.CheckBoxes = true;
            this.tags_lstIncluded.Location = new System.Drawing.Point(9, 98);
            this.tags_lstIncluded.Name = "tags_lstIncluded";
            this.tags_lstIncluded.Size = new System.Drawing.Size(386, 73);
            this.tags_lstIncluded.TabIndex = 6;
            this.tags_lstIncluded.UseCompatibleStateImageBehavior = false;
            this.tags_lstIncluded.View = System.Windows.Forms.View.List;
            // 
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // DlgAutoCat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(599, 384);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.MinimumSize = new System.Drawing.Size(615, 400);
            this.Name = "DlgAutoCat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit AutoCats";
            this.Load += new System.EventHandler(this.DlgAutoCat_Load);
            this.grpList.ResumeLayout(false);
            this.panGenre.ResumeLayout(false);
            this.genre_grpMain.ResumeLayout(false);
            this.genre_grpMain.PerformLayout();
            this.genre_tblIgnore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.genre_numMaxCats)).EndInit();
            this.panFlags.ResumeLayout(false);
            this.flags_grpMain.ResumeLayout(false);
            this.flags_grpMain.PerformLayout();
            this.flags_tblButtons.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panTags.ResumeLayout(false);
            this.tags_grpMain.ResumeLayout(false);
            this.tags_grpMain.PerformLayout();
            this.tags_grpListOpts.ResumeLayout(false);
            this.tags_grpListOpts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numWeightFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numTagsPerGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numMinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_numMaxTags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstAutoCats;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdRename;
        private System.Windows.Forms.Button cmdCreate;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.Panel panGenre;
        private System.Windows.Forms.GroupBox genre_grpMain;
        private System.Windows.Forms.TableLayoutPanel genre_tblIgnore;
        private System.Windows.Forms.Button genre_cmdUncheckAll;
        private System.Windows.Forms.Button genre_cmdCheckAll;
        private System.Windows.Forms.ListView genre_lstIgnore;
        private System.Windows.Forms.Label genre_lblIgnore;
        private System.Windows.Forms.CheckBox genre_chkRemoveExisting;
        private System.Windows.Forms.Label genre_lblMaxCats;
        private System.Windows.Forms.NumericUpDown genre_numMaxCats;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label genre_lblPrefix;
        private System.Windows.Forms.TextBox genre_txtPrefix;
        private System.Windows.Forms.Panel panFlags;
        private System.Windows.Forms.GroupBox flags_grpMain;
        private System.Windows.Forms.TableLayoutPanel flags_tblButtons;
        private System.Windows.Forms.Button flags_cmdCheckAll;
        private System.Windows.Forms.Button flags_cmdUncheckAll;
        private System.Windows.Forms.Label flags_lblInclude;
        private System.Windows.Forms.ListView flags_lstIncluded;
        private System.Windows.Forms.TextBox flags_txtPrefix;
        private System.Windows.Forms.Label flags_lblPrefix;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label genre_helpRemoveExisting;
        private System.Windows.Forms.Label genre_helpPrefix;
        private System.Windows.Forms.Label flags_helpPrefix;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Panel panTags;
        private System.Windows.Forms.GroupBox tags_grpMain;
        private System.Windows.Forms.Label tags_lblIncluded;
        private System.Windows.Forms.CheckBox tags_list_chkOwnedOnly;
        private System.Windows.Forms.Label tags_list_lblMinScore;
        private System.Windows.Forms.NumericUpDown tags_list_numMinScore;
        private System.Windows.Forms.Button tags_cmdCheckAll;
        private System.Windows.Forms.Button tags_cmdUncheckAll;
        private System.Windows.Forms.Button tags_cmdListRebuild;
        private System.Windows.Forms.NumericUpDown tags_numMaxTags;
        private System.Windows.Forms.Label tags_lblMaxTags;
        private System.Windows.Forms.Label tags_lblPrefix;
        private System.Windows.Forms.TextBox tags_txtPrefix;
        private System.Windows.Forms.ListView tags_lstIncluded;
        private System.Windows.Forms.GroupBox tags_grpListOpts;
        private System.Windows.Forms.Label tags_list_lblTagsPerGame;
        private System.Windows.Forms.NumericUpDown tags_list_numTagsPerGame;
        private System.Windows.Forms.Label tags_list_helpOwnedOnly;
        private System.Windows.Forms.Label tags_list_helpWeightFactor;
        private System.Windows.Forms.Label tags_list_helpTagsPerGame;
        private System.Windows.Forms.Label tags_list_helpMinScore;
        private System.Windows.Forms.Label tags_list_lblWeightFactor;
        private System.Windows.Forms.NumericUpDown tags_list_numWeightFactor;
        private System.Windows.Forms.Label tags_list_lblExplain;
        private System.Windows.Forms.Label tags_helpPrefix;
        private System.Windows.Forms.CheckBox tags_list_chkScoreSort;
        private System.Windows.Forms.CheckBox tags_list_chkExcludeGenres;
        private System.Windows.Forms.Label tags_list_helpScoreSort;
        private System.Windows.Forms.Label tags_list_helpExcludeGenres;
    }
}