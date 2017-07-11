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
    public partial class AutoCatConfigPanel_Language : AutoCatConfigPanel
    {
        public AutoCatConfigPanel_Language()
        {
            InitializeComponent();

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);

            FillLanguageLists();
        }

        public void FillLanguageLists()
        {
            lstInterface.Items.Clear();
            lstSubtitles.Items.Clear();
            lstFullAudio.Items.Clear();

            if (Program.GameDB != null)
            {
                LanguageSupport language = Program.GameDB.GetAllLanguages();

                foreach (string s in language.Interface)
                {
                    lstInterface.Items.Add(s);
                }

                foreach (string s in language.Subtitles)
                {
                    lstSubtitles.Items.Add(s);
                }

                foreach (string s in language.FullAudio)
                {
                    lstFullAudio.Items.Add(s);
                }
            }
        }

        public override void LoadFromAutoCat(AutoCat autocat)
        {
            AutoCatLanguage ac = autocat as AutoCatLanguage;
            if (ac == null)
            {
                return;
            }

            txtPrefix.Text = ac.Prefix;

            chkIncludeTypePrefix.Checked = ac.IncludeTypePrefix;
            chkTypeFallback.Checked = ac.TypeFallback;

            foreach (ListViewItem item in lstInterface.Items)
            {
                item.Checked = ac.IncludedLanguages.Interface.Contains(item.Text);
            }

            foreach (ListViewItem item in lstSubtitles.Items)
            {
                item.Checked = ac.IncludedLanguages.Subtitles.Contains(item.Text);
            }

            foreach (ListViewItem item in lstFullAudio.Items)
            {
                item.Checked = ac.IncludedLanguages.FullAudio.Contains(item.Text);
            }
        }

        public override void SaveToAutoCat(AutoCat autocat)
        {
            AutoCatLanguage ac = autocat as AutoCatLanguage;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;

            ac.IncludeTypePrefix = chkIncludeTypePrefix.Checked;
            ac.TypeFallback = chkTypeFallback.Checked;

            ac.IncludedLanguages.Interface.Clear();
            ac.IncludedLanguages.Subtitles.Clear();
            ac.IncludedLanguages.FullAudio.Clear();

            foreach (ListViewItem i in lstInterface.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedLanguages.Interface.Add(i.Text);
                }
            }

            foreach (ListViewItem i in lstSubtitles.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedLanguages.Subtitles.Add(i.Text);
                }
            }

            foreach (ListViewItem i in lstFullAudio.Items)
            {
                if (i.Checked)
                {
                    ac.IncludedLanguages.FullAudio.Add(i.Text);
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
            SetAllListCheckStates(lstInterface, true);
            SetAllListCheckStates(lstSubtitles, true);
            SetAllListCheckStates(lstFullAudio, true);
        }

        private void cmdUncheckAll_Click(object sender, EventArgs e)
        {
            SetAllListCheckStates(lstInterface, false);
            SetAllListCheckStates(lstSubtitles, false);
            SetAllListCheckStates(lstFullAudio, false);
        }
    }
}