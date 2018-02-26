#region LICENSE

//     This file (MyRenderer.cs) is part of Depressurizer.
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

using System.Drawing;
using System.Windows.Forms;

namespace Depressurizer.Models
{
	public sealed class MyRenderer : ToolStripRenderer
	{
		#region Methods

		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
			Color colorText = e.Item.Selected ? Color.FromArgb(255, 255, 255) : Color.FromArgb(169, 167, 167);
			if (e.ToolStrip is ToolStripDropDown)
			{
				Color colorItem = Color.FromArgb(55, 71, 79);
				using (SolidBrush brush = new SolidBrush(colorItem))
				{
					e.Graphics.FillRectangle(brush, rc);
				}
			}
			else
			{
				Color colorItem = Color.FromArgb(38, 50, 56);
				using (SolidBrush brush = new SolidBrush(colorItem))
				{
					e.Graphics.FillRectangle(brush, rc);
				}
			}

			e.Item.ForeColor = colorText;

			base.OnRenderMenuItemBackground(e);
		}

		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			Brush bLight = new SolidBrush(Color.FromArgb(157, 168, 157));

			if (!e.Vertical)
			{
				Rectangle r3;
				if (e.Item.IsOnDropDown)
				{
					r3 = new Rectangle(0, 3, e.Item.Width, 1);
					e.Graphics.FillRectangle(bLight, r3);
				}
			}

			base.OnRenderSeparator(e);
		}

		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDown)
			{
				e.Graphics.Clear(Color.FromArgb(55, 71, 79));

				return;
			}

			base.OnRenderToolStripBackground(e);
		}

		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDown)
			{
				Pen p = new Pen(Color.FromArgb(41, 42, 46));
				if (e.ToolStrip is ToolStripOverflow)
				{
					e.Graphics.DrawLines(p, new[]
					{
						e.AffectedBounds.Location,
						new Point(e.AffectedBounds.Left, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Top),
						new Point(e.AffectedBounds.Left, e.AffectedBounds.Top)
					});
				}
				else
				{
					e.Graphics.DrawLines(p, new[]
					{
						new Point(e.AffectedBounds.Left + e.ConnectedArea.Left, e.AffectedBounds.Top),
						e.AffectedBounds.Location,
						new Point(e.AffectedBounds.Left, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Bottom - 1),
						new Point(e.AffectedBounds.Right - 1, e.AffectedBounds.Top),
						new Point(e.AffectedBounds.Left + e.ConnectedArea.Right, e.AffectedBounds.Top)
					});
				}

				return;
			}

			if (e.ToolStrip is MenuStrip || e.ToolStrip is StatusStrip)
			{
				return;
			}

			using (Pen p = new Pen(Color.FromArgb(41, 42, 46)))
			{
				e.Graphics.DrawLine(p, new Point(e.ToolStrip.Left, e.ToolStrip.Bottom - 1), new Point(e.ToolStrip.Width, e.ToolStrip.Bottom - 1));
			}

			base.OnRenderToolStripBorder(e);
		}

		#endregion
	}
}
