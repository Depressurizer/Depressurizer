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
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(576, 460);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit Curator Autocat";
            // 
            // helpCuratorUrl
            // 
            this.helpCuratorUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpCuratorUrl.AutoSize = true;
            this.helpCuratorUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpCuratorUrl.Location = new System.Drawing.Point(555, 44);
            this.helpCuratorUrl.Name = "helpCuratorUrl";
            this.helpCuratorUrl.Size = new System.Drawing.Size(15, 15);
            this.helpCuratorUrl.TabIndex = 11;
            this.helpCuratorUrl.Text = "?";
            // 
            // helpCategoryName
            // 
            this.helpCategoryName.AutoSize = true;
            this.helpCategoryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpCategoryName.Location = new System.Drawing.Point(285, 18);
            this.helpCategoryName.Name = "helpCategoryName";
            this.helpCategoryName.Size = new System.Drawing.Size(15, 15);
            this.helpCategoryName.TabIndex = 10;
            this.helpCategoryName.Text = "?";
            // 
            // txtCuratorUrl
            // 
            this.txtCuratorUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCuratorUrl.Location = new System.Drawing.Point(114, 41);
            this.txtCuratorUrl.Name = "txtCuratorUrl";
            this.txtCuratorUrl.Size = new System.Drawing.Size(435, 20);
            this.txtCuratorUrl.TabIndex = 8;
            // 
            // lblCuratorUrl
            // 
            this.lblCuratorUrl.AutoSize = true;
            this.lblCuratorUrl.Location = new System.Drawing.Point(39, 46);
            this.lblCuratorUrl.Name = "lblCuratorUrl";
            this.lblCuratorUrl.Size = new System.Drawing.Size(69, 13);
            this.lblCuratorUrl.TabIndex = 7;
            this.lblCuratorUrl.Text = "Curator URL:";
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
            this.lblInclude.Location = new System.Drawing.Point(6, 78);
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
            this.lstIncluded.Location = new System.Drawing.Point(6, 94);
            this.lstIncluded.Name = "lstIncluded";
            this.lstIncluded.Size = new System.Drawing.Size(564, 330);
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
        private System.Windows.Forms.TextBox txtCuratorUrl;
        private System.Windows.Forms.Label lblCuratorUrl;
        private System.Windows.Forms.Label helpCategoryName;
        private System.Windows.Forms.Label helpCuratorUrl;
    }
}
