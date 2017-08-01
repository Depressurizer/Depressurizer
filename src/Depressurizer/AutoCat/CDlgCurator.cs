/*
This file is part of Depressurizer.
Original Work Copyright 2011, 2012, 2013 Steve Labbe.
Modified Work Copyright 2017 Theodoros Dimos.


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
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Depressurizer.Properties;
using Newtonsoft.Json.Linq;
using Rallion;

namespace Depressurizer
{
    public enum CuratorRecommendation
    {
        [Description("Error")] Error,
        [Description("Recommended")] Recommended,
        [Description("Not Recommended")] NotRecommended,
        [Description("Informational")] Informational
    }

    class GetCuratorRecommendationsDlg : CancelableDlg
    {
        private long curatorId;
        public Dictionary<int, CuratorRecommendation> CuratorRecommendations;
        public int TotalCount;

        public GetCuratorRecommendationsDlg(long curatorId)
            : base(GlobalStrings.CDlgCurator_GettingRecommendations, false)
        {
            SetText(GlobalStrings.CDlgCurator_GettingRecommendations);
            this.curatorId = curatorId;
            CuratorRecommendations = new Dictionary<int, CuratorRecommendation>();
        }

        protected override void RunProcess()
        {
            string json;

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                json = wc.DownloadString(string.Format(Resources.UrlSteamCuratorRecommendations, curatorId, 0));
            }

            JObject parsedJson = JObject.Parse(json);
            if (int.TryParse(parsedJson["total_count"].ToString(), out TotalCount))
            {
                SetText(GlobalStrings.CDlgCurator_GettingRecommendations + " " + String.Format(GlobalStrings.CDlg_Progress, 0, TotalCount));
                string resultsHtml = parsedJson["results_html"].ToString();
                CuratorRecommendations = CuratorRecommendations.Union(GetCuratorRecommendationsFromPage(resultsHtml))
                    .ToDictionary(k => k.Key, v => v.Value);
                for (int currentPosition = 50; currentPosition < TotalCount; currentPosition += 50)
                {
                    SetText(GlobalStrings.CDlgCurator_GettingRecommendations + " " + String.Format(GlobalStrings.CDlg_Progress, currentPosition, TotalCount));
                    using (WebClient wc = new WebClient())
                    {
                        wc.Encoding = Encoding.UTF8;
                        json = wc.DownloadString(string.Format(Resources.UrlSteamCuratorRecommendations, curatorId,
                            currentPosition));
                    }
                    parsedJson = JObject.Parse(json);
                    resultsHtml = parsedJson["results_html"].ToString();
                    CuratorRecommendations = CuratorRecommendations
                        .Union(GetCuratorRecommendationsFromPage(resultsHtml)).ToDictionary(k => k.Key, v => v.Value);
                }
            }
            else { Program.Logger.Write(LoggerLevel.Error, "Error: CDlgCurator: Couldn't determine total count of recommendations"); }
            if (CuratorRecommendations.Count != TotalCount)
            {
                Program.Logger.Write(LoggerLevel.Error, "Error: CDlgCurator: Count of recommendations retrieved is different than expected");
            }
            else { Program.Logger.Write(LoggerLevel.Error, String.Format("Retrieved {0} curator recommendations.", TotalCount)); }
            OnThreadCompletion();
        }

        protected override void Finish()
        {
            if (!Canceled  && CuratorRecommendations.Count>0 && Error == null)
            {
                OnJobCompletion();
            }
        }

        /// <summary>
        /// Retrieves all curator recomendations in selected string
        /// </summary>
        /// <param name="page">The results_html json node you get from http://store.steampowered.com/curators/ajaxgetcuratorrecommendations/{0}/?query=&amp;start={1}&amp;count=50</param>
        /// <returns>A dictionary containing ids of games and their respective recommendations</returns>
        private static Dictionary<int, CuratorRecommendation> GetCuratorRecommendationsFromPage(string page)
        {
            Dictionary<int, CuratorRecommendation> curatorRecommendations =
                new Dictionary<int, CuratorRecommendation>();
            Regex curatorRegex = new Regex(@"data-ds-appid=\""(\d+)\"".*?><span class='color_([^']*)",
                RegexOptions.Singleline | RegexOptions.Compiled);
            MatchCollection matches = curatorRegex.Matches(page);
            if (matches.Count > 0)
            {
                foreach (Match ma in matches)
                {
                    CuratorRecommendation recommendation;
                    switch (ma.Groups[2].Value)
                    {
                        case "recommended":
                            recommendation = CuratorRecommendation.Recommended;
                            break;
                        case "not_recommended":
                            recommendation = CuratorRecommendation.NotRecommended;
                            break;
                        case "informational":
                            recommendation = CuratorRecommendation.Informational;
                            break;
                        default:
                            recommendation = CuratorRecommendation.Error;
                            break;
                    }
                    if (int.TryParse(ma.Groups[1].Value, out int id) && recommendation != CuratorRecommendation.Error)
                    {
                        curatorRecommendations.Add(id, recommendation);
                        Program.Logger.Write(LoggerLevel.Verbose, "Retrieved recommendation for game " + id + ": " + ma.Groups[2].Value);
                    }
                    if (recommendation == CuratorRecommendation.Error)
                    {
                        Program.Logger.Write(LoggerLevel.Error, "Error: For game " + id + ": recommendation recognized as \"" + ma.Groups[2].Value + '"');
                    }
                }
            }
            return curatorRecommendations;
        }
    }
}