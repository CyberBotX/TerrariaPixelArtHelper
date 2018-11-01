using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using PostSharp.Patterns.Contracts;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		#region GetColoredMapWallPixels

		static readonly ConcurrentDictionary<(string wallName, string color), Task<int[]>> colorMapWallsPixelsCache = new ConcurrentDictionary<(string wallName, string color), Task<int[]>>();

		/// <summary>
		/// Gets the colored pixels of a wall on the map.
		/// </summary>
		/// <param name="wallName">The name of the wall to get the pixels of.</param>
		/// <param name="color">The color of the wall to get the pixels of.</param>
		/// <returns>The pixels of the colored map wall.</returns>
		internal static async Task<int[]> GetColoredMapWallPixels([Required] string wallName, [Required] string color) => await App.colorMapWallsPixelsCache.GetOrAdd((wallName, color), async key =>
		{
			// Get the map color for the wall and colorize it if necessary.
			var wallColor = ColorExtension.GetMapWallColor(wallName);
			if (wallColor.HasValue)
				wallColor = wallColor.Value.ColorizeMap(color);

			// If we have a proper wall color, return the pixels, return null otherwise.
			return wallColor.HasValue ? await Task.Run(() => new int[256].Populate((wallColor.Value.R << 16) | (wallColor.Value.G << 8) | wallColor.Value.B | (wallColor.Value.A << 24))) : null;
		});

		#endregion

		#region GetColoredWallPixels

		static readonly ConcurrentDictionary<(string wallName, string color), Task<int[]>> colorWallsPixelsCache = new ConcurrentDictionary<(string wallName, string color), Task<int[]>>();

		/// <summary>
		/// Gets the colored pixels of a wall in-game.
		/// </summary>
		/// <param name="wallName">The name of the wall to get the pixels of.</param>
		/// <param name="color">The name of the color to get the pixels of.</param>
		/// <returns>The pixels of the colored in-game wall.</returns>
		internal static async Task<int[]> GetColoredWallPixels([Required] string wallName, [Required] string color) => await App.colorWallsPixelsCache.GetOrAdd((wallName, color), async key =>
		{
			// Get the pixels of the in-game wall.
			int[] pixels = await App.GetWallPixels(wallName);

			// Only colorize the wall if we are using an actual color and had pixels.
			if (color != "Uncolored" && pixels != null)
				await Task.Run(() =>
				{
					// Create a copy of the pixels so we don't muck up the original wall's pixels.
					pixels = pixels.ToArray();
					// Colorize all the pixels.
					int index = 0;
					for (int y = 0; y < App.WallPixelHeight; ++y)
						for (int x = 0; x < App.WallPixelWidth; ++x)
						{
							int c = pixels[index];
							var newColor = Color.FromArgb((byte)(c >> 24), (byte)((c >> 16) & 0xFF), (byte)((c >> 8) & 0xFF), (byte)(c & 0xFF)).ColorizeInGame(color);
							pixels[index++] = (newColor.R << 16) | (newColor.G << 8) | newColor.B | (newColor.A << 24);
						}
				});

			// Return the pixels, if any.
			return pixels;
		});

		#endregion

		#region GetWallPixels

		internal const int WallPixelWidth = 48;
		internal const int WallPixelHeight = 80;

		static readonly ConcurrentDictionary<string, Task<int[]>> wallPixelsCache = new ConcurrentDictionary<string, Task<int[]>>();

		/// <summary>
		/// Gets the pixels of a wall in-game.
		/// </summary>
		/// <param name="wallName">The name of the wall to get the pixels of.</param>
		/// <returns>The pixels of the in-game wall.</returns>
		internal static async Task<int[]> GetWallPixels([Required] string wallName) => await App.wallPixelsCache.GetOrAdd(wallName, async key =>
		{
			try
			{
				// Attempt to load the image from the application's resources, returning its pixels if successful.
				using (var ms = new MemoryStream())
				{
					await Application.GetResourceStream(new Uri($"WallImages/Wall_{wallName.Replace(" ", "")}.png", UriKind.Relative)).Stream.CopyToAsync(ms);
					var bitmap = new BitmapImage();
					bitmap.BeginInit();
					bitmap.CacheOption = BitmapCacheOption.OnLoad;
					bitmap.StreamSource = ms;
					bitmap.EndInit();
					bitmap.Freeze();
					int[] pixels = new int[bitmap.PixelWidth * bitmap.PixelHeight];
					bitmap.CopyPixels(pixels, bitmap.PixelWidth * 4, 0);
					return pixels;
				}
			}
			catch (InvalidOperationException)
			{
				return null;
			}
			catch (IOException)
			{
				return null;
			}
		});

		#endregion

		#region GetWallFrame

		static readonly ConcurrentDictionary<(string wallName, string color, int i, int j, int frame), Task<BitmapSource>> wallFramesCache =
			new ConcurrentDictionary<(string wallName, string color, int i, int j, int frame), Task<BitmapSource>>();

		/// <summary>
		/// Gets a bitmap of the in-game wall frame.
		/// </summary>
		/// <param name="wallName">The name of the wall to get the bitmap of.</param>
		/// <param name="color">The color of the wall to get the bitmap of.</param>
		/// <param name="i">The X coordinate of the wall frame (used to determine which frame is used).</param>
		/// <param name="j">The Y coordinate of the wall frame (used to determine which frame is used).</param>
		/// <param name="frame">The frame of the wall.</param>
		/// <returns>The bitmap of the in-game wall frame.</returns>
		internal static async Task<BitmapSource> GetWallFrame([Required] string wallName, [Required] string color, int i, int j, int frame) =>
			await App.wallFramesCache.GetOrAdd((wallName, color, i, j, frame), async key =>
			{
				// Get the pixels of the in-game wall frame.
				int[] pixels = await App.GetWallFramePixels(wallName, color, i, j, frame);
				if (pixels == null)
					return null;

				// Create a bitmap out of the pixels.
				var bitmap = PixelManip.Create(16, 16, pixels);
				bitmap.Freeze();
				return bitmap;
			});

		#endregion

		#region GetWallFramePixels

		static readonly ConcurrentDictionary<(string wallName, string color, int i, int j, int frame), Task<int[]>> wallFramesPixelsCache =
			new ConcurrentDictionary<(string wallName, string color, int i, int j, int frame), Task<int[]>>();

		/// <summary>
		/// Gets the pixels of the in-game wall frame.
		/// </summary>
		/// <param name="wallName">The name of the wall to get the pixels of.</param>
		/// <param name="color">The color of the wall to get the pixels of.</param>
		/// <param name="i">The X coordinate of the wall frame (used to determine which frame is used).</param>
		/// <param name="j">The Y coordinate of the wall frame (used to determine which frame is used).</param>
		/// <param name="frame">The frame of the wall.</param>
		/// <returns>The pixels of the in-game wall frame.</returns>
		internal static async Task<int[]> GetWallFramePixels([Required] string wallName, [Required] string color, int i, int j, int frame) =>
			await App.wallFramesPixelsCache.GetOrAdd((wallName, color, i, j, frame), async key =>
			{
				// Get the pixels of the colored in-game wall.
				int[] pixels = await App.GetColoredWallPixels(wallName, color);
				if (pixels == null)
					return null;

				// This section for picking y comes from the part of Terraria.Framing (partially the WallFrame() method) that picks a wall frame based on the X and Y coordinates of the tile.
				int x = frame;
				int y = i % 3 == 1 && j % 3 == 1 ? 1 : (i % 3 == 0 && j % 3 == 0 ? 2 : (i % 3 == 2 && j % 3 == 1 ? 3 : (i % 3 == 1 && j % 3 == 2 ? 4 : 0)));

				// Copy the pixels specifically for the frame we want.
				int[] framePixels = new int[256];
				for (int y2 = 0; y2 < 16; ++y2)
					Array.Copy(pixels, x * 16 + (y * 16 + y2) * App.WallPixelWidth, framePixels, y2 * 16, 16);
				return framePixels;
			});

		#endregion

		/// <summary>
		/// Zooms the given bitmap to 16 times its original size.
		/// </summary>
		/// <param name="orig">The original bitmap to zoom.</param>
		/// <returns>The zoomed bitmap.</returns>
		internal static async Task<BitmapData> ZoomBy16([Required] BitmapSource orig)
		{
			int width = orig.PixelWidth;
			int height = orig.PixelHeight;

			var cachedColorPixels = new Dictionary<Color, int[]>();
			var colors = new Dictionary<Color, int>();
			var pixelInfo = new Dictionary<(int x, int y), PixelInfo>();

			int[] zoomedPixels = new int[width * height * 256];
			await Task.Run(async () =>
			{
				// Get the pixels of the original bitmap.
				int[] pixels = new int[width * height];
				orig.CopyPixels(pixels, width * 4, 0);
				int index = 0;

				// Loop over the pixels of the original bitmap and create 16x16 "pixels" out of them.
				for (int y = 0; y < height; ++y)
					for (int x = 0; x < width; ++x)
					{
						int c = pixels[index++];
						var color = Color.FromArgb((byte)(c >> 24), (byte)((c >> 16) & 0xFF), (byte)((c >> 8) & 0xFF), (byte)(c & 0xFF));
						// Replace any pixels that have 0 alpha with the transparent color, makes things more consistent if there are somehow multiple 0 alpha colors.
						if (color.A == 0)
							color = Colors.Transparent;

						colors.TryGetValue(color, out int colorCount);
						colors[color] = colorCount + 1;

						pixelInfo[(x, y)] = new PixelInfo()
						{
							Color = color
						};

						if (!cachedColorPixels.TryGetValue(color, out int[] fillRowPixels))
							cachedColorPixels[color] = fillRowPixels = new int[256].Populate((color.R << 16) | (color.G << 8) | color.B | (color.A << 24));

						await PixelManip.CopyFrom16x16(fillRowPixels, zoomedPixels, x, y, width);
					}
			});

			return new BitmapData()
			{
				CachedColorPixels = cachedColorPixels,
				Colors = colors,
				PixelInfo = pixelInfo,
				ZoomedPixels = zoomedPixels
			};
		}

		void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			var unhandledException = new UnhandledException(e.Exception);
			if (unhandledException.ShowDialog() ?? false)
				this.Shutdown();
			e.Handled = true;
		}
	}
}
