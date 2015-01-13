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
            this.cmdSearchClear = new System.Windows.Forms.Button();
            this.lblSearchClear = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.grpCategories = new System.Windows.Forms.GroupBox();
            this.helpAdvancedCategories = new System.Windows.Forms.Label();
            this.radCatAdvanced = new System.Windows.Forms.RadioButton();
            this.radCatSimple = new System.Windows.Forms.RadioButton();
            this.lstCategories = new Depressurizer.Lib.ExtListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextCat_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCat_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCat_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCat_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextCat_RemoveEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.tableCatButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdCatAdd = new System.Windows.Forms.Button();
            this.cmdCatDelete = new System.Windows.Forms.Button();
            this.cmdCatRename = new System.Windows.Forms.Button();
            this.splitGame = new System.Windows.Forms.SplitContainer();
            this.grpGames = new System.Windows.Forms.GroupBox();
            this.lstGames = new Depressurizer.Lib.ExtListView();
            this.colGameID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFavorite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHidden = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextGame = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGame_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_AddCat = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameAddCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGameAddCat_Create = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_RemCat = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameRemCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGame_SetFav = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameFav = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGameFav_Yes = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameFav_No = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_VisitStore = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_LaunchGame = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbAutoCatType = new System.Windows.Forms.ComboBox();
            this.chkHidden = new System.Windows.Forms.CheckBox();
            this.cmdAddCatAndAssign = new System.Windows.Forms.Button();
            this.cmdAutoCat = new System.Windows.Forms.Button();
            this.txtAddCatAndAssign = new System.Windows.Forms.TextBox();
            this.chkFavorite = new System.Windows.Forms.CheckBox();
            this.lstMultiCat = new System.Windows.Forms.ListView();
            this.imglistTriState = new System.Windows.Forms.ImageList(this.components);
            this.cmdGameRemove = new System.Windows.Forms.Button();
            this.cmdGameAdd = new System.Windows.Forms.Button();
            this.cmdGameEdit = new System.Windows.Forms.Button();
            this.cmdGameLaunch = new System.Windows.Forms.Button();
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
            this.menu_Tools_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Tools_DBEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Tools_SingleCat = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.autoModeHelperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpCategories.SuspendLayout();
            this.contextCat.SuspendLayout();
            this.tableCatButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGame)).BeginInit();
            this.splitGame.Panel1.SuspendLayout();
            this.splitGame.Panel2.SuspendLayout();
            this.splitGame.SuspendLayout();
            this.grpGames.SuspendLayout();
            this.contextGame.SuspendLayout();
            this.contextGameAddCat.SuspendLayout();
            this.contextGameFav.SuspendLayout();
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
            this.splitContainer.Panel1.Controls.Add(this.cmdSearchClear);
            this.splitContainer.Panel1.Controls.Add(this.lblSearchClear);
            this.splitContainer.Panel1.Controls.Add(this.txtSearch);
            this.splitContainer.Panel1.Controls.Add(this.grpCategories);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitGame);
            // 
            // cmdSearchClear
            // 
            resources.ApplyResources(this.cmdSearchClear, "cmdSearchClear");
            this.cmdSearchClear.Name = "cmdSearchClear";
            this.cmdSearchClear.UseVisualStyleBackColor = true;
            this.cmdSearchClear.Click += new System.EventHandler(this.cmdSearchClear_Click);
            // 
            // lblSearchClear
            // 
            resources.ApplyResources(this.lblSearchClear, "lblSearchClear");
            this.lblSearchClear.Name = "lblSearchClear";
            // 
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // grpCategories
            // 
            resources.ApplyResources(this.grpCategories, "grpCategories");
            this.grpCategories.Controls.Add(this.helpAdvancedCategories);
            this.grpCategories.Controls.Add(this.radCatAdvanced);
            this.grpCategories.Controls.Add(this.radCatSimple);
            this.grpCategories.Controls.Add(this.lstCategories);
            this.grpCategories.Controls.Add(this.tableCatButtons);
            this.grpCategories.Name = "grpCategories";
            this.grpCategories.TabStop = false;
            // 
            // helpAdvancedCategories
            // 
            resources.ApplyResources(this.helpAdvancedCategories, "helpAdvancedCategories");
            this.helpAdvancedCategories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpAdvancedCategories.Name = "helpAdvancedCategories";
            // 
            // radCatAdvanced
            // 
            resources.ApplyResources(this.radCatAdvanced, "radCatAdvanced");
            this.radCatAdvanced.Name = "radCatAdvanced";
            this.radCatAdvanced.UseVisualStyleBackColor = true;
            this.radCatAdvanced.CheckedChanged += new System.EventHandler(this.radCatMode_CheckedChanged);
            // 
            // radCatSimple
            // 
            resources.ApplyResources(this.radCatSimple, "radCatSimple");
            this.radCatSimple.Checked = true;
            this.radCatSimple.Name = "radCatSimple";
            this.radCatSimple.TabStop = true;
            this.radCatSimple.UseVisualStyleBackColor = true;
            this.radCatSimple.CheckedChanged += new System.EventHandler(this.radCatMode_CheckedChanged);
            // 
            // lstCategories
            // 
            this.lstCategories.AllowDrop = true;
            resources.ApplyResources(this.lstCategories, "lstCategories");
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
            this.contextCat_Add.Click += new System.EventHandler(this.cmdCatAdd_Click);
            // 
            // contextCat_Rename
            // 
            this.contextCat_Rename.Name = "contextCat_Rename";
            resources.ApplyResources(this.contextCat_Rename, "contextCat_Rename");
            this.contextCat_Rename.Click += new System.EventHandler(this.cmdCatRename_Click);
            // 
            // contextCat_Delete
            // 
            this.contextCat_Delete.Name = "contextCat_Delete";
            resources.ApplyResources(this.contextCat_Delete, "contextCat_Delete");
            this.contextCat_Delete.Click += new System.EventHandler(this.cmdCatDelete_Click);
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
            // tableCatButtons
            // 
            resources.ApplyResources(this.tableCatButtons, "tableCatButtons");
            this.tableCatButtons.Controls.Add(this.cmdCatAdd, 0, 0);
            this.tableCatButtons.Controls.Add(this.cmdCatDelete, 2, 0);
            this.tableCatButtons.Controls.Add(this.cmdCatRename, 1, 0);
            this.tableCatButtons.Name = "tableCatButtons";
            // 
            // cmdCatAdd
            // 
            resources.ApplyResources(this.cmdCatAdd, "cmdCatAdd");
            this.cmdCatAdd.Name = "cmdCatAdd";
            this.cmdCatAdd.UseVisualStyleBackColor = true;
            this.cmdCatAdd.Click += new System.EventHandler(this.cmdCatAdd_Click);
            // 
            // cmdCatDelete
            // 
            resources.ApplyResources(this.cmdCatDelete, "cmdCatDelete");
            this.cmdCatDelete.Name = "cmdCatDelete";
            this.cmdCatDelete.UseVisualStyleBackColor = true;
            this.cmdCatDelete.Click += new System.EventHandler(this.cmdCatDelete_Click);
            // 
            // cmdCatRename
            // 
            resources.ApplyResources(this.cmdCatRename, "cmdCatRename");
            this.cmdCatRename.Name = "cmdCatRename";
            this.cmdCatRename.UseVisualStyleBackColor = true;
            this.cmdCatRename.Click += new System.EventHandler(this.cmdCatRename_Click);
            // 
            // splitGame
            // 
            resources.ApplyResources(this.splitGame, "splitGame");
            this.splitGame.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitGame.Name = "splitGame";
            // 
            // splitGame.Panel1
            // 
            this.splitGame.Panel1.Controls.Add(this.grpGames);
            // 
            // splitGame.Panel2
            // 
            this.splitGame.Panel2.Controls.Add(this.cmbAutoCatType);
            this.splitGame.Panel2.Controls.Add(this.chkHidden);
            this.splitGame.Panel2.Controls.Add(this.cmdAddCatAndAssign);
            this.splitGame.Panel2.Controls.Add(this.cmdAutoCat);
            this.splitGame.Panel2.Controls.Add(this.txtAddCatAndAssign);
            this.splitGame.Panel2.Controls.Add(this.chkFavorite);
            this.splitGame.Panel2.Controls.Add(this.lstMultiCat);
            this.splitGame.Panel2.Controls.Add(this.cmdGameRemove);
            this.splitGame.Panel2.Controls.Add(this.cmdGameAdd);
            this.splitGame.Panel2.Controls.Add(this.cmdGameEdit);
            this.splitGame.Panel2.Controls.Add(this.cmdGameLaunch);
            // 
            // grpGames
            // 
            this.grpGames.Controls.Add(this.lstGames);
            resources.ApplyResources(this.grpGames, "grpGames");
            this.grpGames.Name = "grpGames";
            this.grpGames.TabStop = false;
            // 
            // lstGames
            // 
            this.lstGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colGameID,
            this.colTitle,
            this.colCategory,
            this.colFavorite,
            this.colHidden});
            this.lstGames.ContextMenuStrip = this.contextGame;
            resources.ApplyResources(this.lstGames, "lstGames");
            this.lstGames.FullRowSelect = true;
            this.lstGames.GridLines = true;
            this.lstGames.HideSelection = false;
            this.lstGames.Name = "lstGames";
            this.lstGames.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstGames.UseCompatibleStateImageBehavior = false;
            this.lstGames.View = System.Windows.Forms.View.Details;
            this.lstGames.VirtualMode = true;
            this.lstGames.SelectionChanged += new System.EventHandler(this.lstGames_SelectionChanged);
            this.lstGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstGames_ColumnClick);
            this.lstGames.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstGames_ItemDrag);
            this.lstGames.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lstGames_RetrieveVirtualItem);
            this.lstGames.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.lstGames_SearchForVirtualItem);
            this.lstGames.DoubleClick += new System.EventHandler(this.lstGames_DoubleClick);
            this.lstGames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstGames_KeyDown);
            // 
            // colGameID
            // 
            this.colGameID.Tag = "colGameID";
            resources.ApplyResources(this.colGameID, "colGameID");
            // 
            // colTitle
            // 
            this.colTitle.Tag = "colTitle";
            resources.ApplyResources(this.colTitle, "colTitle");
            // 
            // colCategory
            // 
            this.colCategory.Tag = "colCategory";
            resources.ApplyResources(this.colCategory, "colCategory");
            // 
            // colFavorite
            // 
            this.colFavorite.Tag = "colFavorite";
            resources.ApplyResources(this.colFavorite, "colFavorite");
            // 
            // colHidden
            // 
            this.colHidden.Tag = "colHidden";
            resources.ApplyResources(this.colHidden, "colHidden");
            // 
            // contextGame
            // 
            this.contextGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGame_Add,
            this.contextGame_Edit,
            this.contextGame_Remove,
            this.contextGame_Sep1,
            this.contextGame_AddCat,
            this.contextGame_RemCat,
            this.contextGame_SetFav,
            this.contextGame_Sep2,
            this.contextGame_VisitStore,
            this.contextGame_Sep3,
            this.contextGame_LaunchGame});
            this.contextGame.Name = "contextGame";
            this.contextGame.ShowImageMargin = false;
            resources.ApplyResources(this.contextGame, "contextGame");
            this.contextGame.Opening += new System.ComponentModel.CancelEventHandler(this.contextGame_Opening);
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
            // contextGame_Sep1
            // 
            this.contextGame_Sep1.Name = "contextGame_Sep1";
            resources.ApplyResources(this.contextGame_Sep1, "contextGame_Sep1");
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
            // contextGame_Sep2
            // 
            this.contextGame_Sep2.Name = "contextGame_Sep2";
            resources.ApplyResources(this.contextGame_Sep2, "contextGame_Sep2");
            // 
            // contextGame_VisitStore
            // 
            this.contextGame_VisitStore.Name = "contextGame_VisitStore";
            resources.ApplyResources(this.contextGame_VisitStore, "contextGame_VisitStore");
            this.contextGame_VisitStore.Click += new System.EventHandler(this.contextGame_VisitStore_Click);
            // 
            // contextGame_Sep3
            // 
            this.contextGame_Sep3.Name = "contextGame_Sep3";
            resources.ApplyResources(this.contextGame_Sep3, "contextGame_Sep3");
            // 
            // contextGame_LaunchGame
            // 
            this.contextGame_LaunchGame.Name = "contextGame_LaunchGame";
            resources.ApplyResources(this.contextGame_LaunchGame, "contextGame_LaunchGame");
            this.contextGame_LaunchGame.Click += new System.EventHandler(this.cmdGameLaunch_Click);
            // 
            // cmbAutoCatType
            // 
            this.cmbAutoCatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutoCatType.FormattingEnabled = true;
            resources.ApplyResources(this.cmbAutoCatType, "cmbAutoCatType");
            this.cmbAutoCatType.Name = "cmbAutoCatType";
            // 
            // chkHidden
            // 
            resources.ApplyResources(this.chkHidden, "chkHidden");
            this.chkHidden.Name = "chkHidden";
            this.chkHidden.UseVisualStyleBackColor = true;
            this.chkHidden.CheckedChanged += new System.EventHandler(this.chkHidden_CheckedChanged);
            // 
            // cmdAddCatAndAssign
            // 
            resources.ApplyResources(this.cmdAddCatAndAssign, "cmdAddCatAndAssign");
            this.cmdAddCatAndAssign.Name = "cmdAddCatAndAssign";
            this.cmdAddCatAndAssign.UseVisualStyleBackColor = true;
            this.cmdAddCatAndAssign.Click += new System.EventHandler(this.cmdAddCatAndAssign_Click);
            // 
            // cmdAutoCat
            // 
            resources.ApplyResources(this.cmdAutoCat, "cmdAutoCat");
            this.cmdAutoCat.Name = "cmdAutoCat";
            this.cmdAutoCat.UseVisualStyleBackColor = true;
            this.cmdAutoCat.Click += new System.EventHandler(this.cmdAutoCat_Click);
            // 
            // txtAddCatAndAssign
            // 
            resources.ApplyResources(this.txtAddCatAndAssign, "txtAddCatAndAssign");
            this.txtAddCatAndAssign.Name = "txtAddCatAndAssign";
            // 
            // chkFavorite
            // 
            resources.ApplyResources(this.chkFavorite, "chkFavorite");
            this.chkFavorite.Name = "chkFavorite";
            this.chkFavorite.UseVisualStyleBackColor = false;
            this.chkFavorite.CheckedChanged += new System.EventHandler(this.chkFavorite_CheckedChanged);
            // 
            // lstMultiCat
            // 
            this.lstMultiCat.Activation = System.Windows.Forms.ItemActivation.OneClick;
            resources.ApplyResources(this.lstMultiCat, "lstMultiCat");
            this.lstMultiCat.AutoArrange = false;
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
            // cmdGameRemove
            // 
            resources.ApplyResources(this.cmdGameRemove, "cmdGameRemove");
            this.cmdGameRemove.Name = "cmdGameRemove";
            this.cmdGameRemove.UseVisualStyleBackColor = true;
            this.cmdGameRemove.Click += new System.EventHandler(this.cmdGameRemove_Click);
            // 
            // cmdGameAdd
            // 
            resources.ApplyResources(this.cmdGameAdd, "cmdGameAdd");
            this.cmdGameAdd.Name = "cmdGameAdd";
            this.cmdGameAdd.UseVisualStyleBackColor = true;
            this.cmdGameAdd.Click += new System.EventHandler(this.cmdGameAdd_Click);
            // 
            // cmdGameEdit
            // 
            resources.ApplyResources(this.cmdGameEdit, "cmdGameEdit");
            this.cmdGameEdit.Name = "cmdGameEdit";
            this.cmdGameEdit.UseVisualStyleBackColor = true;
            this.cmdGameEdit.Click += new System.EventHandler(this.cmdGameEdit_Click);
            // 
            // cmdGameLaunch
            // 
            resources.ApplyResources(this.cmdGameLaunch, "cmdGameLaunch");
            this.cmdGameLaunch.Name = "cmdGameLaunch";
            this.cmdGameLaunch.UseVisualStyleBackColor = true;
            this.cmdGameLaunch.Click += new System.EventHandler(this.cmdGameLaunch_Click);
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
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File,
            this.menu_Profile,
            this.menu_Tools,
            this.menu_About});
            resources.ApplyResources(this.menuStrip, "menuStrip");
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
            this.menu_Profile.Name = "menu_Profile";
            resources.ApplyResources(this.menu_Profile, "menu_Profile");
            // 
            // menu_Profile_Update
            // 
            this.menu_Profile_Update.Name = "menu_Profile_Update";
            resources.ApplyResources(this.menu_Profile_Update, "menu_Profile_Update");
            this.menu_Profile_Update.Click += new System.EventHandler(this.menu_Profile_Update_Click);
            // 
            // menu_Profile_Import
            // 
            this.menu_Profile_Import.Name = "menu_Profile_Import";
            resources.ApplyResources(this.menu_Profile_Import, "menu_Profile_Import");
            this.menu_Profile_Import.Click += new System.EventHandler(this.menu_Profile_Import_Click);
            // 
            // menu_Profile_Sep1
            // 
            this.menu_Profile_Sep1.Name = "menu_Profile_Sep1";
            resources.ApplyResources(this.menu_Profile_Sep1, "menu_Profile_Sep1");
            // 
            // menu_Profile_Export
            // 
            this.menu_Profile_Export.Name = "menu_Profile_Export";
            resources.ApplyResources(this.menu_Profile_Export, "menu_Profile_Export");
            this.menu_Profile_Export.Click += new System.EventHandler(this.menu_Profile_Export_Click);
            // 
            // menu_Profile_Sep2
            // 
            this.menu_Profile_Sep2.Name = "menu_Profile_Sep2";
            resources.ApplyResources(this.menu_Profile_Sep2, "menu_Profile_Sep2");
            // 
            // menu_Profile_Edit
            // 
            this.menu_Profile_Edit.Name = "menu_Profile_Edit";
            resources.ApplyResources(this.menu_Profile_Edit, "menu_Profile_Edit");
            this.menu_Profile_Edit.Click += new System.EventHandler(this.menu_Profile_Edit_Click);
            // 
            // menu_Profile_AutoCats
            // 
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
            // autoModeHelperToolStripMenuItem
            // 
            this.autoModeHelperToolStripMenuItem.Name = "autoModeHelperToolStripMenuItem";
            resources.ApplyResources(this.autoModeHelperToolStripMenuItem, "autoModeHelperToolStripMenuItem");
            this.autoModeHelperToolStripMenuItem.Click += new System.EventHandler(this.autoModeHelperToolStripMenuItem_Click);
            // 
            // menu_Tools_Sep1
            // 
            this.menu_Tools_Sep1.Name = "menu_Tools_Sep1";
            resources.ApplyResources(this.menu_Tools_Sep1, "menu_Tools_Sep1");
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpCategories.ResumeLayout(false);
            this.grpCategories.PerformLayout();
            this.contextCat.ResumeLayout(false);
            this.tableCatButtons.ResumeLayout(false);
            this.splitGame.Panel1.ResumeLayout(false);
            this.splitGame.Panel2.ResumeLayout(false);
            this.splitGame.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGame)).EndInit();
            this.splitGame.ResumeLayout(false);
            this.grpGames.ResumeLayout(false);
            this.contextGame.ResumeLayout(false);
            this.contextGameAddCat.ResumeLayout(false);
            this.contextGameFav.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox grpCategories;
        private System.Windows.Forms.GroupBox grpGames;
        private System.Windows.Forms.TableLayoutPanel tableCatButtons;
        private System.Windows.Forms.Button cmdCatAdd;
        private System.Windows.Forms.Button cmdCatDelete;
        private System.Windows.Forms.Button cmdCatRename;
        private System.Windows.Forms.Button cmdGameAdd;
        private System.Windows.Forms.Button cmdGameRemove;
        private System.Windows.Forms.Button cmdGameEdit;
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
        private System.Windows.Forms.Button cmdAutoCat;
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
        private System.Windows.Forms.Button cmdGameLaunch;
        private System.Windows.Forms.ToolStripSeparator contextGame_Sep3;
        private System.Windows.Forms.ToolStripMenuItem contextGame_LaunchGame;
        private System.Windows.Forms.CheckBox chkFavorite;
        private System.Windows.Forms.ListView lstMultiCat;
        private System.Windows.Forms.ToolStripMenuItem contextGame_RemCat;
        private System.Windows.Forms.ContextMenuStrip contextGameRemCat;
        private System.Windows.Forms.ToolStripMenuItem menu_Tools_SingleCat;
        private System.Windows.Forms.ImageList imglistTriState;
        private System.Windows.Forms.SplitContainer splitGame;
        private System.Windows.Forms.Button cmdAddCatAndAssign;
        private System.Windows.Forms.TextBox txtAddCatAndAssign;
        private System.Windows.Forms.ComboBox cmbAutoCatType;
        private System.Windows.Forms.CheckBox chkHidden;
        private Depressurizer.Lib.ExtListView lstGames;
        private System.Windows.Forms.ColumnHeader colGameID;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.ColumnHeader colFavorite;
        private System.Windows.Forms.ColumnHeader colHidden;
        private System.Windows.Forms.ContextMenuStrip menu_Tools_Autocat_List;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_AutoCats;
        private System.Windows.Forms.Button cmdSearchClear;
        private System.Windows.Forms.Label lblSearchClear;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.RadioButton radCatAdvanced;
        private System.Windows.Forms.RadioButton radCatSimple;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imglistFilter;
        private System.Windows.Forms.Label helpAdvancedCategories;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.ToolStripMenuItem menu_About;
        private System.Windows.Forms.ToolStripSeparator menu_Tools_Sep1;
        private System.Windows.Forms.ToolStripMenuItem autoModeHelperToolStripMenuItem;
    }
}

