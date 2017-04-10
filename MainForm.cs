using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using nQuant;
using TerrariaPixelArtHelper.Properties;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	public partial class MainForm : Form
	{
		static Dictionary<Color, Bitmap> cachedColorBlocks = new Dictionary<Color, Bitmap>();
		static Dictionary<Tuple<string, string>, Bitmap> cachedColorWalls = new Dictionary<Tuple<string, string>, Bitmap>();
		static Dictionary<Tuple<string, string>, Bitmap> cachedColorMapWalls = new Dictionary<Tuple<string, string>, Bitmap>();
		static Bitmap currentBitmap = null;
		static Dictionary<Color, int> currentImageColors = new Dictionary<Color, int>();
		static Dictionary<Point, WallInfo> currentWallInfo = new Dictionary<Point, WallInfo>();

		static Random rand = new Random();

		struct BitmapData
		{
			public Bitmap ZoomedBitmap { get; set; }

			public Dictionary<Color, int> Colors { get; set; }

			public Dictionary<Point, WallInfo> WallInfo { get; set; }
		}

		class WallInfo
		{
			public string WallName { get; set; }

			public int WallFrame { get; set; }
		}

		/// <summary>
		/// Will convert an image to be 32bpp, if necessary.
		/// </summary>
		/// <param name="origBitmap">The original bitmap. It will be disposed if replaced, so use only the return value.</param>
		/// <returns>The new image in 32bpp, if necessary.</returns>
		static Bitmap Make32BPPBitmap(Bitmap origBitmap)
		{
			if (origBitmap.PixelFormat != PixelFormat.Format32bppArgb)
			{
				var newBitmap = new Bitmap(origBitmap.Width, origBitmap.Height, PixelFormat.Format32bppArgb);
				using (var g = Graphics.FromImage(newBitmap))
					g.DrawImage(origBitmap, new Rectangle(0, 0, origBitmap.Width, origBitmap.Height));
				origBitmap.Dispose();
				return newBitmap;
			}
			else
				return origBitmap;
		}

		public MainForm()
		{
			this.InitializeComponent();

			using (var ms = new MemoryStream(Resources.Eyedropper))
				this.pbInGame.Cursor = this.pbMap.Cursor = new Cursor(ms);

			this.tbWalls.Font = new Font(FontFamily.GenericMonospace, this.tbWalls.Font.Size);
			this.tbColors.Font = new Font(FontFamily.GenericMonospace, this.tbColors.Font.Size);

			// The grid controls are added to the non-grid controls as children so transparency can be used. This cannot be done in the designer, which is why it is done here instead.
			this.pbInGame.Controls.Add(this.pbInGameGrid);
			this.pbMap.Controls.Add(this.pbMapGrid);
		}

		void colorToWall_WallSelectorChanged(object sender, WallSelectorChangedEventArgs e)
		{
			var locked = new LockBitmap(MainForm.currentBitmap);
			locked.LockBits();

			var color = (Color)e.Color;

			using (var g1 = Graphics.FromImage(this.pbInGame.Image))
			using (var g2 = Graphics.FromImage(this.pbMap.Image))
				for (int x = 0; x < locked.Width; ++x)
					for (int y = 0; y < locked.Height; ++y)
					{
						if (locked.GetPixel(x, y) != color)
							continue;

						// Using the existing wall frame or getting a random one comes from Terraria.Framing.WallFrame().
						var point = new Point(x, y);
						var wallInfo = MainForm.currentWallInfo[point];
						int wallFrameNum;
						if (wallInfo != null && wallInfo.WallName == e.WallName)
							wallFrameNum = wallInfo.WallFrame;
						else
							wallFrameNum = MainForm.rand.Next(0, 2);

						var wallFrame = MainForm.GetWallFrame(e.WallName, e.ColorName, x, y, wallFrameNum);
						if (wallFrame == null)
						{
							MainForm.currentWallInfo[point] = null;
							wallFrame = MainForm.cachedColorBlocks[e.Color];
						}
						else
							MainForm.currentWallInfo[point] = new WallInfo()
							{
								WallName = e.WallName,
								WallFrame = wallFrameNum
							};

						g1.EraseRectangle(new Rectangle(x * 16, y * 16, 16, 16));
						g1.DrawImage(wallFrame, x * 16, y * 16);

						var mapWall = MainForm.GetColoredMapWall(e.WallName, e.ColorName);
						if (mapWall == null)
							mapWall = MainForm.cachedColorBlocks[e.Color];

						g2.EraseRectangle(new Rectangle(x * 16, y * 16, 16, 16));
						g2.DrawImage(mapWall, x * 16, y * 16);
					}

			locked.UnlockBits();

			this.pbInGame.Invalidate();
			this.pbMap.Invalidate();

			this.UpdateNeededWallsAndColors();
		}

		void pictureBox_Click(object sender, EventArgs e)
		{
			if (MainForm.currentBitmap == null)
				return;
			var mouseArgs = e as MouseEventArgs;
			int x = mouseArgs.X / 16;
			int y = mouseArgs.Y / 16;
			var color = MainForm.currentBitmap.GetPixel(x, y);
			int colorIndex = MainForm.currentImageColors.Keys.ToList().IndexOf(color);
			(this.flpColorToWall.Controls[colorIndex] as ColorToWall).Flash(500, Color.Red, 3);
			this.flpColorToWall.ScrollControlIntoView(this.flpColorToWall.Controls[colorIndex]);
		}

		// The following 2 variables plus the function come from http://stackoverflow.com/a/1041986
		Point prevPanel1Pos = new Point();
		Point prevPanel2Pos = new Point();

		void pictureBox_Paint(object sender, PaintEventArgs e)
		{
			if (this.pnlInGame.AutoScrollPosition != this.prevPanel1Pos)
			{
				this.pnlMap.AutoScrollPosition = new Point(-this.pnlInGame.AutoScrollPosition.X, -this.pnlInGame.AutoScrollPosition.Y);
				this.prevPanel1Pos = this.pnlInGame.AutoScrollPosition;
			}
			else if (this.pnlMap.AutoScrollPosition != this.prevPanel2Pos)
			{
				this.pnlInGame.AutoScrollPosition = new Point(-this.pnlMap.AutoScrollPosition.X, -this.pnlMap.AutoScrollPosition.Y);
				this.prevPanel2Pos = this.pnlMap.AutoScrollPosition;
			}
		}

		void picutreBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (MainForm.currentBitmap == null)
				return;
			int x = e.X / 16;
			int y = e.Y / 16;
			if (x == MainForm.currentBitmap.Width || y == MainForm.currentBitmap.Height)
				return;
			var color = MainForm.currentBitmap.GetPixel(x, y);
			int colorIndex = MainForm.currentImageColors.Keys.ToList().IndexOf(color);
			var colorToWall = this.flpColorToWall.Controls[colorIndex] as ColorToWall;
			this.tsslPipe.Visible = true;
			this.tsslCurrentPixel.Text = FormattableString.Invariant($"Current Pixel: {x}, {y} | Currently Selected Wall: {colorToWall.SelectedWall}");
		}

		void pictureBox_MouseLeave(object sender, EventArgs e)
		{
			this.tsslPipe.Visible = false;
			this.tsslCurrentPixel.Text = "";
		}

		void miExit_Click(object sender, EventArgs e) => this.Close();

		void HandleImage(Bitmap origBitmap)
		{
			origBitmap = MainForm.Make32BPPBitmap(origBitmap);
			var bitmapData = MainForm.ZoomBy16(origBitmap);

			if (bitmapData.Colors.Count > 256 &&
				MessageBox.Show(this, "The selected image contains over 256 colors. Would you like to reduce its color count?\n\n(If you select No, it may take forever to handle this image.)",
				"Color count too high", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				var quantizer = new WuQuantizer();
				var newBitmap = quantizer.QuantizeImage(origBitmap) as Bitmap;
				origBitmap.Dispose();
				origBitmap = MainForm.Make32BPPBitmap(newBitmap);
				bitmapData = MainForm.ZoomBy16(origBitmap);
			}

			this.tsslImageDimensions.Text = FormattableString.Invariant($"Image Size: {origBitmap.Width}x{origBitmap.Height}");

			var gridBitmap = new Bitmap(bitmapData.ZoomedBitmap.Width + 1, bitmapData.ZoomedBitmap.Height + 1, PixelFormat.Format32bppArgb);
			using (var g = Graphics.FromImage(gridBitmap))
			using (var pen = new Pen(Color.Black, 1)
			{
				DashStyle = DashStyle.Dash
			})
			{
				for (int x = 0; x <= origBitmap.Width; ++x)
					g.DrawLine(pen, new Point(x * 16, 0), new Point(x * 16, bitmapData.ZoomedBitmap.Height + 1));
				for (int y = 0; y <= origBitmap.Height; ++y)
					g.DrawLine(pen, new Point(0, y * 16), new Point(bitmapData.ZoomedBitmap.Width + 1, y * 16));
			}

			var zoomedBitmapExtra = new Bitmap(bitmapData.ZoomedBitmap.Width + 1, bitmapData.ZoomedBitmap.Height + 1, PixelFormat.Format32bppArgb);
			using (var g = Graphics.FromImage(zoomedBitmapExtra))
				g.DrawImage(bitmapData.ZoomedBitmap, new Point(0, 0));

			this.pbInGame.Image = new Bitmap(zoomedBitmapExtra);
			this.pbMap.Image = new Bitmap(zoomedBitmapExtra);
			this.pbInGameGrid.Image = new Bitmap(gridBitmap);
			this.pbMapGrid.Image = new Bitmap(gridBitmap);

			MainForm.currentBitmap = origBitmap;
			MainForm.currentImageColors = bitmapData.Colors;
			MainForm.currentWallInfo = bitmapData.WallInfo;
			this.flpColorToWall.Controls.Clear();
			this.flpColorToWall.SuspendLayout();
			foreach (var color in bitmapData.Colors)
			{
				var colorToWall = new ColorToWall();
				colorToWall.WallSelectorChanged += this.colorToWall_WallSelectorChanged;
				colorToWall.WallColorSelectorChanged += this.colorToWall_WallSelectorChanged;
				colorToWall.Color = color.Key;
				colorToWall.NumberOfPixels = color.Value;
				this.flpColorToWall.Controls.Add(colorToWall);
			}
			this.flpColorToWall.ResumeLayout();

			this.UpdateNeededWallsAndColors();
		}

		void miOpenFromFile_Click(object sender, EventArgs e)
		{
			// TODO: An error if the opened file isn't an image

			if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				var origBitmap = Image.FromFile(this.OpenFileDialog.FileName) as Bitmap;
				if (origBitmap != null)
					this.HandleImage(origBitmap);
			}
		}

		void miOpenFromClipboard_Click(object sender, EventArgs e)
		{
			// TODO: An error if the clipboard doesn't contain an image

			try
			{
				var data = Clipboard.GetDataObject();
				var newBitmap = Image.FromStream(data.GetData(data.GetFormats()[0]) as MemoryStream) as Bitmap;
				if (newBitmap != null)
					this.HandleImage(newBitmap);
			}
			catch
			{
			}
		}

		void miToggleGrid_Click(object sender, EventArgs e) => this.pbInGameGrid.Visible = this.pbMapGrid.Visible = this.miToggleGrid.Checked = !this.miToggleGrid.Checked;

		void miAbout_Click(object sender, EventArgs e) =>
			MessageBox.Show(this, "Terraria Pixel Art Helper\nBy: Naram Qashat (CyberBotX)", "About", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

		void UpdateNeededWallsAndColors()
		{
			var wallPixels = new Dictionary<string, int>();
			var colorPixels = new Dictionary<string, int>();
			foreach (ColorToWall colorToWall in this.flpColorToWall.Controls)
			{
				string wallName = colorToWall.CurrentWallName;
				if (wallName != "No Wall")
				{
					if (!wallPixels.ContainsKey(wallName))
						wallPixels[wallName] = 0;
					wallPixels[wallName] += colorToWall.NumberOfPixels;
					string colorName = colorToWall.CurrentColorName;
					if (colorName != "Uncolored")
					{
						if (!colorPixels.ContainsKey(colorName))
							colorPixels[colorName] = 0;
						colorPixels[colorName] += colorToWall.NumberOfPixels;
					}
				}
			}
			this.tbWalls.Clear();
			this.tbColors.Clear();
			if (wallPixels.Count != 0)
				this.tbWalls.Text = string.Join(Environment.NewLine, wallPixels.OrderBy(wp => wp.Key).Select(wp => FormattableString.Invariant($"{wp.Key}: {wp.Value}")));
			if (colorPixels.Count != 0)
				this.tbColors.Text = string.Join(Environment.NewLine, colorPixels.OrderBy(cp => ColorToWall.Colors.IndexOf(cp.Key)).Select(cp => FormattableString.Invariant($"{cp.Key}: {cp.Value}")));
		}

		internal static Bitmap GetWall(string wallName) => Resources.ResourceManager.GetObject(FormattableString.Invariant($"Wall_{wallName.Replace(" ", "")}"), Resources.Culture) as Bitmap;

		static Bitmap GetColoredWall(string wallName, string color)
		{
			var wallTuple = Tuple.Create(wallName, color);
			Bitmap wallBitmap;
			if (!MainForm.cachedColorWalls.TryGetValue(wallTuple, out wallBitmap))
			{
				wallBitmap = MainForm.GetWall(wallName);

				if (color != "Uncolored" && wallBitmap != null)
				{
					var locked = new LockBitmap(wallBitmap);
					locked.LockBits();

					for (int x = 0; x < locked.Width; ++x)
						for (int y = 0; y < locked.Height; ++y)
							locked.SetPixel(x, y, locked.GetPixel(x, y).ColorizeInGame(color));

					locked.UnlockBits();
				}

				MainForm.cachedColorWalls[wallTuple] = wallBitmap;
			}
			return wallBitmap;
		}

		internal static Bitmap GetWallFrame(string wallName, string color, int i, int j, int frame)
		{
			var wallBitmap = MainForm.GetColoredWall(wallName, color);
			if (wallBitmap == null)
				return null;

			// This section for picking y comes from the part of Terraria.Framing (partially the WallFrame() method) that picks a wall frame based on the X and Y coordinates of the tile
			int x = frame, y = -1;
			if (i % 3 == 1 && j % 3 == 1)
				y = 1;
			else if (i % 3 == 0 && j % 3 == 0)
				y = 2;
			else if (i % 3 == 2 && j % 3 == 1)
				y = 3;
			else if (i % 3 == 1 && j % 3 == 2)
				y = 4;
			else
				y = 0;

			var wallFrame = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
			using (var g = Graphics.FromImage(wallFrame))
				g.DrawImage(wallBitmap, 0, 0, new Rectangle(x * 16, y * 16, 16, 16), GraphicsUnit.Pixel);

			return wallFrame;
		}

		static Bitmap GetColoredMapWall(string wallName, string color)
		{
			var wallTuple = Tuple.Create(wallName, color);
			Bitmap wallBitmap;
			if (!MainForm.cachedColorMapWalls.TryGetValue(wallTuple, out wallBitmap))
			{
				var wallColor = ColorExtension.GetMapWallColor(wallName);
				if (wallColor.HasValue)
					wallColor = wallColor.Value.ColorizeMap(color);

				wallBitmap = null;

				if (wallColor.HasValue)
				{
					wallBitmap = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
					using (var g = Graphics.FromImage(wallBitmap))
					using (var brush = new SolidBrush(wallColor.Value))
						g.FillRectangle(brush, 0, 0, 16, 16);
				}

				MainForm.cachedColorMapWalls[wallTuple] = wallBitmap;
			}
			return wallBitmap;
		}

		static BitmapData ZoomBy16(Bitmap orig)
		{
			var zoomed = new Bitmap(orig.Width * 16, orig.Height * 16, orig.PixelFormat);
			var locked = new LockBitmap(orig);
			locked.LockBits();

			MainForm.cachedColorBlocks = new Dictionary<Color, Bitmap>();
			var colors = new Dictionary<Color, int>();
			var wallInfo = new Dictionary<Point, WallInfo>();
			using (var zoomg = Graphics.FromImage(zoomed))
				for (int x = 0; x < locked.Width; ++x)
					for (int y = 0; y < locked.Height; ++y)
					{
						var color = locked.GetPixel(x, y);
						if (!colors.ContainsKey(color))
							colors.Add(color, 1);
						else
							++colors[color];

						wallInfo.Add(new Point(x, y), null);

						Bitmap fill;
						if (!MainForm.cachedColorBlocks.TryGetValue(color, out fill))
						{
							fill = new Bitmap(16, 16, PixelFormat.Format32bppArgb);
							using (var fillg = Graphics.FromImage(fill))
							using (var brush = new SolidBrush(color))
								fillg.FillRectangle(brush, 0, 0, 16, 16);
							MainForm.cachedColorBlocks[color] = fill;
						}
						zoomg.DrawImage(fill, x * 16, y * 16);
					}

			locked.UnlockBits();

			return new BitmapData()
			{
				ZoomedBitmap = zoomed,
				Colors = colors,
				WallInfo = wallInfo
			};
		}
	}
}
