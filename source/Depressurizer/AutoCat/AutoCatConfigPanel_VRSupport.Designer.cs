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

namespace Depressurizer {
    partial class AutoCatConfigPanel_VrSupport {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.grpVrPlayArea = new System.Windows.Forms.GroupBox();
            this.lstVrPlayArea = new System.Windows.Forms.ListView();
            this.grpVrInput = new System.Windows.Forms.GroupBox();
            this.lstVrInput = new System.Windows.Forms.ListView();
            this.grpVrHeadsets = new System.Windows.Forms.GroupBox();
            this.lstVrHeadsets = new System.Windows.Forms.ListView();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdUncheckAll = new System.Windows.Forms.Button();
            this.cmdCheckAll = new System.Windows.Forms.Button();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.tblVrFlags = new System.Windows.Forms.TableLayoutPanel();
            this.grpMain.SuspendLayout();
            this.grpVrPlayArea.SuspendLayout();
            this.grpVrInput.SuspendLayout();
            this.grpVrHeadsets.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.tblVrFlags.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
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
            this.grpMain.Text = "Edit VR Support Autocat";
            // 
            // grpVrPlayArea
            // 
            this.grpVrPlayArea.Controls.Add(this.lstVrPlayArea);
            this.grpVrPlayArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpVrPlayArea.Location = new System.Drawing.Point(378, 3);
            this.grpVrPlayArea.Name = "grpVrPlayArea";
            this.grpVrPlayArea.Size = new System.Drawing.Size(183, 349);
            this.grpVrPlayArea.TabIndex = 12;
            this.grpVrPlayArea.TabStop = false;
            this.grpVrPlayArea.Text = "PlayArea";
            // 
            // lstVrPlayArea
            // 
            this.lstVrPlayArea.CheckBoxes = true;
            this.lstVrPlayArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstVrPlayArea.Location = new System.Drawing.Point(3, 16);
            this.lstVrPlayArea.Name = "lstVrPlayArea";
            this.lstVrPlayArea.Size = new System.Drawing.Size(177, 330);
            this.lstVrPlayArea.TabIndex = 7;
            this.lstVrPlayArea.UseCompatibleStateImageBehavior = false;
            this.lstVrPlayArea.View = System.Windows.Forms.View.List;
            // 
            // grpVrInput
            // 
            this.grpVrInput.Controls.Add(this.lstVrInput);
            this.grpVrInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpVrInput.Location = new System.Drawing.Point(190, 3);
            this.grpVrInput.Name = "grpVrInput";
            this.grpVrInput.Size = new System.Drawing.Size(182, 349);
            this.grpVrInput.TabIndex = 11;
            this.grpVrInput.TabStop = false;
            this.grpVrInput.Text = "Input";
            // 
            // lstVrInput
            // 
            this.lstVrInput.CheckBoxes = true;
            this.lstVrInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstVrInput.Location = new System.Drawing.Point(3, 16);
            this.lstVrInput.Name = "lstVrInput";
            this.lstVrInput.Size = new System.Drawing.Size(176, 330);
            this.lstVrInput.TabIndex = 6;
            this.lstVrInput.UseCompatibleStateImageBehavior = false;
            this.lstVrInput.View = System.Windows.Forms.View.List;
            // 
            // grpVrHeadsets
            // 
            this.grpVrHeadsets.Controls.Add(this.lstVrHeadsets);
            this.grpVrHeadsets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpVrHeadsets.Location = new System.Drawing.Point(3, 3);
            this.grpVrHeadsets.Name = "grpVrHeadsets";
            this.grpVrHeadsets.Size = new System.Drawing.Size(181, 349);
            this.grpVrHeadsets.TabIndex = 10;
            this.grpVrHeadsets.TabStop = false;
            this.grpVrHeadsets.Text = "Headsets";
            // 
            // lstVrHeadsets
            // 
            this.lstVrHeadsets.CheckBoxes = true;
            this.lstVrHeadsets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstVrHeadsets.Location = new System.Drawing.Point(3, 16);
            this.lstVrHeadsets.Name = "lstVrHeadsets";
            this.lstVrHeadsets.Size = new System.Drawing.Size(175, 330);
            this.lstVrHeadsets.TabIndex = 4;
            this.lstVrHeadsets.UseCompatibleStateImageBehavior = false;
            this.lstVrHeadsets.View = System.Windows.Forms.View.List;
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
            this.tblVrFlags.Controls.Add(this.grpVrPlayArea, 2, 0);
            this.tblVrFlags.Controls.Add(this.grpVrHeadsets, 0, 0);
            this.tblVrFlags.Controls.Add(this.grpVrInput, 1, 0);
            this.tblVrFlags.Location = new System.Drawing.Point(6, 69);
            this.tblVrFlags.Name = "tblVrFlags";
            this.tblVrFlags.RowCount = 1;
            this.tblVrFlags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblVrFlags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblVrFlags.Size = new System.Drawing.Size(564, 355);
            this.tblVrFlags.TabIndex = 8;
            // 
            // AutoCatConfigPanel_VrSupport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_VrSupport";
            this.Size = new System.Drawing.Size(576, 460);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.grpVrPlayArea.ResumeLayout(false);
            this.grpVrInput.ResumeLayout(false);
            this.grpVrHeadsets.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.tblVrFlags.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button cmdCheckAll;
        private System.Windows.Forms.Button cmdUncheckAll;
        private System.Windows.Forms.ListView lstVrHeadsets;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.ListView lstVrPlayArea;
        private System.Windows.Forms.ListView lstVrInput;
        private System.Windows.Forms.GroupBox grpVrHeadsets;
        private System.Windows.Forms.GroupBox grpVrInput;
        private System.Windows.Forms.GroupBox grpVrPlayArea;
        private System.Windows.Forms.TableLayoutPanel tblVrFlags;
    }
}
