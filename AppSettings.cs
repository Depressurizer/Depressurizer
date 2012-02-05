using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Depressurizer {

    /// <summary>
    /// Base class for a settings object. Capable of loading and saving values of all public properties.
    /// </summary>
    abstract class AppSettings {

        protected readonly object threadLock = new object();

        protected bool outOfDate = false;

        public string FilePath;

        protected AppSettings() {
            FilePath = "Settings.xml";
        }

        /// <summary>
        /// Saves the contents of this instance to the defined config file.
        /// </summary>
        /// <param name="force">If false, will only save if the flag indicates that changes have been made. If true, always saves.</param>
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
                }
                outOfDate = false;
            }
        }

        /// <summary>
        /// Loads settings from the defined config file.
        /// </summary>
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
                } catch( IOException ) {
                }
                outOfDate = false;
            }
        }

        private void SetProperty( PropertyInfo propertyInfo, string value ) {
            try {
                if( propertyInfo.PropertyType.IsEnum ) {
                    object eVal = Enum.Parse( propertyInfo.PropertyType, value, true );
                    propertyInfo.SetValue( this, eVal, null );
                } else if( propertyInfo.PropertyType == typeof( string ) ) {
                    propertyInfo.SetValue( this, value, null );
                } else if( propertyInfo.PropertyType == typeof( bool ) ) {
                    propertyInfo.SetValue( this, bool.Parse( value ), null );
                } else if( propertyInfo.PropertyType == typeof( int ) ) {
                    propertyInfo.SetValue( this, int.Parse( value ), null );
                }
            } catch {
            }
        }
    }
}
