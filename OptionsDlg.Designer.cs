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
            this.grpSteamDir.SuspendLayout();
            this.grpStartup.SuspendLayout();
            this.grpSaving.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSteamDir
            // 
            this.grpSteamDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSteamDir.Controls.Add(this.cmdSteamPathBrowse);
            this.grpSteamDir.Controls.Add(this.txtSteamPath);
            this.grpSteamDir.Location = new System.Drawing.Point(12, 12);
            this.grpSteamDir.Name = "grpSteamDir";
            this.grpSteamDir.Size = new System.Drawing.Size(473, 54);
            this.grpSteamDir.TabIndex = 0;
            this.grpSteamDir.TabStop = false;
            this.grpSteamDir.Text = "Steam Directory";
            // 
            // cmdSteamPathBrowse
            // 
            this.cmdSteamPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSteamPathBrowse.Location = new System.Drawing.Point(389, 20);
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
            this.txtSteamPath.Size = new System.Drawing.Size(372, 20);
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
            this.grpStartup.Location = new System.Drawing.Point(12, 72);
            this.grpStartup.Name = "grpStartup";
            this.grpStartup.Size = new System.Drawing.Size(473, 90);
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
            this.radLoad.Location = new System.Drawing.Point(10, 19);
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
            this.cmdDefaultProfileBrowse.Location = new System.Drawing.Point(389, 16);
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
            this.txtDefaultProfile.Size = new System.Drawing.Size(282, 20);
            this.txtDefaultProfile.TabIndex = 1;
            // 
            // chkRemoveExtraEntries
            // 
            this.chkRemoveExtraEntries.AutoSize = true;
            this.chkRemoveExtraEntries.Location = new System.Drawing.Point(6, 19);
            this.chkRemoveExtraEntries.Name = "chkRemoveExtraEntries";
            this.chkRemoveExtraEntries.Size = new System.Drawing.Size(345, 17);
            this.chkRemoveExtraEntries.TabIndex = 0;
            this.chkRemoveExtraEntries.Text = "Remove entries for deleted or unknown games in Steam settings file";
            this.chkRemoveExtraEntries.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Location = new System.Drawing.Point(410, 219);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(75, 23);
            this.cmdAccept.TabIndex = 4;
            this.cmdAccept.Text = "OK";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(329, 219);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpSaving
            // 
            this.grpSaving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSaving.Controls.Add(this.chkRemoveExtraEntries);
            this.grpSaving.Location = new System.Drawing.Point(12, 168);
            this.grpSaving.Name = "grpSaving";
            this.grpSaving.Size = new System.Drawing.Size(473, 45);
            this.grpSaving.TabIndex = 2;
            this.grpSaving.TabStop = false;
            this.grpSaving.Text = "Manual Saving";
            // 
            // OptionsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 252);
            this.ControlBox = false;
            this.Controls.Add(this.grpSaving);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.grpStartup);
            this.Controls.Add(this.grpSteamDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OptionsDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.grpSteamDir.ResumeLayout(false);
            this.grpSteamDir.PerformLayout();
            this.grpStartup.ResumeLayout(false);
            this.grpStartup.PerformLayout();
            this.grpSaving.ResumeLayout(false);
            this.grpSaving.PerformLayout();
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
    }
}