#region License

//     This file (ScrapeDialog.cs) is part of Depressurizer.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Depressurizer.Models;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Depressurizer.Dialogs
{
	internal partial class ScrapeDialog : MaterialForm
	{
		#region Static Fields

		private static readonly object SyncRoot = new object();

		#endregion

		#region Fields

		private readonly List<int> _jobs;

		private readonly List<DatabaseEntry> _results = new List<DatabaseEntry>();

		private bool _canceled = false;

		private DateTime _start;

		private bool _stopped = false;

		#endregion

		#region Constructors and Destructors

		public ScrapeDialog(IEnumerable<int> appids)
		{
			InitializeComponent();
			MaterialSkinManager.AddFormToManage(this);

			_jobs = appids.Distinct().Where(i => i > 0).ToList();
			TotalJobs = _jobs.Count;
		}

		#endregion

		#region Delegates

		private delegate void SimpleDelegate();

		private delegate void TextUpdateDelegate(string s);

		#endregion

		#region Public Properties

		public int CompletedJobs { get; private set; }

		public Exception Error { get; private set; }

		public int TotalJobs { get; }

		#endregion

		#region Properties

		private static Database Database => Database.Instance;

		private static MaterialSkinManager MaterialSkinManager => MaterialSkinManager.Instance;

		#endregion

		#region Methods

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			lock (SyncRoot)
			{
				_stopped = true;
				_canceled = true;
			}
		}

		private void ButtonStop_Click(object sender, EventArgs e)
		{
			lock (SyncRoot)
			{
				_stopped = true;
			}
		}

		private void Finish()
		{
			lock (SyncRoot)
			{
				if (_canceled)
				{
					return;
				}

				foreach (DatabaseEntry entry in _results)
				{
					if (Database.Contains(entry.Id, out DatabaseEntry databaseEntry))
					{
						databaseEntry.MergeIn(entry);
					}
					else
					{
						Database.Add(entry);
					}
				}
			}
		}

		private void OnJobCompletion()
		{
			lock (SyncRoot)
			{
				CompletedJobs++;
			}

			UpdateText();
		}

		private void RunJob(int appId)
		{
			lock (SyncRoot)
			{
				if (_stopped)
				{
					return;
				}
			}

			DatabaseEntry entry = new DatabaseEntry(appId);
			entry.ScrapeStore();

			if (entry.LastStoreScrape == 0)
			{
				entry.ScrapeTrueSteamAchievements();
			}

			lock (SyncRoot)
			{
				if (_stopped)
				{
					return;
				}

				_results.Add(entry);
				OnJobCompletion();
			}
		}

		private void ScrapeDialog_Load(object sender, EventArgs e)
		{
			_start = DateTime.UtcNow;
			Start();
		}

		private void SetText(string s)
		{
			if (InvokeRequired)
			{
				Invoke(new TextUpdateDelegate(SetText), s);
			}
			else
			{
				LabelStatus.Text = s;
			}
		}

		private void Start()
		{
			try
			{
				Task.Run(() =>
				{
					Parallel.ForEach(_jobs, RunJob);
				});
			}
			catch (Exception e)
			{
				lock (SyncRoot)
				{
					_stopped = true;
					Error = e;
				}

				if (IsHandleCreated)
				{
					Invoke(new SimpleDelegate(Finish));
					Invoke(new SimpleDelegate(Close));
				}
			}
		}

		private void UpdateText()
		{
			TimeSpan timeRemaining = TimeSpan.Zero;
			if (CompletedJobs > 0)
			{
				double msElapsed = (DateTime.UtcNow - _start).TotalMilliseconds;
				double msPerItem = msElapsed / CompletedJobs;
				double msRemaining = msPerItem * (TotalJobs - CompletedJobs);
				timeRemaining = TimeSpan.FromMilliseconds(msRemaining);
			}

			StringBuilder sb = new StringBuilder();
			sb.Append(string.Format(GlobalStrings.CDlgDataScrape_UpdatingComplete, CompletedJobs, TotalJobs));

			sb.Append(GlobalStrings.CDlgDataScrape_TimeRemaining);
			if (timeRemaining == TimeSpan.Zero)
			{
				sb.Append(GlobalStrings.CDlgScrape_Unknown);
			}
			else if (timeRemaining.TotalMinutes < 1.0)
			{
				sb.Append(GlobalStrings.CDlgScrape_1minute);
			}
			else
			{
				double hours = timeRemaining.TotalHours;
				if (hours >= 1.0)
				{
					sb.Append(string.Format("{0:F0}h", hours));
				}

				sb.Append(string.Format("{0:D2}m", timeRemaining.Minutes));
			}

			SetText(sb.ToString());
		}

		#endregion
	}
}
