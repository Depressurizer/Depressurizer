using System.Collections.Generic;
using Depressurizer.Core.Enums;
using Newtonsoft.Json;

// ReSharper disable All

#pragma warning disable 1591

namespace Depressurizer.Core.Models
{
    public sealed class HLTBSearchPayload
    {
        #region Public Properties

        [JsonProperty("searchType")]
        public string SearchType { get; set; }

        [JsonProperty("searchTerms")]
        public List<string> SearchTerms { get; set; }

        [JsonProperty("searchPage")]
        public int SearchPage { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("searchOptions")]
        public SearchOptionsProperty SearchOptions { get; set; }
        #endregion

        #region Nested Types


        public class Game
        {
            #region Public Properties

            [JsonProperty("Playtime")]
            public long Playtime { get; set; }

            [JsonProperty("SteamAppData")]
            public SteamAppData SteamAppData { get; set; }

            #endregion
        }

        public class SearchOptionsProperty
        {
            [JsonProperty("games")]
            public GamesOptions Games { get; set; }

            [JsonProperty("users")]
            public UsersOptions Users { get; set; }

            [JsonProperty("filter")]
            public string Filter { get; set; }

            [JsonProperty("sort")]
            public int Sort { get; set; }

            [JsonProperty("randomizer")]
            public int Randomizer { get; set; }

        }

        public class GamesOptions
        {
            [JsonProperty("userId")]
            public int UserId { get; set; }

            [JsonProperty("platform")]
            public string Platform { get; set; }

            [JsonProperty("sortCategory")]
            public string SortCategory { get; set; }

            [JsonProperty("rangeCategory")]
            public string RangeCategory { get; set; }

            [JsonProperty("rangeTime")]
            public RangeTime RangeTime { get; set; }

            [JsonProperty("gameplay")]
            public GameplayOptions Gameplay { get; set; }

            [JsonProperty("modifier")]
            public string Modifier { get; set; }

        }

        public class UsersOptions
        {
            [JsonProperty("sortCategory")]
            public string SortCategory { get; set; }

        }

        public class RangeTime
        {
            [JsonProperty("min")]
            public int Min { get; set; }

            [JsonProperty("max")]
            public int Max { get; set; }

        }

        public class GameplayOptions
        {
            [JsonProperty("perspective")]
            public string Perspective { get; set; }

            [JsonProperty("flow")]
            public string Flow { get; set; }

            [JsonProperty("genre")]
            public string Genre { get; set; }

        }

        #endregion
    }
}
