#region LICENSE

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
using Depressurizer.Properties;
using DepressurizerCore.Helpers;
using DepressurizerCore.Models;

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

		public ScrapeDialog(List<int> jobs) : base(string.Format(CultureInfo.CurrentUICulture, "{0}...", Resources.Scraping), true)
		{
			_jobs = jobs;
			TotalJobs = jobs.Count;
		}

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

			SetText(string.Format(CultureInfo.CurrentUICulture, "{0}...", Resources.Applying_Data));

			lock (SyncRoot)
			{
				foreach (DatabaseEntry entry in _results)
				{
					Database.Instance.AddOrUpdate(entry);
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
			stringBuilder.AppendLine(string.Format(CultureInfo.CurrentUICulture, Resources.Scraping_Status, jobsCompleted, TotalJobs));

			string timeLeft = string.Format(CultureInfo.CurrentUICulture, "{0}: ", Resources.Time_Left) + "{0}";
			if (jobsCompleted > 0)
			{
				if ((jobsCompleted > (TotalJobs / 4)) || ((jobsCompleted % 5) == 0))
				{
					TimeSpan timeRemaining = TimeSpan.FromTicks((DateTime.UtcNow.Subtract(_start).Ticks * (TotalJobs - (jobsCompleted + 1))) / (jobsCompleted + 1));
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
				_timeLeft = string.Format(CultureInfo.CurrentUICulture, timeLeft, Resources.Unknown);
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

			DatabaseEntry entry = Database.Instance.Contains(appId) ? Database.Instance.Games[appId] : new DatabaseEntry(appId);

			try
			{
				entry.ScrapeStore();
			}
			catch (Exception)
			{
				Logger.Instance.Warn("Error while scraping appid: {0}", appId);
				return;
			}

			if (entry.LastStoreScrape == 0)
			{
				try
				{
					entry.ScrapeTrueSteamAchievements();
				}
				catch (Exception)
				{
					Logger.Instance.Warn("Error while scraping TSA appid: {0}", appId);
					return;
				}
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
