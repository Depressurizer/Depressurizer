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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FatalError));
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
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.Name = "lblMessage";
            // 
            // cmdClose
            // 
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // cmdSave
            // 
            resources.ApplyResources(this.cmdSave, "cmdSave");
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCopy
            // 
            resources.ApplyResources(this.cmdCopy, "cmdCopy");
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // cmdShow
            // 
            resources.ApplyResources(this.cmdShow, "cmdShow");
            this.cmdShow.Name = "cmdShow";
            this.cmdShow.UseVisualStyleBackColor = true;
            this.cmdShow.Click += new System.EventHandler(this.cmdShow_Click);
            // 
            // grpMoreInfo
            // 
            resources.ApplyResources(this.grpMoreInfo, "grpMoreInfo");
            this.grpMoreInfo.Controls.Add(this.txtTrace);
            this.grpMoreInfo.Controls.Add(this.txtErrMsg);
            this.grpMoreInfo.Controls.Add(this.txtErrType);
            this.grpMoreInfo.Controls.Add(this.lblTrace);
            this.grpMoreInfo.Controls.Add(this.lblErrMsg);
            this.grpMoreInfo.Controls.Add(this.lblErrType);
            this.grpMoreInfo.Controls.Add(this.cmdCopy);
            this.grpMoreInfo.Controls.Add(this.cmdSave);
            this.grpMoreInfo.Name = "grpMoreInfo";
            this.grpMoreInfo.TabStop = false;
            // 
            // txtTrace
            // 
            resources.ApplyResources(this.txtTrace, "txtTrace");
            this.txtTrace.Name = "txtTrace";
            this.txtTrace.ReadOnly = true;
            // 
            // txtErrMsg
            // 
            resources.ApplyResources(this.txtErrMsg, "txtErrMsg");
            this.txtErrMsg.Name = "txtErrMsg";
            this.txtErrMsg.ReadOnly = true;
            // 
            // txtErrType
            // 
            resources.ApplyResources(this.txtErrType, "txtErrType");
            this.txtErrType.Name = "txtErrType";
            this.txtErrType.ReadOnly = true;
            // 
            // lblTrace
            // 
            resources.ApplyResources(this.lblTrace, "lblTrace");
            this.lblTrace.Name = "lblTrace";
            // 
            // lblErrMsg
            // 
            resources.ApplyResources(this.lblErrMsg, "lblErrMsg");
            this.lblErrMsg.Name = "lblErrMsg";
            // 
            // lblErrType
            // 
            resources.ApplyResources(this.lblErrType, "lblErrType");
            this.lblErrType.Name = "lblErrType";
            // 
            // FatalError
            // 
            this.AcceptButton = this.cmdClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ControlBox = false;
            this.Controls.Add(this.grpMoreInfo);
            this.Controls.Add(this.cmdShow);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lblMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FatalError";
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