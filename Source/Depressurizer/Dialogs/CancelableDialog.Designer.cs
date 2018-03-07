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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CancelableDialog));
			this.ButtonStop = new MaterialSkin.Controls.MaterialRaisedButton();
			this.ButtonCancel = new MaterialSkin.Controls.MaterialRaisedButton();
			this.LabelText = new MaterialSkin.Controls.MaterialLabel();
			this.SuspendLayout();
			// 
			// ButtonStop
			// 
			resources.ApplyResources(this.ButtonStop, "ButtonStop");
			this.ButtonStop.Depth = 0;
			this.ButtonStop.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonStop.Name = "ButtonStop";
			this.ButtonStop.Primary = true;
			this.ButtonStop.UseVisualStyleBackColor = true;
			this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
			// 
			// ButtonCancel
			// 
			resources.ApplyResources(this.ButtonCancel, "ButtonCancel");
			this.ButtonCancel.Depth = 0;
			this.ButtonCancel.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Primary = true;
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// LabelText
			// 
			resources.ApplyResources(this.LabelText, "LabelText");
			this.LabelText.Depth = 0;
			this.LabelText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.LabelText.MouseState = MaterialSkin.MouseState.HOVER;
			this.LabelText.Name = "LabelText";
			// 
			// CancelableDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ControlBox = false;
			this.Controls.Add(this.LabelText);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonStop);
			this.Name = "CancelableDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CancelableDialog_FormClosing);
			this.Load += new System.EventHandler(this.CancelableDialog_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private MaterialSkin.Controls.MaterialRaisedButton ButtonStop;
		private MaterialSkin.Controls.MaterialRaisedButton ButtonCancel;
		private MaterialSkin.Controls.MaterialLabel LabelText;
	}
}