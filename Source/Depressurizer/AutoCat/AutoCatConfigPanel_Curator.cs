/*
This file is part of Depressurizer.
Original Work Copyright 2011, 2012, 2013 Steve Labbe.
Modified Work Copyright 2017 Theodoros Dimos.

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
    public partial class AutoCatConfigPanel_Curator : AutoCatConfigPanel
    {
        public AutoCatConfigPanel_Curator()
        {
            InitializeComponent();

            // Set up help tooltips
            ttHelp.Ext_SetToolTip(helpCategoryName, GlobalStrings.AutoCatCurator_Help_CategoryName);
            ttHelp.Ext_SetToolTip(helpCuratorUrl, "e.g http://store.steampowered.com/curator/6090344-depressurizer/");

            FillRecommendationsList();
        }

        public void FillRecommendationsList()
        {
            lstIncluded.Items.Clear();
            lstIncluded.Items.Add(Utility.GetEnumDescription(CuratorRecommendation.Recommended));
            lstIncluded.Items[0].Tag = Enum.GetName(typeof(CuratorRecommendation), CuratorRecommendation.Recommended);
            lstIncluded.Items.Add(Utility.GetEnumDescription(CuratorRecommendation.NotRecommended));
            lstIncluded.Items[1].Tag = Enum.GetName(typeof(CuratorRecommendation), CuratorRecommendation.NotRecommended);
            lstIncluded.Items.Add(Utility.GetEnumDescription(CuratorRecommendation.Informational));
            lstIncluded.Items[2].Tag = Enum.GetName(typeof(CuratorRecommendation), CuratorRecommendation.Informational);
        }

        public override void LoadFromAutoCat(AutoCat autocat)
        {
            AutoCatCurator ac = autocat as AutoCatCurator;
            if (ac == null)
            {
                return;
            }

            txtCategoryName.Text = ac.CategoryName;
            txtCuratorUrl.Text = ac.CuratorUrl;

            foreach (CuratorRecommendation rec in ac.IncludedRecommendations)
            {
                lstIncluded.Items[rec.GetHashCode()-1].Checked = true;
            }
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatCurator ac = autocat as AutoCatCurator;
            if (ac == null)
            {
                return;
            }

            ac.CategoryName = txtCategoryName.Text;
            ac.CuratorUrl = txtCuratorUrl.Text;

            ac.IncludedRecommendations.Clear();
            foreach (ListViewItem i in lstIncluded.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedRecommendations.Add((CuratorRecommendation)Enum.Parse(typeof(CuratorRecommendation), i.Tag.ToString()));
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