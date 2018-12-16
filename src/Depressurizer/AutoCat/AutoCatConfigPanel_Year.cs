namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Year : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_Year()
        {
            InitializeComponent();
            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
            ttHelp.Ext_SetToolTip(helpUnknown, GlobalStrings.AutoCatYearPanel_Help_Unknown);
        }

        #endregion

        #region Public Methods and Operators

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatYear acYear = autoCat as AutoCatYear;
            if (acYear == null)
            {
                return;
            }

            txtPrefix.Text = acYear.Prefix == null ? string.Empty : acYear.Prefix;
            chkIncludeUnknown.Checked = acYear.IncludeUnknown;
            txtUnknownText.Text = acYear.UnknownText == null ? string.Empty : acYear.UnknownText;
            switch (acYear.GroupingMode)
            {
                case AutoCatYear_Grouping.Decade:
                    radGroupDec.Checked = true;
                    break;
                case AutoCatYear_Grouping.HalfDecade:
                    radGroupHalf.Checked = true;
                    break;
                default:
                    radGroupNone.Checked = true;
                    break;
            }
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatYear ac = autoCat as AutoCatYear;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;
            ac.IncludeUnknown = chkIncludeUnknown.Checked;
            ac.UnknownText = txtUnknownText.Text;
            if (radGroupNone.Checked)
            {
                ac.GroupingMode = AutoCatYear_Grouping.None;
            }
            else if (radGroupHalf.Checked)
            {
                ac.GroupingMode = AutoCatYear_Grouping.HalfDecade;
            }
            else if (radGroupDec.Checked)
            {
                ac.GroupingMode = AutoCatYear_Grouping.Decade;
            }
        }

        #endregion
    }
}
