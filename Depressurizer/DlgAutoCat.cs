using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Depressurizer {
    public partial class DlgAutoCat : Form {
        public List<AutoCat> AutoCatList;

        public DlgAutoCat( List<AutoCat> autoCats ) {
            InitializeComponent();

            AutoCatList = new List<AutoCat>();

            foreach( AutoCat c in autoCats ) {
                AutoCatList.Add( c.Clone() );
            }
        }
    }
}
