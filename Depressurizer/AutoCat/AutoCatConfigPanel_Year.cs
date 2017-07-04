/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

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

namespace Depressurizer.AutoCat
{
    public partial class AutoCatConfigPanel_Year : AutoCatConfigPanel
    {
        public AutoCatConfigPanel_Year()
        {
            InitializeComponent();
            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
            ttHelp.Ext_SetToolTip(helpUnknown, GlobalStrings.AutoCatYearPanel_Help_Unknown);
        }

        public override void LoadFromAutoCat(AutoCat ac)
        {
            AutoCatYear acYear = ac as AutoCatYear;
            if (acYear == null)
            {
                return;
            }

            txtPrefix.Text = acYear.Prefix == null ? string.Empty : acYear.Prefix;
            chkIncludeUnknown.Checked = acYear.IncludeUnknown;
            txtUnknownText.Text = acYear.UnknownText == null ? string.Empty : acYear.UnknownText;
            switch (acYear.GroupingMode)
            {
                case AutoCatYearGrouping.Decade:
                    radGroupDec.Checked = true;
                    break;
                case AutoCatYearGrouping.HalfDecade:
                    radGroupHalf.Checked = true;
                    break;
                default:
                    radGroupNone.Checked = true;
                    break;
            }
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatYear ac = autocat as AutoCatYear;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;
            ac.IncludeUnknown = chkIncludeUnknown.Checked;
            ac.UnknownText = txtUnknownText.Text;
            if (radGroupNone.Checked)
            {
                ac.GroupingMode = AutoCatYearGrouping.None;
            }
            else if (radGroupHalf.Checked)
            {
                ac.GroupingMode = AutoCatYearGrouping.HalfDecade;
            }
            else if (radGroupDec.Checked)
            {
                ac.GroupingMode = AutoCatYearGrouping.Decade;
            }
        }
    }
}