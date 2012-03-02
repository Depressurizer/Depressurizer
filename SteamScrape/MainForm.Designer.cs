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
            this.cmdUpdateError = new System.Windows.Forms.Button();
            this.cmdFetch = new System.Windows.Forms.Button();
            this.cmdUpdateUnchecked = new System.Windows.Forms.Button();
            this.cmdUpdateSelected = new System.Windows.Forms.Button();
            this.cmdEditGame = new System.Windows.Forms.Button();
            this.cmdDeleteGame = new System.Windows.Forms.Button();
            this.cmdAddGame = new System.Windows.Forms.Button();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(699, 24);
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
            this.lstGames.Size = new System.Drawing.Size(555, 449);
            this.lstGames.TabIndex = 0;
            this.lstGames.UseCompatibleStateImageBehavior = false;
            this.lstGames.View = System.Windows.Forms.View.Details;
            this.lstGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstGames_ColumnClick);
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
            // cmdUpdateError
            // 
            this.cmdUpdateError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdateError.Location = new System.Drawing.Point(573, 453);
            this.cmdUpdateError.Name = "cmdUpdateError";
            this.cmdUpdateError.Size = new System.Drawing.Size(114, 23);
            this.cmdUpdateError.TabIndex = 7;
            this.cmdUpdateError.Text = "Update Error";
            this.cmdUpdateError.UseVisualStyleBackColor = true;
            this.cmdUpdateError.Click += new System.EventHandler(this.cmdUpdateError_Click);
            // 
            // cmdFetch
            // 
            this.cmdFetch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFetch.Location = new System.Drawing.Point(573, 27);
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
            this.cmdUpdateUnchecked.Location = new System.Drawing.Point(573, 424);
            this.cmdUpdateUnchecked.Name = "cmdUpdateUnchecked";
            this.cmdUpdateUnchecked.Size = new System.Drawing.Size(114, 23);
            this.cmdUpdateUnchecked.TabIndex = 6;
            this.cmdUpdateUnchecked.Text = "Update Unchecked";
            this.cmdUpdateUnchecked.UseVisualStyleBackColor = true;
            this.cmdUpdateUnchecked.Click += new System.EventHandler(this.cmdUpdateUnchecked_Click);
            // 
            // cmdUpdateSelected
            // 
            this.cmdUpdateSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdateSelected.Location = new System.Drawing.Point(573, 395);
            this.cmdUpdateSelected.Name = "cmdUpdateSelected";
            this.cmdUpdateSelected.Size = new System.Drawing.Size(114, 23);
            this.cmdUpdateSelected.TabIndex = 5;
            this.cmdUpdateSelected.Text = "Update Selected";
            this.cmdUpdateSelected.UseVisualStyleBackColor = true;
            this.cmdUpdateSelected.Click += new System.EventHandler(this.cmdUpdateSelected_Click);
            // 
            // cmdEditGame
            // 
            this.cmdEditGame.Location = new System.Drawing.Point(573, 344);
            this.cmdEditGame.Name = "cmdEditGame";
            this.cmdEditGame.Size = new System.Drawing.Size(114, 23);
            this.cmdEditGame.TabIndex = 4;
            this.cmdEditGame.Text = "Edit Game";
            this.cmdEditGame.UseVisualStyleBackColor = true;
            this.cmdEditGame.Click += new System.EventHandler(this.cmdEditGame_Click);
            // 
            // cmdDeleteGame
            // 
            this.cmdDeleteGame.Location = new System.Drawing.Point(573, 315);
            this.cmdDeleteGame.Name = "cmdDeleteGame";
            this.cmdDeleteGame.Size = new System.Drawing.Size(114, 23);
            this.cmdDeleteGame.TabIndex = 3;
            this.cmdDeleteGame.Text = "Delete Game";
            this.cmdDeleteGame.UseVisualStyleBackColor = true;
            this.cmdDeleteGame.Click += new System.EventHandler(this.cmdDeleteGame_Click);
            // 
            // cmdAddGame
            // 
            this.cmdAddGame.Location = new System.Drawing.Point(573, 286);
            this.cmdAddGame.Name = "cmdAddGame";
            this.cmdAddGame.Size = new System.Drawing.Size(114, 23);
            this.cmdAddGame.TabIndex = 2;
            this.cmdAddGame.Text = "Add Game";
            this.cmdAddGame.UseVisualStyleBackColor = true;
            this.cmdAddGame.Click += new System.EventHandler(this.cmdAddGame_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 488);
            this.Controls.Add(this.cmdAddGame);
            this.Controls.Add(this.cmdDeleteGame);
            this.Controls.Add(this.cmdEditGame);
            this.Controls.Add(this.cmdUpdateSelected);
            this.Controls.Add(this.cmdUpdateUnchecked);
            this.Controls.Add(this.cmdFetch);
            this.Controls.Add(this.cmdUpdateError);
            this.Controls.Add(this.lstGames);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "SteamScrape";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
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
        private System.Windows.Forms.Button cmdUpdateError;
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
    }
}

