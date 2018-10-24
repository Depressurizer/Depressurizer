#region LICENSE

//     This file (AutoCatName.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Text.RegularExpressions;
using System.Xml;
using Rallion;

namespace Depressurizer
{
    public class AutoCatName : AutoCat
    {
        #region Constants

        public const string TypeIdString = "AutoCatName";

        public const string XmlName_GroupNonEnglishCharacters = "GroupNonEnglishCharacters";

        public const string XmlName_GroupNonEnglishCharactersText = "GroupNonEnglishCharactersText";

        public const string XmlName_GroupNumbers = "GroupNumbers";

        public const string XmlName_Name = "Name";

        public const string XmlName_Prefix = "Prefix";

        public const string XmlName_SkipThe = "SkipThe";

        #endregion

        #region Constructors and Destructors

        public AutoCatName(string name, string prefix = "", bool skipThe = true, bool groupNumbers = false, bool groupNonEnglishCharacters = false, string groupNonEnglishCharactersText = "") : base(name)
        {
            Name = name;
            Prefix = prefix;
            SkipThe = skipThe;
            GroupNumbers = groupNumbers;
            GroupNonEnglishCharacters = groupNonEnglishCharacters;
            GroupNonEnglishCharactersText = groupNonEnglishCharactersText;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatName() { }

        #endregion

        #region Public Properties

        public override AutoCatType AutoCatType => AutoCatType.Name;

        public bool GroupNonEnglishCharacters { get; set; }

        public string GroupNonEnglishCharactersText { get; set; }

        public bool GroupNumbers { get; set; }

        public string Prefix { get; set; }

        public bool SkipThe { get; set; }

        #endregion

        #region Public Methods and Operators

        public static AutoCatName LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlName_Prefix], null);
            bool skipThe = XmlUtil.GetBoolFromNode(xElement[XmlName_SkipThe], true);
            bool groupNumbers = XmlUtil.GetBoolFromNode(xElement[XmlName_GroupNumbers], true);
            bool groupNonEnglishCharacters = XmlUtil.GetBoolFromNode(xElement[XmlName_GroupNonEnglishCharacters], false);
            string groupNonEnglishCharactersText = XmlUtil.GetStringFromNode(xElement[XmlName_GroupNonEnglishCharactersText], null);

            return new AutoCatName(name, prefix, skipThe, groupNumbers, groupNonEnglishCharacters, groupNonEnglishCharactersText);
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

            if (!db.Contains(game.Id))
            {
                return AutoCatResult.NotInDatabase;
            }


            string cat = game.Name.Substring(0, 1);
            cat = cat.ToUpper();
            if (SkipThe && cat == "T" && game.Name.Substring(0, 4).ToUpper() == "THE ")
            {
                cat = game.Name.Substring(4, 1).ToUpper();
            }

            if (GroupNumbers && char.IsDigit(cat[0]))
            {
                cat = "#";
            }
            else if (GroupNonEnglishCharacters && !string.IsNullOrEmpty(GroupNonEnglishCharactersText) && Regex.IsMatch(cat, "[^a-z0-9]", RegexOptions.IgnoreCase))
            {
                cat = GroupNonEnglishCharactersText;
            }

            if (Prefix != null)
            {
                cat = Prefix + cat;
            }

            game.AddCategory(games.GetCategory(cat));

            return AutoCatResult.Success;
        }

        public override AutoCat Clone()
        {
            return new AutoCatName(Name, Prefix, SkipThe, GroupNumbers, GroupNonEnglishCharacters, GroupNonEnglishCharactersText);
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlName_Name, Name);
            writer.WriteElementString(XmlName_Prefix, Prefix);
            writer.WriteElementString(XmlName_SkipThe, SkipThe.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlName_GroupNumbers, GroupNumbers.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlName_GroupNonEnglishCharacters, GroupNonEnglishCharacters.ToString().ToLowerInvariant());
            writer.WriteElementString(XmlName_GroupNonEnglishCharactersText, GroupNonEnglishCharactersText);

            writer.WriteEndElement(); // type ID string
        }

        #endregion
    }
}
