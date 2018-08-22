#region License

//     This file (Settings.cs) is part of Depressurizer.
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

using System.IO;
using System.Threading;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Newtonsoft.Json;

namespace Depressurizer.Core
{
	/// <summary>
	///     Depressurizer Settings Controller
	/// </summary>
	public sealed class Settings
	{
		#region Static Fields

		private static readonly object SyncRoot = new object();

		private static volatile Settings _instance;

		// ReSharper disable once InconsistentNaming
		private static Thread CurrentThread;

		#endregion

		#region Fields

		public int SplitBrowserContainerWidth = 722;

		public int SplitGameContainerHeight = 510;

		private string _autocats;

		private bool _autoSaveDatabase = true;

		private string _category;

		private bool _checkForDepressurizerUpdates = true;

		private int _configBackupCount = 3;

		private string _filter;

		private bool _includeImputedTimes = true;

		private InterfaceLanguage _interfaceLanguage = InterfaceLanguage.English;

		private GameListSource _listSource = GameListSource.XmlPreferred;

		private string _lstGamesState = "";

		private string _profileToLoad;

		private bool _removeExtraEntries = true;

		private int _scrapePromptDays = 30;

		private bool _singleCatMode;

		private int _splitBrowser;

		private int _splitContainer;

		private int _splitGame;

		private StartupAction _startupAction = StartupAction.Create;

		private string _steamPath;

		private bool _updateAppInfoOnStart = true;

		private bool _updateHltbOnStart = true;

		#endregion

		#region Constructors and Destructors

		private Settings() { }

		#endregion

		#region Public Properties

		/// <summary>
		///     Depressurizer Settings Instance
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

		public bool AutoSaveDatabase
		{
			get
			{
				lock (SyncRoot)

				{
					return _autoSaveDatabase;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_autoSaveDatabase = value;
				}
			}
		}

		public bool CheckForDepressurizerUpdates
		{
			get
			{
				lock (SyncRoot)
				{
					return _checkForDepressurizerUpdates;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_checkForDepressurizerUpdates = value;
				}
			}
		}

		public int ConfigBackupCount
		{
			get
			{
				lock (SyncRoot)
				{
					return _configBackupCount;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_configBackupCount = value;
				}
			}
		}

		public bool IncludeImputedTimes
		{
			get
			{
				lock (SyncRoot)
				{
					return _includeImputedTimes;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_includeImputedTimes = value;
				}
			}
		}

		public InterfaceLanguage InterfaceLanguage
		{
			get
			{
				lock (SyncRoot)
				{
					return _interfaceLanguage;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_interfaceLanguage = value;
					ChangeLanguage(_interfaceLanguage);
				}
			}
		}

		public GameListSource ListSource
		{
			get
			{
				lock (SyncRoot)
				{
					return _listSource;
				}
			}

			set

			{
				lock (SyncRoot)
				{
					_listSource = value;
				}
			}
		}

		public string LstGamesState
		{
			get
			{
				lock (SyncRoot)
				{
					return _lstGamesState;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_lstGamesState = value;
				}
			}
		}

		public string ProfileToLoad
		{
			get
			{
				lock (SyncRoot)
				{
					return _profileToLoad;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_profileToLoad = value;
				}
			}
		}

		public bool RemoveExtraEntries
		{
			get
			{
				lock (SyncRoot)
				{
					return _removeExtraEntries;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_removeExtraEntries = value;
				}
			}
		}

		public int ScrapePromptDays
		{
			get
			{
				lock (SyncRoot)
				{
					return _scrapePromptDays;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_scrapePromptDays = value;
				}
			}
		}

		public string SelectedAutoCats
		{
			get
			{
				lock (SyncRoot)
				{
					return _autocats;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_autocats = value;
				}
			}
		}

		public string SelectedCategory
		{
			get
			{
				lock (SyncRoot)
				{
					return _category;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_category = value;
				}
			}
		}

		public string SelectedFilter
		{
			get
			{
				lock (SyncRoot)
				{
					return _filter;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_filter = value;
				}
			}
		}

		public bool SingleCatMode
		{
			get
			{
				lock (SyncRoot)
				{
					return _singleCatMode;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_singleCatMode = value;
				}
			}
		}

		public int SplitBrowser
		{
			get
			{
				lock (SyncRoot)
				{
					if (_splitBrowser <= 100)
					{
						_splitBrowser = SplitBrowserContainerWidth - 300;
					}

					return _splitBrowser;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_splitBrowser = value;
				}
			}
		}

		public int SplitContainer
		{
			get
			{
				lock (SyncRoot)
				{
					if (_splitContainer <= 100)
					{
						_splitContainer = 250;
					}

					return _splitContainer;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_splitContainer = value;
				}
			}
		}

		public int SplitGame
		{
			get
			{
				lock (SyncRoot)
				{
					if (_splitGame <= 100)
					{
						_splitGame = SplitGameContainerHeight - 150;
					}

					return _splitGame;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_splitGame = value;
				}
			}
		}

		public StartupAction StartupAction
		{
			get
			{
				lock (SyncRoot)
				{
					return _startupAction;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_startupAction = value;
				}
			}
		}

		public string SteamPath
		{
			get
			{
				lock (SyncRoot)
				{
					return _steamPath;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_steamPath = value;
				}
			}
		}

		public bool UpdateAppInfoOnStart
		{
			get
			{
				lock (SyncRoot)
				{
					return _updateAppInfoOnStart;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_updateAppInfoOnStart = value;
				}
			}
		}

		public bool UpdateHltbOnStart
		{
			get
			{
				lock (SyncRoot)
				{
					return _updateHltbOnStart;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_updateHltbOnStart = value;
				}
			}
		}

		#endregion

		#region Properties

		private static Logger Logger => Logger.Instance;

		#endregion

		#region Public Methods and Operators

		public static void SetThread(Thread thread)
		{
			CurrentThread = thread;
		}

		public void Load()
		{
			Load(Location.File.Settings);
		}

		public void Load(string path)
		{
			lock (SyncRoot)
			{
				Logger.Info("Settings: Loading a settings instance from '{0}'", path);
				if (!File.Exists(path))
				{
					Logger.Warn("Settings: Couldn't find a settings instance at '{0}'", path);

					return;
				}

				using (StreamReader reader = File.OpenText(path))
				{
					JsonSerializer serializer = new JsonSerializer();
					_instance = (Settings) serializer.Deserialize(reader, typeof(Settings));
				}
			}
		}

		public void Save()
		{
			Save(Location.File.Settings);
		}

		public void Save(string path)
		{
			lock (SyncRoot)
			{
				Logger.Info("Settings: Saving current setttings instance to '{0}'", path);

				using (StreamWriter writer = File.CreateText(path))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(writer, _instance);
				}
			}
		}

		#endregion

		#region Methods

		private static void ChangeLanguage(InterfaceLanguage language)
		{
			CurrentThread.CurrentUICulture = Language.GetCultureInfo(language);
		}

		#endregion
	}
}
