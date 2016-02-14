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

    enum AutoCatYear_Grouping { None, Decade, HalfDecade }

    class AutoCatYear : AutoCat {
        #region Properties
        // Autocat configuration properties
        public string Prefix { get; set; }
        public bool IncludeUnknown { get; set; }
        public string UnknownText { get; set; }
        public AutoCatYear_Grouping GroupingMode { get; set; }

        // Meta properies
        public override AutoCatType AutoCatType {
            get { return Depressurizer.AutoCatType.Year; }
        }

        // Serialization strings
        public const string TypeIdString = "AutoCatYear";
        public const string
            XmlName_Name = "Name",
            XmlName_Filter = "Filter",
            XmlName_Prefix = "Prefix",
            XmlName_IncludeUnknown = "IncludeUnknown",
            XmlName_UnknownText = "UnknownText",
            XmlName_GroupingMode = "GroupingMode";

        #endregion

        #region Construction
        public AutoCatYear( string name, string filter = null, string prefix = null, bool includeUnknown = true, string unknownText = null, AutoCatYear_Grouping groupMode = AutoCatYear_Grouping.None )
            : base( name ) {
            this.Filter = filter;
            this.Prefix = prefix;
            this.IncludeUnknown = includeUnknown;
            this.UnknownText = unknownText;
            this.GroupingMode = groupMode;
        }

        protected AutoCatYear( AutoCatYear other )
            : base( other ) {
            this.Filter = other.Filter;
            this.Prefix = other.Prefix;
            this.IncludeUnknown = other.IncludeUnknown;
            this.UnknownText = other.UnknownText;
            this.GroupingMode = other.GroupingMode;
        }

        public override AutoCat Clone() {
            return new AutoCatYear( this );
        }
        #endregion

        #region Autocategorization Methods
        public override AutoCatResult CategorizeGame( GameInfo game, Filter filter ) {
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

            if (!game.IncludeGame(filter)) return AutoCatResult.Filtered;

            int year = db.GetReleaseYear( game.Id );
            if( year > 0 || IncludeUnknown ) {
                game.AddCategory( games.GetCategory( GetProcessedString( year ) ) );
            }

            return AutoCatResult.Success;
        }

        private string GetProcessedString( int year ) {
            string result = string.Empty;
            if( year <= 0 ) {
                result = UnknownText;
            } else {
                switch( GroupingMode ) {
                    case AutoCatYear_Grouping.Decade:
                        result = GetRangeString( year, 10 );
                        break;
                    case AutoCatYear_Grouping.HalfDecade:
                        result = GetRangeString( year, 5 );
                        break;
                    default:
                        result = year.ToString();
                        break;
                }
            }

            if( string.IsNullOrEmpty( Prefix ) ) {
                return result;
            } else {
                return Prefix + result;
            }
        }

        private string GetRangeString( int year, int rangeSize ) {
            int first = year - year % rangeSize;
            return string.Format( "{0}-{1}", first, first + rangeSize - 1 );
        }
        #endregion

        #region Serialization methods
        public override void WriteToXml( XmlWriter writer ) {
            writer.WriteStartElement( TypeIdString );

            writer.WriteElementString( XmlName_Name, Name );
            if (Filter != null) writer.WriteElementString(XmlName_Filter, Filter);
            if (Prefix != null) writer.WriteElementString(XmlName_Prefix, Prefix);
            writer.WriteElementString( XmlName_IncludeUnknown, IncludeUnknown.ToString() );
            writer.WriteElementString( XmlName_UnknownText, UnknownText );
            writer.WriteElementString( XmlName_GroupingMode, GroupingMode.ToString() );

            writer.WriteEndElement(); // type ID string
        }

        public static AutoCatYear LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlName_Name], TypeIdString );
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            string prefix = XmlUtil.GetStringFromNode( xElement[XmlName_Prefix], null );
            bool includeUnknown = XmlUtil.GetBoolFromNode( xElement[XmlName_IncludeUnknown], true );
            string unknownText = XmlUtil.GetStringFromNode( xElement[XmlName_UnknownText], null );
            AutoCatYear_Grouping groupMode = XmlUtil.GetEnumFromNode<AutoCatYear_Grouping>( xElement[XmlName_GroupingMode], AutoCatYear_Grouping.None );

            return new AutoCatYear( name, filter, prefix, includeUnknown, unknownText, groupMode );
        }
        #endregion
    }
}
