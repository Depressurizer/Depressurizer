#region LICENSE

//     This file (ExtListView.cs) is part of Depressurizer.
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
