using Depressurizer.Core;
using Depressurizer.Dialogs;

namespace Depressurizer
{
    internal class HltbPrcDlg : CancelableDialog
    {
        #region Constructors and Destructors

        public HltbPrcDlg() : base(GlobalStrings.CDlgHltb_Title, false)
        {
            SetText(GlobalStrings.CDlgHltb_UpdateHltb);
            Updated = 0;
        }

        #endregion

        #region Public Properties

        public int Updated { get; private set; }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        #endregion

        #region Methods

        protected override void Finish()
        {
            if (!Canceled && Error == null)
            {
                OnJobCompletion();
            }
        }

        protected override void RunProcess()
        {
            Updated = Database.UpdateFromHLTB(Settings.Instance.IncludeImputedTimes);
            OnThreadCompletion();
        }

        #endregion
    }
}
