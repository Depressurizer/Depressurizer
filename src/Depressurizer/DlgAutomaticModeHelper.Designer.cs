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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgAutomaticModeHelper));
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
            this.hlpTolerant = new System.Windows.Forms.Label();
            this.hlpOutput = new System.Windows.Forms.Label();
            this.lblExplain = new System.Windows.Forms.Label();
            this.hlpUpdateHltb = new System.Windows.Forms.Label();
            this.chkUpdateHltb = new System.Windows.Forms.CheckBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.SuspendLayout();
            // 
            // chkTolerant
            // 
            resources.ApplyResources(this.chkTolerant, "chkTolerant");
            this.chkTolerant.Name = "chkTolerant";
            this.ttHelp.SetToolTip(this.chkTolerant, resources.GetString("chkTolerant.ToolTip"));
            this.chkTolerant.UseVisualStyleBackColor = true;
            this.chkTolerant.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // chkUpdateLib
            // 
            resources.ApplyResources(this.chkUpdateLib, "chkUpdateLib");
            this.chkUpdateLib.Checked = true;
            this.chkUpdateLib.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateLib.Name = "chkUpdateLib";
            this.ttHelp.SetToolTip(this.chkUpdateLib, resources.GetString("chkUpdateLib.ToolTip"));
            this.chkUpdateLib.UseVisualStyleBackColor = true;
            this.chkUpdateLib.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // chkImportCats
            // 
            resources.ApplyResources(this.chkImportCats, "chkImportCats");
            this.chkImportCats.Name = "chkImportCats";
            this.ttHelp.SetToolTip(this.chkImportCats, resources.GetString("chkImportCats.ToolTip"));
            this.chkImportCats.UseVisualStyleBackColor = true;
            this.chkImportCats.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // lblLaunch
            // 
            resources.ApplyResources(this.lblLaunch, "lblLaunch");
            this.lblLaunch.Name = "lblLaunch";
            this.ttHelp.SetToolTip(this.lblLaunch, resources.GetString("lblLaunch.ToolTip"));
            // 
            // lblOutputMode
            // 
            resources.ApplyResources(this.lblOutputMode, "lblOutputMode");
            this.lblOutputMode.Name = "lblOutputMode";
            this.ttHelp.SetToolTip(this.lblOutputMode, resources.GetString("lblOutputMode.ToolTip"));
            // 
            // cmbLaunch
            // 
            resources.ApplyResources(this.cmbLaunch, "cmbLaunch");
            this.cmbLaunch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLaunch.FormattingEnabled = true;
            this.cmbLaunch.Items.AddRange(new object[] {
            resources.GetString("cmbLaunch.Items"),
            resources.GetString("cmbLaunch.Items1"),
            resources.GetString("cmbLaunch.Items2")});
            this.cmbLaunch.Name = "cmbLaunch";
            this.ttHelp.SetToolTip(this.cmbLaunch, resources.GetString("cmbLaunch.ToolTip"));
            this.cmbLaunch.SelectedIndexChanged += new System.EventHandler(this.ItemChanged);
            // 
            // cmbOutputMode
            // 
            resources.ApplyResources(this.cmbOutputMode, "cmbOutputMode");
            this.cmbOutputMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputMode.FormattingEnabled = true;
            this.cmbOutputMode.Items.AddRange(new object[] {
            resources.GetString("cmbOutputMode.Items"),
            resources.GetString("cmbOutputMode.Items1"),
            resources.GetString("cmbOutputMode.Items2")});
            this.cmbOutputMode.Name = "cmbOutputMode";
            this.ttHelp.SetToolTip(this.cmbOutputMode, resources.GetString("cmbOutputMode.ToolTip"));
            this.cmbOutputMode.SelectedIndexChanged += new System.EventHandler(this.ItemChanged);
            // 
            // lstAutocats
            // 
            resources.ApplyResources(this.lstAutocats, "lstAutocats");
            this.lstAutocats.CheckBoxes = true;
            this.lstAutocats.HideSelection = false;
            this.lstAutocats.Name = "lstAutocats";
            this.ttHelp.SetToolTip(this.lstAutocats, resources.GetString("lstAutocats.ToolTip"));
            this.lstAutocats.UseCompatibleStateImageBehavior = false;
            this.lstAutocats.View = System.Windows.Forms.View.List;
            this.lstAutocats.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstAutocats_ItemChecked);
            // 
            // lblAutocats
            // 
            resources.ApplyResources(this.lblAutocats, "lblAutocats");
            this.lblAutocats.Name = "lblAutocats";
            this.ttHelp.SetToolTip(this.lblAutocats, resources.GetString("lblAutocats.ToolTip"));
            // 
            // chkAllAutocats
            // 
            resources.ApplyResources(this.chkAllAutocats, "chkAllAutocats");
            this.chkAllAutocats.Name = "chkAllAutocats";
            this.ttHelp.SetToolTip(this.chkAllAutocats, resources.GetString("chkAllAutocats.ToolTip"));
            this.chkAllAutocats.UseVisualStyleBackColor = true;
            this.chkAllAutocats.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // lblSteamCheck
            // 
            resources.ApplyResources(this.lblSteamCheck, "lblSteamCheck");
            this.lblSteamCheck.Name = "lblSteamCheck";
            this.ttHelp.SetToolTip(this.lblSteamCheck, resources.GetString("lblSteamCheck.ToolTip"));
            // 
            // cmbSteamCheck
            // 
            resources.ApplyResources(this.cmbSteamCheck, "cmbSteamCheck");
            this.cmbSteamCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSteamCheck.FormattingEnabled = true;
            this.cmbSteamCheck.Items.AddRange(new object[] {
            resources.GetString("cmbSteamCheck.Items"),
            resources.GetString("cmbSteamCheck.Items1"),
            resources.GetString("cmbSteamCheck.Items2")});
            this.cmbSteamCheck.Name = "cmbSteamCheck";
            this.ttHelp.SetToolTip(this.cmbSteamCheck, resources.GetString("cmbSteamCheck.ToolTip"));
            this.cmbSteamCheck.SelectedIndexChanged += new System.EventHandler(this.ItemChanged);
            // 
            // chkUpdateAppInfo
            // 
            resources.ApplyResources(this.chkUpdateAppInfo, "chkUpdateAppInfo");
            this.chkUpdateAppInfo.Checked = true;
            this.chkUpdateAppInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateAppInfo.Name = "chkUpdateAppInfo";
            this.ttHelp.SetToolTip(this.chkUpdateAppInfo, resources.GetString("chkUpdateAppInfo.ToolTip"));
            this.chkUpdateAppInfo.UseVisualStyleBackColor = true;
            this.chkUpdateAppInfo.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // chkUpdateWeb
            // 
            resources.ApplyResources(this.chkUpdateWeb, "chkUpdateWeb");
            this.chkUpdateWeb.Checked = true;
            this.chkUpdateWeb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateWeb.Name = "chkUpdateWeb";
            this.ttHelp.SetToolTip(this.chkUpdateWeb, resources.GetString("chkUpdateWeb.ToolTip"));
            this.chkUpdateWeb.UseVisualStyleBackColor = true;
            this.chkUpdateWeb.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // chkSaveDB
            // 
            resources.ApplyResources(this.chkSaveDB, "chkSaveDB");
            this.chkSaveDB.Checked = true;
            this.chkSaveDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveDB.Name = "chkSaveDB";
            this.ttHelp.SetToolTip(this.chkSaveDB, resources.GetString("chkSaveDB.ToolTip"));
            this.chkSaveDB.UseVisualStyleBackColor = true;
            this.chkSaveDB.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // chkSaveProfile
            // 
            resources.ApplyResources(this.chkSaveProfile, "chkSaveProfile");
            this.chkSaveProfile.Checked = true;
            this.chkSaveProfile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveProfile.Name = "chkSaveProfile";
            this.ttHelp.SetToolTip(this.chkSaveProfile, resources.GetString("chkSaveProfile.ToolTip"));
            this.chkSaveProfile.UseVisualStyleBackColor = true;
            this.chkSaveProfile.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // chkExport
            // 
            resources.ApplyResources(this.chkExport, "chkExport");
            this.chkExport.Checked = true;
            this.chkExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExport.Name = "chkExport";
            this.ttHelp.SetToolTip(this.chkExport, resources.GetString("chkExport.ToolTip"));
            this.chkExport.UseVisualStyleBackColor = true;
            this.chkExport.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // txtResult
            // 
            resources.ApplyResources(this.txtResult, "txtResult");
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.ttHelp.SetToolTip(this.txtResult, resources.GetString("txtResult.ToolTip"));
            // 
            // lblResult
            // 
            resources.ApplyResources(this.lblResult, "lblResult");
            this.lblResult.Name = "lblResult";
            this.ttHelp.SetToolTip(this.lblResult, resources.GetString("lblResult.ToolTip"));
            // 
            // cmdShortcut
            // 
            resources.ApplyResources(this.cmdShortcut, "cmdShortcut");
            this.cmdShortcut.Name = "cmdShortcut";
            this.ttHelp.SetToolTip(this.cmdShortcut, resources.GetString("cmdShortcut.ToolTip"));
            this.cmdShortcut.UseVisualStyleBackColor = true;
            this.cmdShortcut.Click += new System.EventHandler(this.cmdShortcut_Click);
            // 
            // cmdClose
            // 
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Name = "cmdClose";
            this.ttHelp.SetToolTip(this.cmdClose, resources.GetString("cmdClose.ToolTip"));
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // hlpSteamCheck
            // 
            resources.ApplyResources(this.hlpSteamCheck, "hlpSteamCheck");
            this.hlpSteamCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpSteamCheck.Name = "hlpSteamCheck";
            this.ttHelp.SetToolTip(this.hlpSteamCheck, resources.GetString("hlpSteamCheck.ToolTip"));
            // 
            // hlpUpdateLib
            // 
            resources.ApplyResources(this.hlpUpdateLib, "hlpUpdateLib");
            this.hlpUpdateLib.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpUpdateLib.Name = "hlpUpdateLib";
            this.ttHelp.SetToolTip(this.hlpUpdateLib, resources.GetString("hlpUpdateLib.ToolTip"));
            // 
            // hlpUpdateAppInfo
            // 
            resources.ApplyResources(this.hlpUpdateAppInfo, "hlpUpdateAppInfo");
            this.hlpUpdateAppInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpUpdateAppInfo.Name = "hlpUpdateAppInfo";
            this.ttHelp.SetToolTip(this.hlpUpdateAppInfo, resources.GetString("hlpUpdateAppInfo.ToolTip"));
            // 
            // hlpSaveDB
            // 
            resources.ApplyResources(this.hlpSaveDB, "hlpSaveDB");
            this.hlpSaveDB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpSaveDB.Name = "hlpSaveDB";
            this.ttHelp.SetToolTip(this.hlpSaveDB, resources.GetString("hlpSaveDB.ToolTip"));
            // 
            // hlpExport
            // 
            resources.ApplyResources(this.hlpExport, "hlpExport");
            this.hlpExport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpExport.Name = "hlpExport";
            this.ttHelp.SetToolTip(this.hlpExport, resources.GetString("hlpExport.ToolTip"));
            // 
            // hlpImportCats
            // 
            resources.ApplyResources(this.hlpImportCats, "hlpImportCats");
            this.hlpImportCats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpImportCats.Name = "hlpImportCats";
            this.ttHelp.SetToolTip(this.hlpImportCats, resources.GetString("hlpImportCats.ToolTip"));
            // 
            // hlpUpdateWeb
            // 
            resources.ApplyResources(this.hlpUpdateWeb, "hlpUpdateWeb");
            this.hlpUpdateWeb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpUpdateWeb.Name = "hlpUpdateWeb";
            this.ttHelp.SetToolTip(this.hlpUpdateWeb, resources.GetString("hlpUpdateWeb.ToolTip"));
            // 
            // hlpSaveProfile
            // 
            resources.ApplyResources(this.hlpSaveProfile, "hlpSaveProfile");
            this.hlpSaveProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpSaveProfile.Name = "hlpSaveProfile";
            this.ttHelp.SetToolTip(this.hlpSaveProfile, resources.GetString("hlpSaveProfile.ToolTip"));
            // 
            // hlpLaunch
            // 
            resources.ApplyResources(this.hlpLaunch, "hlpLaunch");
            this.hlpLaunch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpLaunch.Name = "hlpLaunch";
            this.ttHelp.SetToolTip(this.hlpLaunch, resources.GetString("hlpLaunch.ToolTip"));
            // 
            // hlpTolerant
            // 
            resources.ApplyResources(this.hlpTolerant, "hlpTolerant");
            this.hlpTolerant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpTolerant.Name = "hlpTolerant";
            this.ttHelp.SetToolTip(this.hlpTolerant, resources.GetString("hlpTolerant.ToolTip"));
            // 
            // hlpOutput
            // 
            resources.ApplyResources(this.hlpOutput, "hlpOutput");
            this.hlpOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpOutput.Name = "hlpOutput";
            this.ttHelp.SetToolTip(this.hlpOutput, resources.GetString("hlpOutput.ToolTip"));
            // 
            // lblExplain
            // 
            resources.ApplyResources(this.lblExplain, "lblExplain");
            this.lblExplain.Name = "lblExplain";
            this.ttHelp.SetToolTip(this.lblExplain, resources.GetString("lblExplain.ToolTip"));
            // 
            // hlpUpdateHltb
            // 
            resources.ApplyResources(this.hlpUpdateHltb, "hlpUpdateHltb");
            this.hlpUpdateHltb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hlpUpdateHltb.Name = "hlpUpdateHltb";
            this.ttHelp.SetToolTip(this.hlpUpdateHltb, resources.GetString("hlpUpdateHltb.ToolTip"));
            // 
            // chkUpdateHltb
            // 
            resources.ApplyResources(this.chkUpdateHltb, "chkUpdateHltb");
            this.chkUpdateHltb.Checked = true;
            this.chkUpdateHltb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateHltb.Name = "chkUpdateHltb";
            this.ttHelp.SetToolTip(this.chkUpdateHltb, resources.GetString("chkUpdateHltb.ToolTip"));
            this.chkUpdateHltb.UseVisualStyleBackColor = true;
            this.chkUpdateHltb.CheckedChanged += new System.EventHandler(this.ItemChanged);
            // 
            // DlgAutomaticModeHelper
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ControlBox = false;
            this.Controls.Add(this.hlpUpdateHltb);
            this.Controls.Add(this.chkUpdateHltb);
            this.Controls.Add(this.lblExplain);
            this.Controls.Add(this.hlpOutput);
            this.Controls.Add(this.hlpTolerant);
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
            this.ttHelp.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.DlgAutomaticModeHelper_Load);
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
        private System.Windows.Forms.Label hlpTolerant;
        private System.Windows.Forms.Label hlpOutput;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.Label lblExplain;
        private System.Windows.Forms.Label hlpUpdateHltb;
        private System.Windows.Forms.CheckBox chkUpdateHltb;
    }
}