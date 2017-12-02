﻿/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.Text;
using Rallion;

namespace Depressurizer
{
    class DbScrapeDlg : CancelableDlg
    {
        Queue<int> jobs;
        List<GameDBEntry> results;

        DateTime start;

        public DbScrapeDlg(Queue<int> jobs)
            : base(GlobalStrings.CDlgScrape_ScrapingGameInfo, true)
        {
            this.jobs = jobs;
            totalJobs = jobs.Count;

            results = new List<GameDBEntry>();
        }

        protected override void UpdateForm_Load(object sender, EventArgs e)
        {
            start = DateTime.Now;
            base.UpdateForm_Load(sender, e);
        }

        private int GetNextGameId()
        {
            lock (jobs)
            {
                if (jobs.Count > 0)
                {
                    return jobs.Dequeue();
                }
                return 0;
            }
        }

        protected override void RunProcess()
        {
            bool stillRunning = true;
            while (!Stopped && stillRunning)
            {
                stillRunning = RunNextJob();
            }
            OnThreadCompletion();
        }

        /// <summary>
        /// Runs the next job in the queue, in a thread-safe manner. Aborts ASAP if the form is closed.
        /// </summary>
        /// <returns>True if a job was run, false if it was aborted first</returns>
        private bool RunNextJob()
        {
            int id = GetNextGameId();
            if (id == 0)
            {
                return false;
            }
            if (Stopped) return false;

            GameDBEntry newGame = new GameDBEntry();
            newGame.Id = id;
            newGame.ScrapeStore();

            // This lock is critical, as it makes sure that the abort check and the actual game update funtion essentially atomically with reference to form-closing.
            // If this isn't the case, the form could successfully close before this happens, but then it could still go through, and that's no good.
            lock (abortLock)
            {
                if (!Stopped)
                {
                    results.Add(newGame);
                    OnJobCompletion();
                    return true;
                }
                return false;
            }
        }

        protected override void Finish()
        {
            if (!Canceled)
            {
                SetText(GlobalStrings.CDlgScrape_ApplyingData);

                if (results != null)
                {
                    foreach (GameDBEntry g in results)
                    {
                        if (Program.GameDB.Contains(g.Id))
                        {
                            Program.GameDB.Games[g.Id].MergeIn(g);
                        }
                        else
                        {
                            Program.GameDB.Games.Add(g.Id, g);
                        }
                    }
                }
            }
        }

        protected override void UpdateText()
        {
            TimeSpan timeRemaining = TimeSpan.Zero;
            if (jobsCompleted > 0)
            {
                double msElapsed = (DateTime.Now - start).TotalMilliseconds;
                double msPerItem = msElapsed / jobsCompleted;
                double msRemaining = msPerItem * (totalJobs - jobsCompleted);
                timeRemaining = TimeSpan.FromMilliseconds(msRemaining);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(GlobalStrings.CDlgDataScrape_UpdatingComplete, jobsCompleted, totalJobs));

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
    }
}