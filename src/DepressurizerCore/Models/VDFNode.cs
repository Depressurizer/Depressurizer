#region LICENSE

//     This file (VDFNode.cs) is part of DepressurizerCore.
//     Original Copyright (C) 2011  Steve Labbe
//     Modified Copyright (C) 2018  Martijn Vegter
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
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DepressurizerCore.Models
{
    /// <summary>
    ///     ValveDataFormat Node
    /// </summary>
    public sealed class VDFNode
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Creates a new array-type node
        /// </summary>
        public VDFNode()
        {
            NodeType = ValueType.Array;
            NodeData = new Dictionary<string, VDFNode>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Creates a new string-type node
        /// </summary>
        /// <param name="value">Value of the string</param>
        public VDFNode(string value)
        {
            NodeType = ValueType.String;
            NodeData = value;
        }

        /// <summary>
        ///     Creates a new integer-type node
        /// </summary>
        /// <param name="value">Value of the integer</param>
        public VDFNode(int value)
        {
            NodeType = ValueType.Int;
            NodeData = value;
        }

        /// <summary>
        ///     Creates a new UInt64-type node
        /// </summary>
        /// <param name="value">Value of the unsigned 64-bit integer</param>
        public VDFNode(ulong value)
        {
            NodeType = ValueType.Int;
            NodeData = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Quick shortcut for casting data to a a dictionary
        /// </summary>
        public Dictionary<string, VDFNode> NodeArray => NodeType == ValueType.Array ? NodeData as Dictionary<string, VDFNode> : null;

        /// <summary>
        ///     Can be an int, string or Dictionary
        /// </summary>
        public object NodeData { get; set; }

        /// <summary>
        ///     Quick shortcut for casting data to int. If the node is a string, tries to parse to int. Returns 0 if failure.
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
        ///     Quick shortcut for casting data to string
        /// </summary>
        public string NodeString => NodeType == ValueType.String ? NodeData as string : null;

        public ValueType NodeType { get; set; }

        #endregion

        #region Public Indexers

        /// <summary>
        ///     Gets or sets the subnode with the given key. Can only be used with an array node. If the subnode does not exist,
        ///     creates it as an array type.
        /// </summary>
        /// <param name="key">Key to look for or set</param>
        /// <returns></returns>
        public VDFNode this[string key]
        {
            get
            {
                if (NodeType != ValueType.Array)
                {
                    throw new ArgumentException("NodeType must be an Array");
                }

                if (!(NodeData is Dictionary<string, VDFNode> arrayData))
                {
                    throw new ArgumentException("NodeData is not a Dictionary<string, VDFNode>");
                }

                if (!arrayData.ContainsKey(key))
                {
                    arrayData.Add(key, new VDFNode());
                }

                return arrayData[key];
            }
            set
            {
                if (NodeType == ValueType.String)
                {
                    throw new ArgumentException("NodeType cannot be an String");
                }

                if (!(NodeData is Dictionary<string, VDFNode> arrayData))
                {
                    throw new ArgumentException("NodeData is not a Dictionary<string, VDFNode>");
                }

                if (!arrayData.ContainsKey(key))
                {
                    arrayData.Add(key, value);
                }
                else
                {
                    arrayData[key] = value;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Loads a FileNode from stream.
        /// </summary>
        /// <param name="binaryReader">Stream to load from</param>
        /// <returns>FileNode representing the contents of the stream.</returns>
        public static VDFNode LoadFromBinary(BinaryReader binaryReader)
        {
            long streamLength = binaryReader.BaseStream.Length;

            if (binaryReader.BaseStream.Position == streamLength)
            {
                return null;
            }

            VDFNode thisLevel = new VDFNode();

            bool endOfStream = false;

            while (!endOfStream)
            {
                byte nextByte;
                try
                {
                    nextByte = binaryReader.ReadByte();
                }
                catch (EndOfStreamException)
                {
                    endOfStream = true;
                    nextByte = 8;
                }

                // Get key
                string key = null;
                if (endOfStream || nextByte == 8 || binaryReader.BaseStream.Position == streamLength)
                {
                    break;
                }

                if (nextByte == 0)
                {
                    key = ReadBin_GetStringToken(binaryReader);
                    VDFNode newNode;
                    newNode = LoadFromBinary(binaryReader);
                    thisLevel[key] = newNode;
                }
                else if (nextByte == 1)
                {
                    key = ReadBin_GetStringToken(binaryReader);
                    thisLevel[key] = new VDFNode(ReadBin_GetStringToken(binaryReader));
                }
                else if (nextByte == 2)
                {
                    key = ReadBin_GetStringToken(binaryReader);
                    int val = binaryReader.ReadInt32();
                    thisLevel[key] = new VDFNode(val);
                }
                else if (nextByte == 7)
                {
                    key = ReadBin_GetStringToken(binaryReader);
                    ulong val = binaryReader.ReadUInt64();
                    thisLevel[key] = new VDFNode(val);
                }
                else if (nextByte == 0xFF)
                {
                    return null;
                }
                else
                {
                    throw new DataException(string.Format(CultureInfo.InvariantCulture, "Unexpected character '{0}' found when expecting key.", nextByte));
                }
            }

            return thisLevel;
        }

        /// <summary>
        ///     Loads a FileNode from stream.
        /// </summary>
        /// <param name="stream">Stream to load from</param>
        /// <param name="useFirstAsRoot"></param>
        /// <returns>FileNode representing the contents of the stream.</returns>
        public static VDFNode LoadFromText(StreamReader stream, bool useFirstAsRoot = false)
        {
            VDFNode thisLevel = useFirstAsRoot ? null : new VDFNode();

            ReadText_SkipWhitespace(stream);

            while (!stream.EndOfStream)
            {
                ReadText_SkipWhitespace(stream);
                // Get key
                char nextChar = (char) stream.Read();
                string key = null;
                if (stream.EndOfStream || nextChar == '}')
                {
                    break;
                }

                if (nextChar == '"')
                {
                    key = ReadText_GetStringToken(stream);
                }
                else
                {
                    throw new DataException(string.Format(CultureInfo.InvariantCulture, "Unexpected character '{0}' found when expecting key.", nextChar));
                }

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
                    throw new DataException(string.Format(CultureInfo.InvariantCulture, "Unexpected character '{0}' found when expecting value.", nextChar));
                }

                if (useFirstAsRoot)
                {
                    return newNode;
                }

                thisLevel[key] = newNode;
            }

            return thisLevel;
        }

        public static void ReadBin_SeekTo(BinaryReader stream, byte[] str, long fileLength)
        {
            int indexAt = 0;

            while (indexAt < str.Length && stream.BaseStream.Position < fileLength)
            {
                if (stream.ReadByte() == str[indexAt])
                {
                    indexAt++;
                }
                else
                {
                    indexAt = 0;
                }
            }
        }

        /// <summary>
        ///     Removes any array nodes without any value-type children
        /// </summary>
        public void CleanTree()
        {
            Dictionary<string, VDFNode> nodes = NodeArray;
            if (nodes != null)
            {
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
        }

        /// <summary>
        ///     Checks whether the given key exists within an array-type node
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <returns>True if the key was found, false otherwise</returns>
        public bool ContainsKey(string key)
        {
            if (NodeType != ValueType.Array)
            {
                return false;
            }

            return ((Dictionary<string, VDFNode>) NodeData).ContainsKey(key);
        }

        /// <summary>
        ///     Gets the node at the given address. May be used to build structure.
        /// </summary>
        /// <param name="args">An ordered list of keys, like a path</param>
        /// <param name="create">If true, will create any nodes it does not find along the path.</param>
        /// <param name="index">Start index of the arg array</param>
        /// <returns>The FileNode at the given location, or null if the location was not found / created</returns>
        public VDFNode GetNodeAt(string[] args, bool create = true, int index = 0)
        {
            if (index >= args.Length)
            {
                return this;
            }

            if (NodeType == ValueType.Array)
            {
                Dictionary<string, VDFNode> data = (Dictionary<string, VDFNode>) NodeData;
                if (ContainsKey(args[index]))
                {
                    return data[args[index]].GetNodeAt(args, create, index + 1);
                }

                if (create)
                {
                    VDFNode newNode = new VDFNode();
                    data.Add(args[index], newNode);

                    return newNode.GetNodeAt(args, create, index + 1);
                }
            }

            return null;
        }

        public void MakeArray()
        {
            if (NodeType != ValueType.Array)
            {
                NodeType = ValueType.Array;
                NodeData = new Dictionary<string, VDFNode>(StringComparer.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        ///     Removes the subnode with the given key. Can only be called on array nodes.
        /// </summary>
        /// <param name="key">Key of the subnode to remove</param>
        /// <returns>True if node was removed, false if not found</returns>
        public bool RemoveSubnode(string key)
        {
            if (NodeType != ValueType.Array)
            {
                return false;
            }

            return NodeArray.Remove(key);
        }

        /* This isn't perfect, and it's not needed for now, but it allows reading both the common AND extended elements from AppInfo.vdf
        public static VDFNode LoadFromBinary_AppInfo( BinaryReader stream, int inNodeType, long streamLength ) {
            if( stream.BaseStream.Position == streamLength ) return null;
            VDFNode thisLevel = new VDFNode();

            bool endOfStream = false;

            while( !endOfStream ) {

                byte nextByte = 0;
                byte nextNextByte = 0;
                try {
                    nextByte = stream.ReadByte();
                    nextNextByte = (byte)stream.PeekChar();
                } catch( EndOfStreamException ) {
                    endOfStream = true;
                }
                // Get key
                string key = null;
                if( endOfStream || stream.BaseStream.Position == streamLength ) {
                    break;
                }
                if( nextByte == 8 ) {
                    if( inNodeType == 2 || inNodeType == 3 ) {
                        while( stream.PeekChar() == 8 ) stream.ReadByte();
                    }
                    break;
                } else if( ( nextByte == 2 || nextByte == 3 ) && nextNextByte == 0 ) {
                    stream.ReadByte();
                    key = ReadBin_GetStringToken( stream );
                    VDFNode newNode;
                    newNode = LoadFromBinary_AppInfo( stream, nextByte, streamLength );
                    thisLevel[key] = newNode;
                    if( nextByte == 3 ) break;
                } else if( nextNextByte == 0 ) {
                    break;
                } else if( nextByte == 0 ) {
                    if( inNodeType == 2 || inNodeType == 3  ) {
                    key = ReadBin_GetStringToken( stream );
                    VDFNode newNode;
                    newNode = LoadFromBinary_AppInfo( stream, 0, streamLength );
                    thisLevel[key] = newNode;
                    } else {
                        break;
                    }
                } else if( nextByte == 1 ) {
                    key = ReadBin_GetStringToken( stream );
                    thisLevel[key] = new VDFNode( ReadBin_GetStringToken( stream ) );
                } else if( nextByte == 2 ) {
                    key = ReadBin_GetStringToken( stream );
                    int val = stream.ReadInt32();
                    thisLevel[key] = new VDFNode( val );
                } else if( nextByte == 0xFF ) {
                    return null;
                } else {
                    break;
                    throw new ParseException( string.Format( GlobalStrings.TextVdfFile_UnexpectedCharacterKey, nextByte.ToString() ) );
                }
            }
            return thisLevel;
        }
         */

        /// <summary>
        ///     Writes this FileNode and childs to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="actualKey">Name of node to write.</param>
        public void SaveAsBinary(BinaryWriter stream, string actualKey = null)
        {
            switch (NodeType)
            {
                case ValueType.Array:
                    if (!string.IsNullOrEmpty(actualKey))
                    {
                        WriteBin_WriteArrayKey(stream, actualKey);
                    }

                    Dictionary<string, VDFNode> data = NodeArray;
                    foreach (KeyValuePair<string, VDFNode> entry in data)
                    {
                        entry.Value.SaveAsBinary(stream, entry.Key);
                    }

                    WriteBin_WriteEndByte(stream);

                    break;
                case ValueType.String:
                    if (!string.IsNullOrEmpty(actualKey))
                    {
                        WriteBin_WriteStringValue(stream, actualKey, NodeString);
                    }

                    break;
                case ValueType.Int:
                    if (!string.IsNullOrEmpty(actualKey))
                    {
                        WriteBin_WriteIntegerValue(stream, actualKey, NodeInt);
                    }

                    break;
            }
        }

        /// <summary>
        ///     Writes this FileNode to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="indent">Indentation level of each line.</param>
        public void SaveAsText(StreamWriter stream, int indent = 0)
        {
            if (NodeType == ValueType.Array)
            {
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
                    throw new DataException("Unexpected end-of-file reached: Unterminated string.");
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
                    throw new DataException("Unexpected end-of-file reached: Unterminated string.");
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

            return NodeData as string == null;
        }

        #endregion
    }
}