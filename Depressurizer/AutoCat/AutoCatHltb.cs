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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Depressurizer.Lib;
using Depressurizer.Model;

namespace Depressurizer.AutoCat
{
    public class AutoCatHltb : AutoCat
    {
        public string Prefix { get; set; }
        public bool IncludeUnknown { get; set; }
        public string UnknownText { get; set; }

        public override AutoCatType AutoCatType => AutoCatType.Hltb;

        public const string TypeIdString = "AutoCatHltb";

        public const string XmlNameName = "Name";
        public const string XmlNameFilter = "Filter";
        public const string XmlNamePrefix = "Prefix";
        public const string XmlNameIncludeUnknown = "IncludeUnknown";
        public const string XmlNameUnknownText = "UnknownText";
        public const string XmlNameRule = "Rule";
        public const string XmlNameRuleText = "Text";
        public const string XmlNameRuleMinHours = "MinHours";
        public const string XmlNameRuleMaxHours = "MaxHours";
        public const string XmlNameRuleTimeType = "TimeType";
        public List<HltbRule> Rules;

        public AutoCatHltb(string name = TypeIdString, string filter = null, string prefix = null,
            bool includeUnknown = true, string unknownText = "", List<HltbRule> rules = null,
            bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            IncludeUnknown = includeUnknown;
            UnknownText = unknownText;
            Rules = rules ?? new List<HltbRule>();
            Selected = selected;
        }

        public AutoCatHltb(AutoCatHltb other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludeUnknown = other.IncludeUnknown;
            UnknownText = other.UnknownText;
            Rules = other.Rules.ConvertAll(rule => new HltbRule(rule));
            Selected = other.Selected;
        }

        public override AutoCat Clone() => new AutoCatHltb(this);

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

            if (!Db.Contains(game.Id))
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            string result = null;

            float hltbMain = Db.Games[game.Id].HltbMain / 60.0f;
            float hltbExtras = Db.Games[game.Id].HltbExtras / 60.0f;
            float hltbCompletionist = Db.Games[game.Id].HltbCompletionist / 60.0f;

            if (IncludeUnknown && (hltbMain == 0.0f) && (hltbExtras == 0.0f) && (hltbCompletionist == 0.0f))
            {
                result = UnknownText;
            }
            else
            {
                foreach (HltbRule rule in Rules)
                {
                    if (CheckRule(rule, hltbMain, hltbExtras, hltbCompletionist))
                    {
                        result = rule.Name;
                        break;
                    }
                }
            }

            if (result != null)
            {
                result = GetProcessedString(result);
                game.AddCategory(Games.GetCategory(result));
            }
            return AutoCatResult.Success;
        }

        private bool CheckRule(HltbRule rule, float hltbMain, float hltbExtras, float hltbCompletionist)
        {
            float hours = 0.0f;
            if (rule.TimeType == TimeType.Main)
            {
                hours = hltbMain;
            }
            else if (rule.TimeType == TimeType.Extras)
            {
                hours = hltbExtras;
            }
            else if (rule.TimeType == TimeType.Completionist)
            {
                hours = hltbCompletionist;
            }
            if (hours == 0.0f)
            {
                return false;
            }

            return (hours >= rule.MinHours) && ((hours <= rule.MaxHours) || (rule.MaxHours == 0.0f));
        }

        private string GetProcessedString(string s)
        {
            if (!string.IsNullOrEmpty(Prefix))
            {
                return Prefix + s;
            }

            return s;
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlNameName, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlNameFilter, Filter);
            }
            if (Prefix != null)
            {
                writer.WriteElementString(XmlNamePrefix, Prefix);
            }
            writer.WriteElementString(XmlNameIncludeUnknown, IncludeUnknown.ToString());
            writer.WriteElementString(XmlNameUnknownText, UnknownText);

            foreach (HltbRule rule in Rules)
            {
                writer.WriteStartElement(XmlNameRule);
                writer.WriteElementString(XmlNameRuleText, rule.Name);
                writer.WriteElementString(XmlNameRuleMinHours, rule.MinHours.ToString(CultureInfo.CurrentCulture));
                writer.WriteElementString(XmlNameRuleMaxHours, rule.MaxHours.ToString(CultureInfo.CurrentCulture));
                writer.WriteElementString(XmlNameRuleTimeType, rule.TimeType.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public static AutoCatHltb LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlNamePrefix], string.Empty);
            bool includeUnknown = XmlUtil.GetBoolFromNode(xElement[XmlNameIncludeUnknown], false);
            string unknownText = XmlUtil.GetStringFromNode(xElement[XmlNameUnknownText], string.Empty);

            List<HltbRule> rules = new List<HltbRule>();
            XmlNodeList xmlNodeList = xElement.SelectNodes(XmlNameRule);
            if (xmlNodeList != null)
            {
                foreach (XmlNode node in xmlNodeList)
                {
                    string ruleName = XmlUtil.GetStringFromNode(node[XmlNameRuleText], string.Empty);
                    float ruleMin = XmlUtil.GetFloatFromNode(node[XmlNameRuleMinHours], 0);
                    float ruleMax = XmlUtil.GetFloatFromNode(node[XmlNameRuleMaxHours], 0);
                    string type = XmlUtil.GetStringFromNode(node[XmlNameRuleTimeType], string.Empty);
                    TimeType ruleTimeType;
                    switch (type)
                    {
                        case "Extras":
                            ruleTimeType = TimeType.Extras;
                            break;
                        case "Completionist":
                            ruleTimeType = TimeType.Completionist;
                            break;
                        default:
                            ruleTimeType = TimeType.Main;
                            break;
                    }

                    rules.Add(new HltbRule(ruleName, ruleMin, ruleMax, ruleTimeType));
                }
            }

            AutoCatHltb result = new AutoCatHltb(name, filter, prefix, includeUnknown, unknownText) {Rules = rules};
            return result;
        }
    }
}