using System.Collections.Generic;
using System.Windows.Media;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Contains the data for a bitmap.
	/// </summary>
	internal class BitmapData
	{
		/// <summary>
		/// Cache of color pixels for each color of the bitmap.
		/// </summary>
		public Dictionary<Color, int[]> CachedColorPixels { get; set; }

		/// <summary>
		/// The number of pixels per color.
		/// </summary>
		public Dictionary<Color, int> Colors { get; set; }

		/// <summary>
		/// The pixel info for each coordinate.
		/// </summary>
		public Dictionary<(int x, int y), PixelInfo> PixelInfo { get; set; }

		/// <summary>
		/// The pixels of the zoomed bitmap.
		/// </summary>
		public int[] ZoomedPixels { get; set; }
	}
}
