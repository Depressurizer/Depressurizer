/*
Copyright 2011, 2012, 2013 Steve Labbe.

This file is part of Depressurizer.

Depressurizer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Depressurizer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/
namespace Depressurizer {
    partial class FormMain {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.mlbAutoCatType = new MaterialSkin.Controls.MaterialListBox();
            this.cmbAutoCatType = new System.Windows.Forms.ComboBox();
            this.mbtnCatDelete = new MaterialSkin.Controls.MaterialRaisedButton();
            this.mchkAdvancedCategories = new MaterialSkin.Controls.MaterialCheckBox();
            this.mbtnCatRename = new MaterialSkin.Controls.MaterialRaisedButton();
            this.mbtnCatAdd = new MaterialSkin.Controls.MaterialRaisedButton();
            this.mbtnAutoCategorize = new MaterialSkin.Controls.MaterialRaisedButton();
            this.mlblHelp = new MaterialSkin.Controls.MaterialLabel();
            this.contextCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextCat_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCat_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCat_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCat_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextCat_RemoveEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdAddCatAndAssign = new System.Windows.Forms.Button();
            this.txtAddCatAndAssign = new System.Windows.Forms.TextBox();
            this.mtxtSearch = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.mbtnSearchClear = new MaterialSkin.Controls.MaterialRaisedButton();
            this.splitGame = new System.Windows.Forms.SplitContainer();
            this.lstGames = new BrightIdeasSoftware.FastObjectListView();
            this.colGameID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colTitle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colCategories = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colGenres = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colFlags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colFavorite = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colHidden = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colReviewScore = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colNumberOfReviews = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colReviewLabel = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colYear = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colAchievements = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colDevelopers = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colPublishers = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colHltbMain = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colHltbExtras = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colHltbCompletionist = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colPlatforms = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextGame = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGame_LaunchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_AddCat = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameAddCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGameAddCat_Create = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_RemCat = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameRemCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGame_SetFav = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameFav = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGameFav_Yes = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameFav_No = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_SetHidden = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameHidden = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGameHidden_Yes = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameHidden_No = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_VisitStore = new System.Windows.Forms.ToolStripMenuItem();
            this.lstMultiCat = new System.Windows.Forms.ListView();
            this.imglistTriState = new System.Windows.Forms.ImageList(this.components);
            this.mlblSearch = new MaterialSkin.Controls.MaterialLabel();
            this.mchkBrowser = new MaterialSkin.Controls.MaterialCheckBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.imglistFilter = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_NewProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_LoadProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_SaveProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_SaveProfileAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_File_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_File_Manual = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Manual_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Profile_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Profile_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile_AutoCats = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_AutocatAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Autocat_List = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_Tools_AutonameAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_RemoveEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.autoModeHelperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Tools_DBEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Tools_SingleCat = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.lstCategories = new Depressurizer.Lib.ExtListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextCat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGame)).BeginInit();
            this.splitGame.Panel1.SuspendLayout();
            this.splitGame.Panel2.SuspendLayout();
            this.splitGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstGames)).BeginInit();
            this.contextGame.SuspendLayout();
            this.contextGameAddCat.SuspendLayout();
            this.contextGameFav.SuspendLayout();
            this.contextGameHidden.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.mlbAutoCatType);
            this.splitContainer.Panel1.Controls.Add(this.cmbAutoCatType);
            this.splitContainer.Panel1.Controls.Add(this.mbtnCatDelete);
            this.splitContainer.Panel1.Controls.Add(this.mchkAdvancedCategories);
            this.splitContainer.Panel1.Controls.Add(this.mbtnCatRename);
            this.splitContainer.Panel1.Controls.Add(this.mbtnCatAdd);
            this.splitContainer.Panel1.Controls.Add(this.mbtnAutoCategorize);
            this.splitContainer.Panel1.Controls.Add(this.mlblHelp);
            this.splitContainer.Panel1.Controls.Add(this.lstCategories);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainer1);
            // 
            // mlbAutoCatType
            // 
            resources.ApplyResources(this.mlbAutoCatType, "mlbAutoCatType");
            this.mlbAutoCatType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mlbAutoCatType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mlbAutoCatType.Depth = 0;
            this.mlbAutoCatType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.mlbAutoCatType.FormattingEnabled = true;
            this.mlbAutoCatType.MouseLocation = new System.Drawing.Point(-1, -1);
            this.mlbAutoCatType.MouseState = MaterialSkin.MouseState.HOVER;
            this.mlbAutoCatType.Name = "mlbAutoCatType";
            // 
            // cmbAutoCatType
            // 
            this.cmbAutoCatType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.cmbAutoCatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutoCatType.ForeColor = System.Drawing.Color.White;
            this.cmbAutoCatType.FormattingEnabled = true;
            resources.ApplyResources(this.cmbAutoCatType, "cmbAutoCatType");
            this.cmbAutoCatType.Name = "cmbAutoCatType";
            // 
            // mbtnCatDelete
            // 
            resources.ApplyResources(this.mbtnCatDelete, "mbtnCatDelete");
            this.mbtnCatDelete.Depth = 0;
            this.mbtnCatDelete.MouseState = MaterialSkin.MouseState.HOVER;
            this.mbtnCatDelete.Name = "mbtnCatDelete";
            this.mbtnCatDelete.UseVisualStyleBackColor = true;
            this.mbtnCatDelete.Click += new System.EventHandler(this.mbtnCatDelete_Click);
            // 
            // mchkAdvancedCategories
            // 
            resources.ApplyResources(this.mchkAdvancedCategories, "mchkAdvancedCategories");
            this.mchkAdvancedCategories.Depth = 0;
            this.mchkAdvancedCategories.MouseLocation = new System.Drawing.Point(-1, -1);
            this.mchkAdvancedCategories.MouseState = MaterialSkin.MouseState.HOVER;
            this.mchkAdvancedCategories.Name = "mchkAdvancedCategories";
            this.mchkAdvancedCategories.Ripple = true;
            this.mchkAdvancedCategories.UseVisualStyleBackColor = true;
            this.mchkAdvancedCategories.CheckedChanged += new System.EventHandler(this.mchkAdvancedCategories_CheckedChanged);
            // 
            // mbtnCatRename
            // 
            resources.ApplyResources(this.mbtnCatRename, "mbtnCatRename");
            this.mbtnCatRename.Depth = 0;
            this.mbtnCatRename.MouseState = MaterialSkin.MouseState.HOVER;
            this.mbtnCatRename.Name = "mbtnCatRename";
            this.mbtnCatRename.UseVisualStyleBackColor = true;
            this.mbtnCatRename.Click += new System.EventHandler(this.mbtnCatRename_Click);
            // 
            // mbtnCatAdd
            // 
            resources.ApplyResources(this.mbtnCatAdd, "mbtnCatAdd");
            this.mbtnCatAdd.Depth = 0;
            this.mbtnCatAdd.MouseState = MaterialSkin.MouseState.HOVER;
            this.mbtnCatAdd.Name = "mbtnCatAdd";
            this.mbtnCatAdd.UseVisualStyleBackColor = true;
            this.mbtnCatAdd.Click += new System.EventHandler(this.mbtnCatAdd_Click);
            // 
            // mbtnAutoCategorize
            // 
            this.mbtnAutoCategorize.Depth = 0;
            resources.ApplyResources(this.mbtnAutoCategorize, "mbtnAutoCategorize");
            this.mbtnAutoCategorize.MouseState = MaterialSkin.MouseState.HOVER;
            this.mbtnAutoCategorize.Name = "mbtnAutoCategorize";
            this.ttHelp.SetToolTip(this.mbtnAutoCategorize, resources.GetString("mbtnAutoCategorize.ToolTip"));
            this.mbtnAutoCategorize.UseVisualStyleBackColor = true;
            this.mbtnAutoCategorize.Click += new System.EventHandler(this.mbtnAutoCategorize_Click);
            // 
            // mlblHelp
            // 
            resources.ApplyResources(this.mlblHelp, "mlblHelp");
            this.mlblHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlblHelp.Depth = 0;
            this.mlblHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mlblHelp.MouseState = MaterialSkin.MouseState.HOVER;
            this.mlblHelp.Name = "mlblHelp";
            // 
            // contextCat
            // 
            this.contextCat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextCat_Add,
            this.contextCat_Rename,
            this.contextCat_Delete,
            this.contextCat_Sep1,
            this.contextCat_RemoveEmpty});
            this.contextCat.Name = "contextCat";
            this.contextCat.ShowImageMargin = false;
            resources.ApplyResources(this.contextCat, "contextCat");
            this.contextCat.Opening += new System.ComponentModel.CancelEventHandler(this.contextCat_Opening);
            // 
            // contextCat_Add
            // 
            this.contextCat_Add.Name = "contextCat_Add";
            resources.ApplyResources(this.contextCat_Add, "contextCat_Add");
            this.contextCat_Add.Click += new System.EventHandler(this.mbtnCatAdd_Click);
            // 
            // contextCat_Rename
            // 
            this.contextCat_Rename.Name = "contextCat_Rename";
            resources.ApplyResources(this.contextCat_Rename, "contextCat_Rename");
            this.contextCat_Rename.Click += new System.EventHandler(this.mbtnCatRename_Click);
            // 
            // contextCat_Delete
            // 
            this.contextCat_Delete.Name = "contextCat_Delete";
            resources.ApplyResources(this.contextCat_Delete, "contextCat_Delete");
            this.contextCat_Delete.Click += new System.EventHandler(this.mbtnCatDelete_Click);
            // 
            // contextCat_Sep1
            // 
            this.contextCat_Sep1.Name = "contextCat_Sep1";
            resources.ApplyResources(this.contextCat_Sep1, "contextCat_Sep1");
            // 
            // contextCat_RemoveEmpty
            // 
            this.contextCat_RemoveEmpty.Name = "contextCat_RemoveEmpty";
            resources.ApplyResources(this.contextCat_RemoveEmpty, "contextCat_RemoveEmpty");
            this.contextCat_RemoveEmpty.Click += new System.EventHandler(this.contectCat_RemoveEmpty_Click);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmdAddCatAndAssign);
            this.splitContainer1.Panel1.Controls.Add(this.txtAddCatAndAssign);
            this.splitContainer1.Panel1.Controls.Add(this.mtxtSearch);
            this.splitContainer1.Panel1.Controls.Add(this.mbtnSearchClear);
            this.splitContainer1.Panel1.Controls.Add(this.splitGame);
            this.splitContainer1.Panel1.Controls.Add(this.mlblSearch);
            this.splitContainer1.Panel1.Controls.Add(this.mchkBrowser);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer1.Panel2Collapsed = true;
            // 
            // cmdAddCatAndAssign
            // 
            resources.ApplyResources(this.cmdAddCatAndAssign, "cmdAddCatAndAssign");
            this.cmdAddCatAndAssign.Name = "cmdAddCatAndAssign";
            this.cmdAddCatAndAssign.UseVisualStyleBackColor = true;
            this.cmdAddCatAndAssign.Click += new System.EventHandler(this.cmdAddCatAndAssign_Click);
            // 
            // txtAddCatAndAssign
            // 
            resources.ApplyResources(this.txtAddCatAndAssign, "txtAddCatAndAssign");
            this.txtAddCatAndAssign.Name = "txtAddCatAndAssign";
            // 
            // mtxtSearch
            // 
            this.mtxtSearch.Depth = 0;
            this.mtxtSearch.Hint = "";
            resources.ApplyResources(this.mtxtSearch, "mtxtSearch");
            this.mtxtSearch.MaxLength = 32767;
            this.mtxtSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.mtxtSearch.Name = "mtxtSearch";
            this.mtxtSearch.PasswordChar = '\0';
            this.mtxtSearch.SelectedText = "";
            this.mtxtSearch.SelectionLength = 0;
            this.mtxtSearch.SelectionStart = 0;
            this.mtxtSearch.TabStop = false;
            this.mtxtSearch.UseSystemPasswordChar = false;
            this.mtxtSearch.TextChanged += new System.EventHandler(this.mtxtSearch_TextChanged);
            // 
            // mbtnSearchClear
            // 
            this.mbtnSearchClear.Depth = 0;
            resources.ApplyResources(this.mbtnSearchClear, "mbtnSearchClear");
            this.mbtnSearchClear.MouseState = MaterialSkin.MouseState.HOVER;
            this.mbtnSearchClear.Name = "mbtnSearchClear";
            this.mbtnSearchClear.UseVisualStyleBackColor = true;
            this.mbtnSearchClear.Click += new System.EventHandler(this.mbtnSearchClear_Click);
            // 
            // splitGame
            // 
            resources.ApplyResources(this.splitGame, "splitGame");
            this.splitGame.Name = "splitGame";
            // 
            // splitGame.Panel1
            // 
            this.splitGame.Panel1.Controls.Add(this.lstGames);
            // 
            // splitGame.Panel2
            // 
            this.splitGame.Panel2.Controls.Add(this.lstMultiCat);
            // 
            // lstGames
            // 
            this.lstGames.AllColumns.Add(this.colGameID);
            this.lstGames.AllColumns.Add(this.colTitle);
            this.lstGames.AllColumns.Add(this.colCategories);
            this.lstGames.AllColumns.Add(this.colGenres);
            this.lstGames.AllColumns.Add(this.colFlags);
            this.lstGames.AllColumns.Add(this.colTags);
            this.lstGames.AllColumns.Add(this.colFavorite);
            this.lstGames.AllColumns.Add(this.colHidden);
            this.lstGames.AllColumns.Add(this.colReviewScore);
            this.lstGames.AllColumns.Add(this.colNumberOfReviews);
            this.lstGames.AllColumns.Add(this.colReviewLabel);
            this.lstGames.AllColumns.Add(this.colYear);
            this.lstGames.AllColumns.Add(this.colAchievements);
            this.lstGames.AllColumns.Add(this.colDevelopers);
            this.lstGames.AllColumns.Add(this.colPublishers);
            this.lstGames.AllColumns.Add(this.colHltbMain);
            this.lstGames.AllColumns.Add(this.colHltbExtras);
            this.lstGames.AllColumns.Add(this.colHltbCompletionist);
            this.lstGames.AllColumns.Add(this.colPlatforms);
            this.lstGames.AllowColumnReorder = true;
            this.lstGames.CellEditUseWholeCell = false;
            this.lstGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colGameID,
            this.colTitle,
            this.colCategories,
            this.colTags,
            this.colReviewScore,
            this.colYear});
            this.lstGames.ContextMenuStrip = this.contextGame;
            this.lstGames.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.lstGames, "lstGames");
            this.lstGames.FullRowSelect = true;
            this.lstGames.HideSelection = false;
            this.lstGames.HighlightBackgroundColor = System.Drawing.Color.Gray;
            this.lstGames.HighlightForegroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lstGames.Name = "lstGames";
            this.lstGames.RowHeight = 45;
            this.lstGames.SelectedBackColor = System.Drawing.Color.Gray;
            this.lstGames.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lstGames.ShowGroups = false;
            this.lstGames.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstGames.UseCellFormatEvents = true;
            this.lstGames.UseCompatibleStateImageBehavior = false;
            this.lstGames.UseFilterIndicator = true;
            this.lstGames.UseFiltering = true;
            this.lstGames.View = System.Windows.Forms.View.Details;
            this.lstGames.VirtualMode = true;
            this.lstGames.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.lstGames_FormatCell);
            this.lstGames.ItemsChanged += new System.EventHandler<BrightIdeasSoftware.ItemsChangedEventArgs>(this.lstGames_ItemsChanged);
            this.lstGames.SelectionChanged += new System.EventHandler(this.lstGames_SelectionChanged);
            this.lstGames.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstGames_ItemDrag);
            this.lstGames.SelectedIndexChanged += new System.EventHandler(this.lstGames_SelectedIndexChanged);
            this.lstGames.DoubleClick += new System.EventHandler(this.lstGames_DoubleClick);
            this.lstGames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstGames_KeyDown);
            // 
            // colGameID
            // 
            this.colGameID.AspectName = "Id";
            this.colGameID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colGameID.MaximumWidth = 120;
            this.colGameID.MinimumWidth = 120;
            this.colGameID.Tag = "colGameID";
            resources.ApplyResources(this.colGameID, "colGameID");
            this.colGameID.UseFiltering = false;
            // 
            // colTitle
            // 
            this.colTitle.AspectName = "Name";
            this.colTitle.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTitle.Tag = "colTitle";
            resources.ApplyResources(this.colTitle, "colTitle");
            this.colTitle.UseInitialLetterForGroup = true;
            // 
            // colCategories
            // 
            this.colCategories.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colCategories.Tag = "colCategories";
            resources.ApplyResources(this.colCategories, "colCategories");
            // 
            // colGenres
            // 
            resources.ApplyResources(this.colGenres, "colGenres");
            this.colGenres.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colGenres.IsVisible = false;
            this.colGenres.Tag = "colGenres";
            // 
            // colFlags
            // 
            resources.ApplyResources(this.colFlags, "colFlags");
            this.colFlags.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFlags.IsVisible = false;
            this.colFlags.Tag = "colFlags";
            // 
            // colTags
            // 
            this.colTags.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTags.Tag = "colTags";
            resources.ApplyResources(this.colTags, "colTags");
            // 
            // colFavorite
            // 
            resources.ApplyResources(this.colFavorite, "colFavorite");
            this.colFavorite.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFavorite.IsVisible = false;
            this.colFavorite.Tag = "colFavorite";
            // 
            // colHidden
            // 
            resources.ApplyResources(this.colHidden, "colHidden");
            this.colHidden.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHidden.IsVisible = false;
            this.colHidden.Tag = "colHidden";
            // 
            // colReviewScore
            // 
            this.colReviewScore.AspectName = "";
            this.colReviewScore.AspectToStringFormat = "";
            this.colReviewScore.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colReviewScore.Tag = "colReviewScore";
            resources.ApplyResources(this.colReviewScore, "colReviewScore");
            this.colReviewScore.UseFiltering = false;
            // 
            // colNumberOfReviews
            // 
            this.colNumberOfReviews.AspectName = "";
            resources.ApplyResources(this.colNumberOfReviews, "colNumberOfReviews");
            this.colNumberOfReviews.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colNumberOfReviews.IsVisible = false;
            this.colNumberOfReviews.Tag = "colNumberOfReviews";
            this.colNumberOfReviews.UseFiltering = false;
            // 
            // colReviewLabel
            // 
            this.colReviewLabel.AspectName = "";
            resources.ApplyResources(this.colReviewLabel, "colReviewLabel");
            this.colReviewLabel.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colReviewLabel.IsVisible = false;
            this.colReviewLabel.Tag = "colReviewLabel";
            // 
            // colYear
            // 
            this.colYear.AspectName = "";
            this.colYear.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colYear.Tag = "colYear";
            resources.ApplyResources(this.colYear, "colYear");
            // 
            // colAchievements
            // 
            this.colAchievements.AspectName = "";
            resources.ApplyResources(this.colAchievements, "colAchievements");
            this.colAchievements.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colAchievements.IsVisible = false;
            this.colAchievements.Tag = "colAchievements";
            this.colAchievements.UseFiltering = false;
            // 
            // colDevelopers
            // 
            resources.ApplyResources(this.colDevelopers, "colDevelopers");
            this.colDevelopers.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDevelopers.IsVisible = false;
            this.colDevelopers.Tag = "colDevelopers";
            this.colDevelopers.UseInitialLetterForGroup = true;
            // 
            // colPublishers
            // 
            resources.ApplyResources(this.colPublishers, "colPublishers");
            this.colPublishers.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colPublishers.IsVisible = false;
            this.colPublishers.Tag = "colPublishers";
            this.colPublishers.UseInitialLetterForGroup = true;
            // 
            // colHltbMain
            // 
            this.colHltbMain.AspectName = "";
            resources.ApplyResources(this.colHltbMain, "colHltbMain");
            this.colHltbMain.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHltbMain.IsVisible = false;
            this.colHltbMain.Tag = "colHltbMain";
            this.colHltbMain.UseFiltering = false;
            // 
            // colHltbExtras
            // 
            this.colHltbExtras.AspectName = "";
            resources.ApplyResources(this.colHltbExtras, "colHltbExtras");
            this.colHltbExtras.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHltbExtras.IsVisible = false;
            this.colHltbExtras.Tag = "colHltbExtras";
            this.colHltbExtras.UseFiltering = false;
            // 
            // colHltbCompletionist
            // 
            this.colHltbCompletionist.AspectName = "";
            resources.ApplyResources(this.colHltbCompletionist, "colHltbCompletionist");
            this.colHltbCompletionist.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colHltbCompletionist.IsVisible = false;
            this.colHltbCompletionist.Tag = "colHltbCompletionist";
            this.colHltbCompletionist.UseFiltering = false;
            // 
            // colPlatforms
            // 
            resources.ApplyResources(this.colPlatforms, "colPlatforms");
            this.colPlatforms.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colPlatforms.IsVisible = false;
            this.colPlatforms.Tag = "colPlatforms";
            // 
            // contextGame
            // 
            this.contextGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGame_LaunchGame,
            this.contextGame_Sep1,
            this.contextGame_Add,
            this.contextGame_Edit,
            this.contextGame_Remove,
            this.contextGame_Sep2,
            this.contextGame_AddCat,
            this.contextGame_RemCat,
            this.contextGame_SetFav,
            this.contextGame_SetHidden,
            this.contextGame_Sep3,
            this.contextGame_VisitStore});
            this.contextGame.Name = "contextGame";
            this.contextGame.ShowImageMargin = false;
            resources.ApplyResources(this.contextGame, "contextGame");
            this.contextGame.Opening += new System.ComponentModel.CancelEventHandler(this.contextGame_Opening);
            // 
            // contextGame_LaunchGame
            // 
            this.contextGame_LaunchGame.Name = "contextGame_LaunchGame";
            resources.ApplyResources(this.contextGame_LaunchGame, "contextGame_LaunchGame");
            this.contextGame_LaunchGame.Click += new System.EventHandler(this.cmdGameLaunch_Click);
            // 
            // contextGame_Sep1
            // 
            this.contextGame_Sep1.Name = "contextGame_Sep1";
            resources.ApplyResources(this.contextGame_Sep1, "contextGame_Sep1");
            // 
            // contextGame_Add
            // 
            this.contextGame_Add.Name = "contextGame_Add";
            resources.ApplyResources(this.contextGame_Add, "contextGame_Add");
            this.contextGame_Add.Click += new System.EventHandler(this.cmdGameAdd_Click);
            // 
            // contextGame_Edit
            // 
            this.contextGame_Edit.Name = "contextGame_Edit";
            resources.ApplyResources(this.contextGame_Edit, "contextGame_Edit");
            this.contextGame_Edit.Click += new System.EventHandler(this.cmdGameEdit_Click);
            // 
            // contextGame_Remove
            // 
            this.contextGame_Remove.Name = "contextGame_Remove";
            resources.ApplyResources(this.contextGame_Remove, "contextGame_Remove");
            this.contextGame_Remove.Click += new System.EventHandler(this.cmdGameRemove_Click);
            // 
            // contextGame_Sep2
            // 
            this.contextGame_Sep2.Name = "contextGame_Sep2";
            resources.ApplyResources(this.contextGame_Sep2, "contextGame_Sep2");
            // 
            // contextGame_AddCat
            // 
            this.contextGame_AddCat.DropDown = this.contextGameAddCat;
            this.contextGame_AddCat.Name = "contextGame_AddCat";
            resources.ApplyResources(this.contextGame_AddCat, "contextGame_AddCat");
            // 
            // contextGameAddCat
            // 
            this.contextGameAddCat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGameAddCat_Create});
            this.contextGameAddCat.Name = "contextGameCat";
            this.contextGameAddCat.OwnerItem = this.contextGame_AddCat;
            this.contextGameAddCat.ShowImageMargin = false;
            resources.ApplyResources(this.contextGameAddCat, "contextGameAddCat");
            // 
            // contextGameAddCat_Create
            // 
            this.contextGameAddCat_Create.Name = "contextGameAddCat_Create";
            resources.ApplyResources(this.contextGameAddCat_Create, "contextGameAddCat_Create");
            this.contextGameAddCat_Create.Click += new System.EventHandler(this.contextGameAddCat_Create_Click);
            // 
            // contextGame_RemCat
            // 
            this.contextGame_RemCat.DropDown = this.contextGameRemCat;
            this.contextGame_RemCat.Name = "contextGame_RemCat";
            resources.ApplyResources(this.contextGame_RemCat, "contextGame_RemCat");
            // 
            // contextGameRemCat
            // 
            this.contextGameRemCat.Name = "contextGameRemCat";
            this.contextGameRemCat.OwnerItem = this.contextGame_RemCat;
            this.contextGameRemCat.ShowImageMargin = false;
            resources.ApplyResources(this.contextGameRemCat, "contextGameRemCat");
            // 
            // contextGame_SetFav
            // 
            this.contextGame_SetFav.DropDown = this.contextGameFav;
            this.contextGame_SetFav.Name = "contextGame_SetFav";
            resources.ApplyResources(this.contextGame_SetFav, "contextGame_SetFav");
            // 
            // contextGameFav
            // 
            this.contextGameFav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGameFav_Yes,
            this.contextGameFav_No});
            this.contextGameFav.Name = "contextGameFav";
            this.contextGameFav.OwnerItem = this.contextGame_SetFav;
            this.contextGameFav.ShowImageMargin = false;
            resources.ApplyResources(this.contextGameFav, "contextGameFav");
            // 
            // contextGameFav_Yes
            // 
            this.contextGameFav_Yes.Name = "contextGameFav_Yes";
            resources.ApplyResources(this.contextGameFav_Yes, "contextGameFav_Yes");
            this.contextGameFav_Yes.Click += new System.EventHandler(this.contextGame_SetFav_Yes_Click);
            // 
            // contextGameFav_No
            // 
            this.contextGameFav_No.Name = "contextGameFav_No";
            resources.ApplyResources(this.contextGameFav_No, "contextGameFav_No");
            this.contextGameFav_No.Click += new System.EventHandler(this.contextGame_SetFav_No_Click);
            // 
            // contextGame_SetHidden
            // 
            this.contextGame_SetHidden.DropDown = this.contextGameHidden;
            this.contextGame_SetHidden.Name = "contextGame_SetHidden";
            resources.ApplyResources(this.contextGame_SetHidden, "contextGame_SetHidden");
            // 
            // contextGameHidden
            // 
            this.contextGameHidden.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGameHidden_Yes,
            this.contextGameHidden_No});
            this.contextGameHidden.Name = "contextGameFav";
            this.contextGameHidden.OwnerItem = this.contextGame_SetHidden;
            this.contextGameHidden.ShowImageMargin = false;
            resources.ApplyResources(this.contextGameHidden, "contextGameHidden");
            // 
            // contextGameHidden_Yes
            // 
            this.contextGameHidden_Yes.Name = "contextGameHidden_Yes";
            resources.ApplyResources(this.contextGameHidden_Yes, "contextGameHidden_Yes");
            this.contextGameHidden_Yes.Click += new System.EventHandler(this.contextGameHidden_Yes_Click);
            // 
            // contextGameHidden_No
            // 
            this.contextGameHidden_No.Name = "contextGameHidden_No";
            resources.ApplyResources(this.contextGameHidden_No, "contextGameHidden_No");
            this.contextGameHidden_No.Click += new System.EventHandler(this.contextGameHidden_No_Click);
            // 
            // contextGame_Sep3
            // 
            this.contextGame_Sep3.Name = "contextGame_Sep3";
            resources.ApplyResources(this.contextGame_Sep3, "contextGame_Sep3");
            // 
            // contextGame_VisitStore
            // 
            this.contextGame_VisitStore.Name = "contextGame_VisitStore";
            resources.ApplyResources(this.contextGame_VisitStore, "contextGame_VisitStore");
            this.contextGame_VisitStore.Click += new System.EventHandler(this.contextGame_VisitStore_Click);
            // 
            // lstMultiCat
            // 
            this.lstMultiCat.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstMultiCat.AutoArrange = false;
            this.lstMultiCat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            resources.ApplyResources(this.lstMultiCat, "lstMultiCat");
            this.lstMultiCat.ForeColor = System.Drawing.Color.White;
            this.lstMultiCat.MultiSelect = false;
            this.lstMultiCat.Name = "lstMultiCat";
            this.lstMultiCat.StateImageList = this.imglistTriState;
            this.lstMultiCat.UseCompatibleStateImageBehavior = false;
            this.lstMultiCat.View = System.Windows.Forms.View.List;
            this.lstMultiCat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstMultiCat_KeyPress);
            this.lstMultiCat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstMultiCat_MouseDown);
            // 
            // imglistTriState
            // 
            this.imglistTriState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistTriState.ImageStream")));
            this.imglistTriState.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistTriState.Images.SetKeyName(0, "tscbUnchecked.png");
            this.imglistTriState.Images.SetKeyName(1, "tscbChecked.png");
            this.imglistTriState.Images.SetKeyName(2, "tscbIndeterminate.png");
            // 
            // mlblSearch
            // 
            resources.ApplyResources(this.mlblSearch, "mlblSearch");
            this.mlblSearch.Depth = 0;
            this.mlblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mlblSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.mlblSearch.Name = "mlblSearch";
            // 
            // mchkBrowser
            // 
            resources.ApplyResources(this.mchkBrowser, "mchkBrowser");
            this.mchkBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.mchkBrowser.Depth = 0;
            this.mchkBrowser.MouseLocation = new System.Drawing.Point(-1, -1);
            this.mchkBrowser.MouseState = MaterialSkin.MouseState.HOVER;
            this.mchkBrowser.Name = "mchkBrowser";
            this.mchkBrowser.Ripple = true;
            this.mchkBrowser.UseVisualStyleBackColor = false;
            this.mchkBrowser.CheckedChanged += new System.EventHandler(this.mchkBrowser_CheckedChanged);
            // 
            // webBrowser1
            // 
            resources.ApplyResources(this.webBrowser1, "webBrowser1");
            this.webBrowser1.Name = "webBrowser1";
            // 
            // imglistFilter
            // 
            this.imglistFilter.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistFilter.ImageStream")));
            this.imglistFilter.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistFilter.Images.SetKeyName(0, "filterAny");
            this.imglistFilter.Images.SetKeyName(1, "filterAll");
            this.imglistFilter.Images.SetKeyName(2, "filterNone");
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File,
            this.menu_Profile,
            this.menu_Tools,
            this.menu_About});
            this.menuStrip.Name = "menuStrip";
            // 
            // menu_File
            // 
            this.menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File_NewProfile,
            this.menu_File_LoadProfile,
            this.menu_File_SaveProfile,
            this.menu_File_SaveProfileAs,
            this.menu_File_Sep1,
            this.menu_File_Close,
            this.menu_File_Sep2,
            this.menu_File_Manual,
            this.menu_File_Sep3,
            this.menu_File_Exit});
            this.menu_File.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(173)))), ((int)(((byte)(175)))));
            this.menu_File.Name = "menu_File";
            resources.ApplyResources(this.menu_File, "menu_File");
            // 
            // menu_File_NewProfile
            // 
            this.menu_File_NewProfile.Name = "menu_File_NewProfile";
            resources.ApplyResources(this.menu_File_NewProfile, "menu_File_NewProfile");
            this.menu_File_NewProfile.Click += new System.EventHandler(this.menu_File_NewProfile_Click);
            // 
            // menu_File_LoadProfile
            // 
            this.menu_File_LoadProfile.Name = "menu_File_LoadProfile";
            resources.ApplyResources(this.menu_File_LoadProfile, "menu_File_LoadProfile");
            this.menu_File_LoadProfile.Click += new System.EventHandler(this.menu_File_LoadProfile_Click);
            // 
            // menu_File_SaveProfile
            // 
            this.menu_File_SaveProfile.Name = "menu_File_SaveProfile";
            resources.ApplyResources(this.menu_File_SaveProfile, "menu_File_SaveProfile");
            this.menu_File_SaveProfile.Click += new System.EventHandler(this.menu_File_SaveProfile_Click);
            // 
            // menu_File_SaveProfileAs
            // 
            this.menu_File_SaveProfileAs.Name = "menu_File_SaveProfileAs";
            resources.ApplyResources(this.menu_File_SaveProfileAs, "menu_File_SaveProfileAs");
            this.menu_File_SaveProfileAs.Click += new System.EventHandler(this.menu_File_SaveProfileAs_Click);
            // 
            // menu_File_Sep1
            // 
            this.menu_File_Sep1.Name = "menu_File_Sep1";
            resources.ApplyResources(this.menu_File_Sep1, "menu_File_Sep1");
            // 
            // menu_File_Close
            // 
            this.menu_File_Close.Name = "menu_File_Close";
            resources.ApplyResources(this.menu_File_Close, "menu_File_Close");
            this.menu_File_Close.Click += new System.EventHandler(this.menu_File_Close_Click);
            // 
            // menu_File_Sep2
            // 
            this.menu_File_Sep2.Name = "menu_File_Sep2";
            resources.ApplyResources(this.menu_File_Sep2, "menu_File_Sep2");
            // 
            // menu_File_Manual
            // 
            this.menu_File_Manual.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File_Manual_Export});
            this.menu_File_Manual.Name = "menu_File_Manual";
            resources.ApplyResources(this.menu_File_Manual, "menu_File_Manual");
            // 
            // menu_File_Manual_Export
            // 
            this.menu_File_Manual_Export.Name = "menu_File_Manual_Export";
            resources.ApplyResources(this.menu_File_Manual_Export, "menu_File_Manual_Export");
            this.menu_File_Manual_Export.Click += new System.EventHandler(this.menu_File_Manual_Export_Click);
            // 
            // menu_File_Sep3
            // 
            this.menu_File_Sep3.Name = "menu_File_Sep3";
            resources.ApplyResources(this.menu_File_Sep3, "menu_File_Sep3");
            // 
            // menu_File_Exit
            // 
            this.menu_File_Exit.Name = "menu_File_Exit";
            resources.ApplyResources(this.menu_File_Exit, "menu_File_Exit");
            this.menu_File_Exit.Click += new System.EventHandler(this.menu_File_Exit_Click);
            // 
            // menu_Profile
            // 
            this.menu_Profile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Profile_Update,
            this.menu_Profile_Import,
            this.menu_Profile_Sep1,
            this.menu_Profile_Export,
            this.menu_Profile_Sep2,
            this.menu_Profile_Edit,
            this.menu_Profile_AutoCats});
            this.menu_Profile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(173)))), ((int)(((byte)(175)))));
            this.menu_Profile.Name = "menu_Profile";
            resources.ApplyResources(this.menu_Profile, "menu_Profile");
            // 
            // menu_Profile_Update
            // 
            this.menu_Profile_Update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.menu_Profile_Update.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(168)))), ((int)(((byte)(157)))));
            this.menu_Profile_Update.Name = "menu_Profile_Update";
            resources.ApplyResources(this.menu_Profile_Update, "menu_Profile_Update");
            this.menu_Profile_Update.Click += new System.EventHandler(this.menu_Profile_Update_Click);
            // 
            // menu_Profile_Import
            // 
            this.menu_Profile_Import.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.menu_Profile_Import.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(168)))), ((int)(((byte)(157)))));
            this.menu_Profile_Import.Name = "menu_Profile_Import";
            resources.ApplyResources(this.menu_Profile_Import, "menu_Profile_Import");
            this.menu_Profile_Import.Click += new System.EventHandler(this.menu_Profile_Import_Click);
            // 
            // menu_Profile_Sep1
            // 
            this.menu_Profile_Sep1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.menu_Profile_Sep1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(168)))), ((int)(((byte)(157)))));
            this.menu_Profile_Sep1.Name = "menu_Profile_Sep1";
            resources.ApplyResources(this.menu_Profile_Sep1, "menu_Profile_Sep1");
            // 
            // menu_Profile_Export
            // 
            this.menu_Profile_Export.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.menu_Profile_Export.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(168)))), ((int)(((byte)(157)))));
            this.menu_Profile_Export.Name = "menu_Profile_Export";
            resources.ApplyResources(this.menu_Profile_Export, "menu_Profile_Export");
            this.menu_Profile_Export.Click += new System.EventHandler(this.menu_Profile_Export_Click);
            // 
            // menu_Profile_Sep2
            // 
            this.menu_Profile_Sep2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.menu_Profile_Sep2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(168)))), ((int)(((byte)(157)))));
            this.menu_Profile_Sep2.Name = "menu_Profile_Sep2";
            resources.ApplyResources(this.menu_Profile_Sep2, "menu_Profile_Sep2");
            // 
            // menu_Profile_Edit
            // 
            this.menu_Profile_Edit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.menu_Profile_Edit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(168)))), ((int)(((byte)(157)))));
            this.menu_Profile_Edit.Name = "menu_Profile_Edit";
            resources.ApplyResources(this.menu_Profile_Edit, "menu_Profile_Edit");
            this.menu_Profile_Edit.Click += new System.EventHandler(this.menu_Profile_Edit_Click);
            // 
            // menu_Profile_AutoCats
            // 
            this.menu_Profile_AutoCats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.menu_Profile_AutoCats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(168)))), ((int)(((byte)(157)))));
            this.menu_Profile_AutoCats.Name = "menu_Profile_AutoCats";
            resources.ApplyResources(this.menu_Profile_AutoCats, "menu_Profile_AutoCats");
            this.menu_Profile_AutoCats.Click += new System.EventHandler(this.menu_Profile_EditAutoCats_Click);
            // 
            // menu_Tools
            // 
            this.menu_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Tools_AutocatAll,
            this.menu_Tools_AutonameAll,
            this.menu_Tools_RemoveEmpty,
            this.menu_Tools_Sep1,
            this.autoModeHelperToolStripMenuItem,
            this.menu_Tools_Sep2,
            this.menu_Tools_DBEdit,
            this.menu_Tools_Sep3,
            this.menu_Tools_SingleCat,
            this.menu_Tools_Settings});
            this.menu_Tools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(173)))), ((int)(((byte)(175)))));
            this.menu_Tools.Name = "menu_Tools";
            resources.ApplyResources(this.menu_Tools, "menu_Tools");
            // 
            // menu_Tools_AutocatAll
            // 
            this.menu_Tools_AutocatAll.DropDown = this.menu_Tools_Autocat_List;
            this.menu_Tools_AutocatAll.Name = "menu_Tools_AutocatAll";
            resources.ApplyResources(this.menu_Tools_AutocatAll, "menu_Tools_AutocatAll");
            // 
            // menu_Tools_Autocat_List
            // 
            this.menu_Tools_Autocat_List.Name = "menuToolsAutocat_List";
            this.menu_Tools_Autocat_List.OwnerItem = this.menu_Tools_AutocatAll;
            resources.ApplyResources(this.menu_Tools_Autocat_List, "menu_Tools_Autocat_List");
            // 
            // menu_Tools_AutonameAll
            // 
            this.menu_Tools_AutonameAll.Name = "menu_Tools_AutonameAll";
            resources.ApplyResources(this.menu_Tools_AutonameAll, "menu_Tools_AutonameAll");
            this.menu_Tools_AutonameAll.Click += new System.EventHandler(this.menu_Tools_AutonameAll_Click);
            // 
            // menu_Tools_RemoveEmpty
            // 
            this.menu_Tools_RemoveEmpty.Name = "menu_Tools_RemoveEmpty";
            resources.ApplyResources(this.menu_Tools_RemoveEmpty, "menu_Tools_RemoveEmpty");
            this.menu_Tools_RemoveEmpty.Click += new System.EventHandler(this.menu_Tools_RemoveEmpty_Click);
            // 
            // menu_Tools_Sep1
            // 
            this.menu_Tools_Sep1.Name = "menu_Tools_Sep1";
            resources.ApplyResources(this.menu_Tools_Sep1, "menu_Tools_Sep1");
            // 
            // autoModeHelperToolStripMenuItem
            // 
            this.autoModeHelperToolStripMenuItem.Name = "autoModeHelperToolStripMenuItem";
            resources.ApplyResources(this.autoModeHelperToolStripMenuItem, "autoModeHelperToolStripMenuItem");
            this.autoModeHelperToolStripMenuItem.Click += new System.EventHandler(this.autoModeHelperToolStripMenuItem_Click);
            // 
            // menu_Tools_Sep2
            // 
            this.menu_Tools_Sep2.Name = "menu_Tools_Sep2";
            resources.ApplyResources(this.menu_Tools_Sep2, "menu_Tools_Sep2");
            // 
            // menu_Tools_DBEdit
            // 
            this.menu_Tools_DBEdit.Name = "menu_Tools_DBEdit";
            resources.ApplyResources(this.menu_Tools_DBEdit, "menu_Tools_DBEdit");
            this.menu_Tools_DBEdit.Click += new System.EventHandler(this.menu_Tools_DBEdit_Click);
            // 
            // menu_Tools_Sep3
            // 
            this.menu_Tools_Sep3.Name = "menu_Tools_Sep3";
            resources.ApplyResources(this.menu_Tools_Sep3, "menu_Tools_Sep3");
            // 
            // menu_Tools_SingleCat
            // 
            this.menu_Tools_SingleCat.Name = "menu_Tools_SingleCat";
            resources.ApplyResources(this.menu_Tools_SingleCat, "menu_Tools_SingleCat");
            this.menu_Tools_SingleCat.Click += new System.EventHandler(this.menu_Tools_SingleCat_Click);
            // 
            // menu_Tools_Settings
            // 
            this.menu_Tools_Settings.Name = "menu_Tools_Settings";
            resources.ApplyResources(this.menu_Tools_Settings, "menu_Tools_Settings");
            this.menu_Tools_Settings.Click += new System.EventHandler(this.menu_Tools_Settings_Click);
            // 
            // menu_About
            // 
            this.menu_About.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menu_About.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(173)))), ((int)(((byte)(175)))));
            this.menu_About.Name = "menu_About";
            resources.ApplyResources(this.menu_About, "menu_About");
            this.menu_About.Click += new System.EventHandler(this.menu_About_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMsg,
            this.statusSelection});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // statusMsg
            // 
            this.statusMsg.Name = "statusMsg";
            resources.ApplyResources(this.statusMsg, "statusMsg");
            this.statusMsg.Spring = true;
            // 
            // statusSelection
            // 
            resources.ApplyResources(this.statusSelection, "statusSelection");
            this.statusSelection.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusSelection.Name = "statusSelection";
            // 
            // lstCategories
            // 
            this.lstCategories.AllowDrop = true;
            resources.ApplyResources(this.lstCategories, "lstCategories");
            this.lstCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCategories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstCategories.ContextMenuStrip = this.contextCat;
            this.lstCategories.FullRowSelect = true;
            this.lstCategories.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstCategories.HideSelection = false;
            this.lstCategories.Name = "lstCategories";
            this.lstCategories.ShowGroups = false;
            this.lstCategories.UseCompatibleStateImageBehavior = false;
            this.lstCategories.View = System.Windows.Forms.View.Details;
            this.lstCategories.SelectionChanged += new System.EventHandler(this.lstCategories_SelectedIndexChanged);
            this.lstCategories.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstCategories_DragDrop);
            this.lstCategories.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstCategories_DragEnter);
            this.lstCategories.DragOver += new System.Windows.Forms.DragEventHandler(this.lstCategories_DragOver);
            this.lstCategories.DragLeave += new System.EventHandler(this.lstCategories_DragLeave);
            this.lstCategories.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstCategories_KeyDown);
            this.lstCategories.Layout += new System.Windows.Forms.LayoutEventHandler(this.lstCategories_Layout);
            this.lstCategories.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstCategories_MouseDown);
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(44)))));
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextCat.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitGame.Panel1.ResumeLayout(false);
            this.splitGame.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGame)).EndInit();
            this.splitGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstGames)).EndInit();
            this.contextGame.ResumeLayout(false);
            this.contextGameAddCat.ResumeLayout(false);
            this.contextGameFav.ResumeLayout(false);
            this.contextGameHidden.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menu_File;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Exit;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep2;
        private Lib.ExtListView lstCategories;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusMsg;
        private System.Windows.Forms.ToolStripStatusLabel statusSelection;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Close;
        private System.Windows.Forms.ToolStripMenuItem menu_File_NewProfile;
        private System.Windows.Forms.ToolStripMenuItem menu_File_LoadProfile;
        private System.Windows.Forms.ToolStripMenuItem menu_File_SaveProfile;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep3;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_Update;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_Import;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_Edit;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep1;
        private System.Windows.Forms.ToolStripMenuItem menu_File_SaveProfileAs;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Manual;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Manual_Export;
        private System.Windows.Forms.ToolStripSeparator menu_Profile_Sep1;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_Export;
        private System.Windows.Forms.ToolStripSeparator menu_Profile_Sep2;
        private System.Windows.Forms.ContextMenuStrip contextGame;
        private System.Windows.Forms.ToolStripMenuItem contextGame_Add;
        private System.Windows.Forms.ToolStripMenuItem contextGame_Edit;
        private System.Windows.Forms.ToolStripMenuItem contextGame_Remove;
        private System.Windows.Forms.ToolStripSeparator contextGame_Sep1;
        private System.Windows.Forms.ToolStripMenuItem contextGame_AddCat;
        private System.Windows.Forms.ToolStripMenuItem contextGame_SetFav;
        private System.Windows.Forms.ContextMenuStrip contextGameFav;
        private System.Windows.Forms.ToolStripMenuItem contextGameFav_Yes;
        private System.Windows.Forms.ToolStripMenuItem contextGameFav_No;
        private System.Windows.Forms.ContextMenuStrip contextGameAddCat;
        private System.Windows.Forms.ToolStripMenuItem contextGameAddCat_Create;
        private System.Windows.Forms.ContextMenuStrip contextCat;
        private System.Windows.Forms.ToolStripMenuItem contextCat_Add;
        private System.Windows.Forms.ToolStripMenuItem contextCat_Delete;
        private System.Windows.Forms.ToolStripMenuItem contextCat_Rename;
        private System.Windows.Forms.ToolStripMenuItem menu_Tools;
        private System.Windows.Forms.ToolStripMenuItem menu_Tools_AutonameAll;
        private System.Windows.Forms.ToolStripMenuItem menu_Tools_AutocatAll;
        private System.Windows.Forms.ToolStripSeparator menu_Tools_Sep2;
        private System.Windows.Forms.ToolStripMenuItem menu_Tools_Settings;
        private System.Windows.Forms.ToolStripMenuItem menu_Tools_DBEdit;
        private System.Windows.Forms.ToolStripSeparator menu_Tools_Sep3;
        private System.Windows.Forms.ToolStripMenuItem menu_Tools_RemoveEmpty;
        private System.Windows.Forms.ToolStripSeparator contextCat_Sep1;
        private System.Windows.Forms.ToolStripMenuItem contextCat_RemoveEmpty;
        private System.Windows.Forms.ToolStripSeparator contextGame_Sep2;
        private System.Windows.Forms.ToolStripMenuItem contextGame_VisitStore;
        private System.Windows.Forms.ToolStripSeparator contextGame_Sep3;
        private System.Windows.Forms.ToolStripMenuItem contextGame_LaunchGame;
        private System.Windows.Forms.ListView lstMultiCat;
        private System.Windows.Forms.ToolStripMenuItem contextGame_RemCat;
        private System.Windows.Forms.ContextMenuStrip contextGameRemCat;
        private System.Windows.Forms.ToolStripMenuItem menu_Tools_SingleCat;
        private System.Windows.Forms.ImageList imglistTriState;
        private System.Windows.Forms.SplitContainer splitGame;
        private System.Windows.Forms.Button cmdAddCatAndAssign;
        private System.Windows.Forms.TextBox txtAddCatAndAssign;
        private System.Windows.Forms.ComboBox cmbAutoCatType;
        private System.Windows.Forms.ContextMenuStrip menu_Tools_Autocat_List;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_AutoCats;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imglistFilter;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.ToolStripMenuItem menu_About;
        private System.Windows.Forms.ToolStripSeparator menu_Tools_Sep1;
        private System.Windows.Forms.ToolStripMenuItem autoModeHelperToolStripMenuItem;
        private BrightIdeasSoftware.FastObjectListView lstGames;
        private BrightIdeasSoftware.OLVColumn colGameID;
        private BrightIdeasSoftware.OLVColumn colTitle;
        private BrightIdeasSoftware.OLVColumn colCategories;
        private BrightIdeasSoftware.OLVColumn colFavorite;
        private BrightIdeasSoftware.OLVColumn colHidden;
        private BrightIdeasSoftware.OLVColumn colTags;
        private BrightIdeasSoftware.OLVColumn colYear;
        private BrightIdeasSoftware.OLVColumn colGenres;
        private BrightIdeasSoftware.OLVColumn colFlags;
        private BrightIdeasSoftware.OLVColumn colPlatforms;
        private BrightIdeasSoftware.OLVColumn colDevelopers;
        private BrightIdeasSoftware.OLVColumn colPublishers;
        private BrightIdeasSoftware.OLVColumn colNumberOfReviews;
        private BrightIdeasSoftware.OLVColumn colReviewScore;
        private BrightIdeasSoftware.OLVColumn colReviewLabel;
        private BrightIdeasSoftware.OLVColumn colAchievements;
        private BrightIdeasSoftware.OLVColumn colHltbMain;
        private BrightIdeasSoftware.OLVColumn colHltbExtras;
        private BrightIdeasSoftware.OLVColumn colHltbCompletionist;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private MaterialSkin.Controls.MaterialRaisedButton mbtnSearchClear;
        private MaterialSkin.Controls.MaterialSingleLineTextField mtxtSearch;
        private MaterialSkin.Controls.MaterialLabel mlblSearch;
        private MaterialSkin.Controls.MaterialCheckBox mchkBrowser;
        private MaterialSkin.Controls.MaterialRaisedButton mbtnCatDelete;
        private MaterialSkin.Controls.MaterialRaisedButton mbtnCatRename;
        private MaterialSkin.Controls.MaterialRaisedButton mbtnCatAdd;
        private MaterialSkin.Controls.MaterialLabel mlblHelp;
        private MaterialSkin.Controls.MaterialCheckBox mchkAdvancedCategories;
        private MaterialSkin.Controls.MaterialRaisedButton mbtnAutoCategorize;
        private System.Windows.Forms.ToolStripMenuItem contextGame_SetHidden;
        private System.Windows.Forms.ContextMenuStrip contextGameHidden;
        private System.Windows.Forms.ToolStripMenuItem contextGameHidden_Yes;
        private System.Windows.Forms.ToolStripMenuItem contextGameHidden_No;
        private MaterialSkin.Controls.MaterialListBox mlbAutoCatType;
    }
}

