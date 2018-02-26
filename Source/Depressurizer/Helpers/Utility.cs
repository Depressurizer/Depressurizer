﻿#region LICENSE

//     This file (Utility.cs) is part of Depressurizer.
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
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Depressurizer.Helpers
{
	public static class Utility
	{
		#region Public Methods and Operators

		/// <summary>
		///     Moves a file to back it up in anticipation of a save. Maintains a certain number of old versions of the file.
		/// </summary>
		/// <param name="filePath">File to move</param>
		/// <param name="maxBackups">The number of old versions to maintain</param>
		public static void BackupFile(string filePath, int maxBackups)
		{
			if (maxBackups < 1)
			{
				return;
			}

			string targetPath = BackupFile_ClearSlot(filePath, maxBackups, 1);
			File.Copy(filePath, targetPath);
		}

		/// <summary>
		///     Clamp the value of an item to be between two values.
		/// </summary>
		/// <typeparam name="T">Type of the clamped object</typeparam>
		/// <param name="val">Value to clamp</param>
		/// <param name="min">Minimum return value</param>
		/// <param name="max">Maximum return value</param>
		/// <returns>If val is between min and max, return val. If greater than max, return max. If less than min, return min.</returns>
		public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0)
			{
				return min;
			}

			if (val.CompareTo(max) > 0)
			{
				return max;
			}

			return val;
		}

		public static string GetEnumDescription(Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if ((attributes != null) && (attributes.Length > 0))
			{
				return attributes[0].Description;
			}

			return value.ToString();
		}

		public static void MoveItem(ListBox lb, int direction)
		{
			// Checking selected item
			if ((lb.SelectedItem == null) || (lb.SelectedIndex < 0) || (lb.SelectedItems.Count > 1))
			{
				return; // No selected item or more than one item selected - nothing to do
			}

			// Calculate new index using move direction
			int newIndex = lb.SelectedIndex + direction;

			// Checking bounds of the range
			if ((newIndex < 0) || (newIndex >= lb.Items.Count))
			{
				return; // Index out of range - nothing to do
			}

			object selected = lb.SelectedItem;

			// Removing removable element
			lb.Items.Remove(selected);

			// Insert it in new position
			lb.Items.Insert(newIndex, selected);

			// Restore selection
			lb.SetSelected(newIndex, true);
		}

		#endregion

		#region Methods

		/// <summary>
		///     Clears a slot for a file to be moved into as part of a backup.
		/// </summary>
		/// <param name="basePath">Path of the main file that's being backed up.</param>
		/// <param name="maxBackups">The number of backups that we're looking to keep</param>
		/// <param name="current">
		///     The number of the backup file to process. For example, if 1, this is clearing a spot for the most
		///     recent backup.
		/// </param>
		/// <returns>The path of the cleared slot</returns>
		private static string BackupFile_ClearSlot(string basePath, int maxBackups, int current)
		{
			string thisPath = BackupFile_GetName(basePath, current);
			if (!File.Exists(thisPath))
			{
				return thisPath;
			}

			if (current >= maxBackups)
			{
				File.Delete(thisPath);
				return thisPath;
			}

			string moveTarget = BackupFile_ClearSlot(basePath, maxBackups, current + 1);
			File.Move(thisPath, moveTarget);
			return thisPath;
		}

		/// <summary>
		///     Gets the name for a certain backup slot.
		/// </summary>
		/// <param name="baseName">Name of the current version of the file</param>
		/// <param name="slotNum">Slot number to get the name for</param>
		/// <returns>The name</returns>
		private static string BackupFile_GetName(string baseName, int slotNum)
		{
			if (slotNum == 0)
			{
				return baseName;
			}

			return string.Format("{0}.bak_{1}", baseName, slotNum);
		}

		#endregion
	}
}
