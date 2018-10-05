#region LICENSE

//     This file (AppInfoReader.cs) is part of Depressurizer.
//     Copyright (C) 2018 Martijn Vegter
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
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Depressurizer.Models;

namespace Depressurizer.Helpers
{
    /// <summary>
    ///     Steam AppInfo.vdf Reader
    /// </summary>
    public class AppInfoReader
    {
        #region Static Fields

        private static BinaryReader _binaryReader;

        private static FileStream _fileStream;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Steam AppInfo.vdf Reader
        /// </summary>
        /// <param name="path">appinfo.vdf path</param>
        public AppInfoReader(string path)
        {
            try
            {
                _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                _binaryReader = new BinaryReader(_fileStream);

                // Read some header fields
                _binaryReader.ReadByte();
                if (_binaryReader.ReadByte() != 0x44 || _binaryReader.ReadByte() != 0x56)
                {
                    throw new InvalidDataException("Invalid VDF format");
                }

                // Skip more header fields
                _binaryReader.ReadBytes(5);

                while (true)
                {
                    uint id = _binaryReader.ReadUInt32();
                    if (id == 0)
                    {
                        break;
                    }

                    // Skip unused fields
                    _binaryReader.ReadBytes(44);

                    // Load details
                    Items[id] = ReadEntries();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
            finally
            {
                if (_fileStream != null)
                {
                    _fileStream.Dispose();
                }

                if (_binaryReader != null)
                {
                    _binaryReader.Dispose();
                }
            }
        }

        #endregion

        #region Public Properties

        public Dictionary<uint, AppInfoNode> Items { get; } = new Dictionary<uint, AppInfoNode>();

        #endregion

        #region Methods

        private static AppInfoNode ReadEntries()
        {
            AppInfoNode result = new AppInfoNode();

            while (true)
            {
                byte type = _binaryReader.ReadByte();
                if (type == 0x08)
                {
                    break;
                }

                string key = ReadString();

                switch (type)
                {
                    case 0x00:
                        result[key] = ReadEntries();

                        break;
                    case 0x01:
                        result[key] = new AppInfoNode(ReadString());

                        break;
                    case 0x02:
                        result[key] = new AppInfoNode(_binaryReader.ReadUInt32().ToString(CultureInfo.InvariantCulture));

                        break;
                    default:

                        throw new ArgumentOutOfRangeException(string.Format(CultureInfo.InvariantCulture, "Unknown entry type '{0}'", type));
                }
            }

            return result;
        }

        private static string ReadString()
        {
            List<byte> bytes = new List<byte>();

            try
            {
                bool stringDone = false;
                do
                {
                    byte b = _binaryReader.ReadByte();
                    if (b == 0)
                    {
                        stringDone = true;
                    }
                    else
                    {
                        bytes.Add(b);
                    }
                } while (!stringDone);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }

            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        #endregion
    }
}
