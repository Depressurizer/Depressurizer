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
using Depressurizer.Enums;
using Newtonsoft.Json;
using Rallion;

namespace Depressurizer
{
	internal enum GameListSource
	{
		XmlPreferred,

		XmlOnly,

		WebsiteOnly
	}

	internal sealed class Settings
	{
		#region Static Fields

		private static readonly object SyncRoot = new object();

		private static volatile Settings _instance;

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

		private int _height;

		private bool _includeImputedTimes = true;

		private InterfaceLanguage _interfaceLanguage = InterfaceLanguage.English;

		private GameListSource _listSource = GameListSource.XmlPreferred;

		private int _logBackups = 1;

		private LoggerLevel _logLevel = LoggerLevel.Info;

		private int _logSize = 2000000;

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

		private StoreLanguage _storeLanguage = StoreLanguage.English;

		private bool _updateAppInfoOnStart = true;

		private bool _updateHltbOnStart = true;

		private int _width;

		private int _x;

		private int _y;

		#endregion

		#region Constructors and Destructors

		private Settings()
		{
		}

		#endregion

		#region Public Properties

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

		public int Height
		{
			get
			{
				lock (SyncRoot)
				{
					if (_height <= 350)
					{
						_height = 600;
					}

					return _height;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_height = value;
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

		public int LogBackups
		{
			get
			{
				lock (SyncRoot)
				{
					return _logBackups;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					Program.Logger.MaxBackup = value;

					_logBackups = value;
				}
			}
		}

		public LoggerLevel LogLevel
		{
			get
			{
				lock (SyncRoot)
				{
					return _logLevel;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					Program.Logger.Level = value;
					_logLevel = value;
				}
			}
		}

		public int LogSize
		{
			get
			{
				lock (SyncRoot)
				{
					return _logSize;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					Program.Logger.MaxFileSize = value;
					_logSize = value;
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

		public StoreLanguage StoreLanguage
		{
			get
			{
				lock (SyncRoot)
				{
					return _storeLanguage;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_storeLanguage = value;
					if (Program.Database != null)
					{
						Program.Database.ChangeLanguage(_storeLanguage);
					}
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

		public int Width
		{
			get
			{
				lock (SyncRoot)
				{
					if (_width <= 600)
					{
						_width = 1000;
					}

					return _width;
				}
			}
			set
			{
				lock (SyncRoot)
				{
					_width = value;
				}
			}
		}

		public int X
		{
			get
			{
				lock (SyncRoot)
				{
					return _x;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_x = value;
				}
			}
		}

		public int Y
		{
			get
			{
				lock (SyncRoot)
				{
					return _y;
				}
			}

			set
			{
				lock (SyncRoot)
				{
					_y = value;
				}
			}
		}

		#endregion

		#region Public Methods and Operators

		public void Load()
		{
			Load("settings.json");
		}

		public void Load(string path)
		{
			lock (SyncRoot)
			{
				Program.Logger.Write(LoggerLevel.Info, "Settings: Loading a settings instance from '{0}'", path);
				if (!File.Exists(path))
				{
					Program.Logger.Write(LoggerLevel.Warning, "Settings: Couldn't find a settings instance at '{0}'", path);

					return;
				}

				string settings = File.ReadAllText(path);
				_instance = JsonConvert.DeserializeObject<Settings>(settings);
			}
		}

		public void Save()
		{
			Save("settings.json");
		}

		public void Save(string path)
		{
			lock (SyncRoot)
			{
				Program.Logger.Write(LoggerLevel.Info, "Settings: Saving current setttings instance to '{0}'", path);

				string settings = JsonConvert.SerializeObject(_instance);
				File.WriteAllText(path, settings);
			}
		}

		#endregion

		#region Methods

		private static void ChangeLanguage(InterfaceLanguage language)
		{
			Thread.CurrentThread.CurrentUICulture = Utility.GetCulture(language);
		}

		#endregion
	}
}