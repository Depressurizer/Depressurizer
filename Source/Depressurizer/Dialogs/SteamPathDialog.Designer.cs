namespace Depressurizer.Dialogs
{
	partial class SteamPathDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamPathDialog));
			this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
			this.SelectedSteamPath = new System.Windows.Forms.TextBox();
			this.ButtonBrowse = new MaterialSkin.Controls.MaterialRaisedButton();
			this.ButtonOk = new MaterialSkin.Controls.MaterialRaisedButton();
			this.SuspendLayout();
			// 
			// materialLabel1
			// 
			resources.ApplyResources(this.materialLabel1, "materialLabel1");
			this.materialLabel1.Depth = 0;
			this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
			this.materialLabel1.Name = "materialLabel1";
			// 
			// SelectedSteamPath
			// 
			resources.ApplyResources(this.SelectedSteamPath, "SelectedSteamPath");
			this.SelectedSteamPath.Name = "SelectedSteamPath";
			// 
			// ButtonBrowse
			// 
			this.ButtonBrowse.Depth = 0;
			resources.ApplyResources(this.ButtonBrowse, "ButtonBrowse");
			this.ButtonBrowse.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonBrowse.Name = "ButtonBrowse";
			this.ButtonBrowse.Primary = true;
			this.ButtonBrowse.UseVisualStyleBackColor = true;
			this.ButtonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
			// 
			// ButtonOk
			// 
			this.ButtonOk.Depth = 0;
			resources.ApplyResources(this.ButtonOk, "ButtonOk");
			this.ButtonOk.MouseState = MaterialSkin.MouseState.HOVER;
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Primary = true;
			this.ButtonOk.UseVisualStyleBackColor = true;
			this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
			// 
			// SteamPathDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ButtonOk);
			this.Controls.Add(this.ButtonBrowse);
			this.Controls.Add(this.SelectedSteamPath);
			this.Controls.Add(this.materialLabel1);
			this.Name = "SteamPathDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MaterialSkin.Controls.MaterialLabel materialLabel1;
		private System.Windows.Forms.TextBox SelectedSteamPath;
		private MaterialSkin.Controls.MaterialRaisedButton ButtonBrowse;
		private MaterialSkin.Controls.MaterialRaisedButton ButtonOk;
	}
}