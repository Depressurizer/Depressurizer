#region LICENSE

//     This file (Logger.cs) is part of DepressurizerCore.
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
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DepressurizerCore.Helpers
{
    /// <summary>
    ///     DepressurizerCore Logger
    /// </summary>
    public sealed class Logger : IDisposable
    {
        #region Static Fields

        private static readonly ConcurrentQueue<string> LogQueue = new ConcurrentQueue<string>();

        private static readonly object SyncRoot = new object();

        private static volatile Logger _instance;

        private static DateTime _lastFlushed = DateTime.Now;

        #endregion

        #region Fields

        private LogLevel _level = LogLevel.Debug;

        private FileStream _outputStream;

        #endregion

        #region Constructors and Destructors

        private Logger()
        {
            Info("Logger Instance Initialized");
            _outputStream = new FileStream(Location.File.Log, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     DepressurizerCore Logger Instance
        /// </summary>
        public static Logger Instance
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
                        _instance = new Logger();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        ///     Level to log at
        /// </summary>
        public LogLevel Level
        {
            get => _level;
            set
            {
                if (value == _level)
                {
                    return;
                }

                lock (SyncRoot)
                {
                    Write(LogLevel.Info, string.Format(CultureInfo.InvariantCulture, "Changed Logger Level to {0}, previous level {1}", value, _level), true);
                    _level = value;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Debug(string logMessage)
        {
            Write(LogLevel.Debug, logMessage);
        }

        public void Debug(string logMessage, params object[] args)
        {
            Write(LogLevel.Debug, logMessage, args);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            lock (SyncRoot)
            {
                try
                {
                    FlushLog();

                    byte[] output = new UTF8Encoding().GetBytes(Environment.NewLine);
                    _outputStream.Write(output, 0, output.Length);

                    _outputStream.Flush();
                    _outputStream.Flush(true);
                    _outputStream.Dispose();
                    _outputStream.Close();
                    _outputStream = null;
                    _instance = null;
                }
                catch (Exception e)
                {
                    SentryLogger.LogException(e);
                }
            }
        }

        public void Error(string logMessage)
        {
            Write(LogLevel.Error, logMessage);
        }

        public void Error(string logMessage, params object[] args)
        {
            Write(LogLevel.Error, logMessage, args);
        }

        public void Exception(string logMessage)
        {
            Write(LogLevel.Error, logMessage);
        }

        public void Exception(string logMessage, Exception e)
        {
            Write(LogLevel.Error, logMessage + Environment.NewLine + e);
        }

        public void Exception(Exception e)
        {
            Write(LogLevel.Error, e.ToString());
        }

        public void FlushLog()
        {
            lock (SyncRoot)
            {
                try
                {
                    while (LogQueue.Count > 0)
                    {
                        LogQueue.TryDequeue(out string logEntry);
                        byte[] output = new UTF8Encoding().GetBytes(logEntry + Environment.NewLine);
                        _outputStream.Write(output, 0, output.Length);
                    }
                }
                catch (Exception e)
                {
                    SentryLogger.LogException(e);
                }

                _lastFlushed = DateTime.Now;
            }
        }

        public void Info(string logMessage)
        {
            Write(LogLevel.Info, logMessage);
        }

        public void Info(string logMessage, params object[] args)
        {
            Write(LogLevel.Info, logMessage, args);
        }

        public void Verbose(string logMessage)
        {
            Write(LogLevel.Verbose, logMessage);
        }

        public void Verbose(string logMessage, params object[] args)
        {
            Write(LogLevel.Verbose, logMessage, args);
        }

        public void Warn(string logMessage)
        {
            Write(LogLevel.Warn, logMessage);
        }

        public void Warn(string logMessage, params object[] args)
        {
            Write(LogLevel.Warn, logMessage, args);
        }

        #endregion

        #region Methods

        private static bool DoPeriodicFlush()
        {
            TimeSpan logAge = DateTime.Now - _lastFlushed;

            return logAge.TotalSeconds >= 60;
        }

        private void Write(LogLevel logLevel, string logMessage, params object[] args)
        {
            Write(logLevel, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        private void Write(LogLevel logLevel, string logMessage, bool bypassFilter = false)
        {
            lock (SyncRoot)
            {
                string logEntry = string.Format(CultureInfo.InvariantCulture, "{0} {1,-7} | {2}", DateTime.Now, logLevel, logMessage);

                Task.Run(() => System.Diagnostics.Debug.WriteLine(logEntry));

                if (logLevel >= Level || bypassFilter)
                {
                    LogQueue.Enqueue(logEntry);
                }

                if (LogQueue.Count >= 100 || DoPeriodicFlush())
                {
                    FlushLog();
                }
            }
        }

        #endregion
    }
}