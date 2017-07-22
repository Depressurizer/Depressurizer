namespace Depressurizer {
    partial class DlgAutoCat {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lstAutoCats = new System.Windows.Forms.ListBox();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdRename = new System.Windows.Forms.Button();
            this.cmdCreate = new System.Windows.Forms.Button();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitAutoCat = new System.Windows.Forms.SplitContainer();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAutoCat)).BeginInit();
            this.splitAutoCat.Panel1.SuspendLayout();
            this.splitAutoCat.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstAutoCats
            // 
            this.lstAutoCats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAutoCats.FormattingEnabled = true;
            this.lstAutoCats.HorizontalScrollbar = true;
            this.lstAutoCats.IntegralHeight = false;
            this.lstAutoCats.Location = new System.Drawing.Point(6, 19);
            this.lstAutoCats.Name = "lstAutoCats";
            this.lstAutoCats.Size = new System.Drawing.Size(153, 235);
            this.lstAutoCats.TabIndex = 0;
            this.lstAutoCats.SelectedIndexChanged += new System.EventHandler(this.lstAutoCats_SelectedIndexChanged);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Location = new System.Drawing.Point(6, 311);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(171, 23);
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdRename
            // 
            this.cmdRename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRename.Location = new System.Drawing.Point(6, 285);
            this.cmdRename.Name = "cmdRename";
            this.cmdRename.Size = new System.Drawing.Size(171, 23);
            this.cmdRename.TabIndex = 2;
            this.cmdRename.Text = "Rename";
            this.cmdRename.UseVisualStyleBackColor = true;
            this.cmdRename.Click += new System.EventHandler(this.cmdRename_Click);
            // 
            // cmdCreate
            // 
            this.cmdCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreate.Location = new System.Drawing.Point(6, 259);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(171, 23);
            this.cmdCreate.TabIndex = 1;
            this.cmdCreate.Text = "Create";
            this.cmdCreate.UseVisualStyleBackColor = true;
            this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.btnUp);
            this.grpList.Controls.Add(this.btnDown);
            this.grpList.Controls.Add(this.cmdCreate);
            this.grpList.Controls.Add(this.lstAutoCats);
            this.grpList.Controls.Add(this.cmdRename);
            this.grpList.Controls.Add(this.cmdDelete);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Location = new System.Drawing.Point(0, 0);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(184, 341);
            this.grpList.TabIndex = 0;
            this.grpList.TabStop = false;
            this.grpList.Text = "AutoCat List";
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Enabled = false;
            this.btnUp.Location = new System.Drawing.Point(160, 19);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(21, 23);
            this.btnUp.TabIndex = 15;
            this.btnUp.Text = "+";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Enabled = false;
            this.btnDown.Location = new System.Drawing.Point(160, 48);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(21, 23);
            this.btnDown.TabIndex = 16;
            this.btnDown.Text = "-";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdSave.Location = new System.Drawing.Point(453, 351);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(141, 23);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Save All Changes";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(357, 351);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(90, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(4, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpList);
            this.splitContainer.Panel1MinSize = 125;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitAutoCat);
            this.splitContainer.Panel2MinSize = 400;
            this.splitContainer.Size = new System.Drawing.Size(590, 341);
            this.splitContainer.SplitterDistance = 184;
            this.splitContainer.TabIndex = 7;
            // 
            // splitAutoCat
            // 
            this.splitAutoCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitAutoCat.Location = new System.Drawing.Point(0, 0);
            this.splitAutoCat.Name = "splitAutoCat";
            this.splitAutoCat.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitAutoCat.Panel1
            // 
            this.splitAutoCat.Panel1.Controls.Add(this.chkFilter);
            this.splitAutoCat.Panel1.Controls.Add(this.cboFilter);
            this.splitAutoCat.Size = new System.Drawing.Size(402, 341);
            this.splitAutoCat.SplitterDistance = 26;
            this.splitAutoCat.TabIndex = 0;
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Location = new System.Drawing.Point(3, 5);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(48, 17);
            this.chkFilter.TabIndex = 1;
            this.chkFilter.Text = "Filter";
            this.chkFilter.UseVisualStyleBackColor = true;
            this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // cboFilter
            // 
            this.cboFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFilter.Enabled = false;
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(57, 3);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(338, 21);
            this.cboFilter.TabIndex = 0;
            // 
            // DlgAutoCat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(599, 384);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.MinimumSize = new System.Drawing.Size(615, 400);
            this.Name = "DlgAutoCat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit AutoCats";
            this.Load += new System.EventHandler(this.DlgAutoCat_Load);
            this.grpList.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitAutoCat.Panel1.ResumeLayout(false);
            this.splitAutoCat.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitAutoCat)).EndInit();
            this.splitAutoCat.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstAutoCats;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdRename;
        private System.Windows.Forms.Button cmdCreate;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitAutoCat;
        private System.Windows.Forms.CheckBox chkFilter;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
    }
}