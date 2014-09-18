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
    partial class GameDBEntryDialog {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameDBEntryDialog));
            this.lblId = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblGenre = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtGenres = new System.Windows.Forms.TextBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.txtFlags = new System.Windows.Forms.TextBox();
            this.lblFlags = new System.Windows.Forms.Label();
            this.lblParent = new System.Windows.Forms.Label();
            this.txtParent = new System.Windows.Forms.TextBox();
            this.chkPlatWin = new System.Windows.Forms.CheckBox();
            this.chkPlatMac = new System.Windows.Forms.CheckBox();
            this.chkPlatLinux = new System.Windows.Forms.CheckBox();
            this.lblDev = new System.Windows.Forms.Label();
            this.lblPub = new System.Windows.Forms.Label();
            this.txtDev = new System.Windows.Forms.TextBox();
            this.txtPub = new System.Windows.Forms.TextBox();
            this.lblMCName = new System.Windows.Forms.Label();
            this.txtMCName = new System.Windows.Forms.TextBox();
            this.lblRelease = new System.Windows.Forms.Label();
            this.grpPlat = new System.Windows.Forms.GroupBox();
            this.txtRelease = new System.Windows.Forms.TextBox();
            this.dateWeb = new System.Windows.Forms.DateTimePicker();
            this.dateAppInfo = new System.Windows.Forms.DateTimePicker();
            this.chkWebUpdate = new System.Windows.Forms.CheckBox();
            this.chkAppInfoUpdate = new System.Windows.Forms.CheckBox();
            this.grpPlat.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblId
            // 
            resources.ApplyResources(this.lblId, "lblId");
            this.lblId.Name = "lblId";
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // lblGenre
            // 
            resources.ApplyResources(this.lblGenre, "lblGenre");
            this.lblGenre.Name = "lblGenre";
            // 
            // lblType
            // 
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // txtId
            // 
            resources.ApplyResources(this.txtId, "txtId");
            this.txtId.Name = "txtId";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // txtGenres
            // 
            resources.ApplyResources(this.txtGenres, "txtGenres");
            this.txtGenres.Name = "txtGenres";
            // 
            // cmbType
            // 
            resources.ApplyResources(this.cmbType, "cmbType");
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Name = "cmbType";
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            resources.ApplyResources(this.cmdSave, "cmdSave");
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // txtFlags
            // 
            resources.ApplyResources(this.txtFlags, "txtFlags");
            this.txtFlags.Name = "txtFlags";
            // 
            // lblFlags
            // 
            resources.ApplyResources(this.lblFlags, "lblFlags");
            this.lblFlags.Name = "lblFlags";
            // 
            // lblParent
            // 
            resources.ApplyResources(this.lblParent, "lblParent");
            this.lblParent.Name = "lblParent";
            // 
            // txtParent
            // 
            resources.ApplyResources(this.txtParent, "txtParent");
            this.txtParent.Name = "txtParent";
            // 
            // chkPlatWin
            // 
            resources.ApplyResources(this.chkPlatWin, "chkPlatWin");
            this.chkPlatWin.Name = "chkPlatWin";
            this.chkPlatWin.UseVisualStyleBackColor = true;
            // 
            // chkPlatMac
            // 
            resources.ApplyResources(this.chkPlatMac, "chkPlatMac");
            this.chkPlatMac.Name = "chkPlatMac";
            this.chkPlatMac.UseVisualStyleBackColor = true;
            // 
            // chkPlatLinux
            // 
            resources.ApplyResources(this.chkPlatLinux, "chkPlatLinux");
            this.chkPlatLinux.Name = "chkPlatLinux";
            this.chkPlatLinux.UseVisualStyleBackColor = true;
            // 
            // lblDev
            // 
            resources.ApplyResources(this.lblDev, "lblDev");
            this.lblDev.Name = "lblDev";
            // 
            // lblPub
            // 
            resources.ApplyResources(this.lblPub, "lblPub");
            this.lblPub.Name = "lblPub";
            // 
            // txtDev
            // 
            resources.ApplyResources(this.txtDev, "txtDev");
            this.txtDev.Name = "txtDev";
            // 
            // txtPub
            // 
            resources.ApplyResources(this.txtPub, "txtPub");
            this.txtPub.Name = "txtPub";
            // 
            // lblMCName
            // 
            resources.ApplyResources(this.lblMCName, "lblMCName");
            this.lblMCName.Name = "lblMCName";
            // 
            // txtMCName
            // 
            resources.ApplyResources(this.txtMCName, "txtMCName");
            this.txtMCName.Name = "txtMCName";
            // 
            // lblRelease
            // 
            resources.ApplyResources(this.lblRelease, "lblRelease");
            this.lblRelease.Name = "lblRelease";
            // 
            // grpPlat
            // 
            resources.ApplyResources(this.grpPlat, "grpPlat");
            this.grpPlat.Controls.Add(this.chkPlatLinux);
            this.grpPlat.Controls.Add(this.chkPlatMac);
            this.grpPlat.Controls.Add(this.chkPlatWin);
            this.grpPlat.Name = "grpPlat";
            this.grpPlat.TabStop = false;
            // 
            // txtRelease
            // 
            resources.ApplyResources(this.txtRelease, "txtRelease");
            this.txtRelease.Name = "txtRelease";
            // 
            // dateWeb
            // 
            resources.ApplyResources(this.dateWeb, "dateWeb");
            this.dateWeb.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateWeb.MaxDate = new System.DateTime(2035, 1, 19, 0, 0, 0, 0);
            this.dateWeb.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dateWeb.Name = "dateWeb";
            // 
            // dateAppInfo
            // 
            resources.ApplyResources(this.dateAppInfo, "dateAppInfo");
            this.dateAppInfo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateAppInfo.Name = "dateAppInfo";
            // 
            // chkWebUpdate
            // 
            resources.ApplyResources(this.chkWebUpdate, "chkWebUpdate");
            this.chkWebUpdate.Name = "chkWebUpdate";
            this.chkWebUpdate.UseVisualStyleBackColor = true;
            // 
            // chkAppInfoUpdate
            // 
            resources.ApplyResources(this.chkAppInfoUpdate, "chkAppInfoUpdate");
            this.chkAppInfoUpdate.Name = "chkAppInfoUpdate";
            this.chkAppInfoUpdate.UseVisualStyleBackColor = true;
            // 
            // GameDBEntryDialog
            // 
            this.AcceptButton = this.cmdSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ControlBox = false;
            this.Controls.Add(this.chkAppInfoUpdate);
            this.Controls.Add(this.chkWebUpdate);
            this.Controls.Add(this.dateAppInfo);
            this.Controls.Add(this.dateWeb);
            this.Controls.Add(this.txtRelease);
            this.Controls.Add(this.grpPlat);
            this.Controls.Add(this.lblRelease);
            this.Controls.Add(this.txtMCName);
            this.Controls.Add(this.lblMCName);
            this.Controls.Add(this.txtPub);
            this.Controls.Add(this.txtDev);
            this.Controls.Add(this.lblPub);
            this.Controls.Add(this.lblDev);
            this.Controls.Add(this.txtParent);
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.lblFlags);
            this.Controls.Add(this.txtFlags);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.txtGenres);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GameDBEntryDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.GameDBEntryForm_Load);
            this.grpPlat.ResumeLayout(false);
            this.grpPlat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtGenres;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TextBox txtFlags;
        private System.Windows.Forms.Label lblFlags;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.TextBox txtParent;
        private System.Windows.Forms.CheckBox chkPlatWin;
        private System.Windows.Forms.CheckBox chkPlatMac;
        private System.Windows.Forms.CheckBox chkPlatLinux;
        private System.Windows.Forms.Label lblDev;
        private System.Windows.Forms.Label lblPub;
        private System.Windows.Forms.TextBox txtDev;
        private System.Windows.Forms.TextBox txtPub;
        private System.Windows.Forms.Label lblMCName;
        private System.Windows.Forms.TextBox txtMCName;
        private System.Windows.Forms.Label lblRelease;
        private System.Windows.Forms.GroupBox grpPlat;
        private System.Windows.Forms.TextBox txtRelease;
        private System.Windows.Forms.DateTimePicker dateWeb;
        private System.Windows.Forms.DateTimePicker dateAppInfo;
        private System.Windows.Forms.CheckBox chkWebUpdate;
        private System.Windows.Forms.CheckBox chkAppInfoUpdate;
    }
}