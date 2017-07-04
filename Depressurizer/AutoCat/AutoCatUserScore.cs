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
using System.Linq;
using System.Xml;
using Depressurizer.Lib;
using Depressurizer.Model;

namespace Depressurizer.AutoCat
{
    public class AutoCatUserScore : AutoCat
    {
        public string Prefix { get; set; }
        public bool UseWilsonScore { get; internal set; }

        public override AutoCatType AutoCatType => AutoCatType.UserScore;

        public const string TypeIdString = "AutoCatUserScore";

        public const string XmlNameName = "Name";
        public const string XmlNameFilter = "Filter";
        public const string XmlNamePrefix = "Prefix";
        public const string XmlNameUseWilsonScore = "UseWilsonScore";
        public const string XmlNameRule = "Rule";
        public const string XmlNameRuleText = "Text";
        public const string XmlNameRuleMinScore = "MinScore";
        public const string XmlNameRuleMaxScore = "MaxScore";
        public const string XmlNameRuleMinReviews = "MinReviews";
        public const string XmlNameRuleMaxReviews = "MaxReviews";
        public List<UserScoreRule> Rules;

        public AutoCatUserScore(string name = TypeIdString, string filter = null, string prefix = null,
            bool useWilsonScore = false, List<UserScoreRule> rules = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            UseWilsonScore = useWilsonScore;
            Rules = rules ?? new List<UserScoreRule>();
            Selected = selected;
        }

        public AutoCatUserScore(AutoCatUserScore other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            UseWilsonScore = other.UseWilsonScore;
            Rules = other.Rules.ConvertAll(rule => new UserScoreRule(rule));
            Selected = other.Selected;
        }

        /// <summary>
        ///     Generates rules that match the Steam Store rating labels
        /// </summary>
        /// <param name="rules">List of UserScoreRule objects to populate with the new ones. Should generally be empty.</param>
        public void GenerateSteamRules(ICollection<UserScoreRule> rules)
        {
            rules.Add(new UserScoreRule(GlobalStrings.AutoCatUserScore_Preset_Steam_Positive4, 95, 100, 500, 0));
            rules.Add(new UserScoreRule(GlobalStrings.AutoCatUserScore_Preset_Steam_Positive3, 85, 100, 50, 0));
            rules.Add(new UserScoreRule(GlobalStrings.AutoCatUserScore_Preset_Steam_Positive2, 80, 100, 1, 0));
            rules.Add(new UserScoreRule(GlobalStrings.AutoCatUserScore_Preset_Steam_Positive1, 70, 79, 1, 0));
            rules.Add(new UserScoreRule(GlobalStrings.AutoCatUserScore_Preset_Steam_Mixed, 40, 69, 1, 0));
            rules.Add(new UserScoreRule(GlobalStrings.AutoCatUserScore_Preset_Steam_Negative1, 20, 39, 1, 0));
            rules.Add(new UserScoreRule(GlobalStrings.AutoCatUserScore_Preset_Steam_Negative4, 0, 19, 500, 0));
            rules.Add(new UserScoreRule(GlobalStrings.AutoCatUserScore_Preset_Steam_Negative3, 0, 19, 50, 0));
            rules.Add(new UserScoreRule(GlobalStrings.AutoCatUserScore_Preset_Steam_Negative2, 0, 19, 1, 0));
        }

        public override AutoCat Clone() => new AutoCatUserScore(this);

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

            int score = Db.Games[game.Id].ReviewPositivePercentage;
            int reviews = Db.Games[game.Id].ReviewTotal;
            if (UseWilsonScore && (reviews > 0))
            {
                // calculate the lower bound of the Wilson interval for 95 % confidence
                // see http://www.evanmiller.org/how-not-to-sort-by-average-rating.html
                // $$ w^\pm = \frac{1}{1+\frac{z^2}{n}}
                // \left( \hat p + \frac{z^2}{2n} \pm z \sqrt{ \frac{\hat p (1 - \hat p)}{n} + \frac{z^2}{4n^2} } \right)$$
                // where
                // $\hat p$ is the observed fraction of positive ratings (proportion of successes),
                // $n$ is the total number of ratings (the sample size), and
                // $z$ is the $1-{\frac {\alpha}{2}}$ quantile of a standard normal distribution
                // for 95% confidence, the $z = 1.96$
                double
                    z = 1.96; // normal distribution of (1-(1-confidence)/2), i.e. normal distribution of 0.975 for 95% confidence
                double p = score / 100.0;
                double n = reviews;
                p = Math.Round(100 * (((p + ((z * z) / (2 * n))) -
                                       (z * Math.Sqrt(((p * (1 - p)) + ((z * z) / (4 * n))) / n))) /
                                      (1 + ((z * z) / n))));
                // debug: System.Windows.Forms.MessageBox.Show("score " + score + " of " + reviews + " is\tp = " + p + "\n");
                score = Convert.ToInt32(p);
            }
            string result = (from rule in Rules where CheckRule(rule, score, reviews) select rule.Name)
                .FirstOrDefault();

            if (result != null)
            {
                result = GetProcessedString(result);
                game.AddCategory(Games.GetCategory(result));
            }
            return AutoCatResult.Success;
        }

        private static bool CheckRule(UserScoreRule rule, int score, int reviews) => (score >= rule.MinScore) &&
                                                                                     (score <= rule.MaxScore) &&
                                                                                     (rule.MinReviews <= reviews) &&
                                                                                     ((rule.MaxReviews == 0) ||
                                                                                      (rule.MaxReviews >= reviews));

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
            writer.WriteElementString(XmlNameUseWilsonScore, UseWilsonScore.ToString());

            foreach (UserScoreRule rule in Rules)
            {
                writer.WriteStartElement(XmlNameRule);
                writer.WriteElementString(XmlNameRuleText, rule.Name);
                writer.WriteElementString(XmlNameRuleMinScore, rule.MinScore.ToString());
                writer.WriteElementString(XmlNameRuleMaxScore, rule.MaxScore.ToString());
                writer.WriteElementString(XmlNameRuleMinReviews, rule.MinReviews.ToString());
                writer.WriteElementString(XmlNameRuleMaxReviews, rule.MaxReviews.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public static AutoCatUserScore LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlNamePrefix], string.Empty);
            bool useWilsonScore = XmlUtil.GetBoolFromNode(xElement[XmlNameUseWilsonScore], false);

            List<UserScoreRule> rules = new List<UserScoreRule>();
            if (rules.Count > 0)
            {
                XmlNodeList selectNodes = xElement.SelectNodes(XmlNameRule);
                if (selectNodes != null)
                {
                    int nodesCount = selectNodes.Count;
                    for (int i = 0; i < nodesCount; i++)
                    {
                        XmlNodeList xmlNodeList = selectNodes;
                        {
                            XmlNode node = xmlNodeList[i];
                            string ruleName = XmlUtil.GetStringFromNode(node[XmlNameRuleText], string.Empty);
                            int ruleMin = XmlUtil.GetIntFromNode(node[XmlNameRuleMinScore], 0);
                            int ruleMax = XmlUtil.GetIntFromNode(node[XmlNameRuleMaxScore], 100);
                            int ruleMinRev = XmlUtil.GetIntFromNode(node[XmlNameRuleMinReviews], 0);
                            int ruleMaxRev = XmlUtil.GetIntFromNode(node[XmlNameRuleMaxReviews], 0);
                            rules.Add(new UserScoreRule(ruleName, ruleMin, ruleMax, ruleMinRev, ruleMaxRev));
                        }
                    }
                }

                AutoCatUserScore result = new AutoCatUserScore(name, filter, prefix, useWilsonScore) {Rules = rules};
                return result;
            }

            return null;
        }
    }
}