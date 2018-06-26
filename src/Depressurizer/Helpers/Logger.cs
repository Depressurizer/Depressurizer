#region License

//     This file (Logger.cs) is part of Depressurizer.
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
using Depressurizer.Core.Helpers;

namespace Depressurizer.Helpers
{
	/// <summary>
	///     Defines the set of levels recognized by the system.
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		///     The Verbose level designates fine-grained informational events that are most useful to debug an application.
		/// </summary>
		Verbose,

		/// <summary>
		///     The Debug level designates fine-grained informational events that are most useful to debug an application.
		/// </summary>
		Debug,

		/// <summary>
		///     The Info level designates informational messages that highlight the progress of the application at coarse-grained
		///     level.
		/// </summary>
		Info,

		/// <summary>
		///     The Warn level designates potentially harmful situations.
		/// </summary>
		Warn,

		/// <summary>
		///     The Error level designates error events that might still allow the application to continue running.
		/// </summary>
		Error,

		/// <summary>
		///     The Fatal level designates very severe error events that will presumably lead the application to abort.
		/// </summary>
		Fatal
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

		/// <summary>
		///     Generates a logging event at the Debug level using the message.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		public void Debug(string logMessage)
		{
			Write(LogLevel.Debug, logMessage);
		}

		/// <summary>
		///     Generates a logging event at the Debug level using the message, using the provided objects to format.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
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
					Sentry.Log(e);
				}
			}
		}

		/// <summary>
		///     Generates a logging event at the Error level using the message.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		public void Error(string logMessage)
		{
			Write(LogLevel.Error, logMessage);
		}

		/// <summary>
		///     Generates a logging event at the Error level using the message, using the provided objects to format.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public void Error(string logMessage, params object[] args)
		{
			Write(LogLevel.Error, logMessage, args);
		}

		/// <summary>
		///     Generates a logging event at the Error level using the message and exception.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		/// <param name="e">The exception to log, including its stack trace.</param>
		public void Exception(string logMessage, Exception e)
		{
			Write(LogLevel.Error, logMessage + Environment.NewLine + e);
		}

		/// <summary>
		///     Generates a logging event for the specified level using the exception.
		/// </summary>
		/// <param name="e">The exception to log, including its stack trace.</param>
		public void Exception(Exception e)
		{
			Write(LogLevel.Error, e.ToString());
		}

		/// <summary>
		///     Generates a logging event at the Info level using the message.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		public void Info(string logMessage)
		{
			Write(LogLevel.Info, logMessage);
		}

		/// <summary>
		///     Generates a logging event at the Info level using the message, using the provided objects to format.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public void Info(string logMessage, params object[] args)
		{
			Write(LogLevel.Info, logMessage, args);
		}

		/// <summary>
		///     Generates a logging event at the Verbose level using the message.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		public void Verbose(string logMessage)
		{
			Write(LogLevel.Verbose, logMessage);
		}

		/// <summary>
		///     Generates a logging event at the Verbose level using the message, using the provided objects to format.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		public void Verbose(string logMessage, params object[] args)
		{
			Write(LogLevel.Verbose, logMessage, args);
		}

		/// <summary>
		///     Generates a logging event at the Warn level using the message.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		public void Warn(string logMessage)
		{
			Write(LogLevel.Warn, logMessage);
		}

		/// <summary>
		///     Generates a logging event at the Warn level using the message, using the provided objects to format.
		/// </summary>
		/// <param name="logMessage">The message to log.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
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
					Sentry.Log(e);
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