using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Cache of wall+color for the <see cref="FindClosestColor" /> window.
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public class CachedColor
	{
		/// <summary>
		/// The name of the color.
		/// </summary>
		public string ColorName { get; set; }

		/// <summary>
		/// The L*a*b* representation of the wall color.
		/// </summary>
		public LAB LABColor { get; set; }

		/// <summary>
		/// The color of the wall.
		/// </summary>
		public Color WallColor { get; set; }

		/// <summary>
		/// The colored bitmap of the wall.
		/// </summary>
		public BitmapSource WallImage { get; set; }

		/// <summary>
		/// The name of this wall.
		/// </summary>
		public string WallName { get; set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		string DebuggerDisplay => $"{{{this.WallName}, {this.ColorName}, {this.WallColor}, {this.LABColor}}}";
	}
}
