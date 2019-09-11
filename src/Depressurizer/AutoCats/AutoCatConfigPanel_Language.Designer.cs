namespace Depressurizer.AutoCats
{
    partial class AutoCatConfigPanel_Language
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCatConfigPanel_Language));
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.chkTypeFallback = new System.Windows.Forms.CheckBox();
            this.chkIncludeTypePrefix = new System.Windows.Forms.CheckBox();
            this.helpPrefix = new System.Windows.Forms.Label();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.cmdUncheckAll = new System.Windows.Forms.Button();
            this.cmdCheckAll = new System.Windows.Forms.Button();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.tblVrFlags = new System.Windows.Forms.TableLayoutPanel();
            this.grpFullAudio = new System.Windows.Forms.GroupBox();
            this.lstFullAudio = new System.Windows.Forms.ListView();
            this.grpInterface = new System.Windows.Forms.GroupBox();
            this.lstInterface = new System.Windows.Forms.ListView();
            this.grpSubtitles = new System.Windows.Forms.GroupBox();
            this.lstSubtitles = new System.Windows.Forms.ListView();
            this.ttHelp = new Depressurizer.Lib.ExtToolTip();
            this.grpMain.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.tblVrFlags.SuspendLayout();
            this.grpFullAudio.SuspendLayout();
            this.grpInterface.SuspendLayout();
            this.grpSubtitles.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.chkTypeFallback);
            this.grpMain.Controls.Add(this.chkIncludeTypePrefix);
            this.grpMain.Controls.Add(this.helpPrefix);
            this.grpMain.Controls.Add(this.tblButtons);
            this.grpMain.Controls.Add(this.txtPrefix);
            this.grpMain.Controls.Add(this.lblPrefix);
            this.grpMain.Controls.Add(this.tblVrFlags);
            resources.ApplyResources(this.grpMain, "grpMain");
            this.grpMain.Name = "grpMain";
            this.grpMain.TabStop = false;
            // 
            // chkTypeFallback
            // 
            resources.ApplyResources(this.chkTypeFallback, "chkTypeFallback");
            this.chkTypeFallback.Name = "chkTypeFallback";
            this.chkTypeFallback.UseVisualStyleBackColor = true;
            // 
            // chkIncludeTypePrefix
            // 
            resources.ApplyResources(this.chkIncludeTypePrefix, "chkIncludeTypePrefix");
            this.chkIncludeTypePrefix.Name = "chkIncludeTypePrefix";
            this.chkIncludeTypePrefix.UseVisualStyleBackColor = true;
            // 
            // helpPrefix
            // 
            resources.ApplyResources(this.helpPrefix, "helpPrefix");
            this.helpPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.helpPrefix.Name = "helpPrefix";
            // 
            // tblButtons
            // 
            resources.ApplyResources(this.tblButtons, "tblButtons");
            this.tblButtons.Controls.Add(this.cmdUncheckAll, 1, 0);
            this.tblButtons.Controls.Add(this.cmdCheckAll, 0, 0);
            this.tblButtons.Name = "tblButtons";
            // 
            // cmdUncheckAll
            // 
            resources.ApplyResources(this.cmdUncheckAll, "cmdUncheckAll");
            this.cmdUncheckAll.Name = "cmdUncheckAll";
            this.cmdUncheckAll.UseVisualStyleBackColor = true;
            this.cmdUncheckAll.Click += new System.EventHandler(this.cmdUncheckAll_Click);
            // 
            // cmdCheckAll
            // 
            resources.ApplyResources(this.cmdCheckAll, "cmdCheckAll");
            this.cmdCheckAll.Name = "cmdCheckAll";
            this.cmdCheckAll.UseVisualStyleBackColor = true;
            this.cmdCheckAll.Click += new System.EventHandler(this.cmdCheckAll_Click);
            // 
            // txtPrefix
            // 
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            // 
            // lblPrefix
            // 
            resources.ApplyResources(this.lblPrefix, "lblPrefix");
            this.lblPrefix.Name = "lblPrefix";
            // 
            // tblVrFlags
            // 
            resources.ApplyResources(this.tblVrFlags, "tblVrFlags");
            this.tblVrFlags.Controls.Add(this.grpFullAudio, 2, 0);
            this.tblVrFlags.Controls.Add(this.grpInterface, 0, 0);
            this.tblVrFlags.Controls.Add(this.grpSubtitles, 1, 0);
            this.tblVrFlags.Name = "tblVrFlags";
            // 
            // grpFullAudio
            // 
            this.grpFullAudio.Controls.Add(this.lstFullAudio);
            resources.ApplyResources(this.grpFullAudio, "grpFullAudio");
            this.grpFullAudio.Name = "grpFullAudio";
            this.grpFullAudio.TabStop = false;
            // 
            // lstFullAudio
            // 
            this.lstFullAudio.CheckBoxes = true;
            resources.ApplyResources(this.lstFullAudio, "lstFullAudio");
            this.lstFullAudio.HideSelection = false;
            this.lstFullAudio.Name = "lstFullAudio";
            this.lstFullAudio.UseCompatibleStateImageBehavior = false;
            this.lstFullAudio.View = System.Windows.Forms.View.List;
            // 
            // grpInterface
            // 
            this.grpInterface.Controls.Add(this.lstInterface);
            resources.ApplyResources(this.grpInterface, "grpInterface");
            this.grpInterface.Name = "grpInterface";
            this.grpInterface.TabStop = false;
            // 
            // lstInterface
            // 
            this.lstInterface.CheckBoxes = true;
            resources.ApplyResources(this.lstInterface, "lstInterface");
            this.lstInterface.HideSelection = false;
            this.lstInterface.Name = "lstInterface";
            this.lstInterface.UseCompatibleStateImageBehavior = false;
            this.lstInterface.View = System.Windows.Forms.View.List;
            // 
            // grpSubtitles
            // 
            this.grpSubtitles.Controls.Add(this.lstSubtitles);
            resources.ApplyResources(this.grpSubtitles, "grpSubtitles");
            this.grpSubtitles.Name = "grpSubtitles";
            this.grpSubtitles.TabStop = false;
            // 
            // lstSubtitles
            // 
            this.lstSubtitles.CheckBoxes = true;
            resources.ApplyResources(this.lstSubtitles, "lstSubtitles");
            this.lstSubtitles.HideSelection = false;
            this.lstSubtitles.Name = "lstSubtitles";
            this.lstSubtitles.UseCompatibleStateImageBehavior = false;
            this.lstSubtitles.View = System.Windows.Forms.View.List;
            // 
            // AutoCatConfigPanel_Language
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpMain);
            this.Name = "AutoCatConfigPanel_Language";
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            this.tblButtons.ResumeLayout(false);
            this.tblVrFlags.ResumeLayout(false);
            this.grpFullAudio.ResumeLayout(false);
            this.grpInterface.ResumeLayout(false);
            this.grpSubtitles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.Label helpPrefix;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button cmdCheckAll;
        private System.Windows.Forms.Button cmdUncheckAll;
        private System.Windows.Forms.ListView lstInterface;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblPrefix;
        private Lib.ExtToolTip ttHelp;
        private System.Windows.Forms.ListView lstFullAudio;
        private System.Windows.Forms.ListView lstSubtitles;
        private System.Windows.Forms.GroupBox grpInterface;
        private System.Windows.Forms.GroupBox grpSubtitles;
        private System.Windows.Forms.GroupBox grpFullAudio;
        private System.Windows.Forms.TableLayoutPanel tblVrFlags;
        private System.Windows.Forms.CheckBox chkIncludeTypePrefix;
        private System.Windows.Forms.CheckBox chkTypeFallback;
    }
}
