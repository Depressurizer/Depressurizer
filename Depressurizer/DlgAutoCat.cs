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
        private GameList ownedGames;
        AutoCat current;

        UserControl currentConfigPanel = null;

        public DlgAutoCat( List<AutoCat> autoCats, GameList ownedGames ) {
            InitializeComponent();

            AutoCatList = new List<AutoCat>();

            foreach( AutoCat c in autoCats ) {
                AutoCatList.Add( c.Clone() );
            }

            this.ownedGames = ownedGames;
        }

        #region UI Uptaters

        private void FillAutocatList() {
            lstAutoCats.Items.Clear();
            foreach( AutoCat ac in AutoCatList ) {
                lstAutoCats.Items.Add( ac );
            }
        }

        private void RecreateConfigPanel() {
            if( currentConfigPanel != null ) {
                this.splitContainer.Panel2.Controls.Remove( currentConfigPanel );
            }

            bool added = false;
            if( current is AutoCatGenre ) {
                currentConfigPanel = new AutoCatConfigPanel_Genre(ttHelp);
                added = true;
            } else if( current is AutoCatFlags ) {
                currentConfigPanel = new AutoCatConfigPanel_Flags(ttHelp);
                added = true;
            } else if( current is AutoCatTags ) {
                currentConfigPanel = new AutoCatConfigPanel_Tags(ttHelp, ownedGames);
                added = true;
            }

            if( added ) {
                currentConfigPanel.Dock = DockStyle.Fill;
                this.splitContainer.Panel2.Controls.Add( currentConfigPanel );
            }
        }

        private void FillConfigPanel() {
            if( current == null ) return;
            if( current is AutoCatGenre ) {
                AutoCatConfigPanel_Genre genrePanel = currentConfigPanel as AutoCatConfigPanel_Genre;
                if( genrePanel != null ) {
                    genrePanel.FillSettings( current as AutoCatGenre );
                }
            } else if( current is AutoCatFlags ) {
                AutoCatConfigPanel_Flags flagsPanel = currentConfigPanel as AutoCatConfigPanel_Flags;
                if( flagsPanel != null ) {
                    flagsPanel.FillSettings( current as AutoCatFlags );
                }
            } else if( current is AutoCatTags ) {
                AutoCatConfigPanel_Tags tagsPanel = currentConfigPanel as AutoCatConfigPanel_Tags;
                if( tagsPanel != null ) {
                    tagsPanel.FillSettings( current as AutoCatTags );
                }
            }
        }

        #endregion

        #region Data modifiers
        private void SaveToAutoCat() {
            if( current is AutoCatGenre ) {
                AutoCatConfigPanel_Genre genrePanel = currentConfigPanel as AutoCatConfigPanel_Genre;
                if( genrePanel != null ) {
                    genrePanel.SaveToAutoCat( current as AutoCatGenre );
                };
            } else if( current is AutoCatFlags ) {
                AutoCatConfigPanel_Flags flagsPanel = currentConfigPanel as AutoCatConfigPanel_Flags;
                if( flagsPanel != null ) {
                    flagsPanel.SaveToAutoCat( current as AutoCatFlags );
                };
            } else if( current is AutoCatTags ) {
                AutoCatConfigPanel_Tags tagsPanel = currentConfigPanel as AutoCatConfigPanel_Tags;
                if( tagsPanel != null ) {
                    tagsPanel.SaveToAutoCat( current as AutoCatTags );
                }
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
            if( res == DialogResult.OK ) {
                AutoCat newAutoCat = AutoCat.Create( t, name );
                if( newAutoCat != null ) {
                    AutoCatList.Add( newAutoCat );
                }
            }
            AutoCatList.Sort();
            FillAutocatList();
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

        private void DlgAutoCat_Load( object sender, EventArgs e ) {
            FillAutocatList();

            RecreateConfigPanel();
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
            RemoveAutoCat( lstAutoCats.SelectedItem as AutoCat );
        }

        private void cmdRename_Click( object sender, EventArgs e ) {
            RenameAutoCat( lstAutoCats.SelectedItem as AutoCat );
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
