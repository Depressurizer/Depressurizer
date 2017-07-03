/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

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

namespace Depressurizer.AutoCat {
    partial class AutoCatConfigPanel_Name {
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
            this.helpPrefix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.cbSkipThe = new System.Windows.Forms.CheckBox();
            this.cbGroupNumbers = new System.Windows.Forms.CheckBox();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.cbGroupNumbers);
            this.grpMain.Controls.Add(this.cbSkipThe);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.txtPrefix);
            this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(504, 374);
            this.grpMain.TabIndex = 0;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Edit Name AutoCat";
            // 
            // helpPrefix
            // 
            this.helpPrefix.AutoSize = true;
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Location = new System.Drawing.Point(238, 22);
            this.helpPrefix.Name = "helpPrefix";
            this.helpPrefix.Size = new System.Drawing.Size(15, 15);
            this.helpPrefix.TabIndex = 5;
            this.helpPrefix.Text = "?";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(25, 22);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.lblPrefix.TabIndex = 3;
            this.lblPrefix.Text = "Prefix:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(67, 19);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(165, 20);
            this.txtPrefix.TabIndex = 4;
            // 
            // ttHelp
            // 
            this.ttHelp.UseFading = false;
            // 
            // cbSkipThe
            // 
            this.cbSkipThe.AutoSize = true;
            this.cbSkipThe.Checked = true;
            this.cbSkipThe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSkipThe.Location = new System.Drawing.Point(67, 45);
            this.cbSkipThe.Name = "cbSkipThe";
            this.cbSkipThe.Size = new System.Drawing.Size(79, 17);
            this.cbSkipThe.TabIndex = 6;
            this.cbSkipThe.Text = "Skip \"The\"";
            this.cbSkipThe.UseVisualStyleBackColor = true;
            // 
            // cbGroupNumbers
            // 
            this.cbGroupNumbers.AutoSize = true;
            this.cbGroupNumbers.Checked = true;
            this.cbGroupNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGroupNumbers.Location = new System.Drawing.Point(67, 68);
            this.cbGroupNumbers.Name = "cbGroupNumbers";
            this.cbGroupNumbers.Size = new System.Drawing.Size(143, 17);
            this.cbGroupNumbers.TabIndex = 7;
            this.cbGroupNumbers.Text = "Group all numbers to \"#\"";
            this.cbGroupNumbers.UseVisualStyleBackColor = true;
            // 
            // AutoCatConfigPanel_Name
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Name";
            this.Size = new System.Drawing.Size(504, 374);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.CheckBox cbGroupNumbers;
        private System.Windows.Forms.CheckBox cbSkipThe;
    }
}
