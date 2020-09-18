namespace Depressurizer
{
    partial class DlgRandomGame
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.gameBannerBox = new System.Windows.Forms.PictureBox();
            this.gameTextBox = new System.Windows.Forms.TextBox();
            this.btnLaunch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gameBannerBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(141, 205);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(69, 26);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // gameBannerBox
            // 
            this.gameBannerBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameBannerBox.Location = new System.Drawing.Point(43, 12);
            this.gameBannerBox.Name = "gameBannerBox";
            this.gameBannerBox.Size = new System.Drawing.Size(167, 95);
            this.gameBannerBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gameBannerBox.TabIndex = 1;
            this.gameBannerBox.TabStop = false;
            // 
            // gameTextBox
            // 
            this.gameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameTextBox.Location = new System.Drawing.Point(12, 113);
            this.gameTextBox.Multiline = true;
            this.gameTextBox.Name = "gameTextBox";
            this.gameTextBox.ReadOnly = true;
            this.gameTextBox.Size = new System.Drawing.Size(231, 50);
            this.gameTextBox.TabIndex = 3;
            this.gameTextBox.TabStop = false;
            this.gameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Location = new System.Drawing.Point(43, 205);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(75, 26);
            this.btnLaunch.TabIndex = 4;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // DlgRandomGame
            // 
            this.AcceptButton = this.cmdCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(255, 240);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.gameTextBox);
            this.Controls.Add(this.gameBannerBox);
            this.Controls.Add(this.cmdCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgRandomGame";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Random Game Selection";
            this.Load += new System.EventHandler(this.DlgRandomGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gameBannerBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox gameBannerBox;
        private System.Windows.Forms.TextBox gameTextBox;
        private System.Windows.Forms.Button btnLaunch;
    }
}