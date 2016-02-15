/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

Depressurizer is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Depressurizer is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/
using Rallion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Depressurizer {
    public partial class DlgAutoCat : Form {
        public List<AutoCat> AutoCatList;
        //public List<Filter> FilterList;
        private GameList ownedGames;
        AutoCat current;
        AutoCat initial;

        AutoCatConfigPanel currentConfigPanel = null;

        public DlgAutoCat( List<AutoCat> autoCats, GameList ownedGames, AutoCat selected ) {
            InitializeComponent();

            AutoCatList = new List<AutoCat>();

            foreach (AutoCat c in autoCats) {
                AutoCat clone = c.Clone();
                AutoCatList.Add(clone);
                if (c.Equals(selected))
                {
                    initial = clone;
                }
            }

            this.ownedGames = ownedGames;
        }

        #region UI Uptaters

        private void FillAutocatList() {
            lstAutoCats.Items.Clear();
            foreach( AutoCat ac in AutoCatList ) {
                lstAutoCats.Items.Add( ac );
            }
            lstAutoCats.DisplayMember = "DisplayName";
        }

        private void RecreateConfigPanel() {
            if( currentConfigPanel != null ) {
                this.splitAutoCat.Panel2.Controls.Remove( currentConfigPanel );
            }

            if( current != null ) {
                currentConfigPanel = AutoCatConfigPanel.CreatePanel( current, ownedGames );
            }
   
            if( currentConfigPanel != null ) {
                currentConfigPanel.Dock = DockStyle.Fill;
                this.splitAutoCat.Panel2.Controls.Add( currentConfigPanel );
            }
        }

        private void FillConfigPanel() {
            if( current != null && currentConfigPanel != null ) {
                currentConfigPanel.LoadFromAutoCat( current );
                if (current.Filter != null)
                {
                    chkFilter.Checked = true;
                    cboFilter.Text = current.Filter;
                }
                else
                {
                    chkFilter.Checked = false;
                }
            }
        }

        private void FillFilterList()
        {
            cboFilter.DataSource = null;
            cboFilter.DataSource = ownedGames.Filters;
            cboFilter.ValueMember = null;
            cboFilter.DisplayMember = "Name";
            cboFilter.Text = "";
        }

        #endregion

        #region Data modifiers
        private void SaveToAutoCat() {
            if( current != null && currentConfigPanel != null ) {
                currentConfigPanel.SaveToAutoCat( current );
                if (chkFilter.Checked && cboFilter.Text != string.Empty) current.Filter = cboFilter.Text;
                else current.Filter = null;
            }
        }

        private void CreateNewAutoCat() {
            string name = string.Empty;
            AutoCatType t = AutoCatType.None;
            bool good = true;
            DialogResult res;
            do {
                DlgAutoCatCreate dlg = new DlgAutoCatCreate();
                res = dlg.ShowDialog();
                if( res == System.Windows.Forms.DialogResult.OK ) {
                    good = true;
                    name = dlg.SelectedName;
                    t = dlg.SelectedType;
                    if( string.IsNullOrEmpty( name ) ) {
                        MessageBox.Show( GlobalStrings.DlgAutoCat_MustHaveName );
                        good = false;
                    } else if( NameExists( name ) ) {
                        MessageBox.Show( GlobalStrings.DlgAutoCat_NameInUse );
                        good = false;
                    } else if( t == AutoCatType.None ) {
                        MessageBox.Show( GlobalStrings.DlgAutoCat_SelectValidType );
                        good = false;
                    }
                }
            } while( res == DialogResult.OK && !good );
            AutoCat newAutoCat = null;
            if ( res == DialogResult.OK ) {
                newAutoCat = AutoCat.Create( t, name );
                if( newAutoCat != null ) {
                    AutoCatList.Add( newAutoCat );
                }
            }
            AutoCatList.Sort();
            FillAutocatList();
            if (newAutoCat != null) lstAutoCats.SelectedItem = newAutoCat;
        }

        private void RenameAutoCat( AutoCat ac ) {
            if( ac == null ) return;

            bool good = true;
            DialogResult res;
            string name;

            do {
                GetStringDlg dlg = new GetStringDlg( ac.Name, GlobalStrings.DlgAutoCat_RenameBoxTitle, GlobalStrings.DlgAutoCat_RenameBoxLabel, GlobalStrings.DlgAutoCat_RenameBoxButton );
                res = dlg.ShowDialog();
                name = dlg.Value;
                if( string.IsNullOrEmpty( name ) ) {
                    MessageBox.Show( GlobalStrings.DlgAutoCat_MustHaveName );
                    good = false;
                } else if( NameExists( name ) ) {
                    MessageBox.Show( GlobalStrings.DlgAutoCat_NameInUse );
                    good = false;
                }
            } while( res == DialogResult.OK && !good );
            if( res == System.Windows.Forms.DialogResult.OK ) {
                ac.Name = name;
            }
            AutoCatList.Sort();
            FillAutocatList();
        }

        private void RemoveAutoCat( AutoCat ac ) {
            if( ac == null ) return;
            lstAutoCats.Items.Remove( ac );
            AutoCatList.Remove( ac );
        }
        #endregion

        #region Event Handlers

        private void DlgAutoCat_Load(object sender, EventArgs e)
        {
            FillAutocatList();
            RecreateConfigPanel();
            FillFilterList();

            if (this.initial != null)
            {
                lstAutoCats.SelectedItem = initial;
            }

        }

        private void lstAutoCats_SelectedIndexChanged( object sender, EventArgs e ) {
            if( this.current != null ) {
                SaveToAutoCat();
            }
            current = lstAutoCats.SelectedItem as AutoCat;
            RecreateConfigPanel();
            FillConfigPanel();
        }

        private void cmdSave_Click( object sender, EventArgs e ) {
            SaveToAutoCat();
        }

        private void cmdCreate_Click( object sender, EventArgs e ) {
            CreateNewAutoCat();
        }

        private void cmdDelete_Click( object sender, EventArgs e ) {
            int selectedIndex = lstAutoCats.SelectedIndex;
            RemoveAutoCat( lstAutoCats.SelectedItem as AutoCat );
            // Select previous item after deleting.
            if (lstAutoCats.Items.Count > 0)
            {
                if (selectedIndex > 0)
                {
                    lstAutoCats.SelectedItem = lstAutoCats.Items[selectedIndex - 1];
                }
                else
                {
                    lstAutoCats.SelectedItem = lstAutoCats.Items[selectedIndex];
                }
            }
        }

        private void cmdRename_Click( object sender, EventArgs e ) {
            RenameAutoCat( lstAutoCats.SelectedItem as AutoCat );
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFilter.Checked)
            {
                cboFilter.Enabled = true;
                FillFilterList();
            }
            else cboFilter.Enabled = false;
        }

        #endregion

        #region Utility
        private bool NameExists( string name ) {
            foreach( AutoCat ac in AutoCatList ) {
                if( ac.Name == name ) return true;
            }
            return false;
        }
        #endregion


    }
}
