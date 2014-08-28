using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Depressurizer {

    /// <summary>
    /// Abstract base class for autocategorization schemes.
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

        int maxCategories;
        // bool removeOtherGenres; may add later
        // bool removeAllOthers; may add later

        public AutoCatGenre( GameDB db, GameList games, int maxCategories = 0 ) : base( db, games ) {
            this.maxCategories = maxCategories;
        }

        public override bool CategorizeGame( int gameId ) {
            if( !db.Contains( gameId ) ) return false;

            GameDBEntry dbEntry = db.Games[gameId];
            string genreString = dbEntry.Genre;

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
