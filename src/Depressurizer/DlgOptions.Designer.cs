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
            this.grpDatSrc = new System.Windows.Forms.GroupBox();
            this.cmbDatSrc = new System.Windows.Forms.ComboBox();
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
            this.tabLogging = new System.Windows.Forms.TabPage();
            this.numLogBackup = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLogLevel = new System.Windows.Forms.ComboBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpSteamDir.SuspendLayout();
            this.grpStartup.SuspendLayout();
            this.grpSaving.SuspendLayout();
            this.grpDatSrc.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.grpStoreLanguage.SuspendLayout();
            this.grpDepressurizerUpdates.SuspendLayout();
            this.grpDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScrapePromptDays)).BeginInit();
            this.grpUILanguage.SuspendLayout();
            this.tabLogging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLogBackup)).BeginInit();
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
            // grpDatSrc
            // 
            resources.ApplyResources(this.grpDatSrc, "grpDatSrc");
            this.grpDatSrc.Controls.Add(this.cmbDatSrc);
            this.grpDatSrc.Name = "grpDatSrc";
            this.grpDatSrc.TabStop = false;
            // 
            // cmbDatSrc
            // 
            resources.ApplyResources(this.cmbDatSrc, "cmbDatSrc");
            this.cmbDatSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatSrc.FormattingEnabled = true;
            this.cmbDatSrc.Items.AddRange(new object[] {
            resources.GetString("cmbDatSrc.Items"),
            resources.GetString("cmbDatSrc.Items1"),
            resources.GetString("cmbDatSrc.Items2")});
            this.cmbDatSrc.Name = "cmbDatSrc";
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabLogging);
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
            this.tabGeneral.Controls.Add(this.grpDatSrc);
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
            // tabLogging
            // 
            this.tabLogging.Controls.Add(this.numLogBackup);
            this.tabLogging.Controls.Add(this.label3);
            this.tabLogging.Controls.Add(this.label1);
            this.tabLogging.Controls.Add(this.cmbLogLevel);
            resources.ApplyResources(this.tabLogging, "tabLogging");
            this.tabLogging.Name = "tabLogging";
            this.tabLogging.UseVisualStyleBackColor = true;
            // 
            // numLogBackup
            // 
            resources.ApplyResources(this.numLogBackup, "numLogBackup");
            this.numLogBackup.Name = "numLogBackup";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbLogLevel
            // 
            resources.ApplyResources(this.cmbLogLevel, "cmbLogLevel");
            this.cmbLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogLevel.FormattingEnabled = true;
            this.cmbLogLevel.Name = "cmbLogLevel";
            // 
            // DlgOptions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgOptions";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.grpSteamDir.ResumeLayout(false);
            this.grpSteamDir.PerformLayout();
            this.grpStartup.ResumeLayout(false);
            this.grpStartup.PerformLayout();
            this.grpSaving.ResumeLayout(false);
            this.grpSaving.PerformLayout();
            this.grpDatSrc.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.grpStoreLanguage.ResumeLayout(false);
            this.grpDepressurizerUpdates.ResumeLayout(false);
            this.grpDepressurizerUpdates.PerformLayout();
            this.grpDatabase.ResumeLayout(false);
            this.grpDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScrapePromptDays)).EndInit();
            this.grpUILanguage.ResumeLayout(false);
            this.tabLogging.ResumeLayout(false);
            this.tabLogging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLogBackup)).EndInit();
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
        private System.Windows.Forms.GroupBox grpDatSrc;
        private System.Windows.Forms.ComboBox cmbDatSrc;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabLogging;
        private System.Windows.Forms.NumericUpDown numLogBackup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLogLevel;
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
    }
}