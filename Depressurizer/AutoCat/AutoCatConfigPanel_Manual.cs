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
    public partial class AutoCatConfigPanel_Manual : AutoCatConfigPanel
    {
        // used to remove unchecked items from the Add and Remove checkedlistbox.
        private Thread workerThread;

        private bool loaded;
        private GameList ownedGames;

        public AutoCatConfigPanel_Manual(GameList gamelist)
        {
            InitializeComponent();

            ownedGames = gamelist;

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);

            FillRemoveList();
            FillAddList();

            clbRemoveSelected.DisplayMember = "text";
            clbAddSelected.DisplayMember = "text";

            //Hide count columns
            lstRemove.Columns[1].Width = 0;
            lstAdd.Columns[1].Width = 0;
        }

        #region Data modifiers

        public override void LoadFromAutoCat(AutoCat autocat)
        {
            AutoCatManual ac = autocat as AutoCatManual;
            if (ac == null)
            {
                return;
            }

            chkRemoveAll.Checked = ac.RemoveAllCategories;
            txtPrefix.Text = ac.Prefix;

            lstRemove.BeginUpdate();

            List<string> found = new List<string>();
            foreach (ListViewItem item in lstRemove.Items)
            {
                item.Checked = ac.RemoveCategories.Contains(item.Name);
                found.Add(item.Name);
            }
            lstRemove.EndUpdate();

            foreach (string s in ac.RemoveCategories)
            {
                if (!found.Contains(s))
                {
                    ListViewItem l = new ListViewItem();
                    l.Text = s;
                    l.Name = s;
                    clbRemoveSelected.Items.Add(l, true);
                }
            }

            lstAdd.BeginUpdate();
            found = new List<string>();
            foreach (ListViewItem item in lstAdd.Items)
            {
                item.Checked = ac.AddCategories.Contains(item.Name);
                found.Add(item.Name);
            }
            lstAdd.EndUpdate();

            foreach (string s in ac.AddCategories)
            {
                if (!found.Contains(s))
                {
                    ListViewItem l = new ListViewItem();
                    l.Text = s;
                    l.Name = s;
                    clbAddSelected.Items.Add(l, true);
                }
            }

            UpdateRemoveCount();
            UpdateAddCount();

            loaded = true;
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatManual ac = autocat as AutoCatManual;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;
            ac.RemoveAllCategories = chkRemoveAll.Checked;

            ac.RemoveCategories.Clear();
            if (!chkRemoveAll.Checked)
            {
                foreach (ListViewItem item in clbRemoveSelected.CheckedItems)
                {
                    ac.RemoveCategories.Add(item.Name);
                }
            }

            ac.AddCategories.Clear();
            foreach (ListViewItem item in clbAddSelected.CheckedItems)
            {
                ac.AddCategories.Add(item.Name);
            }
        }

        #endregion

        #region UI Updaters

        public void FillRemoveList()
        {
            clbRemoveSelected.Items.Clear();
            lstRemove.BeginUpdate();
            lstRemove.Items.Clear();

            if (ownedGames.Categories != null)
            {
                foreach (Category c in ownedGames.Categories)
                {
                    ListViewItem l = CreateCategoryListViewItem(c);
                    l.SubItems.Add(c.Count.ToString());
                    lstRemove.Items.Add(l);
                }
            }
            lstRemove.Columns[0].Width = -1;
            SortRemove(1, SortOrder.Descending);
            lstRemove.EndUpdate();
        }

        public void FillAddList()
        {
            clbAddSelected.Items.Clear();
            lstAdd.BeginUpdate();
            lstAdd.Items.Clear();

            if (ownedGames.Categories != null)
            {
                foreach (Category c in ownedGames.Categories)
                {
                    ListViewItem l = CreateCategoryListViewItem(c);
                    l.SubItems.Add(c.Count.ToString());
                    lstAdd.Items.Add(l);
                }
            }
            lstAdd.Columns[0].Width = -1;
            SortAdd(1, SortOrder.Descending);
            lstAdd.EndUpdate();
        }

        private void UpdateRemoveCount()
        {
            groupRemove.Text = "Remove (" + clbRemoveSelected.Items.Count + "):";
        }

        private void UpdateAddCount()
        {
            groupAdd.Text = "Add (" + clbAddSelected.Items.Count + "):";
        }

        private void SetAllListCheckStates(ListView list, bool to)
        {
            foreach (ListViewItem item in list.Items)
            {
                item.Checked = to;
            }
        }

        private void nameascendingRemove_Click(object sender, EventArgs e)
        {
            SortRemove(0, SortOrder.Ascending);
        }

        private void namedescendingRemove_Click(object sender, EventArgs e)
        {
            SortRemove(0, SortOrder.Descending);
        }

        private void countascendingRemove_Click(object sender, EventArgs e)
        {
            SortRemove(1, SortOrder.Ascending);
        }

        private void countdescendingRemove_Click(object sender, EventArgs e)
        {
            SortRemove(1, SortOrder.Descending);
        }

        private void nameascendingAdd_Click(object sender, EventArgs e)
        {
            SortAdd(0, SortOrder.Ascending);
        }

        private void namedescendingAdd_Click(object sender, EventArgs e)
        {
            SortAdd(0, SortOrder.Descending);
        }

        private void countascendingAdd_Click(object sender, EventArgs e)
        {
            SortAdd(1, SortOrder.Ascending);
        }

        private void countdescendingAdd_Click(object sender, EventArgs e)
        {
            SortAdd(1, SortOrder.Descending);
        }

        #endregion

        #region Remove Categories

        #region Event Handlers

        private void btnRemoveCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstRemove, true);
        }

        private void btnRemoveUncheckAll_Click(object sender, EventArgs e)
        {
            loaded = false;
            FillRemoveList();
            loaded = true;
        }

        private void chkRemoveAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRemoveAll.Checked)
            {
                lstRemove.Enabled = false;
                clbRemoveSelected.Enabled = false;
                btnRemoveCheckAll.Enabled = false;
                btnRemoveUncheckAll.Enabled = false;
            }
            else
            {
                lstRemove.Enabled = true;
                clbRemoveSelected.Enabled = true;
                btnRemoveCheckAll.Enabled = true;
                btnRemoveUncheckAll.Enabled = true;
            }
        }

        private void lstRemove_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                clbRemoveSelected.Items.Add(e.Item, true);
            }
            else if ((!e.Item.Checked) && loaded)
            {
                workerThread = new Thread(RemoveItemWorker);
                workerThread.Start(e.Item);
            }
            UpdateRemoveCount();
        }

        private void clbRemoveSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem) clbRemoveSelected.Items[e.Index]).Checked = false;
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (splitRemoveTop.Panel1Collapsed)
            {
                splitRemoveTop.Panel1Collapsed = false;
                btnRemoveSelected.Text = "<";
            }
            else
            {
                splitRemoveTop.Panel1Collapsed = true;
                btnRemoveSelected.Text = ">";
            }
        }

        #endregion

        #region Helper Thread 

        delegate void RemoveItemCallback(ListViewItem obj);

        private void RemoveItem(ListViewItem obj)
        {
            if (clbRemoveSelected.InvokeRequired)
            {
                RemoveItemCallback callback = RemoveItem;
                Invoke(callback, obj);
            }
            else
            {
                clbRemoveSelected.Items.Remove(obj);
                UpdateRemoveCount();
            }
        }

        private void RemoveItemWorker(object obj)
        {
            RemoveItem((ListViewItem) obj);
        }

        #endregion

        #endregion

        #region Add Categories

        #region Event Handlers

        private void btnAddCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstAdd, true);
        }

        private void btnAddUncheckAll_Click(object sender, EventArgs e)
        {
            loaded = false;
            FillAddList();
            loaded = true;
        }

        private void lstAdd_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                clbAddSelected.Items.Add(e.Item, true);
            }
            else if ((!e.Item.Checked) && loaded)
            {
                workerThread = new Thread(AddItemWorker);
                workerThread.Start(e.Item);
            }
            UpdateAddCount();
        }

        private void clbAddSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem) clbAddSelected.Items[e.Index]).Checked = false;
            }
        }

        private void btnAddSelected_Click(object sender, EventArgs e)
        {
            if (splitAddTop.Panel1Collapsed)
            {
                splitAddTop.Panel1Collapsed = false;
                btnAddSelected.Text = "<";
            }
            else
            {
                splitAddTop.Panel1Collapsed = true;
                btnAddSelected.Text = ">";
            }
        }

        #endregion

        #region Helper Thread

        delegate void AddItemCallback(ListViewItem obj);

        private void AddItem(ListViewItem obj)
        {
            if (clbAddSelected.InvokeRequired)
            {
                AddItemCallback callback = AddItem;
                Invoke(callback, obj);
            }
            else
            {
                clbAddSelected.Items.Remove(obj);
                UpdateAddCount();
            }
        }

        private void AddItemWorker(object obj)
        {
            AddItem((ListViewItem) obj);
        }

        #endregion

        #endregion

        #region Utility

        private void SortRemove(int c, SortOrder so)
        {
            // Create a comparer.
            lstRemove.ListViewItemSorter =
                new ListViewComparer(c, so);

            // Sort.
            lstRemove.Sort();
        }

        private void SortAdd(int c, SortOrder so)
        {
            // Create a comparer.
            lstAdd.ListViewItemSorter =
                new ListViewComparer(c, so);

            // Sort.
            lstAdd.Sort();
        }

        private ListViewItem CreateCategoryListViewItem(Category c)
        {
            ListViewItem i = new ListViewItem(c.Name + " (" + c.Count + ")");
            i.Tag = c;
            i.Name = c.Name;
            return i;
        }

        #endregion
    }
}