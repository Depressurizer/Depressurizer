using System.ComponentModel;

namespace Depressurizer.Dialogs
{
	partial class ScrapeDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScrapeDialog));
			this.ButtonCancel = new MaterialSkin.Controls.MaterialRaisedButton();
			this.ButtonStop = new MaterialSkin.Controls.MaterialRaisedButton();
			this.LabelStatus = new MaterialSkin.Controls.MaterialLabel();
			this.SuspendLayout();
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.AutoSize = true;
			this.ButtonCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ButtonCancel.Depth = 0;
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Icon = null;
			this.ButtonCancel.Location = new System.Drawing.Point(299, 168);
			this.ButtonCancel.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Primary = true;
			this.ButtonCancel.Size = new System.Drawing.Size(73, 36);
			this.ButtonCancel.TabIndex = 0;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// ButtonStop
			// 
			this.ButtonStop.AutoSize = true;
			this.ButtonStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ButtonStop.Depth = 0;
			this.ButtonStop.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.ButtonStop.Icon = null;
			this.ButtonStop.Location = new System.Drawing.Point(237, 168);
			this.ButtonStop.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonStop.Name = "ButtonStop";
			this.ButtonStop.Primary = true;
			this.ButtonStop.Size = new System.Drawing.Size(56, 36);
			this.ButtonStop.TabIndex = 1;
			this.ButtonStop.Text = "Stop";
			this.ButtonStop.UseVisualStyleBackColor = true;
			this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
			// 
			// LabelStatus
			// 
			this.LabelStatus.AutoSize = true;
			this.LabelStatus.BackColor = System.Drawing.Color.Transparent;
			this.LabelStatus.Depth = 0;
			this.LabelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
			this.LabelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.LabelStatus.Location = new System.Drawing.Point(12, 74);
			this.LabelStatus.Margin = new System.Windows.Forms.Padding(3, 65, 3, 0);
			this.LabelStatus.MouseState = MaterialSkin.MouseState.HOVER;
			this.LabelStatus.Name = "LabelStatus";
			this.LabelStatus.Size = new System.Drawing.Size(70, 18);
			this.LabelStatus.TabIndex = 2;
			this.LabelStatus.Text = "Starting...";
			// 
			// ScrapeDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 216);
			this.ControlBox = false;
			this.Controls.Add(this.LabelStatus);
			this.Controls.Add(this.ButtonStop);
			this.Controls.Add(this.ButtonCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(384, 216);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(384, 216);
			this.Name = "ScrapeDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ScrapeDialog";
			this.Load += new System.EventHandler(this.ScrapeDialog_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScrapeDialog_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MaterialSkin.Controls.MaterialRaisedButton ButtonCancel;
		private MaterialSkin.Controls.MaterialRaisedButton ButtonStop;
		private MaterialSkin.Controls.MaterialLabel LabelStatus;
	}
}