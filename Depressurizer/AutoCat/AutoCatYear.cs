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
using Rallion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Depressurizer {
    class AutoCatYear : AutoCat {
        #region Properties
        // Autocat configuration properties
        public string Prefix { get; set; }

        // Meta properies
        public override AutoCatType AutoCatType {
            get { return Depressurizer.AutoCatType.Year; }
        }

        // Serialization strings
        public const string TypeIdString = "AutoCatYear";
        public const string
            XmlName_Name = "Name",
            XmlName_Prefix = "Prefix";

        #endregion

        #region Construction
        public AutoCatYear( string name, string prefix = "" )
            : base( name ) {
            this.Prefix = prefix;
        }

        protected AutoCatYear( AutoCatYear other )
            : base( other ) {
            this.Prefix = other.Prefix;
        }

        public override AutoCat Clone() {
            return new AutoCatYear( this );
        }
        #endregion

        #region Autocategorization Methods
        public override AutoCatResult CategorizeGame( GameInfo game ) {
            if( games == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull );
                throw new ApplicationException( GlobalStrings.AutoCatGenre_Exception_NoGameList );
            }
            if( db == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_DBNull );
                throw new ApplicationException( GlobalStrings.AutoCatGenre_Exception_NoGameDB );
            }
            if( game == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull );
                return AutoCatResult.Failure;
            }

            if( !db.Contains( game.Id ) ) return AutoCatResult.NotInDatabase;

            game.AddCategory( games.GetCategory( GetProcessedString( db.GetReleaseYear( game.Id ) ) ) );

            return AutoCatResult.Success;
        }

        private string GetProcessedString( string baseString ) {
            if( string.IsNullOrEmpty( Prefix ) ) {
                return baseString;
            } else {
                return Prefix + baseString;
            }
        }
        #endregion

        #region Serialization methods
        public override void WriteToXml( XmlWriter writer ) {
            writer.WriteStartElement( TypeIdString );

            writer.WriteElementString( XmlName_Name, Name );
            writer.WriteElementString( XmlName_Prefix, Prefix );

            writer.WriteEndElement(); // type ID string
        }

        public static AutoCatYear LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlName_Name], TypeIdString );
            string prefix = XmlUtil.GetStringFromNode( xElement[XmlName_Prefix], null );

            return new AutoCatYear( name, prefix );
        }
        #endregion
    }
}
