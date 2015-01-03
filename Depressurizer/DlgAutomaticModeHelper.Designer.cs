namespace Depressurizer {
    partial class DlgAutomaticModeHelper {
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
            this.chkTolerant = new System.Windows.Forms.CheckBox();
            this.chkUpdateLib = new System.Windows.Forms.CheckBox();
            this.chkImportCats = new System.Windows.Forms.CheckBox();
            this.lblLaunch = new System.Windows.Forms.Label();
            this.lblOutputMode = new System.Windows.Forms.Label();
            this.cmbLaunch = new System.Windows.Forms.ComboBox();
            this.cmbOutputMode = new System.Windows.Forms.ComboBox();
            this.lstAutocats = new System.Windows.Forms.ListView();
            this.lblAutocats = new System.Windows.Forms.Label();
            this.chkAllAutocats = new System.Windows.Forms.CheckBox();
            this.lblSteamCheck = new System.Windows.Forms.Label();
            this.cmbSteamCheck = new System.Windows.Forms.ComboBox();
            this.chkUpdateAppInfo = new System.Windows.Forms.CheckBox();
            this.chkUpdateWeb = new System.Windows.Forms.CheckBox();
            this.chkSaveDB = new System.Windows.Forms.CheckBox();
            this.chkSaveProfile = new System.Windows.Forms.CheckBox();
            this.chkExport = new System.Windows.Forms.CheckBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.cmdShortcut = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.hlpSteamCheck = new System.Windows.Forms.Label();
            this.hlpUpdateLib = new System.Windows.Forms.Label();
            this.hlpUpdateAppInfo = new System.Windows.Forms.Label();
            this.hlpSaveDB = new System.Windows.Forms.Label();
            this.hlpExport = new System.Windows.Forms.Label();
            this.hlpImportCats = new System.Windows.Forms.Label();
            this.hlpUpdateWeb = new System.Windows.Forms.Label();
            this.hlpSaveProfile = new System.Windows.Forms.Label();
            this.hlpLaunch = new System.Windows.Forms.Label();
            this.hlpTolerate = new System.Windows.Forms.Label();
            this.hlpOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkTolerant
            // 
            this.chkTolerant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTolerant.AutoSize = true;
            this.chkTolerant.Location = new System.Drawing.Point(171, 164);
            this.chkTolerant.Name = "chkTolerant";
            this.chkTolerant.Size = new System.Drawing.Size(122, 17);
            this.chkTolerant.TabIndex = 23;
            this.chkTolerant.Text = "Tolerate minor errors";
            this.chkTolerant.UseVisualStyleBackColor = true;
            // 
            // chkUpdateLib
            // 
            this.chkUpdateLib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUpdateLib.AutoSize = true;
            this.chkUpdateLib.Location = new System.Drawing.Point(171, 39);
            this.chkUpdateLib.Name = "chkUpdateLib";
            this.chkUpdateLib.Size = new System.Drawing.Size(91, 17);
            this.chkUpdateLib.TabIndex = 6;
            this.chkUpdateLib.Text = "Update library";
            this.chkUpdateLib.UseVisualStyleBackColor = true;
            // 
            // chkImportCats
            // 
            this.chkImportCats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkImportCats.AutoSize = true;
            this.chkImportCats.Location = new System.Drawing.Point(355, 39);
            this.chkImportCats.Name = "chkImportCats";
            this.chkImportCats.Size = new System.Drawing.Size(140, 17);
            this.chkImportCats.TabIndex = 8;
            this.chkImportCats.Text = "Import Steam categories";
            this.chkImportCats.UseVisualStyleBackColor = true;
            // 
            // lblLaunch
            // 
            this.lblLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLaunch.AutoSize = true;
            this.lblLaunch.Location = new System.Drawing.Point(168, 134);
            this.lblLaunch.Name = "lblLaunch";
            this.lblLaunch.Size = new System.Drawing.Size(79, 13);
            this.lblLaunch.TabIndex = 20;
            this.lblLaunch.Text = "Launch Steam:";
            // 
            // lblOutputMode
            // 
            this.lblOutputMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutputMode.AutoSize = true;
            this.lblOutputMode.Location = new System.Drawing.Point(168, 190);
            this.lblOutputMode.Name = "lblOutputMode";
            this.lblOutputMode.Size = new System.Drawing.Size(71, 13);
            this.lblOutputMode.TabIndex = 25;
            this.lblOutputMode.Text = "Output mode:";
            // 
            // cmbLaunch
            // 
            this.cmbLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLaunch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLaunch.FormattingEnabled = true;
            this.cmbLaunch.Items.AddRange(new object[] {
            "No",
            "In normal mode",
            "In Big Picture mode"});
            this.cmbLaunch.Location = new System.Drawing.Point(258, 131);
            this.cmbLaunch.Name = "cmbLaunch";
            this.cmbLaunch.Size = new System.Drawing.Size(237, 21);
            this.cmbLaunch.TabIndex = 21;
            // 
            // cmbOutputMode
            // 
            this.cmbOutputMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOutputMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputMode.FormattingEnabled = true;
            this.cmbOutputMode.Items.AddRange(new object[] {
            "Normal",
            "Quiet",
            "Silent"});
            this.cmbOutputMode.Location = new System.Drawing.Point(247, 187);
            this.cmbOutputMode.Name = "cmbOutputMode";
            this.cmbOutputMode.Size = new System.Drawing.Size(248, 21);
            this.cmbOutputMode.TabIndex = 26;
            // 
            // lstAutocats
            // 
            this.lstAutocats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAutocats.CheckBoxes = true;
            this.lstAutocats.Location = new System.Drawing.Point(15, 25);
            this.lstAutocats.Name = "lstAutocats";
            this.lstAutocats.Size = new System.Drawing.Size(147, 159);
            this.lstAutocats.TabIndex = 1;
            this.lstAutocats.UseCompatibleStateImageBehavior = false;
            // 
            // lblAutocats
            // 
            this.lblAutocats.AutoSize = true;
            this.lblAutocats.Location = new System.Drawing.Point(12, 9);
            this.lblAutocats.Name = "lblAutocats";
            this.lblAutocats.Size = new System.Drawing.Size(82, 13);
            this.lblAutocats.TabIndex = 0;
            this.lblAutocats.Text = "Autocats to run:";
            // 
            // chkAllAutocats
            // 
            this.chkAllAutocats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAllAutocats.AutoSize = true;
            this.chkAllAutocats.Location = new System.Drawing.Point(15, 190);
            this.chkAllAutocats.Name = "chkAllAutocats";
            this.chkAllAutocats.Size = new System.Drawing.Size(105, 17);
            this.chkAllAutocats.TabIndex = 2;
            this.chkAllAutocats.Text = "Run All Autocats";
            this.chkAllAutocats.UseVisualStyleBackColor = true;
            // 
            // lblSteamCheck
            // 
            this.lblSteamCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSteamCheck.AutoSize = true;
            this.lblSteamCheck.Location = new System.Drawing.Point(168, 15);
            this.lblSteamCheck.Name = "lblSteamCheck";
            this.lblSteamCheck.Size = new System.Drawing.Size(73, 13);
            this.lblSteamCheck.TabIndex = 3;
            this.lblSteamCheck.Text = "Steam check:";
            // 
            // cmbSteamCheck
            // 
            this.cmbSteamCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSteamCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSteamCheck.FormattingEnabled = true;
            this.cmbSteamCheck.Items.AddRange(new object[] {
            "Close steam if it is running",
            "Abort if steam is running",
            "Skip check"});
            this.cmbSteamCheck.Location = new System.Drawing.Point(247, 12);
            this.cmbSteamCheck.Name = "cmbSteamCheck";
            this.cmbSteamCheck.Size = new System.Drawing.Size(248, 21);
            this.cmbSteamCheck.TabIndex = 4;
            // 
            // chkUpdateAppInfo
            // 
            this.chkUpdateAppInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUpdateAppInfo.AutoSize = true;
            this.chkUpdateAppInfo.Location = new System.Drawing.Point(171, 62);
            this.chkUpdateAppInfo.Name = "chkUpdateAppInfo";
            this.chkUpdateAppInfo.Size = new System.Drawing.Size(148, 17);
            this.chkUpdateAppInfo.TabIndex = 10;
            this.chkUpdateAppInfo.Text = "Update DB from local files";
            this.chkUpdateAppInfo.UseVisualStyleBackColor = true;
            // 
            // chkUpdateWeb
            // 
            this.chkUpdateWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUpdateWeb.AutoSize = true;
            this.chkUpdateWeb.Location = new System.Drawing.Point(355, 62);
            this.chkUpdateWeb.Name = "chkUpdateWeb";
            this.chkUpdateWeb.Size = new System.Drawing.Size(125, 17);
            this.chkUpdateWeb.TabIndex = 12;
            this.chkUpdateWeb.Text = "Update DB from web";
            this.chkUpdateWeb.UseVisualStyleBackColor = true;
            // 
            // chkSaveDB
            // 
            this.chkSaveDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSaveDB.AutoSize = true;
            this.chkSaveDB.Location = new System.Drawing.Point(171, 85);
            this.chkSaveDB.Name = "chkSaveDB";
            this.chkSaveDB.Size = new System.Drawing.Size(69, 17);
            this.chkSaveDB.TabIndex = 14;
            this.chkSaveDB.Text = "Save DB";
            this.chkSaveDB.UseVisualStyleBackColor = true;
            // 
            // chkSaveProfile
            // 
            this.chkSaveProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSaveProfile.AutoSize = true;
            this.chkSaveProfile.Location = new System.Drawing.Point(355, 85);
            this.chkSaveProfile.Name = "chkSaveProfile";
            this.chkSaveProfile.Size = new System.Drawing.Size(83, 17);
            this.chkSaveProfile.TabIndex = 16;
            this.chkSaveProfile.Text = "Save Profile";
            this.chkSaveProfile.UseVisualStyleBackColor = true;
            // 
            // chkExport
            // 
            this.chkExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkExport.AutoSize = true;
            this.chkExport.Location = new System.Drawing.Point(171, 108);
            this.chkExport.Name = "chkExport";
            this.chkExport.Size = new System.Drawing.Size(109, 17);
            this.chkExport.TabIndex = 18;
            this.chkExport.Text = "Export Categories";
            this.chkExport.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(12, 249);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(511, 50);
            this.txtResult.TabIndex = 29;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(9, 233);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(40, 13);
            this.lblResult.TabIndex = 28;
            this.lblResult.Text = "Result:";
            // 
            // cmdShortcut
            // 
            this.cmdShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdShortcut.Location = new System.Drawing.Point(12, 305);
            this.cmdShortcut.Name = "cmdShortcut";
            this.cmdShortcut.Size = new System.Drawing.Size(108, 23);
            this.cmdShortcut.TabIndex = 30;
            this.cmdShortcut.Text = "Create Shortcut";
            this.cmdShortcut.UseVisualStyleBackColor = true;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Location = new System.Drawing.Point(448, 305);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 31;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // hlpSteamCheck
            // 
            this.hlpSteamCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpSteamCheck.AutoSize = true;
            this.hlpSteamCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpSteamCheck.Location = new System.Drawing.Point(501, 15);
            this.hlpSteamCheck.Name = "hlpSteamCheck";
            this.hlpSteamCheck.Size = new System.Drawing.Size(15, 15);
            this.hlpSteamCheck.TabIndex = 5;
            this.hlpSteamCheck.Text = "?";
            // 
            // hlpUpdateLib
            // 
            this.hlpUpdateLib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpUpdateLib.AutoSize = true;
            this.hlpUpdateLib.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpUpdateLib.Location = new System.Drawing.Point(325, 40);
            this.hlpUpdateLib.Name = "hlpUpdateLib";
            this.hlpUpdateLib.Size = new System.Drawing.Size(15, 15);
            this.hlpUpdateLib.TabIndex = 7;
            this.hlpUpdateLib.Text = "?";
            // 
            // hlpUpdateAppInfo
            // 
            this.hlpUpdateAppInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpUpdateAppInfo.AutoSize = true;
            this.hlpUpdateAppInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpUpdateAppInfo.Location = new System.Drawing.Point(325, 63);
            this.hlpUpdateAppInfo.Name = "hlpUpdateAppInfo";
            this.hlpUpdateAppInfo.Size = new System.Drawing.Size(15, 15);
            this.hlpUpdateAppInfo.TabIndex = 11;
            this.hlpUpdateAppInfo.Text = "?";
            // 
            // hlpSaveDB
            // 
            this.hlpSaveDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpSaveDB.AutoSize = true;
            this.hlpSaveDB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpSaveDB.Location = new System.Drawing.Point(325, 86);
            this.hlpSaveDB.Name = "hlpSaveDB";
            this.hlpSaveDB.Size = new System.Drawing.Size(15, 15);
            this.hlpSaveDB.TabIndex = 15;
            this.hlpSaveDB.Text = "?";
            // 
            // hlpExport
            // 
            this.hlpExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpExport.AutoSize = true;
            this.hlpExport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpExport.Location = new System.Drawing.Point(325, 109);
            this.hlpExport.Name = "hlpExport";
            this.hlpExport.Size = new System.Drawing.Size(15, 15);
            this.hlpExport.TabIndex = 19;
            this.hlpExport.Text = "?";
            // 
            // hlpImportCats
            // 
            this.hlpImportCats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpImportCats.AutoSize = true;
            this.hlpImportCats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpImportCats.Location = new System.Drawing.Point(501, 40);
            this.hlpImportCats.Name = "hlpImportCats";
            this.hlpImportCats.Size = new System.Drawing.Size(15, 15);
            this.hlpImportCats.TabIndex = 9;
            this.hlpImportCats.Text = "?";
            // 
            // hlpUpdateWeb
            // 
            this.hlpUpdateWeb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpUpdateWeb.AutoSize = true;
            this.hlpUpdateWeb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpUpdateWeb.Location = new System.Drawing.Point(501, 63);
            this.hlpUpdateWeb.Name = "hlpUpdateWeb";
            this.hlpUpdateWeb.Size = new System.Drawing.Size(15, 15);
            this.hlpUpdateWeb.TabIndex = 13;
            this.hlpUpdateWeb.Text = "?";
            // 
            // hlpSaveProfile
            // 
            this.hlpSaveProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpSaveProfile.AutoSize = true;
            this.hlpSaveProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpSaveProfile.Location = new System.Drawing.Point(501, 86);
            this.hlpSaveProfile.Name = "hlpSaveProfile";
            this.hlpSaveProfile.Size = new System.Drawing.Size(15, 15);
            this.hlpSaveProfile.TabIndex = 17;
            this.hlpSaveProfile.Text = "?";
            // 
            // hlpLaunch
            // 
            this.hlpLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpLaunch.AutoSize = true;
            this.hlpLaunch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpLaunch.Location = new System.Drawing.Point(501, 134);
            this.hlpLaunch.Name = "hlpLaunch";
            this.hlpLaunch.Size = new System.Drawing.Size(15, 15);
            this.hlpLaunch.TabIndex = 22;
            this.hlpLaunch.Text = "?";
            // 
            // hlpTolerate
            // 
            this.hlpTolerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpTolerate.AutoSize = true;
            this.hlpTolerate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpTolerate.Location = new System.Drawing.Point(299, 164);
            this.hlpTolerate.Name = "hlpTolerate";
            this.hlpTolerate.Size = new System.Drawing.Size(15, 15);
            this.hlpTolerate.TabIndex = 24;
            this.hlpTolerate.Text = "?";
            // 
            // hlpOutput
            // 
            this.hlpOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpOutput.AutoSize = true;
            this.hlpOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpOutput.Location = new System.Drawing.Point(501, 190);
            this.hlpOutput.Name = "hlpOutput";
            this.hlpOutput.Size = new System.Drawing.Size(15, 15);
            this.hlpOutput.TabIndex = 27;
            this.hlpOutput.Text = "?";
            // 
            // DlgAutomaticModeHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 340);
            this.ControlBox = false;
            this.Controls.Add(this.hlpOutput);
            this.Controls.Add(this.hlpTolerate);
            this.Controls.Add(this.hlpLaunch);
            this.Controls.Add(this.hlpSaveProfile);
            this.Controls.Add(this.hlpUpdateWeb);
            this.Controls.Add(this.hlpImportCats);
            this.Controls.Add(this.hlpExport);
            this.Controls.Add(this.hlpSaveDB);
            this.Controls.Add(this.hlpUpdateAppInfo);
            this.Controls.Add(this.hlpUpdateLib);
            this.Controls.Add(this.hlpSteamCheck);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdShortcut);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.chkExport);
            this.Controls.Add(this.chkSaveProfile);
            this.Controls.Add(this.chkSaveDB);
            this.Controls.Add(this.chkUpdateWeb);
            this.Controls.Add(this.chkUpdateAppInfo);
            this.Controls.Add(this.cmbSteamCheck);
            this.Controls.Add(this.lblSteamCheck);
            this.Controls.Add(this.chkAllAutocats);
            this.Controls.Add(this.lblAutocats);
            this.Controls.Add(this.lstAutocats);
            this.Controls.Add(this.cmbOutputMode);
            this.Controls.Add(this.cmbLaunch);
            this.Controls.Add(this.lblOutputMode);
            this.Controls.Add(this.lblLaunch);
            this.Controls.Add(this.chkImportCats);
            this.Controls.Add(this.chkUpdateLib);
            this.Controls.Add(this.chkTolerant);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DlgAutomaticModeHelper";
            this.Text = "Automatic Mode Helper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkTolerant;
        private System.Windows.Forms.CheckBox chkUpdateLib;
        private System.Windows.Forms.CheckBox chkImportCats;
        private System.Windows.Forms.Label lblLaunch;
        private System.Windows.Forms.Label lblOutputMode;
        private System.Windows.Forms.ComboBox cmbLaunch;
        private System.Windows.Forms.ComboBox cmbOutputMode;
        private System.Windows.Forms.ListView lstAutocats;
        private System.Windows.Forms.Label lblAutocats;
        private System.Windows.Forms.CheckBox chkAllAutocats;
        private System.Windows.Forms.Label lblSteamCheck;
        private System.Windows.Forms.ComboBox cmbSteamCheck;
        private System.Windows.Forms.CheckBox chkUpdateAppInfo;
        private System.Windows.Forms.CheckBox chkUpdateWeb;
        private System.Windows.Forms.CheckBox chkSaveDB;
        private System.Windows.Forms.CheckBox chkSaveProfile;
        private System.Windows.Forms.CheckBox chkExport;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button cmdShortcut;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label hlpSteamCheck;
        private System.Windows.Forms.Label hlpUpdateLib;
        private System.Windows.Forms.Label hlpUpdateAppInfo;
        private System.Windows.Forms.Label hlpSaveDB;
        private System.Windows.Forms.Label hlpExport;
        private System.Windows.Forms.Label hlpImportCats;
        private System.Windows.Forms.Label hlpUpdateWeb;
        private System.Windows.Forms.Label hlpSaveProfile;
        private System.Windows.Forms.Label hlpLaunch;
        private System.Windows.Forms.Label hlpTolerate;
        private System.Windows.Forms.Label hlpOutput;
    }
}