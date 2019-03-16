using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;
using Depressurizer.Properties;

namespace Depressurizer.AutoCats
{
    public class AutoCatCurator : AutoCat
    {
        #region Constants

        // Serialization constants
        public const string TypeIdString = "AutoCatCurator";

        #endregion

        #region Fields

        private Dictionary<int, CuratorRecommendation> curatorRecommendations;

        #endregion

        #region Constructors and Destructors

        public AutoCatCurator(string name, string filter = null, string categoryName = null, string curatorUrl = null, List<CuratorRecommendation> includedRecommendations = null, bool selected = false) : base(name)
        {
            Filter = filter;
            CategoryName = categoryName;
            CuratorUrl = curatorUrl;
            IncludedRecommendations = includedRecommendations ?? new List<CuratorRecommendation>();
            Selected = selected;
        }

        protected AutoCatCurator(AutoCatCurator other) : base(other)
        {
            Filter = other.Filter;
            CategoryName = other.CategoryName;
            CuratorUrl = other.CuratorUrl;
            IncludedRecommendations = other.IncludedRecommendations ?? new List<CuratorRecommendation>();
            Selected = other.Selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatCurator() { }

        #endregion

        #region Public Properties

        public override AutoCatType AutoCatType => AutoCatType.Curator;

        // AutoCat configuration
        public string CategoryName { get; set; }

        public string CuratorUrl { get; set; }

        [XmlArray("Recommendations")]
        [XmlArrayItem("Recommendation")]
        public List<CuratorRecommendation> IncludedRecommendations { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (game == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GameNull);
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

            if (curatorRecommendations.ContainsKey(game.Id) && IncludedRecommendations.Contains(curatorRecommendations[game.Id]))
            {
                string typeName = Utility.GetEnumDescription(curatorRecommendations[game.Id]);
                Category c = games.GetCategory(GetProcessedString(typeName));
                game.AddCategory(c);
            }

            return AutoCatResult.Success;
        }

        /// <inheritdoc />
        public override AutoCat Clone()
        {
            return new AutoCatCurator(this);
        }

        /// <inheritdoc />
        public override void PreProcess(GameList games, Database db)
        {
            this.games = games;
            this.db = db;

            GetRecommendations();
        }

        #endregion

        #region Methods

        private string GetProcessedString(string type)
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
                return type;
            }

            return CategoryName.Replace("{type}", type);
        }

        private void GetRecommendations()
        {
            Regex curatorIdRegex = new Regex(@"(?:https?://)?store.steampowered.com/curator/(\d+)([^\/]*)/?", RegexOptions.Singleline | RegexOptions.Compiled);
            Match m = curatorIdRegex.Match(CuratorUrl);
            if (!m.Success || !long.TryParse(m.Groups[1].Value, out long curatorId))
            {
                Logger.Error($"Failed to parse curator id from url {CuratorUrl}.");
                MessageBox.Show(string.Format(GlobalStrings.AutocatCurator_CuratorIdParsing_Error, CuratorUrl), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (GetCuratorRecommendationsDlg dialog = new GetCuratorRecommendationsDlg(curatorId))
            {
                DialogResult result = dialog.ShowDialog();

                if (dialog.Error != null)
                {
                    Logger.Error(GlobalStrings.AutocatCurator_GetRecommendations_Error, dialog.Error.Message);
                    MessageBox.Show(string.Format(GlobalStrings.AutocatCurator_GetRecommendations_Error, dialog.Error.Message), Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (result != DialogResult.Cancel && result != DialogResult.Abort)
                {
                    curatorRecommendations = dialog.CuratorRecommendations;
                }
            }
        }

        #endregion
    }
}
