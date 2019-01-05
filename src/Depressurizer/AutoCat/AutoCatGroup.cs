using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.Helpers;
using Depressurizer.Models;

namespace Depressurizer
{
    public class AutoCatGroup : AutoCat
    {
        #region Constants

        // Serialization strings
        public const string TypeIdString = "AutoCatGroup";

        public const string XmlName_Name = "Name", XmlName_Filter = "Filter", XmlName_Autocats = "Autocats", XmlName_Autocat = "Autocat";

        #endregion

        #region Constructors and Destructors

        public AutoCatGroup(string name, string filter = null, List<string> autocats = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Autocats = autocats == null ? new List<string>() : autocats;
            Selected = selected;
        }

        protected AutoCatGroup(AutoCatGroup other) : base(other)
        {
            Filter = other.Filter;
            Autocats = new List<string>(other.Autocats);
            Selected = other.Selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatGroup() { }

        #endregion

        #region Public Properties

        // Autocat configuration properties
        [XmlArrayItem("Autocat")]
        public List<string> Autocats { get; set; }

        // Meta properies
        public override AutoCatType AutoCatType => AutoCatType.Group;

        public override string DisplayName
        {
            get
            {
                string displayName = Name + "[" + Autocats.Count + "]";
                if (Filter != null)
                {
                    displayName += "*";
                }

                return displayName;
            }
        }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatGroup LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlName_Filter], null);
            List<string> autocats = XmlUtil.GetStringsFromNodeList(xElement.SelectNodes(XmlName_Autocats + "/" + XmlName_Autocat));

            return new AutoCatGroup(name, filter, autocats);
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

            if (!db.Contains(game.Id))
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            return AutoCatResult.Success;
        }

        public override AutoCat Clone()
        {
            return new AutoCatGroup(this);
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlName_Name, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlName_Filter, Filter);
            }

            if (Autocats != null && Autocats.Count > 0)
            {
                writer.WriteStartElement(XmlName_Autocats);
                foreach (string name in Autocats)
                {
                    writer.WriteElementString(XmlName_Autocat, name);
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement(); // type ID string
        }

        #endregion
    }
}
