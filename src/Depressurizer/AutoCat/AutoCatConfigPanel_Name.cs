#region LICENSE

//     This file (AutoCatConfigPanel_Name.cs) is part of Depressurizer.
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

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Name : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_Name()
        {
            InitializeComponent();
            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
        }

        #endregion

        #region Public Methods and Operators

        public override void LoadFromAutoCat(AutoCat ac)
        {
            AutoCatName acName = ac as AutoCatName;
            if (acName == null)
            {
                return;
            }

            txtPrefix.Text = acName.Prefix == null ? string.Empty : acName.Prefix;
            cbSkipThe.Checked = acName.SkipThe;
            cbGroupNumbers.Checked = acName.GroupNumbers;
            chkgroupNonEnglishCharacters.Checked = acName.GroupNonEnglishCharacters;
            txtGroupNonEnglishCharactersText.Text = acName.GroupNonEnglishCharactersText;
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatName ac = autocat as AutoCatName;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;
            ac.GroupNumbers = cbGroupNumbers.Checked;
            ac.SkipThe = cbSkipThe.Checked;
            ac.GroupNonEnglishCharacters = chkgroupNonEnglishCharacters.Checked;
            ac.GroupNonEnglishCharactersText = txtGroupNonEnglishCharactersText.Text;
        }

        #endregion
    }
}
