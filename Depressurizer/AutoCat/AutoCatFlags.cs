/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

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

        public override AutoCatType AutoCatType => AutoCatType.Flags;

        // AutoCat configuration
        public string Prefix { get; set; }
        public List<string> IncludedFlags { get; set; }

        // Serialization constants
        public const string TypeIdString = "AutoCatFlags";
        private const string
            XmlNameName = "Name",
            XmlNameFilter = "Filter",
            XmlNamePrefix = "Prefix",
            XmlNameFlagList = "Flags",
            XmlNameFlag = "Flag";

        public AutoCatFlags( string name, string filter = null, string prefix = null, List<string> flags = null, bool selected = false)
            : base( name ) {
            Filter = filter;
            Prefix = prefix;
            IncludedFlags = flags ?? new List<string>();
            Selected = selected;
        }

        protected AutoCatFlags( AutoCatFlags other )
            : base( other ) {
            this.Filter = other.Filter;
            this.Prefix = other.Prefix;
            this.IncludedFlags = new List<string>( other.IncludedFlags );
            this.Selected = other.Selected;
        }

        public override AutoCat Clone() => new AutoCatFlags( this );

        public override AutoCatResult CategorizeGame( GameInfo game, Filter filter ) {
            if( Games == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull );
                throw new ApplicationException( GlobalStrings.AutoCatGenre_Exception_NoGameList );
            }
            if( Db == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_DBNull );
                throw new ApplicationException( GlobalStrings.AutoCatGenre_Exception_NoGameDB );
            }
            if( game == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull );
                return AutoCatResult.Failure;
            }

            if( !Db.Contains( game.Id ) || (Db.Games[game.Id].LastStoreScrape == 0) )
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            List<string> gameFlags = Db.GetFlagList( game.Id ) ?? new List<string>();
            IEnumerable<string> categories = gameFlags.Intersect( IncludedFlags );

            foreach( string catString in categories ) {
                Category c = Games.GetCategory( GetProcessedString( catString ) );
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

            writer.WriteElementString( XmlNameName, Name );
            if (Filter != null) writer.WriteElementString(XmlNameFilter, Filter);
            if (Prefix != null) writer.WriteElementString( XmlNamePrefix, Prefix );

            writer.WriteStartElement( XmlNameFlagList );

            foreach( string s in IncludedFlags ) {
                writer.WriteElementString( XmlNameFlag, s );
            }

            writer.WriteEndElement(); // flag list
            writer.WriteEndElement(); // type ID string
        }

        public static AutoCatFlags LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlNameName], TypeIdString );
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            string prefix = XmlUtil.GetStringFromNode( xElement[XmlNamePrefix], null );
            List<string> flags = new List<string>();

            XmlElement flagListElement = xElement[XmlNameFlagList];
            if( flagListElement != null ) {
                XmlNodeList flagElements = flagListElement.SelectNodes( XmlNameFlag );
                foreach( XmlNode n in flagElements ) {
                    string flag;
                    if( XmlUtil.TryGetStringFromNode( n, out flag ) ) {
                        flags.Add( flag );
                    }
                }
            }
            return new AutoCatFlags( name, filter, prefix, flags );
        }

    }
}