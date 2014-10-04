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
        public AutoCatType SelectedType { get; set; }

        private string[] typeNames;
        private AutoCatType[] types;

        public DlgAutoCatCreate( string name = null, AutoCatType type = AutoCatType.None ) {
            InitializeComponent();
            //TODO: literals
            // To add new types to this  dialog, add the name and type to these arrays. The indexes must line up.
            // This method allows adding new types in one place AND allows localization to work properly
            typeNames = new string[] { GlobalStrings.AutoCat_Name_Genre, GlobalStrings.AutoCat_Name_Flags, "Store Tags" };
            types = new AutoCatType[] { AutoCatType.Genre, AutoCatType.Flags, AutoCatType.Tags };

            SelectedName = name;
            SelectedType = type;
        }

        private string TypeToString( AutoCatType t ) {
            if( t == AutoCatType.None ) return null;
            int index = Array.IndexOf( types, t );
            if( index >= 0 && index < typeNames.Length ) return typeNames[index];
            return null;
        }

        private AutoCatType StringToType( string s ) {
            if( s == null ) return AutoCatType.None;
            int index = Array.IndexOf( typeNames, s );
            if( index >= 0 && index < types.Length ) return types[index];
            return AutoCatType.None;
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
