using System;
using System.Collections;
using System.Windows.Forms;

namespace Depressurizer.Lib
{
    internal class ExtListView : ListView
    {
        #region Fields

        private bool isSelecting;

        private IComparer suspendedComparer;

        private int suspendSortDepth;

        #endregion

        #region Constructors and Destructors

        public ExtListView()
        {
            SelectedIndexChanged += ExtListView_SelectedIndexChanged;
        }

        #endregion

        #region Public Events

        public event EventHandler SelectionChanged;

        #endregion

        #region Public Methods and Operators

        public void ExtBeginUpdate()
        {
            BeginUpdate();
            SuspendSorting();
        }

        public void ExtEndUpdate()
        {
            EndUpdate();
            ResumeSorting(true);
        }

        /// <summary>
        ///     Resumes sorting after SuspendSorting has been called.
        /// </summary>
        /// <param name="sortNow">If true, will sort immediately.</param>
        public void ResumeSorting(bool sortNow = false)
        {
            if (suspendSortDepth == 0)
            {
                return;
            }

            if (suspendSortDepth == 1)
            {
                ListViewItemSorter = suspendedComparer;
                suspendedComparer = null;
                if (sortNow)
                {
                    Sort();
                }
            }

            suspendSortDepth--;
        }

        /// <summary>
        ///     Suspends sorting until ResumeSorting is called. Does so by clearing the ListViewItemSorter property.
        /// </summary>
        public void SuspendSorting()
        {
            if (suspendSortDepth == 0)
            {
                suspendedComparer = ListViewItemSorter;
                ListViewItemSorter = null;
            }

            suspendSortDepth++;
        }

        #endregion

        #region Methods

        private void Application_Idle(object sender, EventArgs e)
        {
            isSelecting = false;
            Application.Idle -= Application_Idle;
            if (SelectionChanged != null)
            {
                SelectionChanged(this, new EventArgs());
            }
        }

        private void ExtListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isSelecting)
            {
                isSelecting = true;
                Application.Idle += Application_Idle;
            }
        }

        #endregion
    }
}
