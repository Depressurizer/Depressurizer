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
            this.cmdEditAutocat = new System.Windows.Forms.Button();
            this.cmdRenameAutocat = new System.Windows.Forms.Button();
            this.cmdCreate = new System.Windows.Forms.Button();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.panEditGenre = new System.Windows.Forms.Panel();
            this.grpEditGenre = new System.Windows.Forms.GroupBox();
            this.genreTblIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.genreCmdRemoveIgnore = new System.Windows.Forms.Button();
            this.genreCmdAddIgnore = new System.Windows.Forms.Button();
            this.genreLstIgnore = new System.Windows.Forms.ListView();
            this.lblGenreCatsToIgnore = new System.Windows.Forms.Label();
            this.genreChkRemoveExisting = new System.Windows.Forms.CheckBox();
            this.genreLblMacCats = new System.Windows.Forms.Label();
            this.genreNumMaxCats = new System.Windows.Forms.NumericUpDown();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.genreTxtPrefix = new System.Windows.Forms.TextBox();
            this.genreLblPrefix = new System.Windows.Forms.Label();
            this.panEditFlags = new System.Windows.Forms.Panel();
            this.flagsGrp = new System.Windows.Forms.GroupBox();
            this.flagsLblPrefix = new System.Windows.Forms.Label();
            this.flagsTxtPrefix = new System.Windows.Forms.TextBox();
            this.flagsLstIncluded = new System.Windows.Forms.ListView();
            this.flagsLblInclude = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.grpList.SuspendLayout();
            this.panEditGenre.SuspendLayout();
            this.grpEditGenre.SuspendLayout();
            this.genreTblIgnore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.genreNumMaxCats)).BeginInit();
            this.panEditFlags.SuspendLayout();
            this.flagsGrp.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstAutoCats
            // 
            this.lstAutoCats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAutoCats.FormattingEnabled = true;
            this.lstAutoCats.IntegralHeight = false;
            this.lstAutoCats.Location = new System.Drawing.Point(6, 19);
            this.lstAutoCats.Name = "lstAutoCats";
            this.lstAutoCats.Size = new System.Drawing.Size(141, 137);
            this.lstAutoCats.TabIndex = 0;
            // 
            // cmdEditAutocat
            // 
            this.cmdEditAutocat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEditAutocat.Location = new System.Drawing.Point(6, 214);
            this.cmdEditAutocat.Name = "cmdEditAutocat";
            this.cmdEditAutocat.Size = new System.Drawing.Size(141, 23);
            this.cmdEditAutocat.TabIndex = 1;
            this.cmdEditAutocat.Text = "Delete";
            this.cmdEditAutocat.UseVisualStyleBackColor = true;
            // 
            // cmdRenameAutocat
            // 
            this.cmdRenameAutocat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRenameAutocat.Location = new System.Drawing.Point(6, 188);
            this.cmdRenameAutocat.Name = "cmdRenameAutocat";
            this.cmdRenameAutocat.Size = new System.Drawing.Size(141, 23);
            this.cmdRenameAutocat.TabIndex = 2;
            this.cmdRenameAutocat.Text = "Rename";
            this.cmdRenameAutocat.UseVisualStyleBackColor = true;
            // 
            // cmdCreate
            // 
            this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreate.Location = new System.Drawing.Point(6, 162);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(141, 23);
            this.cmdCreate.TabIndex = 1;
            this.cmdCreate.Text = "Create";
            this.cmdCreate.UseVisualStyleBackColor = true;
            // 
            // grpList
            // 
            this.grpList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpList.Controls.Add(this.cmdCreate);
            this.grpList.Controls.Add(this.lstAutoCats);
            this.grpList.Controls.Add(this.cmdRenameAutocat);
            this.grpList.Controls.Add(this.cmdEditAutocat);
            this.grpList.Location = new System.Drawing.Point(8, 8);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(153, 243);
            this.grpList.TabIndex = 4;
            this.grpList.TabStop = false;
            this.grpList.Text = "AutoCat List";
            // 
            // panEditGenre
            // 
            this.panEditGenre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panEditGenre.Controls.Add(this.grpEditGenre);
            this.panEditGenre.Location = new System.Drawing.Point(167, 8);
            this.panEditGenre.Name = "panEditGenre";
            this.panEditGenre.Size = new System.Drawing.Size(424, 243);
            this.panEditGenre.TabIndex = 5;
            // 
            // grpEditGenre
            // 
            this.grpEditGenre.Controls.Add(this.panEditFlags);
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
            this.grpEditGenre.Size = new System.Drawing.Size(424, 243);
            this.grpEditGenre.TabIndex = 0;
            this.grpEditGenre.TabStop = false;
            this.grpEditGenre.Text = "Edit Genre AutoCat";
            // 
            // genreTblIgnore
            // 
            this.genreTblIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genreTblIgnore.ColumnCount = 2;
            this.genreTblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genreTblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genreTblIgnore.Controls.Add(this.genreCmdRemoveIgnore, 1, 0);
            this.genreTblIgnore.Controls.Add(this.genreCmdAddIgnore, 0, 0);
            this.genreTblIgnore.Location = new System.Drawing.Point(9, 204);
            this.genreTblIgnore.Name = "genreTblIgnore";
            this.genreTblIgnore.RowCount = 1;
            this.genreTblIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genreTblIgnore.Size = new System.Drawing.Size(409, 30);
            this.genreTblIgnore.TabIndex = 9;
            // 
            // genreCmdRemoveIgnore
            // 
            this.genreCmdRemoveIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genreCmdRemoveIgnore.Location = new System.Drawing.Point(207, 3);
            this.genreCmdRemoveIgnore.Name = "genreCmdRemoveIgnore";
            this.genreCmdRemoveIgnore.Size = new System.Drawing.Size(199, 23);
            this.genreCmdRemoveIgnore.TabIndex = 7;
            this.genreCmdRemoveIgnore.Text = "Remove";
            this.genreCmdRemoveIgnore.UseVisualStyleBackColor = true;
            // 
            // genreCmdAddIgnore
            // 
            this.genreCmdAddIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genreCmdAddIgnore.Location = new System.Drawing.Point(3, 3);
            this.genreCmdAddIgnore.Name = "genreCmdAddIgnore";
            this.genreCmdAddIgnore.Size = new System.Drawing.Size(198, 23);
            this.genreCmdAddIgnore.TabIndex = 6;
            this.genreCmdAddIgnore.Text = "Add";
            this.genreCmdAddIgnore.UseVisualStyleBackColor = true;
            // 
            // genreLstIgnore
            // 
            this.genreLstIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genreLstIgnore.Location = new System.Drawing.Point(9, 130);
            this.genreLstIgnore.Name = "genreLstIgnore";
            this.genreLstIgnore.Size = new System.Drawing.Size(409, 74);
            this.genreLstIgnore.TabIndex = 8;
            this.genreLstIgnore.UseCompatibleStateImageBehavior = false;
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
            this.genreChkRemoveExisting.Location = new System.Drawing.Point(46, 86);
            this.genreChkRemoveExisting.Name = "genreChkRemoveExisting";
            this.genreChkRemoveExisting.Size = new System.Drawing.Size(186, 17);
            this.genreChkRemoveExisting.TabIndex = 2;
            this.genreChkRemoveExisting.Text = "Remove existing genre categories";
            this.genreChkRemoveExisting.UseVisualStyleBackColor = true;
            // 
            // genreLblMacCats
            // 
            this.genreLblMacCats.AutoSize = true;
            this.genreLblMacCats.Location = new System.Drawing.Point(64, 47);
            this.genreLblMacCats.Name = "genreLblMacCats";
            this.genreLblMacCats.Size = new System.Drawing.Size(148, 26);
            this.genreLblMacCats.TabIndex = 1;
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
            this.genreNumMaxCats.TabIndex = 0;
            this.genreNumMaxCats.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.genreNumMaxCats.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(450, 257);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(141, 23);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Save All Changes";
            this.cmdSave.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(354, 257);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(90, 23);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // genreTxtPrefix
            // 
            this.genreTxtPrefix.Location = new System.Drawing.Point(67, 19);
            this.genreTxtPrefix.Name = "genreTxtPrefix";
            this.genreTxtPrefix.Size = new System.Drawing.Size(165, 20);
            this.genreTxtPrefix.TabIndex = 10;
            // 
            // genreLblPrefix
            // 
            this.genreLblPrefix.AutoSize = true;
            this.genreLblPrefix.Location = new System.Drawing.Point(25, 22);
            this.genreLblPrefix.Name = "genreLblPrefix";
            this.genreLblPrefix.Size = new System.Drawing.Size(36, 13);
            this.genreLblPrefix.TabIndex = 11;
            this.genreLblPrefix.Text = "Prefix:";
            // 
            // panEditFlags
            // 
            this.panEditFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panEditFlags.Controls.Add(this.flagsGrp);
            this.panEditFlags.Location = new System.Drawing.Point(0, 0);
            this.panEditFlags.Name = "panEditFlags";
            this.panEditFlags.Size = new System.Drawing.Size(424, 243);
            this.panEditFlags.TabIndex = 8;
            // 
            // flagsGrp
            // 
            this.flagsGrp.Controls.Add(this.tableLayoutPanel1);
            this.flagsGrp.Controls.Add(this.flagsLblInclude);
            this.flagsGrp.Controls.Add(this.flagsLstIncluded);
            this.flagsGrp.Controls.Add(this.flagsTxtPrefix);
            this.flagsGrp.Controls.Add(this.flagsLblPrefix);
            this.flagsGrp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flagsGrp.Location = new System.Drawing.Point(0, 0);
            this.flagsGrp.Name = "flagsGrp";
            this.flagsGrp.Size = new System.Drawing.Size(424, 243);
            this.flagsGrp.TabIndex = 0;
            this.flagsGrp.TabStop = false;
            this.flagsGrp.Text = "Edit Flag Autocat";
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
            // flagsTxtPrefix
            // 
            this.flagsTxtPrefix.Location = new System.Drawing.Point(67, 19);
            this.flagsTxtPrefix.Name = "flagsTxtPrefix";
            this.flagsTxtPrefix.Size = new System.Drawing.Size(165, 20);
            this.flagsTxtPrefix.TabIndex = 1;
            // 
            // flagsLstIncluded
            // 
            this.flagsLstIncluded.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagsLstIncluded.Location = new System.Drawing.Point(6, 85);
            this.flagsLstIncluded.Name = "flagsLstIncluded";
            this.flagsLstIncluded.Size = new System.Drawing.Size(409, 116);
            this.flagsLstIncluded.TabIndex = 2;
            this.flagsLstIncluded.UseCompatibleStateImageBehavior = false;
            // 
            // flagsLblInclude
            // 
            this.flagsLblInclude.AutoSize = true;
            this.flagsLblInclude.Location = new System.Drawing.Point(3, 69);
            this.flagsLblInclude.Name = "flagsLblInclude";
            this.flagsLblInclude.Size = new System.Drawing.Size(79, 13);
            this.flagsLblInclude.TabIndex = 3;
            this.flagsLblInclude.Text = "Included Flags:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 204);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(409, 30);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Check All";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(139, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Uncheck All";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(275, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Add Custom";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // DlgAutoCat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 288);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.panEditGenre);
            this.Controls.Add(this.grpList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgAutoCat";
            this.Text = "Edit AutoCats";
            this.grpList.ResumeLayout(false);
            this.panEditGenre.ResumeLayout(false);
            this.grpEditGenre.ResumeLayout(false);
            this.grpEditGenre.PerformLayout();
            this.genreTblIgnore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.genreNumMaxCats)).EndInit();
            this.panEditFlags.ResumeLayout(false);
            this.flagsGrp.ResumeLayout(false);
            this.flagsGrp.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstAutoCats;
        private System.Windows.Forms.Button cmdEditAutocat;
        private System.Windows.Forms.Button cmdRenameAutocat;
        private System.Windows.Forms.Button cmdCreate;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.Panel panEditGenre;
        private System.Windows.Forms.GroupBox grpEditGenre;
        private System.Windows.Forms.TableLayoutPanel genreTblIgnore;
        private System.Windows.Forms.Button genreCmdRemoveIgnore;
        private System.Windows.Forms.Button genreCmdAddIgnore;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label flagsLblInclude;
        private System.Windows.Forms.ListView flagsLstIncluded;
        private System.Windows.Forms.TextBox flagsTxtPrefix;
        private System.Windows.Forms.Label flagsLblPrefix;
    }
}