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
            this.lstGames = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGenre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdFetch = new System.Windows.Forms.Button();
            this.cmdUpdateUnchecked = new System.Windows.Forms.Button();
            this.cmdUpdateSelected = new System.Windows.Forms.Button();
            this.cmdEditGame = new System.Windows.Forms.Button();
            this.cmdDeleteGame = new System.Windows.Forms.Button();
            this.cmdAddGame = new System.Windows.Forms.Button();
            this.grpFilterTypes = new System.Windows.Forms.GroupBox();
            this.chkFilterTypeUnknown = new System.Windows.Forms.CheckBox();
            this.chkFilterTypeOther = new System.Windows.Forms.CheckBox();
            this.chkFilterTypeDLC = new System.Windows.Forms.CheckBox();
            this.chkFilterTypeGame = new System.Windows.Forms.CheckBox();
            this.chkFilterTypeAll = new System.Windows.Forms.CheckBox();
            this.cmdStore = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statSelected = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.grpFilterAppInfo = new System.Windows.Forms.GroupBox();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.colScraped = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAppInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colParent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mainMenu.SuspendLayout();
            this.grpFilterTypes.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpFilterAppInfo.SuspendLayout();
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
            // lstGames
            // 
            resources.ApplyResources(this.lstGames, "lstGames");
            this.lstGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colID,
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
            this.lstGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstGames_ColumnClick);
            this.lstGames.SelectedIndexChanged += new System.EventHandler(this.lstGames_SelectedIndexChanged);
            this.lstGames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstGames_KeyDown);
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            // 
            // colID
            // 
            resources.ApplyResources(this.colID, "colID");
            // 
            // colGenre
            // 
            resources.ApplyResources(this.colGenre, "colGenre");
            // 
            // colType
            // 
            resources.ApplyResources(this.colType, "colType");
            // 
            // cmdFetch
            // 
            resources.ApplyResources(this.cmdFetch, "cmdFetch");
            this.cmdFetch.Name = "cmdFetch";
            this.cmdFetch.UseVisualStyleBackColor = true;
            this.cmdFetch.Click += new System.EventHandler(this.cmdFetch_Click);
            // 
            // cmdUpdateUnchecked
            // 
            resources.ApplyResources(this.cmdUpdateUnchecked, "cmdUpdateUnchecked");
            this.cmdUpdateUnchecked.Name = "cmdUpdateUnchecked";
            this.cmdUpdateUnchecked.UseVisualStyleBackColor = true;
            this.cmdUpdateUnchecked.Click += new System.EventHandler(this.cmdUpdateUnchecked_Click);
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
            // grpFilterTypes
            // 
            resources.ApplyResources(this.grpFilterTypes, "grpFilterTypes");
            this.grpFilterTypes.Controls.Add(this.chkFilterTypeUnknown);
            this.grpFilterTypes.Controls.Add(this.chkFilterTypeOther);
            this.grpFilterTypes.Controls.Add(this.chkFilterTypeDLC);
            this.grpFilterTypes.Controls.Add(this.chkFilterTypeGame);
            this.grpFilterTypes.Controls.Add(this.chkFilterTypeAll);
            this.grpFilterTypes.Name = "grpFilterTypes";
            this.grpFilterTypes.TabStop = false;
            // 
            // chkFilterTypeUnknown
            // 
            resources.ApplyResources(this.chkFilterTypeUnknown, "chkFilterTypeUnknown");
            this.chkFilterTypeUnknown.Name = "chkFilterTypeUnknown";
            this.chkFilterTypeUnknown.UseVisualStyleBackColor = true;
            this.chkFilterTypeUnknown.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkFilterTypeOther
            // 
            resources.ApplyResources(this.chkFilterTypeOther, "chkFilterTypeOther");
            this.chkFilterTypeOther.Name = "chkFilterTypeOther";
            this.chkFilterTypeOther.UseVisualStyleBackColor = true;
            this.chkFilterTypeOther.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkFilterTypeDLC
            // 
            resources.ApplyResources(this.chkFilterTypeDLC, "chkFilterTypeDLC");
            this.chkFilterTypeDLC.Name = "chkFilterTypeDLC";
            this.chkFilterTypeDLC.UseVisualStyleBackColor = true;
            this.chkFilterTypeDLC.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkFilterTypeGame
            // 
            resources.ApplyResources(this.chkFilterTypeGame, "chkFilterTypeGame");
            this.chkFilterTypeGame.Name = "chkFilterTypeGame";
            this.chkFilterTypeGame.UseVisualStyleBackColor = true;
            this.chkFilterTypeGame.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkFilterTypeAll
            // 
            resources.ApplyResources(this.chkFilterTypeAll, "chkFilterTypeAll");
            this.chkFilterTypeAll.Checked = true;
            this.chkFilterTypeAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFilterTypeAll.Name = "chkFilterTypeAll";
            this.chkFilterTypeAll.UseVisualStyleBackColor = true;
            this.chkFilterTypeAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
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
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.radioButton7);
            this.groupBox1.Controls.Add(this.radioButton6);
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateTimePicker1, "dateTimePicker1");
            this.dateTimePicker1.Name = "dateTimePicker1";
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            resources.ApplyResources(this.radioButton3, "radioButton3");
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.TabStop = true;
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            resources.ApplyResources(this.radioButton4, "radioButton4");
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.TabStop = true;
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            resources.ApplyResources(this.radioButton5, "radioButton5");
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.TabStop = true;
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            resources.ApplyResources(this.radioButton6, "radioButton6");
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.TabStop = true;
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            resources.ApplyResources(this.radioButton7, "radioButton7");
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.TabStop = true;
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // grpFilterAppInfo
            // 
            resources.ApplyResources(this.grpFilterAppInfo, "grpFilterAppInfo");
            this.grpFilterAppInfo.Controls.Add(this.radioButton10);
            this.grpFilterAppInfo.Controls.Add(this.radioButton9);
            this.grpFilterAppInfo.Controls.Add(this.radioButton8);
            this.grpFilterAppInfo.Name = "grpFilterAppInfo";
            this.grpFilterAppInfo.TabStop = false;
            // 
            // radioButton8
            // 
            resources.ApplyResources(this.radioButton8, "radioButton8");
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.TabStop = true;
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // radioButton9
            // 
            resources.ApplyResources(this.radioButton9, "radioButton9");
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.TabStop = true;
            this.radioButton9.UseVisualStyleBackColor = true;
            this.radioButton9.CheckedChanged += new System.EventHandler(this.radioButton9_CheckedChanged);
            // 
            // radioButton10
            // 
            resources.ApplyResources(this.radioButton10, "radioButton10");
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.TabStop = true;
            this.radioButton10.UseVisualStyleBackColor = true;
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
            // DBEditDlg
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpFilterAppInfo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdStore);
            this.Controls.Add(this.grpFilterTypes);
            this.Controls.Add(this.cmdAddGame);
            this.Controls.Add(this.cmdDeleteGame);
            this.Controls.Add(this.cmdEditGame);
            this.Controls.Add(this.cmdUpdateSelected);
            this.Controls.Add(this.cmdUpdateUnchecked);
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
            this.grpFilterTypes.ResumeLayout(false);
            this.grpFilterTypes.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpFilterAppInfo.ResumeLayout(false);
            this.grpFilterAppInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menu_File;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Load;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Save;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Exit;
        private System.Windows.Forms.ListView lstGames;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colGenre;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.Button cmdFetch;
        private System.Windows.Forms.Button cmdUpdateUnchecked;
        private System.Windows.Forms.Button cmdUpdateSelected;
        private System.Windows.Forms.Button cmdEditGame;
        private System.Windows.Forms.Button cmdDeleteGame;
        private System.Windows.Forms.Button cmdAddGame;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep1;
        private System.Windows.Forms.ToolStripMenuItem menu_File_Clear;
        private System.Windows.Forms.ToolStripSeparator menu_File_Sep2;
        private System.Windows.Forms.GroupBox grpFilterTypes;
        private System.Windows.Forms.CheckBox chkFilterTypeOther;
        private System.Windows.Forms.CheckBox chkFilterTypeDLC;
        private System.Windows.Forms.CheckBox chkFilterTypeGame;
        private System.Windows.Forms.CheckBox chkFilterTypeAll;
        private System.Windows.Forms.Button cmdStore;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statSelected;
        private System.Windows.Forms.CheckBox chkFilterTypeUnknown;
        private System.Windows.Forms.ToolStripStatusLabel statusMsg;
        private System.Windows.Forms.ToolStripMenuItem menu_File_SaveAs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox grpFilterAppInfo;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.ColumnHeader colScraped;
        private System.Windows.Forms.ColumnHeader colAppInfo;
        private System.Windows.Forms.ColumnHeader colParent;
        private System.Windows.Forms.RadioButton radioButton10;
    }
}

