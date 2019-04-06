using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Depressurizer.Core.Models;
using Depressurizer.Properties;
using Rallion;

namespace Depressurizer
{
    internal class DbScrapeDlg : CancelableDlg
    {
        #region Fields

        private readonly Queue<int> _queue;

        private readonly List<DatabaseEntry> _results = new List<DatabaseEntry>();

        private DateTime _start;

        private string _timeLeft;

        #endregion

        #region Constructors and Destructors

        public DbScrapeDlg(IEnumerable<int> appIds) : base(GlobalStrings.CDlgScrape_ScrapingGameInfo, true)
        {
            _queue = new Queue<int>(appIds.Distinct().Where(id => id > 0));
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
                if (Database.Contains(g.AppId, out DatabaseEntry entry))
                {
                    entry.MergeIn(g);
                }
                else
                {
                    Database.Add(g);
                }
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

        private int GetNextGameId()
        {
            lock (_queue)
            {
                if (_queue.Count > 0)
                {
                    return _queue.Dequeue();
                }

                return 0;
            }
        }

        private bool RunNextJob()
        {
            int id = GetNextGameId();
            if (id == 0)
            {
                return false;
            }

            if (Stopped)
            {
                return false;
            }

            DatabaseEntry newGame = new DatabaseEntry(id);
            newGame.ScrapeStore(Database.LanguageCode);

            // This lock is critical, as it makes sure that the abort check and the actual game update funtion essentially atomically with reference to form-closing.
            // If this isn't the case, the form could successfully close before this happens, but then it could still go through, and that's no good.
            lock (abortLock)
            {
                if (Stopped)
                {
                    return false;
                }

                if (newGame.LastStoreScrape != 0)
                {
                    _results.Add(newGame);
                }

                OnJobCompletion();
                return true;
            }
        }

        #endregion
    }
}
