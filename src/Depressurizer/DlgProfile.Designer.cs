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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgProfile));
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
            this.chkActUpdate = new System.Windows.Forms.CheckBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.grpLoadOpt = new System.Windows.Forms.GroupBox();
            this.chkAutoImport = new System.Windows.Forms.CheckBox();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.grpSaveOpt = new System.Windows.Forms.GroupBox();
            this.lblHelp_ExportDiscard = new System.Windows.Forms.Label();
            this.chkExportDiscard = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.tabOpts = new System.Windows.Forms.TabPage();
            this.grpUpdateOpt = new System.Windows.Forms.GroupBox();
            this.lblHelp_LocalUpdate = new System.Windows.Forms.Label();
            this.lblHelp_WebUpdate = new System.Windows.Forms.Label();
            this.chkWebUpdate = new System.Windows.Forms.CheckBox();
            this.chkLocalUpdate = new System.Windows.Forms.CheckBox();
            this.grpOtherOpt = new System.Windows.Forms.GroupBox();
            this.chkIncludeShortcuts = new System.Windows.Forms.CheckBox();
            this.chkOverwriteNames = new System.Windows.Forms.CheckBox();
            this.tabIgnore = new System.Windows.Forms.TabPage();
            this.grpIgnored = new System.Windows.Forms.GroupBox();
            this.cmdIgnore = new System.Windows.Forms.Button();
            this.txtIgnore = new System.Windows.Forms.TextBox();
            this.cmdUnignore = new System.Windows.Forms.Button();
            this.lstIgnored = new System.Windows.Forms.ListView();
            this.grpIgnoreSettings = new System.Windows.Forms.GroupBox();
            this.chkIncludeUnknown = new System.Windows.Forms.CheckBox();
            this.lblHelp_BypassIgnoreOnImport = new System.Windows.Forms.Label();
            this.lblHelp_IncludeUnknown = new System.Windows.Forms.Label();
            this.chkBypassIgnoreOnImport = new System.Windows.Forms.CheckBox();
            this.chkAutoIgnore = new System.Windows.Forms.CheckBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpUserInfo.SuspendLayout();
            this.grpProfInfo.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.grpLoadOpt.SuspendLayout();
            this.grpSaveOpt.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabOpts.SuspendLayout();
            this.grpUpdateOpt.SuspendLayout();
            this.grpOtherOpt.SuspendLayout();
            this.tabIgnore.SuspendLayout();
            this.grpIgnored.SuspendLayout();
            this.grpIgnoreSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFilePath
            // 
            resources.ApplyResources(this.txtFilePath, "txtFilePath");
            this.txtFilePath.Name = "txtFilePath";
            // 
            // cmdBrowse
            // 
            resources.ApplyResources(this.cmdBrowse, "cmdBrowse");
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // lblPath
            // 
            resources.ApplyResources(this.lblPath, "lblPath");
            this.lblPath.Name = "lblPath";
            // 
            // grpUserInfo
            // 
            resources.ApplyResources(this.grpUserInfo, "grpUserInfo");
            this.grpUserInfo.Controls.Add(this.txtUserUrl);
            this.grpUserInfo.Controls.Add(this.radSelUserByURL);
            this.grpUserInfo.Controls.Add(this.radSelUserByID);
            this.grpUserInfo.Controls.Add(this.radSelUserFromList);
            this.grpUserInfo.Controls.Add(this.cmdUserUpdateCancel);
            this.grpUserInfo.Controls.Add(this.lblUserStatus);
            this.grpUserInfo.Controls.Add(this.cmdUserUpdate);
            this.grpUserInfo.Controls.Add(this.txtUserID);
            this.grpUserInfo.Controls.Add(this.lstUsers);
            this.grpUserInfo.Name = "grpUserInfo";
            this.grpUserInfo.TabStop = false;
            // 
            // txtUserUrl
            // 
            resources.ApplyResources(this.txtUserUrl, "txtUserUrl");
            this.txtUserUrl.Name = "txtUserUrl";
            // 
            // radSelUserByURL
            // 
            resources.ApplyResources(this.radSelUserByURL, "radSelUserByURL");
            this.radSelUserByURL.Name = "radSelUserByURL";
            this.radSelUserByURL.TabStop = true;
            this.radSelUserByURL.UseVisualStyleBackColor = true;
            this.radSelUserByURL.CheckedChanged += new System.EventHandler(this.radSelUser_CheckedChanged);
            // 
            // radSelUserByID
            // 
            resources.ApplyResources(this.radSelUserByID, "radSelUserByID");
            this.radSelUserByID.Name = "radSelUserByID";
            this.radSelUserByID.TabStop = true;
            this.radSelUserByID.UseVisualStyleBackColor = true;
            this.radSelUserByID.CheckedChanged += new System.EventHandler(this.radSelUser_CheckedChanged);
            // 
            // radSelUserFromList
            // 
            resources.ApplyResources(this.radSelUserFromList, "radSelUserFromList");
            this.radSelUserFromList.Name = "radSelUserFromList";
            this.radSelUserFromList.TabStop = true;
            this.radSelUserFromList.UseVisualStyleBackColor = true;
            this.radSelUserFromList.CheckedChanged += new System.EventHandler(this.radSelUser_CheckedChanged);
            // 
            // cmdUserUpdateCancel
            // 
            resources.ApplyResources(this.cmdUserUpdateCancel, "cmdUserUpdateCancel");
            this.cmdUserUpdateCancel.Name = "cmdUserUpdateCancel";
            this.cmdUserUpdateCancel.UseVisualStyleBackColor = true;
            this.cmdUserUpdateCancel.Click += new System.EventHandler(this.cmdUserUpdateCancel_Click);
            // 
            // lblUserStatus
            // 
            resources.ApplyResources(this.lblUserStatus, "lblUserStatus");
            this.lblUserStatus.Name = "lblUserStatus";
            // 
            // cmdUserUpdate
            // 
            resources.ApplyResources(this.cmdUserUpdate, "cmdUserUpdate");
            this.cmdUserUpdate.Name = "cmdUserUpdate";
            this.cmdUserUpdate.UseVisualStyleBackColor = true;
            this.cmdUserUpdate.Click += new System.EventHandler(this.cmdUserUpdate_Click);
            // 
            // txtUserID
            // 
            resources.ApplyResources(this.txtUserID, "txtUserID");
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            resources.ApplyResources(this.lstUsers, "lstUsers");
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // grpProfInfo
            // 
            resources.ApplyResources(this.grpProfInfo, "grpProfInfo");
            this.grpProfInfo.Controls.Add(this.txtFilePath);
            this.grpProfInfo.Controls.Add(this.cmdBrowse);
            this.grpProfInfo.Controls.Add(this.lblPath);
            this.grpProfInfo.Name = "grpProfInfo";
            this.grpProfInfo.TabStop = false;
            // 
            // grpActions
            // 
            resources.ApplyResources(this.grpActions, "grpActions");
            this.grpActions.Controls.Add(this.chkSetStartup);
            this.grpActions.Controls.Add(this.chkActImport);
            this.grpActions.Controls.Add(this.chkActUpdate);
            this.grpActions.Name = "grpActions";
            this.grpActions.TabStop = false;
            // 
            // chkSetStartup
            // 
            resources.ApplyResources(this.chkSetStartup, "chkSetStartup");
            this.chkSetStartup.Checked = true;
            this.chkSetStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSetStartup.Name = "chkSetStartup";
            this.chkSetStartup.UseVisualStyleBackColor = true;
            // 
            // chkActImport
            // 
            resources.ApplyResources(this.chkActImport, "chkActImport");
            this.chkActImport.Checked = true;
            this.chkActImport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActImport.Name = "chkActImport";
            this.chkActImport.UseVisualStyleBackColor = true;
            // 
            // chkActUpdate
            // 
            resources.ApplyResources(this.chkActUpdate, "chkActUpdate");
            this.chkActUpdate.Checked = true;
            this.chkActUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActUpdate.Name = "chkActUpdate";
            this.chkActUpdate.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            resources.ApplyResources(this.cmdOk, "cmdOk");
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpLoadOpt
            // 
            resources.ApplyResources(this.grpLoadOpt, "grpLoadOpt");
            this.grpLoadOpt.Controls.Add(this.chkAutoImport);
            this.grpLoadOpt.Controls.Add(this.chkAutoUpdate);
            this.grpLoadOpt.Name = "grpLoadOpt";
            this.grpLoadOpt.TabStop = false;
            // 
            // chkAutoImport
            // 
            resources.ApplyResources(this.chkAutoImport, "chkAutoImport");
            this.chkAutoImport.Name = "chkAutoImport";
            this.chkAutoImport.UseVisualStyleBackColor = true;
            // 
            // chkAutoUpdate
            // 
            resources.ApplyResources(this.chkAutoUpdate, "chkAutoUpdate");
            this.chkAutoUpdate.Checked = true;
            this.chkAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // grpSaveOpt
            // 
            resources.ApplyResources(this.grpSaveOpt, "grpSaveOpt");
            this.grpSaveOpt.Controls.Add(this.lblHelp_ExportDiscard);
            this.grpSaveOpt.Controls.Add(this.chkExportDiscard);
            this.grpSaveOpt.Name = "grpSaveOpt";
            this.grpSaveOpt.TabStop = false;
            // 
            // lblHelp_ExportDiscard
            // 
            resources.ApplyResources(this.lblHelp_ExportDiscard, "lblHelp_ExportDiscard");
            this.lblHelp_ExportDiscard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp_ExportDiscard.Name = "lblHelp_ExportDiscard";
            // 
            // chkExportDiscard
            // 
            resources.ApplyResources(this.chkExportDiscard, "chkExportDiscard");
            this.chkExportDiscard.Checked = true;
            this.chkExportDiscard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportDiscard.Name = "chkExportDiscard";
            this.chkExportDiscard.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabMain);
            this.tabControl.Controls.Add(this.tabOpts);
            this.tabControl.Controls.Add(this.tabIgnore);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.grpProfInfo);
            this.tabMain.Controls.Add(this.grpUserInfo);
            this.tabMain.Controls.Add(this.grpActions);
            resources.ApplyResources(this.tabMain, "tabMain");
            this.tabMain.Name = "tabMain";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tabOpts
            // 
            this.tabOpts.Controls.Add(this.grpUpdateOpt);
            this.tabOpts.Controls.Add(this.grpOtherOpt);
            this.tabOpts.Controls.Add(this.grpSaveOpt);
            this.tabOpts.Controls.Add(this.grpLoadOpt);
            resources.ApplyResources(this.tabOpts, "tabOpts");
            this.tabOpts.Name = "tabOpts";
            this.tabOpts.UseVisualStyleBackColor = true;
            // 
            // grpUpdateOpt
            // 
            resources.ApplyResources(this.grpUpdateOpt, "grpUpdateOpt");
            this.grpUpdateOpt.Controls.Add(this.lblHelp_LocalUpdate);
            this.grpUpdateOpt.Controls.Add(this.lblHelp_WebUpdate);
            this.grpUpdateOpt.Controls.Add(this.chkWebUpdate);
            this.grpUpdateOpt.Controls.Add(this.chkLocalUpdate);
            this.grpUpdateOpt.Name = "grpUpdateOpt";
            this.grpUpdateOpt.TabStop = false;
            // 
            // lblHelp_LocalUpdate
            // 
            resources.ApplyResources(this.lblHelp_LocalUpdate, "lblHelp_LocalUpdate");
            this.lblHelp_LocalUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp_LocalUpdate.Name = "lblHelp_LocalUpdate";
            // 
            // lblHelp_WebUpdate
            // 
            resources.ApplyResources(this.lblHelp_WebUpdate, "lblHelp_WebUpdate");
            this.lblHelp_WebUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp_WebUpdate.Name = "lblHelp_WebUpdate";
            // 
            // chkWebUpdate
            // 
            resources.ApplyResources(this.chkWebUpdate, "chkWebUpdate");
            this.chkWebUpdate.Checked = true;
            this.chkWebUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWebUpdate.Name = "chkWebUpdate";
            this.chkWebUpdate.UseVisualStyleBackColor = true;
            // 
            // chkLocalUpdate
            // 
            resources.ApplyResources(this.chkLocalUpdate, "chkLocalUpdate");
            this.chkLocalUpdate.Checked = true;
            this.chkLocalUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLocalUpdate.Name = "chkLocalUpdate";
            this.chkLocalUpdate.UseVisualStyleBackColor = true;
            // 
            // grpOtherOpt
            // 
            resources.ApplyResources(this.grpOtherOpt, "grpOtherOpt");
            this.grpOtherOpt.Controls.Add(this.chkIncludeShortcuts);
            this.grpOtherOpt.Controls.Add(this.chkOverwriteNames);
            this.grpOtherOpt.Name = "grpOtherOpt";
            this.grpOtherOpt.TabStop = false;
            // 
            // chkIncludeShortcuts
            // 
            resources.ApplyResources(this.chkIncludeShortcuts, "chkIncludeShortcuts");
            this.chkIncludeShortcuts.Checked = true;
            this.chkIncludeShortcuts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeShortcuts.Name = "chkIncludeShortcuts";
            this.chkIncludeShortcuts.UseVisualStyleBackColor = true;
            // 
            // chkOverwriteNames
            // 
            resources.ApplyResources(this.chkOverwriteNames, "chkOverwriteNames");
            this.chkOverwriteNames.Name = "chkOverwriteNames";
            this.chkOverwriteNames.UseVisualStyleBackColor = true;
            // 
            // tabIgnore
            // 
            this.tabIgnore.Controls.Add(this.grpIgnored);
            this.tabIgnore.Controls.Add(this.grpIgnoreSettings);
            resources.ApplyResources(this.tabIgnore, "tabIgnore");
            this.tabIgnore.Name = "tabIgnore";
            this.tabIgnore.UseVisualStyleBackColor = true;
            // 
            // grpIgnored
            // 
            resources.ApplyResources(this.grpIgnored, "grpIgnored");
            this.grpIgnored.Controls.Add(this.cmdIgnore);
            this.grpIgnored.Controls.Add(this.txtIgnore);
            this.grpIgnored.Controls.Add(this.cmdUnignore);
            this.grpIgnored.Controls.Add(this.lstIgnored);
            this.grpIgnored.Name = "grpIgnored";
            this.grpIgnored.TabStop = false;
            // 
            // cmdIgnore
            // 
            resources.ApplyResources(this.cmdIgnore, "cmdIgnore");
            this.cmdIgnore.Name = "cmdIgnore";
            this.cmdIgnore.UseVisualStyleBackColor = true;
            this.cmdIgnore.Click += new System.EventHandler(this.cmdIgnore_Click);
            // 
            // txtIgnore
            // 
            resources.ApplyResources(this.txtIgnore, "txtIgnore");
            this.txtIgnore.Name = "txtIgnore";
            // 
            // cmdUnignore
            // 
            resources.ApplyResources(this.cmdUnignore, "cmdUnignore");
            this.cmdUnignore.Name = "cmdUnignore";
            this.cmdUnignore.UseVisualStyleBackColor = true;
            this.cmdUnignore.Click += new System.EventHandler(this.cmdUnignore_Click);
            // 
            // lstIgnored
            // 
            resources.ApplyResources(this.lstIgnored, "lstIgnored");
            this.lstIgnored.FullRowSelect = true;
            this.lstIgnored.Name = "lstIgnored";
            this.lstIgnored.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstIgnored.UseCompatibleStateImageBehavior = false;
            this.lstIgnored.View = System.Windows.Forms.View.List;
            // 
            // grpIgnoreSettings
            // 
            resources.ApplyResources(this.grpIgnoreSettings, "grpIgnoreSettings");
            this.grpIgnoreSettings.Controls.Add(this.chkIncludeUnknown);
            this.grpIgnoreSettings.Controls.Add(this.lblHelp_BypassIgnoreOnImport);
            this.grpIgnoreSettings.Controls.Add(this.lblHelp_IncludeUnknown);
            this.grpIgnoreSettings.Controls.Add(this.chkBypassIgnoreOnImport);
            this.grpIgnoreSettings.Controls.Add(this.chkAutoIgnore);
            this.grpIgnoreSettings.Name = "grpIgnoreSettings";
            this.grpIgnoreSettings.TabStop = false;
            // 
            // chkIncludeUnknown
            // 
            resources.ApplyResources(this.chkIncludeUnknown, "chkIncludeUnknown");
            this.chkIncludeUnknown.Name = "chkIncludeUnknown";
            this.chkIncludeUnknown.UseVisualStyleBackColor = true;
            // 
            // lblHelp_BypassIgnoreOnImport
            // 
            resources.ApplyResources(this.lblHelp_BypassIgnoreOnImport, "lblHelp_BypassIgnoreOnImport");
            this.lblHelp_BypassIgnoreOnImport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp_BypassIgnoreOnImport.Name = "lblHelp_BypassIgnoreOnImport";
            // 
            // lblHelp_IncludeUnknown
            // 
            resources.ApplyResources(this.lblHelp_IncludeUnknown, "lblHelp_IncludeUnknown");
            this.lblHelp_IncludeUnknown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp_IncludeUnknown.Name = "lblHelp_IncludeUnknown";
            // 
            // chkBypassIgnoreOnImport
            // 
            resources.ApplyResources(this.chkBypassIgnoreOnImport, "chkBypassIgnoreOnImport");
            this.chkBypassIgnoreOnImport.Name = "chkBypassIgnoreOnImport";
            this.chkBypassIgnoreOnImport.UseVisualStyleBackColor = true;
            // 
            // chkAutoIgnore
            // 
            resources.ApplyResources(this.chkAutoIgnore, "chkAutoIgnore");
            this.chkAutoIgnore.Checked = true;
            this.chkAutoIgnore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoIgnore.Name = "chkAutoIgnore";
            this.chkAutoIgnore.UseVisualStyleBackColor = true;
            // 
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // DlgProfile
            // 
            this.AcceptButton = this.cmdOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgProfile";
            this.ShowInTaskbar = false;
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
            this.grpUpdateOpt.ResumeLayout(false);
            this.grpUpdateOpt.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkActUpdate;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox grpLoadOpt;
        private System.Windows.Forms.CheckBox chkAutoImport;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.GroupBox grpSaveOpt;
        private System.Windows.Forms.CheckBox chkExportDiscard;
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
        private System.Windows.Forms.Label lblUserStatus;
        private System.Windows.Forms.Button cmdUserUpdate;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Button cmdUserUpdateCancel;
        private System.Windows.Forms.TextBox txtUserUrl;
        private System.Windows.Forms.RadioButton radSelUserByURL;
        private System.Windows.Forms.RadioButton radSelUserByID;
        private System.Windows.Forms.RadioButton radSelUserFromList;
        private System.Windows.Forms.CheckBox chkIncludeShortcuts;
        private System.Windows.Forms.GroupBox grpUpdateOpt;
        private System.Windows.Forms.CheckBox chkWebUpdate;
        private System.Windows.Forms.CheckBox chkLocalUpdate;
        private System.Windows.Forms.Label lblHelp_WebUpdate;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Label lblHelp_ExportDiscard;
        private System.Windows.Forms.Label lblHelp_LocalUpdate;
        private System.Windows.Forms.Label lblHelp_BypassIgnoreOnImport;
        private System.Windows.Forms.Label lblHelp_IncludeUnknown;
        private System.Windows.Forms.CheckBox chkBypassIgnoreOnImport;
        private System.Windows.Forms.CheckBox chkIncludeUnknown;
    }
}