/*
Copyright 2011, 2012 Steve Labbe.

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
            this.menu_Config = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Config_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpCategories = new System.Windows.Forms.GroupBox();
            this.lstCategories = new System.Windows.Forms.ListBox();
            this.contextCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextCat_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCat_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCat_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.tableCatButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdCatAdd = new System.Windows.Forms.Button();
            this.cmdCatDelete = new System.Windows.Forms.Button();
            this.cmdCatRename = new System.Windows.Forms.Button();
            this.grpGames = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.combFavorite = new System.Windows.Forms.ComboBox();
            this.cmdGameAdd = new System.Windows.Forms.Button();
            this.cmdGameRemove = new System.Windows.Forms.Button();
            this.cmdGameEdit = new System.Windows.Forms.Button();
            this.combCategory = new System.Windows.Forms.ComboBox();
            this.cmdGameSetCategory = new System.Windows.Forms.Button();
            this.cmdGameSetFavorite = new System.Windows.Forms.Button();
            this.lstGames = new System.Windows.Forms.ListView();
            this.colTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGameID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSelection = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
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
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File,
            this.menu_Profile,
            this.menu_Config});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(659, 24);
            this.menuStrip.TabIndex = 1;
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
            this.menu_File.Size = new System.Drawing.Size(37, 20);
            this.menu_File.Text = "&File";
            // 
            // menu_File_NewProfile
            // 
            this.menu_File_NewProfile.Name = "menu_File_NewProfile";
            this.menu_File_NewProfile.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.menu_File_NewProfile.Size = new System.Drawing.Size(219, 22);
            this.menu_File_NewProfile.Text = "&New Profile...";
            this.menu_File_NewProfile.Click += new System.EventHandler(this.menu_File_NewProfile_Click);
            // 
            // menu_File_LoadProfile
            // 
            this.menu_File_LoadProfile.Name = "menu_File_LoadProfile";
            this.menu_File_LoadProfile.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.menu_File_LoadProfile.Size = new System.Drawing.Size(219, 22);
            this.menu_File_LoadProfile.Text = "&Load Profile...";
            this.menu_File_LoadProfile.Click += new System.EventHandler(this.menu_File_LoadProfile_Click);
            // 
            // menu_File_SaveProfile
            // 
            this.menu_File_SaveProfile.Name = "menu_File_SaveProfile";
            this.menu_File_SaveProfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menu_File_SaveProfile.Size = new System.Drawing.Size(219, 22);
            this.menu_File_SaveProfile.Text = "&Save Profile";
            this.menu_File_SaveProfile.Click += new System.EventHandler(this.menu_File_SaveProfile_Click);
            // 
            // menu_File_SaveProfileAs
            // 
            this.menu_File_SaveProfileAs.Name = "menu_File_SaveProfileAs";
            this.menu_File_SaveProfileAs.Size = new System.Drawing.Size(219, 22);
            this.menu_File_SaveProfileAs.Text = "Save Profile &As...";
            this.menu_File_SaveProfileAs.Click += new System.EventHandler(this.menu_File_SaveProfileAs_Click);
            // 
            // menu_File_Sep1
            // 
            this.menu_File_Sep1.Name = "menu_File_Sep1";
            this.menu_File_Sep1.Size = new System.Drawing.Size(216, 6);
            // 
            // menu_File_Close
            // 
            this.menu_File_Close.Name = "menu_File_Close";
            this.menu_File_Close.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.menu_File_Close.Size = new System.Drawing.Size(219, 22);
            this.menu_File_Close.Text = "&Close";
            this.menu_File_Close.Click += new System.EventHandler(this.menu_File_Close_Click);
            // 
            // menu_File_Sep2
            // 
            this.menu_File_Sep2.Name = "menu_File_Sep2";
            this.menu_File_Sep2.Size = new System.Drawing.Size(216, 6);
            // 
            // menu_File_Manual
            // 
            this.menu_File_Manual.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File_Manual_Import,
            this.menu_File_Manual_Download,
            this.menu_File_Manual_Export});
            this.menu_File_Manual.Name = "menu_File_Manual";
            this.menu_File_Manual.Size = new System.Drawing.Size(219, 22);
            this.menu_File_Manual.Text = "&Manual";
            // 
            // menu_File_Manual_Import
            // 
            this.menu_File_Manual_Import.Name = "menu_File_Manual_Import";
            this.menu_File_Manual_Import.Size = new System.Drawing.Size(192, 22);
            this.menu_File_Manual_Import.Text = "&Import Config File...";
            this.menu_File_Manual_Import.Click += new System.EventHandler(this.menu_File_Manual_Import_Click);
            // 
            // menu_File_Manual_Download
            // 
            this.menu_File_Manual_Download.Name = "menu_File_Manual_Download";
            this.menu_File_Manual_Download.Size = new System.Drawing.Size(192, 22);
            this.menu_File_Manual_Download.Text = "&Download Game List...";
            this.menu_File_Manual_Download.Click += new System.EventHandler(this.menu_File_Manual_Download_Click);
            // 
            // menu_File_Manual_Export
            // 
            this.menu_File_Manual_Export.Name = "menu_File_Manual_Export";
            this.menu_File_Manual_Export.Size = new System.Drawing.Size(192, 22);
            this.menu_File_Manual_Export.Text = "&Export Config File...";
            this.menu_File_Manual_Export.Click += new System.EventHandler(this.menu_File_Manual_Export_Click);
            // 
            // menu_File_Sep3
            // 
            this.menu_File_Sep3.Name = "menu_File_Sep3";
            this.menu_File_Sep3.Size = new System.Drawing.Size(216, 6);
            // 
            // menu_File_Exit
            // 
            this.menu_File_Exit.Name = "menu_File_Exit";
            this.menu_File_Exit.Size = new System.Drawing.Size(219, 22);
            this.menu_File_Exit.Text = "E&xit";
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
            this.menu_Profile.Size = new System.Drawing.Size(53, 20);
            this.menu_Profile.Text = "&Profile";
            // 
            // menu_Profile_Download
            // 
            this.menu_Profile_Download.Name = "menu_Profile_Download";
            this.menu_Profile_Download.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menu_Profile_Download.Size = new System.Drawing.Size(225, 22);
            this.menu_Profile_Download.Text = "&Download Game List";
            this.menu_Profile_Download.Click += new System.EventHandler(this.menu_Profile_Download_Click);
            // 
            // menu_Profile_Import
            // 
            this.menu_Profile_Import.Name = "menu_Profile_Import";
            this.menu_Profile_Import.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.menu_Profile_Import.Size = new System.Drawing.Size(225, 22);
            this.menu_Profile_Import.Text = "&Import Config File";
            this.menu_Profile_Import.Click += new System.EventHandler(this.menu_Profile_Import_Click);
            // 
            // menu_Profile_Sep1
            // 
            this.menu_Profile_Sep1.Name = "menu_Profile_Sep1";
            this.menu_Profile_Sep1.Size = new System.Drawing.Size(222, 6);
            // 
            // menu_Profile_Export
            // 
            this.menu_Profile_Export.Name = "menu_Profile_Export";
            this.menu_Profile_Export.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.menu_Profile_Export.Size = new System.Drawing.Size(225, 22);
            this.menu_Profile_Export.Text = "&Export Config File";
            this.menu_Profile_Export.Click += new System.EventHandler(this.menu_Profile_Export_Click);
            // 
            // menu_Profile_Sep2
            // 
            this.menu_Profile_Sep2.Name = "menu_Profile_Sep2";
            this.menu_Profile_Sep2.Size = new System.Drawing.Size(222, 6);
            // 
            // menu_Profile_Edit
            // 
            this.menu_Profile_Edit.Name = "menu_Profile_Edit";
            this.menu_Profile_Edit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.menu_Profile_Edit.Size = new System.Drawing.Size(225, 22);
            this.menu_Profile_Edit.Text = "Edit &Profile Info";
            this.menu_Profile_Edit.Click += new System.EventHandler(this.menu_Profile_Edit_Click);
            // 
            // menu_Config
            // 
            this.menu_Config.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Config_Settings});
            this.menu_Config.Name = "menu_Config";
            this.menu_Config.Size = new System.Drawing.Size(93, 20);
            this.menu_Config.Text = "&Configuration";
            // 
            // menu_Config_Settings
            // 
            this.menu_Config_Settings.Name = "menu_Config_Settings";
            this.menu_Config_Settings.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menu_Config_Settings.Size = new System.Drawing.Size(168, 22);
            this.menu_Config_Settings.Text = "&Settings...";
            this.menu_Config_Settings.Click += new System.EventHandler(this.menu_Config_Settings_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpCategories);
            this.splitContainer.Panel1MinSize = 100;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpGames);
            this.splitContainer.Panel2MinSize = 550;
            this.splitContainer.Size = new System.Drawing.Size(659, 421);
            this.splitContainer.SplitterDistance = 105;
            this.splitContainer.TabIndex = 0;
            // 
            // grpCategories
            // 
            this.grpCategories.Controls.Add(this.lstCategories);
            this.grpCategories.Controls.Add(this.tableCatButtons);
            this.grpCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCategories.Location = new System.Drawing.Point(0, 0);
            this.grpCategories.Name = "grpCategories";
            this.grpCategories.Size = new System.Drawing.Size(105, 421);
            this.grpCategories.TabIndex = 0;
            this.grpCategories.TabStop = false;
            this.grpCategories.Text = "Categories";
            // 
            // lstCategories
            // 
            this.lstCategories.AllowDrop = true;
            this.lstCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCategories.ContextMenuStrip = this.contextCat;
            this.lstCategories.FormattingEnabled = true;
            this.lstCategories.IntegralHeight = false;
            this.lstCategories.Location = new System.Drawing.Point(6, 20);
            this.lstCategories.Name = "lstCategories";
            this.lstCategories.Size = new System.Drawing.Size(96, 347);
            this.lstCategories.TabIndex = 0;
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
            this.contextCat_Delete});
            this.contextCat.Name = "contextCat";
            this.contextCat.ShowImageMargin = false;
            this.contextCat.Size = new System.Drawing.Size(153, 92);
            this.contextCat.Opening += new System.ComponentModel.CancelEventHandler(this.contextCat_Opening);
            // 
            // contextCat_Add
            // 
            this.contextCat_Add.Name = "contextCat_Add";
            this.contextCat_Add.Size = new System.Drawing.Size(152, 22);
            this.contextCat_Add.Text = "Add Category...";
            this.contextCat_Add.Click += new System.EventHandler(this.cmdCatAdd_Click);
            // 
            // contextCat_Rename
            // 
            this.contextCat_Rename.Name = "contextCat_Rename";
            this.contextCat_Rename.Size = new System.Drawing.Size(152, 22);
            this.contextCat_Rename.Text = "Rename Category...";
            this.contextCat_Rename.Click += new System.EventHandler(this.cmdCatRename_Click);
            // 
            // contextCat_Delete
            // 
            this.contextCat_Delete.Name = "contextCat_Delete";
            this.contextCat_Delete.Size = new System.Drawing.Size(152, 22);
            this.contextCat_Delete.Text = "Delete Category";
            this.contextCat_Delete.Click += new System.EventHandler(this.cmdCatDelete_Click);
            // 
            // tableCatButtons
            // 
            this.tableCatButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableCatButtons.ColumnCount = 3;
            this.tableCatButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableCatButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableCatButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableCatButtons.Controls.Add(this.cmdCatAdd, 0, 0);
            this.tableCatButtons.Controls.Add(this.cmdCatDelete, 2, 0);
            this.tableCatButtons.Controls.Add(this.cmdCatRename, 1, 0);
            this.tableCatButtons.Location = new System.Drawing.Point(3, 369);
            this.tableCatButtons.Name = "tableCatButtons";
            this.tableCatButtons.RowCount = 1;
            this.tableCatButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableCatButtons.Size = new System.Drawing.Size(102, 29);
            this.tableCatButtons.TabIndex = 1;
            // 
            // cmdCatAdd
            // 
            this.cmdCatAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCatAdd.Location = new System.Drawing.Point(3, 3);
            this.cmdCatAdd.Name = "cmdCatAdd";
            this.cmdCatAdd.Size = new System.Drawing.Size(28, 23);
            this.cmdCatAdd.TabIndex = 0;
            this.cmdCatAdd.Text = "Add";
            this.cmdCatAdd.UseVisualStyleBackColor = true;
            this.cmdCatAdd.Click += new System.EventHandler(this.cmdCatAdd_Click);
            // 
            // cmdCatDelete
            // 
            this.cmdCatDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCatDelete.Location = new System.Drawing.Point(71, 3);
            this.cmdCatDelete.Name = "cmdCatDelete";
            this.cmdCatDelete.Size = new System.Drawing.Size(28, 23);
            this.cmdCatDelete.TabIndex = 2;
            this.cmdCatDelete.Text = "Delete";
            this.cmdCatDelete.UseVisualStyleBackColor = true;
            this.cmdCatDelete.Click += new System.EventHandler(this.cmdCatDelete_Click);
            // 
            // cmdCatRename
            // 
            this.cmdCatRename.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCatRename.Location = new System.Drawing.Point(37, 3);
            this.cmdCatRename.Name = "cmdCatRename";
            this.cmdCatRename.Size = new System.Drawing.Size(28, 23);
            this.cmdCatRename.TabIndex = 1;
            this.cmdCatRename.Text = "Rename";
            this.cmdCatRename.UseVisualStyleBackColor = true;
            this.cmdCatRename.Click += new System.EventHandler(this.cmdCatRename_Click);
            // 
            // grpGames
            // 
            this.grpGames.Controls.Add(this.checkBox2);
            this.grpGames.Controls.Add(this.checkBox1);
            this.grpGames.Controls.Add(this.button1);
            this.grpGames.Controls.Add(this.combFavorite);
            this.grpGames.Controls.Add(this.cmdGameAdd);
            this.grpGames.Controls.Add(this.cmdGameRemove);
            this.grpGames.Controls.Add(this.cmdGameEdit);
            this.grpGames.Controls.Add(this.combCategory);
            this.grpGames.Controls.Add(this.cmdGameSetCategory);
            this.grpGames.Controls.Add(this.cmdGameSetFavorite);
            this.grpGames.Controls.Add(this.lstGames);
            this.grpGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGames.Location = new System.Drawing.Point(0, 0);
            this.grpGames.Name = "grpGames";
            this.grpGames.Size = new System.Drawing.Size(550, 421);
            this.grpGames.TabIndex = 1;
            this.grpGames.TabStop = false;
            this.grpGames.Text = "Games";
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(164, 365);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(133, 17);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "Allow update from web";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(164, 348);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Only uncategorized";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(153, 323);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Auto-Categorize Selected";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // combFavorite
            // 
            this.combFavorite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.combFavorite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combFavorite.FormattingEnabled = true;
            this.combFavorite.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.combFavorite.Location = new System.Drawing.Point(392, 350);
            this.combFavorite.Name = "combFavorite";
            this.combFavorite.Size = new System.Drawing.Size(68, 21);
            this.combFavorite.TabIndex = 3;
            // 
            // cmdGameAdd
            // 
            this.cmdGameAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdGameAdd.Location = new System.Drawing.Point(2, 348);
            this.cmdGameAdd.Name = "cmdGameAdd";
            this.cmdGameAdd.Size = new System.Drawing.Size(110, 23);
            this.cmdGameAdd.TabIndex = 6;
            this.cmdGameAdd.Text = "Add Game";
            this.cmdGameAdd.UseVisualStyleBackColor = true;
            this.cmdGameAdd.Click += new System.EventHandler(this.cmdGameAdd_Click);
            // 
            // cmdGameRemove
            // 
            this.cmdGameRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdGameRemove.Location = new System.Drawing.Point(2, 373);
            this.cmdGameRemove.Name = "cmdGameRemove";
            this.cmdGameRemove.Size = new System.Drawing.Size(110, 23);
            this.cmdGameRemove.TabIndex = 7;
            this.cmdGameRemove.Text = "Remove Selected";
            this.cmdGameRemove.UseVisualStyleBackColor = true;
            this.cmdGameRemove.Click += new System.EventHandler(this.cmdGameRemove_Click);
            // 
            // cmdGameEdit
            // 
            this.cmdGameEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdGameEdit.Location = new System.Drawing.Point(2, 323);
            this.cmdGameEdit.Name = "cmdGameEdit";
            this.cmdGameEdit.Size = new System.Drawing.Size(110, 23);
            this.cmdGameEdit.TabIndex = 5;
            this.cmdGameEdit.Text = "Edit Game";
            this.cmdGameEdit.UseVisualStyleBackColor = true;
            this.cmdGameEdit.Click += new System.EventHandler(this.cmdGameEdit_Click);
            // 
            // combCategory
            // 
            this.combCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.combCategory.FormattingEnabled = true;
            this.combCategory.Location = new System.Drawing.Point(338, 325);
            this.combCategory.Name = "combCategory";
            this.combCategory.Size = new System.Drawing.Size(122, 21);
            this.combCategory.TabIndex = 1;
            // 
            // cmdGameSetCategory
            // 
            this.cmdGameSetCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGameSetCategory.Location = new System.Drawing.Point(466, 323);
            this.cmdGameSetCategory.Name = "cmdGameSetCategory";
            this.cmdGameSetCategory.Size = new System.Drawing.Size(78, 23);
            this.cmdGameSetCategory.TabIndex = 2;
            this.cmdGameSetCategory.Text = "Set Category";
            this.cmdGameSetCategory.UseVisualStyleBackColor = true;
            this.cmdGameSetCategory.Click += new System.EventHandler(this.cmdGameSetCategory_Click);
            // 
            // cmdGameSetFavorite
            // 
            this.cmdGameSetFavorite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGameSetFavorite.Location = new System.Drawing.Point(466, 349);
            this.cmdGameSetFavorite.Name = "cmdGameSetFavorite";
            this.cmdGameSetFavorite.Size = new System.Drawing.Size(78, 23);
            this.cmdGameSetFavorite.TabIndex = 4;
            this.cmdGameSetFavorite.Text = "Set Favorite";
            this.cmdGameSetFavorite.UseVisualStyleBackColor = true;
            this.cmdGameSetFavorite.Click += new System.EventHandler(this.cmdGameSetFavorite_Click);
            // 
            // lstGames
            // 
            this.lstGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitle,
            this.colGameID,
            this.colCategory,
            this.colFavorite});
            this.lstGames.ContextMenuStrip = this.contextGame;
            this.lstGames.FullRowSelect = true;
            this.lstGames.GridLines = true;
            this.lstGames.HideSelection = false;
            this.lstGames.LabelEdit = true;
            this.lstGames.Location = new System.Drawing.Point(3, 20);
            this.lstGames.Name = "lstGames";
            this.lstGames.Size = new System.Drawing.Size(541, 299);
            this.lstGames.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstGames.TabIndex = 0;
            this.lstGames.UseCompatibleStateImageBehavior = false;
            this.lstGames.View = System.Windows.Forms.View.Details;
            this.lstGames.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lstGames_AfterLabelEdit);
            this.lstGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstGames_ColumnClick);
            this.lstGames.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstGames_ItemDrag);
            this.lstGames.SelectedIndexChanged += new System.EventHandler(this.lstGames_SelectedIndexChanged);
            this.lstGames.DoubleClick += new System.EventHandler(this.lstGames_DoubleClick);
            this.lstGames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstGames_KeyDown);
            // 
            // colTitle
            // 
            this.colTitle.DisplayIndex = 1;
            this.colTitle.Text = "Title";
            this.colTitle.Width = 286;
            // 
            // colGameID
            // 
            this.colGameID.DisplayIndex = 0;
            this.colGameID.Text = "Game ID";
            // 
            // colCategory
            // 
            this.colCategory.Text = "Category";
            this.colCategory.Width = 118;
            // 
            // colFavorite
            // 
            this.colFavorite.Text = "Favorite";
            this.colFavorite.Width = 52;
            // 
            // contextGame
            // 
            this.contextGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGame_Add,
            this.cntxtGame_Edit,
            this.contextGame_Remove,
            this.contextGame_Sep1,
            this.contextGame_SetCat,
            this.contextGame_SetFav});
            this.contextGame.Name = "contextGame";
            this.contextGame.ShowImageMargin = false;
            this.contextGame.Size = new System.Drawing.Size(142, 120);
            this.contextGame.Opening += new System.ComponentModel.CancelEventHandler(this.contextGame_Opening);
            // 
            // contextGame_Add
            // 
            this.contextGame_Add.Name = "contextGame_Add";
            this.contextGame_Add.Size = new System.Drawing.Size(141, 22);
            this.contextGame_Add.Text = "Add New Game...";
            this.contextGame_Add.Click += new System.EventHandler(this.cmdGameAdd_Click);
            // 
            // cntxtGame_Edit
            // 
            this.cntxtGame_Edit.Name = "cntxtGame_Edit";
            this.cntxtGame_Edit.Size = new System.Drawing.Size(141, 22);
            this.cntxtGame_Edit.Text = "Edit Game...";
            this.cntxtGame_Edit.Click += new System.EventHandler(this.cmdGameEdit_Click);
            // 
            // contextGame_Remove
            // 
            this.contextGame_Remove.Name = "contextGame_Remove";
            this.contextGame_Remove.Size = new System.Drawing.Size(141, 22);
            this.contextGame_Remove.Text = "Remove Game";
            this.contextGame_Remove.Click += new System.EventHandler(this.cmdGameRemove_Click);
            // 
            // contextGame_Sep1
            // 
            this.contextGame_Sep1.Name = "contextGame_Sep1";
            this.contextGame_Sep1.Size = new System.Drawing.Size(138, 6);
            // 
            // contextGame_SetCat
            // 
            this.contextGame_SetCat.DropDown = this.contextGameCat;
            this.contextGame_SetCat.Name = "contextGame_SetCat";
            this.contextGame_SetCat.Size = new System.Drawing.Size(141, 22);
            this.contextGame_SetCat.Text = "Set Category";
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
            this.contextGameCat.Size = new System.Drawing.Size(126, 54);
            // 
            // contextGameCat_Create
            // 
            this.contextGameCat_Create.Name = "contextGameCat_Create";
            this.contextGameCat_Create.Size = new System.Drawing.Size(125, 22);
            this.contextGameCat_Create.Text = "Create New...";
            this.contextGameCat_Create.Click += new System.EventHandler(this.contextGameCat_Create_Click);
            // 
            // contextGameCat_None
            // 
            this.contextGameCat_None.Name = "contextGameCat_None";
            this.contextGameCat_None.Size = new System.Drawing.Size(125, 22);
            this.contextGameCat_None.Text = "Uncategorized";
            this.contextGameCat_None.Click += new System.EventHandler(this.contextGameCat_Category_Click);
            // 
            // contextGameCat_Sep1
            // 
            this.contextGameCat_Sep1.Name = "contextGameCat_Sep1";
            this.contextGameCat_Sep1.Size = new System.Drawing.Size(122, 6);
            // 
            // contextGame_SetFav
            // 
            this.contextGame_SetFav.DropDown = this.contextGameFav;
            this.contextGame_SetFav.Name = "contextGame_SetFav";
            this.contextGame_SetFav.Size = new System.Drawing.Size(141, 22);
            this.contextGame_SetFav.Text = "Set Favorite";
            // 
            // contextGameFav
            // 
            this.contextGameFav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextGameFav_Yes,
            this.contextGameFav_No});
            this.contextGameFav.Name = "contextGameFav";
            this.contextGameFav.OwnerItem = this.contextGame_SetFav;
            this.contextGameFav.ShowImageMargin = false;
            this.contextGameFav.Size = new System.Drawing.Size(68, 48);
            // 
            // contextGameFav_Yes
            // 
            this.contextGameFav_Yes.Name = "contextGameFav_Yes";
            this.contextGameFav_Yes.Size = new System.Drawing.Size(67, 22);
            this.contextGameFav_Yes.Text = "Yes";
            this.contextGameFav_Yes.Click += new System.EventHandler(this.contextGame_SetFav_Yes_Click);
            // 
            // contextGameFav_No
            // 
            this.contextGameFav_No.Name = "contextGameFav_No";
            this.contextGameFav_No.Size = new System.Drawing.Size(67, 22);
            this.contextGameFav_No.Text = "No";
            this.contextGameFav_No.Click += new System.EventHandler(this.contextGame_SetFav_No_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMsg,
            this.statusSelection});
            this.statusStrip.Location = new System.Drawing.Point(0, 423);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(659, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusMsg
            // 
            this.statusMsg.Name = "statusMsg";
            this.statusMsg.Size = new System.Drawing.Size(444, 17);
            this.statusMsg.Spring = true;
            this.statusMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusSelection
            // 
            this.statusSelection.AutoSize = false;
            this.statusSelection.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusSelection.Name = "statusSelection";
            this.statusSelection.Size = new System.Drawing.Size(200, 17);
            this.statusSelection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 445);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(675, 250);
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.Text = "Depressurizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
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
        private System.Windows.Forms.ColumnHeader colCategory;
        private System.Windows.Forms.ColumnHeader colGameID;
        private System.Windows.Forms.TableLayoutPanel tableCatButtons;
        private System.Windows.Forms.Button cmdCatAdd;
        private System.Windows.Forms.Button cmdCatDelete;
        private System.Windows.Forms.Button cmdCatRename;
        private System.Windows.Forms.ColumnHeader colFavorite;
        private System.Windows.Forms.ComboBox combFavorite;
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
        private System.Windows.Forms.ToolStripMenuItem menu_Config;
        private System.Windows.Forms.ToolStripMenuItem menu_Config_Settings;
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
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
    }
}

