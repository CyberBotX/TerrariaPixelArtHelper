using PostSharp.Patterns.Model;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Holds the data for a grid line. Either both X1 and X2 will be the same or both Y1 and Y2 will be the same.
	/// </summary>
	[NotifyPropertyChanged]
	internal class GridLineData
	{
		/// <summary>
		/// Either the X coordinate of a vertical line or the left X coordinate of a horizontal line.
		/// </summary>
		public double X1 { get; set; }

		/// <summary>
		/// Either the X coordinate of a vertical line or the right X coordinate of a horizontal line.
		/// </summary>
		public double X2 { get; set; }

		/// <summary>
		/// Either the Y coordinate of a horizontal line or the top Y coordinate of a vertical line.
		/// </summary>
		public double Y1 { get; set; }

		/// <summary>
		/// Either the Y coordinate of a horizontal line or the bottom Y coordinate of a vertical line.
		/// </summary>
		public double Y2 { get; set; }
	}
}
