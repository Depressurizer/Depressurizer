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
            this.flagsTbl = new System.Windows.Forms.TableLayoutPanel();
            this.flagsCmdCheckAll = new System.Windows.Forms.Button();
            this.flagsCmdUncheckAll = new System.Windows.Forms.Button();
            this.flagsLblInclude = new System.Windows.Forms.Label();
            this.flagsLstIncluded = new System.Windows.Forms.ListView();
            this.flagsTxtPrefix = new System.Windows.Forms.TextBox();
            this.flagsLblPrefix = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.flags_lblHelp_Prefix = new System.Windows.Forms.Label();
            this.genre_lblHelp_Prefix = new System.Windows.Forms.Label();
            this.genre_lblHelp_RemoveExisting = new System.Windows.Forms.Label();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
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
            this.lstAutoCats.Size = new System.Drawing.Size(185, 156);
            this.lstAutoCats.TabIndex = 0;
            this.lstAutoCats.SelectedIndexChanged += new System.EventHandler(this.lstAutoCats_SelectedIndexChanged);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Location = new System.Drawing.Point(6, 232);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(185, 23);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdRename
            // 
            this.cmdRename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRename.Location = new System.Drawing.Point(6, 206);
            this.cmdRename.Name = "cmdRename";
            this.cmdRename.Size = new System.Drawing.Size(185, 23);
            this.cmdRename.TabIndex = 2;
            this.cmdRename.Text = "Rename";
            this.cmdRename.UseVisualStyleBackColor = true;
            this.cmdRename.Click += new System.EventHandler(this.cmdRename_Click);
            // 
            // cmdCreate
            // 
            this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreate.Location = new System.Drawing.Point(6, 180);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(185, 23);
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
            this.grpList.Size = new System.Drawing.Size(198, 262);
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
            this.panEditGenre.Size = new System.Drawing.Size(409, 262);
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
            this.grpEditGenre.Size = new System.Drawing.Size(409, 262);
            this.grpEditGenre.TabIndex = 0;
            this.grpEditGenre.TabStop = false;
            this.grpEditGenre.Text = "Edit Genre AutoCat";
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
            this.genreTblIgnore.Location = new System.Drawing.Point(6, 228);
            this.genreTblIgnore.Name = "genreTblIgnore";
            this.genreTblIgnore.RowCount = 1;
            this.genreTblIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genreTblIgnore.Size = new System.Drawing.Size(401, 30);
            this.genreTblIgnore.TabIndex = 7;
            // 
            // genreCmdUncheckAll
            // 
            this.genreCmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genreCmdUncheckAll.Location = new System.Drawing.Point(203, 3);
            this.genreCmdUncheckAll.Name = "genreCmdUncheckAll";
            this.genreCmdUncheckAll.Size = new System.Drawing.Size(195, 23);
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
            this.genreCmdCheckAll.Size = new System.Drawing.Size(194, 23);
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
            this.genreLstIgnore.Size = new System.Drawing.Size(396, 99);
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
            this.cmdSave.Location = new System.Drawing.Point(472, 268);
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
            this.cmdCancel.Location = new System.Drawing.Point(376, 268);
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
            this.panEditFlags.Size = new System.Drawing.Size(409, 262);
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
            this.flagsGrp.Size = new System.Drawing.Size(409, 262);
            this.flagsGrp.TabIndex = 0;
            this.flagsGrp.TabStop = false;
            this.flagsGrp.Text = "Edit Flag Autocat";
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
            this.flagsTbl.Location = new System.Drawing.Point(3, 228);
            this.flagsTbl.Name = "flagsTbl";
            this.flagsTbl.RowCount = 1;
            this.flagsTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.flagsTbl.Size = new System.Drawing.Size(403, 30);
            this.flagsTbl.TabIndex = 4;
            // 
            // flagsCmdCheckAll
            // 
            this.flagsCmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagsCmdCheckAll.Location = new System.Drawing.Point(3, 3);
            this.flagsCmdCheckAll.Name = "flagsCmdCheckAll";
            this.flagsCmdCheckAll.Size = new System.Drawing.Size(195, 23);
            this.flagsCmdCheckAll.TabIndex = 0;
            this.flagsCmdCheckAll.Text = "Check All";
            this.flagsCmdCheckAll.UseVisualStyleBackColor = true;
            this.flagsCmdCheckAll.Click += new System.EventHandler(this.flagsCmdCheckAll_Click);
            // 
            // flagsCmdUncheckAll
            // 
            this.flagsCmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagsCmdUncheckAll.Location = new System.Drawing.Point(204, 3);
            this.flagsCmdUncheckAll.Name = "flagsCmdUncheckAll";
            this.flagsCmdUncheckAll.Size = new System.Drawing.Size(196, 23);
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
            this.flagsLstIncluded.Size = new System.Drawing.Size(397, 141);
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
            this.splitContainer.Panel2.Controls.Add(this.panEditGenre);
            this.splitContainer.Panel2.Controls.Add(this.panEditFlags);
            this.splitContainer.Panel2MinSize = 250;
            this.splitContainer.Size = new System.Drawing.Size(611, 262);
            this.splitContainer.SplitterDistance = 198;
            this.splitContainer.TabIndex = 7;
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
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // DlgAutoCat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(620, 299);
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
    }
}