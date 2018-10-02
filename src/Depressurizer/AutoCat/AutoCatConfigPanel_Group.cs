#region LICENSE

//     This file (AutoCatConfigPanel_Group.cs) is part of Depressurizer.
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
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Group : AutoCatConfigPanel
    {
        #region Fields

        //private List<string> stringAutocats;
        private readonly List<AutoCat> Autocats;

        private AutoCat current;

        #endregion

        #region Constructors and Destructors

        public AutoCatConfigPanel_Group(List<AutoCat> autocats)
        {
            InitializeComponent();

            Autocats = autocats;
        }

        #endregion

        #region Public Methods and Operators

        public List<string> GetGroup()
        {
            List<string> group = new List<string>();
            foreach (string name in lbAutocats.Items)
            {
                group.Add(name);
            }

            return group;
        }

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

        #region Methods

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

        private void btnDown_Click(object sender, EventArgs e)
        {
            Utility.MoveItem(lbAutocats, 1);
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

        private void btnUp_Click(object sender, EventArgs e)
        {
            Utility.MoveItem(lbAutocats, -1);
        }

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
                btnUp.Enabled = lbAutocats.SelectedIndex == 0 ? false : true;
                btnDown.Enabled = lbAutocats.SelectedIndex == lbAutocats.Items.Count - 1 ? false : true;
            }
            else
            {
                btnRemove.Enabled = false;
                btnUp.Enabled = false;
                btnDown.Enabled = false;
            }
        }

        #endregion
    }
}
