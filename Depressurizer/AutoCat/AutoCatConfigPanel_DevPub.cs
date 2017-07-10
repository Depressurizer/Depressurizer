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
    public partial class AutoCatConfigPanel_DevPub : AutoCatConfigPanel
    {
        // used to remove unchecked items from the Add and Remove checkedlistbox.
        private Thread workerThread;

        private bool loaded;
        private GameList ownedGames;

        public AutoCatConfigPanel_DevPub(GameList g)
        {
            InitializeComponent();

            ownedGames = g;

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
            ttHelp.Ext_SetToolTip(list_helpScore, GlobalStrings.DlgAutoCat_Help_MinScore);
            ttHelp.Ext_SetToolTip(list_helpOwnedOnly, GlobalStrings.DlgAutoCat_Help_ListOwnedOnly);
            ttHelp.Ext_SetToolTip(btnDevSelected, GlobalStrings.DlgAutoCat_Help_DevSelected);
            ttHelp.Ext_SetToolTip(btnPubSelected, GlobalStrings.DlgAutoCat_Help_PubSelected);

            clbDevelopersSelected.DisplayMember = "text";
            clbPublishersSelected.DisplayMember = "text";

            //Hide count columns
            lstDevelopers.Columns[1].Width = 0;
            lstPublishers.Columns[1].Width = 0;
        }

        #region Data Modifiers

        public override void LoadFromAutoCat(AutoCat autocat)
        {
            AutoCatDevPub ac = autocat as AutoCatDevPub;
            if (ac == null) return;
            chkAllDevelopers.Checked = ac.AllDevelopers;
            chkAllPublishers.Checked = ac.AllPublishers;
            txtPrefix.Text = ac.Prefix;
            list_numScore.Value = ac.MinCount;
            chkOwnedOnly.Checked = ac.OwnedOnly;

            FillDevList();
            FillPubList();

            lstDevelopers.BeginUpdate();
            foreach (ListViewItem item in lstDevelopers.Items)
            {
                item.Checked = ac.Developers.Contains(item.Tag.ToString());
            }
            lstDevelopers.EndUpdate();

            lstPublishers.BeginUpdate();
            foreach (ListViewItem item in lstPublishers.Items)
            {
                item.Checked = ac.Publishers.Contains(item.Tag.ToString());
            }
            lstPublishers.EndUpdate();

            loaded = true;
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatDevPub ac = autocat as AutoCatDevPub;
            if (ac == null) return;
            ac.Prefix = txtPrefix.Text;
            ac.OwnedOnly = chkOwnedOnly.Checked;
            ac.MinCount = (int) list_numScore.Value;
            ac.AllDevelopers = chkAllDevelopers.Checked;
            ac.AllPublishers = chkAllPublishers.Checked;

            ac.Developers.Clear();
            if (!chkAllDevelopers.Checked)
            {
                foreach (ListViewItem item in clbDevelopersSelected.CheckedItems)
                {
                    ac.Developers.Add(item.Tag.ToString());
                }
            }

            ac.Publishers.Clear();
            if (!chkAllPublishers.Checked)
            {
                foreach (ListViewItem item in clbPublishersSelected.CheckedItems)
                {
                    ac.Publishers.Add(item.Tag.ToString());
                }
            }
        }

        #endregion

        #region UI Updaters

        public void FillDevList(ICollection<string> preChecked = null)
        {
            if (Program.GameDB != null)
            {
                Cursor = Cursors.WaitCursor;
                IEnumerable<Tuple<string, int>> devList =
                    Program.GameDB.CalculateSortedDevList(chkOwnedOnly.Checked ? ownedGames : null,
                        (int) list_numScore.Value);
                clbDevelopersSelected.Items.Clear();
                lstDevelopers.BeginUpdate();
                lstDevelopers.Items.Clear();
                foreach (Tuple<string, int> dev in devList)
                {
                    ListViewItem newItem = new ListViewItem(string.Format("{0} [{1}]", dev.Item1, dev.Item2));
                    newItem.Tag = dev.Item1;
                    if (preChecked != null && preChecked.Contains(dev.Item1)) newItem.Checked = true;
                    newItem.SubItems.Add(dev.Item2.ToString());
                    lstDevelopers.Items.Add(newItem);
                }
                lstDevelopers.Columns[0].Width = -1;
                SortDevelopers(1, SortOrder.Descending);
                lstDevelopers.EndUpdate();
                chkAllDevelopers.Text = "All (" + lstDevelopers.Items.Count + ")";
                Cursor = Cursors.Default;
            }
        }

        public void FillPubList(ICollection<string> preChecked = null)
        {
            if (Program.GameDB != null)
            {
                Cursor = Cursors.WaitCursor;
                IEnumerable<Tuple<string, int>> pubList =
                    Program.GameDB.CalculateSortedPubList(chkOwnedOnly.Checked ? ownedGames : null,
                        (int) list_numScore.Value);
                clbPublishersSelected.Items.Clear();
                lstPublishers.BeginUpdate();
                lstPublishers.Items.Clear();
                foreach (Tuple<string, int> pub in pubList)
                {
                    ListViewItem newItem = new ListViewItem(string.Format("{0} [{1}]", pub.Item1, pub.Item2));
                    newItem.Tag = pub.Item1;
                    if (preChecked != null && preChecked.Contains(pub.Item1)) newItem.Checked = true;
                    newItem.SubItems.Add(pub.Item2.ToString());
                    lstPublishers.Items.Add(newItem);
                }
                lstPublishers.Columns[0].Width = -1;
                SortPublishers(1, SortOrder.Descending);
                lstPublishers.EndUpdate();
                chkAllPublishers.Text = "All (" + lstPublishers.Items.Count + ")";
                Cursor = Cursors.Default;
            }
        }

        private void SetAllListCheckStates(ListView list, bool to)
        {
            foreach (ListViewItem item in list.Items)
            {
                item.Checked = to;
            }
        }

        private void nameascendingDev_Click(object sender, EventArgs e)
        {
            SortDevelopers(0, SortOrder.Ascending);
        }

        private void namedescendingDev_Click(object sender, EventArgs e)
        {
            SortDevelopers(0, SortOrder.Descending);
        }

        private void countascendingDev_Click(object sender, EventArgs e)
        {
            SortDevelopers(1, SortOrder.Ascending);
        }

        private void countdescendingDev_Click(object sender, EventArgs e)
        {
            SortDevelopers(1, SortOrder.Descending);
        }

        private void nameascendingPub_Click(object sender, EventArgs e)
        {
            SortPublishers(0, SortOrder.Ascending);
        }

        private void namedescendingPub_Click(object sender, EventArgs e)
        {
            SortPublishers(0, SortOrder.Descending);
        }

        private void countascendingPub_Click(object sender, EventArgs e)
        {
            SortPublishers(1, SortOrder.Ascending);
        }

        private void countdescendingPub_Click(object sender, EventArgs e)
        {
            SortPublishers(1, SortOrder.Descending);
        }

        #endregion

        #region Event Handlers

        private void cmdListRebuild_Click(object sender, EventArgs e)
        {
            HashSet<string> checkedTags = new HashSet<string>();
            foreach (ListViewItem item in lstDevelopers.CheckedItems)
            {
                checkedTags.Add(item.Tag as string);
            }
            FillDevList(checkedTags);

            checkedTags = new HashSet<string>();
            foreach (ListViewItem item in lstPublishers.CheckedItems)
            {
                checkedTags.Add(item.Tag as string);
            }
            FillPubList(checkedTags);
        }

        #endregion

        #region Utility

        private void SortDevelopers(int c, SortOrder so)
        {
            // Create a comparer.
            lstDevelopers.ListViewItemSorter =
                new ListViewComparer(c, so);

            // Sort.
            lstDevelopers.Sort();
        }

        private void SortPublishers(int c, SortOrder so)
        {
            // Create a comparer.
            lstPublishers.ListViewItemSorter =
                new ListViewComparer(c, so);

            // Sort.
            lstDevelopers.Sort();
        }

        #endregion

        #region Developers

        #region Event Handlers

        private void btnDevCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstDevelopers, true);
        }

        private void btnDevUncheckAll_Click(object sender, EventArgs e)
        {
            loaded = false;
            FillDevList();
            loaded = true;
        }

        private void chkAllDevelopers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDevelopers.Checked)
            {
                lstDevelopers.Enabled = false;
                clbDevelopersSelected.Enabled = false;
                btnDevCheckAll.Enabled = false;
                btnDevUncheckAll.Enabled = false;
            }
            else
            {
                lstDevelopers.Enabled = true;
                clbDevelopersSelected.Enabled = true;
                btnDevCheckAll.Enabled = true;
                btnDevUncheckAll.Enabled = true;
            }
        }

        private void lstDevelopers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked) clbDevelopersSelected.Items.Add(e.Item, true);
            else if ((!e.Item.Checked) && loaded && clbDevelopersSelected.Items.Contains(e.Item))
            {
                workerThread = new Thread(DevelopersItemWorker);
                workerThread.Start(e.Item);
            }
        }

        private void clbDevelopersSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem) clbDevelopersSelected.Items[e.Index]).Checked = false;
            }
        }

        private void btnDevSelected_Click(object sender, EventArgs e)
        {
            if (splitDevTop.Panel1Collapsed)
            {
                splitDevTop.Panel1Collapsed = false;
                btnDevSelected.Text = "<";
            }
            else
            {
                splitDevTop.Panel1Collapsed = true;
                btnDevSelected.Text = ">";
            }
        }

        #endregion

        #region Helper Thread

        delegate void DevItemCallback(ListViewItem obj);

        private void DevelopersRemoveItem(ListViewItem obj)
        {
            if (clbDevelopersSelected.InvokeRequired)
            {
                DevItemCallback callback = DevelopersRemoveItem;
                Invoke(callback, obj);
            }
            else
            {
                clbDevelopersSelected.Items.Remove(obj);
            }
        }

        private void DevelopersItemWorker(object obj)
        {
            DevelopersRemoveItem((ListViewItem) obj);
        }

        #endregion

        #endregion

        #region Publishers

        #region Event Handlers

        private void btnPubCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstPublishers, true);
        }

        private void btnPubUncheckAll_Click(object sender, EventArgs e)
        {
            loaded = false;
            FillPubList();
            loaded = true;
        }

        private void chkAllPublishers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPublishers.Checked)
            {
                lstPublishers.Enabled = false;
                clbPublishersSelected.Enabled = false;
                btnPubCheckAll.Enabled = false;
                btnPubUncheckAll.Enabled = false;
            }
            else
            {
                lstPublishers.Enabled = true;
                clbPublishersSelected.Enabled = true;
                btnPubCheckAll.Enabled = true;
                btnPubUncheckAll.Enabled = true;
            }
        }

        private void lstPublishers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked) clbPublishersSelected.Items.Add(e.Item, true);
            else if ((!e.Item.Checked) && loaded && clbPublishersSelected.Items.Contains(e.Item))
            {
                workerThread = new Thread(PublishersItemWorker);
                workerThread.Start(e.Item);
            }
        }

        private void clbPublishersSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem) clbPublishersSelected.Items[e.Index]).Checked = false;
            }
        }

        private void btnPubSelected_Click(object sender, EventArgs e)
        {
            if (splitPubTop.Panel1Collapsed)
            {
                splitPubTop.Panel1Collapsed = false;
                btnPubSelected.Text = "<";
            }
            else
            {
                splitPubTop.Panel1Collapsed = true;
                btnPubSelected.Text = ">";
            }
        }

        #endregion

        #region Helper Thread

        delegate void PubItemCallback(ListViewItem obj);

        private void PublishersRemoveItem(ListViewItem obj)
        {
            if (clbPublishersSelected.InvokeRequired)
            {
                PubItemCallback callback = PublishersRemoveItem;
                Invoke(callback, obj);
            }
            else
            {
                clbPublishersSelected.Items.Remove(obj);
            }
        }

        private void PublishersItemWorker(object obj)
        {
            PublishersRemoveItem((ListViewItem) obj);
        }

        #endregion

        #endregion
    }
}