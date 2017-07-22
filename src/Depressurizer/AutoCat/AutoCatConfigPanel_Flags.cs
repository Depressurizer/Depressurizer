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
    public partial class AutoCatConfigPanel_Flags : AutoCatConfigPanel
    {
        public AutoCatConfigPanel_Flags()
        {
            InitializeComponent();

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);

            FillFlagsList();
        }

        public void FillFlagsList()
        {
            lstIncluded.Items.Clear();

            if (Program.GameDB != null)
            {
                SortedSet<string> flagsList = Program.GameDB.GetAllStoreFlags();

                foreach (string s in flagsList)
                {
                    lstIncluded.Items.Add(s);
                }
            }
        }

        public override void LoadFromAutoCat(AutoCat autocat)
        {
            AutoCatFlags ac = autocat as AutoCatFlags;
            if (ac == null)
            {
                return;
            }

            txtPrefix.Text = ac.Prefix;

            foreach (ListViewItem item in lstIncluded.Items)
            {
                item.Checked = ac.IncludedFlags.Contains(item.Text);
            }
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatFlags ac = autocat as AutoCatFlags;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;

            ac.IncludedFlags.Clear();
            foreach (ListViewItem i in lstIncluded.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedFlags.Add(i.Text);
                }
            }
        }

        private void SetAllListCheckStates(ListView list, bool to)
        {
            foreach (ListViewItem item in list.Items)
            {
                item.Checked = to;
            }
        }

        private void cmdCheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIncluded, true);
        }

        private void cmdUncheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstIncluded, false);
        }
    }
}