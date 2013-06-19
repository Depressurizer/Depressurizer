namespace Depressurizer {
    partial class DlgManualDownload {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.radId = new System.Windows.Forms.RadioButton();
            this.radUrl = new System.Windows.Forms.RadioButton();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.txtEntry = new System.Windows.Forms.TextBox();
            this.lblEnter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radId
            // 
            this.radId.AutoSize = true;
            this.radId.Location = new System.Drawing.Point(26, 41);
            this.radId.Name = "radId";
            this.radId.Size = new System.Drawing.Size(101, 17);
            this.radId.TabIndex = 0;
            this.radId.Text = "64-bit Steam ID ";
            this.radId.UseVisualStyleBackColor = true;
            // 
            // radUrl
            // 
            this.radUrl.AutoSize = true;
            this.radUrl.Checked = true;
            this.radUrl.Location = new System.Drawing.Point(26, 22);
            this.radUrl.Name = "radUrl";
            this.radUrl.Size = new System.Drawing.Size(113, 17);
            this.radUrl.TabIndex = 1;
            this.radUrl.TabStop = true;
            this.radUrl.Text = "custom URL name";
            this.radUrl.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(12, 91);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.Location = new System.Drawing.Point(271, 91);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 3;
            this.cmdOk.Text = "OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // txtEntry
            // 
            this.txtEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntry.Location = new System.Drawing.Point(12, 65);
            this.txtEntry.Name = "txtEntry";
            this.txtEntry.Size = new System.Drawing.Size(334, 20);
            this.txtEntry.TabIndex = 4;
            // 
            // lblEnter
            // 
            this.lblEnter.AutoSize = true;
            this.lblEnter.Location = new System.Drawing.Point(12, 6);
            this.lblEnter.Name = "lblEnter";
            this.lblEnter.Size = new System.Drawing.Size(82, 13);
            this.lblEnter.TabIndex = 5;
            this.lblEnter.Text = "Enter either the:";
            // 
            // DlgManualDownload
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(358, 126);
            this.ControlBox = false;
            this.Controls.Add(this.lblEnter);
            this.Controls.Add(this.txtEntry);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.radUrl);
            this.Controls.Add(this.radId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgManualDownload";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manual Download";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radId;
        private System.Windows.Forms.RadioButton radUrl;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.TextBox txtEntry;
        private System.Windows.Forms.Label lblEnter;
    }
}