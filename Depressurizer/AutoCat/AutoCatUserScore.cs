using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rallion;
using System.Xml;

namespace Depressurizer {
    public class UserScore_Rule {
        public string Name { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public int MinReviews { get; set; }
        public int MaxReviews { get; set; }
        public UserScore_Rule( string name, int minScore, int maxScore, int minReviews, int maxReviews ) {
            Name = name;
            MinScore = minScore;
            MaxScore = maxScore;
            MinReviews = minReviews;
            MaxReviews = maxReviews;
        }
        public UserScore_Rule( UserScore_Rule other ) {
            Name = other.Name;
            MinScore = other.MinScore;
            MaxScore = other.MaxScore;
            MinReviews = other.MinReviews;
            MaxReviews = other.MaxReviews;
        }
    }

    public class AutoCatUserScore : AutoCat {
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
            XmlName_Rule_MinScore = "MinScore",
            XmlName_Rule_MaxScore = "MaxScore",
            XmlName_Rule_MinReviews = "MinReviews",
            XmlName_Rule_MaxReviews = "MaxReviews";

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
            int reviews = db.Games[game.Id].ReviewTotal;
            string result = null;
            foreach( UserScore_Rule rule in Rules ) {
                if( CheckRule( rule, score, reviews ) ) {
                    result = rule.Name;
                    break;
                }
            }

            if( result != null ) {
                result = GetProcessedString( result );
                game.AddCategory( games.GetCategory( result ) );
            }
            return AutoCatResult.Success;
        }

        private bool CheckRule( UserScore_Rule rule, int score, int reviews ) {
            return ( score >= rule.MinScore && score <= rule.MaxScore ) && rule.MinReviews <= reviews && ( rule.MaxReviews == 0 || rule.MaxReviews >= reviews );
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
                writer.WriteElementString( XmlName_Rule_MinScore, rule.MinScore.ToString() );
                writer.WriteElementString( XmlName_Rule_MaxScore, rule.MaxScore.ToString() );
                writer.WriteElementString( XmlName_Rule_MinReviews, rule.MinReviews.ToString() );
                writer.WriteElementString( XmlName_Rule_MaxReviews, rule.MaxReviews.ToString() );

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
                int ruleMin = XmlUtil.GetIntFromNode( node[XmlName_Rule_MinScore], 0 );
                int ruleMax = XmlUtil.GetIntFromNode( node[XmlName_Rule_MaxScore], 100 );
                int ruleMinRev = XmlUtil.GetIntFromNode( node[XmlName_Rule_MinReviews], 0 );
                int ruleMaxRev = XmlUtil.GetIntFromNode( node[XmlName_Rule_MaxReviews], 0 );
                rules.Add( new UserScore_Rule( ruleName, ruleMin, ruleMax, ruleMinRev, ruleMaxRev ) );
            }
            AutoCatUserScore result = new AutoCatUserScore( name, prefix );
            result.Rules = rules;
            return result;
        }

        #endregion
    }


}
