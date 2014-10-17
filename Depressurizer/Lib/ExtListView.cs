using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Depressurizer.Lib {
    class ExtListView : ListView {

        public event EventHandler SelectionChanged;

        private bool isSelecting = false;
        private IComparer suspendedComparer;
        private int suspendSortDepth = 0;

        public ExtListView()
            : base() {
            this.SelectedIndexChanged += ExtListView_SelectedIndexChanged;
        }

        public void ExtBeginUpdate() {
            BeginUpdate();
            SuspendSorting();
        }

        public void ExtEndUpdate() {
            EndUpdate();
            ResumeSorting( true );
        }

        /// <summary>
        /// Suspends sorting until ResumeSorting is called. Does so by clearing the ListViewItemSorter property.
        /// </summary>
        public void SuspendSorting() {
            if( suspendSortDepth == 0 ) {
                suspendedComparer = this.ListViewItemSorter;
                this.ListViewItemSorter = null;
            }
            suspendSortDepth++;
        }

        /// <summary>
        /// Resumes sorting after SuspendSorting has been called.
        /// </summary>
        /// <param name="sortNow">If true, will sort immediately.</param>
        public void ResumeSorting( bool sortNow = false ) {
            if( suspendSortDepth == 0 ) return;
            if( suspendSortDepth == 1 ) {
                this.ListViewItemSorter = suspendedComparer;
                suspendedComparer = null;
                if( sortNow ) Sort();
            }
            suspendSortDepth--;
        }

        void ExtListView_SelectedIndexChanged( object sender, EventArgs e ) {
            if( !isSelecting ) {
                isSelecting = true;
                Application.Idle += Application_Idle;
            }
        }

        void Application_Idle( object sender, EventArgs e ) {
            isSelecting = false;
            Application.Idle -= Application_Idle;
            if( SelectionChanged != null ) {
                SelectionChanged( this, new EventArgs() );
            }
        }


    }
}
