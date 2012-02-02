using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

namespace Depressurizer {
    class AppSettings {

        protected readonly object threadLock = new object();

        protected bool outOfDate = false;

        public string FilePath;

        protected AppSettings() {
            FilePath = "Settings.xml";
        }

        public void Save( bool force = false ) {
            if( force || outOfDate ) {
                Type t = this.GetType();

                PropertyInfo[] properties = t.GetProperties();
                XmlDocument doc = new XmlDocument();
                XmlElement config = doc.CreateElement( "config" );
                lock( threadLock ) {
                    foreach( PropertyInfo pi in properties ) {
                        object val = pi.GetValue( this, null );
                        if( val != null ) {
                            XmlElement element = doc.CreateElement( pi.Name );
                            element.InnerText = val.ToString();
                            config.AppendChild( element );
                        }
                    }
                }
                doc.AppendChild( config );
                try {
                    doc.Save( FilePath );
                } catch( IOException ) {
                    // LOG: error
                }
                outOfDate = false;
            }
        }

        public void Load() {
            Type type = this.GetType();
            if( File.Exists( FilePath ) ) {
                XmlDocument doc = new XmlDocument();
                try {
                    doc.Load( FilePath );
                    XmlNode configNode = doc.SelectSingleNode( "/config" );
                    lock( threadLock ) {
                        foreach( XmlNode node in configNode.ChildNodes ) {
                            string name = node.Name;
                            string value = node.InnerText;
                            PropertyInfo pi = type.GetProperty( name );
                            if( pi != null ) {
                                this.SetProperty( pi, value );
                            }
                        }
                    }
                } catch( XmlException ) {
                    // LOG: Error.
                } catch( IOException ) {
                    // LOG: Error.
                }
                outOfDate = false;
            }
        }

        private void SetProperty( PropertyInfo propertyInfo, string value ) {
            try {
                if( propertyInfo.PropertyType == typeof( string ) ) {
                    propertyInfo.SetValue( this, value, null );
                } else if( propertyInfo.PropertyType == typeof( bool ) ) {
                    bool val;
                    if( bool.TryParse( value, out val ) ) {
                        propertyInfo.SetValue( this, val, null );
                    }
                } else if( propertyInfo.PropertyType == typeof( int ) ) {
                    int val;
                    if( int.TryParse( value, out val ) ) {
                        propertyInfo.SetValue( this, val, null );
                    }
                }
            } catch {
                // LOG: Log the error
            }
        }
    }
}
