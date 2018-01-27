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
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Depressurizer;
using Rallion;

namespace Depressurizer
{
    public class AutoCatCurator : AutoCat
    {
        public override AutoCatType AutoCatType
        {
            get { return AutoCatType.Curator; }
        }

        // AutoCat configuration
        public string CategoryName { get; set; }

        public string CuratorUrl { get; set; }

        [XmlArray("Recommendations"), XmlArrayItem("Recommendation")]
        public List<CuratorRecommendation> IncludedRecommendations { get; set; }

        private Dictionary<int, CuratorRecommendation> curatorRecommendations;


        // Serialization constants
        public const string TypeIdString = "AutoCatCurator";
            
        public AutoCatCurator(string name, string filter = null, string categoryName = null, string curatorUrl = null, List<CuratorRecommendation> includedRecommendations = null,
            bool selected = false)
            : base(name)
        {
            Filter = filter;
            CategoryName = categoryName;
            CuratorUrl = curatorUrl;
            IncludedRecommendations = includedRecommendations == null
                ? new List<CuratorRecommendation>()
                : includedRecommendations;
            Selected = selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatCurator() { }

        protected AutoCatCurator(AutoCatCurator other)
            : base(other)
        {
            Filter = other.Filter;
            CategoryName = other.CategoryName;
            CuratorUrl = other.CuratorUrl;
            IncludedRecommendations = other.IncludedRecommendations == null
                ? new List<CuratorRecommendation>()
                : other.IncludedRecommendations;
            Selected = other.Selected;
        }

        public override AutoCat Clone()
        {
            return new AutoCatCurator(this);
        }

        public override void PreProcess(GameList games, GameDB db)
        {
            this.games = games;
            this.db = db;

            GetRecommendations();

        }

        private void GetRecommendations()
        {
            Regex curatorIdRegex = new Regex(@"(?:https?://)?store.steampowered.com/curator/(\d+)([^\/]*)/?",
                RegexOptions.Singleline | RegexOptions.Compiled);
            Match m = curatorIdRegex.Match(CuratorUrl);
            if (!m.Success || !long.TryParse(m.Groups[1].Value, out long curatorId))
            {
                Program.Logger.Write(LoggerLevel.Error, $"Failed to parse curator id from url {CuratorUrl}.");
                MessageBox.Show(string.Format(GlobalStrings.AutocatCurator_CuratorIdParsing_Error, CuratorUrl),
                    GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GetCuratorRecommendationsDlg dlg = new GetCuratorRecommendationsDlg(curatorId);
            DialogResult res = dlg.ShowDialog();

            if (dlg.Error != null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.AutocatCurator_GetRecommendations_Error, dlg.Error.Message);
                MessageBox.Show(string.Format(GlobalStrings.AutocatCurator_GetRecommendations_Error, dlg.Error.Message),
                    GlobalStrings.Gen_Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if ((res != DialogResult.Cancel) && (res != DialogResult.Abort))
            {

                curatorRecommendations = dlg.CuratorRecommendations;
            }
        }

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }
            if (game == null)
            {
                Program.Logger.Write(LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (curatorRecommendations == null || curatorRecommendations.Count == 0)
            {
                return AutoCatResult.Failure;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            if (curatorRecommendations.ContainsKey(game.Id) &&
                IncludedRecommendations.Contains(curatorRecommendations[game.Id]))
            {
                string typeName = Utility.GetEnumDescription(curatorRecommendations[game.Id]);
                Category c = games.GetCategory(GetProcessedString(typeName));
                game.AddCategory(c);
            }
            return AutoCatResult.Success;
        }

        private string GetProcessedString(string type)
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                return CategoryName.Replace("{type}",type);
            }
            return type;
        }
    }
}