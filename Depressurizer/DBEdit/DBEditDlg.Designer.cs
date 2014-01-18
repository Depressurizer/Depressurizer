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
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.chkAgeGate = new System.Windows.Forms.CheckBox();
            this.chkUnknown = new System.Windows.Forms.CheckBox();
            this.chkWebError = new System.Windows.Forms.CheckBox();
            this.chkNonApp = new System.Windows.Forms.CheckBox();
            this.chkRedirect = new System.Windows.Forms.CheckBox();
            this.chkNotFound = new System.Windows.Forms.CheckBox();
            this.chkDLC = new System.Windows.Forms.CheckBox();
            this.chkGame = new System.Windows.Forms.CheckBox();
            this.chkSiteError = new System.Windows.Forms.CheckBox();
            this.chkNew = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.cmdStore = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statSelected = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File});
            this.mainMenu.Name = "mainMenu";
            // 
            // menu_File
            // 
            resources.ApplyResources(this.menu_File, "menu_File");
            this.menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File_Save,
            this.menu_File_SaveAs,
            this.menu_File_Load,
            this.menu_File_Sep1,
            this.menu_File_Clear,
            this.menu_File_Sep2,
            this.menu_File_Exit});
            this.menu_File.Name = "menu_File";
            // 
            // menu_File_Save
            // 
            resources.ApplyResources(this.menu_File_Save, "menu_File_Save");
            this.menu_File_Save.Name = "menu_File_Save";
            this.menu_File_Save.Click += new System.EventHandler(this.menu_File_Save_Click);
            // 
            // menu_File_SaveAs
            // 
            resources.ApplyResources(this.menu_File_SaveAs, "menu_File_SaveAs");
            this.menu_File_SaveAs.Name = "menu_File_SaveAs";
            this.menu_File_SaveAs.Click += new System.EventHandler(this.menu_File_SaveAs_Click);
            // 
            // menu_File_Load
            // 
            resources.ApplyResources(this.menu_File_Load, "menu_File_Load");
            this.menu_File_Load.Name = "menu_File_Load";
            this.menu_File_Load.Click += new System.EventHandler(this.menu_File_Load_Click);
            // 
            // menu_File_Sep1
            // 
            resources.ApplyResources(this.menu_File_Sep1, "menu_File_Sep1");
            this.menu_File_Sep1.Name = "menu_File_Sep1";
            // 
            // menu_File_Clear
            // 
            resources.ApplyResources(this.menu_File_Clear, "menu_File_Clear");
            this.menu_File_Clear.Name = "menu_File_Clear";
            this.menu_File_Clear.Click += new System.EventHandler(this.menu_File_Clear_Click);
            // 
            // menu_File_Sep2
            // 
            resources.ApplyResources(this.menu_File_Sep2, "menu_File_Sep2");
            this.menu_File_Sep2.Name = "menu_File_Sep2";
            // 
            // menu_File_Exit
            // 
            resources.ApplyResources(this.menu_File_Exit, "menu_File_Exit");
            this.menu_File_Exit.Name = "menu_File_Exit";
            this.menu_File_Exit.Click += new System.EventHandler(this.menu_File_Exit_Click);
            // 
            // lstGames
            // 
            resources.ApplyResources(this.lstGames, "lstGames");
            this.lstGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colID,
            this.colGenre,
            this.colType});
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
            // grpFilter
            // 
            resources.ApplyResources(this.grpFilter, "grpFilter");
            this.grpFilter.Controls.Add(this.chkAgeGate);
            this.grpFilter.Controls.Add(this.chkUnknown);
            this.grpFilter.Controls.Add(this.chkWebError);
            this.grpFilter.Controls.Add(this.chkNonApp);
            this.grpFilter.Controls.Add(this.chkRedirect);
            this.grpFilter.Controls.Add(this.chkNotFound);
            this.grpFilter.Controls.Add(this.chkDLC);
            this.grpFilter.Controls.Add(this.chkGame);
            this.grpFilter.Controls.Add(this.chkSiteError);
            this.grpFilter.Controls.Add(this.chkNew);
            this.grpFilter.Controls.Add(this.chkAll);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.TabStop = false;
            // 
            // chkAgeGate
            // 
            resources.ApplyResources(this.chkAgeGate, "chkAgeGate");
            this.chkAgeGate.Name = "chkAgeGate";
            this.chkAgeGate.UseVisualStyleBackColor = true;
            this.chkAgeGate.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkUnknown
            // 
            resources.ApplyResources(this.chkUnknown, "chkUnknown");
            this.chkUnknown.Name = "chkUnknown";
            this.chkUnknown.UseVisualStyleBackColor = true;
            this.chkUnknown.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkWebError
            // 
            resources.ApplyResources(this.chkWebError, "chkWebError");
            this.chkWebError.Name = "chkWebError";
            this.chkWebError.UseVisualStyleBackColor = true;
            this.chkWebError.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkNonApp
            // 
            resources.ApplyResources(this.chkNonApp, "chkNonApp");
            this.chkNonApp.Name = "chkNonApp";
            this.chkNonApp.UseVisualStyleBackColor = true;
            this.chkNonApp.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkRedirect
            // 
            resources.ApplyResources(this.chkRedirect, "chkRedirect");
            this.chkRedirect.Name = "chkRedirect";
            this.chkRedirect.UseVisualStyleBackColor = true;
            this.chkRedirect.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkNotFound
            // 
            resources.ApplyResources(this.chkNotFound, "chkNotFound");
            this.chkNotFound.Name = "chkNotFound";
            this.chkNotFound.UseVisualStyleBackColor = true;
            this.chkNotFound.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkDLC
            // 
            resources.ApplyResources(this.chkDLC, "chkDLC");
            this.chkDLC.Name = "chkDLC";
            this.chkDLC.UseVisualStyleBackColor = true;
            this.chkDLC.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkGame
            // 
            resources.ApplyResources(this.chkGame, "chkGame");
            this.chkGame.Name = "chkGame";
            this.chkGame.UseVisualStyleBackColor = true;
            this.chkGame.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkSiteError
            // 
            resources.ApplyResources(this.chkSiteError, "chkSiteError");
            this.chkSiteError.Name = "chkSiteError";
            this.chkSiteError.UseVisualStyleBackColor = true;
            this.chkSiteError.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkNew
            // 
            resources.ApplyResources(this.chkNew, "chkNew");
            this.chkNew.Name = "chkNew";
            this.chkNew.UseVisualStyleBackColor = true;
            this.chkNew.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkAll
            // 
            resources.ApplyResources(this.chkAll, "chkAll");
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.Name = "chkAll";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
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
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMsg,
            this.statSelected});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // statusMsg
            // 
            resources.ApplyResources(this.statusMsg, "statusMsg");
            this.statusMsg.Name = "statusMsg";
            this.statusMsg.Spring = true;
            // 
            // statSelected
            // 
            resources.ApplyResources(this.statSelected, "statSelected");
            this.statSelected.Name = "statSelected";
            // 
            // DBEditDlg
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdStore);
            this.Controls.Add(this.grpFilter);
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
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.CheckBox chkNonApp;
        private System.Windows.Forms.CheckBox chkRedirect;
        private System.Windows.Forms.CheckBox chkNotFound;
        private System.Windows.Forms.CheckBox chkDLC;
        private System.Windows.Forms.CheckBox chkGame;
        private System.Windows.Forms.CheckBox chkSiteError;
        private System.Windows.Forms.CheckBox chkNew;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Button cmdStore;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statSelected;
        private System.Windows.Forms.CheckBox chkUnknown;
        private System.Windows.Forms.CheckBox chkWebError;
        private System.Windows.Forms.ToolStripStatusLabel statusMsg;
        private System.Windows.Forms.ToolStripMenuItem menu_File_SaveAs;
        private System.Windows.Forms.CheckBox chkAgeGate;
    }
}

