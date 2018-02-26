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

namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Platform : AutoCatConfigPanel
    {
        public AutoCatConfigPanel_Platform()
        {
            InitializeComponent();
            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
        }

        public override void LoadFromAutoCat(AutoCat ac)
        {
            AutoCatPlatform acPlatform = ac as AutoCatPlatform;
            if (acPlatform == null) return;
            txtPrefix.Text = acPlatform.Prefix == null ? string.Empty : acPlatform.Prefix;
            chkboxPlatforms.SetItemChecked(0, acPlatform.Windows);
            chkboxPlatforms.SetItemChecked(1, acPlatform.Mac);
            chkboxPlatforms.SetItemChecked(2, acPlatform.Linux);
            chkboxPlatforms.SetItemChecked(3, acPlatform.SteamOS);
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatPlatform ac = autocat as AutoCatPlatform;
            if (ac == null) return;
            ac.Prefix = txtPrefix.Text;
            ac.Windows = chkboxPlatforms.GetItemChecked(0);
            ac.Mac = chkboxPlatforms.GetItemChecked(1);
            ac.Linux = chkboxPlatforms.GetItemChecked(2);
            ac.SteamOS = chkboxPlatforms.GetItemChecked(3);
        }
    }
}