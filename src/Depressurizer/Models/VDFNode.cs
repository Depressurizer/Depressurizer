#region LICENSE

//     This file (VDFNode.cs) is part of Depressurizer.
//     Copyright (C) 2017 Martijn Vegter
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
using System.Linq;
using System.Text;
using ValueType = Depressurizer.Enums.ValueType;

namespace Depressurizer.Models
{
    /// <summary>
    ///     ValveDataFormat Node
    /// </summary>
    public sealed class VDFNode
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Creates an Array-type VDFNode
        /// </summary>
        public VDFNode()
        {
            NodeType = ValueType.Array;
            NodeData = new Dictionary<string, VDFNode>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Creates an String-type VDFNode
        /// </summary>
        /// <param name="value"></param>
        public VDFNode(string value)
        {
            NodeType = ValueType.String;
            NodeData = value;
        }

        /// <summary>
        ///     Creates an Integer-type VDFNode
        /// </summary>
        /// <param name="value"></param>
        public VDFNode(int value)
        {
            NodeType = ValueType.Int;
            NodeData = value;
        }

        /// <summary>
        ///     Creates an Integer-type VDFNode
        /// </summary>
        /// <param name="value"></param>
        public VDFNode(ulong value)
        {
            NodeType = ValueType.Int;
            NodeData = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     NodeData casted to a Dictionary.
        /// </summary>
        public Dictionary<string, VDFNode> NodeArray
        {
            get
            {
                if (NodeType != ValueType.Array || !(NodeData is Dictionary<string, VDFNode> arrayData))
                {
                    return null;
                }

                return arrayData;
            }
        }

        /// <summary>
        ///     Can be an Int, String or Dictionary.
        /// </summary>
        public object NodeData { get; set; }

        /// <summary>
        ///     NodeData casted to an Integer.
        /// </summary>
        public int NodeInt
        {
            get
            {
                if (NodeType == ValueType.Int)
                {
                    return (int) NodeData;
                }

                if (NodeType == ValueType.String)
                {
                    if (int.TryParse(NodeString, out int res))
                    {
                        return res;
                    }
                }

                return 0;
            }
        }

        /// <summary>
        ///     NodeData casted to an String.
        /// </summary>
        public string NodeString
        {
            get
            {
                if (NodeType != ValueType.String || !(NodeData is string stringData))
                {
                    return null;
                }

                return stringData;
            }
        }

        /// <summary>
        ///     NodeData Type
        /// </summary>
        public ValueType NodeType { get; set; }

        #endregion

        #region Public Indexers

        /// <summary>
        ///     Get or Sets the SubNode at the given index. If there is no SubNode found then it creates an Array-type SubNode.
        /// </summary>
        /// <param name="index">Index to Get or Set.</param>
        /// <returns>SubNode at the given index.</returns>
        public VDFNode this[string index]
        {
            get
            {
                if (NodeType != ValueType.Array || !(NodeData is Dictionary<string, VDFNode> arrayData))
                {
                    return null;
                }

                if (!arrayData.ContainsKey(index))
                {
                    arrayData.Add(index, new VDFNode());
                }

                return arrayData[index];
            }
            set
            {
                if (NodeType != ValueType.Array || !(NodeData is Dictionary<string, VDFNode> arrayData))
                {
                    return;
                }

                if (!arrayData.ContainsKey(index))
                {
                    arrayData.Add(index, value);
                }
                else
                {
                    arrayData[index] = value;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Loads a VDFNode from a stream.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        /// <returns>VDFNode representing the content of the given stream.</returns>
        public static VDFNode LoadFromBinary(BinaryReader stream)
        {
            return LoadFromBinary(stream, stream.BaseStream.Length);
        }

        /// <summary>
        ///     Loads a VDFNode from a stream.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        /// <param name="streamLength">Length of stream in bytes.</param>
        /// <returns>VDFNode representing the content of the given stream.</returns>
        public static VDFNode LoadFromBinary(BinaryReader stream, long streamLength)
        {
            if (stream.BaseStream.Position == streamLength)
            {
                return null;
            }

            VDFNode thisLevel = new VDFNode();

            bool endOfStream = false;

            while (true)
            {
                byte nextByte;
                try
                {
                    nextByte = stream.ReadByte();
                }
                catch (EndOfStreamException)
                {
                    endOfStream = true;
                    nextByte = 8;
                }

                if (endOfStream || nextByte == 8 || stream.BaseStream.Position == streamLength)
                {
                    break;
                }

                string key;

                switch (nextByte)
                {
                    case 0:
                    {
                        key = ReadBin_GetStringToken(stream);
                        VDFNode newNode = LoadFromBinary(stream, streamLength);
                        thisLevel[key] = newNode;

                        break;
                    }

                    case 1:
                        key = ReadBin_GetStringToken(stream);
                        thisLevel[key] = new VDFNode(ReadBin_GetStringToken(stream));

                        break;

                    case 2:
                    {
                        key = ReadBin_GetStringToken(stream);
                        int val = stream.ReadInt32();
                        thisLevel[key] = new VDFNode(val);

                        break;
                    }

                    case 7:
                    {
                        key = ReadBin_GetStringToken(stream);
                        ulong val = stream.ReadUInt64();
                        thisLevel[key] = new VDFNode(val);

                        break;
                    }

                    case 0xFF:

                        return null;

                    default:

                        throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Unexpected Character Key '{0}'", nextByte));
                }
            }

            return thisLevel;
        }

        /// <summary>
        ///     Loads a VDFNode from a stream.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        /// <returns>VDFNode representing the content of the given stream.</returns>
        public static VDFNode LoadFromText(StreamReader stream)
        {
            return LoadFromText(stream, false);
        }

        public static VDFNode LoadFromText(StreamReader stream, bool useFirstAsRoot)
        {
            VDFNode thisLevel = useFirstAsRoot ? null : new VDFNode();

            ReadText_SkipWhitespace(stream);

            while (!stream.EndOfStream)
            {
                ReadText_SkipWhitespace(stream);

                // Get key
                char nextChar = (char) stream.Read();
                if (stream.EndOfStream || nextChar == '}')
                {
                    break;
                }

                if (nextChar != '"')
                {
                    throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Unexpected Character Key '{0}'", nextChar));
                }

                string key = ReadText_GetStringToken(stream);

                ReadText_SkipWhitespace(stream);

                // Get value
                nextChar = (char) stream.Read();
                VDFNode newNode;
                if (nextChar == '"')
                {
                    newNode = new VDFNode(ReadText_GetStringToken(stream));
                }
                else if (nextChar == '{')
                {
                    newNode = LoadFromText(stream);
                }
                else
                {
                    throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Unexpected Character Value '{0}'", nextChar));
                }

                if (useFirstAsRoot)
                {
                    return newNode;
                }

                thisLevel[key] = newNode;
            }

            return thisLevel;
        }

        public static void ReadBin_SeekTo(BinaryReader binaryReader, byte[] bytes, long streamLength)
        {
            int indexAt = 0;

            while (indexAt < bytes.Length && binaryReader.BaseStream.Position < streamLength)
            {
                if (binaryReader.ReadByte() == bytes[indexAt])
                {
                    indexAt++;
                }
                else
                {
                    indexAt = 0;
                }
            }
        }

        public void CleanTree()
        {
            Dictionary<string, VDFNode> nodes = NodeArray;
            if (nodes == null)
            {
                return;
            }

            string[] keys = nodes.Keys.ToArray();
            foreach (string key in keys)
            {
                nodes[key].CleanTree();
                if (nodes[key].IsEmpty())
                {
                    NodeArray.Remove(key);
                }
            }
        }

        /// <summary>
        ///     Checks if the given index exists in the current Node (must be an Array-type).
        /// </summary>
        /// <param name="index">Index to look for.</param>
        /// <returns>True if the given index was found, false if not found.</returns>
        /// <exception cref="InvalidDataException"></exception>
        public bool ContainsKey(string index)
        {
            if (NodeType != ValueType.Array || !(NodeData is Dictionary<string, VDFNode> arrayData))
            {
                throw new InvalidCastException("NodeData is not a Dictionary<string, VDFNode>");
            }

            return arrayData.ContainsKey(index);
        }

        public VDFNode GetNodeAt(string[] args)
        {
            return GetNodeAt(args, true, 0);
        }

        public VDFNode GetNodeAt(string[] args, bool create)
        {
            return GetNodeAt(args, create, 0);
        }

        public VDFNode GetNodeAt(string[] args, bool create, int index)
        {
            if (index >= args.Length)
            {
                return this;
            }

            if (NodeType != ValueType.Array)
            {
                return null;
            }

            if (!(NodeData is Dictionary<string, VDFNode> arrayData))
            {
                throw new InvalidCastException("NodeData is not a Dictionary<string, VDFNode>");
            }

            if (ContainsKey(args[index]))
            {
                return arrayData[args[index]].GetNodeAt(args, create, index + 1);
            }

            if (!create)
            {
                return null;
            }

            VDFNode newNode = new VDFNode();
            arrayData.Add(args[index], newNode);

            return newNode.GetNodeAt(args, true, index + 1);
        }

        public void MakeArray()
        {
            if (NodeType == ValueType.Array)
            {
                return;
            }

            NodeType = ValueType.Array;
            NodeData = new Dictionary<string, VDFNode>(StringComparer.OrdinalIgnoreCase);
        }

        public bool RemoveSubNode(string key)
        {
            return NodeType == ValueType.Array && NodeArray.Remove(key);
        }

        public void SaveAsBinary(BinaryWriter binaryWriter)
        {
            SaveAsBinary(binaryWriter, null);
        }

        public void SaveAsBinary(BinaryWriter binaryWriter, string actualKey)
        {
            switch (NodeType)
            {
                case ValueType.Array:
                    if (!string.IsNullOrEmpty(actualKey))
                    {
                        WriteBin_WriteArrayKey(binaryWriter, actualKey);
                    }

                    Dictionary<string, VDFNode> data = NodeArray;
                    foreach (KeyValuePair<string, VDFNode> entry in data)
                    {
                        entry.Value.SaveAsBinary(binaryWriter, entry.Key);
                    }

                    WriteBin_WriteEndByte(binaryWriter);

                    break;
                case ValueType.String:
                    if (!string.IsNullOrEmpty(actualKey))
                    {
                        WriteBin_WriteStringValue(binaryWriter, actualKey, NodeString);
                    }

                    break;
                case ValueType.Int:
                    if (!string.IsNullOrEmpty(actualKey))
                    {
                        WriteBin_WriteIntegerValue(binaryWriter, actualKey, NodeInt);
                    }

                    break;
                default:

                    throw new ArgumentOutOfRangeException(nameof(NodeType), NodeType, null);
            }
        }

        /// <summary>
        ///     Writes the current VDFNode to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        public void SaveAsText(StreamWriter stream)
        {
            SaveAsText(stream, 0);
        }

        /// <summary>
        ///     Writes the current VDFNode to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="indent">Indentation level of each line.</param>
        public void SaveAsText(StreamWriter stream, int indent)
        {
            if (NodeType != ValueType.Array)
            {
                return;
            }

            Dictionary<string, VDFNode> data = NodeArray;
            foreach (KeyValuePair<string, VDFNode> entry in data)
            {
                if (entry.Value.NodeType == ValueType.Array)
                {
                    WriteText_WriteWhitespace(stream, indent);
                    WriteText_WriteFormattedString(stream, entry.Key);
                    stream.WriteLine();

                    WriteText_WriteWhitespace(stream, indent);
                    stream.WriteLine('{');

                    entry.Value.SaveAsText(stream, indent + 1);

                    WriteText_WriteWhitespace(stream, indent);
                    stream.WriteLine('}');
                }
                else
                {
                    WriteText_WriteWhitespace(stream, indent);
                    WriteText_WriteFormattedString(stream, entry.Key);
                    stream.Write("\t\t");

                    WriteText_WriteFormattedString(stream, entry.Value.NodeData.ToString());
                    stream.WriteLine();
                }
            }
        }

        #endregion

        #region Methods

        private static string ReadBin_GetStringToken(BinaryReader reader, long streamLength = -1)
        {
            if (streamLength == -1)
            {
                streamLength = reader.BaseStream.Length;
            }

            bool endOfStream = false;
            bool stringDone = false;
            List<byte> bytes = new List<byte>();
            do
            {
                try
                {
                    byte b = reader.ReadByte();
                    if (b == 0)
                    {
                        stringDone = true;
                    }
                    else
                    {
                        bytes.Add(b);
                    }
                }
                catch (EndOfStreamException)
                {
                    endOfStream = true;
                }
            } while (!stringDone && !endOfStream && reader.BaseStream.Position < streamLength);

            if (!stringDone)
            {
                if (endOfStream)
                {
                    throw new InvalidDataException("Unexpected End of File");
                }
            }

            string token = Encoding.UTF8.GetString(bytes.ToArray());

            return token;
        }

        private static string ReadText_GetStringToken(StreamReader stream)
        {
            bool escaped = false;
            bool stringDone = false;
            StringBuilder sb = new StringBuilder();
            char nextChar;
            do
            {
                nextChar = (char) stream.Read();
                if (escaped)
                {
                    switch (nextChar)
                    {
                        case '\\':
                            sb.Append('\\');

                            break;
                        case '"':
                            sb.Append('"');

                            break;
                        case '\'':
                            sb.Append('\'');

                            break;
                    }

                    escaped = false;
                }
                else
                {
                    switch (nextChar)
                    {
                        case '\\':
                            escaped = true;

                            break;
                        case '"':
                            stringDone = true;

                            break;
                        default:
                            sb.Append(nextChar);

                            break;
                    }
                }
            } while (!stringDone && !stream.EndOfStream);

            if (!stringDone)
            {
                if (stream.EndOfStream)
                {
                    throw new InvalidDataException("Unexpected End of File");
                }
            }

            return sb.ToString();
        }

        private static void ReadText_SkipWhitespace(StreamReader stream)
        {
            char nextChar = (char) stream.Peek();
            while (nextChar == ' ' || nextChar == '\r' || nextChar == '\n' || nextChar == '\t')
            {
                stream.Read();
                nextChar = (char) stream.Peek();
            }
        }

        private static void WriteBin_WriteArrayKey(BinaryWriter writer, string arrayKey)
        {
            writer.Write((byte) 0);
            writer.Write(arrayKey.ToCharArray());
            writer.Write((byte) 0);
        }

        private static void WriteBin_WriteEndByte(BinaryWriter writer)
        {
            writer.Write((byte) 8);
        }

        private static void WriteBin_WriteIntegerValue(BinaryWriter writer, string key, int val)
        {
            writer.Write((byte) 2);
            writer.Write(key.ToCharArray());
            writer.Write((byte) 0);
            writer.Write(val);
        }

        private static void WriteBin_WriteStringValue(BinaryWriter writer, string key, string val)
        {
            writer.Write((byte) 1);
            writer.Write(key.ToCharArray());
            writer.Write((byte) 0);
            writer.Write(val.ToCharArray());
            writer.Write((byte) 0);
        }

        private static void WriteText_WriteFormattedString(StreamWriter stream, string s)
        {
            stream.Write("\"");
            stream.Write(s.Replace("\"", "\\\""));
            stream.Write("\"");
        }

        private static void WriteText_WriteWhitespace(StreamWriter stream, int indent)
        {
            for (int i = 0; i < indent; i++)
            {
                stream.Write('\t');
            }
        }

        private bool IsEmpty()
        {
            if (NodeArray != null)
            {
                return NodeArray.Count == 0;
            }

            return !(NodeData is string);
        }

        #endregion
    }
}
