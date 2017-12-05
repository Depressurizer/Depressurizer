#region License

//     This file (Logger.cs) is part of Depressurizer.
//     Copyright (C) 2017  Martijn Vegter
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

namespace Depressurizer.Helpers
{
    /// <summary>
    ///     Depressurizer Logger
    /// </summary>
    public sealed class Logger : IDisposable
    {
#if DEBUG
        /// <summary>
        ///     Current LogLevel
        /// </summary>
        public const LogLevel Level = LogLevel.Debug;
#else /// <summary>
///     Current LogLevel
/// </summary>
        public const LogLevel Level = LogLevel.Info;
#endif

        private static readonly ConcurrentQueue<string> LogQueue = new ConcurrentQueue<string>();

        private static volatile Logger _instance;

        private static readonly object SyncRoot = new object();

        private static DateTime _lastFlushed = DateTime.Now;

        private FileStream _outputStream;

        /// <summary>
        ///     Only instance of the Logger
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

        private Logger()
        {
            Info("Logger Instance Initialized");
            _outputStream = new FileStream(Location.File.ActiveLogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            lock (SyncRoot)
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
        }

        /// <summary>
        ///     Write message at the Debug level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        public void Debug(string logMessage)
        {
            Write(LogLevel.Debug, logMessage);
        }

        /// <summary>
        ///     Write formatted message at the Debug level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        /// <param name="args">Arguments</param>
        public void Debug(string logMessage, params object[] args)
        {
            Write(LogLevel.Debug, logMessage, args);
        }

        /// <summary>
        ///     Write message at the Error level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        public void Error(string logMessage)
        {
            Write(LogLevel.Error, logMessage);
        }

        /// <summary>
        ///     Write formatted message at the Error level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        /// <param name="args">Arguments</param>
        public void Error(string logMessage, params object[] args)
        {
            Write(LogLevel.Error, logMessage, args);
        }

        /// <summary>
        ///     Write message at the Error level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        public void Exception(string logMessage)
        {
            Write(LogLevel.Error, logMessage);
        }

        /// <summary>
        ///     Write exception with message header
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        /// <param name="ex">Exception to log</param>
        public void Exception(string logMessage, Exception ex)
        {
            Write(LogLevel.Error, logMessage + Environment.NewLine + ex);
        }

        /// <summary>
        ///     Write e at the Error Level
        /// </summary>
        /// <param name="ex">Exception to log</param>
        public void Exception(Exception ex)
        {
            Write(LogLevel.Error, ex.ToString());
        }

        /// <summary>
        ///     Write message at the Info level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        public void Info(string logMessage)
        {
            Write(LogLevel.Info, logMessage);
        }

        /// <summary>
        ///     Write formatted message at the Info level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        /// <param name="args">Arguments</param>
        public void Info(string logMessage, params object[] args)
        {
            Write(LogLevel.Info, logMessage, args);
        }

        /// <summary>
        ///     Write message at the Verbose level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        public void Verbose(string logMessage)
        {
            Write(LogLevel.Verbose, logMessage);
        }

        /// <summary>
        ///     Write formatted message at the Verbose level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        /// <param name="args">Arguments</param>
        public void Verbose(string logMessage, params object[] args)
        {
            Write(LogLevel.Verbose, logMessage, args);
        }

        /// <summary>
        ///     Write message at the Warn level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        public void Warn(string logMessage)
        {
            Write(LogLevel.Warn, logMessage);
        }

        /// <summary>
        ///     Write formatted message at the Warn level
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        /// <param name="args">Arguments</param>
        public void Warn(string logMessage, params object[] args)
        {
            Write(LogLevel.Warn, logMessage, args);
        }

        private static bool DoPeriodicFlush()
        {
            TimeSpan logAge = DateTime.Now - _lastFlushed;
            return logAge.TotalSeconds >= 60;
        }

        private void FlushLog()
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
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine(exception);
                    throw;
                }
                _lastFlushed = DateTime.Now;
            }
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
                System.Diagnostics.Debug.WriteLine(logEntry);

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
    }
}