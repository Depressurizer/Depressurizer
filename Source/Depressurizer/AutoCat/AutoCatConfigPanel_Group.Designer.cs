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
            this.lbAutocats = new System.Windows.Forms.ListBox();
            this.tblAddRemove = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.panelUpDown = new System.Windows.Forms.Panel();
            this.panelAutocats = new System.Windows.Forms.Panel();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.SuspendLayout();
            this.tblAddRemove.SuspendLayout();
            this.panelUpDown.SuspendLayout();
            this.panelAutocats.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.panelAutocats);
            this.grpMain.Controls.Add(this.panelUpDown);
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
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(3, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(335, 50);
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
            this.splitMain.Size = new System.Drawing.Size(332, 254);
            this.splitMain.SplitterDistance = 269;
            this.splitMain.TabIndex = 18;
            // 
            // lbAutocats
            // 
            this.lbAutocats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAutocats.FormattingEnabled = true;
            this.lbAutocats.Location = new System.Drawing.Point(3, 3);
            this.lbAutocats.Name = "lbAutocats";
            this.lbAutocats.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAutocats.Size = new System.Drawing.Size(268, 226);
            this.lbAutocats.TabIndex = 0;
            this.lbAutocats.SelectedIndexChanged += new System.EventHandler(this.lbAutocats_SelectedIndexChanged);
            // 
            // tblAddRemove
            // 
            this.tblAddRemove.ColumnCount = 2;
            this.tblAddRemove.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAddRemove.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAddRemove.Controls.Add(this.btnAdd, 0, 0);
            this.tblAddRemove.Controls.Add(this.btnRemove, 0, 0);
            this.tblAddRemove.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblAddRemove.Location = new System.Drawing.Point(3, 229);
            this.tblAddRemove.Name = "tblAddRemove";
            this.tblAddRemove.RowCount = 1;
            this.tblAddRemove.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblAddRemove.Size = new System.Drawing.Size(268, 30);
            this.tblAddRemove.TabIndex = 12;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(137, 3);
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
            this.btnRemove.Location = new System.Drawing.Point(3, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(128, 24);
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
            // panelUpDown
            // 
            this.panelUpDown.AutoSize = true;
            this.panelUpDown.Controls.Add(this.btnUp);
            this.panelUpDown.Controls.Add(this.btnDown);
            this.panelUpDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelUpDown.Location = new System.Drawing.Point(277, 66);
            this.panelUpDown.Name = "panelUpDown";
            this.panelUpDown.Size = new System.Drawing.Size(61, 262);
            this.panelUpDown.TabIndex = 15;
            // 
            // panelAutocats
            // 
            this.panelAutocats.Controls.Add(this.lbAutocats);
            this.panelAutocats.Controls.Add(this.tblAddRemove);
            this.panelAutocats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAutocats.Location = new System.Drawing.Point(3, 66);
            this.panelAutocats.Name = "panelAutocats";
            this.panelAutocats.Padding = new System.Windows.Forms.Padding(3);
            this.panelAutocats.Size = new System.Drawing.Size(274, 262);
            this.panelAutocats.TabIndex = 15;
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
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tblAddRemove.ResumeLayout(false);
            this.panelUpDown.ResumeLayout(false);
            this.panelAutocats.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TableLayoutPanel tblAddRemove;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.ListBox lbAutocats;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panelAutocats;
        private System.Windows.Forms.Panel panelUpDown;
    }
}
