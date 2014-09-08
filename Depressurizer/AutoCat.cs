using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Rallion;

namespace Depressurizer {
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
    public abstract class AutoCat {

        protected GameList games;
        protected GameDB db;

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
                default:
                    break;
            }
            return result;
        }
    }

    /// <summary>
    /// Autocategorization scheme that adds genre categories.
    /// </summary>
    public class AutoCatGenre : AutoCat {

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

        private SortedSet<Category> genreCategories;

        /// <summary>
        /// Creates a new AutoCatGenre object, which autocategorizes games based on the genres in the Steam store.
        /// </summary>
        /// <param name="db">Reference to GameDB to use</param>
        /// <param name="games">Reference to the GameList to act on</param>
        /// <param name="maxCategories">Maximum number of categories to assign per game. 0 indicates no limit.</param>
        /// <param name="removeOthers">If true, removes any OTHER genre-named categories from each game processed. Will not remove categories that do not match a genre found in the database.</param>
        public AutoCatGenre( string name, string prefix, int maxCategories, bool removeOthers, List<string> ignore )
            : base( name ) {
            MaxCategories = maxCategories;
            RemoveOtherGenres = removeOthers;
            Prefix = prefix;
            IgnoredGenres = (ignore == null) ? new List<string>() : ignore;
        }

        protected AutoCatGenre( AutoCatGenre other ):base(other) {
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
                    if( games.CategoryExists( cStr ) && !IgnoredGenres.Contains( cStr ) ) {
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

            GameDBEntry dbEntry = db.Games[game.Id];
            string genreString = dbEntry.Genre;

            if( RemoveOtherGenres && genreCategories != null ) {
                game.RemoveCategory( genreCategories );
            }

            if( !String.IsNullOrEmpty( genreString ) ) {
                string[] genreStrings = genreString.Split( new char[] { ',' } );
                List<Category> categories = new List<Category>();
                for( int i = 0; ( i < MaxCategories || MaxCategories == 0 ) && i < genreStrings.Length; i++ ) {
                    string cStr = genreStrings[i].Trim();
                    if( !IgnoredGenres.Contains( cStr ) ) {
                        categories.Add( games.GetCategory( GetProcessedString( cStr ) ) );
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

        public AutoCatFlags( string name, string prefix, List<string> flags ):base(name) {
            Prefix = prefix;
            IncludedFlags = flags;
        }

        protected AutoCatFlags( AutoCatFlags other ) : base (other) {
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

            GameDBEntry dbEntry = db.Games[game.Id];

            IEnumerable<string> categories = dbEntry.Flags.Intersect( IncludedFlags );

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
