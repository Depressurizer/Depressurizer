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

        private string name;
        public string Name {
            get {
                return name;
            }
        }

        public override string ToString() {
            return Name;
        }

        public AutoCat( string name ) {
            this.name = name;
        }

        /// <summary>
        /// Must be called before any categorizations are done. Should be overridden to perform any necessary database analysis or other preparation.
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

        protected int maxCategories;
        protected bool removeOtherGenres;

        public const string TypeIdString = "AutoCatGenre";

        private const string XmlName_Name = "Name", XmlName_RemOther = "RemoveOthers", XmlName_MaxCats = "MaxCategories";

        private SortedSet<Category> genreCategories;

        /// <summary>
        /// Creates a new AutoCatGenre object, which autocategorizes games based on the genres in the Steam store.
        /// </summary>
        /// <param name="db">Reference to GameDB to use</param>
        /// <param name="games">Reference to the GameList to act on</param>
        /// <param name="maxCategories">Maximum number of categories to assign per game. 0 indicates no limit.</param>
        /// <param name="removeOthers">If true, removes any OTHER genre-named categories from each game processed. Will not remove categories that do not match a genre found in the database.</param>
        public AutoCatGenre( string name, int maxCategories, bool removeOthers )
            : base( name ) {
            this.maxCategories = maxCategories;
            this.removeOtherGenres = removeOthers;
        }

        /// <summary>
        /// Prepares to categorize games. Prepares a list of genre categories to remove. Does nothing if removeothergenres is false.
        /// </summary>
        public override void PreProcess( GameList games, GameDB db ) {
            base.PreProcess( games, db );
            if( removeOtherGenres ) {
                SortedSet<string> catStrings = new SortedSet<string>();
                char[] sep = new char[] { ',' };
                foreach( GameDBEntry dbEntry in db.Games.Values ) {
                    if( !String.IsNullOrEmpty( dbEntry.Genre ) ) {
                        string[] cats = dbEntry.Genre.Split( sep );
                        foreach( string cStr in cats ) {
                            catStrings.Add( cStr.Trim() );
                        }
                    }
                }

                genreCategories = new SortedSet<Category>();
                foreach( string cStr in catStrings ) {
                    if( games.CategoryExists( cStr ) ) {
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

            if( removeOtherGenres && genreCategories != null ) {
                game.RemoveCategory( genreCategories );
            }

            if( !String.IsNullOrEmpty( genreString ) ) {
                string[] genreStrings = genreString.Split( new char[] { ',' } );
                List<Category> categories = new List<Category>();
                for( int i = 0; ( i < maxCategories || maxCategories == 0 ) && i < genreStrings.Length; i++ ) {
                    categories.Add( games.GetCategory( genreStrings[i].Trim() ) );
                }

                game.AddCategory( categories );
            }
            return AutoCatResult.Success;
        }

        public override void WriteToXml( XmlWriter writer ) {
            writer.WriteStartElement( TypeIdString );

            writer.WriteElementString( XmlName_Name, this.Name );
            writer.WriteElementString( XmlName_MaxCats, this.maxCategories.ToString() );
            writer.WriteElementString( XmlName_RemOther, this.removeOtherGenres.ToString() );

            writer.WriteEndElement();
        }

        public static AutoCatGenre LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlName_Name], TypeIdString );
            int maxCats = XmlUtil.GetIntFromNode( xElement[XmlName_MaxCats], 0 );
            bool remOther = XmlUtil.GetBoolFromNode( xElement[XmlName_RemOther], false );
            AutoCatGenre result = new AutoCatGenre( name, maxCats, remOther );
            return result;
        }
    }
}
