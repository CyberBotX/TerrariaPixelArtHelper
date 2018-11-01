using System.Windows.Media;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Holds information about a specific pixel.
	/// </summary>
	internal class PixelInfo
	{
		/// <summary>
		/// The color of the pixel.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// The current wall name of the pixel.
		/// </summary>
		public string WallName { get; set; }

		/// <summary>
		/// The current wall frame of the pixel.
		/// </summary>
		public int WallFrame { get; set; }
	}
}
