using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Depressurizer.Core.Models;

namespace Depressurizer.Core.Helpers
{
    /// <summary>
    ///     Steam AppInfo.vdf Reader
    /// </summary>
    public class AppInfoReader
    {
        #region Static Fields

        private static BinaryReader _binaryReader;

        private static FileStream _fileStream;

        private const ulong Magic27 = 0x01_07_56_44_27;
        private const ulong Magic28 = 0x01_07_56_44_28;
        private const ulong Magic29 = 0x01_07_56_44_29;

        private static List<string> _stringPool;

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
                var magic = _binaryReader.ReadUInt64();
                if (magic != Magic27 && magic != Magic28 && magic != Magic29)
                {
                    throw new InvalidDataException("Invalid VDF format");
                }

                if (magic == Magic29)
                {
                    _stringPool = new List<string>();
                    var stringOffset = _binaryReader.ReadUInt64();
                    var prevOffset = _binaryReader.BaseStream.Position;
                    _binaryReader.BaseStream.Position = (long) stringOffset;
                    var stringCount = _binaryReader.ReadUInt32();
                    for (int i = 0; i < stringCount; i++)
                    {
                        _stringPool.Add(ReadString());
                    }
                    _binaryReader.BaseStream.Position = prevOffset;
                }

                while (true)
                {
                    // uint32 - AppID
                    uint id = _binaryReader.ReadUInt32();
                    if (id == 0)
                    {
                        break;
                    }

                    // Skip unused fields
                    // uint32 - size
                    // uint32 - infoState
                    // uint32 - lastUpdated
                    // uint64 - picsToken
                    // 20bytes - SHA1 of text appinfo vdf
                    // uint32 - changeNumber
                    _binaryReader.ReadBytes(44);
                    if (magic == Magic28 || magic == Magic29)
                    {
                        // 20bytes - SHA1 of binary_vdf
                        _binaryReader.ReadBytes(20);
                    }

                    // Load details
                    Items[id] = ReadEntries(magic);
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

        private static AppInfoNode ReadEntries(ulong version)
        {
            AppInfoNode result = new AppInfoNode();

            while (true)
            {
                byte type = _binaryReader.ReadByte();
                if (type == 0x08)
                {
                    break;
                }

                string key = version == Magic29 ? ReadString29() : ReadString();

                switch (type)
                {
                    case 0x00:
                        result[key] = ReadEntries(version);

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

        private static string ReadString29()
        {
            uint index = _binaryReader.ReadUInt32();
            return _stringPool[(int) index];
        }

        #endregion
    }
}
