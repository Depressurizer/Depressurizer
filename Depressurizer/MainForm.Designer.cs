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
            this.grpCategories = new System.Windows.Forms.GroupBox();
            this.lstCategories = new System.Windows.Forms.ListBox();
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
            this.grpGames = new System.Windows.Forms.GroupBox();
            this.chkFavorite = new System.Windows.Forms.CheckBox();
            this.cmdGameLaunch = new System.Windows.Forms.Button();
            this.cmdAutoCat = new System.Windows.Forms.Button();
            this.cmdGameAdd = new System.Windows.Forms.Button();
            this.cmdGameRemove = new System.Windows.Forms.Button();
            this.cmdGameEdit = new System.Windows.Forms.Button();
            this.combCategory = new System.Windows.Forms.ComboBox();
            this.cmdGameSetCategory = new System.Windows.Forms.Button();
            this.cmdGameSetFavorite = new System.Windows.Forms.Button();
            this.lstGames = new System.Windows.Forms.ListView();
            this.colGameID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFavorite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextGame = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGame_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxtGame_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_SetCat = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGameCat_Create = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameCat_None = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameCat_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_SetFav = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameFav = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextGameFav_Yes = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGameFav_No = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_VisitStore = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGame_Sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextGame_LaunchGame = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menu_File_Manual_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Manual_Download = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Manual_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile_Download = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Profile_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Profile_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Profile_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_AutonameAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_AutocatAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_RemoveEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Tools_DBEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tools_Sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Tools_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSelection = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpCategories.SuspendLayout();
            this.contextCat.SuspendLayout();
            this.tableCatButtons.SuspendLayout();
            this.grpGames.SuspendLayout();
            this.contextGame.SuspendLayout();
            this.contextGameCat.SuspendLayout();
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
            this.splitContainer.Panel1.Controls.Add(this.grpCategories);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpGames);
            // 
            // grpCategories
            // 
            this.grpCategories.Controls.Add(this.lstCategories);
            this.grpCategories.Controls.Add(this.tableCatButtons);
            resources.ApplyResources(this.grpCategories, "grpCategories");
            this.grpCategories.Name = "grpCategories";
            this.grpCategories.TabStop = false;
            // 
            // lstCategories
            // 
            this.lstCategories.AllowDrop = true;
            resources.ApplyResources(this.lstCategories, "lstCategories");
            this.lstCategories.ContextMenuStrip = this.contextCat;
            this.lstCategories.FormattingEnabled = true;
            this.lstCategories.Name = "lstCategories";
            this.lstCategories.SelectedIndexChanged += new System.EventHandler(this.lstCategories_SelectedIndexChanged);
            this.lstCategories.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstCategories_DragDrop);
            this.lstCategories.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstCategories_DragEnter);
            this.lstCategories.DragOver += new System.Windows.Forms.DragEventHandler(this.lstCategories_DragOver);
            this.lstCategories.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstCategories_KeyDown);
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
            // grpGames
            // 
            this.grpGames.Controls.Add(this.chkFavorite);
            this.grpGames.Controls.Add(this.cmdGameLaunch);
            this.grpGames.Controls.Add(this.cmdAutoCat);
            this.grpGames.Controls.Add(this.cmdGameAdd);
            this.grpGames.Controls.Add(this.cmdGameRemove);
            this.grpGames.Controls.Add(this.cmdGameEdit);
            this.grpGames.Controls.Add(this.combCategory);
            this.grpGames.Controls.Add(this.cmdGameSetCategory);
            this.grpGames.Controls.Add(this.cmdGameSetFavorite);
            this.grpGames.Controls.Add(this.lstGames);
            resources.ApplyResources(this.grpGames, "grpGames");
            this.grpGames.Name = "grpGames";
            this.grpGames.TabStop = false;
            // 
            // chkFavorite
            // 
            resources.ApplyResources(this.chkFavorite, "chkFavorite");
            this.chkFavorite.Name = "chkFavorite";
            this.chkFavorite.UseVisualStyleBackColor = true;
            // 
            // cmdGameLaunch
            // 
            resources.ApplyResources(this.cmdGameLaunch, "cmdGameLaunch");
            this.cmdGameLaunch.Name = "cmdGameLaunch";
            this.cmdGameLaunch.UseVisualStyleBackColor = true;
            this.cmdGameLaunch.Click += new System.EventHandler(this.cmdGameLaunch_Click);
            // 
            // cmdAutoCat
            // 
            resources.ApplyResources(this.cmdAutoCat, "cmdAutoCat");
            this.cmdAutoCat.Name = "cmdAutoCat";
            this.cmdAutoCat.UseVisualStyleBackColor = true;
            this.cmdAutoCat.Click += new System.EventHandler(this.cmdAutoCat_Click);
            // 
            // cmdGameAdd
            // 
            resources.ApplyResources(this.cmdGameAdd, "cmdGameAdd");
            this.cmdGameAdd.Name = "cmdGameAdd";
            this.cmdGameAdd.UseVisualStyleBackColor = true;
            this.cmdGameAdd.Click += new System.EventHandler(this.cmdGameAdd_Click);
            // 
            // cmdGameRemove
            // 
            resources.ApplyResources(this.cmdGameRemove, "cmdGameRemove");
            this.cmdGameRemove.Name = "cmdGameRemove";
            this.cmdGameRemove.UseVisualStyleBackColor = true;
            this.cmdGameRemove.Click += new System.EventHandler(this.cmdGameRemove_Click);
            // 
            // cmdGameEdit
            // 
            resources.ApplyResources(this.cmdGameEdit, "cmdGameEdit");
            this.cmdGameEdit.Name = "cmdGameEdit";
            this.cmdGameEdit.UseVisualStyleBackColor = true;
            this.cmdGameEdit.Click += new System.EventHandler(this.cmdGameEdit_Click);
            // 
            // combCategory
            // 
            resources.ApplyResources(this.combCategory, "combCategory");
            this.combCategory.FormattingEnabled = true;
            this.combCategory.Name = "combCategory";
            // 
            // cmdGameSetCategory
            // 
            resources.ApplyResources(this.cmdGameSetCategory, "cmdGameSetCategory");
            this.cmdGameSetCategory.Name = "cmdGameSetCategory";
            this.cmdGameSetCategory.UseVisualStyleBackColor = true;
            this.cmdGameSetCategory.Click += new System.EventHandler(this.cmdGameSetCategory_Click);
            // 
            // cmdGameSetFavorite
            // 
            resources.ApplyResources(this.cmdGameSetFavorite, "cmdGameSetFavorite");
            this.cmdGameSetFavorite.Name = "cmdGameSetFavorite";
            this.cmdGameSetFavorite.UseVisualStyleBackColor = true;
            this.cmdGameSetFavorite.Click += new System.EventHandler(this.cmdGameSetFavorite_Click);
            // 
            // lstGames
            // 
            resources.ApplyResources(this.lstGames, "lstGames");
            this.lstGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colGameID,
            this.colTitle,
            this.colCategory,
            this.colFavorite});
            this.lstGames.ContextMenuStrip = this.contextGame;
            this.lstGames.FullRowSelect = true;
            this.lstGames.GridLines = true;
            this.lstGames.HideSelection = false;
            this.lstGames.LabelEdit = true;
            this.lstGames.Name = "lstGames";
            this.lstGames.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstGames.UseCompatibleStateImageBehavior = false;
            this.lstGames.View = System.Windows.Forms.View.Details;
            this.lstGames.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lstGames_AfterLabelEdit);
            this.lstGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstGames_ColumnClick);
            this.lstGames.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstGames_ItemDrag);
            this.lstGames.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstGames_ItemSelectionChanged);
            this.lstGames.SelectedIndexChanged += new System.EventHandler(this.lstGames_SelectedIndexChanged);
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
            // contextGame
            // 
            this.contextGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGame_Add,
            this.cntxtGame_Edit,
            this.contextGame_Remove,
            this.contextGame_Sep1,
            this.contextGame_SetCat,
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
            // cntxtGame_Edit
            // 
            this.cntxtGame_Edit.Name = "cntxtGame_Edit";
            resources.ApplyResources(this.cntxtGame_Edit, "cntxtGame_Edit");
            this.cntxtGame_Edit.Click += new System.EventHandler(this.cmdGameEdit_Click);
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
            // contextGame_SetCat
            // 
            this.contextGame_SetCat.DropDown = this.contextGameCat;
            this.contextGame_SetCat.Name = "contextGame_SetCat";
            resources.ApplyResources(this.contextGame_SetCat, "contextGame_SetCat");
            // 
            // contextGameCat
            // 
            this.contextGameCat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGameCat_Create,
            this.contextGameCat_None,
            this.contextGameCat_Sep1});
            this.contextGameCat.Name = "contextGameCat";
            this.contextGameCat.OwnerItem = this.contextGame_SetCat;
            this.contextGameCat.ShowImageMargin = false;
            resources.ApplyResources(this.contextGameCat, "contextGameCat");
            // 
            // contextGameCat_Create
            // 
            this.contextGameCat_Create.Name = "contextGameCat_Create";
            resources.ApplyResources(this.contextGameCat_Create, "contextGameCat_Create");
            this.contextGameCat_Create.Click += new System.EventHandler(this.contextGameCat_Create_Click);
            // 
            // contextGameCat_None
            // 
            this.contextGameCat_None.Name = "contextGameCat_None";
            resources.ApplyResources(this.contextGameCat_None, "contextGameCat_None");
            this.contextGameCat_None.Click += new System.EventHandler(this.contextGameCat_Category_Click);
            // 
            // contextGameCat_Sep1
            // 
            this.contextGameCat_Sep1.Name = "contextGameCat_Sep1";
            resources.ApplyResources(this.contextGameCat_Sep1, "contextGameCat_Sep1");
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
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File,
            this.menu_Profile,
            this.menu_Tools});
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
            this.menu_File_Manual_Import,
            this.menu_File_Manual_Download,
            this.menu_File_Manual_Export});
            this.menu_File_Manual.Name = "menu_File_Manual";
            resources.ApplyResources(this.menu_File_Manual, "menu_File_Manual");
            // 
            // menu_File_Manual_Import
            // 
            this.menu_File_Manual_Import.Name = "menu_File_Manual_Import";
            resources.ApplyResources(this.menu_File_Manual_Import, "menu_File_Manual_Import");
            this.menu_File_Manual_Import.Click += new System.EventHandler(this.menu_File_Manual_Import_Click);
            // 
            // menu_File_Manual_Download
            // 
            this.menu_File_Manual_Download.Name = "menu_File_Manual_Download";
            resources.ApplyResources(this.menu_File_Manual_Download, "menu_File_Manual_Download");
            this.menu_File_Manual_Download.Click += new System.EventHandler(this.menu_File_Manual_Download_Click);
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
            this.menu_Profile_Download,
            this.menu_Profile_Import,
            this.menu_Profile_Sep1,
            this.menu_Profile_Export,
            this.menu_Profile_Sep2,
            this.menu_Profile_Edit});
            this.menu_Profile.Name = "menu_Profile";
            resources.ApplyResources(this.menu_Profile, "menu_Profile");
            // 
            // menu_Profile_Download
            // 
            this.menu_Profile_Download.Name = "menu_Profile_Download";
            resources.ApplyResources(this.menu_Profile_Download, "menu_Profile_Download");
            this.menu_Profile_Download.Click += new System.EventHandler(this.menu_Profile_Download_Click);
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
            // menu_Tools
            // 
            this.menu_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Tools_AutonameAll,
            this.menu_Tools_AutocatAll,
            this.menu_Tools_RemoveEmpty,
            this.menu_Tools_Sep2,
            this.menu_Tools_DBEdit,
            this.menu_Tools_Sep3,
            this.menu_Tools_Settings});
            this.menu_Tools.Name = "menu_Tools";
            resources.ApplyResources(this.menu_Tools, "menu_Tools");
            // 
            // menu_Tools_AutonameAll
            // 
            this.menu_Tools_AutonameAll.Name = "menu_Tools_AutonameAll";
            resources.ApplyResources(this.menu_Tools_AutonameAll, "menu_Tools_AutonameAll");
            this.menu_Tools_AutonameAll.Click += new System.EventHandler(this.menu_Tools_AutonameAll_Click);
            // 
            // menu_Tools_AutocatAll
            // 
            this.menu_Tools_AutocatAll.Name = "menu_Tools_AutocatAll";
            resources.ApplyResources(this.menu_Tools_AutocatAll, "menu_Tools_AutocatAll");
            this.menu_Tools_AutocatAll.Click += new System.EventHandler(this.menu_Tools_AutocatAll_Click);
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
            // menu_Tools_Settings
            // 
            this.menu_Tools_Settings.Name = "menu_Tools_Settings";
            resources.ApplyResources(this.menu_Tools_Settings, "menu_Tools_Settings");
            this.menu_Tools_Settings.Click += new System.EventHandler(this.menu_Tools_Settings_Click);
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
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpCategories.ResumeLayout(false);
            this.contextCat.ResumeLayout(false);
            this.tableCatButtons.ResumeLayout(false);
            this.grpGames.ResumeLayout(false);
            this.grpGames.PerformLayout();
            this.contextGame.ResumeLayout(false);
            this.contextGameCat.ResumeLayout(false);
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
        private System.Windows.Forms.ListView lstGames;
        private System.Windows.Forms.ColumnHeader colTitle;
        private System.Windows.Forms.ColumnHeader colGameID;
        private System.Windows.Forms.TableLayoutPanel tableCatButtons;
        private System.Windows.Forms.Button cmdCatAdd;
        private System.Windows.Forms.Button cmdCatDelete;
        private System.Windows.Forms.Button cmdCatRename;
        private System.Windows.Forms.ColumnHeader colFavorite;
        private System.Windows.Forms.Button cmdGameAdd;
        private System.Windows.Forms.Button cmdGameRemove;
        private System.Windows.Forms.Button cmdGameEdit;
        private System.Windows.Forms.ComboBox combCategory;
        private System.Windows.Forms.Button cmdGameSetCategory;
        private System.Windows.Forms.Button cmdGameSetFavorite;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep2;
        private System.Windows.Forms.ListBox lstCategories;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusMsg;
        private System.Windows.Forms.ToolStripStatusLabel statusSelection;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Close;
        private System.Windows.Forms.ToolStripMenuItem menu_File_NewProfile;
        private System.Windows.Forms.ToolStripMenuItem menu_File_LoadProfile;
        private System.Windows.Forms.ToolStripMenuItem menu_File_SaveProfile;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep3;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_Download;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_Import;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_Edit;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep1;
        private System.Windows.Forms.ToolStripMenuItem menu_File_SaveProfileAs;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Manual;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Manual_Import;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Manual_Download;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Manual_Export;
        private System.Windows.Forms.ToolStripSeparator menu_Profile_Sep1;
        private System.Windows.Forms.ToolStripMenuItem menu_Profile_Export;
        private System.Windows.Forms.ToolStripSeparator menu_Profile_Sep2;
        private System.Windows.Forms.ContextMenuStrip contextGame;
        private System.Windows.Forms.ToolStripMenuItem contextGame_Add;
        private System.Windows.Forms.ToolStripMenuItem cntxtGame_Edit;
        private System.Windows.Forms.ToolStripMenuItem contextGame_Remove;
        private System.Windows.Forms.ToolStripSeparator contextGame_Sep1;
        private System.Windows.Forms.ToolStripMenuItem contextGame_SetCat;
        private System.Windows.Forms.ToolStripMenuItem contextGame_SetFav;
        private System.Windows.Forms.ContextMenuStrip contextGameFav;
        private System.Windows.Forms.ToolStripMenuItem contextGameFav_Yes;
        private System.Windows.Forms.ToolStripMenuItem contextGameFav_No;
        private System.Windows.Forms.ContextMenuStrip contextGameCat;
        private System.Windows.Forms.ToolStripMenuItem contextGameCat_Create;
        private System.Windows.Forms.ToolStripMenuItem contextGameCat_None;
        private System.Windows.Forms.ToolStripSeparator contextGameCat_Sep1;
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
        private System.Windows.Forms.ColumnHeader colCategory;
    }
}

