using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Net.Cache;
using System.Xml;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;
using Depressurizer.Helpers;
using Depressurizer.Models;

namespace Depressurizer
{
    public class Profile
    {
        #region Constants

        public const int Version = 3;

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

        private const string XmlNameOldAutoDownload = "auto_download";

        private const string XmlNameOldGameFavorite = "favorite";

        private const string XmlNameOldIgnoreExternal = "ignore_external";

        private const string XmlNameOldSteamIdShort = "account_id";

        private const string XmlNameOverwriteNames = "overwrite_names";

        private const string XmlNameProfile = "profile";

        private const string XmlNameSteamId = "steam_id_64";

        private const string XmlNameVersion = "version";

        private const string XmlNameWebUpdate = "web_update";

        #endregion

        #region Fields

        public List<AutoCat> AutoCats = new List<AutoCat>();

        public bool AutoExport = true;

        public bool AutoIgnore = true;

        public bool AutoImport = true;

        public bool AutoUpdate = true;

        public bool BypassIgnoreOnImport;

        public bool ExportDiscard = true;

        public string FilePath;

        public GameList GameData = new GameList();

        public SortedSet<int> IgnoreList = new SortedSet<int>();

        public bool IncludeShortcuts = true;

        public bool IncludeUnknown;

        public bool LocalUpdate = true;

        public bool OverwriteOnDownload;

        public long SteamID64;

        public bool WebUpdate = true;

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static long DirNametoID64(string cId)
        {
            if (long.TryParse(cId, out long res))
            {
                return res + 0x0110000100000000;
            }

            return 0;
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
            foreach (Tuple<string, float> tag in Database.CalculateSortedTagList(null, 1, 20, 0, false, false))
            {
                act.IncludedTags.Add(tag.Item1);
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

        public static string ID64toDirName(long id)
        {
            return (id - 0x0110000100000000).ToString();
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
                // Get the profile version that we're loading
                XmlAttribute versionAttr = profileNode.Attributes[XmlNameVersion];
                int profileVersion = 0;
                if (versionAttr != null)
                {
                    if (!int.TryParse(versionAttr.Value, out profileVersion))
                    {
                        profileVersion = 0;
                    }
                }

                // Get the 64-bit Steam ID
                long accId = XmlUtil.GetInt64FromNode(profileNode[XmlNameSteamId], 0);
                if (accId == 0)
                {
                    string oldAcc = XmlUtil.GetStringFromNode(profileNode[XmlNameOldSteamIdShort], null);
                    if (oldAcc != null)
                    {
                        accId = DirNametoID64(oldAcc);
                    }
                }

                profile.SteamID64 = accId;

                // Get other attributes
                profile.AutoUpdate = XmlUtil.GetBoolFromNode(profileVersion < 3 ? profileNode[XmlNameOldAutoDownload] : profileNode[XmlNameAutoUpdate], profile.AutoUpdate);

                profile.AutoImport = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoImport], profile.AutoImport);
                profile.AutoExport = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoExport], profile.AutoExport);

                profile.LocalUpdate = XmlUtil.GetBoolFromNode(profileNode[XmlNameLocalUpdate], profile.LocalUpdate);
                profile.WebUpdate = XmlUtil.GetBoolFromNode(profileNode[XmlNameWebUpdate], profile.WebUpdate);

                profile.IncludeUnknown = XmlUtil.GetBoolFromNode(profileNode[XmlNameIncludeUnknown], profile.IncludeUnknown);
                profile.BypassIgnoreOnImport = XmlUtil.GetBoolFromNode(profileNode[XmlNameBypassIgnoreOnImport], profile.BypassIgnoreOnImport);

                profile.ExportDiscard = XmlUtil.GetBoolFromNode(profileNode[XmlNameExportDiscard], profile.ExportDiscard);
                profile.AutoIgnore = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoIgnore], profile.AutoIgnore);
                profile.OverwriteOnDownload = XmlUtil.GetBoolFromNode(profileNode[XmlNameOverwriteNames], profile.OverwriteOnDownload);

                if (profileVersion < 2)
                {
                    if (XmlUtil.TryGetBoolFromNode(profileNode[XmlNameOldIgnoreExternal], out bool ignoreShortcuts))
                    {
                        profile.IncludeShortcuts = !ignoreShortcuts;
                    }
                }
                else
                {
                    profile.IncludeShortcuts = XmlUtil.GetBoolFromNode(profileNode[XmlNameIncludeShortcuts], profile.IncludeShortcuts);
                }

                XmlNode exclusionListNode = profileNode.SelectSingleNode(XmlNameExclusionList);
                if (exclusionListNode != null)
                {
                    XmlNodeList exclusionNodes = exclusionListNode.SelectNodes(XmlNameExclusion);
                    foreach (XmlNode node in exclusionNodes)
                    {
                        if (XmlUtil.TryGetIntFromNode(node, out int id))
                        {
                            profile.IgnoreList.Add(id);
                        }
                    }
                }

                XmlNode gameListNode = profileNode.SelectSingleNode(XmlNameGameList);
                if (gameListNode != null)
                {
                    XmlNodeList gameNodes = gameListNode.SelectNodes(XmlNameGame);
                    foreach (XmlNode node in gameNodes)
                    {
                        AddGameFromXmlNode(node, profile, profileVersion);
                    }
                }

                XmlNode filterListNode = profileNode.SelectSingleNode(XmlNameFilterList);
                if (filterListNode != null)
                {
                    XmlNodeList filterNodes = filterListNode.SelectNodes(XmlNameFilter);
                    foreach (XmlNode node in filterNodes)
                    {
                        AddFilterFromXmlNode(node, profile);
                    }
                }

                XmlNode autocatListNode = profileNode.SelectSingleNode(XmlNameAutoCatList);
                if (autocatListNode != null)
                {
                    XmlNodeList autoCatNodes = autocatListNode.ChildNodes;
                    foreach (XmlNode node in autoCatNodes)
                    {
                        XmlElement autocatElement = node as XmlElement;
                        if (node != null)
                        {
                            AutoCat autocat = AutoCat.LoadACFromXmlElement(autocatElement);
                            if (autocat != null)
                            {
                                profile.AutoCats.Add(autocat);
                            }
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

        // using a list of AutoCat names (strings), return a cloned list of AutoCats replacing the filter with a new one if a new filter is provided.
        // This is used to help process AutoCatGroup.
        public List<AutoCat> CloneAutoCatList(List<string> acList, Filter filter)
        {
            List<AutoCat> newList = new List<AutoCat>();
            foreach (string s in acList)
            {
                // find the AutoCat based on name
                AutoCat ac = GetAutoCat(s);
                if (ac != null)
                {
                    // add a cloned copy of the Autocat and replace the filter if one is provided.
                    // a cloned copy is used so that the selected property can be assigned without effecting lvAutoCatType on the Main form.
                    AutoCat clone = ac.Clone();
                    if (filter != null)
                    {
                        clone.Filter = filter.Name;
                    }

                    newList.Add(clone);
                }
            }

            return newList;
        }

        public void ExportSteamData()
        {
            GameData.ExportSteamConfig(SteamID64, ExportDiscard, IncludeShortcuts);
        }

        // find and return AutoCat using the name
        public AutoCat GetAutoCat(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            foreach (AutoCat ac in AutoCats)
            {
                if (string.Equals(ac.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return ac;
                }
            }

            return null;
        }

        public Image GetAvatar()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                string profile = string.Format(CultureInfo.InvariantCulture, Constants.UrlSteamProfile, SteamID64);
                xml.Load(profile);

                XmlNodeList xnList = xml.SelectNodes("/profile/avatarIcon");
                foreach (XmlNode xn in xnList)
                {
                    string avatarURL = xn.InnerText;
                    return Utility.GetImage(avatarURL, RequestCacheLevel.BypassCache);
                }
            }
            catch { }

            return null;
        }

        public int ImportSteamData()
        {
            return GameData.ImportSteamConfig(SteamID64, IgnoreList, IncludeShortcuts);
        }

        public void Save()
        {
            Save(FilePath);
        }

        public bool Save(string path)
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

            writer.WriteAttributeString(XmlNameVersion, Version.ToString());

            writer.WriteElementString(XmlNameSteamId, SteamID64.ToString());

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

            foreach (GameInfo g in GameData.Games.Values)
            {
                if (IncludeShortcuts || g.Id > 0)
                {
                    // Don't save shortcuts if we aren't including them
                    writer.WriteStartElement(XmlNameGame);

                    writer.WriteElementString(XmlNameGameId, g.Id.ToString());
                    writer.WriteElementString(XmlNameGameSource, g.Source.ToString());

                    if (g.Name != null)
                    {
                        writer.WriteElementString(XmlNameGameName, g.Name);
                    }

                    writer.WriteElementString(XmlNameGameHidden, g.Hidden.ToString().ToLowerInvariant());

                    if (g.LastPlayed != 0)
                    {
                        writer.WriteElementString(XmlNameGameLastPlayed, g.LastPlayed.ToString());
                    }

                    if (!g.Executable.Contains("steam://"))
                    {
                        writer.WriteElementString(XmlNameGameExecutable, g.Executable);
                    }

                    writer.WriteStartElement(XmlNameGameCategoryList);
                    foreach (Category c in g.Categories)
                    {
                        string catName = c.Name;
                        if (c.Name == GameList.FAVORITE_NEW_CONFIG_VALUE)
                        {
                            catName = GameList.FAVORITE_CONFIG_VALUE;
                        }

                        writer.WriteElementString(XmlNameGameCategory, catName);
                    }

                    writer.WriteEndElement(); // categories

                    writer.WriteEndElement(); // game
                }
            }

            writer.WriteEndElement(); // games

            writer.WriteStartElement(XmlNameFilterList);

            foreach (Filter f in GameData.Filters)
            {
                f.WriteToXml(writer);
            }

            writer.WriteEndElement(); //game filters

            writer.WriteStartElement(XmlNameAutoCatList);

            foreach (AutoCat autocat in AutoCats)
            {
                autocat.WriteToXml(writer);
            }

            writer.WriteEndElement(); //autocats

            writer.WriteStartElement(XmlNameExclusionList);

            foreach (int i in IgnoreList)
            {
                writer.WriteElementString(XmlNameExclusion, i.ToString());
            }

            writer.WriteEndElement(); // exclusions

            writer.WriteEndElement(); // profile

            writer.Close();
            FilePath = path;
            Logger.Info(GlobalStrings.Profile_ProfileSaveComplete);
            return true;
        }

        #endregion

        #region Methods

        private static void AddFilterFromXmlNode(XmlNode node, Profile profile)
        {
            if (XmlUtil.TryGetStringFromNode(node[XmlNameFilterName], out string name))
            {
                Filter f = profile.GameData.AddFilter(name);
                if (!XmlUtil.TryGetIntFromNode(node[XmlNameFilterUncategorized], out f.Uncategorized))
                {
                    f.Uncategorized = -1;
                }

                if (!XmlUtil.TryGetIntFromNode(node[XmlNameFilterHidden], out f.Hidden))
                {
                    f.Hidden = -1;
                }

                if (!XmlUtil.TryGetIntFromNode(node[XmlNameFilterVR], out f.VR))
                {
                    f.VR = -1;
                }

                XmlNodeList filterNodes = node.SelectNodes(XmlNameFilterAllow);
                foreach (XmlNode fNode in filterNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(fNode, out string catName))
                    {
                        f.Allow.Add(profile.GameData.GetCategory(catName));
                    }
                }

                filterNodes = node.SelectNodes(XmlNameFilterRequire);
                foreach (XmlNode fNode in filterNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(fNode, out string catName))
                    {
                        f.Require.Add(profile.GameData.GetCategory(catName));
                    }
                }

                filterNodes = node.SelectNodes(XmlNameFilterExclude);
                foreach (XmlNode fNode in filterNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(fNode, out string catName))
                    {
                        f.Exclude.Add(profile.GameData.GetCategory(catName));
                    }
                }
            }
        }

        private static void AddGameFromXmlNode(XmlNode node, Profile profile, int profileVersion)
        {
            if (XmlUtil.TryGetIntFromNode(node[XmlNameGameId], out int id))
            {
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

                if (profileVersion < 1)
                {
                    if (XmlUtil.TryGetStringFromNode(node[XmlNameGameCategory], out string catName))
                    {
                        game.AddCategory(profile.GameData.GetCategory(catName));
                    }

                    if (node.SelectSingleNode(XmlNameOldGameFavorite) != null)
                    {
                        game.SetFavorite(true);
                    }
                }
                else
                {
                    XmlNode catListNode = node.SelectSingleNode(XmlNameGameCategoryList);
                    if (catListNode != null)
                    {
                        XmlNodeList catNodes = catListNode.SelectNodes(XmlNameGameCategory);
                        foreach (XmlNode cNode in catNodes)
                        {
                            if (XmlUtil.TryGetStringFromNode(cNode, out string cat))
                            {
                                game.AddCategory(profile.GameData.GetCategory(cat));
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
