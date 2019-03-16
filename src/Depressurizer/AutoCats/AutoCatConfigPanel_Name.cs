namespace Depressurizer.AutoCats
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

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatName acName = autoCat as AutoCatName;
            if (acName == null)
            {
                return;
            }

            txtPrefix.Text = acName.Prefix ?? string.Empty;
            cbSkipThe.Checked = acName.SkipThe;
            cbGroupNumbers.Checked = acName.GroupNumbers;
            chkgroupNonEnglishCharacters.Checked = acName.GroupNonEnglishCharacters;
            txtGroupNonEnglishCharactersText.Text = acName.GroupNonEnglishCharactersText;
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatName ac = autoCat as AutoCatName;
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
