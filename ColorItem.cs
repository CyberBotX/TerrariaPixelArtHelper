using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PostSharp.Patterns.Model;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// An item for the <see cref="FindClosestColor" /> window that stores the data needed to display in the window, as well as distance for sorting purposes.
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[NotifyPropertyChanged]
	internal class ColorItem
	{
		/// <summary>
		/// The name of the color being used.
		/// </summary>
		public string ColorName { get; set; }

		/// <summary>
		/// The color being used.
		/// </summary>
		public Color WallColor { get; set; }

		/// <summary>
		/// The image of the wall+color for this item.
		/// </summary>
		public BitmapSource WallImage { get; set; }

		/// <summary>
		/// The name of the wall being used.
		/// </summary>
		public string WallName { get; set; }

		/// <summary>
		/// The distance that <see cref="ColorItem.WallColor" /> is from the original color.
		/// </summary>
		public double Distance { get; set; }

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		string DebuggerDisplay => $"{{{this.WallName}, {this.ColorName}, {this.WallColor}, {this.Distance}}}";
	}
}
