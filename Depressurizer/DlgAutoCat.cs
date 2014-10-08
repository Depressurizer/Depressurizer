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

        public DlgAutoCat( List<AutoCat> autoCats, GameList ownedGames ) {
            InitializeComponent();

            AutoCatList = new List<AutoCat>();

            foreach( AutoCat c in autoCats ) {
                AutoCatList.Add( c.Clone() );
            }

            this.ownedGames = ownedGames;
        }

        #region UI Uptaters

        private void FillGenreList() {
            genre_lstIgnore.Items.Clear();
            if( Program.GameDB != null ) {
                SortedSet<string> genreList = Program.GameDB.GetAllGenres();
                foreach( string s in genreList ) {
                    genre_lstIgnore.Items.Add( s );
                }
            }
        }

        private void FillFlagList() {
            flags_lstIncluded.Items.Clear();
            if( Program.GameDB != null ) {
                SortedSet<string> flagList = Program.GameDB.GetAllStoreFlags();
                foreach( string s in flagList ) {
                    flags_lstIncluded.Items.Add( s );
                }
            }
        }

        private void FillAutocatList() {
            lstAutoCats.Items.Clear();
            foreach( AutoCat ac in AutoCatList ) {
                lstAutoCats.Items.Add( ac );
            }
        }

        private void FillTagsList( ICollection<string> preChecked = null ) {

            IEnumerable<Tuple<string, float>> tagList = Program.GameDB.CalculateSortedTagList(
                tags_list_chkOwnedOnly.Checked ? ownedGames : null,
                (float)tags_list_numWeightFactor.Value,
                (int)tags_list_numMinScore.Value, (int)tags_list_numTagsPerGame.Value, tags_list_chkExcludeGenres.Checked, tags_list_chkScoreSort.Checked );
            tags_lstIncluded.BeginUpdate();
            tags_lstIncluded.Items.Clear();
            foreach( Tuple<string, float> tag in tagList ) {
                ListViewItem newItem = new ListViewItem( string.Format( "{0} [{1:F0}]", tag.Item1, tag.Item2 ) );
                newItem.Tag = tag.Item1;
                if( preChecked != null && preChecked.Contains( tag.Item1 ) ) newItem.Checked = true;
                tags_lstIncluded.Items.Add( newItem );
            }
            tags_lstIncluded.EndUpdate();
        }

        private void UpdateTypePanelStates() {
            // This is terrible and needs to be replaced with something that looks up the right panels in a list or something like that.
            if( current is AutoCatGenre ) {
                panGenre.BringToFront();
                panFlags.Enabled = panFlags.Visible = false;
                panGenre.Enabled = panGenre.Visible = true;
                panTags.Enabled = panTags.Visible = false;
            } else if( current is AutoCatFlags ) {
                panFlags.BringToFront();
                panFlags.Enabled = panFlags.Visible = true;
                panGenre.Enabled = panGenre.Visible = false;
                panTags.Enabled = panTags.Visible = false;
            } else if( current is AutoCatTags ) {
                panFlags.BringToFront();
                panFlags.Enabled = panFlags.Visible = false;
                panGenre.Enabled = panGenre.Visible = false;
                panTags.Enabled = panTags.Visible = true;
            } else {
                panFlags.Enabled = panFlags.Visible = false;
                panGenre.Enabled = panGenre.Visible = false;
                panTags.Enabled = panTags.Visible = false;
            }
        }

        private void FillSettingsUI() {
            if( current == null ) return;
            if( current is AutoCatGenre ) {
                FillSettingsUIForGenre( current as AutoCatGenre );
            } else if( current is AutoCatFlags ) {
                FillSettingsUIForFlags( current as AutoCatFlags );
            } else if( current is AutoCatTags ) {
                FillSettingsUIForTags( current as AutoCatTags );
            }
        }

        private void FillSettingsUIForGenre( AutoCatGenre ac ) {
            if( ac == null ) return;
            genre_chkRemoveExisting.Checked = ac.RemoveOtherGenres;
            genre_numMaxCats.Value = ac.MaxCategories;
            genre_txtPrefix.Text = ac.Prefix;

            foreach( ListViewItem item in genre_lstIgnore.Items ) {
                item.Checked = ac.IgnoredGenres.Contains( item.Text );
            }
        }

        private void FillSettingsUIForFlags( AutoCatFlags ac ) {
            if( ac == null ) return;
            flags_txtPrefix.Text = ac.Prefix;

            foreach( ListViewItem item in flags_lstIncluded.Items ) {
                item.Checked = ac.IncludedFlags.Contains( item.Text );
            }
        }

        private void FillSettingsUIForTags( AutoCatTags ac ) {
            if( ac == null ) return;
            tags_txtPrefix.Text = ( ac.Prefix == null ) ? string.Empty : ac.Prefix;
            tags_numMaxTags.Value = ac.MaxTags;

            tags_list_numMinScore.Value = ac.ListMinScore;
            tags_list_numTagsPerGame.Value = ac.ListTagsPerGame;
            tags_list_chkOwnedOnly.Checked = ac.ListOwnedOnly;
            tags_list_numWeightFactor.Value = (Decimal)ac.ListWeightFactor;
            tags_list_chkExcludeGenres.Checked = ac.ListExcludeGenres;
            tags_list_chkScoreSort.Checked = ac.ListScoreSort;

            FillTagsList( ac.IncludedTags );
        }

        private void SetAllListCheckStates( ListView list, bool to ) {
            foreach( ListViewItem item in list.Items ) {
                item.Checked = to;
            }
        }
        #endregion

        #region Data modifiers
        private void SaveToAutoCat() {
            if( current is AutoCatGenre ) {
                SaveToAutoCat_Genre( current as AutoCatGenre );
            } else if( current is AutoCatFlags ) {
                SaveToAutoCat_Flags( current as AutoCatFlags );
            } else if( current is AutoCatTags ) {
                SaveToAutoCat_Tags( current as AutoCatTags );
            }
        }

        private void SaveToAutoCat_Genre( AutoCatGenre ac ) {
            ac.Prefix = genre_txtPrefix.Text;
            ac.MaxCategories = (int)genre_numMaxCats.Value;
            ac.RemoveOtherGenres = genre_chkRemoveExisting.Checked;

            ac.IgnoredGenres.Clear();
            foreach( ListViewItem i in genre_lstIgnore.Items ) {
                if( i.Checked ) {
                    ac.IgnoredGenres.Add( i.Text );
                }
            }
        }

        private void SaveToAutoCat_Flags( AutoCatFlags ac ) {
            ac.Prefix = flags_txtPrefix.Text;

            ac.IncludedFlags.Clear();
            foreach( ListViewItem i in flags_lstIncluded.Items ) {
                if( i.Checked ) {
                    ac.IncludedFlags.Add( i.Text );
                }
            }
        }

        private void SaveToAutoCat_Tags( AutoCatTags ac ) {
            ac.Prefix = tags_txtPrefix.Text;

            ac.MaxTags = (int)tags_numMaxTags.Value;

            ac.IncludedTags = new HashSet<string>();
            foreach( ListViewItem i in tags_lstIncluded.CheckedItems ) {
                 ac.IncludedTags.Add( i.Tag as string );
            }

            ac.ListMinScore = (int)tags_list_numMinScore.Value;
            ac.ListOwnedOnly = tags_list_chkOwnedOnly.Checked;
            ac.ListTagsPerGame = (int)tags_list_numTagsPerGame.Value;
            ac.ListWeightFactor = (float)tags_list_numWeightFactor.Value;
            ac.ListExcludeGenres = tags_list_chkExcludeGenres.Checked;
            ac.ListScoreSort = tags_list_chkScoreSort.Checked;
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
            ttHelp.Ext_SetToolTip( genre_helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );
            ttHelp.Ext_SetToolTip( genre_helpRemoveExisting, GlobalStrings.DlgAutoCat_Help_Genre_RemoveExisting );

            ttHelp.Ext_SetToolTip( flags_helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );

            ttHelp.Ext_SetToolTip( tags_helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );
            ttHelp.Ext_SetToolTip( tags_list_helpMinScore, GlobalStrings.DlgAutoCat_Help_ListMinScore );
            ttHelp.Ext_SetToolTip( tags_list_helpOwnedOnly, GlobalStrings.DlgAutoCat_Help_ListOwnedOnly );
            ttHelp.Ext_SetToolTip( tags_list_helpTagsPerGame, GlobalStrings.DlgAutoCat_Help_ListTagsPerGame );
            ttHelp.Ext_SetToolTip( tags_list_helpWeightFactor, GlobalStrings.DlgAutoCat_Help_ListWeightFactor );
            ttHelp.Ext_SetToolTip( tags_list_helpScoreSort, GlobalStrings.DlgAutoCat_Help_ListScoreSort );
            ttHelp.Ext_SetToolTip( tags_list_helpExcludeGenres, GlobalStrings.DlgAutoCat_Help_ListExcludeGenres );

            FillAutocatList();

            FillGenreList();
            FillFlagList();

            UpdateTypePanelStates();
        }

        private void lstAutoCats_SelectedIndexChanged( object sender, EventArgs e ) {
            if( this.current != null ) {
                SaveToAutoCat();
            }
            current = lstAutoCats.SelectedItem as AutoCat;
            UpdateTypePanelStates();
            FillSettingsUI();
        }

        private void cmdSave_Click( object sender, EventArgs e ) {
            SaveToAutoCat();
        }

        private void genreCmdCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( genre_lstIgnore, true );
        }

        private void genreCmdUncheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( genre_lstIgnore, false );
        }

        private void flagsCmdCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( flags_lstIncluded, true );
        }

        private void flagsCmdUncheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( flags_lstIncluded, false );
        }

        private void tags_cmdListRebuild_Click( object sender, EventArgs e ) {
            HashSet<string> checkedTags = new HashSet<string>();
            foreach( ListViewItem item in tags_lstIncluded.CheckedItems ) {
                checkedTags.Add( item.Tag as string );
            }
            FillTagsList( checkedTags );
        }

        private void tags_cmdCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( tags_lstIncluded, true );
        }

        private void tags_cmdUncheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( tags_lstIncluded, false );
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
