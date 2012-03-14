/*
Copyright 2011, 2012 Steve Labbe.

This file is part of Depressurizer.

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
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace Depressurizer {
    /// <summary>
    /// Represents a single game
    /// </summary>
    public class Game {
        public string Name;
        public int Id;
        public Category Category;
        public bool Favorite;

        public Game( int id, string name ) {
            Id = id;
            Name = name;
            Category = null;
            Favorite = false;
        }
    }

    /// <summary>
    /// Represents a single game category
    /// </summary>
    public class Category : IComparable {
        public string Name;

        public Category( string name ) {
            Name = name;
        }

        public override string ToString() {
            return Name;
        }

        public int CompareTo( object o ) {
            return Name.CompareTo( ( o as Category ).Name );
        }
    }

    /// <summary>
    /// Represents a complete collection of games and categories.
    /// </summary>
    public class GameData {
        #region Fields
        public Dictionary<int, Game> Games;
        public List<Category> Categories;
        #endregion

        public GameData() {
            Games = new Dictionary<int, Game>();
            Categories = new List<Category>();
        }

        #region Modifiers
        public void Clear() {
            Games.Clear();
            Categories.Clear();
        }

        /// <summary>
        /// Sets the name of the given game ID, and adds the game to the list if it doesn't already exist.
        /// </summary>
        /// <param name="id">ID of the game to set</param>
        /// <param name="name">Name to assign to the game</param>
        private void SetGameName( int id, string name, bool overWrite ) {
            if( !Games.ContainsKey( id ) ) {
                Games.Add( id, new Game( id, name ) );
            } else if( overWrite ) {
                Games[id].Name = name;
            }
        }

        /// <summary>
        /// Adds a new category to the list.
        /// </summary>
        /// <param name="name">Name of the category to add</param>
        /// <returns>The added category. Returns null if the category already exists.</returns>
        public Category AddCategory( string name ) {
            if( CategoryExists( name ) ) {
                return null;
            } else {
                Category newCat = new Category( name );
                Categories.Add( newCat );
                return newCat;
            }
        }

        /// <summary>
        /// Removes the given category.
        /// </summary>
        /// <param name="c">Category to remove.</param>
        /// <returns>True if removal was successful, false if it was not in the list anyway</returns>
        public bool RemoveCategory( Category c ) {
            if( Categories.Remove( c ) ) {
                foreach( Game g in Games.Values ) {
                    if( g.Category == c ) g.Category = null;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Renames the given category.
        /// </summary>
        /// <param name="c">Category to rename.</param>
        /// <param name="newName">Name to assign to the new category.</param>
        /// <returns>True if rename was successful, false otherwise (if name was in use already)</returns>
        public bool RenameCategory( Category c, string newName ) {
            if( !CategoryExists( newName ) ) {
                c.Name = newName;
                Categories.Sort();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the categories for the given list of game IDs to the same thing
        /// </summary>
        /// <param name="gameIDs">Array of game IDs.</param>
        /// <param name="newCat">Category to assign</param>
        public void SetGameCategories( int[] gameIDs, Category newCat ) {
            for( int i = 0; i < gameIDs.Length; i++ ) {
                Games[gameIDs[i]].Category = newCat;
            }
        }

        /// <summary>
        /// Sets the fav state for the given list of game IDs to the same thing
        /// </summary>
        /// <param name="gameIDs">Array of game IDs.</param>
        /// <param name="newCat">Fav state to assign</param>
        public void SetGameFavorites( int[] gameIDs, bool fav ) {
            for( int i = 0; i < gameIDs.Length; i++ ) {
                Games[gameIDs[i]].Favorite = fav;
            }
        }
        #endregion

        #region Accessors
        /// <summary>
        /// Checks to see if a category with the given name exists
        /// </summary>
        /// <param name="name">Name of the category to look for</param>
        /// <returns>True if the name is found, false otherwise</returns>
        public bool CategoryExists( string name ) {
            foreach( Category c in Categories ) {
                if( c.Name == name ) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the category with the given name. If the category does not exist, creates it.
        /// </summary>
        /// <param name="name">Name to get the category for</param>
        /// <returns>A category with the given name.</returns>
        public Category GetCategory( string name ) {
            if( string.IsNullOrEmpty( name ) ) return null;
            foreach( Category c in Categories ) {
                if( c.Name == name ) return c;
            }
            Category newCat = new Category( name );
            Categories.Add( newCat );
            return newCat;
        }
        #endregion

        /// <summary>
        /// Loads game info from the given Steam profile
        /// </summary>
        /// <param name="profileName">Name of the Steam profile to get</param>
        /// <returns>The number of games found in the profile</returns>
        public int LoadGameList( string profileName, bool overWrite, SortedSet<int> ignore, bool ignoreDlc ) {
            XmlDocument doc = new XmlDocument();
            try {
                string url = string.Format( Properties.Resources.ProfileURL, profileName );
                WebRequest req = HttpWebRequest.Create( url );
                WebResponse response = req.GetResponse();
                doc.Load( response.GetResponseStream() );
                response.Close();

            } catch( Exception e ) {
                throw new ApplicationException( "Failed to download profile data: " + e.Message, e );
            }

            int loadedGames = 0;
            XmlNodeList gameNodes = doc.SelectNodes( "/gamesList/games/game" );
            foreach( XmlNode gameNode in gameNodes ) {
                int appId;
                XmlNode appIdNode = gameNode["appID"];
                if( appIdNode != null && int.TryParse( appIdNode.InnerText, out appId ) ) {
                    if( ( ignore != null && ignore.Contains( appId ) ) || ( ignoreDlc && Program.GameDB.IsDlc( appId ) ) ) {
                        continue;
                    }
                    XmlNode nameNode = gameNode["name"];
                    if( nameNode != null ) {
                        SetGameName( appId, nameNode.InnerText, overWrite );
                        loadedGames++;
                    }
                }
            }
            return loadedGames;
        }

        /// <summary>
        /// Loads category info from the given steam config file.
        /// </summary>
        /// <param name="filePath">The path of the file to open</param>
        /// <returns>The number of game entries found</returns>
        public int ImportSteamFile( string filePath, SortedSet<int> ignore, bool ignoreDlc ) {

            TextVdfFileNode dataRoot;

            try {
                using( StreamReader reader = new StreamReader( filePath, false ) ) {
                    dataRoot = TextVdfFileNode.Load( reader, true );
                }
            } catch( ParseException e ) {
                throw new ApplicationException( "Error parsing Steam config file: " + e.Message, e );
            } catch( IOException e ) {
                throw new ApplicationException( "Error opening Steam config file: " + e.Message, e );
            }

            TextVdfFileNode appsNode = dataRoot.GetNodeAt( new string[] { "Software", "Valve", "Steam", "apps" }, true );
            return LoadGames( appsNode, ignore, ignoreDlc );
        }

        /// <summary>
        /// Loads in games from an node containing a list of games.
        /// </summary>
        /// <param name="appsNode">Node containing the game nodes</param>
        /// <param name="ignore">Set of games to ignore</param>
        /// <returns>Number of games loaded</returns>
        private int LoadGames( TextVdfFileNode appsNode, SortedSet<int> ignore, bool ignoreDlc ) {
            int loadedGames = 0;

            Dictionary<string, TextVdfFileNode> gameNodeArray = appsNode.NodeArray;
            if( gameNodeArray != null ) {
                foreach( KeyValuePair<string, TextVdfFileNode> gameNodePair in gameNodeArray ) {
                    int gameId;
                    if( int.TryParse( gameNodePair.Key, out gameId ) ) {
                        if( ( ignore != null && ignore.Contains( gameId ) ) || ( ignoreDlc && Program.GameDB.IsDlc( gameId ) ) ) {
                            continue;
                        }
                        if( gameNodePair.Value != null && gameNodePair.Value.ContainsKey( "tags" ) ) {
                            Category cat = null;
                            bool fav = false;
                            loadedGames++;
                            TextVdfFileNode tagsNode = gameNodePair.Value["tags"];
                            Dictionary<string, TextVdfFileNode> tagArray = tagsNode.NodeArray;
                            if( tagArray != null ) {
                                foreach( TextVdfFileNode tag in tagArray.Values ) {
                                    string tagName = tag.NodeString;
                                    if( tagName != null ) {
                                        if( tagName == "favorite" ) {
                                            fav = true;
                                        } else {
                                            cat = GetCategory( tagName );
                                        }
                                    }
                                }
                            }

                            if( !Games.ContainsKey( gameId ) ) {
                                Game newGame = new Game( gameId, string.Empty );
                                Games.Add( gameId, newGame );
                                newGame.Name = Program.GameDB.GetName( gameId );
                            }
                            Games[gameId].Category = cat;
                            Games[gameId].Favorite = fav;

                        }
                    }
                }
            }

            return loadedGames;
        }

        /// <summary>
        /// Writes category information out to a steam config file. Also saves any other settings that had been loaded, to avoid setting loss.
        /// </summary>
        /// <param name="path">Full path of the steam config file to save</param>
        public void SaveSteamFile( string filePath, bool discardMissing ) {
            TextVdfFileNode fileData = new TextVdfFileNode();
            try {
                using( StreamReader reader = new StreamReader( filePath, false ) ) {
                    fileData = TextVdfFileNode.Load( reader, true );
                }
            } catch { }

            TextVdfFileNode appListNode = fileData.GetNodeAt( new string[] { "Software", "Valve", "Steam", "apps" }, true );

            if( discardMissing ) {
                Dictionary<string, TextVdfFileNode> gameNodeArray = appListNode.NodeArray;
                if( gameNodeArray != null ) {
                    foreach( KeyValuePair<string, TextVdfFileNode> pair in gameNodeArray ) {
                        int gameId;
                        if( !( int.TryParse( pair.Key, out gameId ) && Games.ContainsKey( gameId ) ) ) {
                            pair.Value.RemoveSubnode( "tags" );
                        }
                    }
                }
            }

            foreach( Game game in Games.Values ) {
                TextVdfFileNode gameNode = appListNode[game.Id.ToString()];
                gameNode.RemoveSubnode( "tags" );
                if( game.Category != null || game.Favorite ) {
                    TextVdfFileNode tagsNode = gameNode["tags"];
                    int key = 0;
                    if( game.Category != null ) {
                        tagsNode[key.ToString()] = new TextVdfFileNode( game.Category.Name );
                        key++;
                    }
                    if( game.Favorite ) {
                        tagsNode[key.ToString()] = new TextVdfFileNode( "favorite" );
                    }
                }
            }

            appListNode.CleanTree();

            TextVdfFileNode fullFile = new TextVdfFileNode();
            fullFile["UserLocalConfigStore"] = fileData;
            try {
                FileStream fStream = File.Open( filePath, FileMode.Create, FileAccess.Write, FileShare.None );
                using( StreamWriter writer = new StreamWriter( fStream ) ) {
                    fullFile.Save( writer );
                }
                fStream.Close();
            } catch( ArgumentException e ) {
                throw new ApplicationException( "Failed to save Steam config file: Invalid path specified.", e );
            } catch( IOException e ) {
                throw new ApplicationException( "Failed to save Steam config file: " + e.Message, e );
            } catch( UnauthorizedAccessException e ) {
                throw new ApplicationException( "Access denied on Steam config file: " + e.Message, e );
            }
        }
    }
}