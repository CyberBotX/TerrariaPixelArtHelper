using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using PostSharp.Patterns.Model;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Interaction logic for FindClosestColor.xaml
	/// </summary>
	public partial class FindClosestColor : Window
	{
		static AsyncLazy<List<CachedColor>> cachedColors = new AsyncLazy<List<CachedColor>>(async () =>
		{
			// This creates a cache for every possible wall+paint (except for No Wall and Glass Wall).
			var walls = await ColorToWall.Walls;
			var items = await Task.WhenAll(walls.Where(w => w.Name != "No Wall" && w.Name != "Glass Wall").SelectMany(c => ColorToWall.Colors, async (w, c) =>
			{
				var wallColor = ColorExtension.GetMapWallColor(w.Name).Value.ColorizeMap(c);
				return new CachedColor()
				{
					ColorName = c,
					LABColor = LAB.FromRGB(wallColor),
					WallColor = wallColor,
					WallImage = await App.GetWallFrame(w.Name, c, 0, 0, 0),
					WallName = w.Name
				};
			}));
			return items.ToList();
		});

		static readonly ConcurrentDictionary<Color, Task<List<ColorItem>>> colorItemCache = new ConcurrentDictionary<Color, Task<List<ColorItem>>>();

		internal new FindClosestColorViewModel DataContext => base.DataContext as FindClosestColorViewModel;

		static async Task<List<ColorItem>> GetColors(Color color) => await FindClosestColor.colorItemCache.GetOrAdd(color, async key =>
		{
			// Find the L*a*b* color for the given color, find the distance between that and the L*a*b* colors for all wall+paint combinations and sort by the distance.
			var originalLAB = LAB.FromRGB(color);
			var cachedColors = await FindClosestColor.InitializeCache();
			var unorderedColors = await Task.WhenAll(cachedColors.Select(async c => await Task.Run(() => new ColorItem()
			{
				ColorName = c.ColorName,
				Distance = LAB.Distance(originalLAB, c.LABColor),
				WallColor = c.WallColor,
				WallImage = c.WallImage,
				WallName = c.WallName
			})));
			return unorderedColors.OrderBy(c => c.Distance).ToList();
		});

		public static async Task<FindClosestColor> Create(Color originalColor)
		{
			var findClosestColor = new FindClosestColor();
			var colors = await FindClosestColor.GetColors(originalColor);

			// Set the data to show.
			findClosestColor.lbColors.ItemsSource = colors;
			findClosestColor.DataContext.OriginalColor = originalColor;
			findClosestColor.DataContext.SelectedColorItem = colors[0];

			return findClosestColor;
		}

		public static async Task<List<CachedColor>> InitializeCache() => await FindClosestColor.cachedColors;

		FindClosestColor() => this.InitializeComponent();

		void OK_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}
	}

	/// <summary>
	/// The view model for <see cref="FindClosestColor" />.
	/// </summary>
	internal class FindClosestColorViewModel
	{
		/// <summary>
		/// The original color we are matching against.
		/// </summary>
		public Color OriginalColor { get; set; } = Colors.White;

		/// <summary>
		/// The text display of the original color.
		/// </summary>
		[SafeForDependencyAnalysis]
		public string OriginalColorText => $"({this.OriginalColor.R}, {this.OriginalColor.G}, {this.OriginalColor.B}, {this.OriginalColor.A})";

		/// <summary>
		/// The currently selected <see cref="ColorItem" />.
		/// </summary>
		public ColorItem SelectedColorItem { get; set; } = new ColorItem();

		/// <summary>
		/// The text display of the selected <see cref="ColorItem" />'s wall color.
		/// </summary>
		[SafeForDependencyAnalysis]
		public string SelectedColorText => $"({this.SelectedColorItem.WallColor.R}, {this.SelectedColorItem.WallColor.G}, {this.SelectedColorItem.WallColor.B}, {this.SelectedColorItem.WallColor.A})";
	}
}
