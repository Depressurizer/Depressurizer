using System.Collections.Generic;
using Depressurizer.Core.Enums;
using Newtonsoft.Json;

// ReSharper disable All

#pragma warning disable 1591

namespace Depressurizer.Core.Models
{
    public sealed class HLTBSearchEntry
    {
        #region Public Properties

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("platforms")]
        public string[] Platforms { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("timeLabels")]
        public string[][] TimeLabels { get; set; }

        [JsonProperty("gameplayMain")]
        public int GameplayMain { get; set; }

        [JsonProperty("gameplayMainExtra")]
        public int GameplayMainExtra { get; set; }

        [JsonProperty("gameplayCompletionist")]
        public int GameplayCompletionist { get; set; }

        [JsonProperty("similarity")]
        public double Similarity { get; set; }

        [JsonProperty("searchTerm")]
        public string SearchTerm { get; set; }

        [JsonProperty("playableOn")]
        public string[] PlayableOn { get; set; }

        #endregion
    }
}
