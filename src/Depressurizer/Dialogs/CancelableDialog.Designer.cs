namespace Depressurizer.Dialogs {
    partial class CancelableDialog {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CancelableDialog));
            this.lblText = new System.Windows.Forms.Label();
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblText
            // 
            resources.ApplyResources(this.lblText, "lblText");
            this.lblText.Name = "lblText";
            // 
            // ButtonStop
            // 
            resources.ApplyResources(this.ButtonStop, "ButtonStop");
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // ButtonCancel
            // 
            resources.ApplyResources(this.ButtonCancel, "ButtonCancel");
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // CancelableDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.lblText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CancelableDialog";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CancelableDialog_FormClosing);
            this.Load += new System.EventHandler(this.CancelableDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.Button ButtonCancel;
    }
}