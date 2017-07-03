/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

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

using Rallion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Depressurizer
{

    /// <summary>
    /// Autocategorization scheme that adds developer and publisher categories.
    /// </summary>
    public class AutoCatDevPub : AutoCat
    {

        public override AutoCatType AutoCatType => AutoCatType.DevPub;

        // Autocat configuration
        public bool AllDevelopers { get; set; }
        public bool AllPublishers { get; set; }
        public string Prefix { get; set; }
        public bool OwnedOnly { get; set; }
        public int MinCount { get; set; }
        public List<string> Developers { get; set; }
        public List<string> Publishers { get; set; }

        private IEnumerable<Tuple<string, int>> _devList;
        private IEnumerable<Tuple<string, int>> _pubList;

        // Serialization keys
        public const string TypeIdString = "AutoCatDevPub";
        private const string
            XmlNameName = "Name",
            XmlNameFilter = "Filter",
            XmlNameAllDevelopers = "AllDevelopers",
            XmlNameAllPublishers = "AllPublishers",
            XmlNamePrefix = "Prefix",
            XmlNameOwnedOnly = "OwnedOnly",
            XmlNameMinCount = "MinCount",
            XmlNameDevelopers = "Developers",
            XmlNameDeveloper = "Developer",
            XmlNamePublishers = "Publishers",
            XmlNamePublisher = "Publisher";

        private GameList _gamelist;

        /// <summary>
        /// Creates a new AutoCatManual object, which removes selected (or all) categories from one list and then, optionally, assigns categories from another list.
        /// </summary>
        public AutoCatDevPub(string name, string filter = null, string prefix = null, bool owned = true, int count = 0, bool developersAll = false, bool publishersAll = false, List<string> developers = null, List<string> publishers = null, bool selected = false)
            : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            OwnedOnly = owned;
            MinCount = count;
            AllDevelopers = developersAll;
            AllPublishers = publishersAll;
            Developers = developers ?? new List<string>();
            Publishers = publishers ?? new List<string>();
            Selected = selected;
        }

        protected AutoCatDevPub(AutoCatDevPub other)
            : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            OwnedOnly = other.OwnedOnly;
            MinCount = other.MinCount;
            AllDevelopers = other.AllDevelopers;
            AllPublishers = other.AllPublishers;
            Developers = new List<string>(other.Developers);
            Publishers = new List<string>(other.Publishers);
            Selected = other.Selected;
        }

        public override AutoCat Clone() => new AutoCatDevPub(this);

        /// <summary>
        /// Prepares to categorize games. Prepares a list of genre categories to remove. Does nothing if removeothergenres is false.
        /// </summary>
        public override void PreProcess(GameList games, GameDB db)
        {
            base.PreProcess(games, db);
            _gamelist = games;
            _devList = Program.GameDB.CalculateSortedDevList(OwnedOnly ? _gamelist : null, MinCount);
            _pubList = Program.GameDB.CalculateSortedPubList(OwnedOnly ? _gamelist : null, MinCount);
        }

        public override void DeProcess()
        {
            base.DeProcess();
            _gamelist = null;
        }

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (Games == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }
            if (Db == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_DBNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }
            if (game == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!Db.Contains(game.Id) || Db.Games[game.Id].LastStoreScrape == 0) return AutoCatResult.NotInDatabase;

            if (!game.IncludeGame(filter)) return AutoCatResult.Filtered;

            List<string> developers = Db.GetDevelopers(game.Id);

            if (developers != null)
            {
                foreach (string developer in developers)
                {
                    if (Developers.Contains(developer) || AllDevelopers)
                    {
                        if (DevCount(developer) >= MinCount)
                        {
                            game.AddCategory(Games.GetCategory(GetProcessedString(developer)));
                        }
                    }
                }
            }

            List<string> publishers = Db.GetPublishers(game.Id);

            if (publishers != null)
            {
                foreach (string publisher in publishers)
                {
                    if (Publishers.Contains(publisher) || AllPublishers)
                    {
                        if (PubCount(publisher) >= MinCount) game.AddCategory(Games.GetCategory(GetProcessedString(publisher)));
                    }
                }
            }

            return AutoCatResult.Success;
        }

        private int DevCount(string name) => (from dev in _devList where dev.Item1 == name select dev.Item2).FirstOrDefault();

        private int PubCount(string name) => (from pub in _pubList where pub.Item1 == name select pub.Item2).FirstOrDefault();

        private string GetProcessedString(string baseString) => string.IsNullOrEmpty(Prefix) ? baseString : Prefix + baseString;

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlNameName, Name);
            if (Filter != null) writer.WriteElementString(XmlNameFilter, Filter);
            if (Prefix != null) writer.WriteElementString(XmlNamePrefix, Prefix);
            writer.WriteElementString(XmlNameOwnedOnly, OwnedOnly.ToString());
            writer.WriteElementString(XmlNameMinCount, MinCount.ToString());
            writer.WriteElementString(XmlNameAllDevelopers, AllDevelopers.ToString());
            writer.WriteElementString(XmlNameAllPublishers, AllPublishers.ToString());

            if (Developers.Count > 0)
            {
                writer.WriteStartElement(XmlNameDevelopers);
                foreach (string s in Developers)
                {
                    writer.WriteElementString(XmlNameDeveloper, s);
                }
                writer.WriteEndElement();
            }

            if (Publishers.Count > 0)
            {
                writer.WriteStartElement(XmlNamePublishers);
                foreach (string s in Publishers)
                {
                    writer.WriteElementString(XmlNamePublisher, s);
                }
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public static AutoCatDevPub LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            bool AllDevelopers = XmlUtil.GetBoolFromNode(xElement[XmlNameAllDevelopers], false);
            bool AllPublishers = XmlUtil.GetBoolFromNode(xElement[XmlNameAllPublishers], false);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlNamePrefix], null);
            bool owned = XmlUtil.GetBoolFromNode(xElement[XmlNameOwnedOnly], false);
            int count = XmlUtil.GetIntFromNode(xElement[XmlNameMinCount], 0);

            List<string> devs = new List<string>();

            XmlElement devsListElement = xElement[XmlNameDevelopers];
            if (devsListElement != null)
            {
                XmlNodeList devNodes = devsListElement.SelectNodes(XmlNameDeveloper);
                foreach (XmlNode node in devNodes)
                {
                    string s;
                    if (XmlUtil.TryGetStringFromNode(node, out s))
                    {
                        devs.Add(s);
                    }
                }
            }

            List<string> pubs = new List<string>();

            XmlElement pubsListElement = xElement[XmlNamePublishers];
            if (pubsListElement != null)
            {
                XmlNodeList pubNodes = pubsListElement.SelectNodes(XmlNamePublisher);
                foreach (XmlNode node in pubNodes)
                {
                    string s;
                    if (XmlUtil.TryGetStringFromNode(node, out s))
                    {
                        pubs.Add(s);
                    }
                }
            }

            AutoCatDevPub result = new AutoCatDevPub(name, filter, prefix, owned, count, AllDevelopers, AllPublishers, devs, pubs);
            return result;
        }
    }

}