namespace Depressurizer {
    partial class AutoCatConfigPanel_Flags {
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
            this.flags_grpMain = new System.Windows.Forms.GroupBox();
            this.flags_helpPrefix = new System.Windows.Forms.Label();
            this.flags_tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.flags_cmdCheckAll = new System.Windows.Forms.Button();
            this.flags_cmdUncheckAll = new System.Windows.Forms.Button();
            this.flags_lblInclude = new System.Windows.Forms.Label();
            this.flags_lstIncluded = new System.Windows.Forms.ListView();
            this.flags_txtPrefix = new System.Windows.Forms.TextBox();
            this.flags_lblPrefix = new System.Windows.Forms.Label();
            this.flags_grpMain.SuspendLayout();
            this.flags_tblButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // flags_grpMain
            // 
            this.flags_grpMain.Controls.Add(this.flags_helpPrefix);
            this.flags_grpMain.Controls.Add(this.flags_tblButtons);
            this.flags_grpMain.Controls.Add(this.flags_lblInclude);
            this.flags_grpMain.Controls.Add(this.flags_lstIncluded);
            this.flags_grpMain.Controls.Add(this.flags_txtPrefix);
            this.flags_grpMain.Controls.Add(this.flags_lblPrefix);
            this.flags_grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flags_grpMain.Location = new System.Drawing.Point(0, 0);
            this.flags_grpMain.Name = "flags_grpMain";
            this.flags_grpMain.Size = new System.Drawing.Size(576, 460);
            this.flags_grpMain.TabIndex = 1;
            this.flags_grpMain.TabStop = false;
            this.flags_grpMain.Text = "Edit Flag Autocat";
            // 
            // flags_helpPrefix
            // 
            this.flags_helpPrefix.AutoSize = true;
            this.flags_helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flags_helpPrefix.Location = new System.Drawing.Point(238, 22);
            this.flags_helpPrefix.Name = "flags_helpPrefix";
            this.flags_helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.flags_helpPrefix.TabIndex = 6;
            this.flags_helpPrefix.Text = "?";
            // 
            // flags_tblButtons
            // 
            this.flags_tblButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flags_tblButtons.ColumnCount = 2;
            this.flags_tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.flags_tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.flags_tblButtons.Controls.Add(this.flags_cmdCheckAll, 0, 0);
            this.flags_tblButtons.Controls.Add(this.flags_cmdUncheckAll, 1, 0);
            this.flags_tblButtons.Location = new System.Drawing.Point(3, 426);
            this.flags_tblButtons.Name = "flags_tblButtons";
            this.flags_tblButtons.RowCount = 1;
            this.flags_tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.flags_tblButtons.Size = new System.Drawing.Size(570, 30);
            this.flags_tblButtons.TabIndex = 4;
            // 
            // flags_cmdCheckAll
            // 
            this.flags_cmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flags_cmdCheckAll.Location = new System.Drawing.Point(3, 3);
            this.flags_cmdCheckAll.Name = "flags_cmdCheckAll";
            this.flags_cmdCheckAll.Size = new System.Drawing.Size(279, 23);
            this.flags_cmdCheckAll.TabIndex = 0;
            this.flags_cmdCheckAll.Text = "Check All";
            this.flags_cmdCheckAll.UseVisualStyleBackColor = true;
            // 
            // flags_cmdUncheckAll
            // 
            this.flags_cmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flags_cmdUncheckAll.Location = new System.Drawing.Point(288, 3);
            this.flags_cmdUncheckAll.Name = "flags_cmdUncheckAll";
            this.flags_cmdUncheckAll.Size = new System.Drawing.Size(279, 23);
            this.flags_cmdUncheckAll.TabIndex = 1;
            this.flags_cmdUncheckAll.Text = "Uncheck All";
            this.flags_cmdUncheckAll.UseVisualStyleBackColor = true;
            // 
            // flags_lblInclude
            // 
            this.flags_lblInclude.AutoSize = true;
            this.flags_lblInclude.Location = new System.Drawing.Point(3, 69);
            this.flags_lblInclude.Name = "flags_lblInclude";
            this.flags_lblInclude.Size = new System.Drawing.Size(79, 13);
            this.flags_lblInclude.TabIndex = 0;
            this.flags_lblInclude.Text = "Included Flags:";
            // 
            // flags_lstIncluded
            // 
            this.flags_lstIncluded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flags_lstIncluded.CheckBoxes = true;
            this.flags_lstIncluded.Location = new System.Drawing.Point(6, 85);
            this.flags_lstIncluded.Name = "flags_lstIncluded";
            this.flags_lstIncluded.Size = new System.Drawing.Size(564, 339);
            this.flags_lstIncluded.TabIndex = 3;
            this.flags_lstIncluded.UseCompatibleStateImageBehavior = false;
            this.flags_lstIncluded.View = System.Windows.Forms.View.List;
            // 
            // flags_txtPrefix
            // 
            this.flags_txtPrefix.Location = new System.Drawing.Point(67, 19);
            this.flags_txtPrefix.Name = "flags_txtPrefix";
            this.flags_txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.flags_txtPrefix.TabIndex = 1;
            // 
            // flags_lblPrefix
            // 
            this.flags_lblPrefix.AutoSize = true;
            this.flags_lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.flags_lblPrefix.Name = "flags_lblPrefix";
            this.flags_lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.flags_lblPrefix.TabIndex = 0;
            this.flags_lblPrefix.Text = "Prefix:";
            // 
            // AutoCatConfigPanel_Flags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flags_grpMain);
            this.Name = "AutoCatConfigPanel_Flags";
            this.Size = new System.Drawing.Size(576, 460);
            this.flags_grpMain.ResumeLayout(false);
            this.flags_grpMain.PerformLayout();
            this.flags_tblButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox flags_grpMain;
        private System.Windows.Forms.Label flags_helpPrefix;
        private System.Windows.Forms.TableLayoutPanel flags_tblButtons;
        private System.Windows.Forms.Button flags_cmdCheckAll;
        private System.Windows.Forms.Button flags_cmdUncheckAll;
        private System.Windows.Forms.Label flags_lblInclude;
        private System.Windows.Forms.ListView flags_lstIncluded;
        private System.Windows.Forms.TextBox flags_txtPrefix;
        private System.Windows.Forms.Label flags_lblPrefix;
    }
}
