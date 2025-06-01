using Depressurizer.Core.Models;
using Sentry.Protocol;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ValveKeyValue;

namespace Depressurizer.Core.Helpers
{
    public enum EUniverse
    {
        Invalid = 0,
        Public = 1,
        Beta = 2,
        Internal = 3,
        Dev = 4,
        Max = 5,
    }

    /// <summary>
    ///     Steam AppInfo.vdf Reader
    /// </summary>
    public class AppInfoReader
    {
        private const uint Magic29 = 0x07_56_44_29;
        private const uint Magic28 = 0x07_56_44_28;
        private const uint Magic27 = 0x07_56_44_27;

        public EUniverse Universe { get; set; }
        public Dictionary<uint, AppInfoNode> Items { get; } = new Dictionary<uint, AppInfoNode>();

        /// <summary>
        ///     Steam AppInfo.vdf Reader
        /// </summary>
        /// <param name="path">appinfo.vdf path</param>
        public AppInfoReader(string path)
        {
            try
            {
                var _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var reader = new BinaryReader(_fileStream);

                // Read some header fields
                var magic = reader.ReadUInt32();
                if (magic != Magic27 && magic != Magic28 && magic != Magic29)
                {
                    throw new InvalidDataException("Invalid VDF format");
                }

                Universe = (EUniverse)reader.ReadUInt32();

                var options = new KVSerializerOptions();

                if (magic == Magic29)
                {
                    var stringTableOffset = reader.ReadInt64();
                    var offset = reader.BaseStream.Position;
                    reader.BaseStream.Position = stringTableOffset;
                    var stringCount = reader.ReadUInt32();
                    var stringPool = new string[stringCount];

                    for (var i = 0; i < stringCount; i++)
                    {
                        stringPool[i] = ReadNullTermUtf8String(reader.BaseStream);
                    }

                    reader.BaseStream.Position = offset;

                    options.StringTable = new(stringPool);
                }

                var deserializer = KVSerializer.Create(KVSerializationFormat.KeyValues1Binary);

                while (true)
                {
                    var appid = reader.ReadUInt32();

                    if (appid == 0)
                    {
                        break;
                    }

                    var size = reader.ReadUInt32(); // size until end of Data
                    var end = reader.BaseStream.Position + size;

                    var infoState = reader.ReadUInt32();
                    var lastUpdated = DateTimeFromUnixTime(reader.ReadUInt32());
                    var token = reader.ReadUInt64();
                    var hash = new ReadOnlyCollection<byte>(reader.ReadBytes(20));
                    var changeNumber = reader.ReadUInt32();

                    if (magic == Magic28 || magic == Magic29)
                    {
                        var binaryDataHash = new ReadOnlyCollection<byte>(reader.ReadBytes(20));
                    }

                    var data = deserializer.Deserialize(_fileStream, options);

                    if (reader.BaseStream.Position != end)
                    {
                        throw new InvalidDataException();
                    }

                    Items[appid] = new("appinfo");
                    Items[appid]["appinfo"] = ReadEntries(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
        }

        private static string ReadNullTermUtf8String(Stream stream)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(32);

            try
            {
                var position = 0;

                do
                {
                    var b = stream.ReadByte();

                    if (b <= 0) // null byte or stream ended
                    {
                        break;
                    }

                    if (position >= buffer.Length)
                    {
                        var newBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length * 2);
                        Buffer.BlockCopy(buffer, 0, newBuffer, 0, buffer.Length);
                        ArrayPool<byte>.Shared.Return(buffer);
                        buffer = newBuffer;
                    }

                    buffer[position++] = (byte)b;
                }
                while (true);

                return Encoding.UTF8.GetString(buffer[..position]);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        private static DateTime DateTimeFromUnixTime(uint unixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);
        }

        private static AppInfoNode ReadEntries(IEnumerable<KVObject> data)
        {
            AppInfoNode result = new AppInfoNode();

            foreach (var item in data)
            {
                if (item.Children.Any())
                    result[item.Name] = ReadEntries(item.Children);
                else
                    result[item.Name] = new AppInfoNode(item.Value.ToString());
            }

            return result;
        }
    }
}
