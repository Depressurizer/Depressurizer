using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Depressurizer {

    /// <summary>
    /// Abstract base class for autocategorization schemes. Call PreProcess before any set of autocat operations.
    /// This is a preliminary form, and may change in future versions.
    /// Returning only true / false on a categorization attempt may prove too simplistic.
    /// </summary>
    abstract class AutoCat {

        protected GameDB db;
        protected GameList games;

        public AutoCat( GameDB db, GameList games ) {
            this.db = db;
            this.games = games;
        }

        /// <summary>
        /// Should be called before any categorizations are done. Should be overridden to perform any necessary database analysis.
        /// </summary>
        public virtual void PreProcess() { }

        /// <summary>
        /// Applies this autocategorization scheme to the game with the given ID.
        /// </summary>
        /// <param name="gameId">The game ID to process</param>
        /// <returns>False if the game was not found in database. This allows the calling function to potentially re-scrape data and reattempt.</returns>
        public abstract bool CategorizeGame( int gameId );

    }

    /// <summary>
    /// Autocategorization scheme that adds genre categories.
    /// </summary>
    class AutoCatGenre : AutoCat {

        protected int maxCategories;
        protected bool removeOtherGenres;

        private SortedSet<Category> genreCategories;

        /// <summary>
        /// Creates a new AutoCatGenre object, which autocategorizes games based on the genres in the Steam store.
        /// </summary>
        /// <param name="db">Reference to GameDB to use</param>
        /// <param name="games">Reference to the GameList to act on</param>
        /// <param name="maxCategories">Maximum number of categories to assign per game. 0 indicates no limit.</param>
        /// <param name="removeOthers">If true, removes any OTHER genre-named categories from each game processed. Will not remove categories that do not match a genre found in the database.</param>
        public AutoCatGenre( GameDB db, GameList games, int maxCategories, bool removeOthers ) : base( db, games ) {
            this.maxCategories = maxCategories;
            this.removeOtherGenres = removeOthers;
        }

        /// <summary>
        /// Prepares to categorize games. Prepares a list of genre categories to remove. Does nothing if removeothergenres is false.
        /// </summary>
        public override void PreProcess() {
            if( removeOtherGenres ) {
                SortedSet<string> catStrings = new SortedSet<string>();
                char[] sep = new char[] { ',' };
                foreach( GameDBEntry dbEntry in db.Games.Values ) {
                    string[] cats = dbEntry.Genre.Split( sep );
                    foreach( string cStr in cats ) {
                        catStrings.Add( cStr );
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

        public override bool CategorizeGame( int gameId ) {
            if( !db.Contains( gameId ) ) return false;

            GameDBEntry dbEntry = db.Games[gameId];
            string genreString = dbEntry.Genre;

            if( removeOtherGenres && genreCategories != null ) {
                games.RemoveGameCategory( gameId, genreCategories );
            }

            string[] genreStrings = genreString.Split( new char[] { ',' } );
            List<Category> categories = new List<Category>();
            for( int i = 0; ( i < maxCategories || maxCategories == 0 ) && i < genreStrings.Length; i++ ) {
                categories.Add( games.GetCategory( genreStrings[i] ) );
            }

            games.AddGameCategory( gameId, categories );

            return true;
        }
    }
}
