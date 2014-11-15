namespace Depressurizer {
    partial class AutoCatConfigPanel_Genre {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.genre_grpMain = new System.Windows.Forms.GroupBox();
            this.genre_helpRemoveExisting = new System.Windows.Forms.Label();
            this.genre_helpPrefix = new System.Windows.Forms.Label();
            this.genre_lblPrefix = new System.Windows.Forms.Label();
            this.genre_txtPrefix = new System.Windows.Forms.TextBox();
            this.genre_tblIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.genre_cmdUncheckAll = new System.Windows.Forms.Button();
            this.genre_cmdCheckAll = new System.Windows.Forms.Button();
            this.genre_lstIgnore = new System.Windows.Forms.ListView();
            this.genre_lblIgnore = new System.Windows.Forms.Label();
            this.genre_chkRemoveExisting = new System.Windows.Forms.CheckBox();
            this.genre_lblMaxCats = new System.Windows.Forms.Label();
            this.genre_numMaxCats = new System.Windows.Forms.NumericUpDown();
            this.genre_grpMain.SuspendLayout();
            this.genre_tblIgnore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.genre_numMaxCats)).BeginInit();
            this.SuspendLayout();
            // 
            // genre_grpMain
            // 
            this.genre_grpMain.Controls.Add(this.genre_helpRemoveExisting);
            this.genre_grpMain.Controls.Add(this.genre_helpPrefix);
            this.genre_grpMain.Controls.Add(this.genre_lblPrefix);
            this.genre_grpMain.Controls.Add(this.genre_txtPrefix);
            this.genre_grpMain.Controls.Add(this.genre_tblIgnore);
            this.genre_grpMain.Controls.Add(this.genre_lstIgnore);
            this.genre_grpMain.Controls.Add(this.genre_lblIgnore);
            this.genre_grpMain.Controls.Add(this.genre_chkRemoveExisting);
            this.genre_grpMain.Controls.Add(this.genre_lblMaxCats);
            this.genre_grpMain.Controls.Add(this.genre_numMaxCats);
            this.genre_grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.genre_grpMain.Location = new System.Drawing.Point(0, 0);
            this.genre_grpMain.Name = "genre_grpMain";
            this.genre_grpMain.Size = new System.Drawing.Size(588, 442);
            this.genre_grpMain.TabIndex = 1;
            this.genre_grpMain.TabStop = false;
            this.genre_grpMain.Text = "Edit Genre AutoCat";
            // 
            // genre_helpRemoveExisting
            // 
            this.genre_helpRemoveExisting.AutoSize = true;
            this.genre_helpRemoveExisting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.genre_helpRemoveExisting.Location = new System.Drawing.Point(238, 82);
            this.genre_helpRemoveExisting.Name = "genre_helpRemoveExisting";
            this.genre_helpRemoveExisting.Size = new System.Drawing.Size(15, 15);
            this.genre_helpRemoveExisting.TabIndex = 1;
            this.genre_helpRemoveExisting.Text = "?";
            // 
            // genre_helpPrefix
            // 
            this.genre_helpPrefix.AutoSize = true;
            this.genre_helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.genre_helpPrefix.Location = new System.Drawing.Point(238, 22);
            this.genre_helpPrefix.Name = "genre_helpPrefix";
            this.genre_helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.genre_helpPrefix.TabIndex = 11;
            this.genre_helpPrefix.Text = "?";
            // 
            // genre_lblPrefix
            // 
            this.genre_lblPrefix.AutoSize = true;
            this.genre_lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.genre_lblPrefix.Name = "genre_lblPrefix";
            this.genre_lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.genre_lblPrefix.TabIndex = 0;
            this.genre_lblPrefix.Text = "Prefix:";
            // 
            // genre_txtPrefix
            // 
            this.genre_txtPrefix.Location = new System.Drawing.Point(67, 19);
            this.genre_txtPrefix.Name = "genre_txtPrefix";
            this.genre_txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.genre_txtPrefix.TabIndex = 1;
            // 
            // genre_tblIgnore
            // 
            this.genre_tblIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genre_tblIgnore.ColumnCount = 2;
            this.genre_tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genre_tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genre_tblIgnore.Controls.Add(this.genre_cmdUncheckAll, 1, 0);
            this.genre_tblIgnore.Controls.Add(this.genre_cmdCheckAll, 0, 0);
            this.genre_tblIgnore.Location = new System.Drawing.Point(6, 408);
            this.genre_tblIgnore.Name = "genre_tblIgnore";
            this.genre_tblIgnore.RowCount = 1;
            this.genre_tblIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.genre_tblIgnore.Size = new System.Drawing.Size(580, 30);
            this.genre_tblIgnore.TabIndex = 7;
            // 
            // genre_cmdUncheckAll
            // 
            this.genre_cmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genre_cmdUncheckAll.Location = new System.Drawing.Point(293, 3);
            this.genre_cmdUncheckAll.Name = "genre_cmdUncheckAll";
            this.genre_cmdUncheckAll.Size = new System.Drawing.Size(284, 23);
            this.genre_cmdUncheckAll.TabIndex = 7;
            this.genre_cmdUncheckAll.Text = "Uncheck All";
            this.genre_cmdUncheckAll.UseVisualStyleBackColor = true;
            // 
            // genre_cmdCheckAll
            // 
            this.genre_cmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genre_cmdCheckAll.Location = new System.Drawing.Point(3, 3);
            this.genre_cmdCheckAll.Name = "genre_cmdCheckAll";
            this.genre_cmdCheckAll.Size = new System.Drawing.Size(284, 23);
            this.genre_cmdCheckAll.TabIndex = 6;
            this.genre_cmdCheckAll.Text = "Check All";
            this.genre_cmdCheckAll.UseVisualStyleBackColor = true;
            // 
            // genre_lstIgnore
            // 
            this.genre_lstIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genre_lstIgnore.CheckBoxes = true;
            this.genre_lstIgnore.Location = new System.Drawing.Point(9, 130);
            this.genre_lstIgnore.Name = "genre_lstIgnore";
            this.genre_lstIgnore.Size = new System.Drawing.Size(575, 279);
            this.genre_lstIgnore.TabIndex = 3;
            this.genre_lstIgnore.UseCompatibleStateImageBehavior = false;
            this.genre_lstIgnore.View = System.Windows.Forms.View.List;
            // 
            // genre_lblIgnore
            // 
            this.genre_lblIgnore.AutoSize = true;
            this.genre_lblIgnore.Location = new System.Drawing.Point(6, 114);
            this.genre_lblIgnore.Name = "genre_lblIgnore";
            this.genre_lblIgnore.Size = new System.Drawing.Size(104, 13);
            this.genre_lblIgnore.TabIndex = 2;
            this.genre_lblIgnore.Text = "Categories to ignore:";
            // 
            // genre_chkRemoveExisting
            // 
            this.genre_chkRemoveExisting.AutoSize = true;
            this.genre_chkRemoveExisting.Location = new System.Drawing.Point(46, 82);
            this.genre_chkRemoveExisting.Name = "genre_chkRemoveExisting";
            this.genre_chkRemoveExisting.Size = new System.Drawing.Size(186, 17);
            this.genre_chkRemoveExisting.TabIndex = 0;
            this.genre_chkRemoveExisting.Text = "Remove existing genre categories";
            this.genre_chkRemoveExisting.UseVisualStyleBackColor = true;
            // 
            // genre_lblMaxCats
            // 
            this.genre_lblMaxCats.AutoSize = true;
            this.genre_lblMaxCats.Location = new System.Drawing.Point(64, 47);
            this.genre_lblMaxCats.Name = "genre_lblMaxCats";
            this.genre_lblMaxCats.Size = new System.Drawing.Size(148, 26);
            this.genre_lblMaxCats.TabIndex = 3;
            this.genre_lblMaxCats.Text = "Maximum categories to assign\r\n(0 for unlimited)";
            // 
            // genre_numMaxCats
            // 
            this.genre_numMaxCats.Location = new System.Drawing.Point(6, 50);
            this.genre_numMaxCats.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.genre_numMaxCats.Name = "genre_numMaxCats";
            this.genre_numMaxCats.Size = new System.Drawing.Size(52, 20);
            this.genre_numMaxCats.TabIndex = 2;
            this.genre_numMaxCats.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.genre_numMaxCats.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // AutoCatConfigPanel_Genre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.genre_grpMain);
            this.Name = "AutoCatConfigPanel_Genre";
            this.Size = new System.Drawing.Size(588, 442);
            this.genre_grpMain.ResumeLayout(false);
            this.genre_grpMain.PerformLayout();
            this.genre_tblIgnore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.genre_numMaxCats)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox genre_grpMain;
        private System.Windows.Forms.Label genre_helpRemoveExisting;
        private System.Windows.Forms.Label genre_helpPrefix;
        private System.Windows.Forms.Label genre_lblPrefix;
        private System.Windows.Forms.TextBox genre_txtPrefix;
        private System.Windows.Forms.TableLayoutPanel genre_tblIgnore;
        private System.Windows.Forms.Button genre_cmdUncheckAll;
        private System.Windows.Forms.Button genre_cmdCheckAll;
        private System.Windows.Forms.ListView genre_lstIgnore;
        private System.Windows.Forms.Label genre_lblIgnore;
        private System.Windows.Forms.CheckBox genre_chkRemoveExisting;
        private System.Windows.Forms.Label genre_lblMaxCats;
        private System.Windows.Forms.NumericUpDown genre_numMaxCats;
    }
}
