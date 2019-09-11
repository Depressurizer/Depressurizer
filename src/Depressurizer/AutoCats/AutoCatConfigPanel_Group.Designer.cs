namespace Depressurizer.AutoCats {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Group));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.panelAutocats = new System.Windows.Forms.Panel();
            this.lbAutocats = new System.Windows.Forms.ListBox();
            this.tblAddRemove = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.panelUpDown = new System.Windows.Forms.Panel();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.panelAutocats.SuspendLayout();
            this.tblAddRemove.SuspendLayout();
            this.panelUpDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.panelAutocats);
            this.grpMain.Controls.Add(this.panelUpDown);
            this.grpMain.Controls.Add(this.textBox1);
            this.grpMain.Controls.Add(this.splitMain);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // panelAutocats
            // 
            this.panelAutocats.Controls.Add(this.lbAutocats);
            this.panelAutocats.Controls.Add(this.tblAddRemove);
            resources.ApplyResources(this.panelAutocats, "panelAutocats");
            this.panelAutocats.Name = "panelAutocats";
            // 
            // lbAutocats
            // 
            resources.ApplyResources(this.lbAutocats, "lbAutocats");
            this.lbAutocats.FormattingEnabled = true;
            this.lbAutocats.Name = "lbAutocats";
            this.lbAutocats.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAutocats.SelectedIndexChanged += new System.EventHandler(this.lbAutocats_SelectedIndexChanged);
            // 
            // tblAddRemove
            // 
            resources.ApplyResources(this.tblAddRemove, "tblAddRemove");
            this.tblAddRemove.Controls.Add(this.btnAdd, 0, 0);
            this.tblAddRemove.Controls.Add(this.btnRemove, 0, 0);
            this.tblAddRemove.Name = "tblAddRemove";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // panelUpDown
            // 
            resources.ApplyResources(this.panelUpDown, "panelUpDown");
            this.panelUpDown.Controls.Add(this.btnUp);
            this.panelUpDown.Controls.Add(this.btnDown);
            this.panelUpDown.Name = "panelUpDown";
            // 
            // btnUp
            // 
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.Name = "btnUp";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            resources.ApplyResources(this.btnDown, "btnDown");
            this.btnDown.Name = "btnDown";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // splitMain
            // 
            resources.ApplyResources(this.splitMain, "splitMain");
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMain.Name = "splitMain";
            // 
            // AutoCatConfigPanel_Group
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Group";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.panelAutocats.ResumeLayout(false);
            this.tblAddRemove.ResumeLayout(false);
            this.panelUpDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
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
