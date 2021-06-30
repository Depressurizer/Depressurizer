using IronLevelDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Depressurizer.Core.Models
{
    public class SteamLevelDB
    {
        private readonly IIronLeveldb internalDatabase;
        private readonly string steamID3;

        private string KeyPrefix => $"_https://steamloopback.host\u0000\u0001U{steamID3}-cloud-storage-namespace-1";

        public SteamLevelDB(string steamID3)
        {
            this.internalDatabase = IronLeveldbBuilder.BuildFromPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Steam", "htmlcache", "Local Storage", "leveldb"));
            this.steamID3 = steamID3;
        }

        public List<CloudStorageNamespace.Element.SteamCollectionValue> getSteamCollections()
        {
            // IEnumerable<IByteArrayKeyValuePair> data = internalDatabase.SeekFirst();
            string data = internalDatabase.GetAsString(KeyPrefix);

            CloudStorageNamespace collections = new CloudStorageNamespace();
            foreach (JToken item in JArray.Parse(data.Substring(1)).Children())
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
