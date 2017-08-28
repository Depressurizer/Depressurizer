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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Depressurizer
{
    /// <summary>
    /// The message level to log at. Any messages below this level will not be logged.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// </summary>
        None = 0,

        /// <summary>
        ///     Critical error messages, aborts the subsystem
        /// </summary>
        Fatal = 1,

        /// <summary>
        ///     Major error messages, some lost functionality
        /// </summary>
        Error = 2,

        /// <summary>
        ///     Warning error messages which do not cause a functional failure
        /// </summary>
        Warn = 3,

        /// <summary>
        ///     Informational messages, showing completion, progress, etc.
        /// </summary>
        Info = 4,

        /// <summary>
        ///     Debug messages, to help in diagnosing a problem
        /// </summary>
        Debug = 5,

        /// <summary>
        ///     Verbose messages (enter/exit subroutine, buffer contents, etc.)
        /// </summary>
        Verbose = 6
    }

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    internal sealed class AppLogger : IDisposable
    {
        #region Fields

        /// <summary>
        /// </summary>
        private static readonly ConcurrentQueue<string> LogQueue = new ConcurrentQueue<string>();

        /// <summary>
        /// </summary>
        private static volatile AppLogger _instance;

        /// <summary>
        /// </summary>
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// </summary>
        private static DateTime _lastFlushed = DateTime.Now;

        /// <summary>
        /// </summary>
        private LogLevel _level = LogLevel.Info;

        /// <summary>
        /// </summary>
        private int _maxDays = 3;

        /// <summary>
        /// </summary>
        private FileStream _outputStream;

        /// <summary>
        /// </summary>
        private AppLogger()
        {
            WriteInfo("Logger Instance Initialized");
            _outputStream = new FileStream(ActiveLogFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
        }

        /// <summary>
        /// </summary>
        public static string ActiveLogFile => Path.Combine(LogPath, LogFile);

        /// <summary>
        /// </summary>
        public static AppLogger Instance
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
                        _instance = new AppLogger();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// </summary>
        public static string LogFile => string.Format(CultureInfo.InvariantCulture, "Depressurizer-({0}).log", DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture));

        /// <summary>
        /// Path in which to place created log files
        /// </summary>
        public static string LogPath
        {
            get
            {
                string path = Path.Combine(Environment.CurrentDirectory, "logs");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        /// <summary>
        /// The message level to log at. Any messages below this level will not be logged.
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
        /// Maximum number of days to keep a log for.
        /// </summary>
        public int MaxDays
        {
            get => _maxDays;
            set
            {
                if (value <= 0 || value == _maxDays)
                {
                    return;
                }

                lock (SyncRoot)
                {
                    WriteInfo("Changed MaxDays limit to {0}, previous limit {1}", value, _maxDays);
                    _maxDays = value;
                }
                CheckFileLimit();
            }
        }

        #endregion

        #region Utility Methods

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
                foreach (FileInfo file in new DirectoryInfo(LogPath).GetFiles().Where(file => fileName.IsMatch(file.Name) && file.FullName != ActiveLogFile).OrderByDescending(x => x.CreationTime).Skip(MaxDays))
                {
                    WriteInfo("Deleted logFile: {0}({1})", file.Name, file.FullName);
                    file.Delete();
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static bool DoPeriodicFlush()
        {
            TimeSpan logAge = DateTime.Now - _lastFlushed;
            return logAge.TotalSeconds >= 10;
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

        #endregion

        #region Writers

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void WriteDebug(string logMessage)
        {
            Write(LogLevel.Debug, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void WriteDebug(string logMessage, params object[] args)
        {
            Write(LogLevel.Debug, logMessage, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void WriteError(string logMessage)
        {
            Write(LogLevel.Error, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void WriteError(string logMessage, params object[] args)
        {
            Write(LogLevel.Error, logMessage, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void WriteException(string logMessage)
        {
            Write(LogLevel.Error, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="exception"></param>
        public void WriteException(string logMessage, Exception exception)
        {
            Write(LogLevel.Error, logMessage + Environment.NewLine + exception);
        }

        /// <summary>
        /// </summary>
        /// <param name="exception"></param>
        public void WriteException(Exception exception)
        {
            Write(LogLevel.Error, exception.ToString());
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void WriteInfo(string logMessage)
        {
            Write(LogLevel.Info, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void WriteInfo(string logMessage, params object[] args)
        {
            Write(LogLevel.Info, logMessage, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void WriteVerbose(string logMessage)
        {
            Write(LogLevel.Verbose, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void WriteVerbose(string logMessage, params object[] args)
        {
            Write(LogLevel.Verbose, logMessage, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        public void WriteWarn(string logMessage)
        {
            Write(LogLevel.Warn, logMessage);
        }

        /// <summary>
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="args"></param>
        public void WriteWarn(string logMessage, params object[] args)
        {
            Write(LogLevel.Warn, logMessage, args);
        }

        /// <summary>
        /// Writes specified message to the given channel.
        /// </summary>
        /// <param name="logLevel">Channel to output on</param>
        /// <param name="logMessage">Template of the message to add</param>
        /// <param name="args">Format parameters</param>
        private void Write(LogLevel logLevel, string logMessage, params object[] args)
        {
            Write(logLevel, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        private void Write(LogLevel logLevel, string logMessage)
        {
            lock (SyncRoot)
            {
                string logEntry = string.Format(CultureInfo.InvariantCulture, "{0} - {1}: {2}", DateTime.Now.ToString("HH:mm:ss'.'ffffff"), logLevel.ToString().ToUpperInvariant(), logMessage);
                System.Diagnostics.Debug.WriteLine(logEntry);

                if (logLevel <= Level)
                {
                    LogQueue.Enqueue(logEntry);
                }

                if (LogQueue.Count >= 100 || DoPeriodicFlush())
                {
                    FlushLog();
                }
            }
        }

        /// <summary>
        /// Writes out public fields of specified object to the log. For IEnumerable values, individual items are written out; this only applies to top-level items.
        /// </summary>
        /// <param name="lev">Channel to output on</param>
        /// <param name="o">Object to write fields of</param>
        /// <param name="prefix">Text prefix, the first line of the output.</param>
        public void WriteObject(LogLevel lev, object o, string prefix = "")
        {
            if (Level >= lev)
            {
                StringBuilder builder = new StringBuilder(prefix + Environment.NewLine);
                FieldInfo[] fields = o.GetType().GetFields();
                foreach (FieldInfo fi in fields)
                {
                    object val = fi.GetValue(o);
                    if (val is IEnumerable<object>)
                    {
                        int index = 0;
                        foreach (object subObj in (val as IEnumerable<object>))
                        {
                            builder.AppendLine(string.Format("{0}[{1}] : {2}", fi.Name, index, subObj));
                            index++;
                        }
                    }
                    else
                    {
                        builder.AppendLine(string.Format("{0} : {1}", fi.Name, val));
                    }
                }
                Write(lev, builder.ToString());
            }
        }

        #endregion
    }
}