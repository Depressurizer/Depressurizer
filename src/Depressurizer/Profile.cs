using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;
using Depressurizer.AutoCats;
using Depressurizer.Core;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using Depressurizer.Core.Models;
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

        private const string XmlNameFilterList = "Filters";

        private const string XmlNameGame = "game";

        private const string XmlNameGameList = "games";

        private const string XmlNameIncludeShortcuts = "include_shortcuts";

        private const string XmlNameIncludeUnknown = "include_unknown";

        private const string XmlNameLocalUpdate = "local_update";

        private const string XmlNameOldSteamIdShort = "account_id";

        private const string XmlNameOverwriteNames = "overwrite_names";

        private const string XmlNameProfile = "profile";

        private const string XmlNameSteamId = "steam_id_64";

        private const string XmlNameWebKey = "web_key";

        private const string XmlNameWebUpdate = "web_update";

        #endregion

        #region Fields

        private SortedSet<int> _ignoreList = new SortedSet<int>();

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

        public string SteamWebApiKey {get;set;}

        public GameList GameData { get; } = new GameList();

        public SortedSet<int> IgnoreList
        {
            get => _ignoreList ?? (_ignoreList = new SortedSet<int>());
            set => _ignoreList = value;
        }

        public bool IncludeShortcuts { get; set; } = true;

        public bool IncludeUnknown { get; set; }

        public bool LocalUpdate { get; set; } = true;

        public bool OverwriteOnDownload { get; set; }

        public long SteamID64 { get; set; }

        public bool WebUpdate { get; set; } = true;

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

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
                        accId = Steam.ToSteamId64(oldAcc);
                    }
                }

                profile.SteamID64 = accId;

                // Get other attributes
                profile.AutoUpdate = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoUpdate], profile.AutoUpdate);

                profile.AutoImport = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoImport], profile.AutoImport);
                profile.AutoExport = XmlUtil.GetBoolFromNode(profileNode[XmlNameAutoExport], profile.AutoExport);

                profile.LocalUpdate = XmlUtil.GetBoolFromNode(profileNode[XmlNameLocalUpdate], profile.LocalUpdate);
                profile.WebUpdate = XmlUtil.GetBoolFromNode(profileNode[XmlNameWebUpdate], profile.WebUpdate);
                profile.SteamWebApiKey = XmlUtil.GetStringFromNode(profileNode[XmlNameWebKey], profile.SteamWebApiKey);

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
                        GameInfo.AddFromNode(node, profile);
                    }
                }

                XmlNode filterListNode = profileNode.SelectSingleNode(XmlNameFilterList);
                XmlNodeList filterNodes = filterListNode?.SelectNodes(XmlNameFilter);
                if (filterNodes != null)
                {
                    foreach (XmlNode node in filterNodes)
                    {
                        Filter.AddFromNode(node, profile);
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
                    AutoCat.GenerateDefaultAutoCatSet(profile.AutoCats);
                }

                //profile.AutoCats.Sort();
            }

            Logger.Info(GlobalStrings.MainForm_ProfileLoaded);
            return profile;
        }

        public Filter AddFilter(string name)
        {
            return GameData.AddFilter(name);
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

        public Category GetCategory(string name)
        {
            return GameData.GetCategory(name);
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
                Locations.File.Backup(path, Settings.Instance.ConfigBackupCount);
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
            writer.WriteElementString(XmlNameWebKey, SteamWebApiKey.ToLowerInvariant());
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

                gameInfo.WriteToXml(writer);
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
    }
}
