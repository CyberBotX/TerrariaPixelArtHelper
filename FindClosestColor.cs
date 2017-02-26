using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// A form to find the closest combination of wall and wall color to the given color.
	/// </summary>
	public partial class FindClosestColor : Form
	{
		internal struct CachedColorKey
		{
			public WallItem WallItem { get; set; }

			public string Color { get; set; }

			public override string ToString() => FormattableString.Invariant($"{{{this.WallItem}, {this.Color}}}");
		}

		internal struct CachedColorValue
		{
			public Color WallColor { get; set; }

			public LAB LABColor { get; set; }

			public override string ToString() => FormattableString.Invariant($"{{{this.WallColor}, {this.LABColor}}}");
		}

		public struct ColorItem : IEquatable<ColorItem>
		{
			public WallItem WallItem { get; set; }

			public string Color { get; set; }

			public Color WallColor { get; set; }

			public double Distance { get; set; }

			public string DisplayMember => FormattableString.Invariant($"{this.WallItem}, {this.Color}");

			public override bool Equals(object obj) => obj != null && obj is ColorItem && this.Equals((ColorItem)obj);

			public bool Equals(ColorItem other) => this == other;

			public static bool operator==(ColorItem colorItem1, ColorItem colorItem2) => colorItem1.WallItem == colorItem2.WallItem && colorItem1.Color == colorItem2.Color &&
				colorItem1.WallColor == colorItem2.WallColor && colorItem1.Distance == colorItem2.Distance;

			public static bool operator!=(ColorItem colorItem1, ColorItem colorItem2) => !(colorItem1 == colorItem2);

			public override int GetHashCode() => this.WallItem.GetHashCode() ^ this.Color.GetHashCode() ^ this.WallColor.GetHashCode() ^ this.Distance.GetHashCode();

			public override string ToString() => FormattableString.Invariant($"{{{this.DisplayMember}, {this.WallColor}, {this.Distance}}}");
		}

		static Dictionary<CachedColorKey, CachedColorValue> cachedColors = ColorToWall.Walls.Where(w => w.Name != "Glass Wall").SelectMany(w => ColorToWall.Colors, (w, c) =>
		{
			var wallColor = ColorExtension.GetMapWallColor(w.Name).Value.ColorizeMap(c);
			return new { w, c, wallColor = (Color)wallColor, labColor = LAB.FromRGB(wallColor) };
		}).ToDictionary(x => new CachedColorKey() { WallItem = x.w, Color = x.c }, x => new CachedColorValue() { WallColor = x.wallColor, LABColor = x.labColor });

		public FindClosestColor(Color originalColor)
		{
			this.InitializeComponent();

			var originalLAB = LAB.FromRGB(originalColor);
			var colors = FindClosestColor.cachedColors.Select(c => new ColorItem()
			{
				WallItem = c.Key.WallItem,
				Color = c.Key.Color,
				WallColor = c.Value.WallColor,
				Distance = LAB.Distance(originalLAB, c.Value.LABColor)
			}).OrderBy(c => c.Distance);
			var bestMatch = colors.First();

			this.lbColors.DataSource = colors.ToList();

			this.OriginalColor = originalColor;
			this.SelectedColor = bestMatch.WallColor;
			this.selectedColorItem = bestMatch;
			this.UpdateSelectedColorWall();
		}

		public Color OriginalColor
		{
			get { return this.pnlOriginalColor.BackColor; }
			set
			{
				this.pnlOriginalColor.BackColor = value;
				this.UpdateOriginalColorLabel();
			}
		}

		public void UpdateOriginalColorLabel() =>
			this.lblOriginalColor.Text = FormattableString.Invariant($"({this.OriginalColor.R}, {this.OriginalColor.G}, {this.OriginalColor.B}, {this.OriginalColor.A})");

		public Color SelectedColor
		{
			get { return this.pnlSelectedColor.BackColor; }
			set
			{
				this.pnlSelectedColor.BackColor = value;
				this.UpdateSelectedColorLabel();
			}
		}

		ColorItem selectedColorItem = new ColorItem();

		public ColorItem SelectedColorItem => this.selectedColorItem;

		public void UpdateSelectedColorLabel() =>
			this.lblSelectedColor.Text = FormattableString.Invariant($"({this.SelectedColor.R}, {this.SelectedColor.G}, {this.SelectedColor.B}, {this.SelectedColor.A})");

		public void UpdateSelectedColorWall()
		{
			this.pnlWall.BackgroundImage = MainForm.GetWallFrame(this.selectedColorItem.WallItem.Name, this.selectedColorItem.Color, 0, 0, 0);
		}

		void lbColors_SelectedIndexChanged(object sender, EventArgs e)
		{
			var color = (ColorItem)this.lbColors.SelectedItem;
			this.SelectedColor = color.WallColor;
			this.selectedColorItem = color;
			this.UpdateSelectedColorWall();
		}
	}
}
