/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Depressurizer
{
    public enum ValueType
    {
        Array,
        String,
        Int
    }

    public class VdfFileNode
    {
        public ValueType NodeType;

        // Can be an int, string or Dictionary<string,VdfFileNode>
        public Object NodeData;

        /// <summary>
        /// Gets or sets the subnode with the given key. Can only be used with an array node. If the subnode does not exist, creates it as an array type.
        /// </summary>
        /// <param name="key">Key to look for or set</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">Thrown if used on a value node.</exception>
        public VdfFileNode this[string key]
        {
            get
            {
                if (NodeType != ValueType.Array)
                {
                    throw new ApplicationException(string.Format(GlobalStrings.TextVdfFile_CanNotGetKey, key));
                }
                Dictionary<string, VdfFileNode> arrayData = (Dictionary<string, VdfFileNode>) NodeData;
                if (!arrayData.ContainsKey(key))
                {
                    arrayData.Add(key, new VdfFileNode());
                }
                return arrayData[key];
            }
            set
            {
                if (NodeType == ValueType.String)
                {
                    throw new ApplicationException(string.Format(GlobalStrings.TextVdfFile_CanNotSetKey, key));
                }
                Dictionary<string, VdfFileNode> arrayData = (Dictionary<string, VdfFileNode>) NodeData;
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

        /// <summary>
        /// Quick shortcut for casting data to a a dictionary
        /// </summary>
        public Dictionary<string, VdfFileNode> NodeArray
        {
            get { return (NodeType == ValueType.Array) ? (NodeData as Dictionary<string, VdfFileNode>) : null; }
        }

        /// <summary>
        /// Quick shortcut for casting data to string
        /// </summary>
        public string NodeString
        {
            get { return (NodeType == ValueType.String) ? (NodeData as string) : null; }
        }

        /// <summary>
        /// Quick shortcut for casting data to int. If the node is a string, tries to parse to int. Returns 0 if failure.
        /// </summary>
        public int NodeInt
        {
            get
            {
                if (NodeType == ValueType.Int)
                    return ((int) NodeData);
                if (NodeType == ValueType.String)
                {
                    int res = 0;
                    int.TryParse(NodeString, out res);
                    return res;
                }
                return 0;
            }
        }

        /// <summary>
        /// Creates a new array-type node
        /// </summary>
        public VdfFileNode()
        {
            NodeType = ValueType.Array;
            NodeData = new Dictionary<string, VdfFileNode>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Creates a new string-type node
        /// </summary>
        /// <param name="value">Value of the string</param>
        public VdfFileNode(string value)
        {
            NodeType = ValueType.String;
            NodeData = value;
        }

        /// <summary>
        /// Creates a new integer-type node
        /// </summary>
        /// <param name="value">Value of the integer</param>
        public VdfFileNode(int value)
        {
            NodeType = ValueType.Int;
            NodeData = value;
        }

        /// <summary>
        /// Creates a new UInt64-type node
        /// </summary>
        /// <param name="value">Value of the unsigned 64-bit integer</param>
        public VdfFileNode(ulong value)
        {
            NodeType = ValueType.Int;
            NodeData = value;
        }

        #region Utility

        /// <summary>
        /// Checks whether or not this node has any children
        /// </summary>
        /// <returns>True if an array with no children, false otherwise</returns>
        protected bool IsEmpty()
        {
            if (NodeArray != null)
            {
                return NodeArray.Count == 0;
            }
            return (NodeData as string) == null;
        }

        #endregion

        #region Accessors

        /// <summary>
        /// Gets the node at the given address. May be used to build structure.
        /// </summary>
        /// <param name="args">An ordered list of keys, like a path</param>
        /// <param name="create">If true, will create any nodes it does not find along the path.</param>
        /// <param name="index">Start index of the arg array</param>
        /// <returns>The FileNode at the given location, or null if the location was not found / created</returns>
        public VdfFileNode GetNodeAt(string[] args, bool create = true, int index = 0)
        {
            if (index >= args.Length)
            {
                return this;
            }
            if (NodeType == ValueType.Array)
            {
                Dictionary<String, VdfFileNode> data = (Dictionary<String, VdfFileNode>) NodeData;
                if (ContainsKey(args[index]))
                {
                    return data[args[index]].GetNodeAt(args, create, index + 1);
                }
                if (create)
                {
                    VdfFileNode newNode = new VdfFileNode();
                    data.Add(args[index], newNode);
                    return newNode.GetNodeAt(args, create, index + 1);
                }
            }
            return null;
        }

        /// <summary>
        /// Checks whether the given key exists within an array-type node
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <returns>True if the key was found, false otherwise</returns>
        public bool ContainsKey(string key)
        {
            if (NodeType != ValueType.Array)
            {
                return false;
            }
            return ((Dictionary<string, VdfFileNode>) NodeData).ContainsKey(key);
        }

        #endregion

        #region Modifiers

        /// <summary>
        /// Removes the subnode with the given key. Can only be called on array nodes.
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

        /// <summary>
        /// Removes any array nodes without any value-type children
        /// </summary>
        public void CleanTree()
        {
            Dictionary<string, VdfFileNode> nodes = NodeArray;
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

        public void MakeArray()
        {
            if (NodeType != ValueType.Array)
            {
                NodeType = ValueType.Array;
                NodeData = new Dictionary<string, VdfFileNode>(StringComparer.OrdinalIgnoreCase);
            }
        }

        #endregion

        #region Saving and Loading - Binary

        /// <summary>
        /// Loads a FileNode from stream.
        /// </summary>
        /// <param name="stream">Stream to load from</param>
        /// <returns>FileNode representing the contents of the stream.</returns>
        public static VdfFileNode LoadFromBinary(BinaryReader stream, long streamLength = -1)
        {
            if (streamLength == -1) streamLength = stream.BaseStream.Length;
            if (stream.BaseStream.Position == streamLength) return null;
            VdfFileNode thisLevel = new VdfFileNode();

            bool endOfStream = false;

            while (!endOfStream)
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
                // Get key
                string key = null;
                if (endOfStream || nextByte == 8 || stream.BaseStream.Position == streamLength)
                {
                    break;
                }
                if (nextByte == 0)
                {
                    key = ReadBin_GetStringToken(stream);
                    VdfFileNode newNode;
                    newNode = LoadFromBinary(stream, streamLength);
                    thisLevel[key] = newNode;
                }
                else if (nextByte == 1)
                {
                    key = ReadBin_GetStringToken(stream);
                    thisLevel[key] = new VdfFileNode(ReadBin_GetStringToken(stream));
                }
                else if (nextByte == 2)
                {
                    key = ReadBin_GetStringToken(stream);
                    int val = stream.ReadInt32();
                    thisLevel[key] = new VdfFileNode(val);
                }
                else if (nextByte == 7)
                {
                    key = ReadBin_GetStringToken(stream);
                    ulong val = stream.ReadUInt64();
                    thisLevel[key] = new VdfFileNode(val);
                }
                else if (nextByte == 0xFF)
                {
                    return null;
                }
                else
                {
                    throw new ParseException(string.Format(GlobalStrings.TextVdfFile_UnexpectedCharacterKey, nextByte));
                }
            }
            return thisLevel;
        }

        /* This isn't perfect, and it's not needed for now, but it allows reading both the common AND extended elements from AppInfo.vdf
        public static VdfFileNode LoadFromBinary_AppInfo( BinaryReader stream, int inNodeType, long streamLength ) {
            if( stream.BaseStream.Position == streamLength ) return null;
            VdfFileNode thisLevel = new VdfFileNode();

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
                    VdfFileNode newNode;
                    newNode = LoadFromBinary_AppInfo( stream, nextByte, streamLength );
                    thisLevel[key] = newNode;
                    if( nextByte == 3 ) break;
                } else if( nextNextByte == 0 ) {
                    break;
                } else if( nextByte == 0 ) {
                    if( inNodeType == 2 || inNodeType == 3  ) {
                    key = ReadBin_GetStringToken( stream );
                    VdfFileNode newNode;
                    newNode = LoadFromBinary_AppInfo( stream, 0, streamLength );
                    thisLevel[key] = newNode;
                    } else {
                        break;
                    }
                } else if( nextByte == 1 ) {
                    key = ReadBin_GetStringToken( stream );
                    thisLevel[key] = new VdfFileNode( ReadBin_GetStringToken( stream ) );
                } else if( nextByte == 2 ) {
                    key = ReadBin_GetStringToken( stream );
                    int val = stream.ReadInt32();
                    thisLevel[key] = new VdfFileNode( val );
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
        /// Writes this FileNode and childs to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="actualKey">Name of node to write.</param>
        public void SaveAsBinary(BinaryWriter stream, string actualKey = null)
        {
            switch (NodeType)
            {
                case ValueType.Array:
                    if (!string.IsNullOrEmpty(actualKey))
                        WriteBin_WriteArrayKey(stream, actualKey);
                    Dictionary<string, VdfFileNode> data = NodeArray;
                    foreach (KeyValuePair<string, VdfFileNode> entry in data)
                    {
                        (entry.Value).SaveAsBinary(stream, entry.Key);
                    }
                    WriteBin_WriteEndByte(stream);
                    break;
                case ValueType.String:
                    if (!string.IsNullOrEmpty(actualKey))
                        WriteBin_WriteStringValue(stream, actualKey, NodeString);
                    break;
                case ValueType.Int:
                    if (!string.IsNullOrEmpty(actualKey))
                    {
                        WriteBin_WriteIntegerValue(stream, actualKey, NodeInt);
                    }
                    break;
            }
        }

        #region Utility

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
        /// Reads from the specified stream until it reaches a string terminator (double quote with no escaping slash).
        /// The opening double quote should already be read, and the last one will be discarded.
        /// </summary>
        /// <param name="stream">The stream to read from. After the operation, the stream position will be just past the closing quote.</param>
        /// <returns>The string encapsulated by the quotes.</returns>
        private static string ReadBin_GetStringToken(BinaryReader reader, long streamLength = -1)
        {
            if (streamLength == -1) streamLength = reader.BaseStream.Length;

            bool endOfStream = false;
            bool stringDone = false;
            List<Byte> bytes = new List<byte>();
            do
            {
                try
                {
                    Byte b = reader.ReadByte();
                    if (b == 0) stringDone = true;
                    else bytes.Add(b);
                }
                catch (EndOfStreamException)
                {
                    endOfStream = true;
                }
            } while (!stringDone && !(endOfStream) && reader.BaseStream.Position < streamLength);

            if (!stringDone)
            {
                if (endOfStream)
                {
                    throw new ParseException(GlobalStrings.TextVdfFile_UnexpectedEOF);
                }
            }

            String token = UTF8Encoding.UTF8.GetString(bytes.ToArray());

            return token;
        }

        /// <summary>
        /// Writes a array key to a stream, adding start/end bytes.
        /// </summary>
        /// <param name="writer">Stream to write to</param>
        /// <param name="arrayKey">String to write</param>
        private void WriteBin_WriteArrayKey(BinaryWriter writer, string arrayKey)
        {
            writer.Write((byte) 0);
            writer.Write(arrayKey.ToCharArray());
            writer.Write((byte) 0);
        }

        /// <summary>
        /// Writes a pair o key and value to a stream, adding star/end and separator bytes
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="pair"></param>
        private void WriteBin_WriteStringValue(BinaryWriter writer, string key, string val)
        {
            writer.Write((byte) 1);
            writer.Write(key.ToCharArray());
            writer.Write((byte) 0);
            writer.Write(val.ToCharArray());
            writer.Write((byte) 0);
        }

        private void WriteBin_WriteIntegerValue(BinaryWriter writer, string key, int val)
        {
            writer.Write((byte) 2);
            writer.Write(key.ToCharArray());
            writer.Write((byte) 0);
            writer.Write(val);
        }

        /// <summary>
        /// Write an end byte to stream
        /// </summary>
        /// <param name="writer"></param>
        private void WriteBin_WriteEndByte(BinaryWriter writer)
        {
            writer.Write((byte) 8);
        }

        #endregion

        #endregion

        #region Saving and Loading - Text

        /// <summary>
        /// Loads a FileNode from stream.
        /// </summary>
        /// <param name="stream">Stream to load from</param>
        /// <returns>FileNode representing the contents of the stream.</returns>
        public static VdfFileNode LoadFromText(StreamReader stream, bool useFirstAsRoot = false)
        {
            VdfFileNode thisLevel = useFirstAsRoot ? null : new VdfFileNode();

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
                    throw new ParseException(string.Format(GlobalStrings.TextVdfFile_UnexpectedCharacterKey, nextChar));
                }
                ReadText_SkipWhitespace(stream);

                // Get value
                nextChar = (char) stream.Read();
                VdfFileNode newNode;
                if (nextChar == '"')
                {
                    newNode = new VdfFileNode(ReadText_GetStringToken(stream));
                }
                else if (nextChar == '{')
                {
                    newNode = LoadFromText(stream);
                }
                else
                {
                    throw new ParseException(
                        string.Format(GlobalStrings.TextVdfFile_UnexpectedCharacterValue, nextChar));
                }

                if (useFirstAsRoot)
                {
                    return newNode;
                }

                thisLevel[key] = newNode;
            }
            return thisLevel;
        }

        /// <summary>
        /// Writes this FileNode to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="indent">Indentation level of each line.</param>
        public void SaveAsText(StreamWriter stream, int indent = 0)
        {
            if (NodeType == ValueType.Array)
            {
                Dictionary<string, VdfFileNode> data = NodeArray;
                foreach (KeyValuePair<string, VdfFileNode> entry in data)
                {
                    if (entry.Value.NodeType == ValueType.Array)
                    {
                        WriteText_WriteWhitespace(stream, indent);
                        WriteText_WriteFormattedString(stream, entry.Key);
                        stream.WriteLine();

                        WriteText_WriteWhitespace(stream, indent);
                        stream.WriteLine('{');

                        (entry.Value).SaveAsText(stream, indent + 1);

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

        #region Utility

        /// <summary>
        /// Reads a from the specified stream until it reaches a string terminator (double quote with no escaping slash).
        /// The opening double quote should already be read, and the last one will be discarded.
        /// </summary>
        /// <param name="stream">The stream to read from. After the operation, the stream position will be just past the closing quote.</param>
        /// <returns>The string encapsulated by the quotes.</returns>
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
                    throw new ParseException(GlobalStrings.TextVdfFile_UnexpectedEOF);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Advances a stream until the next character is not whitespace
        /// </summary>
        /// <param name="stream">The stream to advance</param>
        private static void ReadText_SkipWhitespace(StreamReader stream)
        {
            char nextChar = (char) stream.Peek();
            while (nextChar == ' ' || nextChar == '\r' || nextChar == '\n' || nextChar == '\t')
            {
                stream.Read();
                nextChar = (char) stream.Peek();
            }
        }

        /// <summary>
        /// Writes a string to a stream, adding start/end quotes and escaping any quotes within the string.
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="s">String to write</param>
        private void WriteText_WriteFormattedString(StreamWriter stream, string s)
        {
            stream.Write("\"");
            stream.Write(s.Replace("\"", "\\\""));
            stream.Write("\"");
        }

        /// <summary>
        /// Writes the given number of tab characters to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="indent">Number of tabs</param>
        private void WriteText_WriteWhitespace(StreamWriter stream, int indent)
        {
            for (int i = 0; i < indent; i++)
            {
                stream.Write('\t');
            }
        }

        #endregion

        #endregion
    }

    public class ParseException : ApplicationException
    {
        public ParseException() { }
        public ParseException(string message) : base(message) { }
    }
}