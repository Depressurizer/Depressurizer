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
    public partial class DlgAutoCatSelect : Form
    {
        public List<AutoCat> AutoCatList;
        public string originalGroup;

        public DlgAutoCatSelect(List<AutoCat> autoCats, string name)
        {
            InitializeComponent();

            AutoCatList = new List<AutoCat>();
            originalGroup = name;

            foreach (AutoCat c in autoCats)
            {
                AutoCat clone = c.Clone();
                clone.Selected = false;
                AutoCatList.Add(clone);
            }
        }

        #region UI Uptaters

        private void FillAutocatList()
        {
            clbAutocats.Items.Clear();
            foreach (AutoCat ac in AutoCatList)
            {
                if (ac.Name != originalGroup)
                {
                    bool addAC = true;
                    if (ac.AutoCatType == AutoCatType.Group)
                    {
                        addAC = SafeGroup(((AutoCatGroup) ac).Autocats, new List<string>(new[] {originalGroup}));
                    }
                    if (addAC)
                    {
                        clbAutocats.Items.Add(ac);
                    }
                }
            }
            clbAutocats.DisplayMember = "DisplayName";
        }

        #endregion

        #region Event Handlers

        private void DlgAutoCat_Load(object sender, EventArgs e)
        {
            FillAutocatList();
        }

        private void clbAutocats_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ((AutoCat) clbAutocats.Items[e.Index]).Selected = e.NewValue == CheckState.Checked ? true : false;
        }

        #endregion

        #region Utility

        private bool SafeGroup(List<string> autocats, List<string> groups)
        {
            foreach (string ac in autocats)
            {
                // is AutoCat a group?
                if (IsGroup(ac))
                {
                    // if group list already contains the group then we are stuck in an infinite loop.  RETURN FALSE.
                    if (groups.Contains(ac))
                    {
                        return false;
                    }
                    // add new group to group list
                    groups.Add(ac);
                    // get AutoCat from group name
                    AutoCatGroup group = GetAutoCat(ac) as AutoCatGroup;
                    // send new group to SafeGroup to continue testing
                    return SafeGroup(group.Autocats, groups);
                }
            }
            // no duplicate group found.  All good! RETURN TRUE.
            return true;
        }

        // find and return AutoCat using the name
        public AutoCat GetAutoCat(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            foreach (AutoCat ac in AutoCatList)
            {
                if (String.Equals(ac.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return ac;
                }
            }

            return null;
        }

        private bool IsGroup(string find)
        {
            AutoCat test = GetAutoCat(find);
            return (test.AutoCatType == AutoCatType.Group) ? true : false;
        }

        #endregion
    }
}