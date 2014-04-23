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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Depressurizer {



    /// <summary>
    /// Represents a single node in a Steam config file
    /// </summary>
    public class TextVdfFileNode : VdfFileNode {

        protected override VdfFileNode CreateNode()
        {
            return (VdfFileNode) new TextVdfFileNode();
        }

        /// <summary>
        /// Creates a new array-type node
        /// </summary>
        public TextVdfFileNode() : base() { }

        /// <summary>
        /// Creates a new value-type node
        /// </summary>
        /// <param name="value">Value of the string</param>
        public TextVdfFileNode( string value ) : base(value) { }

        #region Utility

        /// <summary>
        /// Reads a from the specified stream until it reaches a string terminator (double quote with no escaping slash).
        /// The opening double quote should already be read, and the last one will be discarded.
        /// </summary>
        /// <param name="stream">The stream to read from. After the operation, the stream position will be just past the closing quote.</param>
        /// <returns>The string encapsulated by the quotes.</returns>
        private static string GetStringToken( StreamReader stream ) {
            bool escaped = false;
            bool stringDone = false;
            StringBuilder sb = new StringBuilder();
            char nextChar;
            do {
                nextChar = (char)stream.Read();
                if( escaped ) {
                    switch( nextChar ) {
                        case '\\':
                            sb.Append( '\\' );
                            break;
                        case '"':
                            sb.Append( '"' );
                            break;
                        case '\'':
                            sb.Append( '\'' );
                            break;
                    }
                    escaped = false;
                } else {
                    switch( nextChar ) {
                        case '\\':
                            escaped = true;
                            break;
                        case '"':
                            stringDone = true;
                            break;
                        default:
                            sb.Append( nextChar );
                            break;
                    }
                }
            } while( !stringDone && !stream.EndOfStream );
            if( !stringDone ) {
                if( stream.EndOfStream ) {
                    throw new ParseException(GlobalStrings.TextVdfFile_UnexpectedEOF);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Advances a stream until the next character is not whitespace
        /// </summary>
        /// <param name="stream">The stream to advance</param>
        private static void SkipWhitespace( StreamReader stream ) {
            char nextChar = (char)stream.Peek();
            while( nextChar == ' ' || nextChar == '\r' || nextChar == '\n' || nextChar == '\t' ) {
                stream.Read();
                nextChar = (char)stream.Peek();
            }
        }

        /// <summary>
        /// Writes a string to a stream, adding start/end quotes and escaping any quotes within the string.
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="s">String to write</param>
        private void WriteFormattedString( StreamWriter stream, string s ) {
            stream.Write( "\"" );
            stream.Write( s.Replace( "\"", "\\\"" ) );
            stream.Write( "\"" );
        }

        /// <summary>
        /// Writes the given number of tab characters to a stream
        /// </summary>
        /// <param name="stream">Stream to write to</param>
        /// <param name="indent">Number of tabs</param>
        private void WriteWhitespace( StreamWriter stream, int indent ) {
            for( int i = 0; i < indent; i++ ) {
                stream.Write( '\t' );
            }
        }

        #endregion


        #region Saving and loading
        /// <summary>
        /// Loads a FileNode from stream.
        /// </summary>
        /// <param name="stream">Stream to load from</param>
        /// <returns>FileNode representing the contents of the stream.</returns>
        public static TextVdfFileNode Load( StreamReader stream, bool useFirstAsRoot = false ) {
            TextVdfFileNode thisLevel = useFirstAsRoot ? null : new TextVdfFileNode();

            SkipWhitespace( stream );

            while( !stream.EndOfStream ) {

                SkipWhitespace( stream );
                // Get key
                char nextChar = (char)stream.Read();
                string key = null;
                if( stream.EndOfStream || nextChar == '}' ) {
                    break;
                } else if( nextChar == '"' ) {
                    key = GetStringToken( stream );
                } else {
                    throw new ParseException(string.Format(GlobalStrings.TextVdfFile_UnexpectedCharacterKey, nextChar));
                }
                SkipWhitespace( stream );

                // Get value
                nextChar = (char)stream.Read();
                TextVdfFileNode newNode;
                if( nextChar == '"' ) {
                    newNode = new TextVdfFileNode( GetStringToken( stream ) );
                } else if( nextChar == '{' ) {
                    newNode = Load( stream );
                } else {
                    throw new ParseException(string.Format(GlobalStrings.TextVdfFile_UnexpectedCharacterValue, nextChar));
                }

                if( useFirstAsRoot ) {
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
        public void Save( StreamWriter stream, int indent = 0 ) {
            if( NodeType == ValueType.Array ) {
                Dictionary<string, VdfFileNode> data = NodeArray;
                foreach( KeyValuePair<string, VdfFileNode> entry in data ) {
                    if( entry.Value.NodeType == ValueType.Array ) {
                        WriteWhitespace( stream, indent );
                        WriteFormattedString( stream, entry.Key );
                        stream.WriteLine();

                        WriteWhitespace( stream, indent );
                        stream.WriteLine( '{' );

                        ((TextVdfFileNode)entry.Value).Save( stream, indent + 1 );

                        WriteWhitespace( stream, indent );
                        stream.WriteLine( '}' );
                    } else {
                        WriteWhitespace( stream, indent );
                        WriteFormattedString( stream, entry.Key );
                        stream.Write( "\t\t" );

                        WriteFormattedString( stream, entry.Value.NodeData as string );
                        stream.WriteLine();
                    }
                }
            } else {

            }
        }
        #endregion
    }

    public class ParseException : ApplicationException {
        public ParseException() : base() { }
        public ParseException( string message ) : base() { }
    }
}
