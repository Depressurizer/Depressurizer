using System;
using System.Text.RegularExpressions;
using System.Xml;
using Rallion;

namespace Depressurizer
{
    class AutoCatName : AutoCat
    {
        public string Prefix { get; set; }
        public bool SkipThe { get; set; }
        public bool GroupNumbers { get; set; }
        public bool GroupNonEnglishCharacters { get; set; }
        public string GroupNonEnglishCharactersText { get; set; }


        public override AutoCatType AutoCatType
        {
            get { return AutoCatType.Name; }
        }

        public const string TypeIdString = "AutoCatName";
        public const string XmlName_Prefix = "Prefix";
        public const string XmlName_Name = "Name";
        public const string XmlName_SkipThe = "SkipThe";
        public const string XmlName_GroupNumbers = "GroupNumbers";
        public const string XmlName_GroupNonEnglishCharacters = "GroupNonEnglishCharacters";
        public const string XmlName_GroupNonEnglishCharactersText = "GroupNonEnglishCharactersText";

        public AutoCatName(string name, string prefix = "", bool skipThe = true, bool groupNumbers = false, bool groupNonEnglishCharacters = false, string groupNonEnglishCharactersText = "") : base(name)
        {
            Name = name;
            Prefix = prefix;
            SkipThe = skipThe;
            GroupNumbers = groupNumbers;
            GroupNonEnglishCharacters = groupNonEnglishCharacters;
            GroupNonEnglishCharactersText = groupNonEnglishCharactersText;
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
            if (SkipThe && (cat == "T") && (game.Name.Substring(0, 4).ToUpper() == "THE "))
            {
                cat = game.Name.Substring(4, 1).ToUpper();
            }
            if (GroupNumbers && Char.IsDigit(cat[0]))
            {
                cat = "#";
            }
            else if (GroupNonEnglishCharacters && !string.IsNullOrEmpty(GroupNonEnglishCharactersText) &&
                Regex.IsMatch(cat, "[^a-z0-9]", RegexOptions.IgnoreCase))
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
            writer.WriteElementString(XmlName_SkipThe, SkipThe.ToString());
            writer.WriteElementString(XmlName_GroupNumbers, GroupNumbers.ToString());
            writer.WriteElementString(XmlName_GroupNonEnglishCharacters, GroupNonEnglishCharacters.ToString());
            writer.WriteElementString(XmlName_GroupNonEnglishCharactersText, GroupNonEnglishCharactersText);

            writer.WriteEndElement(); // type ID string
        }

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
    }
}