namespace Depressurizer.AutoCats {
    partial class AutoCatConfigPanel_VrSupport {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_VrSupport));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdUncheckAll = new System.Windows.Forms.Button();
            this.cmdCheckAll = new System.Windows.Forms.Button();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.tblVrFlags = new System.Windows.Forms.TableLayoutPanel();
            this.grpVrPlayArea = new System.Windows.Forms.GroupBox();
            this.lstVrPlayArea = new System.Windows.Forms.ListView();
            this.grpVrHeadsets = new System.Windows.Forms.GroupBox();
            this.lstVrHeadsets = new System.Windows.Forms.ListView();
            this.grpVrInput = new System.Windows.Forms.GroupBox();
            this.lstVrInput = new System.Windows.Forms.ListView();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.tblVrFlags.SuspendLayout();
            this.grpVrPlayArea.SuspendLayout();
            this.grpVrHeadsets.SuspendLayout();
            this.grpVrInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.tblButtons);
            this.grpMain.Controls.Add(this.txtPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.tblVrFlags);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // helpPrefix
            // 
            resources.ApplyResources(this.helpPrefix, "helpPrefix");
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Name = "helpPrefix";
            // 
            // tblButtons
            // 
            resources.ApplyResources(this.tblButtons, "tblButtons");
            this.tblButtons.Controls.Add(this.cmdUncheckAll, 1, 0);
            this.tblButtons.Controls.Add(this.cmdCheckAll, 0, 0);
            this.tblButtons.Name = "tblButtons";
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
            // txtPrefix
            // 
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            // 
            // lblPrefix
            // 
            resources.ApplyResources(this.lblPrefix, "lblPrefix");
            this.lblPrefix.Name = "lblPrefix";
            // 
            // tblVrFlags
            // 
            resources.ApplyResources(this.tblVrFlags, "tblVrFlags");
            this.tblVrFlags.Controls.Add(this.grpVrPlayArea, 2, 0);
            this.tblVrFlags.Controls.Add(this.grpVrHeadsets, 0, 0);
            this.tblVrFlags.Controls.Add(this.grpVrInput, 1, 0);
            this.tblVrFlags.Name = "tblVrFlags";
            // 
            // grpVrPlayArea
            // 
            this.grpVrPlayArea.Controls.Add(this.lstVrPlayArea);
            resources.ApplyResources(this.grpVrPlayArea, "grpVrPlayArea");
            this.grpVrPlayArea.Name = "grpVrPlayArea";
            this.grpVrPlayArea.TabStop = false;
            // 
            // lstVrPlayArea
            // 
            this.lstVrPlayArea.CheckBoxes = true;
            resources.ApplyResources(this.lstVrPlayArea, "lstVrPlayArea");
            this.lstVrPlayArea.HideSelection = false;
            this.lstVrPlayArea.Name = "lstVrPlayArea";
            this.lstVrPlayArea.UseCompatibleStateImageBehavior = false;
            this.lstVrPlayArea.View = System.Windows.Forms.View.List;
            // 
            // grpVrHeadsets
            // 
            this.grpVrHeadsets.Controls.Add(this.lstVrHeadsets);
            resources.ApplyResources(this.grpVrHeadsets, "grpVrHeadsets");
            this.grpVrHeadsets.Name = "grpVrHeadsets";
            this.grpVrHeadsets.TabStop = false;
            // 
            // lstVrHeadsets
            // 
            this.lstVrHeadsets.CheckBoxes = true;
            resources.ApplyResources(this.lstVrHeadsets, "lstVrHeadsets");
            this.lstVrHeadsets.HideSelection = false;
            this.lstVrHeadsets.Name = "lstVrHeadsets";
            this.lstVrHeadsets.UseCompatibleStateImageBehavior = false;
            this.lstVrHeadsets.View = System.Windows.Forms.View.List;
            // 
            // grpVrInput
            // 
            this.grpVrInput.Controls.Add(this.lstVrInput);
            resources.ApplyResources(this.grpVrInput, "grpVrInput");
            this.grpVrInput.Name = "grpVrInput";
            this.grpVrInput.TabStop = false;
            // 
            // lstVrInput
            // 
            this.lstVrInput.CheckBoxes = true;
            resources.ApplyResources(this.lstVrInput, "lstVrInput");
            this.lstVrInput.HideSelection = false;
            this.lstVrInput.Name = "lstVrInput";
            this.lstVrInput.UseCompatibleStateImageBehavior = false;
            this.lstVrInput.View = System.Windows.Forms.View.List;
            // 
            // AutoCatConfigPanel_VrSupport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_VrSupport";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.tblButtons.ResumeLayout(false);
            this.tblVrFlags.ResumeLayout(false);
            this.grpVrPlayArea.ResumeLayout(false);
            this.grpVrHeadsets.ResumeLayout(false);
            this.grpVrInput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button cmdCheckAll;
        private System.Windows.Forms.Button cmdUncheckAll;
        private System.Windows.Forms.ListView lstVrHeadsets;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.ListView lstVrPlayArea;
        private System.Windows.Forms.ListView lstVrInput;
        private System.Windows.Forms.GroupBox grpVrHeadsets;
        private System.Windows.Forms.GroupBox grpVrInput;
        private System.Windows.Forms.GroupBox grpVrPlayArea;
        private System.Windows.Forms.TableLayoutPanel tblVrFlags;
    }
}
