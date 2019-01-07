using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using Depressurizer.Core.Models;
using Depressurizer.Models;
using Newtonsoft.Json;

namespace Depressurizer
{
    public class Profile : IProfile
    {
        #region Constants

        private const string XmlNameAutoCatList = "autocats";

        private const string XmlNameAutoExport = "auto_export";

        private const string XmlNameAutoIgnore = "auto_ignore";

        private const string XmlNameAutoImport = "auto_import";

        private const string XmlNameAutoUpdate = "auto_update";

        private const string XmlNameBypassIgnoreOnImport = "bypass_ignore_on_import";

        private const string XmlNameExclusion = "exclusion";

        private const string XmlNameExclusionList = "exclusions";

        private const string XmlNameExportDiscard = "export_discard";

        private const string XmlNameFilter = "Filter";

        private const string XmlNameFilterAllow = "Allow";

        private const string XmlNameFilterExclude = "Exclude";

        private const string XmlNameFilterHidden = "Hidden";

        private const string XmlNameFilterList = "Filters";

        private const string XmlNameFilterName = "Name";

        private const string XmlNameFilterRequire = "Require";

        private const string XmlNameFilterUncategorized = "Uncategorized";

        private const string XmlNameFilterVR = "VR";

        private const string XmlNameGame = "game";

        private const string XmlNameGameCategory = "category";

        private const string XmlNameGameCategoryList = "categories";

        private const string XmlNameGameExecutable = "executable";

        private const string XmlNameGameHidden = "hidden";

        private const string XmlNameGameId = "id";

        private const string XmlNameGameLastPlayed = "lastplayed";

        private const string XmlNameGameList = "games";

        private const string XmlNameGameName = "name";

        private const string XmlNameGameSource = "source";

        private const string XmlNameIncludeShortcuts = "include_shortcuts";

        private const string XmlNameIncludeUnknown = "include_unknown";

        private const string XmlNameLocalUpdate = "local_update";

        private const string XmlNameOldSteamIdShort = "account_id";

        private const string XmlNameOverwriteNames = "overwrite_names";

        private const string XmlNameProfile = "profile";

        private const string XmlNameSteamId = "steam_id_64";

        private const string XmlNameWebUpdate = "web_update";

        #endregion

        #region Public Properties

        public List<AutoCat> AutoCats { get; set; } = new List<AutoCat>();

        public bool AutoExport { get; set; } = true;

        public bool AutoIgnore { get; set; } = true;

        public bool AutoImport { get; set; } = true;

        public bool AutoUpdate { get; set; } = true;

        [JsonIgnore]
        public Image Avatar
        {
            get
            {
                XmlDocument xml = new XmlDocument();
                string profile = string.Format(CultureInfo.InvariantCulture, Constants.SteamProfile, SteamID64);

                try
                {
                    xml.Load(profile);
                }
                catch (Exception e)
                {
                    Logger.Warn("Profile: Error while loading profile from '{0}', exception: {1}.", profile, e);
                    return null;
                }

                XmlNodeList avatarIcon = xml.SelectNodes("/profile/avatarIcon");
                if (avatarIcon == null || avatarIcon.Count == 0)
                {
                    return null;
                }

                string url = avatarIcon[0].InnerText;
                if (string.IsNullOrWhiteSpace(url))
                {
                    return null;
                }

                try
                {
                    return Utility.GetImage(url);
                }
                catch (Exception e)
                {
                    Logger.Warn("Profile: Error while downloading avatar from '{0}', exception: {1}.", url, e);
                    return null;
                }
            }
        }

        public bool BypassIgnoreOnImport { get; set; }

        public bool ExportDiscard { get; set; } = true;

        public string FilePath { get; set; }

        public GameList GameData { get; set; } = new GameList();

        public SortedSet<int> IgnoreList { get; set; } = new SortedSet<int>();

        public bool IncludeShortcuts { get; set; } = true;

        public bool IncludeUnknown { get; set; }

        public bool LocalUpdate { get; set; } = true;

        public bool OverwriteOnDownload { get; set; }

        public long SteamID64 { get; set; }

        public bool WebUpdate { get; set; } = true;

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        private static Logger Logger => Logger.Instance;

        private static long ProfileConstant => 0x0110000100000000;

        #endregion

        #region Public Methods and Operators

        public static void GenerateDefaultAutoCatSet(List<AutoCat> list)
        {
            if (list == null)
            {
                list = new List<AutoCat>();
            }

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
            foreach (KeyValuePair<string, float> tag in Database.CalculateSortedTagList(null, 1, 20, 0, false, false))
            {
                act.IncludedTags.Add(tag.Key);
            }

            list.Add(act);

            //By Flags
            AutoCatFlags acf = new AutoCatFlags(GlobalStrings.Profile_DefaultAutoCatName_Flags, null, "(" + GlobalStrings.Name_Flags + ") ");
            foreach (string flag in Database.AllFlags)
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

        public static Profile Load(string path)
        {
            Logger.Info(GlobalStrings.Profile_LoadingProfile, path);
            Profile profile = new Profile
            {
                FilePath = path
            };

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(path);
            }
            catch (Exception e)
            {
                Logger.Warn(GlobalStrings.Profile_FailedToLoadProfile, e.Message);
                throw new ApplicationException(GlobalStrings.Profile_ErrorLoadingProfile + e.Message, e);
            }

            XmlNode profileNode = doc.SelectSingleNode("/" + XmlNameProfile);

            if (profileNode != null)
            {
                // Get the 64-bit Steam ID
                long accId = XmlUtil.GetInt64FromNode(profileNode[XmlNameSteamId], 0);
                if (accId == 0)
                {
                    string oldAcc = XmlUtil.GetStringFromNode(profileNode[XmlNameOldSteamIdShort], null);
                    if (oldAcc != null)
                    {
                        accId = ToSteamId64(oldAcc);
                    }
                }

                profile.SteamID64 = accId;

                // Get other attributes
                profile.AutoUpdate = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoUpdate], profile.AutoUpdate);

                profile.AutoImport = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoImport], profile.AutoImport);
                profile.AutoExport = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoExport], profile.AutoExport);

                profile.LocalUpdate = XmlUtil.GetBoolFromNode(profileNode[XmlNameLocalUpdate], profile.LocalUpdate);
                profile.WebUpdate = XmlUtil.GetBoolFromNode(profileNode[XmlNameWebUpdate], profile.WebUpdate);

                profile.IncludeUnknown = XmlUtil.GetBoolFromNode(profileNode[XmlNameIncludeUnknown], profile.IncludeUnknown);
                profile.BypassIgnoreOnImport = XmlUtil.GetBoolFromNode(profileNode[XmlNameBypassIgnoreOnImport], profile.BypassIgnoreOnImport);

                profile.ExportDiscard = XmlUtil.GetBoolFromNode(profileNode[XmlNameExportDiscard], profile.ExportDiscard);
                profile.AutoIgnore = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoIgnore], profile.AutoIgnore);
                profile.OverwriteOnDownload = XmlUtil.GetBoolFromNode(profileNode[XmlNameOverwriteNames], profile.OverwriteOnDownload);

                profile.IncludeShortcuts = XmlUtil.GetBoolFromNode(profileNode[XmlNameIncludeShortcuts], profile.IncludeShortcuts);

                XmlNode exclusionListNode = profileNode.SelectSingleNode(XmlNameExclusionList);
                XmlNodeList exclusionNodes = exclusionListNode?.SelectNodes(XmlNameExclusion);
                if (exclusionNodes != null)
                {
                    foreach (XmlNode node in exclusionNodes)
                    {
                        if (XmlUtil.TryGetIntFromNode(node, out int id))
                        {
                            profile.IgnoreList.Add(id);
                        }
                    }
                }

                XmlNode gameListNode = profileNode.SelectSingleNode(XmlNameGameList);
                XmlNodeList gameNodes = gameListNode?.SelectNodes(XmlNameGame);
                if (gameNodes != null)
                {
                    foreach (XmlNode node in gameNodes)
                    {
                        AddGameFromNode(node, profile);
                    }
                }

                XmlNode filterListNode = profileNode.SelectSingleNode(XmlNameFilterList);
                XmlNodeList filterNodes = filterListNode?.SelectNodes(XmlNameFilter);
                if (filterNodes != null)
                {
                    foreach (XmlNode node in filterNodes)
                    {
                        AddFilterFromNode(node, profile);
                    }
                }

                XmlNode autocatListNode = profileNode.SelectSingleNode(XmlNameAutoCatList);
                if (autocatListNode != null)
                {
                    XmlNodeList autoCatNodes = autocatListNode.ChildNodes;
                    foreach (XmlNode node in autoCatNodes)
                    {
                        XmlElement autocatElement = node as XmlElement;
                        if (node == null)
                        {
                            continue;
                        }

                        AutoCat autocat = AutoCat.LoadACFromXmlElement(autocatElement);
                        if (autocat != null)
                        {
                            profile.AutoCats.Add(autocat);
                        }
                    }
                }
                else
                {
                    GenerateDefaultAutoCatSet(profile.AutoCats);
                }

                //profile.AutoCats.Sort();
            }

            Logger.Info(GlobalStrings.MainForm_ProfileLoaded);
            return profile;
        }

        public static string ToSteam3Id(long id)
        {
            return (id - ProfileConstant).ToString(CultureInfo.InvariantCulture);
        }

        public static long ToSteamId64(string id)
        {
            if (long.TryParse(id, out long res))
            {
                return res + ProfileConstant;
            }

            return 0;
        }

        public List<AutoCat> CloneAutoCatList(List<string> autoCats, Filter filter)
        {
            List<AutoCat> newList = new List<AutoCat>();
            if (autoCats == null)
            {
                return newList;
            }

            foreach (string name in autoCats)
            {
                AutoCat autoCat = GetAutoCat(name);
                if (autoCat == null)
                {
                    continue;
                }

                AutoCat catClone = autoCat.Clone();
                if (filter != null)
                {
                    catClone.Filter = filter.Name;
                }

                newList.Add(catClone);
            }

            return newList;
        }

        public void ExportSteamData()
        {
            GameData.ExportSteamConfig(SteamID64, ExportDiscard, IncludeShortcuts);
        }

        public AutoCat GetAutoCat(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            foreach (AutoCat autoCat in AutoCats)
            {
                if (string.Equals(autoCat.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return autoCat;
                }
            }

            return null;
        }

        public int ImportSteamData()
        {
            return GameData.ImportSteamConfig(SteamID64, IgnoreList, IncludeShortcuts);
        }

        /// <inheritdoc />
        public void Save()
        {
            Save(FilePath);
        }

        /// <inheritdoc />
        public void Save(string path)
        {
            Logger.Info(GlobalStrings.Profile_SavingProfile, path);
            XmlWriterSettings writeSettings = new XmlWriterSettings
            {
                CloseOutput = true,
                Indent = true
            };

            try
            {
                Utility.BackupFile(path, Settings.Instance.ConfigBackupCount);
            }
            catch (Exception e)
            {
                Logger.Error(GlobalStrings.Log_Profile_ConfigBackupFailed, e.Message);
            }

            XmlWriter writer;
            try
            {
                writer = XmlWriter.Create(path, writeSettings);
            }
            catch (Exception e)
            {
                Logger.Warn(GlobalStrings.Profile_FailedToOpenProfileFile, e.Message);
                throw new ApplicationException(GlobalStrings.Profile_ErrorSavingProfileFile + e.Message, e);
            }

            writer.WriteStartElement(XmlNameProfile);

            writer.WriteElementString(XmlNameSteamId, SteamID64.ToString(CultureInfo.InvariantCulture));

            writer.WriteElementString(XmlNameAutoUpdate, AutoUpdate.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameAutoImport, AutoImport.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameAutoExport, AutoExport.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameLocalUpdate, LocalUpdate.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameWebUpdate, WebUpdate.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameExportDiscard, ExportDiscard.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameAutoIgnore, AutoIgnore.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameIncludeUnknown, IncludeUnknown.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameBypassIgnoreOnImport, BypassIgnoreOnImport.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameOverwriteNames, OverwriteOnDownload.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameIncludeShortcuts, IncludeShortcuts.ToString().ToLowerInvariant());

            writer.WriteStartElement(XmlNameGameList);

            foreach (GameInfo gameInfo in GameData.Games.Values)
            {
                if (!IncludeShortcuts && gameInfo.Id <= 0)
                {
                    continue;
                }

                // Don't save shortcuts if we aren't including them
                writer.WriteStartElement(XmlNameGame);

                writer.WriteElementString(XmlNameGameId, gameInfo.Id.ToString(CultureInfo.InvariantCulture));
                writer.WriteElementString(XmlNameGameSource, gameInfo.Source.ToString());

                if (gameInfo.Name != null)
                {
                    writer.WriteElementString(XmlNameGameName, gameInfo.Name);
                }

                writer.WriteElementString(XmlNameGameHidden, gameInfo.Hidden.ToString().ToLowerInvariant());

                if (gameInfo.LastPlayed != 0)
                {
                    writer.WriteElementString(XmlNameGameLastPlayed, gameInfo.LastPlayed.ToString(CultureInfo.InvariantCulture));
                }

                if (!gameInfo.Executable.Contains("steam://"))
                {
                    writer.WriteElementString(XmlNameGameExecutable, gameInfo.Executable);
                }

                writer.WriteStartElement(XmlNameGameCategoryList);
                foreach (Category category in gameInfo.Categories)
                {
                    string categoryName = category.Name;
                    if (category.Name == GameList.FavoriteNewConfigValue)
                    {
                        categoryName = GameList.FavoriteConfigValue;
                    }

                    writer.WriteElementString(XmlNameGameCategory, categoryName);
                }

                writer.WriteEndElement(); // categories

                writer.WriteEndElement(); // game
            }

            writer.WriteEndElement(); // games

            writer.WriteStartElement(XmlNameFilterList);

            foreach (Filter filter in GameData.Filters)
            {
                filter.WriteToXml(writer);
            }

            writer.WriteEndElement(); //game filters

            writer.WriteStartElement(XmlNameAutoCatList);

            foreach (AutoCat autoCat in AutoCats)
            {
                autoCat.WriteToXml(writer);
            }

            writer.WriteEndElement(); //autocats

            writer.WriteStartElement(XmlNameExclusionList);

            foreach (int i in IgnoreList)
            {
                writer.WriteElementString(XmlNameExclusion, i.ToString(CultureInfo.InvariantCulture));
            }

            writer.WriteEndElement(); // exclusions

            writer.WriteEndElement(); // profile

            writer.Close();
            FilePath = path;
            Logger.Info(GlobalStrings.Profile_ProfileSaveComplete);
        }

        #endregion

        #region Methods

        private static void AddFilterFromNode(XmlNode node, Profile profile)
        {
            if (!XmlUtil.TryGetStringFromNode(node[XmlNameFilterName], out string name))
            {
                return;
            }

            Filter filter = profile.GameData.AddFilter(name);
            if (!XmlUtil.TryGetIntFromNode(node[XmlNameFilterUncategorized], out filter.Uncategorized))
            {
                filter.Uncategorized = -1;
            }

            if (!XmlUtil.TryGetIntFromNode(node[XmlNameFilterHidden], out filter.Hidden))
            {
                filter.Hidden = -1;
            }

            if (!XmlUtil.TryGetIntFromNode(node[XmlNameFilterVR], out filter.VR))
            {
                filter.VR = -1;
            }

            XmlNodeList filterNodes = node.SelectNodes(XmlNameFilterAllow);
            if (filterNodes != null)
            {
                foreach (XmlNode fNode in filterNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(fNode, out string catName))
                    {
                        filter.Allow.Add(profile.GameData.GetCategory(catName));
                    }
                }
            }

            filterNodes = node.SelectNodes(XmlNameFilterRequire);
            if (filterNodes != null)
            {
                foreach (XmlNode fNode in filterNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(fNode, out string catName))
                    {
                        filter.Require.Add(profile.GameData.GetCategory(catName));
                    }
                }
            }

            filterNodes = node.SelectNodes(XmlNameFilterExclude);
            if (filterNodes != null)
            {
                foreach (XmlNode fNode in filterNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(fNode, out string catName))
                    {
                        filter.Exclude.Add(profile.GameData.GetCategory(catName));
                    }
                }
            }
        }

        private static void AddGameFromNode(XmlNode node, Profile profile)
        {
            if (!XmlUtil.TryGetIntFromNode(node[XmlNameGameId], out int id))
            {
                return;
            }

            GameListingSource source = XmlUtil.GetEnumFromNode(node[XmlNameGameSource], GameListingSource.Unknown);

            if (source < GameListingSource.Manual && profile.IgnoreList.Contains(id))
            {
                return;
            }

            string name = XmlUtil.GetStringFromNode(node[XmlNameGameName], null);
            GameInfo game = new GameInfo(id, name, profile.GameData)
            {
                Source = source
            };
            profile.GameData.Games.Add(id, game);

            game.Hidden = XmlUtil.GetBoolFromNode(node[XmlNameGameHidden], false);
            game.Executable = XmlUtil.GetStringFromNode(node[XmlNameGameExecutable], null);
            game.LastPlayed = XmlUtil.GetIntFromNode(node[XmlNameGameLastPlayed], 0);

            XmlNode catListNode = node.SelectSingleNode(XmlNameGameCategoryList);
            XmlNodeList catNodes = catListNode?.SelectNodes(XmlNameGameCategory);
            if (catNodes == null)
            {
                return;
            }

            foreach (XmlNode cNode in catNodes)
            {
                if (XmlUtil.TryGetStringFromNode(cNode, out string cat))
                {
                    game.AddCategory(profile.GameData.GetCategory(cat));
                }
            }
        }

        #endregion
    }
}
