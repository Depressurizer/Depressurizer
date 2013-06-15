/*
Copyright 2011, 2012, 2013 Steve Labbe.

This file is part of Depressurizer.

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
using System.IO;
using System.Text;

namespace Rallion {

    public enum LoggerLevel {
        None,
        Error,
        Warning,
        Info,
        Verbose
    }

    /// <summary>
    /// Simple application event logging class.
    /// </summary>
    public class AppLogger {
        #region Fields

        #region Internals
        private FileStream outputStream;
        private readonly object threadLock = new object();
        private static string[] LevTxt = { "", " ERR", "WARN", "INFO", "VERB" };
        #endregion

        #region Configuration

        private LoggerLevel _level;
        /// <summary>
        /// The message level to log at. Any messages below this level will not be logged.
        /// </summary>
        public LoggerLevel Level {
            get {
                lock( threadLock ) {
                    return _level;
                }
            }
            set {
                lock( threadLock ) {
                    _level = value;
                }
            }
        }

        private string _fileNameBase;
        /// <summary>
        /// Template for log filenames.
        /// ":d" will be replaced with the date
        /// ":t" with the time
        /// ":n" with the assembly name
        /// </summary>
        public string FileNameTemplate {
            get {
                lock( threadLock ) {
                    return _fileNameBase;
                }
            }
            set {
                lock( threadLock ) {
                    _fileNameBase = value;
                    if( IsActiveSession ) BeginSession();
                }
            }
        }

        private string _dateFormat;
        public string DateFormat {
            get {
                lock( threadLock ) {
                    return _dateFormat;
                }
            }
            set {
                lock( threadLock ) {
                    _dateFormat = value;
                }
            }
        }

        private string _filePath;
        /// <summary>
        /// Path in which to place created log files
        /// </summary>
        public string FilePath {
            get {
                lock( threadLock ) {
                    return _filePath;
                }
            }
            set {
                lock( threadLock ) {
                    _filePath = value;
                    if( IsActiveSession ) BeginSession();
                }
            }
        }

        private TimeSpan _maxFileDuration;
        /// <summary>
        /// Maximum time span that a log file should cover. TimeSpan of zero ticks indicates no limit.
        /// </summary>
        public TimeSpan MaxFileDuration {
            get {
                lock( threadLock ) {
                    return _maxFileDuration;
                }
            }
            set {
                lock( threadLock ) {
                    _maxFileDuration = value;
                }
            }
        }

        private int _maxFileRecords;
        /// <summary>
        /// Maximum number of lines that a log file should contain. Zero indicates no limit.
        /// Some messages might be more than one line.
        /// </summary>
        public int MaxFileRecords {
            get {
                lock( threadLock ) {
                    return _maxFileRecords;
                }
            }
            set {
                lock( threadLock ) {
                    _maxFileRecords = value;
                }
            }
        }

        private int _maxFileSize;
        /// <summary>
        /// Maximum size that a log file should reach.
        /// </summary>
        public int MaxFileSize {
            get {
                lock( threadLock ) {
                    return _maxFileSize;
                }
            }
            set {
                lock( threadLock ) {
                    _maxFileSize = value;
                }
            }
        }

        private int _maxFiles;
        /// <summary>
        /// Maximum number of backup files to keep
        /// </summary>
        public int MaxBackup {
            get {
                lock( threadLock ) {
                    return _maxFiles;
                }
            }
            set {
                lock( threadLock ) {
                    _maxFiles = value;
                }
            }
        }

        private bool _allowAppend;
        /// <summary>
        /// Whether or not to re-use existing log files when opening new logging sessions.
        /// If false, any existing files will be shifted to the backup system.
        /// </summary>
        public bool AllowAppend {
            get {
                lock( threadLock ) {
                    return _allowAppend;
                }
            }
            set {
                lock( threadLock ) {
                    _allowAppend = value;
                }
            }
        }

        public bool _useOrigCreateTime;
        /// <summary>
        /// Whether or not to check for the file creation time when determining the creation date of an existing log file. This might not be accurate.
        /// If false, it will just act as if the log started when the current log session opened on it.
        /// In this case, appended log files can easily cover a longer span of time than specified in the configuration.
        /// </summary>
        public bool UseOriginalCreationTime {
            get {
                lock( threadLock ) {
                    return _useOrigCreateTime;
                }
            }
            set {
                lock( threadLock ) {
                    _useOrigCreateTime = value;
                }
            }
        }

        private bool _useTotalLineCount;
        /// <summary>
        /// Whether or not to check the total number of lines in the file to determine the record count when opening a new session on an existing file.
        /// This might be time consuming.
        /// If false, will act as if there were no entries in the existing file, and appended logs can have more records than specified.
        /// </summary>
        public bool UseTotalLineCount {
            get {
                lock( threadLock ) {
                    return _useTotalLineCount;
                }
            }
            set {
                lock( threadLock ) {
                    _useTotalLineCount = value;
                }
            }
        }

        private bool _autoSessionStart;
        /// <summary>
        /// Whether or not to automatically start a session if a Write call is made without one currently active.
        /// A session will only be started if the current logging level would result in a write.
        /// </summary>
        public bool AutoSessionStart {
            get {
                lock( threadLock ) {
                    return _autoSessionStart;
                }
            }
            set {
                lock( threadLock ) {
                    _autoSessionStart = value;
                }
            }
        }

        #endregion

        #region Information

        private FileInfo _logFile;
        /// <summary>
        /// Gets the currently open log file. If there is no log session in progress, null.
        /// </summary>
        public FileInfo LogFile {
            get {
                lock( threadLock ) {
                    return _logFile;
                }
            }
            private set {
                lock( threadLock ) {
                    _logFile = value;
                }
            }
        }

        private DateTime _currentFileStartTime;
        /// <summary>
        /// Gets the start time of the current open log.
        /// </summary>
        public DateTime CurrentFileStartTime {
            get {
                lock( threadLock ) {
                    return _currentFileStartTime;
                }
            }
            private set {
                lock( threadLock ) {
                    _currentFileStartTime = value;
                }
            }
        }

        private int _currentFileRecords;
        /// <summary>
        /// Gets the number of records in the current open log
        /// </summary>
        public int CurrentFileRecords {
            get {
                lock( threadLock ) {
                    return _currentFileRecords;
                }
            }
            private set {
                lock( threadLock ) {
                    _currentFileRecords = value;
                }
            }
        }

        private bool _isActiveSession;
        /// <summary>
        /// Checks to see if there is a logging session in progress
        /// </summary>
        public bool IsActiveSession {
            get {
                lock( threadLock ) {
                    return _isActiveSession;
                }
            }
            private set {
                lock( threadLock ) {
                    _isActiveSession = value;
                }
            }
        }

        private bool _isSuspended;
        /// <summary>
        /// Checks to see if the current session is suspended
        /// </summary>
        public bool IsSuspended {
            get {
                lock( threadLock ) {
                    return _isSuspended;
                }
            }
            private set {
                lock( threadLock ) {
                    _isSuspended = value;
                }
            }
        }

        /// <summary>
        /// Checks to see if there is currently an active, un-suspended session.
        /// </summary>
        public bool IsLogging {
            get {
                lock( threadLock ) {
                    return IsActiveSession && !IsSuspended;
                }
            }
        }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new AppLog logger with default settings.
        /// </summary>
        public AppLogger() {
            //Settings
            Level = LoggerLevel.Info;

            FilePath = String.Empty;
            FileNameTemplate = "Log_:d-:t.log";
            DateFormat = "o";

            AllowAppend = true;
            MaxFileDuration = new TimeSpan( 0 );
            MaxFileRecords = 0;
            MaxFileSize = 10000000;
            MaxBackup = 10;

            UseOriginalCreationTime = true;
            UseTotalLineCount = true;

            AutoSessionStart = true;

            //Status
            LogFile = null;
            this.CurrentFileRecords = 0;
            this.CurrentFileStartTime = new DateTime( 0 );
            IsActiveSession = false;
            IsSuspended = false;
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Gets the path that will actually be used to store log files.
        /// If there isn't one set, it will just use the current working directory.
        /// </summary>
        /// <returns>String representing the directory to save logs in.</returns>
        public string GetPath() {
            return ( FilePath == string.Empty ) ? Environment.CurrentDirectory : FilePath;
        }

        /// <summary>
        /// Gets the file to write to, moving other files out of the way if neccessary.
        /// </summary>
        /// <param name="forceNew">If true, will not return a reference to an existing file.</param>
        /// <returns>FileInfo representing the file we should write to. Is not guaranteed to exist, and will not if forceNew = true.</returns>
        private FileInfo GetFile( bool forceNew = false ) {
            string targetName = GetPath() + Path.DirectorySeparatorChar + GenerateFileName();
            if( !AllowAppend || forceNew ) {
                DisplaceFile( targetName, 0, MaxBackup );   // If we need to make a new file, make sure there isn't a file in the way
            }
            return new FileInfo( GetPath() + Path.DirectorySeparatorChar + GenerateFileName() );
        }

        /// <summary>
        /// Generates an actual filename based on a template.
        /// </summary>
        /// <param name="template">String to build the name from. If none is passed, will just use the class field.</param>
        /// <returns>String containing the filename to use.</returns>
        private string GenerateFileName( string template = null ) {
            if( template == null ) template = this.FileNameTemplate;
            template = template.Replace( ":d", DateTime.Now.ToString( "yyyyMMdd" ) );
            template = template.Replace( ":t", DateTime.Now.ToString( "hhmmss" ) );
            template = template.Replace( ":n", System.Reflection.Assembly.GetCallingAssembly().GetName().Name );
            return template;
        }

        /// <summary>
        /// Moves a file out of the way so a new log can take its place. Will shift files out of the way by appending numbers to the filename.
        /// </summary>
        /// <param name="baseFile">Full path of the space to clear.</param>
        private void DisplaceFile( string baseFile ) {
            DisplaceFile( baseFile, 0, MaxBackup );
        }

        /// <summary>
        /// Recusive method that does the work of clearing a space for new log files, only deleting the oldest.
        /// After this method runs, there will be NO file at the space specified by baseFile and stepsIn.
        /// </summary>
        /// <param name="baseFile">Full path of the target clear space</param>
        /// <param name="stepsIn">Which backup we're currently looking at (0 means the file itself, 1+ means that backup</param>
        /// <param name="stepsTotal">How many backups to max out at</param>
        private void DisplaceFile( string baseFile, int stepsIn, int stepsTotal ) {
            string thisFile = GetBackupFileName( baseFile, stepsIn );
            if( !File.Exists( thisFile ) ) return;
            if( stepsIn >= stepsTotal ) {
                File.Delete( thisFile ); // Delete if there's no more space for more backups
                return;
            }
            DisplaceFile( baseFile, stepsIn + 1, stepsTotal );
            File.Move( thisFile, GetBackupFileName( baseFile, stepsIn + 1 ) );
        }

        /// <summary>
        /// Gets the filename to use for a given backup, given the base log filename
        /// </summary>
        /// <param name="baseFile">Full path of the actual log filename</param>
        /// <param name="backupNum">Number of this backup. 0 indicates that it is not the backup, it is current.</param>
        /// <returns>Full path of the backup file</returns>
        private string GetBackupFileName( string baseFile, int backupNum ) {
            return ( backupNum == 0 ) ? baseFile : ( baseFile + '.' + backupNum.ToString() );
        }

        /// <summary>
        /// Counts the lines in a file.
        /// </summary>
        /// <param name="logFile">FileInfo object representing the file to check</param>
        /// <returns>Number of lines in the file.</returns>
        private int CountRecords( FileInfo logFile ) {
            int count = 0;
            using( StreamReader reader = new StreamReader( logFile.OpenRead() ) ) {
                while( !reader.EndOfStream ) {
                    reader.ReadLine();
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Checks to see whether the current log file limits allow for the addition of the given message.
        /// </summary>
        /// <param name="message">Message we are looking to add</param>
        /// <returns>True if message can be added, false otherwise</returns>
        private bool CanWriteToFile( string message ) {
            if( MaxFileRecords != 0 && CurrentFileRecords >= MaxFileRecords ) return false;
            if( MaxFileSize != 0 && outputStream.Length + message.Length > MaxFileSize ) return false;
            if( MaxFileDuration != null && MaxFileDuration.Ticks != 0 && DateTime.Now - CurrentFileStartTime > MaxFileDuration ) return false;
            return true;
        }

        #endregion

        #region Status control
        /// <summary>
        /// Begins a new logging session.
        /// </summary>
        /// <param name="forceNew">If true, forces creation of a new file, regardless of Append setting</param>
        /// <returns>Returns true if session start was successful, false otherwise</returns>
        public bool BeginSession( bool forceNew = false ) {
            lock( threadLock ) {
                if( forceNew ) {
                    EndSession();
                } else if( IsActiveSession ) {
                    return true;
                }

                LogFile = GetFile( forceNew );

                try {
                    bool appending = LogFile.Exists;
                    CurrentFileStartTime = ( appending && UseOriginalCreationTime ) ? LogFile.CreationTime : DateTime.Now;
                    CurrentFileRecords = ( appending && UseTotalLineCount ) ? CountRecords( LogFile ) : 0;
                    outputStream = new FileStream( LogFile.FullName, FileMode.Append, FileAccess.Write, FileShare.Read );
                    IsActiveSession = true;
                } catch( IOException ) {
                    IsActiveSession = false;
                }
                return IsActiveSession;
            }
        }

        /// <summary>
        /// Ends an active session and cleans up after it.
        /// </summary>
        public void EndSession() {
            lock( threadLock ) {
                if( outputStream != null ) {
                    outputStream.Close();
                    outputStream = null;
                }
                LogFile = null;
                this.CurrentFileRecords = -1;
                this.CurrentFileStartTime = new DateTime( 0 );
                IsActiveSession = false;
                IsSuspended = false;
            }
        }

        /// <summary>
        /// Pauses existing session without terminating it.
        /// </summary>
        public void Suspend() {
            IsSuspended = true;
        }

        /// <summary>
        /// Resumes logging in a suspended session.
        /// </summary>
        public void Resume() {
            IsSuspended = false;
        }

        /// <summary>
        /// Forces new log file to be created. Current log file will be moved to the first backup position.
        /// </summary>
        /// <returns>True if new log file was opened, false otherwise.</returns>
        public bool ForceNewFile() {
            lock( threadLock ) {
                EndSession();
                return BeginSession( true );
            }
        }

        #endregion

        #region Writers

        /// <summary>
        /// Writes specified message to the given channel.
        /// </summary>
        /// <param name="lev">Channel to output on</param>
        /// <param name="message">Template of the message to add</param>
        /// <param name="args">Format parameters</param>
        public void Write( LoggerLevel lev, string message, params object[] args ) {
            lock( threadLock ) {
                if( Level >= lev ) {
                    if( AutoSessionStart && !IsActiveSession ) {
                        BeginSession();
                    }
                    if( IsActiveSession ) {
                        string fullMessage = string.Format( "{0} - {1}: {2}{3}",
                            DateTime.Now.ToString( DateFormat ),
                            LevTxt[(int)lev],
                            string.Format( message, args ),
                            Environment.NewLine );
                        if( !CanWriteToFile( fullMessage ) ) {
                            BeginSession( true );
                        }
                        
                        byte[] output = new UTF8Encoding().GetBytes( fullMessage );
                        //byte[] output = fullMessage.ToCharArray();
                        outputStream.Write( output, 0, output.Length );
                        outputStream.Flush();
                        CurrentFileRecords++;
                    }
                }
            }
        }
        #endregion
    }
}
