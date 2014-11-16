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
    public enum AutoCatType {
        None,
        Genre,
        Flags,
        Tags
    }

    public enum AutoCatResult {
        Success,
        Failure,
        NotInDatabase
    }

    /// <summary>
    /// Abstract base class for autocategorization schemes. Call PreProcess before any set of autocat operations.
    /// This is a preliminary form, and may change in future versions.
    /// Returning only true / false on a categorization attempt may prove too simplistic.
    /// </summary>
    public abstract class AutoCat : IComparable {

        protected GameList games;
        protected GameDB db;

        public abstract AutoCatType AutoCatType {
            get;
        }

        public string Name { get; set; }

        public override string ToString() {
            return Name;
        }

        protected AutoCat( string name ) {
            Name = name;
        }

        protected AutoCat( AutoCat other ) {
            Name = other.Name;
        }

        public abstract AutoCat Clone();

        public int CompareTo( object other ) {
            if( other is AutoCat ) {
                return string.Compare( this.Name, ( other as AutoCat ).Name );
            } else {
                return 1;
            }
        }

        /// <summary>
        /// Must be called before any categorizations are done. Should be overridden to perform any necessary database analysis or other preparation.
        /// After this is called, no configuration options should be changed before using CategorizeGame.
        /// </summary>
        public virtual void PreProcess( GameList games, GameDB db ) {
            this.games = games;
            this.db = db;
        }

        /// <summary>
        /// Applies this autocategorization scheme to the game with the given ID.
        /// </summary>
        /// <param name="gameId">The game ID to process</param>
        /// <returns>False if the game was not found in database. This allows the calling function to potentially re-scrape data and reattempt.</returns>
        public virtual AutoCatResult CategorizeGame( int gameId ) {
            if( games.Games.ContainsKey( gameId ) ) {
                return CategorizeGame( games.Games[gameId] );
            }
            return AutoCatResult.Failure;
        }

        /// <summary>
        /// Applies this autocategorization scheme to the game with the given ID.
        /// </summary>
        /// <param name="game">The GameInfo object to process</param>
        /// <returns>False if the game was not found in database. This allows the calling function to potentially re-scrape data and reattempt.</returns>
        public abstract AutoCatResult CategorizeGame( GameInfo game );

        public virtual void DeProcess() {
            games = null;
            db = null;
        }

        public abstract void WriteToXml( XmlWriter writer );

        public static AutoCat LoadACFromXmlElement( XmlElement xElement ) {
            string type = xElement.Name;

            AutoCat result = null;
            switch( type ) {
                case AutoCatGenre.TypeIdString:
                    result = AutoCatGenre.LoadFromXmlElement( xElement );
                    break;
                case AutoCatFlags.TypeIdString:
                    result = AutoCatFlags.LoadFromXmlElement( xElement );
                    break;
                case AutoCatTags.TypeIdString:
                    result = AutoCatTags.LoadFromXmlElement( xElement );
                    break;
                default:
                    break;
            }
            return result;
        }

        public static AutoCat Create( AutoCatType type, string name ) {
            switch( type ) {
                case AutoCatType.Genre:
                    return new AutoCatGenre( name );
                case AutoCatType.Flags:
                    return new AutoCatFlags( name );
                case AutoCatType.Tags:
                    return new AutoCatTags( name );
                default:
                    return null;
            }
        }
    }

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

            if( !db.Contains( game.Id ) ) return AutoCatResult.NotInDatabase;

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

    public class AutoCatTags : AutoCat {

        public override AutoCatType AutoCatType {
            get { return AutoCatType.Tags; }
        }

        public string Prefix { get; set; }
        public int MaxTags { get; set; }
        public HashSet<string> IncludedTags { get; set; }

        public bool ListOwnedOnly { get; set; }
        public float ListWeightFactor { get; set; }
        public int ListMinScore { get; set; }
        public int ListTagsPerGame { get; set; }
        public bool ListScoreSort { get; set; }
        public bool ListExcludeGenres { get; set; }

        public const string TypeIdString = "AutoCatTags";
        private const string XmlName_Name = "Name",
            XmlName_Prefix = "Prefix",
            XmlName_TagList = "Tags",
            XmlName_Tag = "Tag",
            XmlName_MaxTags = "MaxTags",
            XmlName_ListOwnedOnly = "List_OwnedOnly",
            XmlName_ListWeightFactor = "List_WeightedScore",
            XmlName_ListMinScore = "List_MinScore",
            XmlName_ListTagsPerGame = "List_TagsPerGame",
            XmlName_ListExcludeGenres = "List_ExcludeGenres",
            XmlName_ListScoreSort = "List_ScoreSort";

        public AutoCatTags( string name, string prefix = "",
            HashSet<string> tags = null, int maxTags = 0,
            bool listOwnedOnly = true, float listWeightFactor = 1, int listMinScore = 0, int listTagsPerGame = 0, bool listScoreSort = true, bool listExcludeGenres = true )
            : base( name ) {
            this.Prefix = prefix;

            if( tags == null ) IncludedTags = new HashSet<string>();
            else IncludedTags = tags;

            this.MaxTags = maxTags;
            this.ListOwnedOnly = listOwnedOnly;
            this.ListWeightFactor = listWeightFactor;
            this.ListMinScore = listMinScore;
            this.ListTagsPerGame = listTagsPerGame;
            this.ListScoreSort = listScoreSort;
            this.ListExcludeGenres = listExcludeGenres;
        }

        protected AutoCatTags( AutoCatTags other )
            : base( other ) {
            this.Prefix = other.Prefix;
            this.IncludedTags = new HashSet<string>( other.IncludedTags );
            this.MaxTags = other.MaxTags;

            this.ListOwnedOnly = other.ListOwnedOnly;
            this.ListWeightFactor = other.ListWeightFactor;
            this.ListMinScore = other.ListMinScore;
            this.ListTagsPerGame = other.ListTagsPerGame;
            this.ListScoreSort = other.ListScoreSort;
            this.ListExcludeGenres = other.ListExcludeGenres;
        }

        public override AutoCat Clone() {
            return new AutoCatTags( this );
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

            List<string> gameTags = db.GetTagList( game.Id );

            if( gameTags != null ) {
                int added = 0;
                for( int index = 0; index < gameTags.Count && ( MaxTags == 0 || added < MaxTags ); index++ ) {
                    if( IncludedTags.Contains( gameTags[index] ) ) {
                        game.AddCategory( games.GetCategory( GetProcessedString( gameTags[index] ) ) );
                        added++;
                    }
                }
            }

            return AutoCatResult.Success;
        }

        public string GetProcessedString( string s ) {
            if( string.IsNullOrEmpty( Prefix ) ) {
                return s;
            } else {
                return Prefix + s;
            }
        }

        public override void WriteToXml( XmlWriter writer ) {
            writer.WriteStartElement( TypeIdString );

            writer.WriteElementString( XmlName_Name, Name );
            if( !string.IsNullOrEmpty( Prefix ) ) writer.WriteElementString( XmlName_Prefix, Prefix );
            writer.WriteElementString( XmlName_MaxTags, MaxTags.ToString() );

            if( IncludedTags != null && IncludedTags.Count > 0 ) {
                writer.WriteStartElement( XmlName_TagList );
                foreach( string s in IncludedTags ) {
                    writer.WriteElementString( XmlName_Tag, s );
                }
                writer.WriteEndElement();
            }

            writer.WriteElementString( XmlName_ListOwnedOnly, ListOwnedOnly.ToString() );
            writer.WriteElementString( XmlName_ListWeightFactor, ListWeightFactor.ToString() );
            writer.WriteElementString( XmlName_ListMinScore, ListMinScore.ToString() );
            writer.WriteElementString( XmlName_ListTagsPerGame, ListTagsPerGame.ToString() );
            writer.WriteElementString( XmlName_ListScoreSort, ListScoreSort.ToString() );
            writer.WriteElementString( XmlName_ListExcludeGenres, ListExcludeGenres.ToString() );

            writer.WriteEndElement();
        }

        public static AutoCatTags LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlName_Name], TypeIdString );

            AutoCatTags result = new AutoCatTags( name );

            string prefix;
            if( XmlUtil.TryGetStringFromNode( xElement[XmlName_Prefix], out prefix ) ) result.Prefix = prefix;

            int maxTags;
            if( XmlUtil.TryGetIntFromNode( xElement[XmlName_MaxTags], out maxTags ) ) result.MaxTags = maxTags;

            bool listOwnedOnly;
            if( XmlUtil.TryGetBoolFromNode( xElement[XmlName_ListOwnedOnly], out listOwnedOnly ) ) result.ListOwnedOnly = listOwnedOnly;

            float listWeightFactor;
            if( XmlUtil.TryGetFloatFromNode( xElement[XmlName_ListWeightFactor], out listWeightFactor ) ) result.ListWeightFactor = listWeightFactor;

            int listMinScore;
            if( XmlUtil.TryGetIntFromNode( xElement[XmlName_ListMinScore], out listMinScore ) ) result.ListMinScore = listMinScore;

            int listTagsPerGame;
            if( XmlUtil.TryGetIntFromNode( xElement[XmlName_ListTagsPerGame], out listTagsPerGame ) ) result.ListTagsPerGame = listTagsPerGame;

            bool listScoreSort;
            if( XmlUtil.TryGetBoolFromNode( xElement[XmlName_ListScoreSort], out listScoreSort ) ) result.ListScoreSort = listScoreSort;

            bool listExcludeGenres;
            if( XmlUtil.TryGetBoolFromNode( xElement[XmlName_ListExcludeGenres], out listExcludeGenres ) ) result.ListExcludeGenres = listExcludeGenres;

            List<string> tagList = XmlUtil.GetStringsFromNodeList( xElement.SelectNodes( XmlName_TagList + "/" + XmlName_Tag ) );
            result.IncludedTags = ( tagList == null ) ? new HashSet<string>() : new HashSet<string>( tagList );

            return result;
        }
    }
}
