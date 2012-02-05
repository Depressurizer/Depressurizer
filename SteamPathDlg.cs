using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Depressurizer {
    public partial class SteamPathDlg : Form {
        public string Path {
            get {
                return txtPath.Text.Trim().TrimEnd( new char[] { '\\' } );
            }
        }
        
        public SteamPathDlg() {
            InitializeComponent();
            txtPath.Text = GetSteamPath();
        }

        private void cmdOk_Click( object sender, EventArgs e ) {
            if( !Directory.Exists( Path ) ) {
                DialogResult res = MessageBox.Show( "That path does not exist. Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2 );
                if( res == System.Windows.Forms.DialogResult.No ) {
                    return;
                }
            }
            this.Close();
        }

        private void cmdBrowse_Click( object sender, EventArgs e ) {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                txtPath.Text = dlg.SelectedPath;
            }
        }

        private string GetSteamPath() {
            try {
                string s = Registry.GetValue( @"HKEY_CURRENT_USER\Software\Valve\Steam", "steamPath", null ) as string;
                if( s == null ) s = string.Empty;
                return s.Replace( '/', '\\' );
            } catch {
                return string.Empty;
            }
        }
    }
}
