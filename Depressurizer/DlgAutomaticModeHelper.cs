using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace Depressurizer {
    public partial class DlgAutomaticModeHelper : Form {
        private AutomaticModeOptions defaultOpts = new AutomaticModeOptions();
        Profile profile;

        public DlgAutomaticModeHelper( Profile profile ) {
            InitializeComponent();
            this.profile = profile;
            ttHelp.Ext_SetToolTip( hlpExport, GlobalStrings.AutoMode_Help_Export );
            ttHelp.Ext_SetToolTip( hlpImportCats, GlobalStrings.AutoMode_Help_ImportCats );
            ttHelp.Ext_SetToolTip( hlpLaunch, GlobalStrings.AutoMode_Help_Launch );
            ttHelp.Ext_SetToolTip( hlpOutput, GlobalStrings.AutoMode_Help_Output );
            ttHelp.Ext_SetToolTip( hlpSaveDB, GlobalStrings.AutoMode_Help_SaveDB );
            ttHelp.Ext_SetToolTip( hlpSaveProfile, GlobalStrings.AutoMode_Help_SaveProfile );
            ttHelp.Ext_SetToolTip( hlpSteamCheck, GlobalStrings.AutoMode_Help_SteamCheck );
            ttHelp.Ext_SetToolTip( hlpTolerant, GlobalStrings.AutoMode_Help_Tolerant );
            ttHelp.Ext_SetToolTip( hlpUpdateAppInfo, GlobalStrings.AutoMode_Help_UpdateAppInfo );
            ttHelp.Ext_SetToolTip( hlpUpdateLib, GlobalStrings.AutoMode_Help_UpdateLib );
            ttHelp.Ext_SetToolTip( hlpUpdateWeb, GlobalStrings.AutoMode_Help_UpdateWeb );
            ttHelp.Ext_SetToolTip(hlpUpdateHltb, GlobalStrings.AutoMode_Help_UpdateHltb);
        }

        private string GenerateCommand() {
            return '"' + Application.ExecutablePath + '"' + GenerateArguments();
        }

        private string GenerateArguments() {
            StringBuilder sb = new StringBuilder();
            sb.Append( " -auto -p \"" );

            sb.Append( profile.FilePath );
            sb.Append( '"' );


            switch( cmbSteamCheck.SelectedIndex ) {
                case 0: // Check and close
                    if( !defaultOpts.CheckSteam ) sb.Append( " -checksteam+" );
                    if( !defaultOpts.CloseSteam ) sb.Append( " -closesteam+" );
                    break;
                case 1: // Check and abort
                    if( !defaultOpts.CheckSteam ) sb.Append( " -checksteam+" );
                    if( defaultOpts.CloseSteam ) sb.Append( " -closesteam-" );
                    break;
                case 2: // skip
                    if( defaultOpts.CheckSteam ) sb.Append( " -checksteam-" );
                    break;
            }

            sb.Append( GetSwitch( "-updatelib", chkUpdateLib.Checked, defaultOpts.UpdateGameList ) );

            sb.Append( GetSwitch( "-import", chkImportCats.Checked, defaultOpts.ImportSteamCategories ) );

            sb.Append( GetSwitch( "-updatedblocal", chkUpdateAppInfo.Checked, defaultOpts.UpdateAppInfo ) );

            sb.Append( GetSwitch( "-updatedbweb", chkUpdateWeb.Checked, defaultOpts.ScrapeUnscrapedGames ) );

            sb.Append(GetSwitch("-updatedbhltb", chkUpdateHltb.Checked, defaultOpts.UpdateHltb));

            sb.Append( GetSwitch( "-savedb", chkSaveDB.Checked, defaultOpts.SaveDBChanges ) );

            sb.Append( GetSwitch( "-saveprofile", chkSaveProfile.Checked, defaultOpts.SaveProfile ) );

            sb.Append( GetSwitch( "-export", chkExport.Checked, defaultOpts.ExportToSteam ) );

            sb.Append( GetSwitch( "-tolerant", chkTolerant.Checked, defaultOpts.TolerateMinorErrors ) );

            switch( cmbLaunch.SelectedIndex ) {
                case 0:
                    break;
                case 1:
                    sb.Append( " -launch" );
                    break;
                case 2:
                    sb.Append( " -launchbp" );
                    break;
            }

            switch( cmbOutputMode.SelectedIndex ) {
                case 0:
                    break;
                case 1:
                    sb.Append( " -quiet" );
                    break;
                case 2:
                    sb.Append( " -silent" );
                    break;
            }

            if( chkAllAutocats.Checked ) {
                sb.Append( " -all" );
            } else {
                foreach( ListViewItem i in lstAutocats.CheckedItems ) {
                    sb.Append( " \"" );
                    sb.Append( i.Text );
                    sb.Append( '"' );
                }
            }

            return sb.ToString();
        }

        private string GetSwitch( string name, bool val, bool defVal ) {
            if( val != defVal ) {
                return " " + name + GetToggle( val );
            }
            return "";
        }

        private string GetToggle( bool val ) {
            return val ? "+" : "-";
        }

        private void CreateShortcut() {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.Desktop );
            dlg.DefaultExt = "lnk";
            dlg.AddExtension = true;
            dlg.Filter = "Shortcuts|*.lnk";
            dlg.FileName = "Depressurizer Auto";

            DialogResult res = dlg.ShowDialog();
            if( res == System.Windows.Forms.DialogResult.OK ) {
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut( dlg.FileName );
                    shortcut.TargetPath = Application.ExecutablePath;
                    shortcut.WorkingDirectory = Application.StartupPath;
                    shortcut.Arguments = GenerateArguments();
                    shortcut.Save();
            }
        }

        private void cmdShortcut_Click( object sender, EventArgs e ) {
            CreateShortcut();
        }

        private void ItemChanged( object sender, EventArgs e ) {
            txtResult.Text = GenerateCommand();
        }

        private void lstAutocats_ItemChecked( object sender, ItemCheckedEventArgs e ) {
            txtResult.Text = GenerateCommand();
        }

        private void DlgAutomaticModeHelper_Load( object sender, EventArgs e ) {
            cmbSteamCheck.SelectedIndex = 0;
            cmbOutputMode.SelectedIndex = 0;
            cmbLaunch.SelectedIndex = 0;

            txtResult.Text = GenerateCommand();

            if( profile != null && profile.AutoCats != null ) {
                foreach( AutoCat ac in profile.AutoCats ) {
                    lstAutocats.Items.Add( ac.Name );
                }
            }
        }
    }
}
