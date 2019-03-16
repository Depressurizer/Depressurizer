using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer.AutoCats
{
    public class AutoCatFlags : AutoCat
    {
        #region Constants

        public const string TypeIdString = "AutoCatFlags";

        private const string XmlName_Filter = "Filter";

        private const string XmlName_Flag = "Flag";

        private const string XmlName_FlagList = "Flags";

        private const string XmlName_Name = "Name";

        private const string XmlName_Prefix = "Prefix";

        #endregion

        #region Constructors and Destructors

        public AutoCatFlags(string name, string filter = null, string prefix = null, List<string> flags = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            IncludedFlags = flags ?? new List<string>();
            Selected = selected;
        }

        protected AutoCatFlags(AutoCatFlags other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludedFlags = new List<string>(other.IncludedFlags);
            Selected = other.Selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatFlags() { }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public override AutoCatType AutoCatType => AutoCatType.Flags;

        [XmlArray("Flags")]
        [XmlArrayItem("Flag")]
        public List<string> IncludedFlags { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatFlags LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlName_Prefix], null);
            List<string> flags = new List<string>();

            XmlElement flagListElement = xElement[XmlName_FlagList];
            if (flagListElement == null)
            {
                return new AutoCatFlags(name, filter, prefix, flags);
            }

            XmlNodeList flagElements = flagListElement.SelectNodes(XmlName_Flag);
            if (flagElements == null)
            {
                return new AutoCatFlags(name, filter, prefix, flags);
            }

            foreach (XmlNode n in flagElements)
            {
                if (XmlUtil.TryGetStringFromNode(n, out string flag))
                {
                    flags.Add(flag);
                }
            }

            return new AutoCatFlags(name, filter, prefix, flags);
        }

        /// <inheritdoc />
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

            Collection<string> gameFlags = db.GetFlagList(game.Id);
            IEnumerable<string> categories = gameFlags.Intersect(IncludedFlags);

            foreach (string catString in categories)
            {
                Category c = games.GetCategory(GetCategoryName(catString));
                game.AddCategory(c);
            }

            return AutoCatResult.Success;
        }

        /// <inheritdoc />
        public override AutoCat Clone()
        {
            return new AutoCatFlags(this);
        }

        /// <inheritdoc />
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

            writer.WriteStartElement(XmlName_FlagList);

            foreach (string s in IncludedFlags)
            {
                writer.WriteElementString(XmlName_Flag, s);
            }

            writer.WriteEndElement(); // flag list
            writer.WriteEndElement(); // type ID string
        }

        #endregion
    }
}
