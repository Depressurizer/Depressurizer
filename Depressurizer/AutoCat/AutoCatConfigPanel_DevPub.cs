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
    public partial class AutoCatConfigPanel_DevPub : AutoCatConfigPanel {

        // used to remove unchecked items from the Add and Remove checkedlistbox.
        private Thread workerThread;
        private bool loaded = false;
        private GameList ownedGames;

        public AutoCatConfigPanel_DevPub(GameList g) {
            
            InitializeComponent();

            ownedGames = g;

            ttHelp.Ext_SetToolTip( helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );

            FillDevelopersList();
            FillPublishersList();

            clbDevelopersSelected.DisplayMember = "text";
            clbPublishersSelected.DisplayMember = "text";
        }

        public void FillDevelopersList()
        {
            if (Program.GameDB != null)
            {
                lstDevelopers.BeginUpdate();
                lstDevelopers.Items.Clear();
                SortedSet<string> developerList = Program.GameDB.GetAllDevelopers( ownedGames );

                foreach (string s in developerList)
                {
                    lstDevelopers.Items.Add(s);
                }
                lstDevelopers.EndUpdate();

                chkAllDevelopers.Text = "All (" + lstDevelopers.Items.Count.ToString() + ")";
            }
        }

        public void FillPublishersList()
        {
            if (Program.GameDB != null)
            {
                lstPublishers.BeginUpdate();
                lstPublishers.Items.Clear();
                SortedSet<string> publisherList = Program.GameDB.GetAllPublishers( ownedGames );

                foreach (string s in publisherList)
                {
                    lstPublishers.Items.Add(s);
                }
                lstPublishers.EndUpdate();

                chkAllPublishers.Text = "All (" + lstPublishers.Items.Count.ToString() + ")";
            }
        }

        public override void LoadFromAutoCat( AutoCat autocat ) {
            AutoCatDevPub ac = autocat as AutoCatDevPub;
            if( ac == null ) return;
            chkAllDevelopers.Checked = ac.AllDevelopers;
            chkAllPublishers.Checked = ac.AllPublishers;
            txtPrefix.Text = ac.Prefix;

            lstDevelopers.BeginUpdate();
            foreach ( ListViewItem item in lstDevelopers.Items ) {
                item.Checked = ac.Developers.Contains( item.Text );
            }
            lstDevelopers.EndUpdate();

            lstPublishers.BeginUpdate();
            foreach (ListViewItem item in lstPublishers.Items)
            {
                item.Checked = ac.Publishers.Contains(item.Text);
            }
            lstPublishers.EndUpdate();
            loaded = true;
        }

        public override void SaveToAutoCat( AutoCat autocat ) {
            AutoCatDevPub ac = autocat as AutoCatDevPub;
            if( ac == null ) return;
            ac.Prefix = txtPrefix.Text;
            ac.AllDevelopers = chkAllDevelopers.Checked;
            ac.AllPublishers = chkAllPublishers.Checked;

            ac.Developers.Clear();
            if (!chkAllDevelopers.Checked)
            {
                foreach (ListViewItem item in clbDevelopersSelected.CheckedItems)
                {
                    ac.Developers.Add(item.Text);
                }
                //foreach (ListViewItem i in lstDevelopers.Items)
                //{
                //    if (i.Checked) ac.Developers.Add(i.Text);
                //}
            }

            ac.Publishers.Clear();
            if (!chkAllPublishers.Checked)
            {
                foreach (ListViewItem item in clbPublishersSelected.CheckedItems)
                {
                    ac.Publishers.Add(item.Text);
                }
                //foreach (ListViewItem i in lstDevelopers.Items)
                //{
                //    if (i.Checked) ac.Developers.Add(i.Text);
                //}
            }
        }

        private void SetAllListCheckStates( ListView list, bool to ) {
            foreach( ListViewItem item in list.Items ) {
                item.Checked = to;
            }
        }

        private void btnDevCheckAll_Click( object sender, EventArgs e ) {
            SetAllListCheckStates( lstDevelopers, true );
        }

        private void btnDevUncheckAll_Click( object sender, EventArgs e ) {
            loaded = false;
            FillDevelopersList();
            loaded = true;
        }

        private void btnPubCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstPublishers, true);
        }

        private void btnPubUncheckAll_Click(object sender, EventArgs e)
        {
            loaded = false;
            FillPublishersList();
            loaded = true;
        }

        #region Remove Categories

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
            else if ((!e.Item.Checked) && loaded)
            {
                workerThread = new Thread(new ParameterizedThreadStart(DevelopersItemWorker));
                workerThread.Start(clbDevelopersSelected.Items.IndexOf(e.Item));
            }
        }

        private void clbDevelopersSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem)clbDevelopersSelected.Items[e.Index]).Checked = false;
            }
        }

        delegate void RemoveItemCallback(int index);

        private void DevelopersRemoveItem(int index)
        {
            if (this.clbDevelopersSelected.InvokeRequired)
            {
                RemoveItemCallback callback = new RemoveItemCallback(DevelopersRemoveItem);
                this.Invoke(callback, new object[] { index });
            }
            else
            {
                clbDevelopersSelected.Items.RemoveAt(index);
            }
        }

        private void DevelopersItemWorker(object obj)
        {
            int index = (int)obj;
            DevelopersRemoveItem(index);
        }

        #endregion

        #region Add Categories

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
            else if ((!e.Item.Checked) && loaded)
            {
                workerThread = new Thread(new ParameterizedThreadStart(PublishersItemWorker));
                workerThread.Start(clbPublishersSelected.Items.IndexOf(e.Item));
            }
        }

        private void clbPublishersSelected_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                ((ListViewItem)clbPublishersSelected.Items[e.Index]).Checked = false;
            }
        }

        delegate void AddItemCallback(int index);

        private void PublishersRemoveItem(int index)
        {
            if (this.clbPublishersSelected.InvokeRequired)
            {
                AddItemCallback callback = new AddItemCallback(PublishersRemoveItem);
                this.Invoke(callback, new object[] { index });
            }
            else
            {
                clbPublishersSelected.Items.RemoveAt(index);
            }
        }

        private void PublishersItemWorker(object obj)
        {
            int index = (int)obj;
            PublishersRemoveItem(index);
        }

        #endregion

    }
}
