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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgOptions));
            this.grpSteamDir = new System.Windows.Forms.GroupBox();
            this.cmdSteamPathBrowse = new System.Windows.Forms.Button();
            this.txtSteamPath = new System.Windows.Forms.TextBox();
            this.grpStartup = new System.Windows.Forms.GroupBox();
            this.radNone = new System.Windows.Forms.RadioButton();
            this.radCreate = new System.Windows.Forms.RadioButton();
            this.radLoad = new System.Windows.Forms.RadioButton();
            this.cmdDefaultProfileBrowse = new System.Windows.Forms.Button();
            this.txtDefaultProfile = new System.Windows.Forms.TextBox();
            this.chkRemoveExtraEntries = new System.Windows.Forms.CheckBox();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.grpSaving = new System.Windows.Forms.GroupBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.grpStoreLanguage = new System.Windows.Forms.GroupBox();
            this.cmbStoreLanguage = new System.Windows.Forms.ComboBox();
            this.grpDepressurizerUpdates = new System.Windows.Forms.GroupBox();
            this.chkCheckForDepressurizerUpdates = new System.Windows.Forms.CheckBox();
            this.grpDatabase = new System.Windows.Forms.GroupBox();
            this.numScrapePromptDays = new System.Windows.Forms.NumericUpDown();
            this.lblScrapePrompt2 = new System.Windows.Forms.Label();
            this.lblScapePrompt1 = new System.Windows.Forms.Label();
            this.helpIncludeImputedTimes = new System.Windows.Forms.Label();
            this.chkIncludeImputedTimes = new System.Windows.Forms.CheckBox();
            this.chkUpdateHltbOnStartup = new System.Windows.Forms.CheckBox();
            this.chkAutosaveDB = new System.Windows.Forms.CheckBox();
            this.chkUpdateAppInfoOnStartup = new System.Windows.Forms.CheckBox();
            this.grpUILanguage = new System.Windows.Forms.GroupBox();
            this.cmbUILanguage = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxPremiumApiKey = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxPremiumServer = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdDefaultIgnored = new System.Windows.Forms.Button();
            this.cmdIgnore = new System.Windows.Forms.Button();
            this.txtIgnore = new System.Windows.Forms.TextBox();
            this.cmdUnignore = new System.Windows.Forms.Button();
            this.lstIgnored = new System.Windows.Forms.ListView();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBoxReadFromLevelDB = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grpSteamDir.SuspendLayout();
            this.grpStartup.SuspendLayout();
            this.grpSaving.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.grpStoreLanguage.SuspendLayout();
            this.grpDepressurizerUpdates.SuspendLayout();
            this.grpDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScrapePromptDays)).BeginInit();
            this.grpUILanguage.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSteamDir
            // 
            resources.ApplyResources(this.grpSteamDir, "grpSteamDir");
            this.grpSteamDir.Controls.Add(this.cmdSteamPathBrowse);
            this.grpSteamDir.Controls.Add(this.txtSteamPath);
            this.grpSteamDir.Name = "grpSteamDir";
            this.grpSteamDir.TabStop = false;
            // 
            // cmdSteamPathBrowse
            // 
            resources.ApplyResources(this.cmdSteamPathBrowse, "cmdSteamPathBrowse");
            this.cmdSteamPathBrowse.Name = "cmdSteamPathBrowse";
            this.cmdSteamPathBrowse.UseVisualStyleBackColor = true;
            this.cmdSteamPathBrowse.Click += new System.EventHandler(this.cmdSteamPathBrowse_Click);
            // 
            // txtSteamPath
            // 
            resources.ApplyResources(this.txtSteamPath, "txtSteamPath");
            this.txtSteamPath.Name = "txtSteamPath";
            // 
            // grpStartup
            // 
            resources.ApplyResources(this.grpStartup, "grpStartup");
            this.grpStartup.Controls.Add(this.radNone);
            this.grpStartup.Controls.Add(this.radCreate);
            this.grpStartup.Controls.Add(this.radLoad);
            this.grpStartup.Controls.Add(this.cmdDefaultProfileBrowse);
            this.grpStartup.Controls.Add(this.txtDefaultProfile);
            this.grpStartup.Name = "grpStartup";
            this.grpStartup.TabStop = false;
            // 
            // radNone
            // 
            resources.ApplyResources(this.radNone, "radNone");
            this.radNone.Name = "radNone";
            this.radNone.TabStop = true;
            this.radNone.UseVisualStyleBackColor = true;
            // 
            // radCreate
            // 
            resources.ApplyResources(this.radCreate, "radCreate");
            this.radCreate.Name = "radCreate";
            this.radCreate.TabStop = true;
            this.radCreate.UseVisualStyleBackColor = true;
            // 
            // radLoad
            // 
            resources.ApplyResources(this.radLoad, "radLoad");
            this.radLoad.Name = "radLoad";
            this.radLoad.TabStop = true;
            this.radLoad.UseVisualStyleBackColor = true;
            // 
            // cmdDefaultProfileBrowse
            // 
            resources.ApplyResources(this.cmdDefaultProfileBrowse, "cmdDefaultProfileBrowse");
            this.cmdDefaultProfileBrowse.Name = "cmdDefaultProfileBrowse";
            this.cmdDefaultProfileBrowse.UseVisualStyleBackColor = true;
            this.cmdDefaultProfileBrowse.Click += new System.EventHandler(this.cmdDefaultProfileBrowse_Click);
            // 
            // txtDefaultProfile
            // 
            resources.ApplyResources(this.txtDefaultProfile, "txtDefaultProfile");
            this.txtDefaultProfile.Name = "txtDefaultProfile";
            // 
            // chkRemoveExtraEntries
            // 
            resources.ApplyResources(this.chkRemoveExtraEntries, "chkRemoveExtraEntries");
            this.chkRemoveExtraEntries.Name = "chkRemoveExtraEntries";
            this.chkRemoveExtraEntries.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            resources.ApplyResources(this.cmdAccept, "cmdAccept");
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpSaving
            // 
            resources.ApplyResources(this.grpSaving, "grpSaving");
            this.grpSaving.Controls.Add(this.chkRemoveExtraEntries);
            this.grpSaving.Name = "grpSaving";
            this.grpSaving.TabStop = false;
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.grpStoreLanguage);
            this.tabGeneral.Controls.Add(this.grpDepressurizerUpdates);
            this.tabGeneral.Controls.Add(this.grpDatabase);
            this.tabGeneral.Controls.Add(this.grpUILanguage);
            this.tabGeneral.Controls.Add(this.grpSteamDir);
            this.tabGeneral.Controls.Add(this.grpStartup);
            this.tabGeneral.Controls.Add(this.grpSaving);
            resources.ApplyResources(this.tabGeneral, "tabGeneral");
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // grpStoreLanguage
            // 
            resources.ApplyResources(this.grpStoreLanguage, "grpStoreLanguage");
            this.grpStoreLanguage.Controls.Add(this.cmbStoreLanguage);
            this.grpStoreLanguage.Name = "grpStoreLanguage";
            this.grpStoreLanguage.TabStop = false;
            // 
            // cmbStoreLanguage
            // 
            this.cmbStoreLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStoreLanguage.FormattingEnabled = true;
            resources.ApplyResources(this.cmbStoreLanguage, "cmbStoreLanguage");
            this.cmbStoreLanguage.Name = "cmbStoreLanguage";
            // 
            // grpDepressurizerUpdates
            // 
            resources.ApplyResources(this.grpDepressurizerUpdates, "grpDepressurizerUpdates");
            this.grpDepressurizerUpdates.Controls.Add(this.chkCheckForDepressurizerUpdates);
            this.grpDepressurizerUpdates.Name = "grpDepressurizerUpdates";
            this.grpDepressurizerUpdates.TabStop = false;
            // 
            // chkCheckForDepressurizerUpdates
            // 
            resources.ApplyResources(this.chkCheckForDepressurizerUpdates, "chkCheckForDepressurizerUpdates");
            this.chkCheckForDepressurizerUpdates.Name = "chkCheckForDepressurizerUpdates";
            this.chkCheckForDepressurizerUpdates.UseVisualStyleBackColor = true;
            // 
            // grpDatabase
            // 
            resources.ApplyResources(this.grpDatabase, "grpDatabase");
            this.grpDatabase.Controls.Add(this.numScrapePromptDays);
            this.grpDatabase.Controls.Add(this.lblScrapePrompt2);
            this.grpDatabase.Controls.Add(this.lblScapePrompt1);
            this.grpDatabase.Controls.Add(this.helpIncludeImputedTimes);
            this.grpDatabase.Controls.Add(this.chkIncludeImputedTimes);
            this.grpDatabase.Controls.Add(this.chkUpdateHltbOnStartup);
            this.grpDatabase.Controls.Add(this.chkAutosaveDB);
            this.grpDatabase.Controls.Add(this.chkUpdateAppInfoOnStartup);
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.TabStop = false;
            // 
            // numScrapePromptDays
            // 
            resources.ApplyResources(this.numScrapePromptDays, "numScrapePromptDays");
            this.numScrapePromptDays.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numScrapePromptDays.Name = "numScrapePromptDays";
            // 
            // lblScrapePrompt2
            // 
            resources.ApplyResources(this.lblScrapePrompt2, "lblScrapePrompt2");
            this.lblScrapePrompt2.Name = "lblScrapePrompt2";
            // 
            // lblScapePrompt1
            // 
            resources.ApplyResources(this.lblScapePrompt1, "lblScapePrompt1");
            this.lblScapePrompt1.Name = "lblScapePrompt1";
            // 
            // helpIncludeImputedTimes
            // 
            resources.ApplyResources(this.helpIncludeImputedTimes, "helpIncludeImputedTimes");
            this.helpIncludeImputedTimes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpIncludeImputedTimes.Name = "helpIncludeImputedTimes";
            // 
            // chkIncludeImputedTimes
            // 
            resources.ApplyResources(this.chkIncludeImputedTimes, "chkIncludeImputedTimes");
            this.chkIncludeImputedTimes.Name = "chkIncludeImputedTimes";
            this.chkIncludeImputedTimes.UseVisualStyleBackColor = true;
            // 
            // chkUpdateHltbOnStartup
            // 
            resources.ApplyResources(this.chkUpdateHltbOnStartup, "chkUpdateHltbOnStartup");
            this.chkUpdateHltbOnStartup.Name = "chkUpdateHltbOnStartup";
            this.chkUpdateHltbOnStartup.UseVisualStyleBackColor = true;
            // 
            // chkAutosaveDB
            // 
            resources.ApplyResources(this.chkAutosaveDB, "chkAutosaveDB");
            this.chkAutosaveDB.Name = "chkAutosaveDB";
            this.chkAutosaveDB.UseVisualStyleBackColor = true;
            // 
            // chkUpdateAppInfoOnStartup
            // 
            resources.ApplyResources(this.chkUpdateAppInfoOnStartup, "chkUpdateAppInfoOnStartup");
            this.chkUpdateAppInfoOnStartup.Name = "chkUpdateAppInfoOnStartup";
            this.chkUpdateAppInfoOnStartup.UseVisualStyleBackColor = true;
            // 
            // grpUILanguage
            // 
            resources.ApplyResources(this.grpUILanguage, "grpUILanguage");
            this.grpUILanguage.Controls.Add(this.cmbUILanguage);
            this.grpUILanguage.Name = "grpUILanguage";
            this.grpUILanguage.TabStop = false;
            // 
            // cmbUILanguage
            // 
            this.cmbUILanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUILanguage.FormattingEnabled = true;
            resources.ApplyResources(this.cmbUILanguage, "cmbUILanguage");
            this.cmbUILanguage.Name = "cmbUILanguage";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.textBoxPremiumApiKey);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // textBoxPremiumApiKey
            // 
            resources.ApplyResources(this.textBoxPremiumApiKey, "textBoxPremiumApiKey");
            this.textBoxPremiumApiKey.Name = "textBoxPremiumApiKey";
            this.textBoxPremiumApiKey.ReadOnly = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.textBoxPremiumServer);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // textBoxPremiumServer
            // 
            resources.ApplyResources(this.textBoxPremiumServer, "textBoxPremiumServer");
            this.textBoxPremiumServer.Name = "textBoxPremiumServer";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdDefaultIgnored);
            this.tabPage1.Controls.Add(this.cmdIgnore);
            this.tabPage1.Controls.Add(this.txtIgnore);
            this.tabPage1.Controls.Add(this.cmdUnignore);
            this.tabPage1.Controls.Add(this.lstIgnored);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdDefaultIgnored
            // 
            resources.ApplyResources(this.cmdDefaultIgnored, "cmdDefaultIgnored");
            this.cmdDefaultIgnored.Name = "cmdDefaultIgnored";
            this.cmdDefaultIgnored.UseVisualStyleBackColor = true;
            this.cmdDefaultIgnored.Click += new System.EventHandler(this.cmdDefaultIgnored_Click);
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
            this.lstIgnored.HideSelection = false;
            this.lstIgnored.Name = "lstIgnored";
            this.lstIgnored.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstIgnored.UseCompatibleStateImageBehavior = false;
            this.lstIgnored.View = System.Windows.Forms.View.List;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBoxReadFromLevelDB
            // 
            resources.ApplyResources(this.checkBoxReadFromLevelDB, "checkBoxReadFromLevelDB");
            this.checkBoxReadFromLevelDB.Name = "checkBoxReadFromLevelDB";
            this.checkBoxReadFromLevelDB.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.checkBoxReadFromLevelDB);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // DlgOptions
            // 
            this.AcceptButton = this.cmdAccept;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgOptions";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.grpSteamDir.ResumeLayout(false);
            this.grpSteamDir.PerformLayout();
            this.grpStartup.ResumeLayout(false);
            this.grpStartup.PerformLayout();
            this.grpSaving.ResumeLayout(false);
            this.grpSaving.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.grpStoreLanguage.ResumeLayout(false);
            this.grpDepressurizerUpdates.ResumeLayout(false);
            this.grpDepressurizerUpdates.PerformLayout();
            this.grpDatabase.ResumeLayout(false);
            this.grpDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScrapePromptDays)).EndInit();
            this.grpUILanguage.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxPremiumServer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxPremiumApiKey;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxReadFromLevelDB;
    }
}