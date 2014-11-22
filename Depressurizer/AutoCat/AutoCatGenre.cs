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
using System.Xml;

namespace Depressurizer {

    /// <summary>
    /// Autocategorization scheme that adds genre categories.
    /// </summary>
    public class AutoCatGenre : AutoCat {

        public override AutoCatType AutoCatType {
            get { return AutoCatType.Genre; }
        }

        // Autocat configuration
        public int MaxCategories { get; set; }
        public bool RemoveOtherGenres { get; set; }
        public string Prefix { get; set; }

        public List<string> IgnoredGenres { get; set; }

        // Serialization keys
        public const string TypeIdString = "AutoCatGenre";
        private const string
            XmlName_Name = "Name",
            XmlName_RemOther = "RemoveOthers",
            XmlName_MaxCats = "MaxCategories",
            XmlName_Prefix = "Prefix",
            XmlName_IgnoreList = "Ignored",
            XmlName_IgnoreItem = "Ignore";

        const int MAX_PARENT_DEPTH = 3;

        private SortedSet<Category> genreCategories;

        /// <summary>
        /// Creates a new AutoCatGenre object, which autocategorizes games based on the genres in the Steam store.
        /// </summary>
        /// <param name="db">Reference to GameDB to use</param>
        /// <param name="games">Reference to the GameList to act on</param>
        /// <param name="maxCategories">Maximum number of categories to assign per game. 0 indicates no limit.</param>
        /// <param name="removeOthers">If true, removes any OTHER genre-named categories from each game processed. Will not remove categories that do not match a genre found in the database.</param>
        public AutoCatGenre( string name, string prefix = "", int maxCategories = 0, bool removeOthers = false, List<string> ignore = null )
            : base( name ) {
            MaxCategories = maxCategories;
            RemoveOtherGenres = removeOthers;
            Prefix = prefix;
            IgnoredGenres = ( ignore == null ) ? new List<string>() : ignore;
        }

        protected AutoCatGenre( AutoCatGenre other )
            : base( other ) {
            this.MaxCategories = other.MaxCategories;
            this.RemoveOtherGenres = other.RemoveOtherGenres;
            this.Prefix = other.Prefix;
            this.IgnoredGenres = new List<string>( other.IgnoredGenres );
        }

        public override AutoCat Clone() {
            return new AutoCatGenre( this );
        }

        /// <summary>
        /// Prepares to categorize games. Prepares a list of genre categories to remove. Does nothing if removeothergenres is false.
        /// </summary>
        public override void PreProcess( GameList games, GameDB db ) {
            base.PreProcess( games, db );
            if( RemoveOtherGenres ) {

                SortedSet<string> genreStrings = db.GetAllGenres();
                genreCategories = new SortedSet<Category>();

                foreach( string cStr in genreStrings ) {
                    if( games.CategoryExists( String.IsNullOrEmpty( Prefix ) ? ( cStr ) : ( Prefix + cStr ) ) && !IgnoredGenres.Contains( cStr ) ) {
                        genreCategories.Add( games.GetCategory( cStr ) );
                    }
                }
            }
        }

        public override void DeProcess() {
            base.DeProcess();
            this.genreCategories = null;
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

            if( !db.Contains( game.Id ) || db.Games[game.Id].LastStoreScrape == 0 ) return AutoCatResult.NotInDatabase;

            if( RemoveOtherGenres && genreCategories != null ) {
                game.RemoveCategory( genreCategories );
            }

            List<string> genreList = db.GetGenreList( game.Id );
            if( genreList != null && genreList.Count > 0 ) {
                List<Category> categories = new List<Category>();
                int max = MaxCategories;
                for( int i = 0; i < genreList.Count && ( MaxCategories == 0 || i < max ); i++ ) {
                    if( !IgnoredGenres.Contains( genreList[i] ) ) {
                        categories.Add( games.GetCategory( GetProcessedString( genreList[i] ) ) );
                    } else {
                        max++; // ignored genres don't contribute to max
                    }
                }

                game.AddCategory( categories );
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
            if( Prefix != null ) writer.WriteElementString( XmlName_Prefix, Prefix );
            writer.WriteElementString( XmlName_MaxCats, MaxCategories.ToString() );
            writer.WriteElementString( XmlName_RemOther, RemoveOtherGenres.ToString() );

            writer.WriteStartElement( XmlName_IgnoreList );

            foreach( string s in IgnoredGenres ) {
                writer.WriteElementString( XmlName_IgnoreItem, s );
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        public static AutoCatGenre LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlName_Name], TypeIdString );
            int maxCats = XmlUtil.GetIntFromNode( xElement[XmlName_MaxCats], 0 );
            bool remOther = XmlUtil.GetBoolFromNode( xElement[XmlName_RemOther], false );
            string prefix = XmlUtil.GetStringFromNode( xElement[XmlName_Prefix], null );

            List<string> ignore = new List<string>();

            XmlElement ignoreListElement = xElement[XmlName_IgnoreList];
            if( ignoreListElement != null ) {
                XmlNodeList ignoreNodes = ignoreListElement.SelectNodes( XmlName_IgnoreItem );
                foreach( XmlNode node in ignoreNodes ) {
                    string s;
                    if( XmlUtil.TryGetStringFromNode( node, out s ) ) {
                        ignore.Add( s );
                    }
                }
            }

            AutoCatGenre result = new AutoCatGenre( name, prefix, maxCats, remOther, ignore );
            return result;
        }
    }

}