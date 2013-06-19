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
namespace Rallion {
    partial class FatalError {
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.cmdShow = new System.Windows.Forms.Button();
            this.grpMoreInfo = new System.Windows.Forms.GroupBox();
            this.txtTrace = new System.Windows.Forms.TextBox();
            this.txtErrMsg = new System.Windows.Forms.TextBox();
            this.txtErrType = new System.Windows.Forms.TextBox();
            this.lblTrace = new System.Windows.Forms.Label();
            this.lblErrMsg = new System.Windows.Forms.Label();
            this.lblErrType = new System.Windows.Forms.Label();
            this.grpMoreInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Location = new System.Drawing.Point(12, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(444, 43);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "This program has encountered a fatal error and needs to close.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(381, 55);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "OK";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(322, 223);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(116, 23);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "Save to File";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCopy.Location = new System.Drawing.Point(200, 223);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(116, 23);
            this.cmdCopy.TabIndex = 6;
            this.cmdCopy.Text = "Copy to Clipboard";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // cmdShow
            // 
            this.cmdShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdShow.Location = new System.Drawing.Point(300, 55);
            this.cmdShow.Name = "cmdShow";
            this.cmdShow.Size = new System.Drawing.Size(75, 23);
            this.cmdShow.TabIndex = 1;
            this.cmdShow.Text = "More Info";
            this.cmdShow.UseVisualStyleBackColor = true;
            this.cmdShow.Click += new System.EventHandler(this.cmdShow_Click);
            // 
            // grpMoreInfo
            // 
            this.grpMoreInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMoreInfo.Controls.Add(this.txtTrace);
            this.grpMoreInfo.Controls.Add(this.txtErrMsg);
            this.grpMoreInfo.Controls.Add(this.txtErrType);
            this.grpMoreInfo.Controls.Add(this.lblTrace);
            this.grpMoreInfo.Controls.Add(this.lblErrMsg);
            this.grpMoreInfo.Controls.Add(this.lblErrType);
            this.grpMoreInfo.Controls.Add(this.cmdCopy);
            this.grpMoreInfo.Controls.Add(this.cmdSave);
            this.grpMoreInfo.Location = new System.Drawing.Point(12, 84);
            this.grpMoreInfo.Name = "grpMoreInfo";
            this.grpMoreInfo.Size = new System.Drawing.Size(444, 252);
            this.grpMoreInfo.TabIndex = 3;
            this.grpMoreInfo.TabStop = false;
            this.grpMoreInfo.Text = "Error Details";
            // 
            // txtTrace
            // 
            this.txtTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTrace.Location = new System.Drawing.Point(92, 91);
            this.txtTrace.Multiline = true;
            this.txtTrace.Name = "txtTrace";
            this.txtTrace.ReadOnly = true;
            this.txtTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTrace.Size = new System.Drawing.Size(346, 126);
            this.txtTrace.TabIndex = 5;
            // 
            // txtErrMsg
            // 
            this.txtErrMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtErrMsg.Location = new System.Drawing.Point(92, 45);
            this.txtErrMsg.Multiline = true;
            this.txtErrMsg.Name = "txtErrMsg";
            this.txtErrMsg.ReadOnly = true;
            this.txtErrMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtErrMsg.Size = new System.Drawing.Size(346, 40);
            this.txtErrMsg.TabIndex = 3;
            // 
            // txtErrType
            // 
            this.txtErrType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtErrType.Location = new System.Drawing.Point(92, 19);
            this.txtErrType.Name = "txtErrType";
            this.txtErrType.ReadOnly = true;
            this.txtErrType.Size = new System.Drawing.Size(346, 20);
            this.txtErrType.TabIndex = 1;
            // 
            // lblTrace
            // 
            this.lblTrace.AutoSize = true;
            this.lblTrace.Location = new System.Drawing.Point(17, 94);
            this.lblTrace.Name = "lblTrace";
            this.lblTrace.Size = new System.Drawing.Size(69, 13);
            this.lblTrace.TabIndex = 4;
            this.lblTrace.Text = "Stack Trace:";
            // 
            // lblErrMsg
            // 
            this.lblErrMsg.AutoSize = true;
            this.lblErrMsg.Location = new System.Drawing.Point(8, 48);
            this.lblErrMsg.Name = "lblErrMsg";
            this.lblErrMsg.Size = new System.Drawing.Size(78, 13);
            this.lblErrMsg.TabIndex = 2;
            this.lblErrMsg.Text = "Error Message:";
            // 
            // lblErrType
            // 
            this.lblErrType.AutoSize = true;
            this.lblErrType.Location = new System.Drawing.Point(27, 22);
            this.lblErrType.Name = "lblErrType";
            this.lblErrType.Size = new System.Drawing.Size(59, 13);
            this.lblErrType.TabIndex = 0;
            this.lblErrType.Text = "Error Type:";
            // 
            // FatalError
            // 
            this.AcceptButton = this.cmdClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(468, 348);
            this.ControlBox = false;
            this.Controls.Add(this.grpMoreInfo);
            this.Controls.Add(this.cmdShow);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lblMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FatalError";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fatal Error";
            this.Load += new System.EventHandler(this.FatalError_Load);
            this.grpMoreInfo.ResumeLayout(false);
            this.grpMoreInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.Button cmdShow;
        private System.Windows.Forms.GroupBox grpMoreInfo;
        private System.Windows.Forms.TextBox txtTrace;
        private System.Windows.Forms.TextBox txtErrMsg;
        private System.Windows.Forms.TextBox txtErrType;
        private System.Windows.Forms.Label lblTrace;
        private System.Windows.Forms.Label lblErrMsg;
        private System.Windows.Forms.Label lblErrType;
    }
}