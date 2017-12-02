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
using System.Collections;
using System.Windows.Forms;

namespace Depressurizer.Lib
{
    class ExtListView : ListView
    {
        public event EventHandler SelectionChanged;

        private bool isSelecting;
        private IComparer suspendedComparer;
        private int suspendSortDepth;

        public ExtListView()
        {
            SelectedIndexChanged += ExtListView_SelectedIndexChanged;
        }

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
        /// Suspends sorting until ResumeSorting is called. Does so by clearing the ListViewItemSorter property.
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

        /// <summary>
        /// Resumes sorting after SuspendSorting has been called.
        /// </summary>
        /// <param name="sortNow">If true, will sort immediately.</param>
        public void ResumeSorting(bool sortNow = false)
        {
            if (suspendSortDepth == 0) return;
            if (suspendSortDepth == 1)
            {
                ListViewItemSorter = suspendedComparer;
                suspendedComparer = null;
                if (sortNow) Sort();
            }
            suspendSortDepth--;
        }

        void ExtListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isSelecting)
            {
                isSelecting = true;
                Application.Idle += Application_Idle;
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            isSelecting = false;
            Application.Idle -= Application_Idle;
            if (SelectionChanged != null)
            {
                SelectionChanged(this, new EventArgs());
            }
        }
    }
}