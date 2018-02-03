namespace Depressurizer.Dialogs
{
	partial class CancelableDialog
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
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.ButtonStop = new System.Windows.Forms.Button();
			this.lblText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.BackColor = System.Drawing.Color.DimGray;
			this.ButtonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ButtonCancel.Location = new System.Drawing.Point(256, 108);
			this.ButtonCancel.Margin = new System.Windows.Forms.Padding(10);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 5;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = false;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// ButtonStop
			// 
			this.ButtonStop.BackColor = System.Drawing.Color.DimGray;
			this.ButtonStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ButtonStop.Location = new System.Drawing.Point(161, 108);
			this.ButtonStop.Margin = new System.Windows.Forms.Padding(10);
			this.ButtonStop.Name = "ButtonStop";
			this.ButtonStop.Size = new System.Drawing.Size(75, 23);
			this.ButtonStop.TabIndex = 4;
			this.ButtonStop.Text = "Stop";
			this.ButtonStop.UseVisualStyleBackColor = false;
			this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
			// 
			// lblText
			// 
			this.lblText.AutoSize = true;
			this.lblText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblText.Location = new System.Drawing.Point(15, 62);
			this.lblText.Margin = new System.Windows.Forms.Padding(10);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(58, 15);
			this.lblText.TabIndex = 3;
			this.lblText.Text = "Starting...";
			// 
			// CancelableDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(350, 150);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonStop);
			this.Controls.Add(this.lblText);
			this.Name = "CancelableDialog";
			this.Text = "CancelableDialog";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CancelableDialog_FormClosing);
			this.Load += new System.EventHandler(this.CancelableDialog_Load);
			this.Controls.SetChildIndex(this.lblText, 0);
			this.Controls.SetChildIndex(this.ButtonStop, 0);
			this.Controls.SetChildIndex(this.ButtonCancel, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.Button ButtonStop;
		private System.Windows.Forms.Label lblText;
	}
}