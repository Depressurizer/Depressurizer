using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using Depressurizer.Core.Models;

namespace Depressurizer.AutoCats
{
    /// <summary>
    ///     Autocategorization scheme that adds and removes manual categories.
    /// </summary>
    public class AutoCatManual : AutoCat
    {
        #region Constants

        public const string TypeIdString = "AutoCatManual";

        private const string XmlName_AddItem = "Category";

        private const string XmlName_AddList = "Add";

        private const string XmlName_RemoveAll = "RemoveAll";

        private const string XmlName_RemoveItem = "Category";

        private const string XmlName_RemoveList = "Remove";

        #endregion

        #region Fields

        private IGameList gamelist;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Creates a new AutoCatManual object, which removes selected (or all) categories from one list and then, optionally,
        ///     assigns categories from another list.
        /// </summary>
        public AutoCatManual(string name, string filter = null, string prefix = null, bool removeAll = false, List<string> remove = null, List<string> add = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            RemoveAllCategories = removeAll;
            RemoveCategories = remove ?? new List<string>();
            AddCategories = add ?? new List<string>();
            Selected = selected;
        }

        protected AutoCatManual(AutoCatManual other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            RemoveAllCategories = other.RemoveAllCategories;
            RemoveCategories = new List<string>(other.RemoveCategories);
            AddCategories = new List<string>(other.AddCategories);
            Selected = other.Selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatManual() { }

        #endregion

        #region Public Properties

        [XmlArray("Add")]
        [XmlArrayItem("Category")]
        public List<string> AddCategories { get; set; }

        /// <inheritdoc />
        public override AutoCatType AutoCatType => AutoCatType.Manual;

        // Autocat configuration
        [XmlElement("RemoveAll")]
        public bool RemoveAllCategories { get; set; }

        [XmlArray("Remove")]
        [XmlArrayItem("Category")]
        public List<string> RemoveCategories { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatManual LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[Serialization.Constants.Name], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[Serialization.Constants.Filter], null);
            bool removeAll = XmlUtil.GetBoolFromNode(xElement[XmlName_RemoveAll], false);
            string prefix = XmlUtil.GetStringFromNode(xElement[Serialization.Constants.Prefix], null);

            List<string> remove = new List<string>();

            XmlElement removeListElement = xElement[XmlName_RemoveList];
            XmlNodeList removeNodes = removeListElement?.SelectNodes(XmlName_RemoveItem);
            if (removeNodes != null)
            {
                foreach (XmlNode node in removeNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(node, out string s))
                    {
                        remove.Add(s);
                    }
                }
            }

            List<string> add = new List<string>();

            XmlElement addListElement = xElement[XmlName_AddList];
            XmlNodeList addNodes = addListElement?.SelectNodes(XmlName_AddItem);
            if (addNodes != null)
            {
                foreach (XmlNode node in addNodes)
                {
                    if (XmlUtil.TryGetStringFromNode(node, out string s))
                    {
                        add.Add(s);
                    }
                }
            }

            AutoCatManual result = new AutoCatManual(name, filter, prefix, removeAll, remove, add);
            return result;
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

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            if (!Database.Contains(game.Id, out DatabaseEntry entry) || entry.LastStoreScrape == 0)
            {
                return AutoCatResult.NotInDatabase;
            }

            if (RemoveAllCategories)
            {
                game.ClearCategories();
            }
            else if (RemoveCategories != null)
            {
                List<Category> removed = new List<Category>();

                foreach (string category in RemoveCategories)
                {
                    Category c = gamelist.GetCategory(category);
                    if (!game.ContainsCategory(c))
                    {
                        continue;
                    }

                    game.RemoveCategory(c);
                    removed.Add(c);
                }

                foreach (Category c in removed)
                {
                    if (c.Count == 0)
                    {
                        gamelist.RemoveCategory(c);
                    }
                }
            }

            if (AddCategories == null)
            {
                return AutoCatResult.Success;
            }

            foreach (string category in AddCategories)
                // add Category, or create it if it doesn't exist
            {
                game.AddCategory(gamelist.GetCategory(GetCategoryName(category)));
            }

            return AutoCatResult.Success;
        }

        /// <inheritdoc />
        public override AutoCat Clone()
        {
            return new AutoCatManual(this);
        }

        /// <inheritdoc />
        public override void DeProcess()
        {
            base.DeProcess();
            gamelist = null;
        }

        /// <summary>
        ///     Prepares to categorize games. Prepares a list of genre categories to remove. Does nothing if removeothergenres is
        ///     false.
        /// </summary>
        public override void PreProcess(IGameList games)
        {
            base.PreProcess(games);
            gamelist = games;
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

            if (Prefix != null)
            {
                writer.WriteElementString(Serialization.Constants.Prefix, Prefix);
            }

            writer.WriteElementString(XmlName_RemoveAll, RemoveAllCategories.ToString().ToLowerInvariant());

            writer.WriteStartElement(XmlName_RemoveList);
            foreach (string s in RemoveCategories)
            {
                writer.WriteElementString(XmlName_RemoveItem, s);
            }

            writer.WriteEndElement();

            writer.WriteStartElement(XmlName_AddList);
            foreach (string s in AddCategories)
            {
                writer.WriteElementString(XmlName_AddItem, s);
            }

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        #endregion
    }
}
