using System;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	internal static class Extensions
	{
		/// <summary>
		/// Clamps a value between the minimum and maximum.
		/// </summary>
		/// <typeparam name="T">The type being operated on.</typeparam>
		/// <param name="value">The value to clamp.</param>
		/// <param name="min">The minimum value to clamp to.</param>
		/// <param name="max">The maximum value to clamp to.</param>
		/// <returns>The clamped value.</returns>
		public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T> => value.CompareTo(min) < 0 ? min : (value.CompareTo(max) > 0 ? max : value);

		/// <summary>
		/// Populates an array with a single value.
		/// </summary>
		/// <remarks>
		/// Comes from https://stackoverflow.com/a/1014015
		/// </remarks>
		/// <typeparam name="T">The type being operated on.</typeparam>
		/// <param name="array">The array to populate.</param>
		/// <param name="value">The value to populate the array with.</param>
		/// <returns>The array, for chaining.</returns>
		public static T[] Populate<T>(this T[] array, T value)
		{
			for (int i = 0; i < array.Length; ++i)
				array[i] = value;
			return array;
		}

		/// <summary>
		/// Swaps the values of the arguments given.
		/// </summary>
		/// <typeparam name="T">The type for the values being swapped.</typeparam>
		/// <param name="a">The first value to swap.</param>
		/// <param name="b">The second value to swap.</param>
		public static void Swap<T>(ref T a, ref T b)
		{
			var temp = a;
			a = b;
			b = temp;
		}
	}
}
