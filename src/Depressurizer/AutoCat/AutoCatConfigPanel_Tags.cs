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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Tags : AutoCatConfigPanel
    {
        // used to remove unchecked items from the Tags checkedlistbox.
        private Thread workerThread;

        private GameList ownedGames;
        private bool loaded;

        public AutoCatConfigPanel_Tags(GameList ownedGames)
        {
            this.ownedGames = ownedGames;

            InitializeComponent();

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
            ttHelp.Ext_SetToolTip(list_helpMinScore, GlobalStrings.DlgAutoCat_Help_ListMinScore);
            ttHelp.Ext_SetToolTip(list_helpOwnedOnly, GlobalStrings.DlgAutoCat_Help_ListOwnedOnly);
            ttHelp.Ext_SetToolTip(helpTagsPerGame, GlobalStrings.DlgAutoCat_Help_ListTagsPerGame);
            ttHelp.Ext_SetToolTip(helpWeightFactor, GlobalStrings.DlgAutoCat_Help_ListWeightFactor);
            ttHelp.Ext_SetToolTip(helpExcludeGenres, GlobalStrings.DlgAutoCat_Help_ListExcludeGenres);

            clbTags.DisplayMember = "text";

            //Hide count column
            lstIncluded.Columns[1].Width = 0;
        }

        public void FillTagsList(ICollection<string> preChecked = null)
        {
            clbTags.Items.Clear();
            loaded = false;

            lstIncluded.Columns[0].Width = -1;
            IEnumerable<Tuple<string, float>> tagList =
                Program.GameDB.CalculateSortedTagList(
                    list_chkOwnedOnly.Checked ? ownedGames : null,
                    (float) list_numWeightFactor.Value,
                    (int) list_numMinScore.Value, (int) list_numTagsPerGame.Value, list_chkExcludeGenres.Checked,
                    false);
            lstIncluded.BeginUpdate();
            lstIncluded.Items.Clear();
            foreach (Tuple<string, float> tag in tagList)
            {
                ListViewItem newItem = new ListViewItem(string.Format("{0} [{1:F0}]", tag.Item1, tag.Item2));
                newItem.Tag = tag.Item1;
                if ((preChecked != null) && preChecked.Contains(tag.Item1))
                {
                    newItem.Checked = true;
                }
                newItem.SubItems.Add(tag.Item2.ToString());
                lstIncluded.Items.Add(newItem);
            }
            lstIncluded.Columns[0].Width = -1;
            SortTags(1, SortOrder.Descending);
            lstIncluded.EndUpdate();

            cmdListRebuild.Text = "Rebuild List (" + lstIncluded.Items.Count + ")";
            loaded = true;
        }

        public override void LoadFromAutoCat(AutoCat autocat)
        {
            AutoCatTags ac = autocat as AutoCatTags;
            if (ac == null)
            {
                return;
            }

            txtPrefix.Text = (ac.Prefix == null) ? string.Empty : ac.Prefix;
            numMaxTags.Value = ac.MaxTags;

            list_numMinScore.Value = ac.ListMinScore;
            list_numTagsPerGame.Value = ac.ListTagsPerGame;
            list_chkOwnedOnly.Checked = ac.ListOwnedOnly;
            list_numWeightFactor.Value = (Decimal) ac.ListWeightFactor;
            list_chkExcludeGenres.Checked = ac.ListExcludeGenres;

            FillTagsList(ac.IncludedTags);

            loaded = true;
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatTags ac = autocat as AutoCatTags;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;

            ac.MaxTags = (int) numMaxTags.Value;

            ac.IncludedTags = new HashSet<string>();
            foreach (ListViewItem i in lstIncluded.CheckedItems)
            {
                ac.IncludedTags.Add(i.Tag as string);
            }

            ac.ListMinScore = (int) list_numMinScore.Value;
            ac.ListOwnedOnly = list_chkOwnedOnly.Checked;
            ac.ListTagsPerGame = (int) list_numTagsPerGame.Value;
            ac.ListWeightFactor = (float) list_numWeightFactor.Value;
            ac.ListExcludeGenres = list_chkExcludeGenres.Checked;
        }

        private void SetAllListCheckStates(ListView list, bool to)
        {
            foreach (ListViewItem item in list.Items)
            {
                item.Checked = to;
            }
        }

        private void cmdListRebuild_Click(object sender, EventArgs e)
        {
            HashSet<string> checkedTags = new HashSet<string>();
            foreach (ListViewItem item in lstIncluded.CheckedItems)
            {
                checkedTags.Add(item.Tag as string);
            }
            FillTagsList(checkedTags);
        }

        private void cmdCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIncluded, true);
        }

        private void cmdUncheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIncluded, false);
        }

        private void clbTags_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem) clbTags.Items[e.Index]).Checked = false;
            }
        }

        private void lstIncluded_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                clbTags.Items.Add(e.Item, true);
            }
            else if ((!e.Item.Checked) && loaded)
            {
                workerThread = new Thread(TagItemWorker);
                workerThread.Start(e.Item);
            }
            lblIncluded.Text = "Included tags (" + clbTags.Items.Count + "):";
        }

        private void btnTagSelected_Click(object sender, EventArgs e)
        {
            if (splitTags.Panel1Collapsed)
            {
                splitTags.Panel1Collapsed = false;
                btnTagSelected.Text = "<";
            }
            else
            {
                splitTags.Panel1Collapsed = true;
                btnTagSelected.Text = ">";
            }
        }

        private void nameascendingTags_Click(object sender, EventArgs e)
        {
            SortTags(0, SortOrder.Ascending);
        }

        private void namedescendingTags_Click(object sender, EventArgs e)
        {
            SortTags(0, SortOrder.Descending);
        }

        private void countascendingTags_Click(object sender, EventArgs e)
        {
            SortTags(1, SortOrder.Ascending);
        }

        private void countdescendingTags_Click(object sender, EventArgs e)
        {
            SortTags(1, SortOrder.Descending);
        }

        #region Helper Thread 

        delegate void TagItemCallback(ListViewItem obj);

        private void TagItem(ListViewItem obj)
        {
            if (clbTags.InvokeRequired)
            {
                TagItemCallback callback = TagItem;
                Invoke(callback, obj);
            }
            else
            {
                clbTags.Items.Remove(obj);
                lblIncluded.Text = "Included tags (" + clbTags.Items.Count + "):";
            }
        }

        private void TagItemWorker(object obj)
        {
            TagItem((ListViewItem) obj);
        }

        #endregion

        #region Utility

        private void SortTags(int c, SortOrder so)
        {
            // Create a comparer.
            lstIncluded.ListViewItemSorter =
                new ListViewComparer(c, so);

            // Sort.
            lstIncluded.Sort();
        }

        #endregion
    }
}