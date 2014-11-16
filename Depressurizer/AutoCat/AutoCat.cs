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

}
