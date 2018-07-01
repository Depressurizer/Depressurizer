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
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Depressurizer.Models;

namespace Depressurizer.Dialogs
{
	internal sealed class ScrapeDialog : CancelableDialog
	{
		#region Fields

		private readonly List<int> _jobs;

		private readonly List<DatabaseEntry> _results = new List<DatabaseEntry>();

		private DateTime _start;

		private string _timeLeft;

		#endregion

		#region Constructors and Destructors

		public ScrapeDialog(List<int> jobs) : base(string.Format(CultureInfo.CurrentUICulture, "{0}...", "Scraping"), true)
		{
			_jobs = jobs;
			TotalJobs = jobs.Count;
		}

		#endregion

		#region Properties

		private static Database Database => Database.Instance;

		#endregion

		#region Methods

		protected override void CancelableDialog_Load(object sender, EventArgs e)
		{
			_start = DateTime.UtcNow;
			base.CancelableDialog_Load(sender, e);
		}

		protected override void OnFinish()
		{
			if (Canceled)
			{
				return;
			}

			SetText(string.Format(CultureInfo.CurrentUICulture, "{0}...", "Applying Data"));

			lock (SyncRoot)
			{
				foreach (DatabaseEntry entry in _results)
				{
					if (Database.Contains(entry.Id))
					{
						Database.Games[entry.Id].MergeIn(entry);
					}
					else
					{
						Database.Games.Add(entry.Id, entry);
					}
				}
			}
		}

		protected override void OnStart()
		{
			Parallel.ForEach(_jobs, RunJob);
			Close();
		}

		protected override void UpdateText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format(CultureInfo.CurrentUICulture, "Scraped {0}/{1}", CompletedJobs, TotalJobs));

			string timeLeft = string.Format(CultureInfo.CurrentUICulture, "{0}: ", "Time left") + "{0}";
			if (CompletedJobs > 0)
			{
				if ((CompletedJobs > (TotalJobs / 4)) || ((CompletedJobs % 5) == 0))
				{
					TimeSpan timeRemaining = TimeSpan.FromTicks((DateTime.UtcNow.Subtract(_start).Ticks * (TotalJobs - (CompletedJobs + 1))) / (CompletedJobs + 1));
					if (timeRemaining.TotalSeconds >= 60)
					{
						_timeLeft = string.Format(CultureInfo.InvariantCulture, timeLeft, timeRemaining.Minutes + ":" + (timeRemaining.Seconds < 10 ? "0" + timeRemaining.Seconds : timeRemaining.Seconds.ToString()));
					}
					else
					{
						_timeLeft = string.Format(CultureInfo.InvariantCulture, timeLeft, timeRemaining.Seconds + "s");
					}
				}
			}
			else
			{
				_timeLeft = string.Format(CultureInfo.CurrentUICulture, timeLeft, "Unknown");
			}

			stringBuilder.AppendLine(_timeLeft);
			SetText(stringBuilder.ToString());
		}

		private void RunJob(int appId)
		{
			if (Stopped)
			{
				return;
			}

			DatabaseEntry entry = Database.Contains(appId) ? Database.Games[appId] : new DatabaseEntry(appId);
			entry.ScrapeStore();

			if (entry.LastStoreScrape == 0)
			{
				entry.ScrapeTrueSteamAchievements();
			}

			lock (SyncRoot)
			{
				if (Stopped)
				{
					return;
				}

				_results.Add(entry);
				OnJobCompletion();
			}
		}

		#endregion
	}
}