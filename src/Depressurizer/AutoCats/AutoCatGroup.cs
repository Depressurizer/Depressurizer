using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer.AutoCats
{
    public class AutoCatGroup : AutoCat
    {
        #region Constants

        // Serialization strings
        public const string TypeIdString = "AutoCatGroup";

        public const string XmlName_Autocat = "Autocat";

        public const string XmlName_Autocats = "Autocats";

        #endregion

        #region Constructors and Destructors

        public AutoCatGroup(string name, string filter = null, List<string> autocats = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Autocats = autocats ?? new List<string>();
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

        /// <inheritdoc />
        public override AutoCatType AutoCatType => AutoCatType.Group;

        /// <inheritdoc />
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
            string name = XmlUtil.GetStringFromNode(xElement[Serialization.Constants.Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[Serialization.Constants.Filter], null);
            List<string> autocats = XmlUtil.GetStringsFromNodeList(xElement.SelectNodes(XmlName_Autocats + "/" + XmlName_Autocat));

            return new AutoCatGroup(name, filter, autocats);
        }

        /// <inheritdoc />
        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (game == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!Database.Contains(game.Id))
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            return AutoCatResult.Success;
        }

        /// <inheritdoc />
        public override AutoCat Clone()
        {
            return new AutoCatGroup(this);
        }

        /// <inheritdoc />
        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(Serialization.Constants.Name, Name);
            if (Filter != null)
            {
                writer.WriteElementString(Serialization.Constants.Filter, Filter);
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
