namespace Depressurizer {
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.lblCuratorUrlExample = new System.Windows.Forms.Label();
            this.txtCuratorUrl = new System.Windows.Forms.TextBox();
            this.lblCuratorUrl = new System.Windows.Forms.Label();
            this.lblCategoryNameExample = new System.Windows.Forms.Label();
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
            this.grpMain.Controls.Add(this.lblCuratorUrlExample);
            this.grpMain.Controls.Add(this.txtCuratorUrl);
            this.grpMain.Controls.Add(this.lblCuratorUrl);
            this.grpMain.Controls.Add(this.lblCategoryNameExample);
            this.grpMain.Controls.Add(this.tblButtons);
            this.grpMain.Controls.Add(this.lblInclude);
            this.grpMain.Controls.Add(this.lstIncluded);
            this.grpMain.Controls.Add(this.txtCategoryName);
            this.grpMain.Controls.Add(this.lblCategoryName);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(576, 460);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit Curator Autocat";
            // 
            // lblCuratorUrlExample
            // 
            this.lblCuratorUrlExample.AutoSize = true;
            this.lblCuratorUrlExample.Location = new System.Drawing.Point(25, 81);
            this.lblCuratorUrlExample.Name = "lblCuratorUrlExample";
            this.lblCuratorUrlExample.Size = new System.Drawing.Size(329, 13);
            this.lblCuratorUrlExample.TabIndex = 9;
            this.lblCuratorUrlExample.Text = "e.g http://store.steampowered.com/curator/6090344-depressurizer/";
            // 
            // txtCuratorUrl
            // 
            this.txtCuratorUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCuratorUrl.Location = new System.Drawing.Point(114, 58);
            this.txtCuratorUrl.Name = "txtCuratorUrl";
            this.txtCuratorUrl.Size = new System.Drawing.Size(456, 20);
            this.txtCuratorUrl.TabIndex = 8;
            // 
            // lblCuratorUrl
            // 
            this.lblCuratorUrl.AutoSize = true;
            this.lblCuratorUrl.Location = new System.Drawing.Point(39, 61);
            this.lblCuratorUrl.Name = "lblCuratorUrl";
            this.lblCuratorUrl.Size = new System.Drawing.Size(69, 13);
            this.lblCuratorUrl.TabIndex = 7;
            this.lblCuratorUrl.Text = "Curator URL:";
            // 
            // lblCategoryNameExample
            // 
            this.lblCategoryNameExample.AutoSize = true;
            this.lblCategoryNameExample.Location = new System.Drawing.Point(25, 38);
            this.lblCategoryNameExample.Name = "lblCategoryNameExample";
            this.lblCategoryNameExample.Size = new System.Drawing.Size(370, 13);
            this.lblCategoryNameExample.TabIndex = 6;
            this.lblCategoryNameExample.Text = "Use {type} keyword for type (recommended, not recommended, informational)";
            // 
            // tblButtons
            // 
            this.tblButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblButtons.ColumnCount = 2;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblButtons.Controls.Add(this.cmdCheckAll, 0, 0);
            this.tblButtons.Controls.Add(this.cmdUncheckAll, 1, 0);
            this.tblButtons.Location = new System.Drawing.Point(3, 426);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(570, 30);
            this.tblButtons.TabIndex = 5;
            // 
            // cmdCheckAll
            // 
            this.cmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCheckAll.Location = new System.Drawing.Point(3, 3);
            this.cmdCheckAll.Name = "cmdCheckAll";
            this.cmdCheckAll.Size = new System.Drawing.Size(279, 23);
            this.cmdCheckAll.TabIndex = 0;
            this.cmdCheckAll.Text = "Check All";
            this.cmdCheckAll.UseVisualStyleBackColor = true;
            this.cmdCheckAll.Click += new System.EventHandler(this.cmdCheckAll_Click);
            // 
            // cmdUncheckAll
            // 
            this.cmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUncheckAll.Location = new System.Drawing.Point(288, 3);
            this.cmdUncheckAll.Name = "cmdUncheckAll";
            this.cmdUncheckAll.Size = new System.Drawing.Size(279, 23);
            this.cmdUncheckAll.TabIndex = 1;
            this.cmdUncheckAll.Text = "Uncheck All";
            this.cmdUncheckAll.UseVisualStyleBackColor = true;
            this.cmdUncheckAll.Click += new System.EventHandler(this.cmdUncheckAll_Click);
            // 
            // lblInclude
            // 
            this.lblInclude.AutoSize = true;
            this.lblInclude.Location = new System.Drawing.Point(3, 109);
            this.lblInclude.Name = "lblInclude";
            this.lblInclude.Size = new System.Drawing.Size(142, 13);
            this.lblInclude.TabIndex = 3;
            this.lblInclude.Text = "Included Recommendations:";
            // 
            // lstIncluded
            // 
            this.lstIncluded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstIncluded.CheckBoxes = true;
            this.lstIncluded.Location = new System.Drawing.Point(6, 125);
            this.lstIncluded.Name = "lstIncluded";
            this.lstIncluded.Size = new System.Drawing.Size(564, 299);
            this.lstIncluded.TabIndex = 4;
            this.lstIncluded.UseCompatibleStateImageBehavior = false;
            this.lstIncluded.View = System.Windows.Forms.View.List;
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Location = new System.Drawing.Point(114, 15);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(165, 20);
            this.txtCategoryName.TabIndex = 1;
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new System.Drawing.Point(25, 22);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(83, 13);
            this.lblCategoryName.TabIndex = 0;
            this.lblCategoryName.Text = "Category Name:";
            // 
            // AutoCatConfigPanel_Curator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Curator";
            this.Size = new System.Drawing.Size(576, 460);
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
        private System.Windows.Forms.Label lblCategoryNameExample;
        private System.Windows.Forms.Label lblCuratorUrlExample;
        private System.Windows.Forms.TextBox txtCuratorUrl;
        private System.Windows.Forms.Label lblCuratorUrl;
    }
}
