namespace Depressurizer.Forms
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
            this.LabelPath = new System.Windows.Forms.TextBox();
            this.lblPathLabel = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelPath
            // 
            this.LabelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelPath.Location = new System.Drawing.Point(13, 32);
            this.LabelPath.Margin = new System.Windows.Forms.Padding(4);
            this.LabelPath.Name = "LabelPath";
            this.LabelPath.Size = new System.Drawing.Size(357, 20);
            this.LabelPath.TabIndex = 2;
            // 
            // lblPathLabel
            // 
            this.lblPathLabel.AutoSize = true;
            this.lblPathLabel.Font = new System.Drawing.Font("Arial", 9F);
            this.lblPathLabel.ForeColor = System.Drawing.Color.White;
            this.lblPathLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPathLabel.Location = new System.Drawing.Point(11, 11);
            this.lblPathLabel.Margin = new System.Windows.Forms.Padding(2);
            this.lblPathLabel.Name = "lblPathLabel";
            this.lblPathLabel.Size = new System.Drawing.Size(203, 15);
            this.lblPathLabel.TabIndex = 3;
            this.lblPathLabel.Text = "Locate the Steam installation folder:";
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(195, 59);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(94, 23);
            this.ButtonOk.TabIndex = 6;
            this.ButtonOk.Text = "Ok";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonBrowse
            // 
            this.ButtonBrowse.Location = new System.Drawing.Point(380, 31);
            this.ButtonBrowse.Name = "ButtonBrowse";
            this.ButtonBrowse.Size = new System.Drawing.Size(92, 23);
            this.ButtonBrowse.TabIndex = 7;
            this.ButtonBrowse.Text = "Browse...";
            this.ButtonBrowse.UseVisualStyleBackColor = true;
            this.ButtonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
            // 
            // SteamPathDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 91);
            this.Controls.Add(this.ButtonBrowse);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.lblPathLabel);
            this.Controls.Add(this.LabelPath);
            this.Name = "SteamPathDialog";
            this.Text = "SteamPathDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LabelPath;
        private System.Windows.Forms.Label lblPathLabel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonBrowse;
    }
}