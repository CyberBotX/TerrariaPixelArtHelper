using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PostSharp.Patterns.Model;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Interaction logic for ColorToWall.xaml
	/// </summary>
	public partial class ColorToWall : UserControl
	{
		#region Events

		public static readonly RoutedEvent FlashEvent = EventManager.RegisterRoutedEvent(nameof(ColorToWall.Flash), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorToWall));

		public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent(nameof(ColorToWall.SelectedColorChanged), RoutingStrategy.Bubble,
			typeof(EventHandler<WallSelectorChangedEventArgs>), typeof(ColorToWall));

		#endregion

		// List of acceptable colors. Comes from items where Terraria.Item.paint is set to a non-0 value.
		internal static readonly List<string> colors = new List<string>()
		{
			"Uncolored",
			"Red",
			"Orange",
			"Yellow",
			"Lime",
			"Green",
			"Teal",
			"Cyan",
			"Sky Blue",
			"Blue",
			"Purple",
			"Violet",
			"Pink",
			"Deep Red",
			"Deep Orange",
			"Deep Yellow",
			"Deep Lime",
			"Deep Green",
			"Deep Teal",
			"Deep Cyan",
			"Deep Sky Blue",
			"Deep Blue",
			"Deep Purple",
			"Deep Violet",
			"Deep Pink",
			"Black",
			"Gray",
			"White",
			"Brown",
			"Shadow",
			"Negative"
		};

		// Order the acceptable walls by name and then insert a "No Wall" option at the start.
		internal static AsyncLazy<ReadOnlyCollection<WallItem>> Walls => new AsyncLazy<ReadOnlyCollection<WallItem>>(async () =>
		{
			// List of acceptable walls (ones which can be crafted, placed by a user and which don't animate). Comes from items where Terraria.Item.createWall is set to a non-0 value.
			// Names of the walls come from Terraria.Lang and checking for the respective item IDs.
			var wallItems = await Task.WhenAll(new List<string>()
			{
				"Stone Wall",
				"Wood Wall",
				"Gray Brick Wall",
				"Red Brick Wall",
				"Gold Brick Wall",
				"Silver Brick Wall",
				"Copper Brick Wall",
				"Dirt Wall",
				"Blue Brick Wall",
				"Green Brick Wall",
				"Pink Brick Wall",
				"Obsidian Brick Wall",
				"Glass Wall",
				"Pearlstone Brick Wall",
				"Iridescent Brick Wall",
				"Mudstone Brick Wall",
				"Cobalt Brick Wall",
				"Mythril Brick Wall",
				"Candy Cane Wall",
				"Green Candy Cane Wall",
				"Snow Brick Wall",
				"Adamantite Beam Wall",
				"Demonite Brick Wall",
				"Sandstone Brick Wall",
				"Ebonstone Brick Wall",
				"Red Stucco Wall",
				"Yellow Stucco Wall",
				"Green Stucco Wall",
				"Gray Stucco Wall",
				"Ebonwood Wall",
				"Rich Mahogany Wall",
				"Pearlwood Wall",
				"Tin Brick Wall",
				"Tungsten Brick Wall",
				"Platinum Brick Wall",
				"Grass Wall",
				"Jungle Wall",
				"Flower Wall",
				"Cactus Wall",
				"Cloud Wall",
				"Mushroom Wall",
				"Bone Block Wall",
				"Slime Block Wall",
				"Flesh Block Wall",
				"Living Wood Wall",
				"Disc Wall",
				"Ice Brick Wall",
				"Shadewood Wall",
				"Blue Slab Wall",
				"Blue Tiled Wall",
				"Pink Slab Wall",
				"Pink Tiled Wall",
				"Green Slab Wall",
				"Green Tiled Wall",
				"Hive Wall",
				"Palladium Column Wall",
				"Bubblegum Block Wall",
				"Titanstone Block Wall",
				"Lihzahrd Brick Wall",
				"Pumpkin Wall",
				"Hay Wall",
				"Spooky Wood Wall",
				"Christmas Tree Wallpaper",
				"Ornament Wallpaper",
				"Candy Cane Wallpaper",
				"Festive Wallpaper",
				"Stars Wallpaper",
				"Squiggles Wallpaper",
				"Snowflake Wallpaper",
				"Krampus Horn Wallpaper",
				"Bluegreen Wallpaper",
				"Grinch Finger Wallpaper",
				"Fancy Gray Wallpaper",
				"Ice Floe Wallpaper",
				"Music Wallpaper",
				"Purple Rain Wallpaper",
				"Rainbow Wallpaper",
				"Sparkle Stone Wallpaper",
				"Starlit Heaven Wallpaper",
				"Bubble Wallpaper",
				"Copper Pipe Wallpaper",
				"Ducky Wallpaper",
				"White Dynasty Wall",
				"Blue Dynasty Wall",
				"Copper Plating Wall",
				"Stone Slab Wall",
				"Sail",
				"Boreal Wood Wall",
				"Palm Wood Wall",
				"Amber Gemspark Wall",
				"Amethyst Gemspark Wall",
				"Diamond Gemspark Wall",
				"Emerald Gemspark Wall",
				"Offline Amber Gemspark Wall",
				"Offline Amethyst Gemspark Wall",
				"Offline Diamond Gemspark Wall",
				"Offline Emerald Gemspark Wall",
				"Offline Ruby Gemspark Wall",
				"Offline Sapphire Gemspark Wall",
				"Offline Topaz Gemspark Wall",
				"Ruby Gemspark Wall",
				"Sapphire Gemspark Wall",
				"Topaz Gemspark Wall",
				"Tin Plating Wall",
				"Chlorophyte Brick Wall",
				"Crimtane Brick Wall",
				"Shroomite Plating Wall",
				"Martian Conduit Wall",
				"Hellstone Brick Wall",
				"Smooth Marble Wall",
				"Smooth Granite Wall",
				"Meteorite Brick Wall",
				"Marble Wall",
				"Granite Wall",
				"Crystal Block Wall",
				"Luminite Brick Wall",
				"Silly Pink Balloon Wall",
				"Silly Purple Balloon Wall",
				"Silly Green Balloon Wall"
			}.OrderBy(w => w).Select(async w => await WallItem.Create(w)));
			var wallItemsList = wallItems.ToList();
			wallItemsList.Insert(0, await WallItem.Create("No Wall"));
			return wallItemsList.AsReadOnly();
		});

		/// <summary>
		/// Read-only collection of the acceptable colors.
		/// </summary>
		internal static ReadOnlyCollection<string> Colors => ColorToWall.colors.AsReadOnly();

		internal new ColorToWallViewModel DataContext => base.DataContext as ColorToWallViewModel;

		/// <summary>
		/// Adds or removes the event to flash the background of this control.
		/// </summary>
		public event RoutedEventHandler Flash
		{
			add { this.AddHandler(ColorToWall.FlashEvent, value); }
			remove { this.AddHandler(ColorToWall.FlashEvent, value); }
		}

		bool ProcessColorChange { get; set; }

		/// <summary>
		/// Adds or removes the event for handling the selected color changing.
		/// </summary>
		public event EventHandler<WallSelectorChangedEventArgs> SelectedColorChanged
		{
			add { this.AddHandler(ColorToWall.SelectedColorChangedEvent, value); }
			remove { this.RemoveHandler(ColorToWall.SelectedColorChangedEvent, value); }
		}

		ColorToWall() => this.InitializeComponent();

		public static async Task<ColorToWall> Create(Color color, int numberOfPixels, EventHandler<WallSelectorChangedEventArgs> selectedColorChanged)
		{
			var colorToWall = new ColorToWall();
			colorToWall.cbWall.ItemsSource = await ColorToWall.Walls;
			colorToWall.cbWallColor.ItemsSource = ColorToWall.Colors;
			colorToWall.DataContext.Color = colorToWall.DataContext.SelectedColor = color;
			colorToWall.DataContext.NumberOfPixels = numberOfPixels;
			colorToWall.SelectedColorChanged += selectedColorChanged;
			colorToWall.ProcessColorChange = true;
			if (color.A == 0)
			{
				colorToWall.DataContext.SelectedWall = await WallItem.Create("Glass Wall");
				colorToWall.Wall_SelectionChanged(colorToWall, null);
			}
			return colorToWall;
		}

		public void DoFlash() => this.RaiseEvent(new RoutedEventArgs(ColorToWall.FlashEvent));

		async void FindClosestColor_Click(object sender, RoutedEventArgs e)
		{
			// Show the overlay on the main window and show the Find Closest Color dialog box.
			var mainWindow = Window.GetWindow(this) as MainWindow;
			mainWindow.ShowOverlay(false);
			var findClosestColor = await FindClosestColor.Create(this.DataContext.Color);
			findClosestColor.Owner = mainWindow;
			if (findClosestColor.ShowDialog() ?? false)
			{
				// This will only happen if the user clicked OK in the Find Closest Color dialog box.
				this.ProcessColorChange = false;
				this.DataContext.SelectedWall = await WallItem.Create(findClosestColor.DataContext.SelectedColorItem.WallName);
				this.DataContext.SelectedWallColor = findClosestColor.DataContext.SelectedColorItem.ColorName;
				this.ProcessColorChange = true;
				this.Wall_SelectionChanged(this, null);
			}
			mainWindow.HideOverlay();
		}

		void RaiseSelectedColorChangedEvent() => this.RaiseEvent(new WallSelectorChangedEventArgs(this.DataContext.Color, this.DataContext.CurrentWallName, this.DataContext.SelectedWallColor,
			ColorToWall.SelectedColorChangedEvent));

		void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			// Reset back to No Wall, Uncolored (this will also disable the wall color combobox).
			this.ProcessColorChange = false;
			this.DataContext.SelectedWallIndex = this.DataContext.SelectedWallColorIndex = 0;
			this.ProcessColorChange = true;
			this.Wall_SelectionChanged(this, null);
		}

		void Wall_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ProcessColorChange)
			{
				// When either the wall or the wall color changes, update the selected color.
				var wallColor = ColorExtension.GetMapWallColor(this.DataContext.CurrentWallName);
				if (wallColor.HasValue)
					wallColor = wallColor.Value.ColorizeMap(this.DataContext.SelectedWallColor);
				this.DataContext.SelectedColor = wallColor ?? this.DataContext.Color;
				this.RaiseSelectedColorChangedEvent();
			}
		}
	}

	/// <summary>
	/// The view model for <see cref="ColorToWall" />.
	/// </summary>
	internal class ColorToWallViewModel
	{
		/// <summary>
		/// The color associated with the control.
		/// </summary>
		public Color Color { get; set; } = Colors.White;

		/// <summary>
		/// The text representation of the color.
		/// </summary>
		[SafeForDependencyAnalysis]
		public string ColorText => $"({this.Color.R}, {this.Color.G}, {this.Color.B}, {this.Color.A})";

		/// <summary>
		/// The name of the currently selected wall.
		/// </summary>
		public string CurrentWallName => this.SelectedWall?.Name ?? "";

		/// <summary>
		/// The number of pixels that match the color.
		/// </summary>
		public int NumberOfPixels { get; set; }

		/// <summary>
		/// The color of the selected wall and wall color.
		/// </summary>
		public Color SelectedColor { get; set; } = Colors.DarkGray;

		/// <summary>
		/// The text representation of the selected color.
		/// </summary>
		[SafeForDependencyAnalysis]
		public string SelectedColorText => $"({this.SelectedColor.R}, {this.SelectedColor.G}, {this.SelectedColor.B}, {this.SelectedColor.A})";

		/// <summary>
		/// The currently selected wall.
		/// </summary>
		public WallItem SelectedWall { get; set; }

		/// <summary>
		/// The currently selected wall color.
		/// </summary>
		public string SelectedWallColor { get; set; }

		/// <summary>
		/// The index of the currently selected wall color.
		/// </summary>
		public int SelectedWallColorIndex { get; set; }

		/// <summary>
		/// The index of the currently selected wall.
		/// </summary>
		public int SelectedWallIndex { get; set; }

		/// <summary>
		/// The name of the currently selected wall and if applicable, the currently selected wall color.
		/// </summary>
		public string SelectedWallName => $"{this.CurrentWallName}{(this.SelectedWallIndex != 0 ? $", {this.SelectedWallColor}" : "")}";
	}

	/// <summary>
	/// The event data for use when a <see cref="ColorToWall" /> control has had its wall or wall color changed.
	/// </summary>
	public class WallSelectorChangedEventArgs : RoutedEventArgs
	{
		/// <summary>The color of the wall selector.</summary>
		public Color Color { get; }

		/// <summary>The wall name of the wall selector.</summary>
		public string WallName { get; }

		/// <summary>The wall color name of the wall selector.</summary>
		public string ColorName { get; }

		public WallSelectorChangedEventArgs(Color color, string wallName, string colorName, RoutedEvent routedEvent) : base(routedEvent)
		{
			this.Color = color;
			this.WallName = wallName;
			this.ColorName = colorName;
		}
	}
}
