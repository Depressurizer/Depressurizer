/*
Copyright 2014 Juan Luis Podadera.

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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Depressurizer
{
    /// <summary>
    /// Represents a single node in a Valve's VDF binary file.
    /// </summary>
    public class BinaryVdfFileNode : VdfFileNode
    {

        protected override VdfFileNode CreateNode()
        {
            return new BinaryVdfFileNode();
        }

        /// <summary>
        /// Creates a new array-type node
        /// </summary>
        public BinaryVdfFileNode() : base() {
        }

        /// <summary>
        /// Creates a new value-type node
        /// </summary>
        /// <param name="value">Value of the string</param>
        public BinaryVdfFileNode(string value) : base(value) {
        }

        #region Utility

        /// <summary>
        /// Reads a from the specified stream until it reaches a string terminator (double quote with no escaping slash).
        /// The opening double quote should already be read, and the last one will be discarded.
        /// </summary>
        /// <param name="stream">The stream to read from. After the operation, the stream position will be just past the closing quote.</param>
        /// <returns>The string encapsulated by the quotes.</returns>
        private static string GetStringToken(BinaryReader reader)
        {
            bool endOfStream = false;
            bool stringDone = false;
            StringBuilder sb = new StringBuilder();
            byte nextByte;
            do
            {
                try
                {
                    nextByte = reader.ReadByte();

                    if (nextByte == 0)
                    {
                        stringDone = true;
                    }
                    else
                    {
                        sb.Append((char)nextByte);
                    }
                }
                catch (EndOfStreamException e)
                {
                    endOfStream = true;
                }
            } while (!stringDone && !(endOfStream));

            if (!stringDone)
            {
                if (endOfStream)
                {
                    throw new ParseException(GlobalStrings.TextVdfFile_UnexpectedEOF);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Writes a array key to a stream, adding start/end bytes.
        /// </summary>
        /// <param name="writer">Stream to write to</param>
        /// <param name="arrayKey">String to write</param>
        private void WriteArrayKey(BinaryWriter writer, string arrayKey)
        {
            writer.Write((byte)0);
            writer.Write(arrayKey.ToCharArray());
            writer.Write((byte)0);
        }

        /// <summary>
        /// Writes a pair o key and value to a stream, adding star/end and separator bytes
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="pair"></param>
        private void WriteKeyValuePair(BinaryWriter writer, KeyValuePair<string, string> pair)
        {
            writer.Write((byte)1);
            writer.Write(pair.Key.ToCharArray());
            writer.Write((byte)0);
            writer.Write(pair.Value.ToCharArray());
            writer.Write((byte)0);
        }

        /// <summary>
        /// Write an end byte to stream
        /// </summary>
        /// <param name="writer"></param>
        private void WriteEndByte(BinaryWriter writer)
        {
            writer.Write((byte)8);
        }

        #endregion

        #region Saving and loading
        /// <summary>
        /// Loads a FileNode from stream.
        /// </summary>
        /// <param name="stream">Stream to load from</param>
        /// <returns>FileNode representing the contents of the stream.</returns>
        public static BinaryVdfFileNode Load(BinaryReader stream) {
            BinaryVdfFileNode thisLevel = new BinaryVdfFileNode();

            bool endOfStream = false;

            while( !endOfStream ) {

                //SkipWhitespace( stream );
                byte nextByte;
                try
                {
                    nextByte = stream.ReadByte();
                }
                catch (EndOfStreamException e)
                {
                    endOfStream = true;
                    nextByte = 8;
                }
                // Get key
                string key = null;
                if( endOfStream || nextByte == 8 ) {
                    break;
                } else if( nextByte == 0 ) {
                    key = GetStringToken( stream );
                    BinaryVdfFileNode newNode;
                    newNode = Load(stream);
                    thisLevel[key] = newNode;
                } else if (nextByte == 1) {
                    key = GetStringToken(stream);
                    thisLevel[key] = new BinaryVdfFileNode(GetStringToken(stream));
                }
                else {
                    throw new ParseException(string.Format(GlobalStrings.TextVdfFile_UnexpectedCharacterKey, nextByte.ToString()));
                }
            }
            return thisLevel;
        }

        /// <summary>
        /// Write complete VdfFileNode to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        public void Save(BinaryWriter stream)
        {
            Save(stream, null);
        }

        /// <summary>
        /// Writes this FileNode and childs to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="actualKey">Name of node to write.</param>
        private void Save(BinaryWriter stream, string actualKey)
        {
            if (NodeType == ValueType.Array)
            {
                if (!string.IsNullOrEmpty(actualKey))
                    WriteArrayKey(stream, actualKey);
                Dictionary<string, VdfFileNode> data = NodeArray;
                foreach (KeyValuePair<string, VdfFileNode> entry in data)
                {
                    ((BinaryVdfFileNode)entry.Value).Save(stream, entry.Key);
                }
                WriteEndByte(stream);
            }
            else
            {
                if (!string.IsNullOrEmpty(actualKey))
                    WriteKeyValuePair(stream, new KeyValuePair<string, string>(actualKey, NodeString));
            }
        }

        #endregion
    }
}
