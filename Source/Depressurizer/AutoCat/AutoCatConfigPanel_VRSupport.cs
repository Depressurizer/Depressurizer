/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Theodoros Dimos.

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
using System.Windows.Forms;

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_VrSupport : AutoCatConfigPanel
    {
        public AutoCatConfigPanel_VrSupport()
        {
            InitializeComponent();

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);

            FillVrSupportLists();
        }

        public void FillVrSupportLists()
        {
            lstVrHeadsets.Items.Clear();
            lstVrInput.Items.Clear();
            lstVrPlayArea.Items.Clear();

            if (Program.GameDB != null)
            {
                VrSupport vrSupport = Program.GameDB.GetAllVrSupportFlags();

                foreach (string s in vrSupport.Headsets)
                {
                    lstVrHeadsets.Items.Add(s);
                }

                foreach (string s in vrSupport.Input)
                {
                    lstVrInput.Items.Add(s);
                }

                foreach (string s in vrSupport.PlayArea)
                {
                    lstVrPlayArea.Items.Add(s);
                }
            }
        }

        public override void LoadFromAutoCat(AutoCat autocat)
        {
            AutoCatVrSupport ac = autocat as AutoCatVrSupport;
            if (ac == null)
            {
                return;
            }

            txtPrefix.Text = ac.Prefix;

            foreach (ListViewItem item in lstVrHeadsets.Items)
            {
                item.Checked = ac.IncludedVrSupportFlags.Headsets.Contains(item.Text);
            }

            foreach (ListViewItem item in lstVrInput.Items)
            {
                item.Checked = ac.IncludedVrSupportFlags.Input.Contains(item.Text);
            }

            foreach (ListViewItem item in lstVrPlayArea.Items)
            {
                item.Checked = ac.IncludedVrSupportFlags.PlayArea.Contains(item.Text);
            }
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatVrSupport ac = autocat as AutoCatVrSupport;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;

            ac.IncludedVrSupportFlags.Headsets.Clear();
            ac.IncludedVrSupportFlags.Input.Clear();
            ac.IncludedVrSupportFlags.PlayArea.Clear();

            foreach (ListViewItem i in lstVrHeadsets.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedVrSupportFlags.Headsets.Add(i.Text);
                }
            }

            foreach (ListViewItem i in lstVrInput.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedVrSupportFlags.Input.Add(i.Text);
                }
            }

            foreach (ListViewItem i in lstVrPlayArea.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedVrSupportFlags.PlayArea.Add(i.Text);
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
            SetAllListCheckStates(lstVrHeadsets, true);
            SetAllListCheckStates(lstVrInput, true);
            SetAllListCheckStates(lstVrPlayArea, true);
        }

        private void cmdUncheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstVrHeadsets, false);
            SetAllListCheckStates(lstVrInput, false);
            SetAllListCheckStates(lstVrPlayArea, false);
        }
    }
}