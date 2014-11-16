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
using System.Xml;

namespace Depressurizer {

    public class AutoCatFlags : AutoCat {

        public override AutoCatType AutoCatType {
            get { return AutoCatType.Flags; }
        }

        // AutoCat configuration
        public string Prefix { get; set; }
        public List<string> IncludedFlags { get; set; }

        // Serialization constants
        public const string TypeIdString = "AutoCatFlags";
        private const string
            XmlName_Name = "Name",
            XmlName_Prefix = "Prefix",
            XmlName_FlagList = "Flags",
            XmlName_Flag = "Flag";

        public AutoCatFlags( string name, string prefix = "", List<string> flags = null )
            : base( name ) {
            Prefix = prefix;
            IncludedFlags = ( flags == null ) ? ( new List<string>() ) : flags;
        }

        protected AutoCatFlags( AutoCatFlags other )
            : base( other ) {
            this.Prefix = other.Prefix;
            this.IncludedFlags = new List<string>( other.IncludedFlags );
        }

        public override AutoCat Clone() {
            return new AutoCatFlags( this );
        }

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

            List<string> gameFlags = db.GetFlagList( game.Id );
            if( gameFlags == null ) gameFlags = new List<string>();
            IEnumerable<string> categories = gameFlags.Intersect( IncludedFlags );

            foreach( string catString in categories ) {
                Category c = games.GetCategory( GetProcessedString( catString ) );
                game.AddCategory( c );
            }
            return AutoCatResult.Success;
        }

        private string GetProcessedString( string baseString ) {
            if( string.IsNullOrEmpty( Prefix ) ) {
                return baseString;
            } else {
                return Prefix + baseString;
            }
        }

        public override void WriteToXml( XmlWriter writer ) {
            writer.WriteStartElement( TypeIdString );

            writer.WriteElementString( XmlName_Name, Name );
            writer.WriteElementString( XmlName_Prefix, Prefix );

            writer.WriteStartElement( XmlName_FlagList );

            foreach( string s in IncludedFlags ) {
                writer.WriteElementString( XmlName_Flag, s );
            }

            writer.WriteEndElement(); // flag list
            writer.WriteEndElement(); // type ID string
        }

        public static AutoCatFlags LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlName_Name], TypeIdString );
            string prefix = XmlUtil.GetStringFromNode( xElement[XmlName_Prefix], null );
            List<string> flags = new List<string>();

            XmlElement flagListElement = xElement[XmlName_FlagList];
            if( flagListElement != null ) {
                XmlNodeList flagElements = flagListElement.SelectNodes( XmlName_Flag );
                foreach( XmlNode n in flagElements ) {
                    string flag;
                    if( XmlUtil.TryGetStringFromNode( n, out flag ) ) {
                        flags.Add( flag );
                    }
                }
            }
            return new AutoCatFlags( name, prefix, flags );
        }

    }
}