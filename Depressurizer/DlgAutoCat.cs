/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Depressurizer.AutoCat;
using Depressurizer.Lib;

namespace Depressurizer {
    public partial class DlgAutoCat : Form {
        public List<AutoCat.AutoCat> AutoCatList;
        //public List<Filter> FilterList;
        private GameList ownedGames;
        private AutoCat.AutoCat current;
        private AutoCat.AutoCat initial;
        private string profilePath;

        AutoCatConfigPanel currentConfigPanel;

        public DlgAutoCat( List<AutoCat.AutoCat> autoCats, GameList ownedGames, AutoCat.AutoCat selected, string profile ) {
            InitializeComponent();

            AutoCatList = new List<AutoCat.AutoCat>();

            profilePath = profile;

            foreach (AutoCat.AutoCat c in autoCats) {
                AutoCat.AutoCat clone = c.Clone();
                AutoCatList.Add(clone);
                if (c.Equals(selected))
                {
                    initial = clone;
                }
            }

            this.ownedGames = ownedGames;
        }

        #region UI Updaters

        private void FillAutocatList() {
            lstAutoCats.Items.Clear();
            foreach( AutoCat.AutoCat ac in AutoCatList ) {
                lstAutoCats.Items.Add( ac );
            }
            lstAutoCats.DisplayMember = "DisplayName";
        }

        private void RecreateConfigPanel() {
            if( currentConfigPanel != null ) {
                splitAutoCat.Panel2.Controls.Remove( currentConfigPanel );
            }

            if( current != null ) {
                currentConfigPanel = AutoCatConfigPanel.CreatePanel( current, ownedGames, AutoCatList );
            }
   
            if( currentConfigPanel != null ) {
                currentConfigPanel.Dock = DockStyle.Fill;
                splitAutoCat.Panel2.Controls.Add( currentConfigPanel );
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
            if( current != null && currentConfigPanel != null )
            {
                currentConfigPanel.SaveToAutoCat(current);
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
                if( res == DialogResult.OK ) {
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
            AutoCat.AutoCat newAutoCat = null;
            if ( res == DialogResult.OK ) {
                newAutoCat = AutoCat.AutoCat.Create( t, name );
                if( newAutoCat != null ) {
                    AutoCatList.Add( newAutoCat );
                }
            }
            AutoCatList.Sort();
            FillAutocatList();
            if (newAutoCat != null) lstAutoCats.SelectedItem = newAutoCat;
        }

        private void RenameAutoCat( AutoCat.AutoCat ac ) {
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
            if( res == DialogResult.OK ) {
                ac.Name = name;
            }
            AutoCatList.Sort();
            FillAutocatList();
        }

        private void RemoveAutoCat( AutoCat.AutoCat ac ) {
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

            if (initial != null)
            {
                lstAutoCats.SelectedItem = initial;
            }

        }

        private void lstAutoCats_SelectedIndexChanged( object sender, EventArgs e ) {
            if( current != null ) {
                SaveToAutoCat();
            }
            current = lstAutoCats.SelectedItem as AutoCat.AutoCat;
            RecreateConfigPanel();
            FillConfigPanel();

            if (lstAutoCats.SelectedItem != null)
            {
                btnUp.Enabled = (lstAutoCats.SelectedIndex == 0) ? false : true;
                btnDown.Enabled = (lstAutoCats.SelectedIndex == (lstAutoCats.Items.Count - 1)) ? false : true;
            }
            else
            {
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            Utility.MoveItem(lstAutoCats, -1);
            RepositionAutoCats();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            Utility.MoveItem(lstAutoCats, 1);
            RepositionAutoCats();
        }

        private void cmdSave_Click( object sender, EventArgs e ) {
            SaveToAutoCat();
        }

        private void cmdCreate_Click( object sender, EventArgs e ) {
            CreateNewAutoCat();
        }

        private void cmdDelete_Click( object sender, EventArgs e ) {
            int selectedIndex = lstAutoCats.SelectedIndex;
            RemoveAutoCat( lstAutoCats.SelectedItem as AutoCat.AutoCat );
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
            RenameAutoCat( lstAutoCats.SelectedItem as AutoCat.AutoCat );
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

        private void RepositionAutoCats()
        {
            AutoCatList.Clear();
            foreach (AutoCat.AutoCat ac in lstAutoCats.Items)
            {
                AutoCatList.Add(ac);
            }
        }


        private bool NameExists( string name ) {
            foreach( AutoCat.AutoCat ac in AutoCatList ) {
                if( ac.Name == name ) return true;
            }
            return false;
        }

        #endregion

    }
}
