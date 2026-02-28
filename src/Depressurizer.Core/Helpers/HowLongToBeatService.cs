using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fastenshtein;
using Newtonsoft.Json;
using RandomUserAgent;
using AngleSharp;
using Depressurizer.Core.Models;
using Microsoft.Data.Edm.Validation;
using System.Collections;

namespace Depressurizer.Core.Helpers
{
    /// <summary>
    /// Provides services to interact with the HowLongToBeat website, allowing users to search for games and retrieve detailed information about them.
    /// </summary>
    public class HowLongToBeatService
    {
        private HltbSearch hltb = new HltbSearch();

        /// <summary>
        /// Initializes a new instance of the HowLongToBeatService class.
        /// </summary>
        public HowLongToBeatService() { }

        /// <summary>
        /// Retrieves detailed information about a specific game by its unique identifier.
        /// </summary>
        /// <param name="gameId">The unique identifier for the game.</param>
        /// <param name="signal">Optional cancellation token to cancel the request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the detailed information about the game.</returns>
        public async Task<HowLongToBeatEntry> Detail(string gameId, CancellationToken signal = default)
        {
            var detailPage = await hltb.DetailHtml(gameId, signal);
            var entry = HowLongToBeatParser.ParseDetails(detailPage, gameId);
            return entry;
        }

        /// <summary>
        /// Searches for games that match the specified query.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <param name="signal">Optional cancellation token to cancel the request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of games that match the query.</returns>
        public async Task<List<HowLongToBeatEntry>> Search(string query, CancellationToken signal = default)
        {
            var searchTerms = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var search = await hltb.Search(searchTerms, signal);
            var hltbEntries = new List<HowLongToBeatEntry>();
            foreach (var resultEntry in search.data)
            {
                hltbEntries.Add(new HowLongToBeatEntry(
                    resultEntry.game_id.ToString(),
                    resultEntry.game_name,
                    "", // no description
                    string.IsNullOrEmpty(resultEntry.profile_platform) ? new string[0] : resultEntry.profile_platform.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries),
                    $"{HltbSearch.IMAGE_URL}{resultEntry.game_image}",
                    new List<string[]> { new string[] { "Main", "Main" }, new string[] { "Main + Extra", "Main + Extra" }, new string[] { "Completionist", "Completionist" } },
                    (int)Math.Round(resultEntry.comp_main / 3600.0),
                    (int)Math.Round(resultEntry.comp_plus / 3600.0),
                    (int)Math.Round(resultEntry.comp_100 / 3600.0),
                    CalcDistancePercentage(resultEntry.game_name, query),
                    query
                ));
            }
            return hltbEntries;
        }

        /// <summary>
        /// Calculates the similarity percentage between two strings based on the Levenshtein distance.
        /// </summary>
        /// <param name="text">The first string to compare.</param>
        /// <param name="term">The second string to compare.</param>
        /// <returns>The similarity percentage between the two strings.</returns>
        public static double CalcDistancePercentage(string text, string term)
        {
            var longer = text.ToLower().Trim();
            var shorter = term.ToLower().Trim();
            if (longer.Length < shorter.Length)
            {
                var temp = longer;
                longer = shorter;
                shorter = temp;
            }
            var longerLength = longer.Length;
            if (longerLength == 0)
                return 1.0;
            var distance = Levenshtein.Distance(longer, shorter);
            return Math.Round(((longerLength - distance) / (double)longerLength) * 100) / 100;
        }
    }

    /// <summary>
    /// Represents an entry from the HowLongToBeat website, containing information about a game.
    /// </summary>
    public class HowLongToBeatEntry
    {
        #region Public Properties
        /// <summary>
        /// Gets an array of platform names on which the game can be played. This property provides backward compatibility for platforms information.
        /// </summary>
        public readonly string[] PlayableOn;
        /// <summary>
        /// Gets the unique identifier for the game.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Gets the name of the game.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Gets a description of the game. This may include a brief overview or storyline of the game.
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// Gets an array of platforms on which the game is available. Each string in the array represents a different gaming platform.
        /// </summary>
        public readonly string[] Platforms;

        /// <summary>
        /// Gets the URL of the game's image. This URL points to the cover art or a primary promotional image of the game.
        /// </summary>
        public readonly string ImageUrl;

        /// <summary>
        /// Gets a list of string arrays where each array represents a time label and its corresponding gameplay time category. Each array includes two strings: the category identifier and the label text.
        /// </summary>
        public readonly List<string[]> TimeLabels;

        /// <summary>
        /// Gets the main gameplay duration in hours. This represents the average time it takes to complete the main objectives of the game.
        /// </summary>
        public readonly int GameplayMain;

        /// <summary>
        /// Gets the gameplay duration in hours for completing main objectives along with extra content or side missions. This represents a more thorough playthrough than the main gameplay.
        /// </summary>
        public readonly int GameplayMainExtra;

        /// <summary>
        /// Gets the completionist gameplay duration in hours. This time represents how long it takes to complete the game to its fullest extent, including all side missions, collectibles, and achievements.
        /// </summary>
        public readonly int GameplayCompletionist;

        /// <summary>
        /// Gets the similarity score between the search term used to find the game and the game's name. This score is a measure of how closely the game matches the search criteria.
        /// </summary>
        public readonly double Similarity;

        /// <summary>
        /// Gets the search term that was used to find this game. This reflects the user's original query.
        /// </summary>
        public readonly string SearchTerm;

        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes a new instance of the HowLongToBeatEntry class with specified details about a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="platforms">The platforms the game is available on.</param>
        /// <param name="imageUrl">The URL of the game's image.</param>
        /// <param name="timeLabels">Labels for different gameplay time categories.</param>
        /// <param name="gameplayMain">The main gameplay duration in hours.</param>
        /// <param name="gameplayMainExtra">The gameplay duration with extras in hours.</param>
        /// <param name="gameplayCompletionist">The completionist gameplay duration in hours.</param>
        /// <param name="similarity">The similarity score of the search term to the game name.</param>
        /// <param name="searchTerm">The search term used to find this game.</param>
        public HowLongToBeatEntry(string id, string name, string description, string[] platforms, string imageUrl, List<string[]> timeLabels, int gameplayMain, int gameplayMainExtra, int gameplayCompletionist, double similarity, string searchTerm)
        {
            Id = id;
            Name = name;
            Description = description;
            Platforms = platforms;
            ImageUrl = imageUrl;
            TimeLabels = timeLabels;
            GameplayMain = gameplayMain;
            GameplayMainExtra = gameplayMainExtra;
            GameplayCompletionist = gameplayCompletionist;
            Similarity = similarity;
            SearchTerm = searchTerm;
            PlayableOn = platforms; // Backward compatibility
        }

        #endregion
    }

    /// <summary>
    /// Parses HTML content from the HowLongToBeat website to extract detailed information about games.
    /// </summary>
    public class HowLongToBeatParser
    {
        /// <summary>
        /// Parses the HTML content of a game's detail page to extract game information.
        /// </summary>
        /// <param name="html">The HTML content of the detail page.</param>
        /// <param name="id">The unique identifier of the game.</param>
        /// <returns>An instance of HowLongToBeatEntry containing the parsed game details.</returns>
        public static HowLongToBeatEntry ParseDetails(string html, string id)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(req => req.Content(html)).Result;
            var gameName = document.QuerySelector("[class*='GameHeader_profile_header_game__']")?.FirstChild?.TextContent.Trim();
            var imageUrl = document.QuerySelector("[class*='GameHeader_game_image__'] img")?.GetAttribute("src");
            var gameDescription = document.QuerySelector("[class*='GameSummary_large__']").TextContent;
            var platforms = document.QuerySelectorAll("[class*='GameSummary_profile_info__']")
                .Select(element => element.TextContent)
                .FirstOrDefault(metaData => metaData.Contains("Platforms:"))?
                .Replace("\n", "")
                .Replace("Platforms:", "")
                .Split(',')
                .Select(data => data.Trim())
                .ToArray() ?? new string[0];

            var liElements = document.QuerySelectorAll("[class*='GameStats_game_times__'] li");
            var timeLabels = new List<string[]>();
            var gameplayMain = 0;
            var gameplayMainExtra = 0;
            var gameplayCompletionist = 0;

            foreach (var liElement in liElements)
            {
                var type = liElement.QuerySelector("h4").TextContent;
                var time = ParseTime(liElement.QuerySelector("h5").TextContent);
                if (type.StartsWith("Main Story") || type.StartsWith("Single-Player") || type.StartsWith("Solo"))
                {
                    gameplayMain = time;
                    timeLabels.Add(new string[] { "gameplayMain", type });
                }
                else if (type.StartsWith("Main + Sides") || type.StartsWith("Co-Op"))
                {
                    gameplayMainExtra = time;
                    timeLabels.Add(new string[] { "gameplayMainExtra", type });
                }
                else if (type.StartsWith("Completionist") || type.StartsWith("Vs."))
                {
                    gameplayCompletionist = time;
                    timeLabels.Add(new string[] { "gameplayComplete", type });
                }
            }

            return new HowLongToBeatEntry(
                id,
                gameName,
                gameDescription,
                platforms,
                imageUrl,
                timeLabels,
                gameplayMain,
                gameplayMainExtra,
                gameplayCompletionist,
                1,
                gameName
            );
        }

        private static int ParseTime(string text)
        {
            if (text.StartsWith("--"))
                return 0;
            if (text.Contains(" - "))
                return HandleRange(text);
            return GetTime(text);
        }

        private static int HandleRange(string text)
        {
            var range = text.Split(new string[] { " - " }, StringSplitOptions.None);
            var d = (GetTime(range[0]) + GetTime(range[1])) / 2;
            return (int)d;
        }

        private static int GetTime(string text)
        {
            var timeUnit = text.Substring(text.IndexOf(' ') + 1).Trim();
            if (timeUnit == "Mins")
                return 1;
            var time = text.Substring(0, text.IndexOf(' '));
            if (time.Contains("½"))
                return (int)(0.5 + int.Parse(time.Substring(0, text.IndexOf('½'))));
            return int.Parse(time);
        }
    }

    /// <summary>
    /// Handles HTTP requests to the HowLongToBeat website for searching games and retrieving game details.
    /// </summary>
    public class HltbSearch
    {

        #region Public Properties
        /// <summary>
        /// Gets the base URL for the HowLongToBeat website. This URL is the starting point for accessing various resources on the site.
        /// </summary>
        public static string BASE_URL = "https://howlongtobeat.com/";

        /// <summary>
        /// Gets the URL template for accessing the detail page of a specific game on the HowLongToBeat website. The game's unique identifier should be appended to this URL.
        /// </summary>
        public static string DETAIL_URL = $"{BASE_URL}game?id=";

        /// <summary>
        /// Gets the URL for the search API on the HowLongToBeat website. This URL is used to query the site's database for games that match specific search criteria.
        /// </summary>
        public static string SEARCH_URL = $"{BASE_URL}api/search";

        /// <summary>
        /// Gets the base URL for accessing images of games on the HowLongToBeat website. This URL is used as a prefix for the relative paths to game images.
        /// </summary>
        public static string IMAGE_URL = $"{BASE_URL}games/";

        #endregion

        private HttpClient httpClient;

        #region Public Methods
        /// <summary>
        /// Initializes a new instance of the HltbSearch class.
        /// </summary>
        public HltbSearch()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Fetches the HTML content of a game's detail page by its unique identifier.
        /// </summary>
        /// <param name="gameId">The unique identifier of the game.</param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the HTML content of the game's detail page.</returns>
        public async Task<string> DetailHtml(string gameId, CancellationToken? cancellationToken = null)
        {
            try
            {
                string userAgent = RandomUa.RandomUserAgent;

                // Create HttpRequestMessage with headers
                var request = new HttpRequestMessage(HttpMethod.Get, $"{DETAIL_URL}{gameId}");
                request.Headers.Add("User-Agent", userAgent);
                request.Headers.Add("origin", "https://howlongtobeat.com");
                request.Headers.Add("referer", "https://howlongtobeat.com");

                // Send request
                var response = await httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None);

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to fetch game detail HTML.", ex);
            }
        }

        /// <summary>
        /// Performs a search query on the HowLongToBeat website and returns the search results.
        /// </summary>
        public async Task<SearchResponse> Search(List<string> query, CancellationToken? cancellationToken = null)
        {
            var payload = new HLTBSearchPayload
            {
                SearchType = "games",
                SearchTerms = query,
                SearchPage = 1,
                Size = 20,
                SearchOptions = new HLTBSearchPayload.SearchOptionsProperty
                {
                    Games = new HLTBSearchPayload.GamesOptions
                    {
                        UserId = 0,
                        Platform = "",
                        SortCategory = "popular",
                        RangeCategory = "main",
                        RangeTime = new HLTBSearchPayload.RangeTime { Min = 0, Max = 0 },
                        Gameplay = new HLTBSearchPayload.GameplayOptions { Perspective = "", Flow = "", Genre = "" },
                        Modifier = ""
                    },
                    Users = new HLTBSearchPayload.UsersOptions { SortCategory = "postcount" },
                    Filter = "",
                    Sort = 0,
                    Randomizer = 0
                }
            };

            try
            {
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                string userAgent = RandomUa.RandomUserAgent;
                var request = new HttpRequestMessage(HttpMethod.Post, SEARCH_URL);
                request.Content = content;
                request.Headers.Add("User-Agent", userAgent);
                request.Headers.Add("origin", "https://howlongtobeat.com");
                request.Headers.Add("referer", "https://howlongtobeat.com");
                var response = await httpClient.SendAsync(request, cancellationToken ?? CancellationToken.None);

                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<SearchResponse>(jsonString);
                return obj;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to perform search.", ex);
            }
        }

        #endregion
    }

    /// <summary>
    /// Represents the response received from a search query to the HowLongToBeat website. Contains an array of <see cref="HLTBSearchEntry"/> objects, each representing a game that matches the search criteria.
    /// </summary>
    public class SearchResponse
    {
        /// <summary>
        /// Gets or sets an array of <see cref="HLTBSearchEntry"/> objects, each representing a game that matches the search criteria. This array contains the data returned by the HowLongToBeat search API.
        /// </summary>
        public HLTBSearchResponseEntry[] data { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="HLTBSearchEntry"/> object, representing a game that matches the search criteria
        /// </summary>
        public sealed class HLTBSearchResponseEntry
        {
            /// <summary>
            /// Gets or sets the unique identifier for the game.
            /// </summary>
            public int game_id { get; set; }

            /// <summary>
            /// Gets or sets the name of the game.
            /// </summary>
            public string game_name { get; set; }

            /// <summary>
            /// Gets or sets the date associated with the game name. This is likely to be used for versioning or release years.
            /// </summary>
            public int game_name_date { get; set; }

            /// <summary>
            /// Gets or sets the alias or alternative name for the game.
            /// </summary>
            public string game_alias { get; set; }

            /// <summary>
            /// Gets or sets the type of the game (e.g., game, DLC).
            /// </summary>
            public string game_type { get; set; }

            /// <summary>
            /// Gets or sets the file name or path of the game's cover image.
            /// </summary>
            public string game_image { get; set; }

            /// <summary>
            /// Gets or sets the combined completion level for the game.
            /// </summary>
            public int comp_lvl_combine { get; set; }

            /// <summary>
            /// Gets or sets the completion level for single-player mode.
            /// </summary>
            public int comp_lvl_sp { get; set; }

            /// <summary>
            /// Gets or sets the completion level for co-op mode.
            /// </summary>
            public int comp_lvl_co { get; set; }

            /// <summary>
            /// Gets or sets the completion level for multiplayer mode.
            /// </summary>
            public int comp_lvl_mp { get; set; }

            /// <summary>
            /// Gets or sets the completion level for speedrun mode.
            /// </summary>
            public int comp_lvl_spd { get; set; }

            /// <summary>
            /// Gets or sets the average completion time for the main content.
            /// </summary>
            public int comp_main { get; set; }

            /// <summary>
            /// Gets or sets the average completion time including main content plus extras.
            /// </summary>
            public int comp_plus { get; set; }

            /// <summary>
            /// Gets or sets the average completion time for 100% completion.
            /// </summary>
            public int comp_100 { get; set; }

            /// <summary>
            /// Gets or sets the average completion time for all available content.
            /// </summary>
            public int comp_all { get; set; }

            /// <summary>
            /// Gets or sets the count of users who completed the main content.
            /// </summary>
            public int comp_main_count { get; set; }

            /// <summary>
            /// Gets or sets the count of users who completed the main content plus extras.
            /// </summary>
            public int comp_plus_count { get; set; }

            /// <summary>
            /// Gets or sets the count of users who achieved 100% completion.
            /// </summary>
            public int comp_100_count { get; set; }

            /// <summary>
            /// Gets or sets the count of users who completed all available content.
            /// </summary>
            public int comp_all_count { get; set; }

            /// <summary>
            /// Gets or sets the time invested in co-op mode.
            /// </summary>
            public int invested_co { get; set; }

            /// <summary>
            /// Gets or sets the time invested in multiplayer mode.
            /// </summary>
            public int invested_mp { get; set; }

            /// <summary>
            /// Gets or sets the count of users who invested time in co-op mode.
            /// </summary>
            public int invested_co_count { get; set; }

            /// <summary>
            /// Gets or sets the count of users who invested time in multiplayer mode.
            /// </summary>
            public int invested_mp_count { get; set; }

            /// <summary>
            /// Gets or sets the total count of completions.
            /// </summary>
            public int count_comp { get; set; }

            /// <summary>
            /// Gets or sets the count of speedrun attempts.
            /// </summary>
            public int count_speedrun { get; set; }

            /// <summary>
            /// Gets or sets the count of users who have added the game to their backlog.
            /// </summary>
            public int count_backlog { get; set; }

            /// <summary>
            /// Gets or sets the count of reviews for the game.
            /// </summary>
            public int count_review { get; set; }

            /// <summary>
            /// Gets or sets the average review score for the game.
            /// </summary>
            public int review_score { get; set; }

            /// <summary>
            /// Gets or sets the count of users currently playing the game.
            /// </summary>
            public int count_playing { get; set; }

            /// <summary>
            /// Gets or sets the count of users who have retired from playing the game.
            /// </summary>
            public int count_retired { get; set; }

            /// <summary>
            /// Gets or sets the developer of the game.
            /// </summary>
            public string profile_dev { get; set; }

            /// <summary>
            /// Gets or sets the popularity rank of the game on the HowLongToBeat website.
            /// </summary>
            public int profile_popular { get; set; }

            /// <summary>
            /// Gets or sets the game's Steam ID, if available.
            /// </summary>
            public int profile_steam { get; set; }

            /// <summary>
            /// Gets or sets the platforms the game is available on.
            /// </summary>
            public string profile_platform { get; set; }

            /// <summary>
            /// Gets or sets the worldwide release year of the game.
            /// </summary>
            public int release_world { get; set; }

        }
    }
}