namespace Depressurizer.Dialogs
{
    partial class AboutDialog
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
            this.ButtonClose = new System.Windows.Forms.Button();
            this.LinkHomePage = new System.Windows.Forms.LinkLabel();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.LabelHomePage = new System.Windows.Forms.Label();
            this.LabelLicense = new System.Windows.Forms.Label();
            this.LinkLicense = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonClose.Location = new System.Drawing.Point(0, 120);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(320, 30);
            this.ButtonClose.TabIndex = 1;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // LinkHomePage
            // 
            this.LinkHomePage.AutoSize = true;
            this.LinkHomePage.Location = new System.Drawing.Point(24, 44);
            this.LinkHomePage.Margin = new System.Windows.Forms.Padding(15, 3, 3, 0);
            this.LinkHomePage.Name = "LinkHomePage";
            this.LinkHomePage.Size = new System.Drawing.Size(114, 13);
            this.LinkHomePage.TabIndex = 2;
            this.LinkHomePage.TabStop = true;
            this.LinkHomePage.Text = "mvegter/Depressurizer";
            this.LinkHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkHomePage_LinkClicked);
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelVersion.Location = new System.Drawing.Point(12, 9);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(129, 16);
            this.LabelVersion.TabIndex = 3;
            this.LabelVersion.Text = "Depressurizer v0.0.0";
            // 
            // LabelHomePage
            // 
            this.LabelHomePage.AutoSize = true;
            this.LabelHomePage.Location = new System.Drawing.Point(18, 28);
            this.LabelHomePage.Margin = new System.Windows.Forms.Padding(9, 3, 3, 0);
            this.LabelHomePage.Name = "LabelHomePage";
            this.LabelHomePage.Size = new System.Drawing.Size(96, 13);
            this.LabelHomePage.TabIndex = 4;
            this.LabelHomePage.Text = "Project homepage:";
            // 
            // LabelLicense
            // 
            this.LabelLicense.AutoSize = true;
            this.LabelLicense.Location = new System.Drawing.Point(18, 69);
            this.LabelLicense.Margin = new System.Windows.Forms.Padding(9, 12, 3, 0);
            this.LabelLicense.Name = "LabelLicense";
            this.LabelLicense.Size = new System.Drawing.Size(47, 13);
            this.LabelLicense.TabIndex = 6;
            this.LabelLicense.Text = "License:";
            // 
            // LinkLicense
            // 
            this.LinkLicense.AutoSize = true;
            this.LinkLicense.Location = new System.Drawing.Point(24, 85);
            this.LinkLicense.Margin = new System.Windows.Forms.Padding(15, 3, 3, 0);
            this.LinkLicense.Name = "LinkLicense";
            this.LinkLicense.Size = new System.Drawing.Size(167, 13);
            this.LinkLicense.TabIndex = 5;
            this.LinkLicense.TabStop = true;
            this.LinkLicense.Text = "GNU General Public License v3.0";
            this.LinkLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLicense_LinkClicked);
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonClose;
            this.ClientSize = new System.Drawing.Size(320, 150);
            this.ControlBox = false;
            this.Controls.Add(this.LabelLicense);
            this.Controls.Add(this.LinkLicense);
            this.Controls.Add(this.LabelHomePage);
            this.Controls.Add(this.LabelVersion);
            this.Controls.Add(this.LinkHomePage);
            this.Controls.Add(this.ButtonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AboutDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.LinkLabel LinkHomePage;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Label LabelHomePage;
        private System.Windows.Forms.Label LabelLicense;
        private System.Windows.Forms.LinkLabel LinkLicense;
    }
}