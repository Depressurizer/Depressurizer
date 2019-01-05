using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.Helpers;
using Depressurizer.Models;

namespace Depressurizer
{
    /// <summary>
    ///     Autocategorization scheme that adds developer and publisher categories.
    /// </summary>
    public class AutoCatDevPub : AutoCat
    {
        #region Constants

        // Serialization keys
        public const string TypeIdString = "AutoCatDevPub";

        private const string XmlName_Name = "Name", XmlName_Filter = "Filter", XmlName_AllDevelopers = "AllDevelopers", XmlName_AllPublishers = "AllPublishers", XmlName_Prefix = "Prefix", XmlName_OwnedOnly = "OwnedOnly", XmlName_MinCount = "MinCount", XmlName_Developers = "Developers", XmlName_Developer = "Developer", XmlName_Publishers = "Publishers", XmlName_Publisher = "Publisher";

        #endregion

        #region Fields

        private Dictionary<string, int> devList;

        private GameList gamelist;

        private Dictionary<string, int> pubList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Creates a new AutoCatManual object, which removes selected (or all) categories from one list and then, optionally,
        ///     assigns categories from another list.
        /// </summary>
        public AutoCatDevPub(string name, string filter = null, string prefix = null, bool owned = true, int count = 0, bool developersAll = false, bool publishersAll = false, List<string> developers = null, List<string> publishers = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            OwnedOnly = owned;
            MinCount = count;
            AllDevelopers = developersAll;
            AllPublishers = publishersAll;
            Developers = developers == null ? new List<string>() : developers;
            Publishers = publishers == null ? new List<string>() : publishers;
            Selected = selected;
        }

        protected AutoCatDevPub(AutoCatDevPub other) : base(other)
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

        //XmlSerializer requires a parameterless constructor
        private AutoCatDevPub() { }

        #endregion

        #region Public Properties

        // Autocat configuration
        public bool AllDevelopers { get; set; }

        public bool AllPublishers { get; set; }

        public override AutoCatType AutoCatType => AutoCatType.DevPub;

        [XmlArrayItem("Developer")]
        public List<string> Developers { get; set; }

        public int MinCount { get; set; }

        public bool OwnedOnly { get; set; }

        public string Prefix { get; set; }

        [XmlArrayItem("Publisher")]
        public List<string> Publishers { get; set; }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatDevPub LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            bool AllDevelopers = XmlUtil.GetBoolFromNode(xElement[XmlName_AllDevelopers], false);
            bool AllPublishers = XmlUtil.GetBoolFromNode(xElement[XmlName_AllPublishers], false);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlName_Prefix], null);
            bool owned = XmlUtil.GetBoolFromNode(xElement[XmlName_OwnedOnly], false);
            int count = XmlUtil.GetIntFromNode(xElement[XmlName_MinCount], 0);

            List<string> devs = new List<string>();

            XmlElement devsListElement = xElement[XmlName_Developers];
            if (devsListElement != null)
            {
                XmlNodeList devNodes = devsListElement.SelectNodes(XmlName_Developer);
                foreach (XmlNode node in devNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(node, out string s))
                    {
                        devs.Add(s);
                    }
                }
            }

            List<string> pubs = new List<string>();

            XmlElement pubsListElement = xElement[XmlName_Publishers];
            if (pubsListElement != null)
            {
                XmlNodeList pubNodes = pubsListElement.SelectNodes(XmlName_Publisher);
                foreach (XmlNode node in pubNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(node, out string s))
                    {
                        pubs.Add(s);
                    }
                }
            }

            AutoCatDevPub result = new AutoCatDevPub(name, filter, prefix, owned, count, AllDevelopers, AllPublishers, devs, pubs);
            return result;
        }

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (db == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_DBNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }

            if (game == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!db.Contains(game.Id, out DatabaseEntry entry) || entry.LastStoreScrape == 0)
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            Collection<string> developers = db.GetDevelopers(game.Id);
            foreach (string developer in developers)
            {
                if (!Developers.Contains(developer) && !AllDevelopers)
                {
                    continue;
                }

                if (DevCount(developer) >= MinCount)
                {
                    game.AddCategory(games.GetCategory(GetProcessedString(developer)));
                }
            }

            Collection<string> publishers = db.GetPublishers(game.Id);
            foreach (string publisher in publishers)
            {
                if (!Publishers.Contains(publisher) && !AllPublishers)
                {
                    continue;
                }

                if (PubCount(publisher) >= MinCount)
                {
                    game.AddCategory(games.GetCategory(GetProcessedString(publisher)));
                }
            }

            return AutoCatResult.Success;
        }

        public override AutoCat Clone()
        {
            return new AutoCatDevPub(this);
        }

        public override void DeProcess()
        {
            base.DeProcess();
            gamelist = null;
        }

        /// <summary>
        ///     Prepares to categorize games. Prepares a list of genre categories to remove. Does nothing if removeothergenres is
        ///     false.
        /// </summary>
        public override void PreProcess(GameList games, Database db)
        {
            base.PreProcess(games, db);
            gamelist = games;
            devList = Database.CalculateSortedDevList(OwnedOnly ? gamelist : null, MinCount);
            pubList = Database.CalculateSortedPubList(OwnedOnly ? gamelist : null, MinCount);
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlName_Name, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlName_Filter, Filter);
            }

            if (Prefix != null)
            {
                writer.WriteElementString(XmlName_Prefix, Prefix);
            }

            writer.WriteElementString(XmlName_OwnedOnly, OwnedOnly.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlName_MinCount, MinCount.ToString());
            writer.WriteElementString(XmlName_AllDevelopers, AllDevelopers.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlName_AllPublishers, AllPublishers.ToString().ToLowerInvariant());

            if (Developers.Count > 0)
            {
                writer.WriteStartElement(XmlName_Developers);
                foreach (string s in Developers)
                {
                    writer.WriteElementString(XmlName_Developer, s);
                }

                writer.WriteEndElement();
            }

            if (Publishers.Count > 0)
            {
                writer.WriteStartElement(XmlName_Publishers);
                foreach (string s in Publishers)
                {
                    writer.WriteElementString(XmlName_Publisher, s);
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        #endregion

        #region Methods

        private int DevCount(string name)
        {
            return devList.Where(dev => dev.Key == name).Select(dev => dev.Value).FirstOrDefault();
        }

        private string GetProcessedString(string baseString)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                return baseString;
            }

            return Prefix + baseString;
        }

        private int PubCount(string name)
        {
            return pubList.Where(pub => pub.Key == name).Select(pub => pub.Value).FirstOrDefault();
        }

        #endregion
    }
}
