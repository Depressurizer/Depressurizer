using System;
using System.Windows.Forms;
using Depressurizer.Core.Models;

namespace Depressurizer.AutoCats
{
    public partial class AutoCatConfigPanel_Language : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_Language()
        {
            InitializeComponent();

            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);

            FillLanguageLists();
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        #endregion

        #region Public Methods and Operators

        public void FillLanguageLists()
        {
            lstInterface.Items.Clear();
            lstSubtitles.Items.Clear();
            lstFullAudio.Items.Clear();

            LanguageSupport language = Database.AllLanguages;

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

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatLanguage ac = autoCat as AutoCatLanguage;
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

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatLanguage ac = autoCat as AutoCatLanguage;
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

        #endregion

        #region Methods

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

        private void SetAllListCheckStates(ListView list, bool to)
        {
            foreach (ListViewItem item in list.Items)
            {
                item.Checked = to;
            }
        }

        #endregion
    }
}
