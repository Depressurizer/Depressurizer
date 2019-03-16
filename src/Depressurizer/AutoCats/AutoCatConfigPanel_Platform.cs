namespace Depressurizer
{
    public partial class AutoCatConfigPanel_Platform : AutoCatConfigPanel
    {
        #region Constructors and Destructors

        public AutoCatConfigPanel_Platform()
        {
            InitializeComponent();
            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
        }

        #endregion

        #region Public Methods and Operators

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatPlatform acPlatform = autoCat as AutoCatPlatform;
            if (acPlatform == null)
            {
                return;
            }

            txtPrefix.Text = acPlatform.Prefix == null ? string.Empty : acPlatform.Prefix;
            chkboxPlatforms.SetItemChecked(0, acPlatform.Windows);
            chkboxPlatforms.SetItemChecked(1, acPlatform.Mac);
            chkboxPlatforms.SetItemChecked(2, acPlatform.Linux);
            chkboxPlatforms.SetItemChecked(3, acPlatform.SteamOS);
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatPlatform ac = autoCat as AutoCatPlatform;
            if (ac == null)
            {
                return;
            }

            ac.Prefix = txtPrefix.Text;
            ac.Windows = chkboxPlatforms.GetItemChecked(0);
            ac.Mac = chkboxPlatforms.GetItemChecked(1);
            ac.Linux = chkboxPlatforms.GetItemChecked(2);
            ac.SteamOS = chkboxPlatforms.GetItemChecked(3);
        }

        #endregion
    }
}
