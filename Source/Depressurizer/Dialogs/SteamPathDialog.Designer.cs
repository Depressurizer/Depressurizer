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
            this.PathLabel = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonBrowse = new System.Windows.Forms.Button();
            this.SelectedPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PathLabel.Location = new System.Drawing.Point(16, 56);
            this.PathLabel.Margin = new System.Windows.Forms.Padding(5);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(202, 15);
            this.PathLabel.TabIndex = 4;
            this.PathLabel.Text = "Locate the Steam installation folder:";
            // 
            // ButtonOk
            // 
            this.ButtonOk.BackColor = System.Drawing.Color.DimGray;
            this.ButtonOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ButtonOk.Location = new System.Drawing.Point(212, 106);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 25);
            this.ButtonOk.TabIndex = 7;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = false;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonBrowse
            // 
            this.ButtonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonBrowse.BackColor = System.Drawing.Color.DimGray;
            this.ButtonBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ButtonBrowse.Location = new System.Drawing.Point(398, 77);
            this.ButtonBrowse.Name = "ButtonBrowse";
            this.ButtonBrowse.Size = new System.Drawing.Size(75, 25);
            this.ButtonBrowse.TabIndex = 6;
            this.ButtonBrowse.Text = "Browse...";
            this.ButtonBrowse.UseVisualStyleBackColor = false;
            this.ButtonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
            // 
            // SelectedPath
            // 
            this.SelectedPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedPath.Location = new System.Drawing.Point(19, 79);
            this.SelectedPath.Name = "SelectedPath";
            this.SelectedPath.Size = new System.Drawing.Size(373, 21);
            this.SelectedPath.TabIndex = 5;
            this.SelectedPath.Text = "C:\\Program Files (x86)\\Steam";
            // 
            // SteamPathDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 141);
            this.Controls.Add(this.PathLabel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.ButtonBrowse);
            this.Controls.Add(this.SelectedPath);
            this.Name = "SteamPathDialog";
            this.Text = "SteamPathDialog";
            this.Controls.SetChildIndex(this.SelectedPath, 0);
            this.Controls.SetChildIndex(this.ButtonBrowse, 0);
            this.Controls.SetChildIndex(this.ButtonOk, 0);
            this.Controls.SetChildIndex(this.PathLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonBrowse;
        private System.Windows.Forms.TextBox SelectedPath;
    }
}