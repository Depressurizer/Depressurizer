#region LICENSE

//     This file (Settings.cs) is part of DepressurizerCore.
//     Copyright (C) 2018  Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <https://www.gnu.org/licenses/>.

#endregion

using System;
using System.Globalization;
using System.IO;
using System.Threading;
using DepressurizerCore.Helpers;
using Newtonsoft.Json;

namespace DepressurizerCore
{
	/// <summary>
	///     Action on startup
	/// </summary>
	public enum StartupAction
	{
		/// <summary>
		///     Load a profile
		/// </summary>
		Load,

		/// <summary>
		///     Create a profile
		/// </summary>
		Create
	}

	/// <summary>
	///     Languages supported on Steam.
	/// </summary>
	/// <remarks>
	///     https://partner.steamgames.com/doc/store/localization
	///     https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx
	/// </remarks>
	public enum StoreLanguage
	{
		/// <summary>
		///     Equal to the Depressurizer Interface language or English
		/// </summary>
		Default,

		// TODO: Arabic

		/// <summary>
		///     Bulgarian - Bulgaria
		/// </summary>
		Bulgarian,

		// TODO: Chinese (Simplified)

		// TODO: Chinese (Traditional)

		/// <summary>
		///     Czech - Czech Republic
		/// </summary>
		Czech,

		/// <summary>
		///     Danish - Denmark
		/// </summary>
		Danish,

		/// <summary>
		///     Dutch - The Netherlands
		/// </summary>
		Dutch,

		/// <summary>
		///     English - United States
		/// </summary>
		English,

		/// <summary>
		///     Finnish - Finland
		/// </summary>
		Finnish,

		/// <summary>
		///     French - France
		/// </summary>
		French,

		/// <summary>
		///     German - Germany
		/// </summary>
		German,

		/// <summary>
		///     Greek - Greece
		/// </summary>
		Greek,

		/// <summary>
		///     Hungarian - Hungary
		/// </summary>
		Hungarian,

		/// <summary>
		///     Italian - Italy
		/// </summary>
		Italian,

		/// <summary>
		///     Japanese - Japan
		/// </summary>
		Japanese,

		/// <summary>
		///     Korean - Korea
		/// </summary>
		Korean,

		// TODO: Norwegian

		/// <summary>
		///     Polish - Poland
		/// </summary>
		Polish,

		/// <summary>
		///     Portuguese - Portugal
		/// </summary>
		Portuguese,

		// TODO: Portuguese-Brazil

		/// <summary>
		///     Romanian - Romania
		/// </summary>
		Romanian,

		/// <summary>
		///     Russian - Russia
		/// </summary>
		Russian,

		/// <summary>
		///     Spanish - Spain
		/// </summary>
		Spanish,

		/// <summary>
		///     Swedish - Sweden
		/// </summary>
		Swedish,

		/// <summary>
		///     Thai - Thailand
		/// </summary>
		Thai,

		/// <summary>
		///     Turkish - Turkey
		/// </summary>
		Turkish,

		/// <summary>
		///     Ukrainian - Ukraine
		/// </summary>
		Ukrainian
	}

	/// <summary>
	///     Depressurizer Interface Languages
	/// </summary>
	/// <remarks>
	///     Format: https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx
	/// </remarks>
	public enum InterfaceLanguage
	{
		/// <summary>
		///     English - United States
		/// </summary>
		English,

		/// <summary>
		///     Spanish - Spain
		/// </summary>
		Spanish,

		/// <summary>
		///     Russian - Russia
		/// </summary>
		Russian,

		/// <summary>
		///     Ukrainian - Ukraine
		/// </summary>
		Ukranian,

		/// <summary>
		///     Dutch - The Netherlands
		/// </summary>
		Dutch
	}

	public enum GameListSource
	{
		XmlPreferred,
		XmlOnly,
		WebsiteOnly
	}

	/// <summary>
	///     Settings Controller
	/// </summary>
	public sealed class Settings
	{
		#region Static Fields

		private static readonly object SyncRoot = new object();

		private static volatile Settings _instance;

		#endregion

		#region Fields

		private InterfaceLanguage _interfaceLanguage = InterfaceLanguage.English;

		private int _splitBrowser;
		private int _splitContainer;
		private int _splitGame;
		private StoreLanguage _storeLang = StoreLanguage.Default;

		#endregion

		#region Constructors and Destructors

		private Settings() { }

		#endregion

		#region Public Properties

		/// <summary>
		///     Settings Instance
		/// </summary>
		public static Settings Instance
		{
			get
			{
				if (_instance != null)
				{
					return _instance;
				}

				lock (SyncRoot)
				{
					if (_instance == null)
					{
						_instance = new Settings();
					}
				}

				return _instance;
			}
		}

		/// <summary>
		///     Automatically Save Database
		/// </summary>
		public bool AutoSaveDatabase { get; set; } = true;

		/// <summary>
		///     Check for Depressurizer updates on startup
		/// </summary>
		public bool CheckForUpdates { get; set; } = true;

		/// <summary>
		///     Num of backups to keep of the Steam config files
		/// </summary>
		public int ConfigBackupCount { get; set; } = 3;

		public bool IncludeImputedTimes { get; set; } = true;

		/// <summary>
		///     Interface Language
		/// </summary>
		public InterfaceLanguage InterfaceLanguage
		{
			get => _interfaceLanguage;
			set => _interfaceLanguage = ChangeLanguage(value);
		}

		public string ListGamesState { get; set; } = null;

		public GameListSource ListSource { get; set; } = GameListSource.XmlPreferred;

		/// <summary>
		///     On start update from AppInfo
		/// </summary>
		public bool OnStartUpdateFromAppInfo { get; set; } = true;

		/// <summary>
		///     On start update from HowLongToBeat.com
		/// </summary>
		public bool OnStartUpdateFromHLTB { get; set; } = true;

		/// <summary>
		///     Steam profile to Load
		/// </summary>
		public string ProfileToLoad { get; set; }

		public bool RemoveExtraEntries { get; set; }

		public int ScrapePromptDays { get; set; } = 30;

		/// <summary>
		///     Selected AutoCats
		/// </summary>
		public string SelectedAutoCats { get; set; } = null;

		/// <summary>
		///     Selected Category
		/// </summary>
		public string SelectedCategory { get; set; } = null;

		/// <summary>
		///     Selected Filter
		/// </summary>
		public string SelectedFilter { get; set; } = null;

		public bool SingleCatMode { get; set; } = false;

		public int SplitBrowser
		{
			get
			{
				if (_splitBrowser <= 100)
				{
					SplitBrowser = SplitBrowserContainerWidth - 300;
				}

				return _splitBrowser;
			}
			set => _splitBrowser = value;
		}

		public int SplitBrowserContainerWidth { get; set; } = 722;

		public int SplitContainer
		{
			get
			{
				if (_splitContainer <= 100)
				{
					SplitContainer = 250;
				}

				return _splitContainer;
			}
			set => _splitContainer = value;
		}

		public int SplitGame
		{
			get
			{
				if (_splitGame <= 100)
				{
					SplitGame = SplitGameContainerHeight - 150;
				}

				return _splitGame;
			}
			set => _splitGame = value;
		}

		public int SplitGameContainerHeight { get; set; } = 510;

		/// <summary>
		///     Action On Startup
		/// </summary>
		public StartupAction StartupAction { get; set; } = StartupAction.Create;

		/// <summary>
		///     Steam installation path
		/// </summary>
		public string SteamPath { get; set; } = null;

		/// <summary>
		///     Steam Store language, used for scraping
		/// </summary>
		public StoreLanguage StoreLanguage
		{
			get
			{
				if (_storeLang == StoreLanguage.Default)
				{
					_storeLang = (StoreLanguage) Enum.Parse(typeof(StoreLanguage), InterfaceLanguage.ToString(), true);
				}

				return _storeLang;
			}
			set => _storeLang = value;
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		///     Load Settings from the default location
		/// </summary>
		public void Load()
		{
			Load(Location.File.Settings);
		}

		/// <summary>
		///     Load Settings from the specified location
		/// </summary>
		/// <param name="path">Path to load from</param>
		public void Load(string path)
		{
			lock (SyncRoot)
			{
				try
				{
					if (!File.Exists(path))
					{
						return;
					}

					string settings = File.ReadAllText(path);
					_instance = JsonConvert.DeserializeObject<Settings>(settings);
				}
				catch (Exception e)
				{
					SentryLogger.Log(e);
					throw;
				}
			}
		}

		/// <summary>
		///     Save Settings to the default location
		/// </summary>
		public void Save()
		{
			Save(Location.File.Settings);
		}

		/// <summary>
		///     Save Settings to the default location
		/// </summary>
		/// <param name="path">Path to save to</param>
		public void Save(string path)
		{
			lock (SyncRoot)
			{
				try
				{
					string settings = JsonConvert.SerializeObject(_instance);
					File.WriteAllText(path, settings);
				}
				catch (Exception e)
				{
					SentryLogger.Log(e);
					throw;
				}
			}
		}

		#endregion

		#region Methods

		private static InterfaceLanguage ChangeLanguage(InterfaceLanguage interfaceLanguage)
		{
			CultureInfo newCulture = Thread.CurrentThread.CurrentUICulture;

			try
			{
				switch (interfaceLanguage)
				{
					case InterfaceLanguage.English:
						newCulture = new CultureInfo("en");

						break;
					case InterfaceLanguage.Spanish:
						newCulture = new CultureInfo("esS");

						break;
					case InterfaceLanguage.Russian:
						newCulture = new CultureInfo("ru");

						break;
					case InterfaceLanguage.Ukranian:
						newCulture = new CultureInfo("uk");

						break;
					case InterfaceLanguage.Dutch:
						newCulture = new CultureInfo("nl");

						break;
					default:

						throw new ArgumentOutOfRangeException(nameof(interfaceLanguage), interfaceLanguage, null);
				}
			}
			catch (Exception e)
			{
				SentryLogger.Log(e);
			}

			Thread.CurrentThread.CurrentUICulture = newCulture;

			return interfaceLanguage;
		}

		#endregion
	}
}