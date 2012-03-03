namespace SteamScrape {
    partial class MainForm {
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_SaveRaw = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_File_SavePruned = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Unknown = new System.Windows.Forms.CheckBox();
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
            this.statSelected = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(669, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // menu_File
            // 
            this.menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File_Load,
            this.menu_File_SaveRaw,
            this.menu_File_SavePruned,
            this.menu_File_Sep1,
            this.menu_File_Clear,
            this.menu_File_Sep2,
            this.menu_File_Exit});
            this.menu_File.Name = "menu_File";
            this.menu_File.Size = new System.Drawing.Size(37, 20);
            this.menu_File.Text = "File";
            // 
            // menu_File_Load
            // 
            this.menu_File_Load.Name = "menu_File_Load";
            this.menu_File_Load.Size = new System.Drawing.Size(148, 22);
            this.menu_File_Load.Text = "Load...";
            this.menu_File_Load.Click += new System.EventHandler(this.menu_File_Load_Click);
            // 
            // menu_File_SaveRaw
            // 
            this.menu_File_SaveRaw.Name = "menu_File_SaveRaw";
            this.menu_File_SaveRaw.Size = new System.Drawing.Size(148, 22);
            this.menu_File_SaveRaw.Text = "Save Raw...";
            this.menu_File_SaveRaw.Click += new System.EventHandler(this.menu_File_SaveRaw_Click);
            // 
            // menu_File_SavePruned
            // 
            this.menu_File_SavePruned.Name = "menu_File_SavePruned";
            this.menu_File_SavePruned.Size = new System.Drawing.Size(148, 22);
            this.menu_File_SavePruned.Text = "Save Pruned...";
            this.menu_File_SavePruned.Click += new System.EventHandler(this.menu_File_SavePruned_Click);
            // 
            // menu_File_Sep1
            // 
            this.menu_File_Sep1.Name = "menu_File_Sep1";
            this.menu_File_Sep1.Size = new System.Drawing.Size(145, 6);
            // 
            // menu_File_Clear
            // 
            this.menu_File_Clear.Name = "menu_File_Clear";
            this.menu_File_Clear.Size = new System.Drawing.Size(148, 22);
            this.menu_File_Clear.Text = "Clear";
            this.menu_File_Clear.Click += new System.EventHandler(this.menu_File_Clear_Click);
            // 
            // menu_File_Sep2
            // 
            this.menu_File_Sep2.Name = "menu_File_Sep2";
            this.menu_File_Sep2.Size = new System.Drawing.Size(145, 6);
            // 
            // menu_File_Exit
            // 
            this.menu_File_Exit.Name = "menu_File_Exit";
            this.menu_File_Exit.Size = new System.Drawing.Size(148, 22);
            this.menu_File_Exit.Text = "Exit";
            this.menu_File_Exit.Click += new System.EventHandler(this.menu_File_Exit_Click);
            // 
            // lstGames
            // 
            this.lstGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colID,
            this.colGenre,
            this.colType});
            this.lstGames.FullRowSelect = true;
            this.lstGames.GridLines = true;
            this.lstGames.Location = new System.Drawing.Point(12, 27);
            this.lstGames.Name = "lstGames";
            this.lstGames.Size = new System.Drawing.Size(525, 390);
            this.lstGames.TabIndex = 0;
            this.lstGames.UseCompatibleStateImageBehavior = false;
            this.lstGames.View = System.Windows.Forms.View.Details;
            this.lstGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstGames_ColumnClick);
            this.lstGames.SelectedIndexChanged += new System.EventHandler(this.lstGames_SelectedIndexChanged);
            // 
            // colName
            // 
            this.colName.DisplayIndex = 1;
            this.colName.Text = "Name";
            this.colName.Width = 266;
            // 
            // colID
            // 
            this.colID.DisplayIndex = 0;
            this.colID.Text = "ID";
            // 
            // colGenre
            // 
            this.colGenre.Text = "Genre";
            this.colGenre.Width = 105;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 87;
            // 
            // cmdFetch
            // 
            this.cmdFetch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFetch.Location = new System.Drawing.Point(543, 27);
            this.cmdFetch.Name = "cmdFetch";
            this.cmdFetch.Size = new System.Drawing.Size(114, 23);
            this.cmdFetch.TabIndex = 1;
            this.cmdFetch.Text = "Fetch List";
            this.cmdFetch.UseVisualStyleBackColor = true;
            this.cmdFetch.Click += new System.EventHandler(this.cmdFetch_Click);
            // 
            // cmdUpdateUnchecked
            // 
            this.cmdUpdateUnchecked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdateUnchecked.Location = new System.Drawing.Point(543, 391);
            this.cmdUpdateUnchecked.Name = "cmdUpdateUnchecked";
            this.cmdUpdateUnchecked.Size = new System.Drawing.Size(114, 23);
            this.cmdUpdateUnchecked.TabIndex = 6;
            this.cmdUpdateUnchecked.Text = "Update New";
            this.cmdUpdateUnchecked.UseVisualStyleBackColor = true;
            this.cmdUpdateUnchecked.Click += new System.EventHandler(this.cmdUpdateUnchecked_Click);
            // 
            // cmdUpdateSelected
            // 
            this.cmdUpdateSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdateSelected.Location = new System.Drawing.Point(543, 367);
            this.cmdUpdateSelected.Name = "cmdUpdateSelected";
            this.cmdUpdateSelected.Size = new System.Drawing.Size(114, 23);
            this.cmdUpdateSelected.TabIndex = 5;
            this.cmdUpdateSelected.Text = "Update Selected";
            this.cmdUpdateSelected.UseVisualStyleBackColor = true;
            this.cmdUpdateSelected.Click += new System.EventHandler(this.cmdUpdateSelected_Click);
            // 
            // cmdEditGame
            // 
            this.cmdEditGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEditGame.Location = new System.Drawing.Point(543, 334);
            this.cmdEditGame.Name = "cmdEditGame";
            this.cmdEditGame.Size = new System.Drawing.Size(114, 23);
            this.cmdEditGame.TabIndex = 4;
            this.cmdEditGame.Text = "Edit Game";
            this.cmdEditGame.UseVisualStyleBackColor = true;
            this.cmdEditGame.Click += new System.EventHandler(this.cmdEditGame_Click);
            // 
            // cmdDeleteGame
            // 
            this.cmdDeleteGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDeleteGame.Location = new System.Drawing.Point(543, 310);
            this.cmdDeleteGame.Name = "cmdDeleteGame";
            this.cmdDeleteGame.Size = new System.Drawing.Size(114, 23);
            this.cmdDeleteGame.TabIndex = 3;
            this.cmdDeleteGame.Text = "Delete Game";
            this.cmdDeleteGame.UseVisualStyleBackColor = true;
            this.cmdDeleteGame.Click += new System.EventHandler(this.cmdDeleteGame_Click);
            // 
            // cmdAddGame
            // 
            this.cmdAddGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAddGame.Location = new System.Drawing.Point(543, 286);
            this.cmdAddGame.Name = "cmdAddGame";
            this.cmdAddGame.Size = new System.Drawing.Size(114, 23);
            this.cmdAddGame.TabIndex = 2;
            this.cmdAddGame.Text = "Add Game";
            this.cmdAddGame.UseVisualStyleBackColor = true;
            this.cmdAddGame.Click += new System.EventHandler(this.cmdAddGame_Click);
            // 
            // grpFilter
            // 
            this.grpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFilter.Controls.Add(this.Unknown);
            this.grpFilter.Controls.Add(this.chkWebError);
            this.grpFilter.Controls.Add(this.chkNonApp);
            this.grpFilter.Controls.Add(this.chkRedirect);
            this.grpFilter.Controls.Add(this.chkNotFound);
            this.grpFilter.Controls.Add(this.chkDLC);
            this.grpFilter.Controls.Add(this.chkGame);
            this.grpFilter.Controls.Add(this.chkSiteError);
            this.grpFilter.Controls.Add(this.chkNew);
            this.grpFilter.Controls.Add(this.chkAll);
            this.grpFilter.Location = new System.Drawing.Point(547, 56);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(110, 193);
            this.grpFilter.TabIndex = 8;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Filter";
            // 
            // Unknown
            // 
            this.Unknown.AutoSize = true;
            this.Unknown.Location = new System.Drawing.Point(6, 172);
            this.Unknown.Name = "Unknown";
            this.Unknown.Size = new System.Drawing.Size(72, 17);
            this.Unknown.TabIndex = 9;
            this.Unknown.Text = "Unknown";
            this.Unknown.UseVisualStyleBackColor = true;
            this.Unknown.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkWebError
            // 
            this.chkWebError.AutoSize = true;
            this.chkWebError.Location = new System.Drawing.Point(6, 155);
            this.chkWebError.Name = "chkWebError";
            this.chkWebError.Size = new System.Drawing.Size(71, 17);
            this.chkWebError.TabIndex = 8;
            this.chkWebError.Text = "WebError";
            this.chkWebError.UseVisualStyleBackColor = true;
            this.chkWebError.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkNonApp
            // 
            this.chkNonApp.AutoSize = true;
            this.chkNonApp.Location = new System.Drawing.Point(6, 104);
            this.chkNonApp.Name = "chkNonApp";
            this.chkNonApp.Size = new System.Drawing.Size(65, 17);
            this.chkNonApp.TabIndex = 7;
            this.chkNonApp.Text = "NonApp";
            this.chkNonApp.UseVisualStyleBackColor = true;
            this.chkNonApp.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkRedirect
            // 
            this.chkRedirect.AutoSize = true;
            this.chkRedirect.Location = new System.Drawing.Point(6, 87);
            this.chkRedirect.Name = "chkRedirect";
            this.chkRedirect.Size = new System.Drawing.Size(75, 17);
            this.chkRedirect.TabIndex = 6;
            this.chkRedirect.Text = "IdRedirect";
            this.chkRedirect.UseVisualStyleBackColor = true;
            this.chkRedirect.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkNotFound
            // 
            this.chkNotFound.AutoSize = true;
            this.chkNotFound.Location = new System.Drawing.Point(6, 121);
            this.chkNotFound.Name = "chkNotFound";
            this.chkNotFound.Size = new System.Drawing.Size(73, 17);
            this.chkNotFound.TabIndex = 5;
            this.chkNotFound.Text = "NotFound";
            this.chkNotFound.UseVisualStyleBackColor = true;
            this.chkNotFound.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkDLC
            // 
            this.chkDLC.AutoSize = true;
            this.chkDLC.Location = new System.Drawing.Point(6, 70);
            this.chkDLC.Name = "chkDLC";
            this.chkDLC.Size = new System.Drawing.Size(47, 17);
            this.chkDLC.TabIndex = 4;
            this.chkDLC.Text = "DLC";
            this.chkDLC.UseVisualStyleBackColor = true;
            this.chkDLC.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkGame
            // 
            this.chkGame.AutoSize = true;
            this.chkGame.Location = new System.Drawing.Point(6, 53);
            this.chkGame.Name = "chkGame";
            this.chkGame.Size = new System.Drawing.Size(54, 17);
            this.chkGame.TabIndex = 3;
            this.chkGame.Text = "Game";
            this.chkGame.UseVisualStyleBackColor = true;
            this.chkGame.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkSiteError
            // 
            this.chkSiteError.AutoSize = true;
            this.chkSiteError.Location = new System.Drawing.Point(6, 138);
            this.chkSiteError.Name = "chkSiteError";
            this.chkSiteError.Size = new System.Drawing.Size(66, 17);
            this.chkSiteError.TabIndex = 2;
            this.chkSiteError.Text = "SiteError";
            this.chkSiteError.UseVisualStyleBackColor = true;
            this.chkSiteError.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkNew
            // 
            this.chkNew.AutoSize = true;
            this.chkNew.Location = new System.Drawing.Point(6, 36);
            this.chkNew.Name = "chkNew";
            this.chkNew.Size = new System.Drawing.Size(48, 17);
            this.chkNew.TabIndex = 1;
            this.chkNew.Text = "New";
            this.chkNew.UseVisualStyleBackColor = true;
            this.chkNew.CheckedChanged += new System.EventHandler(this.chkAny_CheckedChanged);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.Location = new System.Drawing.Point(6, 19);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(37, 17);
            this.chkAll.TabIndex = 0;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // cmdStore
            // 
            this.cmdStore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdStore.Location = new System.Drawing.Point(543, 255);
            this.cmdStore.Name = "cmdStore";
            this.cmdStore.Size = new System.Drawing.Size(114, 23);
            this.cmdStore.TabIndex = 9;
            this.cmdStore.Text = "View Store";
            this.cmdStore.UseVisualStyleBackColor = true;
            this.cmdStore.Click += new System.EventHandler(this.cmdStore_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statSelected});
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(669, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statSelected
            // 
            this.statSelected.Name = "statSelected";
            this.statSelected.Size = new System.Drawing.Size(654, 17);
            this.statSelected.Spring = true;
            this.statSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 442);
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
            this.MinimumSize = new System.Drawing.Size(685, 480);
            this.Name = "MainForm";
            this.Text = "SteamScrape";
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
        private System.Windows.Forms.ToolStripMenuItem menu_File_SaveRaw;
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
        private System.Windows.Forms.ToolStripMenuItem menu_File_SavePruned;
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
        private System.Windows.Forms.CheckBox Unknown;
        private System.Windows.Forms.CheckBox chkWebError;
    }
}

