using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Depressurizer {
    public partial class AutoCatConfigPanel_Genre : UserControl {
        public AutoCatConfigPanel_Genre() {
            InitializeComponent();
        }

        public void FillGenreList() {
            genre_lstIgnore.Items.Clear();

            if( Program.GameDB != null ) {
                SortedSet<string> genreList = Program.GameDB.GetAllGenres();
            
                foreach( string s in genreList ) {
                    genre_lstIgnore.Items.Add( s );
                }
            }
        }
    }
}
