using System;
using Microsoft.Xna.Framework;

namespace Hgm.Utilities
{
	public static class RectangleExtensions
	{
		public static Vector2 Size(this Rectangle rect)
		{
			return new Vector2(rect.Width, rect.Height);
		}

		public static Vector2 Position(this Rectangle rect)
		{
			return new Vector2(rect.X, rect.Y);
		}

		public static Rectangle SizeFrom(this Rectangle rect, Vector2 size)
		{
			rect.Width = (int) Math.Round(size.X);
			rect.Height = (int) Math.Round(size.Y);
			return rect;
		}
		
		public static Rectangle PositionFrom(this Rectangle rect, Vector2 position)
		{
			rect.X = (int) Math.Round(position.X);
			rect.Y = (int) Math.Round(position.Y);
			return rect;
		}
	}
}