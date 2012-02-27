using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DPLib {
    /// <summary>
    /// Implements the manual sorting of ListView items by columns. Supports sorting string representations of integers numerically.
    /// </summary>
    public class MultiColumnListViewComparer : IComparer {
        private int _col;
        private int _direction;
        private bool _asInt;

        private HashSet<int> _intCols = new HashSet<int>();

        public MultiColumnListViewComparer( int column = 0, int dir = 1 ) {
            this._col = column;
            this._direction = dir;
            this._asInt = _intCols.Contains( _col );
        }

        public void ColClick( int clickedCol ) {
            _direction = ( clickedCol == _col ) ? -_direction : 1;
            _col = clickedCol;
            _asInt = _intCols.Contains( _col );
        }

        public void AddIntCol( int col ) {
            _intCols.Add( col );
        }

        public int Compare( object x, object y ) {
            if( _asInt ) {
                int a, b;
                if( int.TryParse( ( (ListViewItem)x ).SubItems[_col].Text, out a ) && int.TryParse( ( (ListViewItem)y ).SubItems[_col].Text, out b ) ) {
                    return _direction * ( a - b );
                }
            }
            return _direction * String.Compare( ( (ListViewItem)x ).SubItems[_col].Text, ( (ListViewItem)y ).SubItems[_col].Text );
        }
    }
}
