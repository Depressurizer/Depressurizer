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
            this.panEditGenre = new System.Windows.Forms.Panel();
            this.grpEditGenre = new System.Windows.Forms.GroupBox();
            this.genre_lblHelp_RemoveExisting = new System.Windows.Forms.Label();
            this.genre_lblHelp_Prefix = new System.Windows.Forms.Label();
            this.genreLblPrefix = new System.Windows.Forms.Label();
            this.genreTxtPrefix = new System.Windows.Forms.TextBox();
            this.genreTblIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.genreCmdUncheckAll = new System.Windows.Forms.Button();
            this.genreCmdCheckAll = new System.Windows.Forms.Button();
            this.genreLstIgnore = new System.Windows.Forms.ListView();
            this.lblGenreCatsToIgnore = new System.Windows.Forms.Label();
            this.genreChkRemoveExisting = new System.Windows.Forms.CheckBox();
            this.genreLblMacCats = new System.Windows.Forms.Label();
            this.genreNumMaxCats = new System.Windows.Forms.NumericUpDown();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.panEditFlags = new System.Windows.Forms.Panel();
            this.flagsGrp = new System.Windows.Forms.GroupBox();
            this.flags_lblHelp_Prefix = new System.Windows.Forms.Label();
            this.flagsTbl = new System.Windows.Forms.TableLayoutPanel();
            this.flagsCmdCheckAll = new System.Windows.Forms.Button();
            this.flagsCmdUncheckAll = new System.Windows.Forms.Button();
            this.flagsLblInclude = new System.Windows.Forms.Label();
            this.flagsLstIncluded = new System.Windows.Forms.ListView();
            this.flagsTxtPrefix = new System.Windows.Forms.TextBox();
            this.flagsLblPrefix = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panEditTags = new System.Windows.Forms.Panel();
            this.tags_Group = new System.Windows.Forms.GroupBox();
            this.tags_grpListOpts = new System.Windows.Forms.GroupBox();
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
            this.tags_list_numWeightFactor = new System.Windows.Forms.NumericUpDown();
            this.tags_list_lblWeightFactor = new System.Windows.Forms.Label();
            this.grpList.SuspendLayout();
            this.panEditGenre.SuspendLayout();
            this.grpEditGenre.SuspendLayout();
            this.genreTblIgnore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.genreNumMaxCats)).BeginInit();
            this.panEditFlags.SuspendLayout();
            this.flagsGrp.SuspendLayout();
            this.flagsTbl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panEditTags.SuspendLayout();
            this.tags_Group.SuspendLayout();
            this.tags_grpListOpts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numTagsPerGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numMinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_numMaxTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numWeightFactor)).BeginInit();
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
            this.lstAutoCats.Size = new System.Drawing.Size(195, 213);
            this.lstAutoCats.TabIndex = 0;
            this.lstAutoCats.SelectedIndexChanged += new System.EventHandler(this.lstAutoCats_SelectedIndexChanged);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Location = new System.Drawing.Point(6, 289);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(195, 23);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdRename
            // 
            this.cmdRename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRename.Location = new System.Drawing.Point(6, 263);
            this.cmdRename.Name = "cmdRename";
            this.cmdRename.Size = new System.Drawing.Size(195, 23);
            this.cmdRename.TabIndex = 2;
            this.cmdRename.Text = "Rename";
            this.cmdRename.UseVisualStyleBackColor = true;
            this.cmdRename.Click += new System.EventHandler(this.cmdRename_Click);
            // 
            // cmdCreate
            // 
            this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreate.Location = new System.Drawing.Point(6, 237);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(195, 23);
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
            this.grpList.Size = new System.Drawing.Size(208, 319);
            this.grpList.TabIndex = 0;
            this.grpList.TabStop = false;
            this.grpList.Text = "AutoCat List";
            // 
            // panEditGenre
            // 
            this.panEditGenre.Controls.Add(this.grpEditGenre);
            this.panEditGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panEditGenre.Location = new System.Drawing.Point(0, 0);
            this.panEditGenre.Name = "panEditGenre";
            this.panEditGenre.Size = new System.Drawing.Size(435, 319);
            this.panEditGenre.TabIndex = 1;
            // 
            // grpEditGenre
            // 
            this.grpEditGenre.Controls.Add(this.genre_lblHelp_RemoveExisting);
            this.grpEditGenre.Controls.Add(this.genre_lblHelp_Prefix);
            this.grpEditGenre.Controls.Add(this.genreLblPrefix);
            this.grpEditGenre.Controls.Add(this.genreTxtPrefix);
            this.grpEditGenre.Controls.Add(this.genreTblIgnore);
            this.grpEditGenre.Controls.Add(this.genreLstIgnore);
            this.grpEditGenre.Controls.Add(this.lblGenreCatsToIgnore);
            this.grpEditGenre.Controls.Add(this.genreChkRemoveExisting);
            this.grpEditGenre.Controls.Add(this.genreLblMacCats);
            this.grpEditGenre.Controls.Add(this.genreNumMaxCats);
            this.grpEditGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEditGenre.Location = new System.Drawing.Point(0, 0);
            this.grpEditGenre.Name = "grpEditGenre";
            this.grpEditGenre.Size = new System.Drawing.Size(435, 319);
            this.grpEditGenre.TabIndex = 0;
            this.grpEditGenre.TabStop = false;
            this.grpEditGenre.Text = "Edit Genre AutoCat";
            // 
            // genre_lblHelp_RemoveExisting
            // 
            this.genre_lblHelp_RemoveExisting.AutoSize = true;
            this.genre_lblHelp_RemoveExisting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.genre_lblHelp_RemoveExisting.Location = new System.Drawing.Point(238, 82);
            this.genre_lblHelp_RemoveExisting.Name = "genre_lblHelp_RemoveExisting";
            this.genre_lblHelp_RemoveExisting.Size = new System.Drawing.Size(15, 15);
            this.genre_lblHelp_RemoveExisting.TabIndex = 12;
            this.genre_lblHelp_RemoveExisting.Text = "?";
            // 
            // genre_lblHelp_Prefix
            // 
            this.genre_lblHelp_Prefix.AutoSize = true;
            this.genre_lblHelp_Prefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.genre_lblHelp_Prefix.Location = new System.Drawing.Point(238, 22);
            this.genre_lblHelp_Prefix.Name = "genre_lblHelp_Prefix";
            this.genre_lblHelp_Prefix.Size = new System.Drawing.Size(15, 15);
            this.genre_lblHelp_Prefix.TabIndex = 11;
            this.genre_lblHelp_Prefix.Text = "?";
            // 
            // genreLblPrefix
            // 
            this.genreLblPrefix.AutoSize = true;
            this.genreLblPrefix.Location = new System.Drawing.Point(25, 22);
            this.genreLblPrefix.Name = "genreLblPrefix";
            this.genreLblPrefix.Size = new System.Drawing.Size(36, 13);
            this.genreLblPrefix.TabIndex = 0;
            this.genreLblPrefix.Text = "Prefix:";
            // 
            // genreTxtPrefix
            // 
            this.genreTxtPrefix.Location = new System.Drawing.Point(67, 19);
            this.genreTxtPrefix.Name = "genreTxtPrefix";
            this.genreTxtPrefix.Size = new System.Drawing.Size(165, 20);
            this.genreTxtPrefix.TabIndex = 1;
            // 
            // genreTblIgnore
            // 
            this.genreTblIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genreTblIgnore.ColumnCount = 2;
            this.genreTblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genreTblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genreTblIgnore.Controls.Add(this.genreCmdUncheckAll, 1, 0);
            this.genreTblIgnore.Controls.Add(this.genreCmdCheckAll, 0, 0);
            this.genreTblIgnore.Location = new System.Drawing.Point(6, 285);
            this.genreTblIgnore.Name = "genreTblIgnore";
            this.genreTblIgnore.RowCount = 1;
            this.genreTblIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genreTblIgnore.Size = new System.Drawing.Size(427, 30);
            this.genreTblIgnore.TabIndex = 7;
            // 
            // genreCmdUncheckAll
            // 
            this.genreCmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genreCmdUncheckAll.Location = new System.Drawing.Point(216, 3);
            this.genreCmdUncheckAll.Name = "genreCmdUncheckAll";
            this.genreCmdUncheckAll.Size = new System.Drawing.Size(208, 23);
            this.genreCmdUncheckAll.TabIndex = 7;
            this.genreCmdUncheckAll.Text = "Uncheck All";
            this.genreCmdUncheckAll.UseVisualStyleBackColor = true;
            this.genreCmdUncheckAll.Click += new System.EventHandler(this.genreCmdUncheckAll_Click);
            // 
            // genreCmdCheckAll
            // 
            this.genreCmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genreCmdCheckAll.Location = new System.Drawing.Point(3, 3);
            this.genreCmdCheckAll.Name = "genreCmdCheckAll";
            this.genreCmdCheckAll.Size = new System.Drawing.Size(207, 23);
            this.genreCmdCheckAll.TabIndex = 6;
            this.genreCmdCheckAll.Text = "Check All";
            this.genreCmdCheckAll.UseVisualStyleBackColor = true;
            this.genreCmdCheckAll.Click += new System.EventHandler(this.genreCmdCheckAll_Click);
            // 
            // genreLstIgnore
            // 
            this.genreLstIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genreLstIgnore.CheckBoxes = true;
            this.genreLstIgnore.Location = new System.Drawing.Point(9, 130);
            this.genreLstIgnore.Name = "genreLstIgnore";
            this.genreLstIgnore.Size = new System.Drawing.Size(422, 156);
            this.genreLstIgnore.TabIndex = 6;
            this.genreLstIgnore.UseCompatibleStateImageBehavior = false;
            this.genreLstIgnore.View = System.Windows.Forms.View.List;
            // 
            // lblGenreCatsToIgnore
            // 
            this.lblGenreCatsToIgnore.AutoSize = true;
            this.lblGenreCatsToIgnore.Location = new System.Drawing.Point(6, 114);
            this.lblGenreCatsToIgnore.Name = "lblGenreCatsToIgnore";
            this.lblGenreCatsToIgnore.Size = new System.Drawing.Size(104, 13);
            this.lblGenreCatsToIgnore.TabIndex = 5;
            this.lblGenreCatsToIgnore.Text = "Categories to ignore:";
            // 
            // genreChkRemoveExisting
            // 
            this.genreChkRemoveExisting.AutoSize = true;
            this.genreChkRemoveExisting.Location = new System.Drawing.Point(46, 82);
            this.genreChkRemoveExisting.Name = "genreChkRemoveExisting";
            this.genreChkRemoveExisting.Size = new System.Drawing.Size(186, 17);
            this.genreChkRemoveExisting.TabIndex = 4;
            this.genreChkRemoveExisting.Text = "Remove existing genre categories";
            this.genreChkRemoveExisting.UseVisualStyleBackColor = true;
            // 
            // genreLblMacCats
            // 
            this.genreLblMacCats.AutoSize = true;
            this.genreLblMacCats.Location = new System.Drawing.Point(64, 47);
            this.genreLblMacCats.Name = "genreLblMacCats";
            this.genreLblMacCats.Size = new System.Drawing.Size(148, 26);
            this.genreLblMacCats.TabIndex = 3;
            this.genreLblMacCats.Text = "Maximum categories to assign\r\n(0 for unlimited)";
            // 
            // genreNumMaxCats
            // 
            this.genreNumMaxCats.Location = new System.Drawing.Point(6, 50);
            this.genreNumMaxCats.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.genreNumMaxCats.Name = "genreNumMaxCats";
            this.genreNumMaxCats.Size = new System.Drawing.Size(52, 20);
            this.genreNumMaxCats.TabIndex = 2;
            this.genreNumMaxCats.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.genreNumMaxCats.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdSave.Location = new System.Drawing.Point(508, 325);
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
            this.cmdCancel.Location = new System.Drawing.Point(412, 325);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(90, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // panEditFlags
            // 
            this.panEditFlags.Controls.Add(this.flagsGrp);
            this.panEditFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panEditFlags.Location = new System.Drawing.Point(0, 0);
            this.panEditFlags.Name = "panEditFlags";
            this.panEditFlags.Size = new System.Drawing.Size(435, 319);
            this.panEditFlags.TabIndex = 1;
            // 
            // flagsGrp
            // 
            this.flagsGrp.Controls.Add(this.flags_lblHelp_Prefix);
            this.flagsGrp.Controls.Add(this.flagsTbl);
            this.flagsGrp.Controls.Add(this.flagsLblInclude);
            this.flagsGrp.Controls.Add(this.flagsLstIncluded);
            this.flagsGrp.Controls.Add(this.flagsTxtPrefix);
            this.flagsGrp.Controls.Add(this.flagsLblPrefix);
            this.flagsGrp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagsGrp.Location = new System.Drawing.Point(0, 0);
            this.flagsGrp.Name = "flagsGrp";
            this.flagsGrp.Size = new System.Drawing.Size(435, 319);
            this.flagsGrp.TabIndex = 0;
            this.flagsGrp.TabStop = false;
            this.flagsGrp.Text = "Edit Flag Autocat";
            // 
            // flags_lblHelp_Prefix
            // 
            this.flags_lblHelp_Prefix.AutoSize = true;
            this.flags_lblHelp_Prefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flags_lblHelp_Prefix.Location = new System.Drawing.Point(238, 22);
            this.flags_lblHelp_Prefix.Name = "flags_lblHelp_Prefix";
            this.flags_lblHelp_Prefix.Size = new System.Drawing.Size(15, 15);
            this.flags_lblHelp_Prefix.TabIndex = 6;
            this.flags_lblHelp_Prefix.Text = "?";
            // 
            // flagsTbl
            // 
            this.flagsTbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagsTbl.ColumnCount = 2;
            this.flagsTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.flagsTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.flagsTbl.Controls.Add(this.flagsCmdCheckAll, 0, 0);
            this.flagsTbl.Controls.Add(this.flagsCmdUncheckAll, 1, 0);
            this.flagsTbl.Location = new System.Drawing.Point(3, 285);
            this.flagsTbl.Name = "flagsTbl";
            this.flagsTbl.RowCount = 1;
            this.flagsTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.flagsTbl.Size = new System.Drawing.Size(429, 30);
            this.flagsTbl.TabIndex = 4;
            // 
            // flagsCmdCheckAll
            // 
            this.flagsCmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagsCmdCheckAll.Location = new System.Drawing.Point(3, 3);
            this.flagsCmdCheckAll.Name = "flagsCmdCheckAll";
            this.flagsCmdCheckAll.Size = new System.Drawing.Size(208, 23);
            this.flagsCmdCheckAll.TabIndex = 0;
            this.flagsCmdCheckAll.Text = "Check All";
            this.flagsCmdCheckAll.UseVisualStyleBackColor = true;
            this.flagsCmdCheckAll.Click += new System.EventHandler(this.flagsCmdCheckAll_Click);
            // 
            // flagsCmdUncheckAll
            // 
            this.flagsCmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagsCmdUncheckAll.Location = new System.Drawing.Point(217, 3);
            this.flagsCmdUncheckAll.Name = "flagsCmdUncheckAll";
            this.flagsCmdUncheckAll.Size = new System.Drawing.Size(209, 23);
            this.flagsCmdUncheckAll.TabIndex = 1;
            this.flagsCmdUncheckAll.Text = "Uncheck All";
            this.flagsCmdUncheckAll.UseVisualStyleBackColor = true;
            this.flagsCmdUncheckAll.Click += new System.EventHandler(this.flagsCmdUncheckAll_Click);
            // 
            // flagsLblInclude
            // 
            this.flagsLblInclude.AutoSize = true;
            this.flagsLblInclude.Location = new System.Drawing.Point(3, 69);
            this.flagsLblInclude.Name = "flagsLblInclude";
            this.flagsLblInclude.Size = new System.Drawing.Size(79, 13);
            this.flagsLblInclude.TabIndex = 2;
            this.flagsLblInclude.Text = "Included Flags:";
            // 
            // flagsLstIncluded
            // 
            this.flagsLstIncluded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagsLstIncluded.CheckBoxes = true;
            this.flagsLstIncluded.Location = new System.Drawing.Point(6, 85);
            this.flagsLstIncluded.Name = "flagsLstIncluded";
            this.flagsLstIncluded.Size = new System.Drawing.Size(423, 198);
            this.flagsLstIncluded.TabIndex = 3;
            this.flagsLstIncluded.UseCompatibleStateImageBehavior = false;
            this.flagsLstIncluded.View = System.Windows.Forms.View.List;
            // 
            // flagsTxtPrefix
            // 
            this.flagsTxtPrefix.Location = new System.Drawing.Point(67, 19);
            this.flagsTxtPrefix.Name = "flagsTxtPrefix";
            this.flagsTxtPrefix.Size = new System.Drawing.Size(165, 20);
            this.flagsTxtPrefix.TabIndex = 1;
            // 
            // flagsLblPrefix
            // 
            this.flagsLblPrefix.AutoSize = true;
            this.flagsLblPrefix.Location = new System.Drawing.Point(25, 22);
            this.flagsLblPrefix.Name = "flagsLblPrefix";
            this.flagsLblPrefix.Size = new System.Drawing.Size(36, 13);
            this.flagsLblPrefix.TabIndex = 0;
            this.flagsLblPrefix.Text = "Prefix:";
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
            this.splitContainer.Panel2.Controls.Add(this.panEditTags);
            this.splitContainer.Panel2.Controls.Add(this.panEditGenre);
            this.splitContainer.Panel2.Controls.Add(this.panEditFlags);
            this.splitContainer.Panel2MinSize = 250;
            this.splitContainer.Size = new System.Drawing.Size(647, 319);
            this.splitContainer.SplitterDistance = 208;
            this.splitContainer.TabIndex = 7;
            // 
            // panEditTags
            // 
            this.panEditTags.Controls.Add(this.tags_Group);
            this.panEditTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panEditTags.Location = new System.Drawing.Point(0, 0);
            this.panEditTags.Name = "panEditTags";
            this.panEditTags.Size = new System.Drawing.Size(435, 319);
            this.panEditTags.TabIndex = 2;
            // 
            // tags_Group
            // 
            this.tags_Group.Controls.Add(this.tags_grpListOpts);
            this.tags_Group.Controls.Add(this.tags_lblIncluded);
            this.tags_Group.Controls.Add(this.tags_cmdListRebuild);
            this.tags_Group.Controls.Add(this.tags_cmdCheckAll);
            this.tags_Group.Controls.Add(this.tags_cmdUncheckAll);
            this.tags_Group.Controls.Add(this.tags_numMaxTags);
            this.tags_Group.Controls.Add(this.tags_lblMaxTags);
            this.tags_Group.Controls.Add(this.tags_lblPrefix);
            this.tags_Group.Controls.Add(this.tags_txtPrefix);
            this.tags_Group.Controls.Add(this.tags_lstIncluded);
            this.tags_Group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tags_Group.Location = new System.Drawing.Point(0, 0);
            this.tags_Group.Name = "tags_Group";
            this.tags_Group.Size = new System.Drawing.Size(435, 319);
            this.tags_Group.TabIndex = 0;
            this.tags_Group.TabStop = false;
            this.tags_Group.Text = "Edit Tag Autocat";
            // 
            // tags_grpListOpts
            // 
            this.tags_grpListOpts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.tags_grpListOpts.Location = new System.Drawing.Point(6, 237);
            this.tags_grpListOpts.Name = "tags_grpListOpts";
            this.tags_grpListOpts.Size = new System.Drawing.Size(422, 74);
            this.tags_grpListOpts.TabIndex = 13;
            this.tags_grpListOpts.TabStop = false;
            this.tags_grpListOpts.Text = "Tag List Options";
            // 
            // tags_list_helpOwnedOnly
            // 
            this.tags_list_helpOwnedOnly.AutoSize = true;
            this.tags_list_helpOwnedOnly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpOwnedOnly.Location = new System.Drawing.Point(283, 47);
            this.tags_list_helpOwnedOnly.Name = "tags_list_helpOwnedOnly";
            this.tags_list_helpOwnedOnly.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpOwnedOnly.TabIndex = 17;
            this.tags_list_helpOwnedOnly.Text = "?";
            // 
            // tags_list_helpWeightFactor
            // 
            this.tags_list_helpWeightFactor.AutoSize = true;
            this.tags_list_helpWeightFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpWeightFactor.Location = new System.Drawing.Point(330, 22);
            this.tags_list_helpWeightFactor.Name = "tags_list_helpWeightFactor";
            this.tags_list_helpWeightFactor.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpWeightFactor.TabIndex = 16;
            this.tags_list_helpWeightFactor.Text = "?";
            // 
            // tags_list_helpTagsPerGame
            // 
            this.tags_list_helpTagsPerGame.AutoSize = true;
            this.tags_list_helpTagsPerGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpTagsPerGame.Location = new System.Drawing.Point(134, 47);
            this.tags_list_helpTagsPerGame.Name = "tags_list_helpTagsPerGame";
            this.tags_list_helpTagsPerGame.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpTagsPerGame.TabIndex = 15;
            this.tags_list_helpTagsPerGame.Text = "?";
            // 
            // tags_list_helpMinScore
            // 
            this.tags_list_helpMinScore.AutoSize = true;
            this.tags_list_helpMinScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tags_list_helpMinScore.Location = new System.Drawing.Point(134, 22);
            this.tags_list_helpMinScore.Name = "tags_list_helpMinScore";
            this.tags_list_helpMinScore.Size = new System.Drawing.Size(15, 15);
            this.tags_list_helpMinScore.TabIndex = 14;
            this.tags_list_helpMinScore.Text = "?";
            // 
            // tags_list_lblTagsPerGame
            // 
            this.tags_list_lblTagsPerGame.AutoSize = true;
            this.tags_list_lblTagsPerGame.Location = new System.Drawing.Point(50, 49);
            this.tags_list_lblTagsPerGame.Name = "tags_list_lblTagsPerGame";
            this.tags_list_lblTagsPerGame.Size = new System.Drawing.Size(78, 13);
            this.tags_list_lblTagsPerGame.TabIndex = 13;
            this.tags_list_lblTagsPerGame.Text = "Tags per game";
            // 
            // tags_list_numTagsPerGame
            // 
            this.tags_list_numTagsPerGame.Location = new System.Drawing.Point(6, 45);
            this.tags_list_numTagsPerGame.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.tags_list_numTagsPerGame.Name = "tags_list_numTagsPerGame";
            this.tags_list_numTagsPerGame.Size = new System.Drawing.Size(38, 20);
            this.tags_list_numTagsPerGame.TabIndex = 12;
            // 
            // tags_list_chkOwnedOnly
            // 
            this.tags_list_chkOwnedOnly.AutoSize = true;
            this.tags_list_chkOwnedOnly.Location = new System.Drawing.Point(195, 46);
            this.tags_list_chkOwnedOnly.Name = "tags_list_chkOwnedOnly";
            this.tags_list_chkOwnedOnly.Size = new System.Drawing.Size(82, 17);
            this.tags_list_chkOwnedOnly.TabIndex = 11;
            this.tags_list_chkOwnedOnly.Text = "Owned only";
            this.tags_list_chkOwnedOnly.UseVisualStyleBackColor = true;
            // 
            // tags_list_numMinScore
            // 
            this.tags_list_numMinScore.Location = new System.Drawing.Point(6, 19);
            this.tags_list_numMinScore.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.tags_list_numMinScore.Name = "tags_list_numMinScore";
            this.tags_list_numMinScore.Size = new System.Drawing.Size(38, 20);
            this.tags_list_numMinScore.TabIndex = 9;
            // 
            // tags_list_lblMinScore
            // 
            this.tags_list_lblMinScore.AutoSize = true;
            this.tags_list_lblMinScore.Location = new System.Drawing.Point(50, 23);
            this.tags_list_lblMinScore.Name = "tags_list_lblMinScore";
            this.tags_list_lblMinScore.Size = new System.Drawing.Size(71, 13);
            this.tags_list_lblMinScore.TabIndex = 10;
            this.tags_list_lblMinScore.Text = "Min tag score";
            // 
            // tags_lblIncluded
            // 
            this.tags_lblIncluded.AutoSize = true;
            this.tags_lblIncluded.Location = new System.Drawing.Point(6, 82);
            this.tags_lblIncluded.Name = "tags_lblIncluded";
            this.tags_lblIncluded.Size = new System.Drawing.Size(74, 13);
            this.tags_lblIncluded.TabIndex = 12;
            this.tags_lblIncluded.Text = "Included tags:";
            // 
            // tags_cmdListRebuild
            // 
            this.tags_cmdListRebuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tags_cmdListRebuild.Location = new System.Drawing.Point(9, 209);
            this.tags_cmdListRebuild.Name = "tags_cmdListRebuild";
            this.tags_cmdListRebuild.Size = new System.Drawing.Size(75, 23);
            this.tags_cmdListRebuild.TabIndex = 6;
            this.tags_cmdListRebuild.Text = "Rebuild List";
            this.tags_cmdListRebuild.UseVisualStyleBackColor = true;
            this.tags_cmdListRebuild.Click += new System.EventHandler(this.tags_cmdListRebuild_Click);
            // 
            // tags_cmdCheckAll
            // 
            this.tags_cmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tags_cmdCheckAll.Location = new System.Drawing.Point(270, 209);
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
            this.tags_cmdUncheckAll.Location = new System.Drawing.Point(351, 209);
            this.tags_cmdUncheckAll.Name = "tags_cmdUncheckAll";
            this.tags_cmdUncheckAll.Size = new System.Drawing.Size(75, 23);
            this.tags_cmdUncheckAll.TabIndex = 7;
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
            this.tags_numMaxTags.TabIndex = 4;
            // 
            // tags_lblMaxTags
            // 
            this.tags_lblMaxTags.AutoSize = true;
            this.tags_lblMaxTags.Location = new System.Drawing.Point(64, 47);
            this.tags_lblMaxTags.Name = "tags_lblMaxTags";
            this.tags_lblMaxTags.Size = new System.Drawing.Size(97, 26);
            this.tags_lblMaxTags.TabIndex = 3;
            this.tags_lblMaxTags.Text = "Max tags per game\r\n(0 for unlimited)";
            // 
            // tags_lblPrefix
            // 
            this.tags_lblPrefix.AutoSize = true;
            this.tags_lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.tags_lblPrefix.Name = "tags_lblPrefix";
            this.tags_lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.tags_lblPrefix.TabIndex = 2;
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
            this.tags_lstIncluded.Size = new System.Drawing.Size(419, 105);
            this.tags_lstIncluded.TabIndex = 0;
            this.tags_lstIncluded.UseCompatibleStateImageBehavior = false;
            this.tags_lstIncluded.View = System.Windows.Forms.View.List;
            // 
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // tags_list_numWeightFactor
            // 
            this.tags_list_numWeightFactor.DecimalPlaces = 1;
            this.tags_list_numWeightFactor.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.tags_list_numWeightFactor.Location = new System.Drawing.Point(195, 19);
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
            this.tags_list_numWeightFactor.TabIndex = 18;
            this.tags_list_numWeightFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tags_list_lblWeightFactor
            // 
            this.tags_list_lblWeightFactor.AutoSize = true;
            this.tags_list_lblWeightFactor.Location = new System.Drawing.Point(239, 23);
            this.tags_list_lblWeightFactor.Name = "tags_list_lblWeightFactor";
            this.tags_list_lblWeightFactor.Size = new System.Drawing.Size(85, 13);
            this.tags_list_lblWeightFactor.TabIndex = 19;
            this.tags_list_lblWeightFactor.Text = "Weighting factor";
            // 
            // DlgAutoCat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(656, 356);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "DlgAutoCat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit AutoCats";
            this.Load += new System.EventHandler(this.DlgAutoCat_Load);
            this.grpList.ResumeLayout(false);
            this.panEditGenre.ResumeLayout(false);
            this.grpEditGenre.ResumeLayout(false);
            this.grpEditGenre.PerformLayout();
            this.genreTblIgnore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.genreNumMaxCats)).EndInit();
            this.panEditFlags.ResumeLayout(false);
            this.flagsGrp.ResumeLayout(false);
            this.flagsGrp.PerformLayout();
            this.flagsTbl.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panEditTags.ResumeLayout(false);
            this.tags_Group.ResumeLayout(false);
            this.tags_Group.PerformLayout();
            this.tags_grpListOpts.ResumeLayout(false);
            this.tags_grpListOpts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numTagsPerGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numMinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_numMaxTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tags_list_numWeightFactor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstAutoCats;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdRename;
        private System.Windows.Forms.Button cmdCreate;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.Panel panEditGenre;
        private System.Windows.Forms.GroupBox grpEditGenre;
        private System.Windows.Forms.TableLayoutPanel genreTblIgnore;
        private System.Windows.Forms.Button genreCmdUncheckAll;
        private System.Windows.Forms.Button genreCmdCheckAll;
        private System.Windows.Forms.ListView genreLstIgnore;
        private System.Windows.Forms.Label lblGenreCatsToIgnore;
        private System.Windows.Forms.CheckBox genreChkRemoveExisting;
        private System.Windows.Forms.Label genreLblMacCats;
        private System.Windows.Forms.NumericUpDown genreNumMaxCats;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label genreLblPrefix;
        private System.Windows.Forms.TextBox genreTxtPrefix;
        private System.Windows.Forms.Panel panEditFlags;
        private System.Windows.Forms.GroupBox flagsGrp;
        private System.Windows.Forms.TableLayoutPanel flagsTbl;
        private System.Windows.Forms.Button flagsCmdCheckAll;
        private System.Windows.Forms.Button flagsCmdUncheckAll;
        private System.Windows.Forms.Label flagsLblInclude;
        private System.Windows.Forms.ListView flagsLstIncluded;
        private System.Windows.Forms.TextBox flagsTxtPrefix;
        private System.Windows.Forms.Label flagsLblPrefix;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label genre_lblHelp_RemoveExisting;
        private System.Windows.Forms.Label genre_lblHelp_Prefix;
        private System.Windows.Forms.Label flags_lblHelp_Prefix;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Panel panEditTags;
        private System.Windows.Forms.GroupBox tags_Group;
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
    }
}