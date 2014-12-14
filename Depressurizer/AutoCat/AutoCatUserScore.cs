using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rallion;
using System.Xml;

namespace Depressurizer {
    class UserScore_Rule {
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public UserScore_Rule( string name, int min, int max ) {
            Name = name;
            Min = min;
            Max = max;
        }
        public UserScore_Rule( UserScore_Rule other ) {
            Name = other.Name;
            Min = other.Min;
            Max = other.Max;
        }
    }

    class AutoCatUserScore : AutoCat {
        #region Properties
        public string Prefix { get; set; }
        public List<UserScore_Rule> Rules;

        public override AutoCatType AutoCatType {
            get { return AutoCatType.UserScore; }
        }

        public const string TypeIdString = "AutoCatUserScore";
        public const string XmlName_Name = "Name",
            XmlName_Prefix = "Prefix",
            XmlName_Rule = "Rule",
            XmlName_Rule_Text = "Text",
            XmlName_Rule_Min = "Min",
            XmlName_Rule_Max = "Max";

        #endregion

        #region Construction

        public AutoCatUserScore( string name = TypeIdString, string prefix = "", List<UserScore_Rule> rules = null )
            : base( name ) {
            Prefix = prefix;
            Rules = ( rules == null ) ? new List<UserScore_Rule>() : rules;
        }

        public AutoCatUserScore( AutoCatUserScore other )
            : base( other ) {
            Prefix = other.Prefix;
            Rules = other.Rules.ConvertAll( rule => new UserScore_Rule( rule ) );
        }

        public override AutoCat Clone() {
            return new AutoCatUserScore( this );
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

            int score = db.Games[game.Id].ReviewPositivePercentage;
            string result = null;
            foreach( UserScore_Rule rule in Rules ) {
                if( CheckRule( rule, score ) ) {
                    result = rule.Name;
                    continue;
                }
            }

            if( result != null ) {
                result = GetProcessedString( result );
                game.AddCategory( games.GetCategory( result ) );
            }
            return AutoCatResult.Success;
        }

        private bool CheckRule( UserScore_Rule rule, int score ) {
            return ( score >= rule.Min && score <= rule.Max );
        }

        private string GetProcessedString( string s ) {
            if( !string.IsNullOrEmpty( Prefix ) ) return Prefix + s;
            return s;
        }

        #endregion

        #region Serialization

        public override void WriteToXml( System.Xml.XmlWriter writer ) {
            writer.WriteStartElement( TypeIdString );

            writer.WriteElementString( XmlName_Name, this.Name );
            writer.WriteElementString( XmlName_Prefix, this.Prefix );

            foreach( UserScore_Rule rule in Rules ) {
                writer.WriteStartElement( XmlName_Rule );
                writer.WriteElementString( XmlName_Rule_Text, rule.Name );
                writer.WriteElementString( XmlName_Rule_Min, rule.Min.ToString() );
                writer.WriteElementString( XmlName_Rule_Max, rule.Max.ToString() );
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public static AutoCatUserScore LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlName_Name], TypeIdString );
            string prefix = XmlUtil.GetStringFromNode( xElement[XmlName_Prefix], string.Empty );

            List<UserScore_Rule> rules = new List<UserScore_Rule>();
            foreach( XmlNode node in xElement.SelectNodes( XmlName_Rule ) ) {
                string ruleName = XmlUtil.GetStringFromNode( node[XmlName_Rule_Text], string.Empty );
                int ruleMin = XmlUtil.GetIntFromNode( node[XmlName_Rule_Min], 0 );
                int ruleMax = XmlUtil.GetIntFromNode( node[XmlName_Rule_Max], 0 );
                rules.Add( new UserScore_Rule( ruleName, ruleMin, ruleMax ) );
            }
            AutoCatUserScore result = new AutoCatUserScore( name, prefix );
            result.Rules = rules;
            return result;
        }

        #endregion
    }
}
