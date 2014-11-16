using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
