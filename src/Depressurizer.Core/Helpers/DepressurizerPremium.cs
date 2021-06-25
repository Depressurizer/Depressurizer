using Depressurizer.Core.Enums;
using Depressurizer.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Depressurizer.Core.Helpers
{
    public class DepressurizerPremium
    {
        class DepressurizerPremiumResponse
        {
            public int appId { get; set; }
            public string name { get; set; }
            public string appType { get; set; }
            public List<string> flags { get; set; }
            public List<string> genres { get; set; }
            public List<string> tags { get; set; }
            public List<string> developers { get; set; }
            public List<string> publishers { get; set; }
            public List<string> virtualRealityHeadsets { get; set; }
            public List<string> virtualRealityInput { get; set; }
            public List<string> virtualRealityPlayArea { get; set; }
            public int releaseYear { get; set; }
            public string releaseDate { get; set; }
            public int totalAchievements { get; set; }
            public int totalReview { get; set; }
            public int totalPositiveReview { get; set; }
            public double reviewPositivePercentage { get; set; }
            public string metacriticLink { get; set; }
            public bool supportsLinux { get; set; }
            public bool supportsMac { get; set; }
            public bool supportsWindows { get; set; }
            public int hltbCompletionists { get; set; }
            public int hltbExtras { get; set; }
            public int hltbMain { get; set; }
        }

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        public static AppType load(DatabaseEntry entry, string steamWebApi, string languageCode)
        {
            string url = string.Format("{0}/{1}?key={2}&language={3}", Settings.Instance.PremiumServer, entry.AppId, steamWebApi, languageCode);

            try
            {
                return load(entry, new Uri(url));
            }
            catch (Exception e)
            {
                Logger.Error("Could not load Premium API ({0}) due to: {1}", url, e.ToString());
            }

            return AppType.Unknown;
        }

        public static AppType load(DatabaseEntry entry, Uri uri)
        {
            HttpClient client = new HttpClient();
            using (Stream s = client.GetStreamAsync(uri).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                DepressurizerPremiumResponse response = new JsonSerializer().Deserialize<DepressurizerPremiumResponse>(reader);
                if (!Enum.TryParse(response.appType, true, out AppType type) || type == AppType.Unknown)
                {
                    return AppType.Unknown;
                }

                entry.Name = response.name;
                entry.AppType = type;
                entry.SteamReleaseDate = response.releaseDate;
                entry.TotalAchievements = response.totalAchievements;
                entry.ReviewTotal = response.totalReview;
                entry.ReviewPositivePercentage = (int)Math.Round(response.totalPositiveReview / (double)response.totalReview * 100.00);
                entry.MetacriticUrl = response.metacriticLink;
                entry.HltbCompletionists = response.hltbCompletionists;
                entry.HltbExtras = response.hltbExtras;
                entry.HltbMain = response.hltbMain;

                entry.Platforms = 0;
                if (response.supportsLinux)
                {
                    entry.Platforms |= AppPlatforms.Linux;
                }
                if (response.supportsMac)
                {
                    entry.Platforms |= AppPlatforms.Mac;
                }
                if (response.supportsWindows)
                {
                    entry.Platforms |= AppPlatforms.Windows;
                }

                entry.Flags.Clear();
                foreach (string flag in response.flags)
                {
                    entry.Flags.Add(flag);
                }

                entry.Genres.Clear();
                foreach (string genre in response.genres)
                {
                    entry.Genres.Add(genre);
                }

                entry.Tags.Clear();
                foreach (string tag in response.tags)
                {
                    entry.Tags.Add(tag);
                }

                entry.Developers.Clear();
                foreach (string developer in response.developers)
                {
                    entry.Developers.Add(developer);
                }

                entry.Publishers.Clear();
                foreach (string publisher in response.publishers)
                {
                    entry.Publishers.Add(publisher);
                }

                entry.VRSupport = new VRSupport()
                {
                    Headsets = response.virtualRealityHeadsets,
                    Input = response.virtualRealityInput,
                    PlayArea = response.virtualRealityPlayArea
                };

                entry.LastStoreScrape = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }

            return entry.AppType;
        }
    }
}
