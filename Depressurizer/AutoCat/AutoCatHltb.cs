/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013, 2014 Steve Labbe.

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
using System.Xml;

namespace Depressurizer {

    public enum TimeType
    {
        Main,
        Extras,
        Completionist
    }

    public class Hltb_Rule {
        public string Name { get; set; }
        public float MinHours { get; set; }
        public float MaxHours { get; set; }
        public TimeType TimeType { get; set; }

        public Hltb_Rule( string name, float minHours, float maxHours, TimeType timeType ) {
            Name = name;
            MinHours = minHours;
            MaxHours = maxHours;
            TimeType = timeType;
        }
        public Hltb_Rule( Hltb_Rule other ) {
            Name = other.Name;
            MinHours = other.MinHours;
            MaxHours = other.MaxHours;
            TimeType = other.TimeType;
        }
    }

    public class AutoCatHltb : AutoCat {
        #region Properties
        public string Prefix { get; set; }
        public bool IncludeUnknown { get; set; }
        public string UnknownText { get; set; }
        public List<Hltb_Rule> Rules;

        public override AutoCatType AutoCatType {
            get { return AutoCatType.Hltb; }
        }

        public const string TypeIdString = "AutoCatHltb";

        public const string XmlName_Name = "Name",
            XmlName_Prefix = "Prefix",
            XmlName_IncludeUnknown = "IncludeUnknown",
            XmlName_UnknownText = "UnknownText",
            XmlName_Rule = "Rule",
            XmlName_Rule_Text = "Text",
            XmlName_Rule_MinHours = "MinHours",
            XmlName_Rule_MaxHours = "MaxHours",
            XmlName_Rule_TimeType = "TimeType";

        #endregion

        #region Construction

        public AutoCatHltb(string name = TypeIdString, string prefix = "", bool includeUnknown = true, string unknownText = "", List<Hltb_Rule> rules = null)
            : base( name ) {
            Prefix = prefix;
            IncludeUnknown = includeUnknown;
            UnknownText = unknownText;
            Rules = ( rules == null ) ? new List<Hltb_Rule>() : rules;
        }

        public AutoCatHltb( AutoCatHltb other )
            : base( other ) {
            Prefix = other.Prefix;
            IncludeUnknown = other.IncludeUnknown;
            UnknownText = other.UnknownText;
            Rules = other.Rules.ConvertAll( rule => new Hltb_Rule( rule ) );
        }

        public override AutoCat Clone() {
            return new AutoCatHltb( this );
        }

        #endregion

        #region Autocategorization
        public override AutoCatResult CategorizeGame( GameInfo game ) {
            if( games == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull );
                throw new ApplicationException( GlobalStrings.AutoCatGenre_Exception_NoGameList );
            }
            if( db == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_DBNull );
                throw new ApplicationException( GlobalStrings.AutoCatGenre_Exception_NoGameDB );
            }
            if( game == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull );
                return AutoCatResult.Failure;
            }

            if( !db.Contains( game.Id ) ) return AutoCatResult.NotInDatabase;

            string result = null;
            
            float hltbMain = db.Games[game.Id].HltbMain/60.0f;
            float hltbExtras = db.Games[game.Id].HltbExtras/60.0f;
            float hltbCompletionist = db.Games[game.Id].HltbCompletionist/60.0f;

            if (IncludeUnknown && hltbMain == 0.0f && hltbExtras == 0.0f && hltbCompletionist == 0.0f)
                result = UnknownText;
            else
            {
                foreach (Hltb_Rule rule in Rules)
                {
                    if (CheckRule(rule, hltbMain, hltbExtras, hltbCompletionist))
                    {
                        result = rule.Name;
                        break;
                    }
                }
            }

            if( result != null ) {
                result = GetProcessedString( result );
                game.AddCategory( games.GetCategory( result ) );
            }
            return AutoCatResult.Success;
        }

        private bool CheckRule( Hltb_Rule rule, float hltbMain, float hltbExtras, float hltbCompletionist )
        {
            float hours = 0.0f;
            if (rule.TimeType == TimeType.Main)
                hours = hltbMain;
            else if (rule.TimeType == TimeType.Extras)
                hours = hltbExtras;
            else if (rule.TimeType == TimeType.Completionist)
                hours = hltbCompletionist;
            if (hours == 0.0f) return false;
            return ( hours >= rule.MinHours && ( hours <= rule.MaxHours || rule.MaxHours ==0.0f ) );
        }

        private string GetProcessedString( string s ) {
            if( !string.IsNullOrEmpty( Prefix ) ) return Prefix + s;
            return s;
        }

        #endregion

        #region Serialization

        public override void WriteToXml( XmlWriter writer ) {
            writer.WriteStartElement( TypeIdString );

            writer.WriteElementString( XmlName_Name, this.Name );
            writer.WriteElementString( XmlName_Prefix, this.Prefix );
            writer.WriteElementString(XmlName_IncludeUnknown, this.IncludeUnknown.ToString());
            writer.WriteElementString(XmlName_UnknownText, this.UnknownText);


            foreach( Hltb_Rule rule in Rules ) {
                writer.WriteStartElement( XmlName_Rule );
                writer.WriteElementString( XmlName_Rule_Text, rule.Name );
                writer.WriteElementString( XmlName_Rule_MinHours, rule.MinHours.ToString() );
                writer.WriteElementString( XmlName_Rule_MaxHours, rule.MaxHours.ToString() );
                writer.WriteElementString( XmlName_Rule_TimeType, rule.TimeType.ToString() );

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public static AutoCatHltb LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlName_Name], TypeIdString );
            string prefix = XmlUtil.GetStringFromNode( xElement[XmlName_Prefix], string.Empty );
            bool includeUnknown = XmlUtil.GetBoolFromNode(xElement[XmlName_IncludeUnknown], false);
            string unknownText = XmlUtil.GetStringFromNode(xElement[XmlName_UnknownText], string.Empty);

            List<Hltb_Rule> rules = new List<Hltb_Rule>();
            foreach( XmlNode node in xElement.SelectNodes( XmlName_Rule ) ) {
                string ruleName = XmlUtil.GetStringFromNode( node[XmlName_Rule_Text], string.Empty );
                float ruleMin = XmlUtil.GetFloatFromNode(node[XmlName_Rule_MinHours], 0 );
                float ruleMax = XmlUtil.GetFloatFromNode( node[XmlName_Rule_MaxHours], 0 );
                string type = XmlUtil.GetStringFromNode(node[XmlName_Rule_TimeType], string.Empty);
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
                rules.Add( new Hltb_Rule( ruleName, ruleMin, ruleMax, ruleTimeType ) );
            }
            AutoCatHltb result = new AutoCatHltb(name, prefix, includeUnknown, unknownText) {Rules = rules};
            return result;
        }

        #endregion
    }
}
