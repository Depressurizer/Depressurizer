#region License

//     This file (Profile.cs) is part of Depressurizer.
//     Copyright (C) 2017  Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Cache;
using System.Xml;
using Depressurizer.Properties;
using Newtonsoft.Json;

namespace Depressurizer.Models
{
    /// <summary>
    ///     Depressurizer Profile
    /// </summary>
    public sealed class Profile
    {
        private const long BaseSteamID = 76561197960265728;

        private Image _avatar = null;

        /// <summary>
        ///     User's Steam Avatar
        /// </summary>
        public Image Avatar
        {
            get
            {
                if (_avatar != null)
                {
                    return _avatar;
                }

                try
                {
                    XmlDocument doc = new XmlDocument();
                    string profile = string.Format(CultureInfo.InvariantCulture, Resources.UrlSteamProfile, SteamID64);
                    doc.Load(profile);

                    XmlNodeList nodeList = doc.SelectNodes(Resources.XmlNodeAvatar);
                    if (nodeList == null)
                    {
                        return null;
                    }

                    foreach (XmlNode node in nodeList)
                    {
                        string avatarURL = node.InnerText;
                        return _avatar = Utility.GetImage(avatarURL, RequestCacheLevel.BypassCache);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return null;
            }
        }

        /// <summary>
        ///     AutoCats
        /// </summary>
        public List<AutoCat> AutoCats { get; set; } = new List<AutoCat>();

        /// <summary>
        ///     Automatically Export
        /// </summary>
        public bool AutoExport { get; set; } = true;

        /// <summary>
        ///     Automatically Ignore
        /// </summary>
        public bool AutoIgnore { get; set; } = true;

        /// <summary>
        ///     Automatically Import
        /// </summary>
        public bool AutoImport { get; set; } = true;

        /// <summary>
        ///     Automatically Update
        /// </summary>
        public bool AutoUpdate { get; set; } = true;

        /// <summary>
        ///     Bypass ignore list on import
        /// </summary>
        public bool BypassIgnoreOnImport { get; set; } = false;

        /// <summary>
        ///     Discard changes on export
        /// </summary>
        public bool ExportDiscard { get; set; } = true;

        /// <summary>
        ///     Location of profile
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        ///     Game Data
        /// </summary>
        public GameList GameData { get; set; } = new GameList();

        /// <summary>
        ///     Apps to ignore
        /// </summary>
        public SortedSet<int> IgnoreList { get; set; } = new SortedSet<int>();

        /// <summary>
        ///     Include Shortcuts
        /// </summary>
        public bool IncludeShortcuts { get; set; } = true;

        /// <summary>
        ///     Include Unknown App Types
        /// </summary>
        public bool IncludeUnknown { get; set; } = false;

        /// <summary>
        ///     Update from local
        /// </summary>
        public bool LocalUpdate { get; set; } = true;

        /// <summary>
        ///     Overwrite on download
        /// </summary>
        public bool OverwriteOnDownload { get; set; } = false;

        /// <summary>
        ///     User's Steam ID64
        /// </summary>
        public long SteamID64 { get; set; } = 0;

        /// <summary>
        ///     Update from web
        /// </summary>
        public bool WebUpdate { get; set; } = true;

        /// <summary>
        ///     Clones AutoCat list and applies filter if supplied
        /// </summary>
        /// <param name="autoCatList">List to clone</param>
        /// <param name="filter">Optional filter</param>
        /// <returns>Cloned list</returns>
        public List<AutoCat> CloneAutoCatList(List<string> autoCatList, Filter filter)
        {
            List<AutoCat> autoCats = new List<AutoCat>();

            foreach (string autoCatName in autoCatList)
            {
                AutoCat autoCat = GetAutoCat(autoCatName);
                if (autoCat == null)
                {
                    continue;
                }

                AutoCat autoCatClone = autoCat.Clone();
                if (filter != null)
                {
                    autoCatClone.Filter = filter.Name;
                }
                autoCats.Add(autoCatClone);
            }

            return autoCats;
        }

        /// <summary>
        ///     Export Data to Steam
        /// </summary>
        public void ExportSteamData()
        {
            GameData.ExportSteamConfig(SteamID64, ExportDiscard, IncludeShortcuts);
        }

        /// <summary>
        ///     Generates default autocats and adds them to a list
        /// </summary>
        /// <param name="list">The list where the autocats should be added</param>
        public static void GenerateDefaultAutoCatSet(List<AutoCat> list)
        {
            //By Genre
            list.Add(new AutoCatGenre(GlobalStrings.Profile_DefaultAutoCatName_Genre, null, "(" + GlobalStrings.Name_Genre + ") "));

            //By Year
            list.Add(new AutoCatYear(GlobalStrings.Profile_DefaultAutoCatName_Year, null, "(" + GlobalStrings.Name_Year + ") "));

            //By Score
            AutoCatUserScore ac = new AutoCatUserScore(GlobalStrings.Profile_DefaultAutoCatName_UserScore, null, "(" + GlobalStrings.Name_Score + ") ");
            ac.GenerateSteamRules(ac.Rules);
            list.Add(ac);

            //By Tags
            AutoCatTags act = new AutoCatTags(GlobalStrings.Profile_DefaultAutoCatName_Tags, null, "(" + GlobalStrings.Name_Tags + ") ");
            foreach (Tuple<string, float> tag in Program.GameDB.CalculateSortedTagList(null, 1, 20, 0, false, false))
            {
                act.IncludedTags.Add(tag.Item1);
            }
            list.Add(act);

            //By Flags
            AutoCatFlags acf = new AutoCatFlags(GlobalStrings.Profile_DefaultAutoCatName_Flags, null, "(" + GlobalStrings.Name_Flags + ") ");
            foreach (string flag in Program.GameDB.GetAllStoreFlags())
            {
                acf.IncludedFlags.Add(flag);
            }
            list.Add(acf);

            //By HLTB
            AutoCatHltb ach = new AutoCatHltb(GlobalStrings.Profile_DefaultAutoCatName_Hltb, null, "(HLTB) ", false);
            ach.Rules.Add(new Hltb_Rule(" 0-5", 0, 5, TimeType.Extras));
            ach.Rules.Add(new Hltb_Rule(" 5-10", 5, 10, TimeType.Extras));
            ach.Rules.Add(new Hltb_Rule("10-20", 10, 20, TimeType.Extras));
            ach.Rules.Add(new Hltb_Rule("20-50", 20, 50, TimeType.Extras));
            ach.Rules.Add(new Hltb_Rule("50+", 20, 0, TimeType.Extras));
            list.Add(ach);

            //By Platform
            AutoCatPlatform acPlatform = new AutoCatPlatform(GlobalStrings.Profile_DefaultAutoCatName_Platform, null, "(" + GlobalStrings.AutoCat_Name_Platform + ") ", true, true, true, true);
            list.Add(acPlatform);
        }

        /// <summary>
        ///     Get AutoCat by name
        /// </summary>
        /// <param name="autoCatName">Name of AutoCat to find</param>
        /// <returns>AutoCat</returns>
        public AutoCat GetAutoCat(string autoCatName)
        {
            if (string.IsNullOrEmpty(autoCatName))
            {
                return null;
            }

            foreach (AutoCat autoCat in AutoCats)
            {
                if (string.Equals(autoCat.Name, autoCatName, StringComparison.OrdinalIgnoreCase))
                {
                    return autoCat;
                }
            }

            return null;
        }

        /// <summary>
        ///     Import Data from Steam
        /// </summary>
        public int ImportSteamData()
        {
            AppTypes included = AppTypes.IncludeNormal;
            if (BypassIgnoreOnImport)
            {
                included = AppTypes.IncludeAll;
            }
            else if (IncludeUnknown)
            {
                included |= AppTypes.Unknown;
            }

            return GameData.ImportSteamConfig(SteamID64, IgnoreList, included, IncludeShortcuts);
        }

        /// <summary>
        ///     Load profile from specified path
        /// </summary>
        /// <param name="localPath">Path to load from</param>
        /// <returns>Profile from path or null</returns>
        public static Profile Load(string localPath)
        {
            if (string.IsNullOrEmpty(localPath) || !File.Exists(localPath))
            {
                return null;
            }

            try
            {
                string jsonProfile = File.ReadAllText(localPath);
                Profile profile = JsonConvert.DeserializeObject<Profile>(jsonProfile, Program.SerializerSettings);
                profile.FilePath = localPath;

                return profile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     Save profile
        /// </summary>
        public void Save()
        {
            Save(FilePath);
        }

        /// <summary>
        ///     Save profile to specified location
        /// </summary>
        /// <param name="localPath">Path to save to</param>
        /// <returns>Success or not</returns>
        public bool Save(string localPath)
        {
            try
            {
                string jsonProfile = JsonConvert.SerializeObject(this, Program.SerializerSettings);
                File.WriteAllText(localPath, jsonProfile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            FilePath = localPath;
            return true;
        }

        /// <summary>
        ///     Convert SteamID64 to Steam3ID
        /// </summary>
        /// <param name="steamID64">SteamID64 to convert</param>
        /// <returns>Steam3ID</returns>
        public static int ToSteam3ID(long steamID64)
        {
            return int.Parse((steamID64 - BaseSteamID).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert Steam3ID to SteamID64
        /// </summary>
        /// <param name="steam3ID">Steam3ID to convert</param>
        /// <returns>SteamID64</returns>
        public static long ToSteamID64(int steam3ID)
        {
            return steam3ID + BaseSteamID;
        }

        /// <summary>
        ///     Convert Steam3ID to SteamID64
        /// </summary>
        /// <param name="steam3ID">Steam3ID to convert</param>
        /// <returns>SteamID64</returns>
        public static long ToSteamID64(string steam3ID)
        {
            return ToSteamID64(int.Parse(steam3ID, CultureInfo.InvariantCulture));
        }
    }
}