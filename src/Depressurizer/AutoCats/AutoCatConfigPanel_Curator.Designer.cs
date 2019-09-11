namespace Depressurizer.AutoCats {
    partial class AutoCatConfigPanel_Curator {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Curator));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.helpCuratorUrl = new System.Windows.Forms.Label();
            this.helpCategoryName = new System.Windows.Forms.Label();
            this.txtCuratorUrl = new System.Windows.Forms.TextBox();
            this.lblCuratorUrl = new System.Windows.Forms.Label();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdCheckAll = new System.Windows.Forms.Button();
            this.cmdUncheckAll = new System.Windows.Forms.Button();
            this.lblInclude = new System.Windows.Forms.Label();
            this.lstIncluded = new System.Windows.Forms.ListView();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.helpCuratorUrl);
            this.grpMain.Controls.Add(this.helpCategoryName);
            this.grpMain.Controls.Add(this.txtCuratorUrl);
            this.grpMain.Controls.Add(this.lblCuratorUrl);
            this.grpMain.Controls.Add(this.tblButtons);
            this.grpMain.Controls.Add(this.lblInclude);
            this.grpMain.Controls.Add(this.lstIncluded);
            this.grpMain.Controls.Add(this.txtCategoryName);
            this.grpMain.Controls.Add(this.lblCategoryName);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // helpCuratorUrl
            // 
            resources.ApplyResources(this.helpCuratorUrl, "helpCuratorUrl");
            this.helpCuratorUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpCuratorUrl.Name = "helpCuratorUrl";
            // 
            // helpCategoryName
            // 
            resources.ApplyResources(this.helpCategoryName, "helpCategoryName");
            this.helpCategoryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpCategoryName.Name = "helpCategoryName";
            // 
            // txtCuratorUrl
            // 
            resources.ApplyResources(this.txtCuratorUrl, "txtCuratorUrl");
            this.txtCuratorUrl.Name = "txtCuratorUrl";
            // 
            // lblCuratorUrl
            // 
            resources.ApplyResources(this.lblCuratorUrl, "lblCuratorUrl");
            this.lblCuratorUrl.Name = "lblCuratorUrl";
            // 
            // tblButtons
            // 
            resources.ApplyResources(this.tblButtons, "tblButtons");
            this.tblButtons.Controls.Add(this.cmdCheckAll, 0, 0);
            this.tblButtons.Controls.Add(this.cmdUncheckAll, 1, 0);
            this.tblButtons.Name = "tblButtons";
            // 
            // cmdCheckAll
            // 
            resources.ApplyResources(this.cmdCheckAll, "cmdCheckAll");
            this.cmdCheckAll.Name = "cmdCheckAll";
            this.cmdCheckAll.UseVisualStyleBackColor = true;
            this.cmdCheckAll.Click += new System.EventHandler(this.cmdCheckAll_Click);
            // 
            // cmdUncheckAll
            // 
            resources.ApplyResources(this.cmdUncheckAll, "cmdUncheckAll");
            this.cmdUncheckAll.Name = "cmdUncheckAll";
            this.cmdUncheckAll.UseVisualStyleBackColor = true;
            this.cmdUncheckAll.Click += new System.EventHandler(this.cmdUncheckAll_Click);
            // 
            // lblInclude
            // 
            resources.ApplyResources(this.lblInclude, "lblInclude");
            this.lblInclude.Name = "lblInclude";
            // 
            // lstIncluded
            // 
            resources.ApplyResources(this.lstIncluded, "lstIncluded");
            this.lstIncluded.CheckBoxes = true;
            this.lstIncluded.HideSelection = false;
            this.lstIncluded.Name = "lstIncluded";
            this.lstIncluded.UseCompatibleStateImageBehavior = false;
            this.lstIncluded.View = System.Windows.Forms.View.List;
            // 
            // txtCategoryName
            // 
            resources.ApplyResources(this.txtCategoryName, "txtCategoryName");
            this.txtCategoryName.Name = "txtCategoryName";
            // 
            // lblCategoryName
            // 
            resources.ApplyResources(this.lblCategoryName, "lblCategoryName");
            this.lblCategoryName.Name = "lblCategoryName";
            // 
            // AutoCatConfigPanel_Curator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Curator";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.tblButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button cmdCheckAll;
        private System.Windows.Forms.Button cmdUncheckAll;
        private System.Windows.Forms.Label lblInclude;
        private System.Windows.Forms.ListView lstIncluded;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblCategoryName;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.TextBox txtCuratorUrl;
        private System.Windows.Forms.Label lblCuratorUrl;
        private System.Windows.Forms.Label helpCategoryName;
        private System.Windows.Forms.Label helpCuratorUrl;
    }
}
