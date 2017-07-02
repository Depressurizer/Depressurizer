/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

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

using Rallion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Depressurizer
{
    class AutoCatName : AutoCat
    {
        public string Prefix { get; set; }
        public bool SkipThe { get; set; }
        public bool GroupNumbers { get; set; }

        public override AutoCatType AutoCatType
        {
            get
            {
                return AutoCatType.Name;
            }
        }

        public const string TypeIdString = "AutoCatName";
        public const string XmlName_Prefix = "Prefix";
        public const string XmlName_Name = "Name";
        public const string XmlName_SkipThe = "SkipThe";
        public const string XmlName_GroupNumbers = "GroupNumbers";

        public AutoCatName(string name, string prefix="",bool skipThe = true, bool groupNumbers = false):base(name)
        {
            Name = name;
            Prefix = prefix;
            SkipThe = skipThe;
            GroupNumbers = groupNumbers;
        }

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (Games == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }
            if (Db == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_DBNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }
            if (game == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!Db.Contains(game.Id)) return AutoCatResult.NotInDatabase;

            
            string cat = game.Name.Substring(0, 1);
            cat = cat.ToUpper();
            if (SkipThe && cat == "T" && game.Name.Substring(0, 4).ToUpper() == "THE ") cat = game.Name.Substring(4, 1).ToUpper();
            if (GroupNumbers && Char.IsDigit(cat[0])) cat = "#";
            if (Prefix!=null) cat = Prefix + cat;

            game.AddCategory(Games.GetCategory(cat));

            return AutoCatResult.Success;
        }

        public override AutoCat Clone()
        {
            return new AutoCatName(Name,Prefix,SkipThe, GroupNumbers);
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlName_Name, Name);
            writer.WriteElementString(XmlName_Prefix, Prefix);
            writer.WriteElementString(XmlName_SkipThe, SkipThe.ToString());
            writer.WriteElementString(XmlName_GroupNumbers, GroupNumbers.ToString());

            writer.WriteEndElement(); // type ID string
        }

        public static AutoCatName LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlName_Name], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlName_Prefix], null);
            bool skipThe = Boolean.Parse(XmlUtil.GetStringFromNode(xElement[XmlName_SkipThe], null));
            bool groupNumbers = Boolean.Parse(XmlUtil.GetStringFromNode(xElement[XmlName_GroupNumbers], null));

            return new AutoCatName(name, prefix,skipThe,groupNumbers);
        }
    }
}
