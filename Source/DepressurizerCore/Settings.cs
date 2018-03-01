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
		///     Load a Depressurizer profile
		/// </summary>
		LoadProfile,

		/// <summary>
		///     Create a Depressurizer profile
		/// </summary>
		CreateProfile
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

		/// <summary>
		///     Arabic	العربية	arabic	ar
		/// </summary>
		Arabic,

		/// <summary>
		///     Bulgarian	български език	bulgarian	bg
		/// </summary>
		Bulgarian,

		/// <summary>
		///     Chinese (Simplified)	简体中文	schinese	zh-CN
		/// </summary>
		Schinese,

		/// <summary>
		///     Chinese (Traditional)	繁體中文	tchinese	zh-TW
		/// </summary>
		Tchinese,

		/// <summary>
		///     Czech	čeština	czech	cs
		/// </summary>
		Czech,

		/// <summary>
		///     Danish	Dansk	danish	da
		/// </summary>
		Danish,

		/// <summary>
		///     Dutch	Nederlands	dutch	nl
		/// </summary>
		Dutch,

		/// <summary>
		///     English	English	english	en
		/// </summary>
		English,

		/// <summary>
		///     Finnish	Suomi	finnish	fi
		/// </summary>
		Finnish,

		/// <summary>
		///     French	Français	french	fr
		/// </summary>
		French,

		/// <summary>
		///     German	Deutsch	german	de
		/// </summary>
		German,

		/// <summary>
		///     Greek	Ελληνικά	greek	el
		/// </summary>
		Greek,

		/// <summary>
		///     Hungarian	Magyar	hungarian	hu
		/// </summary>
		Hungarian,

		/// <summary>
		///     Italian	Italiano	italian	it
		/// </summary>
		Italian,

		/// <summary>
		///     Japanese	日本語	japanese	ja
		/// </summary>
		Japanese,

		/// <summary>
		///     Korean	한국어	koreana	ko
		/// </summary>
		Koreana,

		/// <summary>
		///     Norwegian	Norsk	norwegian	no
		/// </summary>
		Norwegian,

		/// <summary>
		///     Polish	Polski	polish	pl
		/// </summary>
		Polish,

		/// <summary>
		///     Portuguese	Português	portuguese	pt
		/// </summary>
		Portuguese,

		/// <summary>
		///     Portuguese-Brazil	Português-Brasil	brazilian	pt-BR
		/// </summary>
		Brazilian,

		/// <summary>
		///     Romanian	Română	romanian	ro
		/// </summary>
		Romanian,

		/// <summary>
		///     Russian	Русский	russian	ru
		/// </summary>
		Russian,

		/// <summary>
		///     Spanish	Español	spanish	es
		/// </summary>
		Spanish,

		/// <summary>
		///     Swedish	Svenska	swedish	sv
		/// </summary>
		Swedish,

		/// <summary>
		///     Thai	ไทย	thai	th
		/// </summary>
		Thai,

		/// <summary>
		///     Turkish	Türkçe	turkish	tr
		/// </summary>
		Turkish,

		/// <summary>
		///     Ukrainian	Українська	ukrainian	uk
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
		private StoreLanguage _storeLanguage = StoreLanguage.Default;

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

		/// <summary>
		///     Include HowLongToBeat imputed times
		/// </summary>
		public bool IncludeImputedTimes { get; set; } = true;

		/// <summary>
		///     Interface Language
		/// </summary>
		public InterfaceLanguage InterfaceLanguage
		{
			get => _interfaceLanguage;
			set => _interfaceLanguage = ChangeLanguage(value);
		}

		/// <summary>
		///     Last state of ListGames
		/// </summary>
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

		/// <summary>
		///     Re-scrape after X days
		/// </summary>
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

		/// <summary>
		///     Single category mode
		/// </summary>
		public bool SingleCatMode { get; set; } = false;

		/// <summary>
		///     Action On Startup
		/// </summary>
		public StartupAction StartupAction { get; set; } = StartupAction.CreateProfile;

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
				if (_storeLanguage == StoreLanguage.Default)
				{
					_storeLanguage = (StoreLanguage) Enum.Parse(typeof(StoreLanguage), InterfaceLanguage.ToString(), true);
				}

				return _storeLanguage;
			}
			set => _storeLanguage = value;
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
						newCulture = new CultureInfo("es");

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
