﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer.Core.AutoCats;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Interfaces;
using Depressurizer.Core.Models;
using Depressurizer.Properties;

namespace Depressurizer.AutoCats
{
    /// <summary>
    ///     Abstract base class for autocategorization schemes. Call PreProcess before any set of autocat operations.
    ///     This is a preliminary form, and may change in future versions.
    ///     Returning only true / false on a categorization attempt may prove too simplistic.
    /// </summary>
    public abstract class AutoCat : IComparable, IComparable<AutoCat>
    {
        #region Fields

        protected IGameList games;

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

        public string Prefix { get; set; }

        [XmlIgnore]
        public bool Selected { get; set; }

        #endregion

        #region Properties

        protected static Database Database => Database.Instance;

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

        public static void GenerateDefaultAutoCatSet(List<AutoCat> list)
        {
            if (list == null)
            {
                list = new List<AutoCat>();
            }

            //By Genre
            list.Add(new AutoCatGenre(GlobalStrings.Profile_DefaultAutoCatName_Genre, null, "(" + GlobalStrings.Name_Genre + ") "));

            //By Year
            list.Add(new AutoCatYear(GlobalStrings.Profile_DefaultAutoCatName_Year, null, "(" + GlobalStrings.Name_Year + ") "));

            //By Score
            AutoCatUserScore ac = new AutoCatUserScore(GlobalStrings.Profile_DefaultAutoCatName_UserScore, null, "(" + GlobalStrings.Name_Score + ") ");
            ac.GenerateSteamRules(ac.Rules);
            list.Add(ac);

            //By Tags
            AutoCatTags act = new AutoCatTags(GlobalStrings.Profile_DefaultAutoCatName_Tags, null, "(" + GlobalStrings.Name_Tags + ") ");
            foreach (KeyValuePair<string, float> tag in Database.CalculateSortedTagList(null, 1, 20, 0, false, false))
            {
                act.IncludedTags.Add(tag.Key);
            }

            list.Add(act);

            //By Flags
            AutoCatFlags acf = new AutoCatFlags(GlobalStrings.Profile_DefaultAutoCatName_Flags, null, "(" + GlobalStrings.Name_Flags + ") ");
            foreach (string flag in Database.AllFlags)
            {
                acf.IncludedFlags.Add(flag);
            }

            list.Add(acf);

            //By HLTB
            AutoCatHltb ach = new AutoCatHltb(GlobalStrings.Profile_DefaultAutoCatName_Hltb, null, "(HLTB) ", false);
            ach.Rules.Add(new HowLongToBeatRule("0-5", 0, 5, TimeType.Extras));
            ach.Rules.Add(new HowLongToBeatRule("5-10", 5, 10, TimeType.Extras));
            ach.Rules.Add(new HowLongToBeatRule("10-20", 10, 20, TimeType.Extras));
            ach.Rules.Add(new HowLongToBeatRule("20-50", 20, 50, TimeType.Extras));
            ach.Rules.Add(new HowLongToBeatRule("50+", 20, 0, TimeType.Extras));
            list.Add(ach);

            //By Platform
            AutoCatPlatform acPlatform = new AutoCatPlatform(GlobalStrings.Profile_DefaultAutoCatName_Platform, null, "(" + GlobalStrings.AutoCat_Name_Platform + ") ", true, true, true, true);
            list.Add(acPlatform);
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
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture, GlobalStrings.Autocat_LoadFromXmlElement_Error, type.Name), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        /// <inheritdoc />
        public int CompareTo(AutoCat other)
        {
            if (other == null)
            {
                return 1;
            }

            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is AutoCat other))
            {
                throw new ArgumentException("Object must be of type AutoCat.");
            }

            return CompareTo(other);
        }

        public virtual void DeProcess()
        {
            games = null;
        }

        public virtual string GetCategoryName(string name)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                return name;
            }

            return Prefix + name;
        }

        /// <summary>
        ///     Must be called before any categorizations are done. Should be overridden to perform any necessary database analysis
        ///     or other preparation.
        ///     After this is called, no configuration options should be changed before using CategorizeGame.
        /// </summary>
        public virtual void PreProcess(IGameList games)
        {
            this.games = games;
        }

        /// <inheritdoc />
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
