using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Depressurizer.Lib {
    class ExtListView : ListView{

        public event EventHandler SelectionChanged;

        private bool isSelecting = false;

        public ExtListView()
            : base() {
                this.SelectedIndexChanged += ExtListView_SelectedIndexChanged;
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
