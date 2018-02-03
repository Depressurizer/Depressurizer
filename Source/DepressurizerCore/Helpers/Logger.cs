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
	///     Level of the log message
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		///     Verbose messages (enter/exit subroutine, buffer contents, etc.)
		/// </summary>
		Verbose = 0,

		/// <summary>
		///     Debug messages, to help in diagnosing a problem
		/// </summary>
		Debug = 1,

		/// <summary>
		///     Informational messages, showing completion, progress, etc.
		/// </summary>
		Info = 2,

		/// <summary>
		///     Warning error messages which do not cause a functional failure
		/// </summary>
		Warn = 3,

		/// <summary>
		///     Major error messages, some lost functionality
		/// </summary>
		Error = 4,

		/// <summary>
		///     Critical error messages, aborts the subsystem
		/// </summary>
		Fatal = 5
	}

	/// <summary>
	///     Logger Controller
	/// </summary>
	public sealed class Logger : IDisposable
	{
		#region Static Fields

		private static readonly ConcurrentQueue<string> LogQueue = new ConcurrentQueue<string>();

		private static readonly object SyncRoot = new object();

		private static volatile Logger _instance;

		#endregion

		#region Fields

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
		///     Logger Instance
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
					SentryLogger.Log(e);
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
				catch (Exception e)
				{
					SentryLogger.Log(e);
				}
			}
		}

		private void Write(LogLevel logLevel, string logMessage, params object[] args)
		{
			Write(logLevel, string.Format(CultureInfo.InvariantCulture, logMessage, args));
		}

		private void Write(LogLevel logLevel, string logMessage)
		{
			lock (SyncRoot)
			{
				if (logLevel == LogLevel.Verbose)
				{
					return;
				}

				string logEntry = string.Format(CultureInfo.InvariantCulture, "{0} {1,-7} | {2}", DateTime.Now, logLevel, logMessage);
				LogQueue.Enqueue(logEntry);

				Task.Run(() => System.Diagnostics.Debug.WriteLine(logEntry));

				if (LogQueue.Count >= 100)
				{
					FlushLog();
				}
			}
		}

		#endregion
	}
}