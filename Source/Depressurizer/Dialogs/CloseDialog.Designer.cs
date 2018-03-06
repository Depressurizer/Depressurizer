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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloseDialog));
			this.ButtonYes = new MaterialSkin.Controls.MaterialRaisedButton();
			this.ButtonNo = new MaterialSkin.Controls.MaterialRaisedButton();
			this.ButtonCancel = new MaterialSkin.Controls.MaterialRaisedButton();
			this.MessageIcon = new System.Windows.Forms.PictureBox();
			this.CheckSaveToSteam = new MaterialSkin.Controls.MaterialCheckBox();
			this.MessageLabel = new MaterialSkin.Controls.MaterialLabel();
			((System.ComponentModel.ISupportInitialize)(this.MessageIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// ButtonYes
			// 
			resources.ApplyResources(this.ButtonYes, "ButtonYes");
			this.ButtonYes.Depth = 0;
			this.ButtonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.ButtonYes.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonYes.Name = "ButtonYes";
			this.ButtonYes.Primary = true;
			this.ButtonYes.UseVisualStyleBackColor = true;
			// 
			// ButtonNo
			// 
			resources.ApplyResources(this.ButtonNo, "ButtonNo");
			this.ButtonNo.Depth = 0;
			this.ButtonNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.ButtonNo.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonNo.Name = "ButtonNo";
			this.ButtonNo.Primary = true;
			this.ButtonNo.UseVisualStyleBackColor = true;
			// 
			// ButtonCancel
			// 
			resources.ApplyResources(this.ButtonCancel, "ButtonCancel");
			this.ButtonCancel.Depth = 0;
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Primary = true;
			this.ButtonCancel.UseVisualStyleBackColor = true;
			// 
			// MessageIcon
			// 
			resources.ApplyResources(this.MessageIcon, "MessageIcon");
			this.MessageIcon.Name = "MessageIcon";
			this.MessageIcon.TabStop = false;
			// 
			// CheckSaveToSteam
			// 
			resources.ApplyResources(this.CheckSaveToSteam, "CheckSaveToSteam");
			this.CheckSaveToSteam.Depth = 0;
			this.CheckSaveToSteam.MouseLocation = new System.Drawing.Point(-1, -1);
			this.CheckSaveToSteam.MouseState = MaterialSkin.MouseState.HOVER;
			this.CheckSaveToSteam.Name = "CheckSaveToSteam";
			this.CheckSaveToSteam.Ripple = true;
			this.CheckSaveToSteam.UseVisualStyleBackColor = true;
			this.CheckSaveToSteam.CheckedChanged += new System.EventHandler(this.CheckSaveToSteam_CheckedChanged);
			// 
			// MessageLabel
			// 
			resources.ApplyResources(this.MessageLabel, "MessageLabel");
			this.MessageLabel.Depth = 0;
			this.MessageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.MessageLabel.MouseState = MaterialSkin.MouseState.HOVER;
			this.MessageLabel.Name = "MessageLabel";
			// 
			// CloseDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
			this.Controls.Add(this.MessageLabel);
			this.Controls.Add(this.CheckSaveToSteam);
			this.Controls.Add(this.MessageIcon);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonNo);
			this.Controls.Add(this.ButtonYes);
			this.Name = "CloseDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			((System.ComponentModel.ISupportInitialize)(this.MessageIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MaterialSkin.Controls.MaterialRaisedButton ButtonYes;
		private MaterialSkin.Controls.MaterialRaisedButton ButtonNo;
		private MaterialSkin.Controls.MaterialRaisedButton ButtonCancel;
		private System.Windows.Forms.PictureBox MessageIcon;
		private MaterialSkin.Controls.MaterialCheckBox CheckSaveToSteam;
		private MaterialSkin.Controls.MaterialLabel MessageLabel;
	}
}