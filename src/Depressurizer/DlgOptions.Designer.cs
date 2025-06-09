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
    partial class DlgOptions {
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgOptions));
            grpSteamDir = new System.Windows.Forms.GroupBox();
            cmdSteamPathBrowse = new System.Windows.Forms.Button();
            txtSteamPath = new System.Windows.Forms.TextBox();
            grpStartup = new System.Windows.Forms.GroupBox();
            radNone = new System.Windows.Forms.RadioButton();
            radCreate = new System.Windows.Forms.RadioButton();
            radLoad = new System.Windows.Forms.RadioButton();
            cmdDefaultProfileBrowse = new System.Windows.Forms.Button();
            txtDefaultProfile = new System.Windows.Forms.TextBox();
            chkRemoveExtraEntries = new System.Windows.Forms.CheckBox();
            cmdAccept = new System.Windows.Forms.Button();
            cmdCancel = new System.Windows.Forms.Button();
            grpSaving = new System.Windows.Forms.GroupBox();
            tabControl = new System.Windows.Forms.TabControl();
            tabGeneral = new System.Windows.Forms.TabPage();
            grpStoreLanguage = new System.Windows.Forms.GroupBox();
            cmbStoreLanguage = new System.Windows.Forms.ComboBox();
            grpDepressurizerUpdates = new System.Windows.Forms.GroupBox();
            chkCheckForDepressurizerUpdates = new System.Windows.Forms.CheckBox();
            grpDatabase = new System.Windows.Forms.GroupBox();
            numScrapePromptDays = new System.Windows.Forms.NumericUpDown();
            lblScrapePrompt2 = new System.Windows.Forms.Label();
            lblScapePrompt1 = new System.Windows.Forms.Label();
            helpIncludeImputedTimes = new System.Windows.Forms.Label();
            chkIncludeImputedTimes = new System.Windows.Forms.CheckBox();
            chkUpdateHltbOnStartup = new System.Windows.Forms.CheckBox();
            chkAutosaveDB = new System.Windows.Forms.CheckBox();
            chkUpdateAppInfoOnStartup = new System.Windows.Forms.CheckBox();
            grpUILanguage = new System.Windows.Forms.GroupBox();
            cmbUILanguage = new System.Windows.Forms.ComboBox();
            tabPage1 = new System.Windows.Forms.TabPage();
            cmdDefaultIgnored = new System.Windows.Forms.Button();
            cmdIgnore = new System.Windows.Forms.Button();
            txtIgnore = new System.Windows.Forms.TextBox();
            cmdUnignore = new System.Windows.Forms.Button();
            lstIgnored = new System.Windows.Forms.ListView();
            ttHelp = new Depressurizer.Lib.ExtToolTip();
            checkBoxReadFromLevelDB = new System.Windows.Forms.CheckBox();
            grpSteamDir.SuspendLayout();
            grpStartup.SuspendLayout();
            grpSaving.SuspendLayout();
            tabControl.SuspendLayout();
            tabGeneral.SuspendLayout();
            grpStoreLanguage.SuspendLayout();
            grpDepressurizerUpdates.SuspendLayout();
            grpDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScrapePromptDays).BeginInit();
            grpUILanguage.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // grpSteamDir
            // 
            resources.ApplyResources(grpSteamDir, "grpSteamDir");
            grpSteamDir.Controls.Add(cmdSteamPathBrowse);
            grpSteamDir.Controls.Add(txtSteamPath);
            grpSteamDir.Name = "grpSteamDir";
            grpSteamDir.TabStop = false;
            // 
            // cmdSteamPathBrowse
            // 
            resources.ApplyResources(cmdSteamPathBrowse, "cmdSteamPathBrowse");
            cmdSteamPathBrowse.Name = "cmdSteamPathBrowse";
            cmdSteamPathBrowse.UseVisualStyleBackColor = true;
            cmdSteamPathBrowse.Click += cmdSteamPathBrowse_Click;
            // 
            // txtSteamPath
            // 
            resources.ApplyResources(txtSteamPath, "txtSteamPath");
            txtSteamPath.Name = "txtSteamPath";
            // 
            // grpStartup
            // 
            resources.ApplyResources(grpStartup, "grpStartup");
            grpStartup.Controls.Add(radNone);
            grpStartup.Controls.Add(radCreate);
            grpStartup.Controls.Add(radLoad);
            grpStartup.Controls.Add(cmdDefaultProfileBrowse);
            grpStartup.Controls.Add(txtDefaultProfile);
            grpStartup.Name = "grpStartup";
            grpStartup.TabStop = false;
            // 
            // radNone
            // 
            resources.ApplyResources(radNone, "radNone");
            radNone.Name = "radNone";
            radNone.TabStop = true;
            radNone.UseVisualStyleBackColor = true;
            // 
            // radCreate
            // 
            resources.ApplyResources(radCreate, "radCreate");
            radCreate.Name = "radCreate";
            radCreate.TabStop = true;
            radCreate.UseVisualStyleBackColor = true;
            // 
            // radLoad
            // 
            resources.ApplyResources(radLoad, "radLoad");
            radLoad.Name = "radLoad";
            radLoad.TabStop = true;
            radLoad.UseVisualStyleBackColor = true;
            // 
            // cmdDefaultProfileBrowse
            // 
            resources.ApplyResources(cmdDefaultProfileBrowse, "cmdDefaultProfileBrowse");
            cmdDefaultProfileBrowse.Name = "cmdDefaultProfileBrowse";
            cmdDefaultProfileBrowse.UseVisualStyleBackColor = true;
            cmdDefaultProfileBrowse.Click += cmdDefaultProfileBrowse_Click;
            // 
            // txtDefaultProfile
            // 
            resources.ApplyResources(txtDefaultProfile, "txtDefaultProfile");
            txtDefaultProfile.Name = "txtDefaultProfile";
            // 
            // chkRemoveExtraEntries
            // 
            resources.ApplyResources(chkRemoveExtraEntries, "chkRemoveExtraEntries");
            chkRemoveExtraEntries.Name = "chkRemoveExtraEntries";
            chkRemoveExtraEntries.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            resources.ApplyResources(cmdAccept, "cmdAccept");
            cmdAccept.Name = "cmdAccept";
            cmdAccept.UseVisualStyleBackColor = true;
            cmdAccept.Click += cmdAccept_Click;
            // 
            // cmdCancel
            // 
            resources.ApplyResources(cmdCancel, "cmdCancel");
            cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cmdCancel.Name = "cmdCancel";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += cmdCancel_Click;
            // 
            // grpSaving
            // 
            resources.ApplyResources(grpSaving, "grpSaving");
            grpSaving.Controls.Add(chkRemoveExtraEntries);
            grpSaving.Name = "grpSaving";
            grpSaving.TabStop = false;
            // 
            // tabControl
            // 
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.Controls.Add(tabGeneral);
            tabControl.Controls.Add(tabPage1);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            // 
            // tabGeneral
            // 
            tabGeneral.Controls.Add(grpStoreLanguage);
            tabGeneral.Controls.Add(grpDepressurizerUpdates);
            tabGeneral.Controls.Add(grpDatabase);
            tabGeneral.Controls.Add(grpUILanguage);
            tabGeneral.Controls.Add(grpSteamDir);
            tabGeneral.Controls.Add(grpStartup);
            tabGeneral.Controls.Add(grpSaving);
            resources.ApplyResources(tabGeneral, "tabGeneral");
            tabGeneral.Name = "tabGeneral";
            tabGeneral.UseVisualStyleBackColor = true;
            // 
            // grpStoreLanguage
            // 
            resources.ApplyResources(grpStoreLanguage, "grpStoreLanguage");
            grpStoreLanguage.Controls.Add(cmbStoreLanguage);
            grpStoreLanguage.Name = "grpStoreLanguage";
            grpStoreLanguage.TabStop = false;
            // 
            // cmbStoreLanguage
            // 
            cmbStoreLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbStoreLanguage.FormattingEnabled = true;
            resources.ApplyResources(cmbStoreLanguage, "cmbStoreLanguage");
            cmbStoreLanguage.Name = "cmbStoreLanguage";
            // 
            // grpDepressurizerUpdates
            // 
            resources.ApplyResources(grpDepressurizerUpdates, "grpDepressurizerUpdates");
            grpDepressurizerUpdates.Controls.Add(chkCheckForDepressurizerUpdates);
            grpDepressurizerUpdates.Name = "grpDepressurizerUpdates";
            grpDepressurizerUpdates.TabStop = false;
            // 
            // chkCheckForDepressurizerUpdates
            // 
            resources.ApplyResources(chkCheckForDepressurizerUpdates, "chkCheckForDepressurizerUpdates");
            chkCheckForDepressurizerUpdates.Name = "chkCheckForDepressurizerUpdates";
            chkCheckForDepressurizerUpdates.UseVisualStyleBackColor = true;
            // 
            // grpDatabase
            // 
            resources.ApplyResources(grpDatabase, "grpDatabase");
            grpDatabase.Controls.Add(checkBoxReadFromLevelDB);
            grpDatabase.Controls.Add(numScrapePromptDays);
            grpDatabase.Controls.Add(lblScrapePrompt2);
            grpDatabase.Controls.Add(lblScapePrompt1);
            grpDatabase.Controls.Add(helpIncludeImputedTimes);
            grpDatabase.Controls.Add(chkIncludeImputedTimes);
            grpDatabase.Controls.Add(chkUpdateHltbOnStartup);
            grpDatabase.Controls.Add(chkAutosaveDB);
            grpDatabase.Controls.Add(chkUpdateAppInfoOnStartup);
            grpDatabase.Name = "grpDatabase";
            grpDatabase.TabStop = false;
            // 
            // numScrapePromptDays
            // 
            resources.ApplyResources(numScrapePromptDays, "numScrapePromptDays");
            numScrapePromptDays.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numScrapePromptDays.Name = "numScrapePromptDays";
            // 
            // lblScrapePrompt2
            // 
            resources.ApplyResources(lblScrapePrompt2, "lblScrapePrompt2");
            lblScrapePrompt2.Name = "lblScrapePrompt2";
            // 
            // lblScapePrompt1
            // 
            resources.ApplyResources(lblScapePrompt1, "lblScapePrompt1");
            lblScapePrompt1.Name = "lblScapePrompt1";
            // 
            // helpIncludeImputedTimes
            // 
            resources.ApplyResources(helpIncludeImputedTimes, "helpIncludeImputedTimes");
            helpIncludeImputedTimes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            helpIncludeImputedTimes.Name = "helpIncludeImputedTimes";
            // 
            // chkIncludeImputedTimes
            // 
            resources.ApplyResources(chkIncludeImputedTimes, "chkIncludeImputedTimes");
            chkIncludeImputedTimes.Name = "chkIncludeImputedTimes";
            chkIncludeImputedTimes.UseVisualStyleBackColor = true;
            // 
            // chkUpdateHltbOnStartup
            // 
            resources.ApplyResources(chkUpdateHltbOnStartup, "chkUpdateHltbOnStartup");
            chkUpdateHltbOnStartup.Name = "chkUpdateHltbOnStartup";
            chkUpdateHltbOnStartup.UseVisualStyleBackColor = true;
            // 
            // chkAutosaveDB
            // 
            resources.ApplyResources(chkAutosaveDB, "chkAutosaveDB");
            chkAutosaveDB.Name = "chkAutosaveDB";
            chkAutosaveDB.UseVisualStyleBackColor = true;
            // 
            // chkUpdateAppInfoOnStartup
            // 
            resources.ApplyResources(chkUpdateAppInfoOnStartup, "chkUpdateAppInfoOnStartup");
            chkUpdateAppInfoOnStartup.Name = "chkUpdateAppInfoOnStartup";
            chkUpdateAppInfoOnStartup.UseVisualStyleBackColor = true;
            // 
            // grpUILanguage
            // 
            resources.ApplyResources(grpUILanguage, "grpUILanguage");
            grpUILanguage.Controls.Add(cmbUILanguage);
            grpUILanguage.Name = "grpUILanguage";
            grpUILanguage.TabStop = false;
            // 
            // cmbUILanguage
            // 
            cmbUILanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbUILanguage.FormattingEnabled = true;
            resources.ApplyResources(cmbUILanguage, "cmbUILanguage");
            cmbUILanguage.Name = "cmbUILanguage";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(cmdDefaultIgnored);
            tabPage1.Controls.Add(cmdIgnore);
            tabPage1.Controls.Add(txtIgnore);
            tabPage1.Controls.Add(cmdUnignore);
            tabPage1.Controls.Add(lstIgnored);
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Name = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdDefaultIgnored
            // 
            resources.ApplyResources(cmdDefaultIgnored, "cmdDefaultIgnored");
            cmdDefaultIgnored.Name = "cmdDefaultIgnored";
            cmdDefaultIgnored.UseVisualStyleBackColor = true;
            cmdDefaultIgnored.Click += cmdDefaultIgnored_Click;
            // 
            // cmdIgnore
            // 
            resources.ApplyResources(cmdIgnore, "cmdIgnore");
            cmdIgnore.Name = "cmdIgnore";
            cmdIgnore.UseVisualStyleBackColor = true;
            cmdIgnore.Click += cmdIgnore_Click;
            // 
            // txtIgnore
            // 
            resources.ApplyResources(txtIgnore, "txtIgnore");
            txtIgnore.Name = "txtIgnore";
            // 
            // cmdUnignore
            // 
            resources.ApplyResources(cmdUnignore, "cmdUnignore");
            cmdUnignore.Name = "cmdUnignore";
            cmdUnignore.UseVisualStyleBackColor = true;
            cmdUnignore.Click += cmdUnignore_Click;
            // 
            // lstIgnored
            // 
            resources.ApplyResources(lstIgnored, "lstIgnored");
            lstIgnored.FullRowSelect = true;
            lstIgnored.Name = "lstIgnored";
            lstIgnored.Sorting = System.Windows.Forms.SortOrder.Ascending;
            lstIgnored.UseCompatibleStateImageBehavior = false;
            lstIgnored.View = System.Windows.Forms.View.List;
            // 
            // checkBoxReadFromLevelDB
            // 
            resources.ApplyResources(checkBoxReadFromLevelDB, "checkBoxReadFromLevelDB");
            checkBoxReadFromLevelDB.Name = "checkBoxReadFromLevelDB";
            checkBoxReadFromLevelDB.UseVisualStyleBackColor = true;
            // 
            // DlgOptions
            // 
            AcceptButton = cmdAccept;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cmdCancel;
            ControlBox = false;
            Controls.Add(tabControl);
            Controls.Add(cmdCancel);
            Controls.Add(cmdAccept);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "DlgOptions";
            Load += OptionsForm_Load;
            grpSteamDir.ResumeLayout(false);
            grpSteamDir.PerformLayout();
            grpStartup.ResumeLayout(false);
            grpStartup.PerformLayout();
            grpSaving.ResumeLayout(false);
            grpSaving.PerformLayout();
            tabControl.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            grpStoreLanguage.ResumeLayout(false);
            grpDepressurizerUpdates.ResumeLayout(false);
            grpDepressurizerUpdates.PerformLayout();
            grpDatabase.ResumeLayout(false);
            grpDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScrapePromptDays).EndInit();
            grpUILanguage.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSteamDir;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Button cmdSteamPathBrowse;
        private System.Windows.Forms.TextBox txtSteamPath;
        private System.Windows.Forms.GroupBox grpStartup;
        private System.Windows.Forms.Button cmdDefaultProfileBrowse;
        private System.Windows.Forms.TextBox txtDefaultProfile;
        private System.Windows.Forms.CheckBox chkRemoveExtraEntries;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox grpSaving;
        private System.Windows.Forms.RadioButton radNone;
        private System.Windows.Forms.RadioButton radCreate;
        private System.Windows.Forms.RadioButton radLoad;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox grpUILanguage;
        private System.Windows.Forms.ComboBox cmbUILanguage;
        private System.Windows.Forms.GroupBox grpDatabase;
        private System.Windows.Forms.CheckBox chkAutosaveDB;
        private System.Windows.Forms.CheckBox chkUpdateAppInfoOnStartup;
        private System.Windows.Forms.CheckBox chkIncludeImputedTimes;
        private System.Windows.Forms.CheckBox chkUpdateHltbOnStartup;
        private System.Windows.Forms.Label helpIncludeImputedTimes;
        private System.Windows.Forms.GroupBox grpDepressurizerUpdates;
        private System.Windows.Forms.CheckBox chkCheckForDepressurizerUpdates;
        private System.Windows.Forms.Label lblScrapePrompt2;
        private System.Windows.Forms.Label lblScapePrompt1;
        private System.Windows.Forms.NumericUpDown numScrapePromptDays;
        private System.Windows.Forms.GroupBox grpStoreLanguage;
        private System.Windows.Forms.ComboBox cmbStoreLanguage;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView lstIgnored;
        private System.Windows.Forms.Button cmdIgnore;
        private System.Windows.Forms.TextBox txtIgnore;
        private System.Windows.Forms.Button cmdUnignore;
        private System.Windows.Forms.Button cmdDefaultIgnored;
        private System.Windows.Forms.CheckBox checkBoxReadFromLevelDB;
    }
}