using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;
using Depressurizer.Properties;

namespace Depressurizer
{
    /// <summary>
    ///     Abstract base class for autocategorization schemes. Call PreProcess before any set of autocat operations.
    ///     This is a preliminary form, and may change in future versions.
    ///     Returning only true / false on a categorization attempt may prove too simplistic.
    /// </summary>
    public abstract class AutoCat : IComparable
    {
        #region Fields

        protected Database db;

        protected GameList games;

        #endregion

        #region Constructors and Destructors

        protected AutoCat(string name)
        {
            Name = name;
            Filter = null;
        }

        protected AutoCat(AutoCat other)
        {
            Name = other.Name;
            Filter = other.Filter;
        }

        protected AutoCat() { }

        #endregion

        #region Public Properties

        public abstract AutoCatType AutoCatType { get; }

        public virtual string DisplayName
        {
            get
            {
                string displayName = Name;
                if (Filter != null)
                {
                    displayName += "*";
                }

                return displayName;
            }
        }

        public string Filter { get; set; }

        public string Name { get; set; }

        [XmlIgnore]
        public bool Selected { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCat Create(AutoCatType type, string name)
        {
            switch (type)
            {
                case AutoCatType.None:
                    return null;
                case AutoCatType.Genre:
                    return new AutoCatGenre(name);
                case AutoCatType.Flags:
                    return new AutoCatFlags(name);
                case AutoCatType.Tags:
                    return new AutoCatTags(name);
                case AutoCatType.Year:
                    return new AutoCatYear(name);
                case AutoCatType.UserScore:
                    return new AutoCatUserScore(name);
                case AutoCatType.Hltb:
                    return new AutoCatHltb(name);
                case AutoCatType.Manual:
                    return new AutoCatManual(name);
                case AutoCatType.DevPub:
                    return new AutoCatDevPub(name);
                case AutoCatType.Group:
                    return new AutoCatGroup(name);
                case AutoCatType.Name:
                    return new AutoCatName(name);
                case AutoCatType.VrSupport:
                    return new AutoCatVrSupport(name);
                case AutoCatType.Language:
                    return new AutoCatLanguage(name);
                case AutoCatType.Curator:
                    return new AutoCatCurator(name);
                case AutoCatType.Platform:
                    return new AutoCatPlatform(name);
                case AutoCatType.HoursPlayed:
                    return new AutoCatHoursPlayed(name);
                default:
                    return null;
            }
        }

        public static AutoCat LoadACFromXmlElement(XmlElement xElement)
        {
            string type = xElement.Name;

            switch (type)
            {
                case AutoCatGenre.TypeIdString:
                    return AutoCatGenre.LoadFromXmlElement(xElement);
                case AutoCatFlags.TypeIdString:
                    return AutoCatFlags.LoadFromXmlElement(xElement);
                case AutoCatTags.TypeIdString:
                    return AutoCatTags.LoadFromXmlElement(xElement);
                case AutoCatYear.TypeIdString:
                    return AutoCatYear.LoadFromXmlElement(xElement);
                case AutoCatUserScore.TypeIdString:
                    return AutoCatUserScore.LoadFromXmlElement(xElement);
                case AutoCatHltb.TypeIdString:
                    return AutoCatHltb.LoadFromXmlElement(xElement);
                case AutoCatManual.TypeIdString:
                    return AutoCatManual.LoadFromXmlElement(xElement);
                case AutoCatDevPub.TypeIdString:
                    return AutoCatDevPub.LoadFromXmlElement(xElement);
                case AutoCatGroup.TypeIdString:
                    return AutoCatGroup.LoadFromXmlElement(xElement);
                case AutoCatName.TypeIdString:
                    return AutoCatName.LoadFromXmlElement(xElement);
                case AutoCatVrSupport.TypeIdString:
                    return AutoCatVrSupport.LoadFromXmlElement(xElement);
                case AutoCatLanguage.TypeIdString:
                    return AutoCatLanguage.LoadFromXmlElement(xElement);
                case AutoCatCurator.TypeIdString:
                    return LoadFromXmlElement(xElement, typeof(AutoCatCurator));
                case AutoCatPlatform.TypeIdString:
                    return LoadFromXmlElement(xElement, typeof(AutoCatPlatform));
                case AutoCatHoursPlayed.TypeIdString:
                    return AutoCatHoursPlayed.LoadFromXmlElement(xElement);
                default:
                    return null;
            }
        }

        public static AutoCat LoadFromXmlElement(XmlElement xElement, Type type)
        {
            XmlReader reader = new XmlNodeReader(xElement);
            XmlSerializer x = new XmlSerializer(type);
            try
            {
                return (AutoCat) x.Deserialize(reader);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(GlobalStrings.Autocat_LoadFromXmlElement_Error, type.Name), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.Exception($"Failed to load from xml an Autocat of type {type.FullName}: ", e);
            }

            return null;
        }

        /// <summary>
        ///     Applies this autocategorization scheme to the game with the given ID.
        /// </summary>
        /// <param name="game">The GameInfo object to process</param>
        /// <returns>
        ///     False if the game was not found in database. This allows the calling function to potentially re-scrape data
        ///     and reattempt.
        /// </returns>
        public abstract AutoCatResult CategorizeGame(GameInfo game, Filter filter);

        public abstract AutoCat Clone();

        public int CompareTo(object other)
        {
            if (other is AutoCat)
            {
                return string.Compare(Name, (other as AutoCat).Name);
            }

            return 1;
        }

        public virtual void DeProcess()
        {
            games = null;
            db = null;
        }

        /// <summary>
        ///     Must be called before any categorizations are done. Should be overridden to perform any necessary database analysis
        ///     or other preparation.
        ///     After this is called, no configuration options should be changed before using CategorizeGame.
        /// </summary>
        public virtual void PreProcess(GameList games, Database db)
        {
            this.games = games;
            this.db = db;
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual void WriteToXml(XmlWriter writer)
        {
            XmlSerializer x = new XmlSerializer(GetType());
            XmlSerializerNamespaces nameSpace = new XmlSerializerNamespaces();
            nameSpace.Add("", "");
            x.Serialize(writer, this, nameSpace);
        }

        #endregion
    }
}
