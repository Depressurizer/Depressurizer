using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Depressurizer {
    public partial class OptionsDlg : Form {
        public OptionsDlg() {
            InitializeComponent();
        }

        private void OptionsForm_Load( object sender, EventArgs e ) {
            FillFieldsFromSettings();
        }

        private void FillFieldsFromSettings() {
            DepSettings settings = DepSettings.Instance();
            txtSteamPath.Text = settings.SteamPath;
            txtDefaultProfile.Text = settings.ProfileToLoad;
            chkLoadProfileOnStart.Checked = settings.LoadProfileOnStartup;
            chkRemoveExtraEntries.Checked = settings.RemoveExtraEntries;
        }

        private void SaveFieldsToSettings() {
            DepSettings settings = DepSettings.Instance();

            settings.SteamPath = txtSteamPath.Text;
            settings.LoadProfileOnStartup = chkLoadProfileOnStart.Checked;
            settings.ProfileToLoad = txtDefaultProfile.Text;
            settings.RemoveExtraEntries = chkRemoveExtraEntries.Checked;
            try {
                settings.Save();
            } catch( Exception e ) {
                MessageBox.Show( "Error saving settings file: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void cmdCancel_Click( object sender, EventArgs e ) {
            this.Close();
        }

        private void cmdAccept_Click( object sender, EventArgs e ) {
            SaveFieldsToSettings();
            this.Close();
        }

        private void cmdSteamPathBrowse_Click( object sender, EventArgs e ) {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                txtSteamPath.Text = dlg.SelectedPath;
            }
        }

        private void cmdDefaultProfileBrowse_Click( object sender, EventArgs e ) {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                txtDefaultProfile.Text = dlg.FileName;
            }
        }
    }
}
