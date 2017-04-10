using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// A user control to select a wall and wall color for a specific color from the original image.
	/// </summary>
	public partial class ColorToWall : UserControl
	{
		// List of acceptable walls (ones which can be crafted, placed by a user and which don't animate). Comes from items where Terraria.Item.createWall is set to a non-0 value.
		// Names of the walls come from Terraria.Lang and checking for the respective item IDs.
		static readonly List<WallItem> walls = new List<WallItem>()
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
		};
		// List of acceptable colors. Comes from items where Terraria.Item.paint is set to a non-0 value.
		static readonly List<string> colors = new List<string>()
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

		/// <summary>Read-only collection of the acceptable walls.</summary>
		public static ReadOnlyCollection<WallItem> Walls => ColorToWall.walls.AsReadOnly();

		/// <summary>Read-only collection of the acceptable colors.</summary>
		public static ReadOnlyCollection<string> Colors => ColorToWall.colors.AsReadOnly();

		public ColorToWall()
		{
			this.InitializeComponent();

			// Order the acceptable walls by name and then insert a "No Wall" option at the start
			var thisWalls = ColorToWall.walls.OrderBy(w => w.Name).ToList();
			thisWalls.Insert(0, "No Wall");
			this.cbWall.DataSource = thisWalls;

			this.cbWallColor.DataSource = ColorToWall.colors.ToList();
		}

		/// <summary>
		/// Gets or sets the color associated to this control.
		/// </summary>
		public Color Color
		{
			get { return this.pnlColor.BackColor; }
			set
			{
				this.pnlColor.BackColor = value;
				this.UpdateColorLabel();

				if (value.A == 0)
					this.cbWall.SelectedIndex = (this.cbWall.DataSource as List<WallItem>).IndexOf("Glass Wall");
			}
		}

		/// <summary>
		/// Updates the label showing the current color.
		/// </summary>
		public void UpdateColorLabel() => this.lblColor.Text = FormattableString.Invariant($"({this.Color.R}, {this.Color.G}, {this.Color.B}, {this.Color.A})");

		int numberOfPixels = 0;

		/// <summary>
		/// Gets or sets the number of pixels that share the color associated to this control.
		/// </summary>
		public int NumberOfPixels
		{
			get { return this.numberOfPixels; }
			set
			{
				this.numberOfPixels = value;
				this.UpdateNumberOfPixelsLabel();
			}
		}

		/// <summary>
		/// Updates the label showing the number of pixels that share the current color.
		/// </summary>
		public void UpdateNumberOfPixelsLabel() => this.lblNumberOfPixels.Text = FormattableString.Invariant($"Number of Pixels: {this.NumberOfPixels}");

		/// <summary>Gets the current wall name.</summary>
		public string CurrentWallName => (this.cbWall.SelectedItem as WallItem).Name;

		/// <summary>Gets the current wall color name.</summary>
		public string CurrentColorName => this.cbWallColor.SelectedItem as string;

		/// <summary>
		/// Gets the full name of the wall that has been selected by this control, for use in the status bar when hovering over a tile.
		/// </summary>
		public string SelectedWall =>
			FormattableString.Invariant($"{(this.cbWall.SelectedItem as WallItem).Name}{(this.cbWall.SelectedIndex != 0 ? FormattableString.Invariant($", {this.CurrentColorName}") : "")}");

		// TODO: Somehow figure out how to make it so the flash can be stopped mid-way

		public void Flash(int interval, Color color, int flashes) => new Thread(() => this.FlashInternal(interval, color, flashes)).Start();

		public void UpdateControlBackColor(Color color)
		{
			if (this.InvokeRequired)
				this.Invoke((Action<Color>)this.UpdateControlBackColor, color);
			this.BackColor = color;
		}

		void FlashInternal(int interval, Color flashColor, int flashes)
		{
			var original = this.BackColor;
			for (int i = 0; i < flashes; ++i)
			{
				this.UpdateControlBackColor(flashColor);
				Thread.Sleep(interval / 2);
				this.UpdateControlBackColor(original);
				Thread.Sleep(interval / 2);
			}
		}

		public event EventHandler<WallSelectorChangedEventArgs> WallSelectorChanged;

		void cbWall_SelectedIndexChanged(object sender, EventArgs e)
		{
			string wallItemName = this.CurrentWallName;
			this.cbWallColor.Enabled = wallItemName != "No Wall";

			this.WallSelectorChanged?.Invoke(sender, new WallSelectorChangedEventArgs(this.Color, wallItemName, this.CurrentColorName));
		}

		public event EventHandler<WallSelectorChangedEventArgs> WallColorSelectorChanged;

		void cbWallColor_SelectedIndexChanged(object sender, EventArgs e) =>
			this.WallColorSelectorChanged?.Invoke(sender, new WallSelectorChangedEventArgs(this.Color, this.CurrentWallName, this.CurrentColorName));

		void btnFindClosestColor_Click(object sender, EventArgs e)
		{
			using (var findClosestColor = new FindClosestColor(this.Color))
			{
				if (findClosestColor.ShowDialog(this.Parent) == DialogResult.OK)
				{
					this.cbWall.SelectedItem = findClosestColor.SelectedColorItem.WallItem;
					this.cbWallColor.SelectedItem = findClosestColor.SelectedColorItem.Color;
				}
			}
		}

		void btnReset_Click(object sender, EventArgs e)
		{
			this.cbWallColor.SelectedItem = "Uncolored";
			this.cbWall.SelectedIndex = 0;
		}
	}

	/// <summary>
	/// The event data for use when a <see cref="ColorToWall" /> control has had its wall or wall color changed.
	/// </summary>
	public class WallSelectorChangedEventArgs : EventArgs
	{
		/// <summary>Gets the color of the wall selector.</summary>
		public Color Color { get; }

		/// <summary>Gets the wall name of the wall selector.</summary>
		public string WallName { get; }

		/// <summary>Gets the wall color name of the wall selector.</summary>
		public string ColorName { get; }

		public WallSelectorChangedEventArgs(Color color, string wallName, string colorName) : base()
		{
			this.Color = color;
			this.WallName = wallName;
			this.ColorName = colorName;
		}
	}
}
