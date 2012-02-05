namespace Depressurizer {
    partial class ProfileDlg {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileDlg));
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.cmbAccountID = new System.Windows.Forms.ComboBox();
            this.lblAccIDLabel = new System.Windows.Forms.Label();
            this.txtCommunityName = new System.Windows.Forms.TextBox();
            this.lblCommNameLabel = new System.Windows.Forms.Label();
            this.grpUserInfo = new System.Windows.Forms.GroupBox();
            this.lblCommNameDesc = new System.Windows.Forms.Label();
            this.lblAccIDDesc = new System.Windows.Forms.Label();
            this.grpProfInfo = new System.Windows.Forms.GroupBox();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.chkSetStartup = new System.Windows.Forms.CheckBox();
            this.chkActImport = new System.Windows.Forms.CheckBox();
            this.chkActDownload = new System.Windows.Forms.CheckBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.grpLoadOpt = new System.Windows.Forms.GroupBox();
            this.chkAutoImport = new System.Windows.Forms.CheckBox();
            this.chkAutoDownload = new System.Windows.Forms.CheckBox();
            this.grpSaveOpt = new System.Windows.Forms.GroupBox();
            this.chkExportDiscard = new System.Windows.Forms.CheckBox();
            this.chkAutoExport = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.tabOpts = new System.Windows.Forms.TabPage();
            this.grpUserInfo.SuspendLayout();
            this.grpProfInfo.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.grpLoadOpt.SuspendLayout();
            this.grpSaveOpt.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabOpts.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(91, 13);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(435, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.Location = new System.Drawing.Point(532, 11);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(76, 23);
            this.cmdBrowse.TabIndex = 2;
            this.cmdBrowse.Text = "Browse...";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(6, 16);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(79, 13);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "Profile file path:";
            // 
            // cmbAccountID
            // 
            this.cmbAccountID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAccountID.FormattingEnabled = true;
            this.cmbAccountID.Location = new System.Drawing.Point(162, 13);
            this.cmbAccountID.Name = "cmbAccountID";
            this.cmbAccountID.Size = new System.Drawing.Size(446, 21);
            this.cmbAccountID.TabIndex = 1;
            // 
            // lblAccIDLabel
            // 
            this.lblAccIDLabel.AutoSize = true;
            this.lblAccIDLabel.Location = new System.Drawing.Point(59, 16);
            this.lblAccIDLabel.Name = "lblAccIDLabel";
            this.lblAccIDLabel.Size = new System.Drawing.Size(97, 13);
            this.lblAccIDLabel.TabIndex = 0;
            this.lblAccIDLabel.Text = "Steam Account ID:";
            // 
            // txtCommunityName
            // 
            this.txtCommunityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommunityName.Location = new System.Drawing.Point(162, 84);
            this.txtCommunityName.Name = "txtCommunityName";
            this.txtCommunityName.Size = new System.Drawing.Size(446, 20);
            this.txtCommunityName.TabIndex = 4;
            // 
            // lblCommNameLabel
            // 
            this.lblCommNameLabel.AutoSize = true;
            this.lblCommNameLabel.Location = new System.Drawing.Point(6, 87);
            this.lblCommNameLabel.Name = "lblCommNameLabel";
            this.lblCommNameLabel.Size = new System.Drawing.Size(150, 13);
            this.lblCommNameLabel.TabIndex = 3;
            this.lblCommNameLabel.Text = "Steam Community URL Name:";
            // 
            // grpUserInfo
            // 
            this.grpUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUserInfo.Controls.Add(this.lblCommNameDesc);
            this.grpUserInfo.Controls.Add(this.lblAccIDDesc);
            this.grpUserInfo.Controls.Add(this.lblAccIDLabel);
            this.grpUserInfo.Controls.Add(this.lblCommNameLabel);
            this.grpUserInfo.Controls.Add(this.txtCommunityName);
            this.grpUserInfo.Controls.Add(this.cmbAccountID);
            this.grpUserInfo.Location = new System.Drawing.Point(6, 52);
            this.grpUserInfo.Name = "grpUserInfo";
            this.grpUserInfo.Size = new System.Drawing.Size(614, 181);
            this.grpUserInfo.TabIndex = 1;
            this.grpUserInfo.TabStop = false;
            this.grpUserInfo.Text = "User Info";
            // 
            // lblCommNameDesc
            // 
            this.lblCommNameDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCommNameDesc.Location = new System.Drawing.Point(159, 107);
            this.lblCommNameDesc.Name = "lblCommNameDesc";
            this.lblCommNameDesc.Size = new System.Drawing.Size(449, 72);
            this.lblCommNameDesc.TabIndex = 5;
            this.lblCommNameDesc.Text = resources.GetString("lblCommNameDesc.Text");
            // 
            // lblAccIDDesc
            // 
            this.lblAccIDDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccIDDesc.Location = new System.Drawing.Point(162, 37);
            this.lblAccIDDesc.Name = "lblAccIDDesc";
            this.lblAccIDDesc.Size = new System.Drawing.Size(446, 41);
            this.lblAccIDDesc.TabIndex = 2;
            this.lblAccIDDesc.Text = "Your account ID should be a number. It determines where Steam stores your setting" +
    "s.\r\nThe list will contain all the IDs with saved info on this computer.";
            // 
            // grpProfInfo
            // 
            this.grpProfInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProfInfo.Controls.Add(this.txtFilePath);
            this.grpProfInfo.Controls.Add(this.cmdBrowse);
            this.grpProfInfo.Controls.Add(this.lblPath);
            this.grpProfInfo.Location = new System.Drawing.Point(6, 6);
            this.grpProfInfo.Name = "grpProfInfo";
            this.grpProfInfo.Size = new System.Drawing.Size(614, 40);
            this.grpProfInfo.TabIndex = 0;
            this.grpProfInfo.TabStop = false;
            this.grpProfInfo.Text = "Profile Info";
            // 
            // grpActions
            // 
            this.grpActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpActions.Controls.Add(this.chkSetStartup);
            this.grpActions.Controls.Add(this.chkActImport);
            this.grpActions.Controls.Add(this.chkActDownload);
            this.grpActions.Location = new System.Drawing.Point(6, 239);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(614, 91);
            this.grpActions.TabIndex = 2;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Additional Actions";
            // 
            // chkSetStartup
            // 
            this.chkSetStartup.AutoSize = true;
            this.chkSetStartup.Checked = true;
            this.chkSetStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSetStartup.Location = new System.Drawing.Point(9, 67);
            this.chkSetStartup.Name = "chkSetStartup";
            this.chkSetStartup.Size = new System.Drawing.Size(245, 17);
            this.chkSetStartup.TabIndex = 2;
            this.chkSetStartup.Text = "Set this profile to be loaded on program startup";
            this.chkSetStartup.UseVisualStyleBackColor = true;
            // 
            // chkActImport
            // 
            this.chkActImport.AutoSize = true;
            this.chkActImport.Checked = true;
            this.chkActImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActImport.Location = new System.Drawing.Point(9, 44);
            this.chkActImport.Name = "chkActImport";
            this.chkActImport.Size = new System.Drawing.Size(209, 17);
            this.chkActImport.TabIndex = 1;
            this.chkActImport.Text = "Import categories from Steam right now";
            this.chkActImport.UseVisualStyleBackColor = true;
            // 
            // chkActDownload
            // 
            this.chkActDownload.AutoSize = true;
            this.chkActDownload.Checked = true;
            this.chkActDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActDownload.Location = new System.Drawing.Point(9, 20);
            this.chkActDownload.Name = "chkActDownload";
            this.chkActDownload.Size = new System.Drawing.Size(259, 17);
            this.chkActDownload.TabIndex = 0;
            this.chkActDownload.Text = "Download game list from community site right now";
            this.chkActDownload.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.Location = new System.Drawing.Point(571, 379);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(490, 379);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpLoadOpt
            // 
            this.grpLoadOpt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLoadOpt.Controls.Add(this.chkAutoImport);
            this.grpLoadOpt.Controls.Add(this.chkAutoDownload);
            this.grpLoadOpt.Location = new System.Drawing.Point(6, 6);
            this.grpLoadOpt.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.grpLoadOpt.Name = "grpLoadOpt";
            this.grpLoadOpt.Size = new System.Drawing.Size(614, 65);
            this.grpLoadOpt.TabIndex = 0;
            this.grpLoadOpt.TabStop = false;
            this.grpLoadOpt.Text = "Profile Loading Options";
            // 
            // chkAutoImport
            // 
            this.chkAutoImport.AutoSize = true;
            this.chkAutoImport.Location = new System.Drawing.Point(6, 42);
            this.chkAutoImport.Name = "chkAutoImport";
            this.chkAutoImport.Size = new System.Drawing.Size(265, 17);
            this.chkAutoImport.TabIndex = 1;
            this.chkAutoImport.Text = "Automatically import categories from Steam on load";
            this.chkAutoImport.UseVisualStyleBackColor = true;
            // 
            // chkAutoDownload
            // 
            this.chkAutoDownload.AutoSize = true;
            this.chkAutoDownload.Checked = true;
            this.chkAutoDownload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoDownload.Location = new System.Drawing.Point(6, 19);
            this.chkAutoDownload.Name = "chkAutoDownload";
            this.chkAutoDownload.Size = new System.Drawing.Size(329, 17);
            this.chkAutoDownload.TabIndex = 0;
            this.chkAutoDownload.Text = "Automatically download game list from Steam Community on load";
            this.chkAutoDownload.UseVisualStyleBackColor = true;
            // 
            // grpSaveOpt
            // 
            this.grpSaveOpt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSaveOpt.Controls.Add(this.chkExportDiscard);
            this.grpSaveOpt.Controls.Add(this.chkAutoExport);
            this.grpSaveOpt.Location = new System.Drawing.Point(6, 77);
            this.grpSaveOpt.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.grpSaveOpt.Name = "grpSaveOpt";
            this.grpSaveOpt.Size = new System.Drawing.Size(614, 65);
            this.grpSaveOpt.TabIndex = 1;
            this.grpSaveOpt.TabStop = false;
            this.grpSaveOpt.Text = "Profile Saving Options";
            // 
            // chkExportDiscard
            // 
            this.chkExportDiscard.AutoSize = true;
            this.chkExportDiscard.Checked = true;
            this.chkExportDiscard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportDiscard.Location = new System.Drawing.Point(6, 42);
            this.chkExportDiscard.Name = "chkExportDiscard";
            this.chkExportDiscard.Size = new System.Drawing.Size(370, 17);
            this.chkExportDiscard.TabIndex = 1;
            this.chkExportDiscard.Text = "Remove entries for deleted or unknown games in Steam configuration file";
            this.chkExportDiscard.UseVisualStyleBackColor = true;
            // 
            // chkAutoExport
            // 
            this.chkAutoExport.AutoSize = true;
            this.chkAutoExport.Checked = true;
            this.chkAutoExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoExport.Location = new System.Drawing.Point(6, 19);
            this.chkAutoExport.Name = "chkAutoExport";
            this.chkAutoExport.Size = new System.Drawing.Size(305, 17);
            this.chkAutoExport.TabIndex = 0;
            this.chkAutoExport.Text = "Automatically save categories to Steam when saving profile";
            this.chkAutoExport.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabMain);
            this.tabControl.Controls.Add(this.tabOpts);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(634, 361);
            this.tabControl.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.grpProfInfo);
            this.tabMain.Controls.Add(this.grpUserInfo);
            this.tabMain.Controls.Add(this.grpActions);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(626, 335);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tabOpts
            // 
            this.tabOpts.Controls.Add(this.grpSaveOpt);
            this.tabOpts.Controls.Add(this.grpLoadOpt);
            this.tabOpts.Location = new System.Drawing.Point(4, 22);
            this.tabOpts.Name = "tabOpts";
            this.tabOpts.Padding = new System.Windows.Forms.Padding(3);
            this.tabOpts.Size = new System.Drawing.Size(626, 335);
            this.tabOpts.TabIndex = 1;
            this.tabOpts.Text = "Options";
            this.tabOpts.UseVisualStyleBackColor = true;
            // 
            // ProfileDlg
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(658, 412);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ProfileDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Profile";
            this.Load += new System.EventHandler(this.ProfileDlg_Load);
            this.grpUserInfo.ResumeLayout(false);
            this.grpUserInfo.PerformLayout();
            this.grpProfInfo.ResumeLayout(false);
            this.grpProfInfo.PerformLayout();
            this.grpActions.ResumeLayout(false);
            this.grpActions.PerformLayout();
            this.grpLoadOpt.ResumeLayout(false);
            this.grpLoadOpt.PerformLayout();
            this.grpSaveOpt.ResumeLayout(false);
            this.grpSaveOpt.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabOpts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.ComboBox cmbAccountID;
        private System.Windows.Forms.Label lblAccIDLabel;
        private System.Windows.Forms.TextBox txtCommunityName;
        private System.Windows.Forms.Label lblCommNameLabel;
        private System.Windows.Forms.GroupBox grpUserInfo;
        private System.Windows.Forms.Label lblCommNameDesc;
        private System.Windows.Forms.Label lblAccIDDesc;
        private System.Windows.Forms.GroupBox grpProfInfo;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.CheckBox chkActImport;
        private System.Windows.Forms.CheckBox chkActDownload;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox grpLoadOpt;
        private System.Windows.Forms.CheckBox chkAutoImport;
        private System.Windows.Forms.CheckBox chkAutoDownload;
        private System.Windows.Forms.GroupBox grpSaveOpt;
        private System.Windows.Forms.CheckBox chkExportDiscard;
        private System.Windows.Forms.CheckBox chkAutoExport;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabOpts;
        private System.Windows.Forms.CheckBox chkSetStartup;
    }
}