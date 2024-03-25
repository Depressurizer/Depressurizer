using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fastenshtein;
using Newtonsoft.Json;
using RandomUserAgent;
using AngleSharp;
using AngleSharp.Dom;

namespace Depressurizer.Core.Helpers
{
    public class HowLongToBeatService
    {
        private HltbSearch hltb = new HltbSearch();

        public HowLongToBeatService() { }

        public async Task<HowLongToBeatEntry> Detail(string gameId, CancellationToken signal = default)
        {
            var detailPage = await hltb.DetailHtml(gameId, signal);
            var entry = HowLongToBeatParser.ParseDetails(detailPage, gameId);
            return entry;
        }

        public async Task<List<HowLongToBeatEntry>> Search(string query, CancellationToken signal = default)
        {
            var searchTerms = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var search = await hltb.Search(searchTerms, signal);
            var hltbEntries = new List<HowLongToBeatEntry>();
            foreach (var resultEntry in search.data)
            {
                hltbEntries.Add(new HowLongToBeatEntry(
                    resultEntry.id.ToString(),
                    resultEntry.name,
                    "", // no description
                    resultEntry.platforms != null ? resultEntry.platforms : new string[0],
                    HltbSearch.IMAGE_URL + resultEntry.imageUrl,
                    new List<string[]> { new string[] { "Main", "Main" }, new string[] { "Main + Extra", "Main + Extra" }, new string[] { "Completionist", "Completionist" } },
                    (int)Math.Round(resultEntry.gameplayMain / 3600.0),
                    (int)Math.Round(resultEntry.gameplayMainExtra / 3600.0),
                    (int)Math.Round(resultEntry.gameplayCompletionist / 3600.0),
                    CalcDistancePercentage(resultEntry.name, query),
                    query
                ));
            }
            return hltbEntries;
        }

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

    public class HowLongToBeatEntry
    {
        public readonly string[] PlayableOn;
        public readonly string Id;
        public readonly string Name;
        public readonly string Description;
        public readonly string[] Platforms;
        public readonly string ImageUrl;
        public readonly List<string[]> TimeLabels;
        public readonly int GameplayMain;
        public readonly int GameplayMainExtra;
        public readonly int GameplayCompletionist;
        public readonly double Similarity;
        public readonly string SearchTerm;

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
    }

    public class HowLongToBeatParser
    {
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


    public class HltbSearch
    {
        public static string BASE_URL = "https://howlongtobeat.com/";
        public static string DETAIL_URL = $"{BASE_URL}game?id=";
        public static string SEARCH_URL = $"{BASE_URL}api/search";
        public static string IMAGE_URL = $"{BASE_URL}games/";

        private HttpClient httpClient;

        public HltbSearch()
        {
            httpClient = new HttpClient();
        }

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

        public async Task<SearchResponse> Search(List<string> query, CancellationToken? cancellationToken = null)
        {
            var payload = new SearchPayload
            {
                searchType = "games",
                searchTerms = query,
                searchPage = 1,
                size = 20,
                searchOptions = new SearchOptions
                {
                    games = new GamesOptions
                    {
                        userId = 0,
                        platform = "",
                        sortCategory = "popular",
                        rangeCategory = "main",
                        rangeTime = new RangeTime { min = 0, max = 0 },
                        gameplay = new GameplayOptions { perspective = "", flow = "", genre = "" },
                        modifier = ""
                    },
                    users = new UsersOptions { sortCategory = "postcount" },
                    filter = "",
                    sort = 0,
                    randomizer = 0
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
                return JsonConvert.DeserializeObject<SearchResponse>(jsonString);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Failed to perform search.", ex);
            }
        }
    }

    public class SearchResponse
    {
        public SearchEntry[] data { get; set; } 
    }

    public class SearchEntry
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string[] platforms { get; set; }
        public string imageUrl { get; set; }
        public string[][] timeLabels { get; set; }
        public int gameplayMain { get; set; }
        public int gameplayMainExtra { get; set; }
        public int gameplayCompletionist { get; set; }
        public double similarity { get; set; }
        public string searchTerm { get; set; }
        public string[] playableOn { get; set; }
    }

    public class SearchPayload
    {
        public string searchType { get; set; }
        public List<string> searchTerms { get; set; }
        public int searchPage { get; set; }
        public int size { get; set; }
        public SearchOptions searchOptions { get; set; }
    }

    public class SearchOptions
    {
        public GamesOptions games { get; set; }
        public UsersOptions users { get; set; }
        public string filter { get; set; }
        public int sort { get; set; }
        public int randomizer { get; set; }
    }

    public class GamesOptions
    {
        public int userId { get; set; }
        public string platform { get; set; }
        public string sortCategory { get; set; }
        public string rangeCategory { get; set; }
        public RangeTime rangeTime { get; set; }
        public GameplayOptions gameplay { get; set; }
        public string modifier { get; set; }
    }

    public class UsersOptions
    {
        public string sortCategory { get; set; }
    }

    public class RangeTime
    {
        public int min { get; set; }
        public int max { get; set; }
    }

    public class GameplayOptions
    {
        public string perspective { get; set; }
        public string flow { get; set; }
        public string genre { get; set; }
    }
}



/* [
  HowLongToBeatEntry {
    id: '36936',
    name: 'Nioh',
    description: '',
    platforms: [ 'PC', 'PlayStation 4' ],
    imageUrl: 'https://howlongtobeat.com/games/36936_Nioh.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 35,
    gameplayMainExtra: 64,
    gameplayCompletionist: 97,
    similarity: 1,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4' ]
  },
  HowLongToBeatEntry {
    id: '85713',
    name: 'Nioh 2: Complete Edition',
    description: '',
    platforms: [ 'PC', 'PlayStation 4', 'PlayStation 5' ],
    imageUrl: 'https://howlongtobeat.com/games/85713_Nioh_2_Complete_Edition.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 43,
    gameplayMainExtra: 87,
    gameplayCompletionist: 130,
    similarity: 0.17,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4', 'PlayStation 5' ]
  },
  HowLongToBeatEntry {
    id: '50419',
    name: 'Nioh: Complete Edition',
    description: '',
    platforms: [ 'PC', 'PlayStation 4', 'PlayStation 5' ],
    imageUrl: 'https://howlongtobeat.com/games/50419_Nioh_Complete_Edition.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 42,
    gameplayMainExtra: 74,
    gameplayCompletionist: 143,
    similarity: 0.18,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4', 'PlayStation 5' ]
  },
  HowLongToBeatEntry {
    id: '60877',
    name: 'Nioh 2',
    description: '',
    platforms: [ 'PC', 'PlayStation 4' ],
    imageUrl: 'https://howlongtobeat.com/games/60877_Nioh_2.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 45,
    gameplayMainExtra: 73,
    gameplayCompletionist: 106,
    similarity: 0.67,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4' ]
  },
  HowLongToBeatEntry {
    id: '84043',
    name: 'Nioh 2 - Darkness in the Capital',
    description: '',
    platforms: [ 'PC', 'PlayStation 4', 'PlayStation 5' ],
    imageUrl: 'https://howlongtobeat.com/games/84043_Nioh_2_-_Darkness_in_the_Capital.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 5,
    gameplayMainExtra: 11,
    gameplayCompletionist: 12,
    similarity: 0.13,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4', 'PlayStation 5' ]
  },
  HowLongToBeatEntry {
    id: '81537',
    name: "Nioh 2 - The Tengu's Disciple",
    description: '',
    platforms: [ 'PC', 'PlayStation 4', 'PlayStation 5' ],
    imageUrl: 'https://howlongtobeat.com/games/81537_Nioh_2_-_The_Tengus_Disciple.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 4,
    gameplayMainExtra: 10,
    gameplayCompletionist: 11,
    similarity: 0.14,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4', 'PlayStation 5' ]
  },
  HowLongToBeatEntry {
    id: '85711',
    name: 'Nioh 2 - The First Samurai',
    description: '',
    platforms: [ 'PC', 'PlayStation 4', 'PlayStation 5' ],
    imageUrl: 'https://howlongtobeat.com/games/85711_Nioh_2_-_The_First_Samurai.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 4,
    gameplayMainExtra: 9,
    gameplayCompletionist: 21,
    similarity: 0.15,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4', 'PlayStation 5' ]
  },
  HowLongToBeatEntry {
    id: '50087',
    name: "Nioh - Bloodshed's End DLC",
    description: '',
    platforms: [ 'PC', 'PlayStation 4' ],
    imageUrl: 'https://howlongtobeat.com/games/50087_Nioh_-_Bloodsheds_End_DLC.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 6,
    gameplayMainExtra: 10,
    gameplayCompletionist: 16,
    similarity: 0.15,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4' ]
  },
  HowLongToBeatEntry {
    id: '47796',
    name: 'Nioh - Defiant Honor DLC',
    description: '',
    platforms: [ 'PC', 'PlayStation 4' ],
    imageUrl: 'https://howlongtobeat.com/games/47796_Nioh_-_Defiant_Honor_DLC.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 5,
    gameplayMainExtra: 9,
    gameplayCompletionist: 15,
    similarity: 0.17,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4' ]
  },
  HowLongToBeatEntry {
    id: '46360',
    name: 'Nioh - Dragon of the North DLC',
    description: '',
    platforms: [ 'PC', 'PlayStation 4' ],
    imageUrl: 'https://howlongtobeat.com/games/46360_Nioh_-_Dragon_of_the_north_DLC.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 7,
    gameplayMainExtra: 10,
    gameplayCompletionist: 12,
    similarity: 0.13,
    searchTerm: 'Nioh',
    playableOn: [ 'PC', 'PlayStation 4' ]
  },
  HowLongToBeatEntry {
    id: '94652',
    name: 'Nioh Collection',
    description: '',
    platforms: [ 'PlayStation 5' ],
    imageUrl: 'https://howlongtobeat.com/games/94652_Nioh_Collection.jpg',
    timeLabels: [ [Array], [Array], [Array] ],
    gameplayMain: 0,
    gameplayMainExtra: 160,
    gameplayCompletionist: 0,
    similarity: 0.27,
    searchTerm: 'Nioh',
    playableOn: [ 'PlayStation 5' ]
  }
]
*/