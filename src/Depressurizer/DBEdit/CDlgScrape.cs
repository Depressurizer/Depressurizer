using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Depressurizer.Core.Models;
using Depressurizer.Properties;
using Rallion;

namespace Depressurizer
{
    internal class DbScrapeDlg : CancelableDlg
    {
        #region Fields

        private readonly ConcurrentQueue<ScrapeJob> _queue;

        private readonly ConcurrentQueue<DatabaseEntry> _results = new ConcurrentQueue<DatabaseEntry>();

        private DateTime _start;

        private string _timeLeft;

        #endregion

        #region Constructors and Destructors

        public DbScrapeDlg(Dictionary<int, int> appIds) : base(GlobalStrings.CDlgScrape_ScrapingGameInfo, true)
        {
            _queue = new ConcurrentQueue<ScrapeJob>();
            foreach (KeyValuePair<int, int> pair in appIds)
            {
                _queue.Enqueue(new ScrapeJob(pair.Key, pair.Value));
            }

            totalJobs = _queue.Count;
        }

        #endregion

        #region Properties

        private static Database Database => Database.Instance;

        #endregion

        #region Methods

        protected override void Finish()
        {
            if (Canceled)
            {
                return;
            }

            SetText(GlobalStrings.CDlgScrape_ApplyingData);

            if (_results == null)
            {
                return;
            }

            foreach (DatabaseEntry g in _results)
            {
                Database.Add(g);
            }

            SetText("Applied data...");
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

        protected override void UpdateForm_Load(object sender, EventArgs e)
        {
            _start = DateTime.UtcNow;
            base.UpdateForm_Load(sender, e);
        }

        protected override void UpdateText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format(CultureInfo.CurrentCulture, Resources.ScrapedProgress, jobsCompleted, totalJobs));

            string timeLeft = string.Format(CultureInfo.CurrentCulture, "{0}: ", Resources.TimeLeft) + "{0}";
            if (jobsCompleted > 0)
            {
                TimeSpan timeRemaining = TimeSpan.FromTicks(DateTime.UtcNow.Subtract(_start).Ticks * (totalJobs - (jobsCompleted + 1)) / (jobsCompleted + 1));
                if (timeRemaining.TotalHours >= 1)
                {
                    _timeLeft = string.Format(CultureInfo.InvariantCulture, timeLeft, timeRemaining.Hours + ":" + (timeRemaining.Minutes < 10 ? "0" + timeRemaining.Minutes : timeRemaining.Minutes.ToString(CultureInfo.InvariantCulture)) + ":" + (timeRemaining.Seconds < 10 ? "0" + timeRemaining.Seconds : timeRemaining.Seconds.ToString(CultureInfo.InvariantCulture)));
                }
                else if (timeRemaining.TotalSeconds >= 60)
                {
                    _timeLeft = string.Format(CultureInfo.InvariantCulture, timeLeft, (timeRemaining.Minutes < 10 ? "0" + timeRemaining.Minutes : timeRemaining.Minutes.ToString(CultureInfo.InvariantCulture)) + ":" + (timeRemaining.Seconds < 10 ? "0" + timeRemaining.Seconds : timeRemaining.Seconds.ToString(CultureInfo.InvariantCulture)));
                }
                else
                {
                    _timeLeft = string.Format(CultureInfo.InvariantCulture, timeLeft, timeRemaining.Seconds + "s");
                }
            }
            else
            {
                _timeLeft = string.Format(CultureInfo.CurrentCulture, timeLeft, Resources.Unknown);
            }

            stringBuilder.AppendLine(_timeLeft);
            SetText(stringBuilder.ToString());
        }

        private bool GetNextJob(out ScrapeJob job)
        {
            job = null;

            return !Stopped && _queue.TryDequeue(out job);
        }

        private bool RunNextJob()
        {
            if (!GetNextJob(out ScrapeJob job))
            {
                return false;
            }

            if (Stopped)
            {
                return false;
            }

            DatabaseEntry newGame = new DatabaseEntry(job.Id)
            {
                AppId = job.ScrapeId
            };

            newGame.ScrapeStore(Database.LanguageCode);
            if (Stopped)
            {
                return false;
            }

            if (newGame.LastStoreScrape != 0)
            {
                _results.Enqueue(newGame);
            }

            OnJobCompletion();

            return true;
        }

        #endregion

        private class ScrapeJob
        {
            #region Fields

            public readonly int Id;

            public readonly int ScrapeId;

            #endregion

            #region Constructors and Destructors

            public ScrapeJob(int id, int scrapeId)
            {
                Id = id;
                ScrapeId = scrapeId;
            }

            #endregion
        }
    }
}
