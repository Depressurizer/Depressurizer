using Newtonsoft.Json;

// ReSharper disable All

#pragma warning disable 1591

namespace Depressurizer.Core.Models
{
    public class AppList_RawData
    {
        #region Public Properties

        [JsonProperty("applist")]
        public Applist Applist { get; set; }

        #endregion
    }

    public class Applist
    {
        #region Public Properties

        [JsonProperty("apps")]
        public App[] Apps { get; set; }

        #endregion
    }

    public class App
    {
        #region Public Properties

        [JsonProperty("appid")]
        public int AppId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion
    }
}
