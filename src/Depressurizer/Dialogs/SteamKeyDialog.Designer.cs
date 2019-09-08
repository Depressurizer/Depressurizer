namespace Depressurizer.Dialogs
{
    partial class SteamKeyDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamKeyDialog));
            this.TextSteamKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonGetKey = new System.Windows.Forms.Button();
            this.ButtonSaveKey = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextSteamKey
            // 
            resources.ApplyResources(this.TextSteamKey, "TextSteamKey");
            this.TextSteamKey.Name = "TextSteamKey";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ButtonGetKey
            // 
            resources.ApplyResources(this.ButtonGetKey, "ButtonGetKey");
            this.ButtonGetKey.Name = "ButtonGetKey";
            this.ButtonGetKey.UseVisualStyleBackColor = true;
            this.ButtonGetKey.Click += new System.EventHandler(this.ButtonGetKey_Click);
            // 
            // ButtonSaveKey
            // 
            resources.ApplyResources(this.ButtonSaveKey, "ButtonSaveKey");
            this.ButtonSaveKey.Name = "ButtonSaveKey";
            this.ButtonSaveKey.UseVisualStyleBackColor = true;
            this.ButtonSaveKey.Click += new System.EventHandler(this.ButtonSaveKey_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.ButtonCancel, "ButtonCancel");
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // SteamKeyDialog
            // 
            this.AcceptButton = this.ButtonSaveKey;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ControlBox = false;
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSaveKey);
            this.Controls.Add(this.ButtonGetKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextSteamKey);
            this.Name = "SteamKeyDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextSteamKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonGetKey;
        private System.Windows.Forms.Button ButtonSaveKey;
        private System.Windows.Forms.Button ButtonCancel;
    }
}