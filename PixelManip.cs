using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PostSharp.Patterns.Contracts;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Pixel manipulation methods.
	/// </summary>
	internal static class PixelManip
	{
		/// <summary>
		/// Copies integer-based pixels from a 16x16 image into another image.
		/// </summary>
		/// <param name="source">The 16x16 image to copy from.</param>
		/// <param name="dest">The image to copy to.</param>
		/// <param name="x">The destination X coordinate.</param>
		/// <param name="y">The destination Y coordinate.</param>
		/// <param name="width">The width of the destination image.</param>
		internal static async Task CopyFrom16x16([Required] int[] source, [Required] int[] dest, int x, int y, int width) => await Task.Run(() =>
		{
			for (int y2 = 0; y2 < 16; ++y2)
				Array.Copy(source, y2 * 16, dest, (x + (y * 16 + y2) * width) * 16, 16);
		});

		/// <summary>
		/// Creates a new <see cref="BitmapSource" /> from the given pixels.
		/// </summary>
		/// <remarks>
		/// Pixel data must be in BGRA format, not premultiplied.
		/// </remarks>
		/// <param name="width">The width of the new bitmap.</param>
		/// <param name="height">The height of the new bitmap.</param>
		/// <param name="pixels">The pixels of the new bitmap.</param>
		/// <returns>The new <see cref="BitmapSource" />.</returns>
		internal static BitmapSource Create(int width, int height, [Required] Array pixels) => BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, pixels, width * 4);
	}
}
