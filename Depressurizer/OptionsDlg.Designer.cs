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
    partial class OptionsDlg {
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
            this.chkIgnoreDlc = new System.Windows.Forms.CheckBox();
            this.grpAutocat = new System.Windows.Forms.GroupBox();
            this.chkFullAutocat = new System.Windows.Forms.CheckBox();
            this.grpDatSrc = new System.Windows.Forms.GroupBox();
            this.cmbDatSrc = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.tabLogging = new System.Windows.Forms.TabPage();
            this.cmbLogLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numLogSize = new System.Windows.Forms.NumericUpDown();
            this.numLogBackup = new System.Windows.Forms.NumericUpDown();
            this.grpSteamDir.SuspendLayout();
            this.grpStartup.SuspendLayout();
            this.grpSaving.SuspendLayout();
            this.grpAutocat.SuspendLayout();
            this.grpDatSrc.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabLogging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLogSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLogBackup)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSteamDir
            // 
            this.grpSteamDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSteamDir.Controls.Add(this.cmdSteamPathBrowse);
            this.grpSteamDir.Controls.Add(this.txtSteamPath);
            this.grpSteamDir.Location = new System.Drawing.Point(6, 6);
            this.grpSteamDir.Name = "grpSteamDir";
            this.grpSteamDir.Size = new System.Drawing.Size(524, 54);
            this.grpSteamDir.TabIndex = 0;
            this.grpSteamDir.TabStop = false;
            this.grpSteamDir.Text = "Steam Directory";
            // 
            // cmdSteamPathBrowse
            // 
            this.cmdSteamPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSteamPathBrowse.Location = new System.Drawing.Point(440, 20);
            this.cmdSteamPathBrowse.Name = "cmdSteamPathBrowse";
            this.cmdSteamPathBrowse.Size = new System.Drawing.Size(75, 23);
            this.cmdSteamPathBrowse.TabIndex = 1;
            this.cmdSteamPathBrowse.Text = "Browse...";
            this.cmdSteamPathBrowse.UseVisualStyleBackColor = true;
            this.cmdSteamPathBrowse.Click += new System.EventHandler(this.cmdSteamPathBrowse_Click);
            // 
            // txtSteamPath
            // 
            this.txtSteamPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSteamPath.Location = new System.Drawing.Point(11, 22);
            this.txtSteamPath.Name = "txtSteamPath";
            this.txtSteamPath.Size = new System.Drawing.Size(423, 20);
            this.txtSteamPath.TabIndex = 0;
            // 
            // grpStartup
            // 
            this.grpStartup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStartup.Controls.Add(this.radNone);
            this.grpStartup.Controls.Add(this.radCreate);
            this.grpStartup.Controls.Add(this.radLoad);
            this.grpStartup.Controls.Add(this.cmdDefaultProfileBrowse);
            this.grpStartup.Controls.Add(this.txtDefaultProfile);
            this.grpStartup.Location = new System.Drawing.Point(6, 66);
            this.grpStartup.Name = "grpStartup";
            this.grpStartup.Size = new System.Drawing.Size(524, 90);
            this.grpStartup.TabIndex = 1;
            this.grpStartup.TabStop = false;
            this.grpStartup.Text = "On Startup";
            // 
            // radNone
            // 
            this.radNone.AutoSize = true;
            this.radNone.Location = new System.Drawing.Point(11, 65);
            this.radNone.Name = "radNone";
            this.radNone.Size = new System.Drawing.Size(77, 17);
            this.radNone.TabIndex = 4;
            this.radNone.TabStop = true;
            this.radNone.Text = "Do nothing";
            this.radNone.UseVisualStyleBackColor = true;
            // 
            // radCreate
            // 
            this.radCreate.AutoSize = true;
            this.radCreate.Location = new System.Drawing.Point(11, 42);
            this.radCreate.Name = "radCreate";
            this.radCreate.Size = new System.Drawing.Size(87, 17);
            this.radCreate.TabIndex = 3;
            this.radCreate.TabStop = true;
            this.radCreate.Text = "Create profile";
            this.radCreate.UseVisualStyleBackColor = true;
            // 
            // radLoad
            // 
            this.radLoad.AutoSize = true;
            this.radLoad.Location = new System.Drawing.Point(11, 19);
            this.radLoad.Name = "radLoad";
            this.radLoad.Size = new System.Drawing.Size(84, 17);
            this.radLoad.TabIndex = 0;
            this.radLoad.TabStop = true;
            this.radLoad.Text = "Load Profile:";
            this.radLoad.UseVisualStyleBackColor = true;
            // 
            // cmdDefaultProfileBrowse
            // 
            this.cmdDefaultProfileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDefaultProfileBrowse.Location = new System.Drawing.Point(440, 16);
            this.cmdDefaultProfileBrowse.Name = "cmdDefaultProfileBrowse";
            this.cmdDefaultProfileBrowse.Size = new System.Drawing.Size(75, 23);
            this.cmdDefaultProfileBrowse.TabIndex = 2;
            this.cmdDefaultProfileBrowse.Text = "Browse...";
            this.cmdDefaultProfileBrowse.UseVisualStyleBackColor = true;
            this.cmdDefaultProfileBrowse.Click += new System.EventHandler(this.cmdDefaultProfileBrowse_Click);
            // 
            // txtDefaultProfile
            // 
            this.txtDefaultProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDefaultProfile.Location = new System.Drawing.Point(101, 18);
            this.txtDefaultProfile.Name = "txtDefaultProfile";
            this.txtDefaultProfile.Size = new System.Drawing.Size(333, 20);
            this.txtDefaultProfile.TabIndex = 1;
            // 
            // chkRemoveExtraEntries
            // 
            this.chkRemoveExtraEntries.AutoSize = true;
            this.chkRemoveExtraEntries.Location = new System.Drawing.Point(6, 19);
            this.chkRemoveExtraEntries.Name = "chkRemoveExtraEntries";
            this.chkRemoveExtraEntries.Size = new System.Drawing.Size(321, 17);
            this.chkRemoveExtraEntries.TabIndex = 0;
            this.chkRemoveExtraEntries.Text = "Remove entries for deleted or unknown games when exporting";
            this.chkRemoveExtraEntries.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Location = new System.Drawing.Point(481, 384);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(75, 23);
            this.cmdAccept.TabIndex = 2;
            this.cmdAccept.Text = "OK";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(400, 384);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpSaving
            // 
            this.grpSaving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSaving.Controls.Add(this.chkIgnoreDlc);
            this.grpSaving.Controls.Add(this.chkRemoveExtraEntries);
            this.grpSaving.Location = new System.Drawing.Point(6, 268);
            this.grpSaving.Name = "grpSaving";
            this.grpSaving.Size = new System.Drawing.Size(524, 66);
            this.grpSaving.TabIndex = 4;
            this.grpSaving.TabStop = false;
            this.grpSaving.Text = "Manual Operations";
            // 
            // chkIgnoreDlc
            // 
            this.chkIgnoreDlc.AutoSize = true;
            this.chkIgnoreDlc.Location = new System.Drawing.Point(6, 42);
            this.chkIgnoreDlc.Name = "chkIgnoreDlc";
            this.chkIgnoreDlc.Size = new System.Drawing.Size(196, 17);
            this.chkIgnoreDlc.TabIndex = 1;
            this.chkIgnoreDlc.Text = "Ignore DLC on import and download";
            this.chkIgnoreDlc.UseVisualStyleBackColor = true;
            // 
            // grpAutocat
            // 
            this.grpAutocat.Controls.Add(this.chkFullAutocat);
            this.grpAutocat.Location = new System.Drawing.Point(6, 162);
            this.grpAutocat.Name = "grpAutocat";
            this.grpAutocat.Size = new System.Drawing.Size(650, 42);
            this.grpAutocat.TabIndex = 2;
            this.grpAutocat.TabStop = false;
            this.grpAutocat.Text = "Auto-Categorization";
            // 
            // chkFullAutocat
            // 
            this.chkFullAutocat.AutoSize = true;
            this.chkFullAutocat.Location = new System.Drawing.Point(11, 19);
            this.chkFullAutocat.Name = "chkFullAutocat";
            this.chkFullAutocat.Size = new System.Drawing.Size(255, 17);
            this.chkFullAutocat.TabIndex = 0;
            this.chkFullAutocat.Text = "Use full genre lists instead of only the main genre";
            this.chkFullAutocat.UseVisualStyleBackColor = true;
            // 
            // grpDatSrc
            // 
            this.grpDatSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatSrc.Controls.Add(this.cmbDatSrc);
            this.grpDatSrc.Location = new System.Drawing.Point(6, 210);
            this.grpDatSrc.Name = "grpDatSrc";
            this.grpDatSrc.Size = new System.Drawing.Size(524, 52);
            this.grpDatSrc.TabIndex = 3;
            this.grpDatSrc.TabStop = false;
            this.grpDatSrc.Text = "Profile Data Source";
            // 
            // cmbDatSrc
            // 
            this.cmbDatSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDatSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatSrc.FormattingEnabled = true;
            this.cmbDatSrc.Items.AddRange(new object[] {
            "Prefer XML",
            "Use XML Only",
            "Use HTML Only"});
            this.cmbDatSrc.Location = new System.Drawing.Point(11, 19);
            this.cmbDatSrc.Name = "cmbDatSrc";
            this.cmbDatSrc.Size = new System.Drawing.Size(507, 21);
            this.cmbDatSrc.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabLogging);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(544, 366);
            this.tabControl.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.grpSteamDir);
            this.tabGeneral.Controls.Add(this.grpDatSrc);
            this.tabGeneral.Controls.Add(this.grpStartup);
            this.tabGeneral.Controls.Add(this.grpAutocat);
            this.tabGeneral.Controls.Add(this.grpSaving);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(536, 340);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // tabLogging
            // 
            this.tabLogging.Controls.Add(this.numLogBackup);
            this.tabLogging.Controls.Add(this.numLogSize);
            this.tabLogging.Controls.Add(this.label3);
            this.tabLogging.Controls.Add(this.label2);
            this.tabLogging.Controls.Add(this.label1);
            this.tabLogging.Controls.Add(this.cmbLogLevel);
            this.tabLogging.Location = new System.Drawing.Point(4, 22);
            this.tabLogging.Name = "tabLogging";
            this.tabLogging.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogging.Size = new System.Drawing.Size(536, 340);
            this.tabLogging.TabIndex = 1;
            this.tabLogging.Text = "Logging";
            this.tabLogging.UseVisualStyleBackColor = true;
            // 
            // cmbLogLevel
            // 
            this.cmbLogLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogLevel.FormattingEnabled = true;
            this.cmbLogLevel.Location = new System.Drawing.Point(130, 6);
            this.cmbLogLevel.Name = "cmbLogLevel";
            this.cmbLogLevel.Size = new System.Drawing.Size(120, 21);
            this.cmbLogLevel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Logging Level: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Max file size (bytes):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Backups to keep:";
            // 
            // numLogSize
            // 
            this.numLogSize.Increment = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numLogSize.Location = new System.Drawing.Point(130, 33);
            this.numLogSize.Maximum = new decimal(new int[] {
            20000000,
            0,
            0,
            0});
            this.numLogSize.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numLogSize.Name = "numLogSize";
            this.numLogSize.Size = new System.Drawing.Size(120, 20);
            this.numLogSize.TabIndex = 5;
            this.numLogSize.ThousandsSeparator = true;
            this.numLogSize.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // numLogBackup
            // 
            this.numLogBackup.Location = new System.Drawing.Point(130, 59);
            this.numLogBackup.Name = "numLogBackup";
            this.numLogBackup.Size = new System.Drawing.Size(120, 20);
            this.numLogBackup.TabIndex = 6;
            // 
            // OptionsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 417);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OptionsDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.grpSteamDir.ResumeLayout(false);
            this.grpSteamDir.PerformLayout();
            this.grpStartup.ResumeLayout(false);
            this.grpStartup.PerformLayout();
            this.grpSaving.ResumeLayout(false);
            this.grpSaving.PerformLayout();
            this.grpAutocat.ResumeLayout(false);
            this.grpAutocat.PerformLayout();
            this.grpDatSrc.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabLogging.ResumeLayout(false);
            this.tabLogging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLogSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLogBackup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSteamDir;
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
        private System.Windows.Forms.CheckBox chkIgnoreDlc;
        private System.Windows.Forms.GroupBox grpAutocat;
        private System.Windows.Forms.CheckBox chkFullAutocat;
        private System.Windows.Forms.GroupBox grpDatSrc;
        private System.Windows.Forms.ComboBox cmbDatSrc;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabLogging;
        private System.Windows.Forms.NumericUpDown numLogBackup;
        private System.Windows.Forms.NumericUpDown numLogSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLogLevel;
    }
}