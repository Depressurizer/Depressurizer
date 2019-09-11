namespace Depressurizer.AutoCats {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Genre));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.helpTagFallback = new System.Windows.Forms.Label();
            this.chkTagFallback = new System.Windows.Forms.CheckBox();
            this.helpRemoveExisting = new System.Windows.Forms.Label();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.tblIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.cmdUncheckAll = new System.Windows.Forms.Button();
            this.cmdCheckAll = new System.Windows.Forms.Button();
            this.lstIgnore = new System.Windows.Forms.ListView();
            this.lblIgnore = new System.Windows.Forms.Label();
            this.chkRemoveExisting = new System.Windows.Forms.CheckBox();
            this.lblMaxCats = new System.Windows.Forms.Label();
            this.numMaxCats = new System.Windows.Forms.NumericUpDown();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.tblIgnore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCats)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.helpTagFallback);
            this.grpMain.Controls.Add(this.chkTagFallback);
            this.grpMain.Controls.Add(this.helpRemoveExisting);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.txtPrefix);
            this.grpMain.Controls.Add(this.tblIgnore);
            this.grpMain.Controls.Add(this.lstIgnore);
            this.grpMain.Controls.Add(this.lblIgnore);
            this.grpMain.Controls.Add(this.chkRemoveExisting);
            this.grpMain.Controls.Add(this.lblMaxCats);
            this.grpMain.Controls.Add(this.numMaxCats);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // helpTagFallback
            // 
            resources.ApplyResources(this.helpTagFallback, "helpTagFallback");
            this.helpTagFallback.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpTagFallback.Name = "helpTagFallback";
            // 
            // chkTagFallback
            // 
            resources.ApplyResources(this.chkTagFallback, "chkTagFallback");
            this.chkTagFallback.Name = "chkTagFallback";
            this.chkTagFallback.UseVisualStyleBackColor = true;
            // 
            // helpRemoveExisting
            // 
            resources.ApplyResources(this.helpRemoveExisting, "helpRemoveExisting");
            this.helpRemoveExisting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpRemoveExisting.Name = "helpRemoveExisting";
            // 
            // helpPrefix
            // 
            resources.ApplyResources(this.helpPrefix, "helpPrefix");
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Name = "helpPrefix";
            // 
            // lblPrefix
            // 
            resources.ApplyResources(this.lblPrefix, "lblPrefix");
            this.lblPrefix.Name = "lblPrefix";
            // 
            // txtPrefix
            // 
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            // 
            // tblIgnore
            // 
            resources.ApplyResources(this.tblIgnore, "tblIgnore");
            this.tblIgnore.Controls.Add(this.cmdUncheckAll, 1, 0);
            this.tblIgnore.Controls.Add(this.cmdCheckAll, 0, 0);
            this.tblIgnore.Name = "tblIgnore";
            // 
            // cmdUncheckAll
            // 
            resources.ApplyResources(this.cmdUncheckAll, "cmdUncheckAll");
            this.cmdUncheckAll.Name = "cmdUncheckAll";
            this.cmdUncheckAll.UseVisualStyleBackColor = true;
            this.cmdUncheckAll.Click += new System.EventHandler(this.cmdUncheckAll_Click);
            // 
            // cmdCheckAll
            // 
            resources.ApplyResources(this.cmdCheckAll, "cmdCheckAll");
            this.cmdCheckAll.Name = "cmdCheckAll";
            this.cmdCheckAll.UseVisualStyleBackColor = true;
            this.cmdCheckAll.Click += new System.EventHandler(this.cmdCheckAll_Click);
            // 
            // lstIgnore
            // 
            resources.ApplyResources(this.lstIgnore, "lstIgnore");
            this.lstIgnore.CheckBoxes = true;
            this.lstIgnore.HideSelection = false;
            this.lstIgnore.Name = "lstIgnore";
            this.lstIgnore.UseCompatibleStateImageBehavior = false;
            this.lstIgnore.View = System.Windows.Forms.View.List;
            // 
            // lblIgnore
            // 
            resources.ApplyResources(this.lblIgnore, "lblIgnore");
            this.lblIgnore.Name = "lblIgnore";
            // 
            // chkRemoveExisting
            // 
            resources.ApplyResources(this.chkRemoveExisting, "chkRemoveExisting");
            this.chkRemoveExisting.Name = "chkRemoveExisting";
            this.chkRemoveExisting.UseVisualStyleBackColor = true;
            // 
            // lblMaxCats
            // 
            resources.ApplyResources(this.lblMaxCats, "lblMaxCats");
            this.lblMaxCats.Name = "lblMaxCats";
            // 
            // numMaxCats
            // 
            resources.ApplyResources(this.numMaxCats, "numMaxCats");
            this.numMaxCats.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMaxCats.Name = "numMaxCats";
            // 
            // AutoCatConfigPanel_Genre
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Genre";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.tblIgnore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxCats)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpRemoveExisting;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.TableLayoutPanel tblIgnore;
        private System.Windows.Forms.Button cmdUncheckAll;
        private System.Windows.Forms.Button cmdCheckAll;
        private System.Windows.Forms.ListView lstIgnore;
        private System.Windows.Forms.Label lblIgnore;
        private System.Windows.Forms.CheckBox chkRemoveExisting;
        private System.Windows.Forms.Label lblMaxCats;
        private System.Windows.Forms.NumericUpDown numMaxCats;
        private System.Windows.Forms.Label helpTagFallback;
        private System.Windows.Forms.CheckBox chkTagFallback;
        private Lib.ExtToolTip ttHelp;
    }
}
