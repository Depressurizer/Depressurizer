using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LevelDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Depressurizer.Core.Models
{
    public class SteamLevelDB
    {
        private readonly string databasePath;
        private readonly string steamID3;

        private string KeyPrefix => $"_https://steamloopback.host\u0000\u0001U{steamID3}-cloud-storage-namespace-1";

        public SteamLevelDB(string steamID3)
        {
            this.databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Steam", "htmlcache", "Local Storage", "leveldb");
            this.steamID3 = steamID3;
        }

        public List<CloudStorageNamespace.Element.SteamCollectionValue> getSteamCollections()
        {
            var options = new Options()
            {
                ParanoidChecks = true,
            };
            var db = new DB(options, this.databasePath);

            string data = db.Get(KeyPrefix).Replace("\x01", "").Replace("\0", "");

            db.Close();

            CloudStorageNamespace collections = new CloudStorageNamespace();
            foreach (JToken item in JArray.Parse(data).Children())
            {
                collections.children.Add(item[0].ToString(), JsonConvert.DeserializeObject<CloudStorageNamespace.Element>(item[1].ToString(), new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                }));
            }

            List<CloudStorageNamespace.Element.SteamCollectionValue> steamCollections = new List<CloudStorageNamespace.Element.SteamCollectionValue>();
            foreach (var item in collections.children.Values)
            {
                if (item.key.StartsWith("user-collections") && !item.is_deleted)
                {
                    if (item.collectionValue != null)
                    {
                        steamCollections.Add(item.collectionValue);
                    }
                }
            }

            return steamCollections;
        }

        public void setSteamCollections(Dictionary<int, GameInfo> Games)
        {
            var categoryData = new Dictionary<string, List<int>>();

            // Prepare output categories
            foreach (GameInfo game in Games.Values)
            {
                foreach (Category c in game.Categories)
                {
                    string categoryName = c.Name.ToUpper();

                    if (!categoryData.ContainsKey(categoryName))
                    {
                        categoryData[categoryName] = new List<int>();
                    }

                    categoryData[categoryName].Add(game.Id);
                }
            }

            var newArray = GenerateCategories(categoryData);

            // Save the new categories in leveldb
            var options = new Options()
            {
                ParanoidChecks = true,
            };
            var db = new DB(options, this.databasePath);

            string data = db.Get(KeyPrefix).Replace("\x01", "").Replace("\0", "");
            var existingArray = JArray.Parse(data);

            JObject existingObj = ToObjectByKey(existingArray);
            JObject newObj = ToObjectByKey(newArray);

            existingObj.Merge(newObj, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            JArray mergedArray = ToArrayFromKeyedObject(existingObj);

            db.Put(KeyPrefix, mergedArray.ToString(Formatting.None));
            db.Close();
        }

        private static JArray GenerateCategories(Dictionary<string, List<int>> categoryData)
        {
            var result = new JArray();
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string version = DateTime.UtcNow.ToString("yyyyMMdd");

            foreach (var entry in categoryData)
            {
                string categoryName = entry.Key;
                List<int> gameIds = entry.Value;

                string id = "uc-" + GetDeterministicId(categoryName);
                string key = "user-collections." + id;

                var inner = new JObject
                {
                    ["key"] = key,
                    ["timestamp"] = timestamp,
                    ["value"] = JsonConvert.SerializeObject(new
                    {
                        id = id,
                        name = categoryName.ToUpper(),
                        added = gameIds,
                        removed = new List<int>()
                    }),
                    ["version"] = version,
                    ["conflictResolutionMethod"] = "custom",
                    ["strMethodId"] = "union-collections"
                };

                result.Add(new JArray { key, inner });
            }

            return result;
        }

        private static JObject ToObjectByKey(JArray array)
        {
            var obj = new JObject();
            foreach (var item in array)
            {
                if (item is JArray arr && arr.Count == 2)
                {
                    obj[arr[0]?.ToString()] = arr[1];
                }
            }
            return obj;
        }

        private static JArray ToArrayFromKeyedObject(JObject obj)
        {
            var array = new JArray();
            foreach (var prop in obj)
            {
                array.Add(new JArray { prop.Key, prop.Value });
            }
            return array;
        }

        private static string GetDeterministicId(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input.ToLowerInvariant()));
                return Convert.ToBase64String(hash).Replace("+", "").Replace("/", "").Replace("=", "").Substring(0, 12);
            }
        }

        public class CloudStorageNamespace
        {
            public Dictionary<string, Element> children { get; } = new Dictionary<string, Element>();

            public class Element
            {
                private SteamCollectionValue collectionValue1;

                public string key { get; set; }
                public int timestamp { get; set; }
                public bool is_deleted { get; set; }
                public string value { get; set; }

                public SteamCollectionValue collectionValue
                {
                    get => collectionValue1 ?? (collectionValue1 = JsonConvert.DeserializeObject<SteamCollectionValue>(value));
                    set => collectionValue1 = value;
                }

                public class SteamCollectionValue
                {
                    public string id { get; set; }
                    public string name { get; set; }
                    public int[] added { get; set; }
                    public int[] removed { get; set; }
                }
            }
        }

    }

}
