﻿#region LICENSE

//     This file (AutoCatLanguage.cs) is part of Depressurizer.
//     Original Copyright (C) 2017  Theodoros Dimos
//     Modified Copyright (C) 2018  Martijn Vegter
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
using System.Linq;
using System.Xml;
using DepressurizerCore.Models;

namespace Depressurizer
{
    public class AutoCatLanguage : AutoCat
    {
        private LanguageSupport _includedLanguages;

        #region Constants

        // Serialization constants
        public const string TypeIdString = "AutoCatLanguage";

        private const string XmlNameFilter = "Filter";

        private const string XmlNameFullAudioList = "FullAudio";

        private const string XmlNameIncludeTypePrefix = "IncludeTypePrefix";

        private const string XmlNameInterfaceList = "Interface";

        private const string XmlNameLanguage = "Langauge";

        private const string XmlNameName = "Name";

        private const string XmlNamePrefix = "Prefix";

        private const string XmlNameSubtitlesList = "Subtitles";

        private const string XmlNameTypeFallback = "TypeFallback";

        #endregion

        #region Fields

        public LanguageSupport IncludedLanguages
        {
            get { return _includedLanguages ?? (_includedLanguages = new LanguageSupport()); }
            set { _includedLanguages = value; }
        }

        #endregion

        #region Constructors and Destructors

        public AutoCatLanguage(string name, string filter = null, string prefix = null, bool includeTypePrefix = false, bool typeFallback = false, List<string> interfaceLanguage = null, List<string> subtitles = null, List<string> fullAudio = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            IncludeTypePrefix = includeTypePrefix;
            TypeFallback = typeFallback;

            IncludedLanguages.Interface = interfaceLanguage ?? new List<string>();
            IncludedLanguages.Subtitles = subtitles ?? new List<string>();
            IncludedLanguages.FullAudio = fullAudio ?? new List<string>();
            Selected = selected;
        }

        protected AutoCatLanguage(AutoCatLanguage other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludeTypePrefix = other.IncludeTypePrefix;
            TypeFallback = other.TypeFallback;
            IncludedLanguages = other.IncludedLanguages;
            Selected = other.Selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatLanguage()
        {
        }

        #endregion

        #region Public Properties

        public override AutoCatType AutoCatType => AutoCatType.Language;

        public bool IncludeTypePrefix { get; set; }

        // AutoCat configuration
        public string Prefix { get; set; }

        public bool TypeFallback { get; set; }

        #endregion

        #region Public Methods and Operators

        public static AutoCatLanguage LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlNamePrefix], null);
            bool includeTypePrefix = XmlUtil.GetBoolFromNode(xElement[XmlNameIncludeTypePrefix], false);
            bool typeFallback = XmlUtil.GetBoolFromNode(xElement[XmlNameTypeFallback], false);
            List<string> interfaceList = new List<string>();
            List<string> subtitlesList = new List<string>();
            List<string> fullAudioList = new List<string>();

            XmlElement interfaceLanguage = xElement[XmlNameInterfaceList];
            XmlElement subtitles = xElement[XmlNameSubtitlesList];
            XmlElement fullAudio = xElement[XmlNameFullAudioList];

            XmlNodeList interfaceElements = interfaceLanguage?.SelectNodes(XmlNameLanguage);
            if (interfaceElements != null)
            {
                for (int i = 0; i < interfaceElements.Count; i++)
                {
                    XmlNode n = interfaceElements[i];
                    if (XmlUtil.TryGetStringFromNode(n, out string language))
                    {
                        interfaceList.Add(language);
                    }
                }
            }

            XmlNodeList subtitlesElements = subtitles?.SelectNodes(XmlNameLanguage);
            if (subtitlesElements != null)
            {
                for (int i = 0; i < subtitlesElements.Count; i++)
                {
                    XmlNode n = subtitlesElements[i];
                    if (XmlUtil.TryGetStringFromNode(n, out string language))
                    {
                        subtitlesList.Add(language);
                    }
                }
            }

            XmlNodeList fullAudioElements = fullAudio?.SelectNodes(XmlNameLanguage);
            if (fullAudioElements != null)
            {
                for (int i = 0; i < fullAudioElements.Count; i++)
                {
                    XmlNode n = fullAudioElements[i];
                    if (XmlUtil.TryGetStringFromNode(n, out string language))
                    {
                        fullAudioList.Add(language);
                    }
                }
            }

            return new AutoCatLanguage(name, filter, prefix, includeTypePrefix, typeFallback, interfaceList, subtitlesList, fullAudioList);
        }

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (db == null)
            {
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }

            if (game == null)
            {
                return AutoCatResult.Failure;
            }

            if (!db.Contains(game.Id) || db.Games[game.Id].LastStoreScrape == 0)
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            LanguageSupport Language = db.Games[game.Id].LanguageSupport;

            Language.Interface = Language.Interface ?? new List<string>();
            Language.Subtitles = Language.Subtitles ?? new List<string>();
            Language.FullAudio = Language.FullAudio ?? new List<string>();

            IEnumerable<string> interfaceLanguage = Language.Interface.Intersect(IncludedLanguages.Interface);
            foreach (string catString in interfaceLanguage)
            {
                Category c = games.GetCategory(GetProcessedString(catString, "Interface"));
                game.AddCategory(c);
            }

            foreach (string catString in IncludedLanguages.Subtitles)
            {
                if (Language.Subtitles.Contains(catString) || Language.Subtitles.Count == 0 && Language.FullAudio.Contains(catString) || Language.FullAudio.Count == 0 && Language.Interface.Contains(catString))
                {
                    game.AddCategory(games.GetCategory(GetProcessedString(catString, "Subtitles")));
                }
            }

            foreach (string catString in IncludedLanguages.FullAudio)
            {
                if (Language.FullAudio.Contains(catString) || Language.FullAudio.Count == 0 && Language.Subtitles.Contains(catString) || Language.Subtitles.Count == 0 && Language.Interface.Contains(catString))
                {
                    game.AddCategory(games.GetCategory(GetProcessedString(catString, "Full Audio")));
                }
            }

            return AutoCatResult.Success;
        }

        public override AutoCat Clone()
        {
            return new AutoCatLanguage(this);
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlNameName, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlNameFilter, Filter);
            }

            if (Prefix != null)
            {
                writer.WriteElementString(XmlNamePrefix, Prefix);
            }

            writer.WriteElementString(XmlNameIncludeTypePrefix, IncludeTypePrefix.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameTypeFallback, TypeFallback.ToString().ToLowerInvariant());

            writer.WriteStartElement(XmlNameInterfaceList);

            foreach (string s in IncludedLanguages.Interface)
            {
                writer.WriteElementString(XmlNameLanguage, s);
            }

            writer.WriteEndElement(); // Interface Language list

            writer.WriteStartElement(XmlNameSubtitlesList);

            foreach (string s in IncludedLanguages.Subtitles)
            {
                writer.WriteElementString(XmlNameLanguage, s);
            }

            writer.WriteEndElement(); // Subtitles Language list

            writer.WriteStartElement(XmlNameFullAudioList);

            foreach (string s in IncludedLanguages.FullAudio)
            {
                writer.WriteElementString(XmlNameLanguage, s);
            }

            writer.WriteEndElement(); // Full Audio list
            writer.WriteEndElement(); // type ID string
        }

        #endregion

        #region Methods

        private string GetProcessedString(string baseString, string type = "")
        {
            string result = baseString;

            if (IncludeTypePrefix && !string.IsNullOrEmpty(type))
            {
                result = "(" + type + ") " + result;
            }

            if (!string.IsNullOrEmpty(Prefix))
            {
                result = Prefix + result;
            }

            return result;
        }

        #endregion
    }
}