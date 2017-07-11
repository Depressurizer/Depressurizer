/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Theodoros Dimos.

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

namespace Depressurizer
{
    partial class AutoCatConfigPanel_Language
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.chkIncludeTypePrefix = new System.Windows.Forms.CheckBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdUncheckAll = new System.Windows.Forms.Button();
            this.cmdCheckAll = new System.Windows.Forms.Button();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.tblVrFlags = new System.Windows.Forms.TableLayoutPanel();
            this.grpFullAudio = new System.Windows.Forms.GroupBox();
            this.lstFullAudio = new System.Windows.Forms.ListView();
            this.grpInterface = new System.Windows.Forms.GroupBox();
            this.lstInterface = new System.Windows.Forms.ListView();
            this.grpSubtitles = new System.Windows.Forms.GroupBox();
            this.lstSubtitles = new System.Windows.Forms.ListView();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.chkTypeFallback = new System.Windows.Forms.CheckBox();
            this.grpMain.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.tblVrFlags.SuspendLayout();
            this.grpFullAudio.SuspendLayout();
            this.grpInterface.SuspendLayout();
            this.grpSubtitles.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.chkTypeFallback);
            this.grpMain.Controls.Add(this.chkIncludeTypePrefix);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.tblButtons);
            this.grpMain.Controls.Add(this.txtPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.tblVrFlags);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(576, 460);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit Language Autocat";
            // 
            // chkIncludeTypePrefix
            // 
            this.chkIncludeTypePrefix.AutoSize = true;
            this.chkIncludeTypePrefix.Location = new System.Drawing.Point(28, 45);
            this.chkIncludeTypePrefix.Name = "chkIncludeTypePrefix";
            this.chkIncludeTypePrefix.Size = new System.Drawing.Size(323, 17);
            this.chkIncludeTypePrefix.TabIndex = 10;
            this.chkIncludeTypePrefix.Text = "Include Interface/Subtitles/Full Audio prefix on category names";
            this.chkIncludeTypePrefix.UseVisualStyleBackColor = true;
            // 
            // helpPrefix
            // 
            this.helpPrefix.AutoSize = true;
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Location = new System.Drawing.Point(238, 22);
            this.helpPrefix.Name = "helpPrefix";
            this.helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.helpPrefix.TabIndex = 2;
            this.helpPrefix.Text = "?";
            // 
            // tblButtons
            // 
            this.tblButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblButtons.ColumnCount = 2;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblButtons.Controls.Add(this.cmdUncheckAll, 1, 0);
            this.tblButtons.Controls.Add(this.cmdCheckAll, 0, 0);
            this.tblButtons.Location = new System.Drawing.Point(3, 426);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(570, 30);
            this.tblButtons.TabIndex = 5;
            // 
            // cmdUncheckAll
            // 
            this.cmdUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUncheckAll.Location = new System.Drawing.Point(288, 4);
            this.cmdUncheckAll.Name = "cmdUncheckAll";
            this.cmdUncheckAll.Size = new System.Drawing.Size(279, 23);
            this.cmdUncheckAll.TabIndex = 1;
            this.cmdUncheckAll.Text = "Uncheck All";
            this.cmdUncheckAll.UseVisualStyleBackColor = true;
            this.cmdUncheckAll.Click += new System.EventHandler(this.cmdUncheckAll_Click);
            // 
            // cmdCheckAll
            // 
            this.cmdCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCheckAll.Location = new System.Drawing.Point(3, 4);
            this.cmdCheckAll.Name = "cmdCheckAll";
            this.cmdCheckAll.Size = new System.Drawing.Size(279, 23);
            this.cmdCheckAll.TabIndex = 0;
            this.cmdCheckAll.Text = "Check All";
            this.cmdCheckAll.UseVisualStyleBackColor = true;
            this.cmdCheckAll.Click += new System.EventHandler(this.cmdCheckAll_Click);
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(67, 19);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.txtPrefix.TabIndex = 1;
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.lblPrefix.TabIndex = 0;
            this.lblPrefix.Text = "Prefix:";
            // 
            // tblVrFlags
            // 
            this.tblVrFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblVrFlags.ColumnCount = 3;
            this.tblVrFlags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblVrFlags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblVrFlags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblVrFlags.Controls.Add(this.grpFullAudio, 2, 0);
            this.tblVrFlags.Controls.Add(this.grpInterface, 0, 0);
            this.tblVrFlags.Controls.Add(this.grpSubtitles, 1, 0);
            this.tblVrFlags.Location = new System.Drawing.Point(6, 94);
            this.tblVrFlags.Name = "tblVrFlags";
            this.tblVrFlags.RowCount = 1;
            this.tblVrFlags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblVrFlags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblVrFlags.Size = new System.Drawing.Size(564, 330);
            this.tblVrFlags.TabIndex = 8;
            // 
            // grpFullAudio
            // 
            this.grpFullAudio.Controls.Add(this.lstFullAudio);
            this.grpFullAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFullAudio.Location = new System.Drawing.Point(378, 3);
            this.grpFullAudio.Name = "grpFullAudio";
            this.grpFullAudio.Size = new System.Drawing.Size(183, 324);
            this.grpFullAudio.TabIndex = 12;
            this.grpFullAudio.TabStop = false;
            this.grpFullAudio.Text = "Full Audio";
            // 
            // lstFullAudio
            // 
            this.lstFullAudio.CheckBoxes = true;
            this.lstFullAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFullAudio.Location = new System.Drawing.Point(3, 16);
            this.lstFullAudio.Name = "lstFullAudio";
            this.lstFullAudio.Size = new System.Drawing.Size(177, 305);
            this.lstFullAudio.TabIndex = 7;
            this.lstFullAudio.UseCompatibleStateImageBehavior = false;
            this.lstFullAudio.View = System.Windows.Forms.View.List;
            // 
            // grpInterface
            // 
            this.grpInterface.Controls.Add(this.lstInterface);
            this.grpInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInterface.Location = new System.Drawing.Point(3, 3);
            this.grpInterface.Name = "grpInterface";
            this.grpInterface.Size = new System.Drawing.Size(181, 324);
            this.grpInterface.TabIndex = 10;
            this.grpInterface.TabStop = false;
            this.grpInterface.Text = "Interface";
            // 
            // lstInterface
            // 
            this.lstInterface.CheckBoxes = true;
            this.lstInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInterface.Location = new System.Drawing.Point(3, 16);
            this.lstInterface.Name = "lstInterface";
            this.lstInterface.Size = new System.Drawing.Size(175, 305);
            this.lstInterface.TabIndex = 4;
            this.lstInterface.UseCompatibleStateImageBehavior = false;
            this.lstInterface.View = System.Windows.Forms.View.List;
            // 
            // grpSubtitles
            // 
            this.grpSubtitles.Controls.Add(this.lstSubtitles);
            this.grpSubtitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSubtitles.Location = new System.Drawing.Point(190, 3);
            this.grpSubtitles.Name = "grpSubtitles";
            this.grpSubtitles.Size = new System.Drawing.Size(182, 324);
            this.grpSubtitles.TabIndex = 11;
            this.grpSubtitles.TabStop = false;
            this.grpSubtitles.Text = "Subtitles";
            // 
            // lstSubtitles
            // 
            this.lstSubtitles.CheckBoxes = true;
            this.lstSubtitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSubtitles.Location = new System.Drawing.Point(3, 16);
            this.lstSubtitles.Name = "lstSubtitles";
            this.lstSubtitles.Size = new System.Drawing.Size(176, 305);
            this.lstSubtitles.TabIndex = 6;
            this.lstSubtitles.UseCompatibleStateImageBehavior = false;
            this.lstSubtitles.View = System.Windows.Forms.View.List;
            // 
            // chkTypeFallback
            // 
            this.chkTypeFallback.AutoSize = true;
            this.chkTypeFallback.Location = new System.Drawing.Point(28, 68);
            this.chkTypeFallback.Name = "chkTypeFallback";
            this.chkTypeFallback.Size = new System.Drawing.Size(356, 17);
            this.chkTypeFallback.TabIndex = 11;
            this.chkTypeFallback.Text = "If a game doesn\'t support Subtitles/Full Audio at all, use Interface data";
            this.chkTypeFallback.UseVisualStyleBackColor = true;
            // 
            // AutoCatConfigPanel_Language
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Language";
            this.Size = new System.Drawing.Size(576, 460);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.tblButtons.ResumeLayout(false);
            this.tblVrFlags.ResumeLayout(false);
            this.grpFullAudio.ResumeLayout(false);
            this.grpInterface.ResumeLayout(false);
            this.grpSubtitles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button cmdCheckAll;
        private System.Windows.Forms.Button cmdUncheckAll;
        private System.Windows.Forms.ListView lstInterface;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.ListView lstFullAudio;
        private System.Windows.Forms.ListView lstSubtitles;
        private System.Windows.Forms.GroupBox grpInterface;
        private System.Windows.Forms.GroupBox grpSubtitles;
        private System.Windows.Forms.GroupBox grpFullAudio;
        private System.Windows.Forms.TableLayoutPanel tblVrFlags;
        private System.Windows.Forms.CheckBox chkIncludeTypePrefix;
        private System.Windows.Forms.CheckBox chkTypeFallback;
    }
}
