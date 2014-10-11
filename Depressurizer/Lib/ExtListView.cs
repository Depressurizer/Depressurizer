using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Depressurizer.Lib {
    class ExtListView : ListView{

        public event EventHandler SelectionChanged;

        private bool isSelecting = false;
        private IComparer suspendedComparer;

        public ExtListView()
            : base() {
                this.SelectedIndexChanged += ExtListView_SelectedIndexChanged;
        }

        /// <summary>
        /// Suspends sorting until ResumeSorting is called. Does so by clearing the ListViewItemSorter property.
        /// </summary>
        public void SuspendSorting() {
            suspendedComparer = this.ListViewItemSorter;
            this.ListViewItemSorter = null;
        }

        /// <summary>
        /// Resumes sorting after SuspendSorting has been called.
        /// </summary>
        /// <param name="sortNow">If true, will sort immediately.</param>
        public void ResumeSorting( bool sortNow = false ) {
            this.ListViewItemSorter = suspendedComparer;
            suspendedComparer = null;
            if( sortNow ) Sort();
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
            SelectionChanged( this, new EventArgs() );
        }

        
    }
}
