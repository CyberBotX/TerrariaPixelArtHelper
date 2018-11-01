using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using PostSharp.Patterns.Contracts;
using PostSharp.Patterns.Model;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// The item type to store within a combo box of a <see cref="ColorToWall" /> control.
	/// </summary>
	[DebuggerDisplay("{Name,nq}")]
	[NotifyPropertyChanged]
	public class WallItem
	{
		static readonly ConcurrentDictionary<string, Task<WallItem>> wallItemsCache = new ConcurrentDictionary<string, Task<WallItem>>();

		/// <summary>
		/// Gets or sets this wall's name.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets or sets this wall's image, if any.
		/// </summary>
		public BitmapSource Image { get; private set; }

		/// <summary>
		/// Creates a new <see cref="WallItem" /> with an image, if possible.
		/// </summary>
		/// <param name="wallName">The name of the wall.</param>
		/// <returns>A new <see cref="WallItem" />.</returns>
		internal static async Task<WallItem> Create([Required] string wallName) => await WallItem.wallItemsCache.GetOrAdd(wallName, async key =>
		{
			var wallItem = new WallItem()
			{
				Name = wallName
			};
			if (!string.IsNullOrWhiteSpace(wallName))
				wallItem.Image = await App.GetWallFrame(wallName, "Uncolored", 0, 0, 0);
			return wallItem;
		});
	}
}
