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
    public partial class DlgAutoCatCreate : Form {
        public string SelectedName { get; set; }
        public Type SelectedType { get; set; }

        private string[] typeNames;
        private Type[] typeObjs;

        public DlgAutoCatCreate( string name = null, Type type = null ) {
            InitializeComponent();

            // To add new types to this  dialog, add the name and type to these arrays. The indexes must line up.
            // This method allows adding new types in one place AND allows localization to work properly
            typeNames = new string[] { GlobalStrings.AutoCat_Name_Genre, GlobalStrings.AutoCat_Name_Flags };
            typeObjs = new Type[] { typeof( AutoCatGenre ), typeof( AutoCatFlags ) };

            SelectedName = name;
            SelectedType = type;
        }

        private string TypeToString( Type t ) {
            if( t == null ) return null;
            int index = Array.IndexOf( typeObjs, t );
            if( index >= 0 && index < typeNames.Length ) return typeNames[index];
            return null;
        }

        private Type StringToType( string s ) {
            if( s == null ) return null;
            int index = Array.IndexOf( typeNames, s );
            if( index >= 0 && index < typeObjs.Length ) return typeObjs[index];
            return null;
        }

        private void SaveUIToFields() {
            SelectedName = txtName.Text;
            SelectedType = StringToType( cmbType.SelectedItem as string );
        }

        private void LoadUIFromFields() {
            if( SelectedName == null ) {
                txtName.Clear();
            } else {
                txtName.Text = SelectedName;
            }

            string selString = TypeToString( SelectedType );
            if( selString == null ) {
                cmbType.SelectedIndex = 0;
            } else {
                cmbType.SelectedItem = selString;
            }
        }

        private void DlgAutoCatCreate_Load( object sender, EventArgs e ) {
            foreach( string s in typeNames ) {
                cmbType.Items.Add( s );
            }
            LoadUIFromFields();
        }

        private void cmdCreate_Click( object sender, EventArgs e ) {
            SaveUIToFields();
        }
    }
}
