namespace Depressurizer.Dialogs
{
    partial class CloseDialog
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
			this.chkSaveSteam = new System.Windows.Forms.CheckBox();
			this.btnNo = new System.Windows.Forms.Button();
			this.Icon = new System.Windows.Forms.PictureBox();
			this.btnYes = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.MessageLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
			this.SuspendLayout();
			// 
			// chkSaveSteam
			// 
			this.chkSaveSteam.AutoSize = true;
			this.chkSaveSteam.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSaveSteam.Location = new System.Drawing.Point(18, 104);
			this.chkSaveSteam.Margin = new System.Windows.Forms.Padding(10, 5, 10, 10);
			this.chkSaveSteam.Name = "chkSaveSteam";
			this.chkSaveSteam.Size = new System.Drawing.Size(303, 19);
			this.chkSaveSteam.TabIndex = 13;
			this.chkSaveSteam.Text = "Save changes to Steam? (CLOSE STEAM FIRST!!!)";
			this.chkSaveSteam.UseVisualStyleBackColor = true;
			this.chkSaveSteam.CheckedChanged += new System.EventHandler(this.chkSaveSteam_CheckedChanged);
			// 
			// btnNo
			// 
			this.btnNo.BackColor = System.Drawing.Color.DimGray;
			this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.btnNo.ForeColor = System.Drawing.Color.White;
			this.btnNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnNo.Location = new System.Drawing.Point(124, 138);
			this.btnNo.Margin = new System.Windows.Forms.Padding(10, 5, 10, 10);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(75, 25);
			this.btnNo.TabIndex = 12;
			this.btnNo.Text = "No";
			this.btnNo.UseVisualStyleBackColor = false;
			// 
			// Icon
			// 
			this.Icon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.Icon.Location = new System.Drawing.Point(18, 57);
			this.Icon.Margin = new System.Windows.Forms.Padding(10);
			this.Icon.Name = "Icon";
			this.Icon.Size = new System.Drawing.Size(32, 32);
			this.Icon.TabIndex = 10;
			this.Icon.TabStop = false;
			// 
			// btnYes
			// 
			this.btnYes.BackColor = System.Drawing.Color.DimGray;
			this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnYes.ForeColor = System.Drawing.Color.White;
			this.btnYes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnYes.Location = new System.Drawing.Point(29, 138);
			this.btnYes.Margin = new System.Windows.Forms.Padding(10, 5, 10, 10);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(75, 25);
			this.btnYes.TabIndex = 9;
			this.btnYes.Text = "Yes";
			this.btnYes.UseVisualStyleBackColor = false;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.DimGray;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.ForeColor = System.Drawing.Color.White;
			this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCancel.Location = new System.Drawing.Point(219, 138);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(10, 5, 30, 10);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 25);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			// 
			// MessageLabel
			// 
			this.MessageLabel.AutoSize = true;
			this.MessageLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.MessageLabel.Location = new System.Drawing.Point(70, 64);
			this.MessageLabel.Margin = new System.Windows.Forms.Padding(10, 17, 10, 10);
			this.MessageLabel.Name = "MessageLabel";
			this.MessageLabel.Size = new System.Drawing.Size(70, 15);
			this.MessageLabel.TabIndex = 14;
			this.MessageLabel.Text = "Message ...";
			// 
			// CloseDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(333, 188);
			this.Controls.Add(this.MessageLabel);
			this.Controls.Add(this.chkSaveSteam);
			this.Controls.Add(this.btnNo);
			this.Controls.Add(this.Icon);
			this.Controls.Add(this.btnYes);
			this.Controls.Add(this.btnCancel);
			this.MaximumSize = new System.Drawing.Size(333, 188);
			this.MinimumSize = new System.Drawing.Size(333, 188);
			this.Name = "CloseDialog";
			this.Text = "CloseDialog";
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnYes, 0);
			this.Controls.SetChildIndex(this.Icon, 0);
			this.Controls.SetChildIndex(this.btnNo, 0);
			this.Controls.SetChildIndex(this.chkSaveSteam, 0);
			this.Controls.SetChildIndex(this.MessageLabel, 0);
			((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.CheckBox chkSaveSteam;
		private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.PictureBox Icon;
		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label MessageLabel;
	}
}