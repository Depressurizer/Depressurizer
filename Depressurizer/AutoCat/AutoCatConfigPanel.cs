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
using System.Windows.Forms;

namespace Depressurizer {
    public abstract class AutoCatConfigPanel : UserControl {

        public abstract void SaveToAutoCat( AutoCat ac );

        public abstract void LoadFromAutoCat( AutoCat ac );

        public static AutoCatConfigPanel CreatePanel( AutoCat ac, Lib.ExtToolTip ttHelp, GameList ownedGames ) {
            AutoCatType t = ac.AutoCatType;
            switch( t ) {
                case AutoCatType.Genre:
                    return new AutoCatConfigPanel_Genre( ttHelp );
                case AutoCatType.Flags:
                    return new AutoCatConfigPanel_Flags( ttHelp );
                case AutoCatType.Tags:
                    return new AutoCatConfigPanel_Tags( ttHelp, ownedGames );
                default:
                    return null;
            }

        }
    }
}
