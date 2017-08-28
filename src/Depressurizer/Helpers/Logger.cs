#region GNU GENERAL PUBLIC LICENSE

// 
// This file is part of Depressurizer.
// Copyright (C) 2017 Martijn Vegter
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// 

#endregion

using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Depressurizer.Helpers
{
    /// <summary>
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// </summary>
        Invalid = 0,

        /// <summary>
        ///     Verbose messages (enter/exit subroutine, buffer contents, etc.)
        /// </summary>
        Verbose = 1,

        /// <summary>
        ///     Debug messages, to help in diagnosing a problem
        /// </summary>
        Debug = 2,

        /// <summary>
        ///     Informational messages, showing completion, progress, etc.
        /// </summary>
        Info = 3,

        /// <summary>
        ///     Warning error messages which do not cause a functional failure
        /// </summary>
        Warn = 4,

        /// <summary>
        ///     Major error messages, some lost functionality
        /// </summary>
        Error = 5,

        /// <summary>
        ///     Critical error messages, aborts the subsystem
        /// </summary>
        Fatal = 6
    }

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    internal sealed class Logger : IDisposable
    {
        /// <summary>
        /// </summary>
        private static readonly ConcurrentQueue<string> LogQueue = new ConcurrentQueue<string>();

        /// <summary>
        /// </summary>
        private static volatile Logger _instance;

        /// <summary>
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// </summary>
        private static DateTime _lastFlushed = DateTime.Now;

        /// <summary>
        /// </summary>
        private LogLevel _level = LogLevel.Debug;

        /// <summary>
        /// </summary>
        private int _maxBackup = 7;

        /// <summary>
        /// </summary>
        private FileStream _outputStream;

        /// <summary>
        /// </summary>
        private Logger()
        {
            Info("Logger Instance Initialized");
            _outputStream = new FileStream(ActiveLogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        /// <summary>
        /// </summary>
        public static string ActiveLogFile => Path.Combine(LogPath, LogFile);

        /// <summary>
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
        /// </summary>
        public static string LogFile => string.Format(CultureInfo.InvariantCulture, "Depressurizer-({0}).log", DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture));

        /// <summary>
        /// </summary>
        public static string LogPath
        {
            get
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Depressurizer", "Logs");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        /// <summary>
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

        /// <summary>
        /// </summary>
        public int MaxBackup
        {
            get => _maxBackup;
            set
            {
                if (value <= 0 || value == _maxBackup)
                {
                    return;
                }

                lock (SyncRoot)
                {
                    Info("Changed MaxBackup limit to {0}, previous limit {1}", value, _maxBackup);
                    _maxBackup = value;
                }
                CheckFileLimit();
            }
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

        public void CheckFileLimit()
        {
            lock (SyncRoot)
            {
                Regex fileName = new Regex("(Depressurizer)(-)(\\()[0-9]+-[0-9]+-[0-9]*(\\))");
                foreach (FileInfo file in new DirectoryInfo(LogPath).GetFiles().Where(file => fileName.IsMatch(file.Name) && file.FullName != ActiveLogFile).OrderByDescending(x => x.CreationTime).Skip(MaxBackup))
                {
                    Info("Deleted logFile: {0}({1})", file.Name, file.FullName);
                    file.Delete();
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void Debug(string logMessage)
        {
            Write(LogLevel.Debug, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void Debug(string logMessage, params object[] args)
        {
            Write(LogLevel.Debug, logMessage, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void Error(string logMessage)
        {
            Write(LogLevel.Error, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void Error(string logMessage, params object[] args)
        {
            Write(LogLevel.Error, logMessage, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void Exception(string logMessage)
        {
            Write(LogLevel.Error, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="exception"></param>
        public void Exception(string logMessage, Exception exception)
        {
            Write(LogLevel.Error, logMessage + Environment.NewLine + exception);
        }

        /// <summary>
        /// </summary>
        /// <param name="exception"></param>
        public void Exception(Exception exception)
        {
            Write(LogLevel.Error, exception.ToString());
        }

        /// <summary>
        /// </summary>
        /// TODO: Handle Exception
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
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine(exception);
                    throw;
                }
                _lastFlushed = DateTime.Now;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void Info(string logMessage)
        {
            Write(LogLevel.Info, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void Info(string logMessage, params object[] args)
        {
            Write(LogLevel.Info, logMessage, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void Verbose(string logMessage)
        {
            Write(LogLevel.Verbose, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void Verbose(string logMessage, params object[] args)
        {
            Write(LogLevel.Verbose, logMessage, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void Warn(string logMessage)
        {
            Write(LogLevel.Warn, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void Warn(string logMessage, params object[] args)
        {
            Write(LogLevel.Warn, logMessage, args);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static bool DoPeriodicFlush()
        {
            TimeSpan logAge = DateTime.Now - _lastFlushed;
            return logAge.TotalSeconds >= 60;
        }

        /// <summary>
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        private void Write(LogLevel logLevel, string logMessage, params object[] args)
        {
            Write(logLevel, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        /// <summary>
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="logMessage"></param>
        /// <param name="bypassFilter"></param>
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