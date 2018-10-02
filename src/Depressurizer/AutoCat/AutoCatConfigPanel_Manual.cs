#region LICENSE

//     This file (AutoCatConfigPanel_Manual.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Manual : AutoCatConfigPanel
    {
        #region Fields

        private readonly GameList ownedGames;
        private bool loaded;

        // used to remove unchecked items from the Add and Remove checkedlistbox.
        private Thread workerThread;

        #endregion

        #region Constructors and Destructors

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

        #endregion

        #region Delegates

        private delegate void AddItemCallback(ListViewItem obj);

        private delegate void RemoveItemCallback(ListViewItem obj);

        #endregion

        #region Public Methods and Operators

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

        #region Methods

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

        private void btnAddCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstAdd, true);
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

        private void btnAddUncheckAll_Click(object sender, EventArgs e)
        {
            loaded = false;
            FillAddList();
            loaded = true;
        }

        private void btnRemoveCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstRemove, true);
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

        private void clbAddSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem) clbAddSelected.Items[e.Index]).Checked = false;
            }
        }

        private void clbRemoveSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem) clbRemoveSelected.Items[e.Index]).Checked = false;
            }
        }

        private void countascendingAdd_Click(object sender, EventArgs e)
        {
            SortAdd(1, SortOrder.Ascending);
        }

        private void countascendingRemove_Click(object sender, EventArgs e)
        {
            SortRemove(1, SortOrder.Ascending);
        }

        private void countdescendingAdd_Click(object sender, EventArgs e)
        {
            SortAdd(1, SortOrder.Descending);
        }

        private void countdescendingRemove_Click(object sender, EventArgs e)
        {
            SortRemove(1, SortOrder.Descending);
        }

        private ListViewItem CreateCategoryListViewItem(Category c)
        {
            ListViewItem i = new ListViewItem(c.Name + " (" + c.Count + ")");
            i.Tag = c;
            i.Name = c.Name;
            return i;
        }

        private void lstAdd_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                clbAddSelected.Items.Add(e.Item, true);
            }
            else if (!e.Item.Checked && loaded)
            {
                workerThread = new Thread(AddItemWorker);
                workerThread.Start(e.Item);
            }

            UpdateAddCount();
        }

        private void lstRemove_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                clbRemoveSelected.Items.Add(e.Item, true);
            }
            else if (!e.Item.Checked && loaded)
            {
                workerThread = new Thread(RemoveItemWorker);
                workerThread.Start(e.Item);
            }

            UpdateRemoveCount();
        }

        private void nameascendingAdd_Click(object sender, EventArgs e)
        {
            SortAdd(0, SortOrder.Ascending);
        }

        private void nameascendingRemove_Click(object sender, EventArgs e)
        {
            SortRemove(0, SortOrder.Ascending);
        }

        private void namedescendingAdd_Click(object sender, EventArgs e)
        {
            SortAdd(0, SortOrder.Descending);
        }

        private void namedescendingRemove_Click(object sender, EventArgs e)
        {
            SortRemove(0, SortOrder.Descending);
        }

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

        private void SetAllListCheckStates(ListView list, bool to)
        {
            foreach (ListViewItem item in list.Items)
            {
                item.Checked = to;
            }
        }

        private void SortAdd(int c, SortOrder so)
        {
            // Create a comparer.
            lstAdd.ListViewItemSorter = new ListViewComparer(c, so);

            // Sort.
            lstAdd.Sort();
        }

        private void SortRemove(int c, SortOrder so)
        {
            // Create a comparer.
            lstRemove.ListViewItemSorter = new ListViewComparer(c, so);

            // Sort.
            lstRemove.Sort();
        }

        private void UpdateAddCount()
        {
            groupAdd.Text = "Add (" + clbAddSelected.Items.Count + "):";
        }

        private void UpdateRemoveCount()
        {
            groupRemove.Text = "Remove (" + clbRemoveSelected.Items.Count + "):";
        }

        #endregion
    }
}
