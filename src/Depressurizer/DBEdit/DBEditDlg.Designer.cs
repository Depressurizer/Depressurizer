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
    partial class DBEditDlg {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBEditDlg));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_File_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdFetch = new System.Windows.Forms.Button();
            this.cmdUpdateNew = new System.Windows.Forms.Button();
            this.cmdUpdateSelected = new System.Windows.Forms.Button();
            this.cmdEditGame = new System.Windows.Forms.Button();
            this.cmdDeleteGame = new System.Windows.Forms.Button();
            this.cmdAddGame = new System.Windows.Forms.Button();
            this.grpTypes = new System.Windows.Forms.GroupBox();
            this.chkTypeUnknown = new System.Windows.Forms.CheckBox();
            this.chkTypeOther = new System.Windows.Forms.CheckBox();
            this.chkTypeDLC = new System.Windows.Forms.CheckBox();
            this.chkTypeGame = new System.Windows.Forms.CheckBox();
            this.chkTypeAll = new System.Windows.Forms.CheckBox();
            this.cmdStore = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statSelected = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmdUpdateAppInfo = new System.Windows.Forms.Button();
            this.grpWebUpdate = new System.Windows.Forms.GroupBox();
            this.radWebNo = new System.Windows.Forms.RadioButton();
            this.radWebSince = new System.Windows.Forms.RadioButton();
            this.radWebYes = new System.Windows.Forms.RadioButton();
            this.radWebAll = new System.Windows.Forms.RadioButton();
            this.dateWeb = new System.Windows.Forms.DateTimePicker();
            this.grpAppInfo = new System.Windows.Forms.GroupBox();
            this.radAppNo = new System.Windows.Forms.RadioButton();
            this.radAppYes = new System.Windows.Forms.RadioButton();
            this.radAppAll = new System.Windows.Forms.RadioButton();
            this.chkOwned = new System.Windows.Forms.CheckBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cmdSearchClear = new System.Windows.Forms.Button();
            this.numIdRangeMax = new System.Windows.Forms.NumericUpDown();
            this.numIdRangeMin = new System.Windows.Forms.NumericUpDown();
            this.lblIdRangeSep = new System.Windows.Forms.Label();
            this.chkIdRange = new System.Windows.Forms.CheckBox();
            this.lstGames = new Depressurizer.Lib.ExtListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGenre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colScraped = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAppInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colParent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdUpdateHltb = new System.Windows.Forms.Button();
            this.CheckShowIgnored = new System.Windows.Forms.CheckBox();
            this.chkCommunityInsteadStore = new System.Windows.Forms.CheckBox();
            this.mainMenu.SuspendLayout();
            this.grpTypes.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grpWebUpdate.SuspendLayout();
            this.grpAppInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIdRangeMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdRangeMin)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File});
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.Name = "mainMenu";
            // 
            // menu_File
            // 
            this.menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File_Save,
            this.menu_File_SaveAs,
            this.menu_File_Load,
            this.menu_File_Sep1,
            this.menu_File_Clear,
            this.menu_File_Sep2,
            this.menu_File_Exit});
            this.menu_File.Name = "menu_File";
            resources.ApplyResources(this.menu_File, "menu_File");
            // 
            // menu_File_Save
            // 
            this.menu_File_Save.Name = "menu_File_Save";
            resources.ApplyResources(this.menu_File_Save, "menu_File_Save");
            this.menu_File_Save.Click += new System.EventHandler(this.menu_File_Save_Click);
            // 
            // menu_File_SaveAs
            // 
            this.menu_File_SaveAs.Name = "menu_File_SaveAs";
            resources.ApplyResources(this.menu_File_SaveAs, "menu_File_SaveAs");
            this.menu_File_SaveAs.Click += new System.EventHandler(this.menu_File_SaveAs_Click);
            // 
            // menu_File_Load
            // 
            this.menu_File_Load.Name = "menu_File_Load";
            resources.ApplyResources(this.menu_File_Load, "menu_File_Load");
            this.menu_File_Load.Click += new System.EventHandler(this.menu_File_Load_Click);
            // 
            // menu_File_Sep1
            // 
            this.menu_File_Sep1.Name = "menu_File_Sep1";
            resources.ApplyResources(this.menu_File_Sep1, "menu_File_Sep1");
            // 
            // menu_File_Clear
            // 
            this.menu_File_Clear.Name = "menu_File_Clear";
            resources.ApplyResources(this.menu_File_Clear, "menu_File_Clear");
            this.menu_File_Clear.Click += new System.EventHandler(this.menu_File_Clear_Click);
            // 
            // menu_File_Sep2
            // 
            this.menu_File_Sep2.Name = "menu_File_Sep2";
            resources.ApplyResources(this.menu_File_Sep2, "menu_File_Sep2");
            // 
            // menu_File_Exit
            // 
            this.menu_File_Exit.Name = "menu_File_Exit";
            resources.ApplyResources(this.menu_File_Exit, "menu_File_Exit");
            this.menu_File_Exit.Click += new System.EventHandler(this.menu_File_Exit_Click);
            // 
            // cmdFetch
            // 
            resources.ApplyResources(this.cmdFetch, "cmdFetch");
            this.cmdFetch.Name = "cmdFetch";
            this.cmdFetch.UseVisualStyleBackColor = true;
            this.cmdFetch.Click += new System.EventHandler(this.cmdFetch_Click);
            // 
            // cmdUpdateNew
            // 
            resources.ApplyResources(this.cmdUpdateNew, "cmdUpdateNew");
            this.cmdUpdateNew.Name = "cmdUpdateNew";
            this.cmdUpdateNew.UseVisualStyleBackColor = true;
            this.cmdUpdateNew.Click += new System.EventHandler(this.cmdUpdateUnchecked_Click);
            // 
            // cmdUpdateSelected
            // 
            resources.ApplyResources(this.cmdUpdateSelected, "cmdUpdateSelected");
            this.cmdUpdateSelected.Name = "cmdUpdateSelected";
            this.cmdUpdateSelected.UseVisualStyleBackColor = true;
            this.cmdUpdateSelected.Click += new System.EventHandler(this.cmdUpdateSelected_Click);
            // 
            // cmdEditGame
            // 
            resources.ApplyResources(this.cmdEditGame, "cmdEditGame");
            this.cmdEditGame.Name = "cmdEditGame";
            this.cmdEditGame.UseVisualStyleBackColor = true;
            this.cmdEditGame.Click += new System.EventHandler(this.cmdEditGame_Click);
            // 
            // cmdDeleteGame
            // 
            resources.ApplyResources(this.cmdDeleteGame, "cmdDeleteGame");
            this.cmdDeleteGame.Name = "cmdDeleteGame";
            this.cmdDeleteGame.UseVisualStyleBackColor = true;
            this.cmdDeleteGame.Click += new System.EventHandler(this.cmdDeleteGame_Click);
            // 
            // cmdAddGame
            // 
            resources.ApplyResources(this.cmdAddGame, "cmdAddGame");
            this.cmdAddGame.Name = "cmdAddGame";
            this.cmdAddGame.UseVisualStyleBackColor = true;
            this.cmdAddGame.Click += new System.EventHandler(this.cmdAddGame_Click);
            // 
            // grpTypes
            // 
            resources.ApplyResources(this.grpTypes, "grpTypes");
            this.grpTypes.Controls.Add(this.chkTypeUnknown);
            this.grpTypes.Controls.Add(this.chkTypeOther);
            this.grpTypes.Controls.Add(this.chkTypeDLC);
            this.grpTypes.Controls.Add(this.chkTypeGame);
            this.grpTypes.Controls.Add(this.chkTypeAll);
            this.grpTypes.Name = "grpTypes";
            this.grpTypes.TabStop = false;
            // 
            // chkTypeUnknown
            // 
            resources.ApplyResources(this.chkTypeUnknown, "chkTypeUnknown");
            this.chkTypeUnknown.Name = "chkTypeUnknown";
            this.chkTypeUnknown.UseVisualStyleBackColor = true;
            this.chkTypeUnknown.CheckedChanged += new System.EventHandler(this.chkType_CheckedChanged);
            // 
            // chkTypeOther
            // 
            resources.ApplyResources(this.chkTypeOther, "chkTypeOther");
            this.chkTypeOther.Name = "chkTypeOther";
            this.chkTypeOther.UseVisualStyleBackColor = true;
            this.chkTypeOther.CheckedChanged += new System.EventHandler(this.chkType_CheckedChanged);
            // 
            // chkTypeDLC
            // 
            resources.ApplyResources(this.chkTypeDLC, "chkTypeDLC");
            this.chkTypeDLC.Name = "chkTypeDLC";
            this.chkTypeDLC.UseVisualStyleBackColor = true;
            this.chkTypeDLC.CheckedChanged += new System.EventHandler(this.chkType_CheckedChanged);
            // 
            // chkTypeGame
            // 
            resources.ApplyResources(this.chkTypeGame, "chkTypeGame");
            this.chkTypeGame.Name = "chkTypeGame";
            this.chkTypeGame.UseVisualStyleBackColor = true;
            this.chkTypeGame.CheckedChanged += new System.EventHandler(this.chkType_CheckedChanged);
            // 
            // chkTypeAll
            // 
            resources.ApplyResources(this.chkTypeAll, "chkTypeAll");
            this.chkTypeAll.Checked = true;
            this.chkTypeAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTypeAll.Name = "chkTypeAll";
            this.chkTypeAll.UseVisualStyleBackColor = true;
            this.chkTypeAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // cmdStore
            // 
            resources.ApplyResources(this.cmdStore, "cmdStore");
            this.cmdStore.Name = "cmdStore";
            this.cmdStore.UseVisualStyleBackColor = true;
            this.cmdStore.Click += new System.EventHandler(this.cmdStore_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMsg,
            this.statSelected});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // statusMsg
            // 
            this.statusMsg.Name = "statusMsg";
            resources.ApplyResources(this.statusMsg, "statusMsg");
            this.statusMsg.Spring = true;
            // 
            // statSelected
            // 
            this.statSelected.Name = "statSelected";
            resources.ApplyResources(this.statSelected, "statSelected");
            // 
            // cmdUpdateAppInfo
            // 
            resources.ApplyResources(this.cmdUpdateAppInfo, "cmdUpdateAppInfo");
            this.cmdUpdateAppInfo.Name = "cmdUpdateAppInfo";
            this.cmdUpdateAppInfo.UseVisualStyleBackColor = true;
            this.cmdUpdateAppInfo.Click += new System.EventHandler(this.cmdUpdateAppInfo_Click);
            // 
            // grpWebUpdate
            // 
            resources.ApplyResources(this.grpWebUpdate, "grpWebUpdate");
            this.grpWebUpdate.Controls.Add(this.radWebNo);
            this.grpWebUpdate.Controls.Add(this.radWebSince);
            this.grpWebUpdate.Controls.Add(this.radWebYes);
            this.grpWebUpdate.Controls.Add(this.radWebAll);
            this.grpWebUpdate.Controls.Add(this.dateWeb);
            this.grpWebUpdate.Name = "grpWebUpdate";
            this.grpWebUpdate.TabStop = false;
            // 
            // radWebNo
            // 
            resources.ApplyResources(this.radWebNo, "radWebNo");
            this.radWebNo.Name = "radWebNo";
            this.radWebNo.UseVisualStyleBackColor = true;
            this.radWebNo.CheckedChanged += new System.EventHandler(this.radWeb_CheckedChanged);
            // 
            // radWebSince
            // 
            resources.ApplyResources(this.radWebSince, "radWebSince");
            this.radWebSince.Name = "radWebSince";
            this.radWebSince.UseVisualStyleBackColor = true;
            this.radWebSince.CheckedChanged += new System.EventHandler(this.radWeb_CheckedChanged);
            // 
            // radWebYes
            // 
            resources.ApplyResources(this.radWebYes, "radWebYes");
            this.radWebYes.Name = "radWebYes";
            this.radWebYes.UseVisualStyleBackColor = true;
            this.radWebYes.CheckedChanged += new System.EventHandler(this.radWeb_CheckedChanged);
            // 
            // radWebAll
            // 
            resources.ApplyResources(this.radWebAll, "radWebAll");
            this.radWebAll.Checked = true;
            this.radWebAll.Name = "radWebAll";
            this.radWebAll.TabStop = true;
            this.radWebAll.UseVisualStyleBackColor = true;
            this.radWebAll.CheckedChanged += new System.EventHandler(this.radWeb_CheckedChanged);
            // 
            // dateWeb
            // 
            this.dateWeb.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateWeb, "dateWeb");
            this.dateWeb.Name = "dateWeb";
            this.dateWeb.ValueChanged += new System.EventHandler(this.dateWeb_ValueChanged);
            // 
            // grpAppInfo
            // 
            resources.ApplyResources(this.grpAppInfo, "grpAppInfo");
            this.grpAppInfo.Controls.Add(this.radAppNo);
            this.grpAppInfo.Controls.Add(this.radAppYes);
            this.grpAppInfo.Controls.Add(this.radAppAll);
            this.grpAppInfo.Name = "grpAppInfo";
            this.grpAppInfo.TabStop = false;
            // 
            // radAppNo
            // 
            resources.ApplyResources(this.radAppNo, "radAppNo");
            this.radAppNo.Name = "radAppNo";
            this.radAppNo.UseVisualStyleBackColor = true;
            this.radAppNo.CheckedChanged += new System.EventHandler(this.radApp_CheckedChanged);
            // 
            // radAppYes
            // 
            resources.ApplyResources(this.radAppYes, "radAppYes");
            this.radAppYes.Name = "radAppYes";
            this.radAppYes.UseVisualStyleBackColor = true;
            this.radAppYes.CheckedChanged += new System.EventHandler(this.radApp_CheckedChanged);
            // 
            // radAppAll
            // 
            resources.ApplyResources(this.radAppAll, "radAppAll");
            this.radAppAll.Checked = true;
            this.radAppAll.Name = "radAppAll";
            this.radAppAll.TabStop = true;
            this.radAppAll.UseVisualStyleBackColor = true;
            this.radAppAll.CheckedChanged += new System.EventHandler(this.radApp_CheckedChanged);
            // 
            // chkOwned
            // 
            resources.ApplyResources(this.chkOwned, "chkOwned");
            this.chkOwned.Checked = true;
            this.chkOwned.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOwned.Name = "chkOwned";
            this.chkOwned.UseVisualStyleBackColor = true;
            this.chkOwned.CheckedChanged += new System.EventHandler(this.chkOwned_CheckedChanged);
            // 
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            resources.ApplyResources(this.lblSearch, "lblSearch");
            this.lblSearch.Name = "lblSearch";
            // 
            // cmdSearchClear
            // 
            resources.ApplyResources(this.cmdSearchClear, "cmdSearchClear");
            this.cmdSearchClear.Name = "cmdSearchClear";
            this.cmdSearchClear.UseVisualStyleBackColor = true;
            this.cmdSearchClear.Click += new System.EventHandler(this.cmdSearchClear_Click);
            // 
            // numIdRangeMax
            // 
            resources.ApplyResources(this.numIdRangeMax, "numIdRangeMax");
            this.numIdRangeMax.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numIdRangeMax.Name = "numIdRangeMax";
            this.numIdRangeMax.Value = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numIdRangeMax.ValueChanged += new System.EventHandler(this.IdFilter_Changed);
            // 
            // numIdRangeMin
            // 
            resources.ApplyResources(this.numIdRangeMin, "numIdRangeMin");
            this.numIdRangeMin.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numIdRangeMin.Name = "numIdRangeMin";
            this.numIdRangeMin.ValueChanged += new System.EventHandler(this.IdFilter_Changed);
            // 
            // lblIdRangeSep
            // 
            resources.ApplyResources(this.lblIdRangeSep, "lblIdRangeSep");
            this.lblIdRangeSep.Name = "lblIdRangeSep";
            // 
            // chkIdRange
            // 
            resources.ApplyResources(this.chkIdRange, "chkIdRange");
            this.chkIdRange.Name = "chkIdRange";
            this.chkIdRange.UseVisualStyleBackColor = true;
            this.chkIdRange.CheckedChanged += new System.EventHandler(this.IdFilter_Changed);
            // 
            // lstGames
            // 
            resources.ApplyResources(this.lstGames, "lstGames");
            this.lstGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colName,
            this.colGenre,
            this.colType,
            this.colScraped,
            this.colAppInfo,
            this.colParent});
            this.lstGames.FullRowSelect = true;
            this.lstGames.GridLines = true;
            this.lstGames.HideSelection = false;
            this.lstGames.Name = "lstGames";
            this.lstGames.UseCompatibleStateImageBehavior = false;
            this.lstGames.View = System.Windows.Forms.View.Details;
            this.lstGames.VirtualMode = true;
            this.lstGames.SelectionChanged += new System.EventHandler(this.lstGames_SelectedIndexChanged);
            this.lstGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstGames_ColumnClick);
            this.lstGames.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lstGames_RetrieveVirtualItem);
            this.lstGames.SearchForVirtualItem += new System.Windows.Forms.SearchForVirtualItemEventHandler(this.lstGames_SearchForVirtualItem);
            this.lstGames.DoubleClick += new System.EventHandler(this.lstGames_DoubleClick);
            this.lstGames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstGames_KeyDown);
            // 
            // colID
            // 
            resources.ApplyResources(this.colID, "colID");
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            // 
            // colGenre
            // 
            resources.ApplyResources(this.colGenre, "colGenre");
            // 
            // colType
            // 
            resources.ApplyResources(this.colType, "colType");
            // 
            // colScraped
            // 
            resources.ApplyResources(this.colScraped, "colScraped");
            // 
            // colAppInfo
            // 
            resources.ApplyResources(this.colAppInfo, "colAppInfo");
            // 
            // colParent
            // 
            resources.ApplyResources(this.colParent, "colParent");
            // 
            // cmdUpdateHltb
            // 
            resources.ApplyResources(this.cmdUpdateHltb, "cmdUpdateHltb");
            this.cmdUpdateHltb.Name = "cmdUpdateHltb";
            this.cmdUpdateHltb.UseVisualStyleBackColor = true;
            this.cmdUpdateHltb.Click += new System.EventHandler(this.cmdUpdateHltb_Click);
            // 
            // CheckShowIgnored
            // 
            resources.ApplyResources(this.CheckShowIgnored, "CheckShowIgnored");
            this.CheckShowIgnored.Name = "CheckShowIgnored";
            this.CheckShowIgnored.UseVisualStyleBackColor = true;
            this.CheckShowIgnored.CheckedChanged += new System.EventHandler(this.CheckShowIgnored_CheckedChanged);
            // 
            // chkCommunityInsteadStore
            // 
            resources.ApplyResources(this.chkCommunityInsteadStore, "chkCommunityInsteadStore");
            this.chkCommunityInsteadStore.Checked = true;
            this.chkCommunityInsteadStore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCommunityInsteadStore.Name = "chkCommunityInsteadStore";
            this.chkCommunityInsteadStore.UseVisualStyleBackColor = true;
            this.chkCommunityInsteadStore.CheckedChanged += new System.EventHandler(this.ChkCommunityInsteadStore_CheckedChanged);
            // 
            // DBEditDlg
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkCommunityInsteadStore);
            this.Controls.Add(this.CheckShowIgnored);
            this.Controls.Add(this.cmdUpdateHltb);
            this.Controls.Add(this.chkIdRange);
            this.Controls.Add(this.lblIdRangeSep);
            this.Controls.Add(this.numIdRangeMin);
            this.Controls.Add(this.numIdRangeMax);
            this.Controls.Add(this.cmdSearchClear);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.chkOwned);
            this.Controls.Add(this.grpAppInfo);
            this.Controls.Add(this.grpWebUpdate);
            this.Controls.Add(this.cmdUpdateAppInfo);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdStore);
            this.Controls.Add(this.grpTypes);
            this.Controls.Add(this.cmdAddGame);
            this.Controls.Add(this.cmdDeleteGame);
            this.Controls.Add(this.cmdEditGame);
            this.Controls.Add(this.cmdUpdateSelected);
            this.Controls.Add(this.cmdUpdateNew);
            this.Controls.Add(this.cmdFetch);
            this.Controls.Add(this.lstGames);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "DBEditDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBEditDlg_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.grpTypes.ResumeLayout(false);
            this.grpTypes.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grpWebUpdate.ResumeLayout(false);
            this.grpWebUpdate.PerformLayout();
            this.grpAppInfo.ResumeLayout(false);
            this.grpAppInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIdRangeMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdRangeMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menu_File;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Load;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Save;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Exit;
        private Depressurizer.Lib.ExtListView lstGames;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colGenre;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.Button cmdFetch;
        private System.Windows.Forms.Button cmdUpdateNew;
        private System.Windows.Forms.Button cmdUpdateSelected;
        private System.Windows.Forms.Button cmdEditGame;
        private System.Windows.Forms.Button cmdDeleteGame;
        private System.Windows.Forms.Button cmdAddGame;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep1;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Clear;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep2;
        private System.Windows.Forms.GroupBox grpTypes;
        private System.Windows.Forms.CheckBox chkTypeOther;
        private System.Windows.Forms.CheckBox chkTypeDLC;
        private System.Windows.Forms.CheckBox chkTypeGame;
        private System.Windows.Forms.CheckBox chkTypeAll;
        private System.Windows.Forms.Button cmdStore;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statSelected;
        private System.Windows.Forms.CheckBox chkTypeUnknown;
        private System.Windows.Forms.ToolStripStatusLabel statusMsg;
        private System.Windows.Forms.ToolStripMenuItem menu_File_SaveAs;
        private System.Windows.Forms.Button cmdUpdateAppInfo;
        private System.Windows.Forms.GroupBox grpWebUpdate;
        private System.Windows.Forms.RadioButton radWebNo;
        private System.Windows.Forms.RadioButton radWebSince;
        private System.Windows.Forms.RadioButton radWebYes;
        private System.Windows.Forms.RadioButton radWebAll;
        private System.Windows.Forms.DateTimePicker dateWeb;
        private System.Windows.Forms.GroupBox grpAppInfo;
        private System.Windows.Forms.RadioButton radAppYes;
        private System.Windows.Forms.RadioButton radAppAll;
        private System.Windows.Forms.ColumnHeader colScraped;
        private System.Windows.Forms.ColumnHeader colAppInfo;
        private System.Windows.Forms.ColumnHeader colParent;
        private System.Windows.Forms.RadioButton radAppNo;
        private System.Windows.Forms.CheckBox chkOwned;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button cmdSearchClear;
        private System.Windows.Forms.NumericUpDown numIdRangeMax;
        private System.Windows.Forms.NumericUpDown numIdRangeMin;
        private System.Windows.Forms.Label lblIdRangeSep;
        private System.Windows.Forms.CheckBox chkIdRange;
        private System.Windows.Forms.Button cmdUpdateHltb;
        private System.Windows.Forms.CheckBox CheckShowIgnored;
        private System.Windows.Forms.CheckBox chkCommunityInsteadStore;
    }
}

