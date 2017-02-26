using System;
using System.Drawing;
using System.Drawing.Drawing2D;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	public static class Extensions
	{
		/// <summary>
		/// Clamps a value between the minimum and maximum.
		/// </summary>
		/// <typeparam name="T">The type being operated on.</typeparam>
		/// <param name="value">The value to clamp.</param>
		/// <param name="min">The minimum value to clamp to.</param>
		/// <param name="max">The maximum value to clamp to.</param>
		/// <returns>The clamped value.</returns>
		public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
		{
			if (value.CompareTo(min) < 0)
				return min;
			else if (value.CompareTo(max) > 0)
				return max;
			else
				return value;
		}

		/// <summary>
		/// Swaps the values of the arguments given.
		/// </summary>
		/// <typeparam name="T">The type for the values being swapped.</typeparam>
		/// <param name="a">The first value to swap.</param>
		/// <param name="b">The second value to swap.</param>
		public static void Swap<T>(ref T a, ref T b)
		{
			T temp = a;
			a = b;
			b = temp;
		}

		/// <summary>
		/// Erases a portion of a Graphics object.
		/// Code comes from http://stackoverflow.com/a/8485524
		/// </summary>
		/// <param name="g">Graphics object to erase from.</param>
		/// <param name="rect">Rectangle to erase.</param>
		public static void EraseRectangle(this Graphics g, Rectangle rect)
		{
			var compMode = g.CompositingMode;
			g.FillRectangle(Brushes.White, rect);
			g.CompositingMode = CompositingMode.SourceCopy;
			g.FillRectangle(Brushes.Transparent, rect);
			g.CompositingMode = compMode;
		}
	}
}
