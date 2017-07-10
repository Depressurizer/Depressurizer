namespace Depressurizer {
    partial class AutoCatConfigPanel_Group {
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitAutocats = new System.Windows.Forms.SplitContainer();
            this.lbAutocats = new System.Windows.Forms.ListBox();
            this.tblIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAutocats)).BeginInit();
            this.splitAutocats.Panel1.SuspendLayout();
            this.splitAutocats.Panel2.SuspendLayout();
            this.splitAutocats.SuspendLayout();
            this.tblIgnore.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.textBox1);
            this.grpMain.Controls.Add(this.splitMain);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(341, 331);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit Group AutoCat";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(6, 18);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(331, 50);
            this.textBox1.TabIndex = 20;
            this.textBox1.Text = "Group AutoCats filters take precedence over AutoCat filters.  Groups can be added" +
    " to Groups as long as they won\'t cause an infinite loop.  AutoCats run in the or" +
    "der listed below.";
            // 
            // splitMain
            // 
            this.splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(6, 74);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.splitAutocats);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.btnUp);
            this.splitMain.Panel2.Controls.Add(this.btnDown);
            this.splitMain.Size = new System.Drawing.Size(332, 254);
            this.splitMain.SplitterDistance = 269;
            this.splitMain.TabIndex = 18;
            // 
            // splitAutocats
            // 
            this.splitAutocats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitAutocats.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitAutocats.Location = new System.Drawing.Point(0, 0);
            this.splitAutocats.Name = "splitAutocats";
            this.splitAutocats.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitAutocats.Panel1
            // 
            this.splitAutocats.Panel1.Controls.Add(this.lbAutocats);
            // 
            // splitAutocats.Panel2
            // 
            this.splitAutocats.Panel2.Controls.Add(this.tblIgnore);
            this.splitAutocats.Size = new System.Drawing.Size(269, 254);
            this.splitAutocats.SplitterDistance = 220;
            this.splitAutocats.TabIndex = 17;
            // 
            // lbAutocats
            // 
            this.lbAutocats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAutocats.FormattingEnabled = true;
            this.lbAutocats.Location = new System.Drawing.Point(0, 0);
            this.lbAutocats.Name = "lbAutocats";
            this.lbAutocats.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAutocats.Size = new System.Drawing.Size(269, 220);
            this.lbAutocats.TabIndex = 0;
            this.lbAutocats.SelectedIndexChanged += new System.EventHandler(this.lbAutocats_SelectedIndexChanged);
            // 
            // tblIgnore
            // 
            this.tblIgnore.ColumnCount = 2;
            this.tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.Controls.Add(this.btnAdd, 0, 0);
            this.tblIgnore.Controls.Add(this.btnRemove, 1, 0);
            this.tblIgnore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblIgnore.Location = new System.Drawing.Point(0, 0);
            this.tblIgnore.Name = "tblIgnore";
            this.tblIgnore.RowCount = 1;
            this.tblIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblIgnore.Size = new System.Drawing.Size(269, 30);
            this.tblIgnore.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(128, 24);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add Autocat";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(137, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(129, 24);
            this.btnRemove.TabIndex = 16;
            this.btnRemove.Text = "Remove Autocat";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnUp
            // 
            this.btnUp.Enabled = false;
            this.btnUp.Location = new System.Drawing.Point(2, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(56, 23);
            this.btnUp.TabIndex = 13;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Enabled = false;
            this.btnDown.Location = new System.Drawing.Point(2, 29);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(56, 23);
            this.btnDown.TabIndex = 14;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // AutoCatConfigPanel_Group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "AutoCatConfigPanel_Group";
            this.Size = new System.Drawing.Size(341, 331);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitAutocats.Panel1.ResumeLayout(false);
            this.splitAutocats.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitAutocats)).EndInit();
            this.splitAutocats.ResumeLayout(false);
            this.tblIgnore.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.SplitContainer splitAutocats;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TableLayoutPanel tblIgnore;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.ListBox lbAutocats;
        private System.Windows.Forms.TextBox textBox1;
    }
}
