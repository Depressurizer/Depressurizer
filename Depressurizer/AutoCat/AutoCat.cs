/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.ComponentModel;
using System.Xml;

/* ADDING NEW AUTOCAT METHODS
 * 
 * Here is a list of everything you need to do to add an additional autocat method.
 * 
 * 1) Add an element to the AutoCatType enum.
 * 
 * 2a) Create a new class that extends the AutoCat abstract base class.
 *    Things to implement in derived classes:
 *       public override AutoCatType AutoCatType: Just return the enum value you added.
 *       public override AutoCat Clone(): return a complete deep copy of the object
 *       
 *       public abstract AutoCatResult CategorizeGame( GameInfo game ): Perform autocategorization on a game.
 *       [Optional] public override void PreProcess( GameList games, GameDB db ): Do any pre-processing you want to do before a set of autocategorization operations.
 *       [Optional] public override void DeProcess(): Clean up after a set of autocategorization operations.
 *
 *       [Recommended] public string TypeIDString: Just a constant string that serves as a type identifier for serialization purposes.
 *       public override void WriteToXml( XmlWriter writer ): Write an XML object for saving. Write to one element, with a name that matches your type (TypeIDString).
 *       [Recommended] public AutoCat LoadFromXMLElement( XmlElement ): Read the object from an XmlElement.
 * 
 * 2b) Update the following static methods in the AutoCat class to include your new class:
 *       LoadACFromXMLElement: Must be able to handle reading an object of your selected type.
 *       Create: Just create a new object.
 *       
 * 3a) Create a class that extends AutoCatConfigPanel.
 *    This is a user control that defines the settings UI used in the AutoCat config dialog.
 *    The easiest way to start  in VS is to create a new User Control, then change it so that it extends AutoCatConfigPanel instead of UserControl.
 *       NOTE: You should be able to edit the new control using the VS designer. If it gives you an error about not being able to create an instance of AutoCatConfigPanel,
 *             close the derived class, clean solution, restart VS, rebuild solution. It should work then. If not, make AutoCatConfigPanel non-abstract when you want to
 *             use the designer.
 *    Things to implement:
 *       public override void SaveToAutoCat( AutoCat ac ): Takes the settings in the UI and saves them to the given AutoCat object.
 *       public override void LoadFromAutoCat( AutoCat ac ): Take the settings in the given AutoCat object and fill in the UI with them.
 * 
 * 3b) Update AutoCatConfigPanel.CreatePanel so that it can create a panel for your type.
 * 
 * 4) Update the arrays in the DlgAutoCatCreate constructor to allow creating AutoCats of your type.
 */

namespace Depressurizer
{
    public enum AutoCatType
    {
        [Description("None")] None,
        [Description("AutoCatGenre")] Genre,
        [Description("AutoCatFlags")] Flags,
        [Description("AutoCatTags")] Tags,
        [Description("AutoCatYear")] Year,
        [Description("AutoCatUserScore")] UserScore,
        [Description("AutoCatHltb")] Hltb,
        [Description("AutoCatManual")] Manual,
        [Description("AutoCatDevPub")] DevPub,
        [Description("AutoCatGroup")] Group,
        [Description("AutoCatName")] Name,
        [Description("AutoCatVrSupport")] VrSupport
    }

    public enum AutoCatResult
    {
        Success,
        Failure,
        NotInDatabase,
        Filtered
    }

    /// <summary>
    /// Abstract base class for autocategorization schemes. Call PreProcess before any set of autocat operations.
    /// This is a preliminary form, and may change in future versions.
    /// Returning only true / false on a categorization attempt may prove too simplistic.
    /// </summary>
    public abstract class AutoCat : IComparable
    {
        private const string
            XmlName_Filter = "Filter";

        protected GameList games;
        protected GameDB db;

        public abstract AutoCatType AutoCatType { get; }

        public string Name { get; set; }

        public virtual string DisplayName
        {
            get
            {
                string displayName = Name;
                if (Filter != null) displayName += "*";
                return displayName;
            }
        }

        public string Filter { get; set; }

        public bool Selected { get; set; }

        public override string ToString()
        {
            return Name;
        }

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

        public abstract AutoCat Clone();

        public int CompareTo(object other)
        {
            if (other is AutoCat)
            {
                return string.Compare(Name, (other as AutoCat).Name);
            }
            return 1;
        }

        /// <summary>
        /// Must be called before any categorizations are done. Should be overridden to perform any necessary database analysis or other preparation.
        /// After this is called, no configuration options should be changed before using CategorizeGame.
        /// </summary>
        public virtual void PreProcess(GameList games, GameDB db)
        {
            this.games = games;
            this.db = db;
        }

        /// <summary>
        /// Applies this autocategorization scheme to the game with the given ID.
        /// </summary>
        /// <param name="gameId">The game ID to process</param>
        /// <returns>False if the game was not found in database. This allows the calling function to potentially re-scrape data and reattempt.</returns>
        public virtual AutoCatResult CategorizeGame(int gameId, Filter filter)
        {
            if (games.Games.ContainsKey(gameId))
            {
                return CategorizeGame(games.Games[gameId], filter);
            }
            return AutoCatResult.Failure;
        }

        /// <summary>
        /// Applies this autocategorization scheme to the game with the given ID.
        /// </summary>
        /// <param name="game">The GameInfo object to process</param>
        /// <returns>False if the game was not found in database. This allows the calling function to potentially re-scrape data and reattempt.</returns>
        public abstract AutoCatResult CategorizeGame(GameInfo game, Filter filter);

        public virtual void DeProcess()
        {
            games = null;
            db = null;
        }

        public abstract void WriteToXml(XmlWriter writer);

        public static AutoCat LoadACFromXmlElement(XmlElement xElement)
        {
            string type = xElement.Name;

            AutoCat result = null;
            switch (type)
            {
                case AutoCatGenre.TypeIdString:
                    result = AutoCatGenre.LoadFromXmlElement(xElement);
                    break;
                case AutoCatFlags.TypeIdString:
                    result = AutoCatFlags.LoadFromXmlElement(xElement);
                    break;
                case AutoCatTags.TypeIdString:
                    result = AutoCatTags.LoadFromXmlElement(xElement);
                    break;
                case AutoCatYear.TypeIdString:
                    result = AutoCatYear.LoadFromXmlElement(xElement);
                    break;
                case AutoCatUserScore.TypeIdString:
                    result = AutoCatUserScore.LoadFromXmlElement(xElement);
                    break;
                case AutoCatHltb.TypeIdString:
                    result = AutoCatHltb.LoadFromXmlElement(xElement);
                    break;
                case AutoCatManual.TypeIdString:
                    result = AutoCatManual.LoadFromXmlElement(xElement);
                    break;
                case AutoCatDevPub.TypeIdString:
                    result = AutoCatDevPub.LoadFromXmlElement(xElement);
                    break;
                case AutoCatGroup.TypeIdString:
                    result = AutoCatGroup.LoadFromXmlElement(xElement);
                    break;
                case AutoCatName.TypeIdString:
                    return AutoCatName.LoadFromXmlElement(xElement);
                case AutoCatVrSupport.TypeIdString:
                    return AutoCatVrSupport.LoadFromXmlElement(xElement);
                default:
                    break;
            }
            return result;
        }

        public static AutoCat Create(AutoCatType type, string name)
        {
            switch (type)
            {
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
                case AutoCatType.None:
                    return null;
                default:
                    return null;
            }
        }
    }
}