using System;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer.AutoCats
{
    public class AutoCatPlatform : AutoCat
    {
        #region Constants

        public const string TypeIdString = "AutoCatPlatform";

        #endregion

        #region Constructors and Destructors

        public AutoCatPlatform(string name, string filter = null, string prefix = null, bool windows = false, bool mac = false, bool linux = false, bool steamOS = false, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;
            Windows = windows;
            Mac = mac;
            Linux = linux;
            SteamOS = steamOS;
            Selected = selected;
        }

        protected AutoCatPlatform(AutoCatPlatform other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            Windows = other.Windows;
            Mac = other.Mac;
            Linux = other.Linux;
            SteamOS = other.SteamOS;
            Selected = other.Selected;
        }

        /// <summary>
        ///     Empty constructor for XmlSerializer.
        /// </summary>
        private AutoCatPlatform() { }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public override AutoCatType AutoCatType => AutoCatType.Platform;

        public bool Linux { get; set; }

        public bool Mac { get; set; }

        public bool SteamOS { get; set; }

        public bool Windows { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
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

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            if (!db.Contains(game.Id, out DatabaseEntry entry) || entry.LastStoreScrape == 0)
            {
                return AutoCatResult.NotInDatabase;
            }

            AppPlatforms platforms = entry.Platforms;
            if (Windows && platforms.HasFlag(AppPlatforms.Windows))
            {
                game.AddCategory(games.GetCategory(GetCategoryName("Windows")));
            }

            if (Mac && platforms.HasFlag(AppPlatforms.Mac))
            {
                game.AddCategory(games.GetCategory(GetCategoryName("Mac")));
            }

            if (Linux && platforms.HasFlag(AppPlatforms.Linux))
            {
                game.AddCategory(games.GetCategory(GetCategoryName("Linux")));
            }

            if (Linux && platforms.HasFlag(AppPlatforms.Linux))
            {
                game.AddCategory(games.GetCategory(GetCategoryName("SteamOS")));
            }

            return AutoCatResult.Success;
        }

        /// <inheritdoc />
        public override AutoCat Clone()
        {
            return new AutoCatPlatform(this);
        }

        #endregion
    }
}
