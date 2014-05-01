using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

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

        // Complete steam setting file data that was loaded
        private FileNode backingData;

        public bool AutoLoaded { get; private set; }
        private string steamId;
        private string steamPath;

        bool roamingNode = false;
        #endregion

        public void SetAutoload( string steamPath, string steamId ) {
            AutoLoaded = true;
            this.steamId = steamId;
            this.steamPath = steamPath;
        }

        public GameData() {
            Games = new Dictionary<int, Game>();
            Categories = new List<Category>();
            AutoLoaded = false;
        }

        #region Modifiers
        public void Clear() {
            AutoLoaded = false;
            roamingNode = false;
            Games.Clear();
            Categories.Clear();
        }

        /// <summary>
        /// Sets the name of the given game ID, and adds the game to the list if it doesn't already exist.
        /// </summary>
        /// <param name="id">ID of the game to set</param>
        /// <param name="name">Name to assign to the game</param>
        private void SetGameName( int id, string name ) {
            if( !Games.ContainsKey( id ) ) {
                Games.Add( id, new Game( id, name ) );
            } else {
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
        public int LoadProfile( string profileName ) {
            string url = string.Format( @"http://steamcommunity.com/id/{0}/games?tab=all&sort=name", profileName );
            WebRequest req = HttpWebRequest.Create( url );
            WebResponse response = req.GetResponse();
            StreamReader reader = new StreamReader( response.GetResponseStream() );

            string line;

            // Get to relevant javascript
            do {
                line = reader.ReadLine();
                if( line == null ) return 0;
            } while( line != null && !line.Contains( "rgGames" ) );

            int loadedGames = 0;
            line = reader.ReadLine();
            Regex regex = new Regex( @"rgGames\['(\d+)'\]\s*=\s*'(.*)';" );
            while( !line.StartsWith( "</script>" ) ) {
                Match m = regex.Match( line );
                if( m.Success ) {
                    int id;
                    if( int.TryParse( m.Groups[1].Value, out id ) ) {
                        SetGameName( id, m.Groups[2].Value.Replace( "\\'", "'" ) );
                        loadedGames++;
                    }
                }
                line = reader.ReadLine();
            }
            return loadedGames;


        }

        /// <summary>
        /// Loads category info from the given steam config file.
        /// </summary>
        /// <param name="filePath">The path of the file to open</param>
        /// <returns>The number of game entries found</returns>
        public int LoadSteamFile( string filePath ) {

            FileNode dataRoot;

            using( StreamReader reader = new StreamReader( filePath, false ) ) {
                dataRoot = FileNode.Load( reader, true );
            }

            Games.Clear();
            Categories.Clear();
            this.backingData = dataRoot;

            FileNode appsNode = dataRoot.GetNodeAt( new string[] { "Software", "Valve", "Steam", "apps" }, true );
            return LoadGames( appsNode );
        }

        private int LoadGames( FileNode appsNode ) {
            int loadedGames = 0;

            Dictionary<string, FileNode> gameNodeArray = appsNode.NodeArray;
            if( gameNodeArray != null ) {
                foreach( KeyValuePair<string, FileNode> gameNodePair in gameNodeArray ) {
                    int gameId;
                    if( int.TryParse( gameNodePair.Key, out gameId ) ) {
                        Category cat = null;
                        bool fav = false;
                        if( gameNodePair.Value != null && gameNodePair.Value.ContainsKey( "tags" ) ) {
                            FileNode tagsNode = gameNodePair.Value["tags"];
                            Dictionary<string, FileNode> tagArray = tagsNode.NodeArray;
                            if( tagArray != null ) {
                                foreach( FileNode tag in tagArray.Values ) {
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
                        }
                        if( !Games.ContainsKey( gameId ) ) {
                            Game newGame = new Game( gameId, string.Empty );
                            Games.Add( gameId, newGame );
                            loadedGames++;
                        }
                        Games[gameId].Category = cat;
                        Games[gameId].Favorite = fav;
                    }
                }
            }

            return loadedGames;
        }

        /// <summary>
        /// Writes category information out to a steam config file. Also saves any other settings that had been loaded, to avoid setting loss.
        /// </summary>
        /// <param name="path">Full path of the steam config file to save</param>
        public void SaveSteamFile( string path ) {
            FileNode appListNode = backingData.GetNodeAt( new string[] { "Software", "Valve", "Steam", "apps" }, true );

            foreach( Game game in Games.Values ) {
                FileNode gameNode = appListNode[game.Id.ToString()];
                gameNode.RemoveSubnode( "tags" );
                if( game.Category != null || game.Favorite ) {
                    FileNode tagsNode = gameNode["tags"];
                    int key = 0;
                    if( game.Category != null ) {
                        tagsNode[key.ToString()] = new FileNode( game.Category.Name );
                        key++;
                    }
                    if( game.Favorite ) {
                        tagsNode[key.ToString()] = new FileNode( "favorite" );
                    }
                }
            }

            appListNode.CleanTree();

            FileNode fullFile = new FileNode();
            fullFile["UserLocalConfigStore"] = backingData;

            using( StreamWriter writer = new StreamWriter( path, false ) ) {
                fullFile.Save( writer );
            }
        }

        public void AutoSave() {
            if( !AutoLoaded ) return;
            SaveSteamFile( string.Format( @"{0}\userdata\{1}\7\remote\sharedconfig.vdf", steamPath, steamId ) );
        }
    }
}
