#region LICENSE

//     This file (VDFNode.cs) is part of DepressurizerCore.
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DepressurizerCore.Models
{
	public enum ValueType
	{
		Array,
		String,
		Int
	}

	public sealed class VDFNode
	{
		#region Constructors and Destructors

		public VDFNode()
		{
			NodeType = ValueType.Array;
			NodeData = new Dictionary<string, VDFNode>(StringComparer.OrdinalIgnoreCase);
		}

		public VDFNode(string value)
		{
			NodeType = ValueType.String;
			NodeData = value;
		}

		public VDFNode(int value)
		{
			NodeType = ValueType.Int;
			NodeData = value;
		}

		public VDFNode(ulong value)
		{
			NodeType = ValueType.Int;
			NodeData = value;
		}

		#endregion

		#region Public Properties

		public Dictionary<string, VDFNode> NodeArray
		{
			get
			{
				if (NodeType != ValueType.Array)
				{
					return null;
				}

				if (!(NodeData is Dictionary<string, VDFNode> arrayData))
				{
					throw new InvalidCastException("NodeData is not a Dictionary<string, VdfFileNode>");
				}

				return arrayData;
			}
		}

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
					int res = 0;
					int.TryParse(NodeString, out res);
					return res;
				}

				return 0;
			}
		}

		public string NodeString
		{
			get
			{
				if (NodeType != ValueType.String)
				{
					return null;
				}

				if (!(NodeData is string stringData))
				{
					throw new InvalidCastException("NodeData is not a String");
				}

				return stringData;
			}
		}

		public ValueType NodeType { get; set; }

		#endregion

		#region Public Indexers

		public VDFNode this[string index]
		{
			get
			{
				if (NodeType != ValueType.Array)
				{
					throw new InvalidCastException("NodeType must be an Array");
				}

				if (!(NodeData is Dictionary<string, VDFNode> arrayData))
				{
					throw new InvalidCastException("NodeData is not a Dictionary<string, VdfFileNode>");
				}

				if (!arrayData.ContainsKey(index))
				{
					arrayData.Add(index, new VDFNode());
				}

				return arrayData[index];
			}
			set
			{
				if (NodeType == ValueType.String)
				{
					throw new InvalidCastException("NodeType cannot be a String");
				}

				if (!(NodeData is Dictionary<string, VDFNode> arrayData))
				{
					throw new InvalidCastException("NodeData is not a Dictionary<string, VdfFileNode>");
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

		public static VDFNode LoadFromBinary(BinaryReader binaryReader, long streamLength = -1)
		{
			if (streamLength == -1)
			{
				streamLength = binaryReader.BaseStream.Length;
			}

			if (binaryReader.BaseStream.Position == streamLength)
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
					nextByte = binaryReader.ReadByte();
				}
				catch (EndOfStreamException)
				{
					endOfStream = true;
					nextByte = 8;
				}

				if (endOfStream || (nextByte == 8) || (binaryReader.BaseStream.Position == streamLength))
				{
					break;
				}

				string key;

				switch (nextByte)
				{
					case 0:
					{
						key = ReadBin_GetStringToken(binaryReader);
						VDFNode newNode = LoadFromBinary(binaryReader, streamLength);
						thisLevel[key] = newNode;
						break;
					}

					case 1:
						key = ReadBin_GetStringToken(binaryReader);
						thisLevel[key] = new VDFNode(ReadBin_GetStringToken(binaryReader));
						break;

					case 2:
					{
						key = ReadBin_GetStringToken(binaryReader);
						int val = binaryReader.ReadInt32();
						thisLevel[key] = new VDFNode(val);
						break;
					}

					case 7:
					{
						key = ReadBin_GetStringToken(binaryReader);
						ulong val = binaryReader.ReadUInt64();
						thisLevel[key] = new VDFNode(val);
						break;
					}

					case 0xFF:
						return null;

					default:
						throw new InvalidDataException($"Unexpected Character Key '{nextByte}'");
				}
			}

			return thisLevel;
		}

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
				if (stream.EndOfStream || (nextChar == '}'))
				{
					break;
				}

				if (nextChar == '"')
				{
					key = ReadText_GetStringToken(stream);
				}
				else
				{
					throw new InvalidDataException($"Unexpected Character Key '{nextChar}'");
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
					throw new InvalidDataException($"Unexpected Character Value '{nextChar}'");
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

			while ((indexAt < str.Length) && (stream.BaseStream.Position < fileLength))
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

		public bool ContainsKey(string index)
		{
			if (NodeType != ValueType.Array)
			{
				return false;
			}

			if (!(NodeData is Dictionary<string, VDFNode> arrayData))
			{
				throw new InvalidCastException("NodeData is not a Dictionary<string, VdfFileNode>");
			}

			return arrayData.ContainsKey(index);
		}

		public VDFNode GetNodeAt(string[] args, bool create = true, int index = 0)
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
				throw new InvalidCastException("NodeData is not a Dictionary<string, VdfFileNode>");
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

		public bool RemoveSubnode(string key)
		{
			if (NodeType != ValueType.Array)
			{
				return false;
			}

			return NodeArray.Remove(key);
		}

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
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

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

		protected bool IsEmpty()
		{
			if (NodeArray != null)
			{
				return NodeArray.Count == 0;
			}

			return NodeData as string == null;
		}

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
			} while (!stringDone && !endOfStream && (reader.BaseStream.Position < streamLength));

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
			while ((nextChar == ' ') || (nextChar == '\r') || (nextChar == '\n') || (nextChar == '\t'))
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

		#endregion
	}
}