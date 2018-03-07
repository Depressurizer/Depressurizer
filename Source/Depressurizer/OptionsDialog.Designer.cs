namespace Depressurizer
{
    partial class OptionsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.TabGeneral = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.ListStoreLangauge = new System.Windows.Forms.ComboBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.ListInterfaceLangauge = new System.Windows.Forms.ComboBox();
			this.CheckDepressurizerUpdates = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.CheckIncludeImputed = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.NumReScrapeDays = new System.Windows.Forms.NumericUpDown();
			this.CheckAutoSave = new System.Windows.Forms.CheckBox();
			this.CheckUpdateHLTB = new System.Windows.Forms.CheckBox();
			this.CheckUpdateAppInfo = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ButtonProfileBrowse = new System.Windows.Forms.Button();
			this.TextBoxProfile = new System.Windows.Forms.TextBox();
			this.RadioCreateProfile = new System.Windows.Forms.RadioButton();
			this.RadioLoadProfile = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ButtonSteamBrowse = new System.Windows.Forms.Button();
			this.TextBoxSteamDirectory = new System.Windows.Forms.TextBox();
			this.ButtonOk = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.TabGeneral.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumReScrapeDays)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TabGeneral
			// 
			this.TabGeneral.Controls.Add(this.tabPage1);
			this.TabGeneral.Location = new System.Drawing.Point(8, 79);
			this.TabGeneral.Margin = new System.Windows.Forms.Padding(3, 70, 3, 3);
			this.TabGeneral.Name = "TabGeneral";
			this.TabGeneral.SelectedIndex = 0;
			this.TabGeneral.Size = new System.Drawing.Size(494, 351);
			this.TabGeneral.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
			this.tabPage1.Controls.Add(this.groupBox5);
			this.tabPage1.Controls.Add(this.groupBox4);
			this.tabPage1.Controls.Add(this.CheckDepressurizerUpdates);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.ForeColor = System.Drawing.Color.White;
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(486, 325);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.ListStoreLangauge);
			this.groupBox5.ForeColor = System.Drawing.Color.White;
			this.groupBox5.Location = new System.Drawing.Point(250, 270);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(231, 52);
			this.groupBox5.TabIndex = 5;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Steam Store Language";
			// 
			// ListStoreLangauge
			// 
			this.ListStoreLangauge.FormattingEnabled = true;
			this.ListStoreLangauge.Location = new System.Drawing.Point(7, 19);
			this.ListStoreLangauge.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ListStoreLangauge.Name = "ListStoreLangauge";
			this.ListStoreLangauge.Size = new System.Drawing.Size(218, 21);
			this.ListStoreLangauge.TabIndex = 1;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.ListInterfaceLangauge);
			this.groupBox4.ForeColor = System.Drawing.Color.White;
			this.groupBox4.Location = new System.Drawing.Point(5, 270);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(231, 52);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Interface Language";
			// 
			// ListInterfaceLangauge
			// 
			this.ListInterfaceLangauge.FormattingEnabled = true;
			this.ListInterfaceLangauge.Location = new System.Drawing.Point(7, 19);
			this.ListInterfaceLangauge.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ListInterfaceLangauge.Name = "ListInterfaceLangauge";
			this.ListInterfaceLangauge.Size = new System.Drawing.Size(218, 21);
			this.ListInterfaceLangauge.TabIndex = 0;
			// 
			// CheckDepressurizerUpdates
			// 
			this.CheckDepressurizerUpdates.AutoSize = true;
			this.CheckDepressurizerUpdates.Location = new System.Drawing.Point(12, 5);
			this.CheckDepressurizerUpdates.Name = "CheckDepressurizerUpdates";
			this.CheckDepressurizerUpdates.Size = new System.Drawing.Size(221, 17);
			this.CheckDepressurizerUpdates.TabIndex = 3;
			this.CheckDepressurizerUpdates.Text = "Check for Depressurizer updates on start.";
			this.CheckDepressurizerUpdates.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.CheckIncludeImputed);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.NumReScrapeDays);
			this.groupBox3.Controls.Add(this.CheckAutoSave);
			this.groupBox3.Controls.Add(this.CheckUpdateHLTB);
			this.groupBox3.Controls.Add(this.CheckUpdateAppInfo);
			this.groupBox3.ForeColor = System.Drawing.Color.White;
			this.groupBox3.Location = new System.Drawing.Point(5, 145);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(477, 120);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Database Options";
			// 
			// CheckIncludeImputed
			// 
			this.CheckIncludeImputed.AutoSize = true;
			this.CheckIncludeImputed.Location = new System.Drawing.Point(232, 44);
			this.CheckIncludeImputed.Name = "CheckIncludeImputed";
			this.CheckIncludeImputed.Size = new System.Drawing.Size(162, 17);
			this.CheckIncludeImputed.TabIndex = 6;
			this.CheckIncludeImputed.Text = "Include imputed HLTB times.";
			this.CheckIncludeImputed.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(301, 94);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "days.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 94);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(225, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Re-scrape web-sourced data that is older than";
			// 
			// NumReScrapeDays
			// 
			this.NumReScrapeDays.Location = new System.Drawing.Point(232, 93);
			this.NumReScrapeDays.Margin = new System.Windows.Forms.Padding(0);
			this.NumReScrapeDays.Name = "NumReScrapeDays";
			this.NumReScrapeDays.Size = new System.Drawing.Size(64, 20);
			this.NumReScrapeDays.TabIndex = 3;
			// 
			// CheckAutoSave
			// 
			this.CheckAutoSave.AutoSize = true;
			this.CheckAutoSave.Location = new System.Drawing.Point(7, 69);
			this.CheckAutoSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.CheckAutoSave.Name = "CheckAutoSave";
			this.CheckAutoSave.Size = new System.Drawing.Size(232, 17);
			this.CheckAutoSave.TabIndex = 2;
			this.CheckAutoSave.Text = "Automatically save database after changes.";
			this.CheckAutoSave.UseVisualStyleBackColor = true;
			// 
			// CheckUpdateHLTB
			// 
			this.CheckUpdateHLTB.AutoSize = true;
			this.CheckUpdateHLTB.Location = new System.Drawing.Point(7, 44);
			this.CheckUpdateHLTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.CheckUpdateHLTB.Name = "CheckUpdateHLTB";
			this.CheckUpdateHLTB.Size = new System.Drawing.Size(237, 17);
			this.CheckUpdateHLTB.TabIndex = 1;
			this.CheckUpdateHLTB.Text = "Update HowLongToBeat data once a week.";
			this.CheckUpdateHLTB.UseVisualStyleBackColor = true;
			// 
			// CheckUpdateAppInfo
			// 
			this.CheckUpdateAppInfo.AutoSize = true;
			this.CheckUpdateAppInfo.Location = new System.Drawing.Point(7, 19);
			this.CheckUpdateAppInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.CheckUpdateAppInfo.Name = "CheckUpdateAppInfo";
			this.CheckUpdateAppInfo.Size = new System.Drawing.Size(229, 17);
			this.CheckUpdateAppInfo.TabIndex = 0;
			this.CheckUpdateAppInfo.Text = "On start update the database with AppInfo.";
			this.CheckUpdateAppInfo.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
			this.groupBox2.Controls.Add(this.ButtonProfileBrowse);
			this.groupBox2.Controls.Add(this.TextBoxProfile);
			this.groupBox2.Controls.Add(this.RadioCreateProfile);
			this.groupBox2.Controls.Add(this.RadioLoadProfile);
			this.groupBox2.ForeColor = System.Drawing.Color.White;
			this.groupBox2.Location = new System.Drawing.Point(5, 79);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(477, 61);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "On Startup";
			// 
			// ButtonProfileBrowse
			// 
			this.ButtonProfileBrowse.BackColor = System.Drawing.Color.DimGray;
			this.ButtonProfileBrowse.Location = new System.Drawing.Point(405, 15);
			this.ButtonProfileBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ButtonProfileBrowse.Name = "ButtonProfileBrowse";
			this.ButtonProfileBrowse.Size = new System.Drawing.Size(64, 22);
			this.ButtonProfileBrowse.TabIndex = 2;
			this.ButtonProfileBrowse.Text = "Browse...";
			this.ButtonProfileBrowse.UseVisualStyleBackColor = false;
			this.ButtonProfileBrowse.Click += new System.EventHandler(this.ButtonProfileBrowse_Click);
			// 
			// TextBoxProfile
			// 
			this.TextBoxProfile.Location = new System.Drawing.Point(93, 16);
			this.TextBoxProfile.Name = "TextBoxProfile";
			this.TextBoxProfile.Size = new System.Drawing.Size(305, 20);
			this.TextBoxProfile.TabIndex = 2;
			// 
			// RadioCreateProfile
			// 
			this.RadioCreateProfile.AutoSize = true;
			this.RadioCreateProfile.Location = new System.Drawing.Point(7, 39);
			this.RadioCreateProfile.Name = "RadioCreateProfile";
			this.RadioCreateProfile.Size = new System.Drawing.Size(88, 17);
			this.RadioCreateProfile.TabIndex = 1;
			this.RadioCreateProfile.TabStop = true;
			this.RadioCreateProfile.Text = "Create Profile";
			this.RadioCreateProfile.UseVisualStyleBackColor = true;
			// 
			// RadioLoadProfile
			// 
			this.RadioLoadProfile.AutoSize = true;
			this.RadioLoadProfile.Location = new System.Drawing.Point(7, 17);
			this.RadioLoadProfile.Name = "RadioLoadProfile";
			this.RadioLoadProfile.Size = new System.Drawing.Size(84, 17);
			this.RadioLoadProfile.TabIndex = 0;
			this.RadioLoadProfile.TabStop = true;
			this.RadioLoadProfile.Text = "Load Profile:";
			this.RadioLoadProfile.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
			this.groupBox1.Controls.Add(this.ButtonSteamBrowse);
			this.groupBox1.Controls.Add(this.TextBoxSteamDirectory);
			this.groupBox1.ForeColor = System.Drawing.Color.White;
			this.groupBox1.Location = new System.Drawing.Point(5, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(477, 47);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Steam Directory";
			// 
			// ButtonSteamBrowse
			// 
			this.ButtonSteamBrowse.BackColor = System.Drawing.Color.DimGray;
			this.ButtonSteamBrowse.Location = new System.Drawing.Point(405, 17);
			this.ButtonSteamBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ButtonSteamBrowse.Name = "ButtonSteamBrowse";
			this.ButtonSteamBrowse.Size = new System.Drawing.Size(64, 22);
			this.ButtonSteamBrowse.TabIndex = 1;
			this.ButtonSteamBrowse.Text = "Browse...";
			this.ButtonSteamBrowse.UseVisualStyleBackColor = false;
			this.ButtonSteamBrowse.Click += new System.EventHandler(this.ButtonSteamBrowse_Click);
			// 
			// TextBoxSteamDirectory
			// 
			this.TextBoxSteamDirectory.Location = new System.Drawing.Point(7, 19);
			this.TextBoxSteamDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.TextBoxSteamDirectory.Name = "TextBoxSteamDirectory";
			this.TextBoxSteamDirectory.Size = new System.Drawing.Size(391, 20);
			this.TextBoxSteamDirectory.TabIndex = 0;
			this.TextBoxSteamDirectory.Text = "C:\\Program Files (x86)\\Steam";
			// 
			// ButtonOk
			// 
			this.ButtonOk.BackColor = System.Drawing.Color.DimGray;
			this.ButtonOk.Location = new System.Drawing.Point(436, 437);
			this.ButtonOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Size = new System.Drawing.Size(64, 22);
			this.ButtonOk.TabIndex = 3;
			this.ButtonOk.Text = "OK";
			this.ButtonOk.UseVisualStyleBackColor = false;
			this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.BackColor = System.Drawing.Color.DimGray;
			this.ButtonCancel.Location = new System.Drawing.Point(363, 437);
			this.ButtonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(64, 22);
			this.ButtonCancel.TabIndex = 4;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = false;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// OptionsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(515, 465);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonOk);
			this.Controls.Add(this.TabGeneral);
			this.Name = "OptionsDialog";
			this.Text = "Options";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsDialog_FormClosing);
			this.Load += new System.EventHandler(this.OptionsDialog_Load);
			this.TabGeneral.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumReScrapeDays)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.TabControl TabGeneral;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button ButtonSteamBrowse;
		private System.Windows.Forms.TextBox TextBoxSteamDirectory;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button ButtonProfileBrowse;
		private System.Windows.Forms.TextBox TextBoxProfile;
		private System.Windows.Forms.RadioButton RadioCreateProfile;
		private System.Windows.Forms.RadioButton RadioLoadProfile;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox CheckAutoSave;
		private System.Windows.Forms.CheckBox CheckUpdateHLTB;
		private System.Windows.Forms.CheckBox CheckUpdateAppInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown NumReScrapeDays;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox CheckIncludeImputed;
		private System.Windows.Forms.CheckBox CheckDepressurizerUpdates;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ComboBox ListInterfaceLangauge;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.ComboBox ListStoreLangauge;
		private System.Windows.Forms.Button ButtonOk;
		private System.Windows.Forms.Button ButtonCancel;
	}
}