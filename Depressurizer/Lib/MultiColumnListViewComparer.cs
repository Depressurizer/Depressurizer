/*
Copyright 2011, 2012, 2013 Steve Labbe.

This file is part of Depressurizer.

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
using System.Collections.Generic;
using System.Windows.Forms;

namespace Depressurizer {
    /// <summary>
    /// Implements the manual sorting of ListView items by columns. Supports sorting string representations of integers numerically.
    /// </summary>
    public class MultiColumnListViewComparer : IComparer {
        private int _col;
        private int _direction;
        private bool _asInt;
        private bool _rev;

        private HashSet<int> _intCols = new HashSet<int>();
        private HashSet<int> _revCols = new HashSet<int>();

        public MultiColumnListViewComparer( int column = 0, int dir = 1 ) {
            this._col = column;
            this._direction = dir;
            this._asInt = _intCols.Contains( _col );
        }

        public void SetSortCol( int clickedCol, int forceDir = 0 ) {

            if( forceDir == 0 ) {
                if( clickedCol == _col ) {
                    _direction = -_direction;
                } else {
                    _direction = 1;
                }
            } else {
                _direction = forceDir;
            }

            _col = clickedCol;
            _asInt = _intCols.Contains( _col );
            _rev = _revCols.Contains( _col );
        }

        public int GetSortCol() {
            return _col;
        }

        public int GetSortDir() {
            return _direction;
        }

        public void AddIntCol( int col ) {
            _intCols.Add( col );
            _asInt = _intCols.Contains( _col );
        }

        public void RemoveIntCol( int col ) {
            _intCols.Remove( col );
            _asInt = _intCols.Contains( _col );
        }

        public void AddRevCol( int col ) {
            _revCols.Add( col );
            _rev = _revCols.Contains( _col );
        }

        public void RemoveRevCol( int col ) {
            _revCols.Remove( col );
            _rev = _revCols.Contains( _col );
        }

        public int Compare( object x, object y ) {
            int dir = _direction * ( _rev ? -1 : 1 );
            if( _asInt ) {
                int a, b;
                if( int.TryParse( ( (ListViewItem)x ).SubItems[_col].Text, out a ) && int.TryParse( ( (ListViewItem)y ).SubItems[_col].Text, out b ) ) {
                    return dir * ( a - b );
                }
            }
            return dir * String.Compare( ( (ListViewItem)x ).SubItems[_col].Text, ( (ListViewItem)y ).SubItems[_col].Text );
        }
    }
}
