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

namespace Depressurizer {
    public partial class AutoCatConfigPanel_Manual : AutoCatConfigPanel {

        // used to remove unchecked items from the Add and Remove checkedlistbox.
        private Thread workerThread;
        private bool loaded = false;
        private List<Category> categories;

        public AutoCatConfigPanel_Manual(GameList gamelist) {
            
            InitializeComponent();

            categories = gamelist.Categories;

            ttHelp.Ext_SetToolTip( helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );

            FillRemoveList();
            FillAddList();

            clbRemoveSelected.DisplayMember = "text";
            clbAddSelected.DisplayMember = "text";
        }

        public void FillRemoveList()
        {
            clbRemoveSelected.Items.Clear();
            lstRemove.Items.Clear();

            if (categories != null)
            {
                foreach (Category c in categories)
                {
                    lstRemove.Items.Add(c.Name);
                }
            }
        }

        public void FillAddList()
        {
            clbAddSelected.Items.Clear();
            lstAdd.Items.Clear();

            if (categories != null)
            {
                foreach (Category c in categories)
                {
                    lstAdd.Items.Add(c.Name);
                }
            }
        }


        public override void LoadFromAutoCat( AutoCat autocat ) {
            AutoCatManual ac = autocat as AutoCatManual;
            if( ac == null ) return;
            chkRemoveAll.Checked = ac.RemoveAllCategories;
            txtPrefix.Text = ac.Prefix;

            foreach( ListViewItem item in lstRemove.Items ) {
                item.Checked = ac.RemoveCategories.Contains( item.Text );
            }

            foreach (ListViewItem item in lstAdd.Items)
            {
                item.Checked = ac.AddCategories.Contains(item.Text);
            }
            loaded = true;
        }

        public override void SaveToAutoCat( AutoCat autocat ) {
            AutoCatManual ac = autocat as AutoCatManual;
            if( ac == null ) return;
            ac.Prefix = txtPrefix.Text;
            ac.RemoveAllCategories = chkRemoveAll.Checked;

            ac.RemoveCategories.Clear();
            if (!chkRemoveAll.Checked)
            {
                foreach (ListViewItem i in lstRemove.Items)
                {
                    if (i.Checked) ac.RemoveCategories.Add(i.Text);
                }
            }

            ac.AddCategories.Clear();
            foreach (ListViewItem i in lstAdd.Items)
            {
                if (i.Checked) ac.AddCategories.Add(i.Text);
            }
        }

        private void SetAllListCheckStates( ListView list, bool to ) {
            foreach( ListViewItem item in list.Items ) {
                item.Checked = to;
            }
        }

        private void btnRemoveCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( lstRemove, true );
        }

        private void btnRemoveUncheckAll_Click( object sender, EventArgs e ) {
            loaded = false;
            FillRemoveList();
            loaded = true;
        }

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

        #region Remove Categories

        private void chkRemoveAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRemoveAll.Checked)
            {
                lstRemove.Enabled = false;
                clbRemoveSelected.Enabled = false;
            }
            else
            {
                lstRemove.Enabled = true;
                clbRemoveSelected.Enabled = true;
            }
        }

        private void lstRemove_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked) clbRemoveSelected.Items.Add(e.Item, true);
            else if ((!e.Item.Checked) && loaded)
            {
                workerThread = new Thread(new ParameterizedThreadStart(RemoveItemWorker));
                workerThread.Start(clbRemoveSelected.Items.IndexOf(e.Item));
            }
        }

        private void clbRemoveSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem)clbRemoveSelected.Items[e.Index]).Checked = false;
            }
        }

        delegate void RemoveItemCallback(int index);

        private void RemoveItem(int index)
        {
            if (this.clbRemoveSelected.InvokeRequired)
            {
                RemoveItemCallback callback = new RemoveItemCallback(RemoveItem);
                this.Invoke(callback, new object[] { index });
            }
            else
            {
                clbRemoveSelected.Items.RemoveAt(index);
            }
        }

        private void RemoveItemWorker(object obj)
        {
            int index = (int)obj;
            RemoveItem(index);
        }

        #endregion

        #region Add Categories

        private void lstAdd_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked) clbAddSelected.Items.Add(e.Item, true);
            else if ((!e.Item.Checked) && loaded)
            {
                workerThread = new Thread(new ParameterizedThreadStart(AddItemWorker));
                workerThread.Start(clbAddSelected.Items.IndexOf(e.Item));
            }
        }

        private void clbAddSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem)clbAddSelected.Items[e.Index]).Checked = false;
            }
        }

        delegate void AddItemCallback(int index);

        private void AddItem(int index)
        {
            if (this.clbAddSelected.InvokeRequired)
            {
                AddItemCallback callback = new AddItemCallback(AddItem);
                this.Invoke(callback, new object[] { index });
            }
            else
            {
                clbAddSelected.Items.RemoveAt(index);
            }
        }

        private void AddItemWorker(object obj)
        {
            int index = (int)obj;
            AddItem(index);
        }

        #endregion
    }
}
