using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Depressurizer.Lib;

namespace Depressurizer
{
    partial class AutoCatConfigPanel_Flags
    {
        #region Fields

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly IContainer components = null;

        private Button cmdCheckAll;

        private Button cmdUncheckAll;

        private GroupBox grpMain;

        private Label helpPrefix;

        private Label lblInclude;

        private Label lblPrefix;

        private ListView lstIncluded;

        private TableLayoutPanel tblButtons;

        private ExtToolTip ttHelp;

        private TextBox txtPrefix;

        #endregion

        #region Methods

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpMain = new GroupBox();
            helpPrefix = new Label();
            tblButtons = new TableLayoutPanel();
            cmdCheckAll = new Button();
            cmdUncheckAll = new Button();
            lblInclude = new Label();
            lstIncluded = new ListView();
            txtPrefix = new TextBox();
            lblPrefix = new Label();
            ttHelp = new ExtToolTip();
            grpMain.SuspendLayout();
            tblButtons.SuspendLayout();
            SuspendLayout();
            // 
            // grpMain
            // 
            grpMain.Controls.Add(helpPrefix);
            grpMain.Controls.Add(tblButtons);
            grpMain.Controls.Add(lblInclude);
            grpMain.Controls.Add(lstIncluded);
            grpMain.Controls.Add(txtPrefix);
            grpMain.Controls.Add(lblPrefix);
            grpMain.Dock = DockStyle.Fill;
            grpMain.Location = new Point(0, 0);
            grpMain.Name = "grpMain";
            grpMain.Size = new Size(576, 460);
            grpMain.TabIndex = 0;
            grpMain.TabStop = false;
            grpMain.Text = "Edit Flag Autocat";
            // 
            // helpPrefix
            // 
            helpPrefix.AutoSize = true;
            helpPrefix.BorderStyle = BorderStyle.FixedSingle;
            helpPrefix.Location = new Point(238, 22);
            helpPrefix.Name = "helpPrefix";
            helpPrefix.Size = new Size(15, 15);
            helpPrefix.TabIndex = 2;
            helpPrefix.Text = "?";
            // 
            // tblButtons
            // 
            tblButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tblButtons.ColumnCount = 2;
            tblButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tblButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tblButtons.Controls.Add(cmdCheckAll, 0, 0);
            tblButtons.Controls.Add(cmdUncheckAll, 1, 0);
            tblButtons.Location = new Point(3, 426);
            tblButtons.Name = "tblButtons";
            tblButtons.RowCount = 1;
            tblButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblButtons.Size = new Size(570, 30);
            tblButtons.TabIndex = 5;
            // 
            // cmdCheckAll
            // 
            cmdCheckAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmdCheckAll.Location = new Point(3, 3);
            cmdCheckAll.Name = "cmdCheckAll";
            cmdCheckAll.Size = new Size(279, 23);
            cmdCheckAll.TabIndex = 0;
            cmdCheckAll.Text = "Check All";
            cmdCheckAll.UseVisualStyleBackColor = true;
            cmdCheckAll.Click += cmdCheckAll_Click;
            // 
            // cmdUncheckAll
            // 
            cmdUncheckAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmdUncheckAll.Location = new Point(288, 3);
            cmdUncheckAll.Name = "cmdUncheckAll";
            cmdUncheckAll.Size = new Size(279, 23);
            cmdUncheckAll.TabIndex = 1;
            cmdUncheckAll.Text = "Uncheck All";
            cmdUncheckAll.UseVisualStyleBackColor = true;
            cmdUncheckAll.Click += cmdUncheckAll_Click;
            // 
            // lblInclude
            // 
            lblInclude.AutoSize = true;
            lblInclude.Location = new Point(3, 69);
            lblInclude.Name = "lblInclude";
            lblInclude.Size = new Size(79, 13);
            lblInclude.TabIndex = 3;
            lblInclude.Text = "Included Flags:";
            // 
            // lstIncluded
            // 
            lstIncluded.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstIncluded.CheckBoxes = true;
            lstIncluded.Location = new Point(6, 85);
            lstIncluded.Name = "lstIncluded";
            lstIncluded.Size = new Size(564, 339);
            lstIncluded.TabIndex = 4;
            lstIncluded.UseCompatibleStateImageBehavior = false;
            lstIncluded.View = View.List;
            // 
            // txtPrefix
            // 
            txtPrefix.Location = new Point(67, 19);
            txtPrefix.Name = "txtPrefix";
            txtPrefix.Size = new Size(165, 20);
            txtPrefix.TabIndex = 1;
            // 
            // lblPrefix
            // 
            lblPrefix.AutoSize = true;
            lblPrefix.Location = new Point(25, 22);
            lblPrefix.Name = "lblPrefix";
            lblPrefix.Size = new Size(36, 13);
            lblPrefix.TabIndex = 0;
            lblPrefix.Text = "Prefix:";
            // 
            // AutoCatConfigPanel_Flags
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(grpMain);
            Name = "AutoCatConfigPanel_Flags";
            Size = new Size(576, 460);
            grpMain.ResumeLayout(false);
            grpMain.PerformLayout();
            tblButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}