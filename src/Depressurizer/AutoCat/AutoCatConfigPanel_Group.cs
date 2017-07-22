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
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Group : AutoCatConfigPanel
    {
        //private List<string> stringAutocats;
        private List<AutoCat> Autocats;

        private AutoCat current;

        public AutoCatConfigPanel_Group(List<AutoCat> autocats)
        {
            InitializeComponent();

            Autocats = autocats;
        }

        #region Data modifiers

        public override void LoadFromAutoCat(AutoCat autocat)
        {
            AutoCatGroup ac = autocat as AutoCatGroup;
            current = autocat;
            if (ac == null)
            {
                return;
            }

            FillAutocatList(ac.Autocats);
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatGroup ac = autocat as AutoCatGroup;
            if (ac == null)
            {
                return;
            }

            ac.Autocats = GetGroup();
        }

        #endregion

        #region Event Handlers

        private void btnUp_Click(object sender, EventArgs e)
        {
            Utility.MoveItem(lbAutocats, -1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            Utility.MoveItem(lbAutocats, 1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DlgAutoCatSelect dlg = new DlgAutoCatSelect(Autocats, current.Name);

            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                foreach (AutoCat ac in dlg.AutoCatList)
                {
                    if (ac.Selected && !InGroup(ac.Name))
                    {
                        lbAutocats.Items.Add(ac.Name);
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbAutocats.SelectedItems.Count > 1)
            {
                foreach (string s in lbAutocats.SelectedItems)
                {
                    lbAutocats.Items.Remove(s);
                }
            }
            else if (lbAutocats.SelectedItem != null)
            {
                lbAutocats.Items.Remove(lbAutocats.SelectedItem);
            }
        }

        private void lbAutocats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAutocats.SelectedItems.Count > 1)
            {
                btnRemove.Enabled = true;
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }
            else if (lbAutocats.SelectedItem != null)
            {
                btnRemove.Enabled = true;
                btnUp.Enabled = (lbAutocats.SelectedIndex == 0) ? false : true;
                btnDown.Enabled = (lbAutocats.SelectedIndex == (lbAutocats.Items.Count - 1)) ? false : true;
            }
            else
            {
                btnRemove.Enabled = false;
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }
        }

        #endregion

        #region UI Updaters

        private void FillAutocatList(List<string> group)
        {
            if (group != null)
            {
                lbAutocats.Items.Clear();
            }
            {
                foreach (string name in group)
                {
                    lbAutocats.Items.Add(name);
                }
            }
        }

        #endregion

        #region Utility

        private bool InGroup(string find)
        {
            foreach (string name in lbAutocats.Items)
            {
                if (name == find)
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> GetGroup()
        {
            List<string> group = new List<string>();
            foreach (string name in lbAutocats.Items)
            {
                group.Add(name);
            }
            return group;
        }

        #endregion
    }
}