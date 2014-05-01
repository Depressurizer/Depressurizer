namespace Depressurizer {
    partial class AutoLoadDlg {
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
            this.components = new System.ComponentModel.Container();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.lblSteamPath = new System.Windows.Forms.Label();
            this.txtSteamPath = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.lblUserId = new System.Windows.Forms.Label();
            this.combUserId = new System.Windows.Forms.ComboBox();
            this.cmdRefreshIdList = new System.Windows.Forms.Button();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.txtProfileName = new System.Windows.Forms.TextBox();
            this.lnkHelpId = new System.Windows.Forms.LinkLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lnkHelpPath = new System.Windows.Forms.LinkLabel();
            this.lnkHelpProfile = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCancel.Location = new System.Drawing.Point(359, 100);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 11;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLoad.Location = new System.Drawing.Point(440, 100);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(75, 23);
            this.cmdLoad.TabIndex = 12;
            this.cmdLoad.Text = "Load";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // lblSteamPath
            // 
            this.lblSteamPath.AutoSize = true;
            this.lblSteamPath.Location = new System.Drawing.Point(12, 7);
            this.lblSteamPath.Name = "lblSteamPath";
            this.lblSteamPath.Size = new System.Drawing.Size(77, 13);
            this.lblSteamPath.TabIndex = 0;
            this.lblSteamPath.Text = "Path to Steam:";
            // 
            // txtSteamPath
            // 
            this.txtSteamPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSteamPath.Location = new System.Drawing.Point(12, 23);
            this.txtSteamPath.Name = "txtSteamPath";
            this.txtSteamPath.Size = new System.Drawing.Size(422, 20);
            this.txtSteamPath.TabIndex = 2;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.Location = new System.Drawing.Point(440, 21);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowse.TabIndex = 3;
            this.cmdBrowse.Text = "Browse...";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(12, 55);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(79, 13);
            this.lblUserId.TabIndex = 4;
            this.lblUserId.Text = "Select User ID:";
            // 
            // combUserId
            // 
            this.combUserId.FormattingEnabled = true;
            this.combUserId.Location = new System.Drawing.Point(12, 71);
            this.combUserId.Name = "combUserId";
            this.combUserId.Size = new System.Drawing.Size(186, 21);
            this.combUserId.TabIndex = 6;
            // 
            // cmdRefreshIdList
            // 
            this.cmdRefreshIdList.Location = new System.Drawing.Point(204, 69);
            this.cmdRefreshIdList.Name = "cmdRefreshIdList";
            this.cmdRefreshIdList.Size = new System.Drawing.Size(75, 23);
            this.cmdRefreshIdList.TabIndex = 7;
            this.cmdRefreshIdList.Text = "Refresh";
            this.cmdRefreshIdList.UseVisualStyleBackColor = true;
            this.cmdRefreshIdList.Click += new System.EventHandler(this.cmdRefreshIdList_Click);
            // 
            // lblProfileName
            // 
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(297, 55);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(95, 13);
            this.lblProfileName.TabIndex = 8;
            this.lblProfileName.Text = "Enter profile name:";
            // 
            // txtProfileName
            // 
            this.txtProfileName.Location = new System.Drawing.Point(297, 71);
            this.txtProfileName.Name = "txtProfileName";
            this.txtProfileName.Size = new System.Drawing.Size(219, 20);
            this.txtProfileName.TabIndex = 10;
            // 
            // lnkHelpId
            // 
            this.lnkHelpId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkHelpId.AutoSize = true;
            this.lnkHelpId.Location = new System.Drawing.Point(171, 55);
            this.lnkHelpId.Name = "lnkHelpId";
            this.lnkHelpId.Size = new System.Drawing.Size(27, 13);
            this.lnkHelpId.TabIndex = 5;
            this.lnkHelpId.TabStop = true;
            this.lnkHelpId.Text = "help";
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 0;
            this.toolTip.AutoPopDelay = 30000;
            this.toolTip.InitialDelay = 0;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 50;
            this.toolTip.ShowAlways = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Help";
            // 
            // lnkHelpPath
            // 
            this.lnkHelpPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkHelpPath.AutoSize = true;
            this.lnkHelpPath.Location = new System.Drawing.Point(407, 7);
            this.lnkHelpPath.Name = "lnkHelpPath";
            this.lnkHelpPath.Size = new System.Drawing.Size(27, 13);
            this.lnkHelpPath.TabIndex = 1;
            this.lnkHelpPath.TabStop = true;
            this.lnkHelpPath.Text = "help";
            // 
            // lnkHelpProfile
            // 
            this.lnkHelpProfile.AutoSize = true;
            this.lnkHelpProfile.Location = new System.Drawing.Point(488, 55);
            this.lnkHelpProfile.Name = "lnkHelpProfile";
            this.lnkHelpProfile.Size = new System.Drawing.Size(27, 13);
            this.lnkHelpProfile.TabIndex = 9;
            this.lnkHelpProfile.TabStop = true;
            this.lnkHelpProfile.Text = "help";
            // 
            // AutoLoadDlg
            // 
            this.AcceptButton = this.cmdLoad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(527, 135);
            this.Controls.Add(this.lnkHelpProfile);
            this.Controls.Add(this.lnkHelpPath);
            this.Controls.Add(this.lnkHelpId);
            this.Controls.Add(this.txtProfileName);
            this.Controls.Add(this.lblProfileName);
            this.Controls.Add(this.cmdRefreshIdList);
            this.Controls.Add(this.combUserId);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.lblUserId);
            this.Controls.Add(this.txtSteamPath);
            this.Controls.Add(this.lblSteamPath);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoLoadDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Auto Load";
            this.Load += new System.EventHandler(this.AutoLoadDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.Label lblSteamPath;
        private System.Windows.Forms.TextBox txtSteamPath;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.ComboBox combUserId;
        private System.Windows.Forms.Button cmdRefreshIdList;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.TextBox txtProfileName;
        private System.Windows.Forms.LinkLabel lnkHelpId;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.LinkLabel lnkHelpPath;
        private System.Windows.Forms.LinkLabel lnkHelpProfile;
    }
}