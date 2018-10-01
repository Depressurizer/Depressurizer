/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Theodoros Dimos.

    Depressurizer is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Depressurizer is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Rallion;

namespace Depressurizer
{
    public class AutoCatLanguage : AutoCat
    {
        // Serialization constants
        public const string TypeIdString = "AutoCatLanguage";

        private const string XmlNameName = "Name";
        private const string XmlNameFilter = "Filter";
        private const string XmlNamePrefix = "Prefix";
        private const string XmlNameIncludeTypePrefix = "IncludeTypePrefix";
        private const string XmlNameTypeFallback = "TypeFallback";
        private const string XmlNameInterfaceList = "Interface";
        private const string XmlNameSubtitlesList = "Subtitles";
        private const string XmlNameFullAudioList = "FullAudio";
        private const string XmlNameLanguage = "Langauge";

        public LanguageSupport IncludedLanguages;

        public AutoCatLanguage(string name, string filter = null, string prefix = null, bool includeTypePrefix = false,
            bool typeFallback = false, List<string> interfaceLanguage = null,
            List<string> subtitles = null, List<string> fullAudio = null, bool selected = false) : base(name)
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

        //XmlSerializer requires a parameterless constructor
        private AutoCatLanguage()
        {
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

        public override AutoCatType AutoCatType => AutoCatType.Language;

        // AutoCat configuration
        public string Prefix { get; set; }

        public bool IncludeTypePrefix { get; set; }

        public bool TypeFallback { get; set; }

        public override AutoCat Clone()
        {
            return new AutoCatLanguage(this);
        }

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (db == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_DBNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }

            if (game == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!db.Contains(game.Id) || db.Games[game.Id].LastStoreScrape == 0) return AutoCatResult.NotInDatabase;

            if (!game.IncludeGame(filter)) return AutoCatResult.Filtered;

            var Language = db.Games[game.Id].LanguageSupport;

            Language.Interface = Language.Interface ?? new List<string>();
            Language.Subtitles = Language.Subtitles ?? new List<string>();
            Language.FullAudio = Language.FullAudio ?? new List<string>();

            var interfaceLanguage = Language.Interface.Intersect(IncludedLanguages.Interface);
            foreach (var catString in interfaceLanguage)
            {
                var c = games.GetCategory(GetProcessedString(catString, "Interface"));
                game.AddCategory(c);
            }

            foreach (var catString in IncludedLanguages.Subtitles)
                if (Language.Subtitles.Contains(catString) ||
                    Language.Subtitles.Count == 0 && Language.FullAudio.Contains(catString) ||
                    Language.FullAudio.Count == 0 && Language.Interface.Contains(catString))
                    game.AddCategory(games.GetCategory(GetProcessedString(catString, "Subtitles")));

            foreach (var catString in IncludedLanguages.FullAudio)
                if (Language.FullAudio.Contains(catString) ||
                    Language.FullAudio.Count == 0 && Language.Subtitles.Contains(catString) ||
                    Language.Subtitles.Count == 0 && Language.Interface.Contains(catString))
                    game.AddCategory(games.GetCategory(GetProcessedString(catString, "Full Audio")));

            return AutoCatResult.Success;
        }

        private string GetProcessedString(string baseString, string type = "")
        {
            var result = baseString;

            if (IncludeTypePrefix && !string.IsNullOrEmpty(type)) result = "(" + type + ") " + result;

            if (!string.IsNullOrEmpty(Prefix)) result = Prefix + result;

            return result;
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlNameName, Name);
            if (Filter != null) writer.WriteElementString(XmlNameFilter, Filter);
            if (Prefix != null) writer.WriteElementString(XmlNamePrefix, Prefix);

            writer.WriteElementString(XmlNameIncludeTypePrefix, IncludeTypePrefix.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlNameTypeFallback, TypeFallback.ToString().ToLowerInvariant());

            writer.WriteStartElement(XmlNameInterfaceList);

            foreach (var s in IncludedLanguages.Interface) writer.WriteElementString(XmlNameLanguage, s);

            writer.WriteEndElement(); // Interface Language list

            writer.WriteStartElement(XmlNameSubtitlesList);

            foreach (var s in IncludedLanguages.Subtitles) writer.WriteElementString(XmlNameLanguage, s);

            writer.WriteEndElement(); // Subtitles Language list

            writer.WriteStartElement(XmlNameFullAudioList);

            foreach (var s in IncludedLanguages.FullAudio) writer.WriteElementString(XmlNameLanguage, s);

            writer.WriteEndElement(); // Full Audio list
            writer.WriteEndElement(); // type ID string
        }

        public static AutoCatLanguage LoadFromXmlElement(XmlElement xElement)
        {
            var name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            var filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            var prefix = XmlUtil.GetStringFromNode(xElement[XmlNamePrefix], null);
            var includeTypePrefix = XmlUtil.GetBoolFromNode(xElement[XmlNameIncludeTypePrefix], false);
            var typeFallback = XmlUtil.GetBoolFromNode(xElement[XmlNameTypeFallback], false);
            var interfaceList = new List<string>();
            var subtitlesList = new List<string>();
            var fullAudioList = new List<string>();

            var interfaceLanguage = xElement[XmlNameInterfaceList];
            var subtitles = xElement[XmlNameSubtitlesList];
            var fullAudio = xElement[XmlNameFullAudioList];

            var interfaceElements = interfaceLanguage?.SelectNodes(XmlNameLanguage);
            if (interfaceElements != null)
                for (var i = 0; i < interfaceElements.Count; i++)
                {
                    var n = interfaceElements[i];
                    if (XmlUtil.TryGetStringFromNode(n, out var language)) interfaceList.Add(language);
                }

            var subtitlesElements = subtitles?.SelectNodes(XmlNameLanguage);
            if (subtitlesElements != null)
                for (var i = 0; i < subtitlesElements.Count; i++)
                {
                    var n = subtitlesElements[i];
                    if (XmlUtil.TryGetStringFromNode(n, out var language)) subtitlesList.Add(language);
                }

            var fullAudioElements = fullAudio?.SelectNodes(XmlNameLanguage);
            if (fullAudioElements != null)
                for (var i = 0; i < fullAudioElements.Count; i++)
                {
                    var n = fullAudioElements[i];
                    if (XmlUtil.TryGetStringFromNode(n, out var language)) fullAudioList.Add(language);
                }

            return new AutoCatLanguage(name, filter, prefix, includeTypePrefix, typeFallback, interfaceList,
                subtitlesList, fullAudioList);
        }
    }
}