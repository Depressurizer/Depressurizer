/*
Copyright 2011, 2012, 2013 Steve Labbe.

This file is part of Depressurizer.

Depressurizer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Depressurizer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/
namespace Depressurizer {
    partial class DlgProfile {
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
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.grpUserInfo = new System.Windows.Forms.GroupBox();
            this.txtUserUrl = new System.Windows.Forms.TextBox();
            this.radSelUserByURL = new System.Windows.Forms.RadioButton();
            this.radSelUserByID = new System.Windows.Forms.RadioButton();
            this.radSelUserFromList = new System.Windows.Forms.RadioButton();
            this.cmdUserUpdateCancel = new System.Windows.Forms.Button();
            this.lblUserStatus = new System.Windows.Forms.Label();
            this.cmdUserUpdate = new System.Windows.Forms.Button();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lstUsers = new System.Windows.Forms.ListBox();
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
            this.grpOtherOpt = new System.Windows.Forms.GroupBox();
            this.chkOverwriteNames = new System.Windows.Forms.CheckBox();
            this.tabIgnore = new System.Windows.Forms.TabPage();
            this.grpIgnored = new System.Windows.Forms.GroupBox();
            this.cmdIgnore = new System.Windows.Forms.Button();
            this.txtIgnore = new System.Windows.Forms.TextBox();
            this.cmdUnignore = new System.Windows.Forms.Button();
            this.lstIgnored = new System.Windows.Forms.ListView();
            this.grpIgnoreSettings = new System.Windows.Forms.GroupBox();
            this.chkIgnoreDlc = new System.Windows.Forms.CheckBox();
            this.chkAutoIgnore = new System.Windows.Forms.CheckBox();
            this.grpUserInfo.SuspendLayout();
            this.grpProfInfo.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.grpLoadOpt.SuspendLayout();
            this.grpSaveOpt.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabOpts.SuspendLayout();
            this.grpOtherOpt.SuspendLayout();
            this.tabIgnore.SuspendLayout();
            this.grpIgnored.SuspendLayout();
            this.grpIgnoreSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(91, 13);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(371, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.Location = new System.Drawing.Point(468, 11);
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
            // grpUserInfo
            // 
            this.grpUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUserInfo.Controls.Add(this.txtUserUrl);
            this.grpUserInfo.Controls.Add(this.radSelUserByURL);
            this.grpUserInfo.Controls.Add(this.radSelUserByID);
            this.grpUserInfo.Controls.Add(this.radSelUserFromList);
            this.grpUserInfo.Controls.Add(this.cmdUserUpdateCancel);
            this.grpUserInfo.Controls.Add(this.lblUserStatus);
            this.grpUserInfo.Controls.Add(this.cmdUserUpdate);
            this.grpUserInfo.Controls.Add(this.txtUserID);
            this.grpUserInfo.Controls.Add(this.lstUsers);
            this.grpUserInfo.Location = new System.Drawing.Point(6, 52);
            this.grpUserInfo.Name = "grpUserInfo";
            this.grpUserInfo.Size = new System.Drawing.Size(550, 189);
            this.grpUserInfo.TabIndex = 1;
            this.grpUserInfo.TabStop = false;
            this.grpUserInfo.Text = "Select User";
            // 
            // txtUserUrl
            // 
            this.txtUserUrl.Location = new System.Drawing.Point(201, 159);
            this.txtUserUrl.Name = "txtUserUrl";
            this.txtUserUrl.Size = new System.Drawing.Size(343, 20);
            this.txtUserUrl.TabIndex = 0;
            // 
            // radSelUserByURL
            // 
            this.radSelUserByURL.AutoSize = true;
            this.radSelUserByURL.Location = new System.Drawing.Point(6, 160);
            this.radSelUserByURL.Name = "radSelUserByURL";
            this.radSelUserByURL.Size = new System.Drawing.Size(189, 17);
            this.radSelUserByURL.TabIndex = 7;
            this.radSelUserByURL.TabStop = true;
            this.radSelUserByURL.Text = "Manually enter Custom URL name:";
            this.radSelUserByURL.UseVisualStyleBackColor = true;
            this.radSelUserByURL.CheckedChanged += new System.EventHandler(this.radSelUser_CheckedChanged);
            // 
            // radSelUserByID
            // 
            this.radSelUserByID.AutoSize = true;
            this.radSelUserByID.Location = new System.Drawing.Point(6, 134);
            this.radSelUserByID.Name = "radSelUserByID";
            this.radSelUserByID.Size = new System.Drawing.Size(173, 17);
            this.radSelUserByID.TabIndex = 5;
            this.radSelUserByID.TabStop = true;
            this.radSelUserByID.Text = "Manually enter 64-bit Steam ID:";
            this.radSelUserByID.UseVisualStyleBackColor = true;
            this.radSelUserByID.CheckedChanged += new System.EventHandler(this.radSelUser_CheckedChanged);
            // 
            // radSelUserFromList
            // 
            this.radSelUserFromList.AutoSize = true;
            this.radSelUserFromList.Location = new System.Drawing.Point(6, 19);
            this.radSelUserFromList.Name = "radSelUserFromList";
            this.radSelUserFromList.Size = new System.Drawing.Size(222, 17);
            this.radSelUserFromList.TabIndex = 0;
            this.radSelUserFromList.TabStop = true;
            this.radSelUserFromList.Text = "Select from users found on local machine:";
            this.radSelUserFromList.UseVisualStyleBackColor = true;
            this.radSelUserFromList.CheckedChanged += new System.EventHandler(this.radSelUser_CheckedChanged);
            // 
            // cmdUserUpdateCancel
            // 
            this.cmdUserUpdateCancel.Enabled = false;
            this.cmdUserUpdateCancel.Location = new System.Drawing.Point(469, 104);
            this.cmdUserUpdateCancel.Name = "cmdUserUpdateCancel";
            this.cmdUserUpdateCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdUserUpdateCancel.TabIndex = 4;
            this.cmdUserUpdateCancel.Text = "Cancel";
            this.cmdUserUpdateCancel.UseVisualStyleBackColor = true;
            this.cmdUserUpdateCancel.Click += new System.EventHandler(this.cmdUserUpdateCancel_Click);
            // 
            // lblUserStatus
            // 
            this.lblUserStatus.Location = new System.Drawing.Point(6, 109);
            this.lblUserStatus.Name = "lblUserStatus";
            this.lblUserStatus.Size = new System.Drawing.Size(375, 13);
            this.lblUserStatus.TabIndex = 2;
            this.lblUserStatus.Text = "Press Update to get display names for the account numbers above.";
            this.lblUserStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmdUserUpdate
            // 
            this.cmdUserUpdate.Location = new System.Drawing.Point(387, 104);
            this.cmdUserUpdate.Name = "cmdUserUpdate";
            this.cmdUserUpdate.Size = new System.Drawing.Size(76, 23);
            this.cmdUserUpdate.TabIndex = 3;
            this.cmdUserUpdate.Text = "Update";
            this.cmdUserUpdate.UseVisualStyleBackColor = true;
            this.cmdUserUpdate.Click += new System.EventHandler(this.cmdUserUpdate_Click);
            // 
            // txtUserID
            // 
            this.txtUserID.Enabled = false;
            this.txtUserID.Location = new System.Drawing.Point(201, 133);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(343, 20);
            this.txtUserID.TabIndex = 6;
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.Location = new System.Drawing.Point(22, 42);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(522, 56);
            this.lstUsers.TabIndex = 1;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
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
            this.grpProfInfo.Size = new System.Drawing.Size(550, 40);
            this.grpProfInfo.TabIndex = 0;
            this.grpProfInfo.TabStop = false;
            this.grpProfInfo.Text = "Profile Info";
            // 
            // grpActions
            // 
            this.grpActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpActions.Controls.Add(this.chkSetStartup);
            this.grpActions.Controls.Add(this.chkActImport);
            this.grpActions.Controls.Add(this.chkActDownload);
            this.grpActions.Location = new System.Drawing.Point(6, 247);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(550, 91);
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
            this.cmdOk.Location = new System.Drawing.Point(507, 387);
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
            this.cmdCancel.Location = new System.Drawing.Point(426, 387);
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
            this.grpLoadOpt.Size = new System.Drawing.Size(523, 65);
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
            this.grpSaveOpt.Size = new System.Drawing.Size(523, 65);
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
            this.tabControl.Controls.Add(this.tabIgnore);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(570, 369);
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
            this.tabMain.Size = new System.Drawing.Size(562, 343);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tabOpts
            // 
            this.tabOpts.Controls.Add(this.grpOtherOpt);
            this.tabOpts.Controls.Add(this.grpSaveOpt);
            this.tabOpts.Controls.Add(this.grpLoadOpt);
            this.tabOpts.Location = new System.Drawing.Point(4, 22);
            this.tabOpts.Name = "tabOpts";
            this.tabOpts.Padding = new System.Windows.Forms.Padding(3);
            this.tabOpts.Size = new System.Drawing.Size(562, 343);
            this.tabOpts.TabIndex = 1;
            this.tabOpts.Text = "Options";
            this.tabOpts.UseVisualStyleBackColor = true;
            // 
            // grpOtherOpt
            // 
            this.grpOtherOpt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOtherOpt.Controls.Add(this.chkOverwriteNames);
            this.grpOtherOpt.Location = new System.Drawing.Point(6, 148);
            this.grpOtherOpt.Name = "grpOtherOpt";
            this.grpOtherOpt.Size = new System.Drawing.Size(523, 45);
            this.grpOtherOpt.TabIndex = 2;
            this.grpOtherOpt.TabStop = false;
            this.grpOtherOpt.Text = "Other Options";
            // 
            // chkOverwriteNames
            // 
            this.chkOverwriteNames.AutoSize = true;
            this.chkOverwriteNames.Location = new System.Drawing.Point(6, 19);
            this.chkOverwriteNames.Name = "chkOverwriteNames";
            this.chkOverwriteNames.Size = new System.Drawing.Size(241, 17);
            this.chkOverwriteNames.TabIndex = 0;
            this.chkOverwriteNames.Text = "Overwrite names when downloading game list";
            this.chkOverwriteNames.UseVisualStyleBackColor = true;
            // 
            // tabIgnore
            // 
            this.tabIgnore.Controls.Add(this.grpIgnored);
            this.tabIgnore.Controls.Add(this.grpIgnoreSettings);
            this.tabIgnore.Location = new System.Drawing.Point(4, 22);
            this.tabIgnore.Name = "tabIgnore";
            this.tabIgnore.Size = new System.Drawing.Size(562, 343);
            this.tabIgnore.TabIndex = 2;
            this.tabIgnore.Text = "Ignored Games";
            this.tabIgnore.UseVisualStyleBackColor = true;
            // 
            // grpIgnored
            // 
            this.grpIgnored.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIgnored.Controls.Add(this.cmdIgnore);
            this.grpIgnored.Controls.Add(this.txtIgnore);
            this.grpIgnored.Controls.Add(this.cmdUnignore);
            this.grpIgnored.Controls.Add(this.lstIgnored);
            this.grpIgnored.Location = new System.Drawing.Point(3, 3);
            this.grpIgnored.Name = "grpIgnored";
            this.grpIgnored.Size = new System.Drawing.Size(296, 329);
            this.grpIgnored.TabIndex = 0;
            this.grpIgnored.TabStop = false;
            this.grpIgnored.Text = "Ignored Games";
            // 
            // cmdIgnore
            // 
            this.cmdIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdIgnore.Location = new System.Drawing.Point(187, 45);
            this.cmdIgnore.Name = "cmdIgnore";
            this.cmdIgnore.Size = new System.Drawing.Size(103, 23);
            this.cmdIgnore.TabIndex = 2;
            this.cmdIgnore.Text = "Ignore";
            this.cmdIgnore.UseVisualStyleBackColor = true;
            this.cmdIgnore.Click += new System.EventHandler(this.cmdIgnore_Click);
            // 
            // txtIgnore
            // 
            this.txtIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIgnore.Location = new System.Drawing.Point(187, 19);
            this.txtIgnore.Name = "txtIgnore";
            this.txtIgnore.Size = new System.Drawing.Size(103, 20);
            this.txtIgnore.TabIndex = 1;
            // 
            // cmdUnignore
            // 
            this.cmdUnignore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUnignore.Location = new System.Drawing.Point(187, 300);
            this.cmdUnignore.Name = "cmdUnignore";
            this.cmdUnignore.Size = new System.Drawing.Size(103, 23);
            this.cmdUnignore.TabIndex = 3;
            this.cmdUnignore.Text = "Unignore Selected";
            this.cmdUnignore.UseVisualStyleBackColor = true;
            this.cmdUnignore.Click += new System.EventHandler(this.cmdUnignore_Click);
            // 
            // lstIgnored
            // 
            this.lstIgnored.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstIgnored.FullRowSelect = true;
            this.lstIgnored.Location = new System.Drawing.Point(6, 19);
            this.lstIgnored.Name = "lstIgnored";
            this.lstIgnored.Size = new System.Drawing.Size(175, 304);
            this.lstIgnored.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstIgnored.TabIndex = 0;
            this.lstIgnored.UseCompatibleStateImageBehavior = false;
            this.lstIgnored.View = System.Windows.Forms.View.List;
            // 
            // grpIgnoreSettings
            // 
            this.grpIgnoreSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIgnoreSettings.Controls.Add(this.chkIgnoreDlc);
            this.grpIgnoreSettings.Controls.Add(this.chkAutoIgnore);
            this.grpIgnoreSettings.Location = new System.Drawing.Point(305, 3);
            this.grpIgnoreSettings.Name = "grpIgnoreSettings";
            this.grpIgnoreSettings.Size = new System.Drawing.Size(227, 329);
            this.grpIgnoreSettings.TabIndex = 1;
            this.grpIgnoreSettings.TabStop = false;
            this.grpIgnoreSettings.Text = "Settings";
            // 
            // chkIgnoreDlc
            // 
            this.chkIgnoreDlc.AutoSize = true;
            this.chkIgnoreDlc.Checked = true;
            this.chkIgnoreDlc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreDlc.Location = new System.Drawing.Point(6, 58);
            this.chkIgnoreDlc.Name = "chkIgnoreDlc";
            this.chkIgnoreDlc.Size = new System.Drawing.Size(115, 17);
            this.chkIgnoreDlc.TabIndex = 1;
            this.chkIgnoreDlc.Text = "Also ignore all DLC";
            this.chkIgnoreDlc.UseVisualStyleBackColor = true;
            // 
            // chkAutoIgnore
            // 
            this.chkAutoIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoIgnore.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAutoIgnore.Checked = true;
            this.chkAutoIgnore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoIgnore.Location = new System.Drawing.Point(6, 19);
            this.chkAutoIgnore.Name = "chkAutoIgnore";
            this.chkAutoIgnore.Size = new System.Drawing.Size(215, 33);
            this.chkAutoIgnore.TabIndex = 0;
            this.chkAutoIgnore.Text = "Automatically Ignore games when removing them";
            this.chkAutoIgnore.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAutoIgnore.UseVisualStyleBackColor = true;
            // 
            // ProfileDlg
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(594, 420);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ProfileDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Profile";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProfileDlg_FormClosing);
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
            this.grpOtherOpt.ResumeLayout(false);
            this.grpOtherOpt.PerformLayout();
            this.tabIgnore.ResumeLayout(false);
            this.grpIgnored.ResumeLayout(false);
            this.grpIgnored.PerformLayout();
            this.grpIgnoreSettings.ResumeLayout(false);
            this.grpIgnoreSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.GroupBox grpUserInfo;
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
        private System.Windows.Forms.TabPage tabIgnore;
        private System.Windows.Forms.GroupBox grpIgnored;
        private System.Windows.Forms.Button cmdIgnore;
        private System.Windows.Forms.TextBox txtIgnore;
        private System.Windows.Forms.Button cmdUnignore;
        private System.Windows.Forms.ListView lstIgnored;
        private System.Windows.Forms.GroupBox grpIgnoreSettings;
        private System.Windows.Forms.CheckBox chkAutoIgnore;
        private System.Windows.Forms.GroupBox grpOtherOpt;
        private System.Windows.Forms.CheckBox chkOverwriteNames;
        private System.Windows.Forms.CheckBox chkIgnoreDlc;
        private System.Windows.Forms.Label lblUserStatus;
        private System.Windows.Forms.Button cmdUserUpdate;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Button cmdUserUpdateCancel;
        private System.Windows.Forms.TextBox txtUserUrl;
        private System.Windows.Forms.RadioButton radSelUserByURL;
        private System.Windows.Forms.RadioButton radSelUserByID;
        private System.Windows.Forms.RadioButton radSelUserFromList;
    }
}