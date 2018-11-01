using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JeremyAnsel.ColorQuant;
using Microsoft.Win32;
using PostSharp.Patterns.Contracts;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Commands

		public static readonly RoutedUICommand AboutCommand = new RoutedUICommand("_About", "About", typeof(MainWindow));

		public static readonly RoutedUICommand ToggleGridCommand = new RoutedUICommand("Toggle _Grid", "ToggleGrid", typeof(MainWindow), new InputGestureCollection(new[]
		{
			new KeyGesture(Key.G, ModifierKeys.Control)
		}));

		#endregion

		static readonly Random rand = new Random();

		new MainWindowViewModel DataContext => base.DataContext as MainWindowViewModel;

		public MainWindow() => this.InitializeComponent();

		void About_Executed(object sender, ExecutedRoutedEventArgs e) => new About()
		{
			Owner = this
		}.ShowDialog();

		void Close_Executed(object sender, ExecutedRoutedEventArgs e) => this.Close();

		async void ColorToWall_SelectedColorChanged(object sender, WallSelectorChangedEventArgs e)
		{
			// Check if we have an image loaded first.
			var bitmapData = this.DataContext.CurrentBitmapData;
			if (bitmapData == null)
				return;

			int width = this.DataContext.ImageWidth;
			int height = this.DataContext.ImageHeight;
			// Get the pixels into temporary arrays in order to allow use to write all the pixels in a thread to avoid slowdowns from block copying to the writeable bitmap.
			int[] inGamePixels = new int[width * height * 256];
			using (var context = this.DataContext.InGameBitmap.GetBitmapContext(ReadWriteMode.ReadOnly))
				BitmapContext.BlockCopy(context, 0, inGamePixels, 0, width * height * 1024);
			int[] mapPixels = new int[width * height * 256];
			using (var context = this.DataContext.MapBitmap.GetBitmapContext(ReadWriteMode.ReadOnly))
				BitmapContext.BlockCopy(context, 0, mapPixels, 0, width * height * 1024);
			await Task.Run(async () =>
			{
				// Loop over all the pixels of the image.
				for (int x = 0; x < width; ++x)
					for (int y = 0; y < height; ++y)
					{
						// Check if the pixel's color matches the color that was just changed.
						var wallInfo = bitmapData.PixelInfo[(x, y)];
						if (wallInfo.Color != e.Color)
							continue;

						// Using the existing wall frame or getting a random one comes from Terraria.Framing.WallFrame().
						int wallFrameNum = wallInfo.WallName == e.WallName ? wallInfo.WallFrame : MainWindow.rand.Next(0, 3);

						// Get the in-game wall frame (or the original color).
						int[] wallFramePixels = await App.GetWallFramePixels(e.WallName, e.ColorName, x, y, wallFrameNum);
						if (wallFramePixels == null)
						{
							wallInfo.WallName = null;
							wallFramePixels = bitmapData.CachedColorPixels[e.Color];
						}
						else
						{
							wallInfo.WallName = e.WallName;
							wallInfo.WallFrame = wallFrameNum;
						}

						// Update the in-game image with the wall frame.
						await PixelManip.CopyFrom16x16(wallFramePixels, inGamePixels, x, y, width);

						// Get the map wall (or the original color).
						int[] mapWallPixels = await App.GetColoredMapWallPixels(e.WallName, e.ColorName);
						if (mapWallPixels == null)
							mapWallPixels = bitmapData.CachedColorPixels[e.Color];

						// Update the map image with the wall.
						await PixelManip.CopyFrom16x16(mapWallPixels, mapPixels, x, y, width);
					}
			});

			using (var context = this.DataContext.InGameBitmap.GetBitmapContext())
				BitmapContext.BlockCopy(inGamePixels, 0, context, 0, width * height * 1024);
			using (var context = this.DataContext.MapBitmap.GetBitmapContext())
				BitmapContext.BlockCopy(mapPixels, 0, context, 0, width * height * 1024);

			inGamePixels = null;
			mapPixels = null;

			this.UpdateNeededWallsAndColors();
		}

		async Task HandleImage([Required] BitmapSource origBitmap)
		{
			this.ShowOverlay(true, "Processing Image...");

			// Check if the image is in BGRA format, if not, convert it.
			if (origBitmap.Format != PixelFormats.Bgra32 || origBitmap.Format != PixelFormats.Pbgra32)
			{
				origBitmap = new FormatConvertedBitmap(origBitmap, PixelFormats.Bgra32, null, 0);
				origBitmap.Freeze();
			}

			// Zoom the image.
			var bitmapData = await App.ZoomBy16(origBitmap);

			int imageWidth = origBitmap.PixelWidth;
			int imageHeight = origBitmap.PixelHeight;

			// If the image has more than 256 colors, inform the user and give them the option to reduce the color count.
			if (bitmapData.Colors.Count > 256)
			{
				var response = MessageBox.Show(this, "The selected image contains over 256 colors. Would you like to reduce its color count?\n\n(If you select No, it may take a long time to handle this image and the program may possibly crash from having not enough memory.)",
					"Color count too high", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
				if (response == MessageBoxResult.Yes)
				{
					// If the user wants to reduce the color count, use a quantizer to do so.
					var quantizer = new WuAlphaColorQuantizer();
					byte[] bitmapBytes = new byte[origBitmap.PixelWidth * origBitmap.PixelHeight * 4];
					origBitmap.CopyPixels(bitmapBytes, origBitmap.PixelWidth * 4, 0);
					origBitmap = await Task.Run(() =>
					{
						var result = quantizer.Quantize(bitmapBytes);
						// This will create an indexed bitmap from the palette and bytes of the quantizer's result, then convert that into a BGRA image.
						var newIndexedBitmap = BitmapSource.Create(imageWidth, imageHeight, 96, 96, PixelFormats.Indexed8, new BitmapPalette(Enumerable.Range(0, 256).Select(i =>
							Color.FromArgb(result.Palette[i * 4 + 3], result.Palette[i * 4 + 2], result.Palette[i * 4 + 1], result.Palette[i * 4])).ToArray()), result.Bytes, imageWidth);
						newIndexedBitmap.Freeze();
						var newBitmap = new FormatConvertedBitmap(newIndexedBitmap, PixelFormats.Bgra32, null, 0);
						newBitmap.Freeze();
						return newBitmap;
					});
					bitmapData = await App.ZoomBy16(origBitmap);
				}
				else if (response == MessageBoxResult.Cancel)
				{
					this.HideOverlay();
					return;
				}
			}

			// Remove the previous Color to Wall controls and create new ones for this image's colors.
			if (this.DataContext.ColorToWallControls != null)
				foreach (var colorToWall in this.DataContext.ColorToWallControls)
					colorToWall.SelectedColorChanged -= this.ColorToWall_SelectedColorChanged;
			var newColorToWallControls = new List<ColorToWall>();
			foreach (var color in bitmapData.Colors)
				newColorToWallControls.Add(await ColorToWall.Create(color.Key, color.Value, this.ColorToWall_SelectedColorChanged));
			this.DataContext.ColorToWallControls = newColorToWallControls;

			// Set the image resolution in the status bar and create the actual bitmaps to display.
			this.DataContext.ImageWidth = imageWidth;
			this.DataContext.ImageHeight = imageHeight;
			this.DataContext.ShowImageResolution = true;
			this.DataContext.InGameBitmap?.Freeze();
			this.DataContext.InGameBitmap = new WriteableBitmap(PixelManip.Create(imageWidth * 16, imageHeight * 16, bitmapData.ZoomedPixels));
			this.DataContext.MapBitmap?.Freeze();
			this.DataContext.MapBitmap = new WriteableBitmap(PixelManip.Create(imageWidth * 16, imageHeight * 16, bitmapData.ZoomedPixels));
			bitmapData.ZoomedPixels = null;

			// Create the grid lines.
			var gridLines = new List<GridLineData>();
			for (int x = 0; x <= imageWidth; ++x)
				gridLines.Add(new GridLineData()
				{
					X1 = x * 16 + 0.5,
					X2 = x * 16 + 0.5,
					Y1 = 0.5,
					Y2 = imageHeight * 16 + 0.5
				});
			for (int y = 0; y <= imageHeight; ++y)
				gridLines.Add(new GridLineData()
				{
					X1 = 0.5,
					X2 = imageWidth * 16 + 0.5,
					Y1 = y * 16 + 0.5,
					Y2 = y * 16 + 0.5
				});

			this.DataContext.GridLines = gridLines;
			this.DataContext.CurrentBitmapData = bitmapData;

			this.UpdateNeededWallsAndColors();

			this.HideOverlay();
		}

		public void HideOverlay() => this.overlay.Visibility = Visibility.Hidden;

		void Image_MouseLeave(object sender, MouseEventArgs e) => this.DataContext.ShowPixelStatus = false;

		void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// If there is no image loaded, ignore clicks, shouldn't be possible as there won't be anything in the first place.
			var currentBitmapData = this.DataContext.CurrentBitmapData;
			if (currentBitmapData == null)
				return;
			// Get the position of the mouse and convert to pixel coordinates.
			var point = e.GetPosition(sender as IInputElement);
			int x = (int)(point.X / 16);
			int y = (int)(point.Y / 16);
			// Ignore the mouse if it is outside the image width (I don't think this will actually be able to happen, though).
			if (x == this.DataContext.ImageWidth || y == this.DataContext.ImageHeight)
				return;
			// Find the Color to Wall control matching the color of the pixel, make it flash and bring it into view.
			var color = currentBitmapData.PixelInfo[(x, y)].Color;
			int colorIndex = currentBitmapData.Colors.Keys.ToList().IndexOf(color);
			var colorToWall = this.DataContext.ColorToWallControls[colorIndex];
			colorToWall.DoFlash();
			colorToWall.BringIntoView();
		}

		void Image_MouseMove(object sender, MouseEventArgs e)
		{
			// If there is no image loaded, ignore clicks, shouldn't be possible as there won't be anything in the first place.
			var currentBitmapData = this.DataContext.CurrentBitmapData;
			if (currentBitmapData == null)
				return;
			// Get the position of the mouse and convert to pixel coordinates.
			var point = e.GetPosition(sender as IInputElement);
			int x = (int)(point.X / 16);
			int y = (int)(point.Y / 16);
			// Ignore the mouse if it is outside the image width (I don't think this will actually be able to happen, though).
			if (x == this.DataContext.ImageWidth || y == this.DataContext.ImageHeight)
				return;
			// Find the Color to Wall control matching the color of the pixel.
			var color = currentBitmapData.PixelInfo[(x, y)].Color;
			int colorIndex = currentBitmapData.Colors.Keys.ToList().IndexOf(color);
			// Set the status to show in the status bar.
			this.DataContext.ShowPixelStatus = true;
			this.DataContext.CurrentPixelX = x;
			this.DataContext.CurrentPixelY = y;
			this.DataContext.CurrentColorToWall = this.DataContext.ColorToWallControls[colorIndex];
		}

		void NoOverlay_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = this.overlay.Visibility != Visibility.Visible;

		void OpenFromClipboard_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = this.overlay.Visibility != Visibility.Visible && Clipboard.ContainsImage();

		async void OpenFromClipboard_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			var dataObject = Clipboard.GetDataObject();
			// Check each format in the clipboard data, only checking the ones that are memory streams.
			foreach (string format in dataObject.GetFormats())
				if (dataObject.GetData(format) is MemoryStream ms)
					try
					{
						// Attempt to load the image as a BitmapImage, will throw an exception if the stream isn't image data that can be read in.
						var bitmap = new BitmapImage();
						bitmap.BeginInit();
						bitmap.CacheOption = BitmapCacheOption.OnLoad;
						bitmap.StreamSource = ms;
						bitmap.EndInit();
						bitmap.Freeze();
						await this.HandleImage(bitmap);
						return;
					}
					catch (InvalidOperationException)
					{
					}
					catch (NotSupportedException)
					{
					}
			MessageBox.Show(this, "The clipboard did not contain a suitable image.", "Unable to Load", MessageBoxButton.OK, MessageBoxImage.Error);
			this.HideOverlay();
		}

		static (string descr, string exts)[] Filters = new[]
		{
			(descr: "BMP files", exts: new[] { "bmp", "dib" }),
			(descr: "JPEF files", exts: new[] { "jpg", "jpeg", "jpe", "jif", "jfif", "jfi" }),
			(descr: "PNG files", exts: new[] { "png" }),
			(descr: "TIFF files", exts: new[] { "tiff", "tif" }),
			(descr: "Windows Media Photo files", exts: new[] { "hdp", "wdp" }),
			(descr: "GIF files", exts: new[] { "gif" }),
			(descr: "ICO files", exts: new[] { "ico" })
		}.Select(x => (x.descr, exts: string.Join(";", x.exts.Select(y => $"*.{y}")))).ToArray();
		static string AllExts = string.Join(";", MainWindow.Filters.Select(x => x.exts));

		static OpenFileDialog OpenImageDialog = new OpenFileDialog()
		{
			Filter = $"Image files ({MainWindow.AllExts})|{MainWindow.AllExts}|{string.Join("|", MainWindow.Filters.Select(x => $"{x.descr} ({x.exts})|{x.exts}"))}|All files (*.*)|*.*",
			ReadOnlyChecked = true,
			Title = "Select image..."
		};

		async void OpenFromFile_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			void OnInvalidImage()
			{
				MessageBox.Show(this, "The file you selected could not be loaded as an image.", "Unable to Load", MessageBoxButton.OK, MessageBoxImage.Error);
				this.HideOverlay();
			}
			// Show the Open File dialog and attempt to load the file as a BitmapImage, will throw an exception if the file isn't image data that can be read in.
			if (MainWindow.OpenImageDialog.ShowDialog(this) ?? false)
				try
				{
					var bitmap = new BitmapImage();
					using (var stream = File.OpenRead(MainWindow.OpenImageDialog.FileName))
					{
						bitmap.BeginInit();
						bitmap.CacheOption = BitmapCacheOption.OnLoad;
						bitmap.StreamSource = stream;
						bitmap.EndInit();
					}
					bitmap.Freeze();
					await this.HandleImage(bitmap);
				}
				catch (InvalidOperationException)
				{
					OnInvalidImage();
				}
				catch (NotSupportedException)
				{
					OnInvalidImage();
				}
		}

		void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			// Synchronize both scroll viewers to each other.
			var other = sender == this.inGameScrollViewer ? this.mapScrollViewer : this.inGameScrollViewer;
			other.ScrollToVerticalOffset(e.VerticalOffset);
			other.ScrollToHorizontalOffset(e.HorizontalOffset);
		}

		/// <summary>
		/// Shows the overlay over the main window.
		/// </summary>
		/// <param name="showPleaseWait">Whether or not we should show the "Please Wait" message.</param>
		/// <param name="waitMessage">An optional wait message to show below "Please Wait".</param>
		public void ShowOverlay(bool showPleaseWait, string waitMessage = "")
		{
			this.overlay.Visibility = Visibility.Visible;
			this.overlay.DataContext.ShowPleaseWait = showPleaseWait;
			this.overlay.DataContext.WaitMessage = waitMessage;
		}

		void ToggleGrid_Executed(object sender, ExecutedRoutedEventArgs e) => this.DataContext.ShowGrid = !this.DataContext.ShowGrid;

		void UpdateNeededWallsAndColors()
		{
			// Ignore if there are no ColorToWall controls available yet.
			if (this.DataContext.ColorToWallControls == null)
				return;
			// Collect the number of pixels for each wall and each color.
			var wallPixels = new Dictionary<string, int>();
			var colorPixels = new Dictionary<string, int>();
			foreach (var colorToWall in this.DataContext.ColorToWallControls)
			{
				string wallName = colorToWall.DataContext.CurrentWallName;
				if (wallName != "No Wall")
				{
					int numberOfPixels = colorToWall.DataContext.NumberOfPixels;
					wallPixels.TryGetValue(wallName, out int wallPixelsCount);
					wallPixels[wallName] = wallPixelsCount + numberOfPixels;
					string colorName = colorToWall.DataContext.SelectedWallColor;
					if (!string.IsNullOrEmpty(colorName) && colorName != "Uncolored")
					{
						colorPixels.TryGetValue(colorName, out int colorPixelsCount);
						colorPixels[colorName] = colorPixelsCount + numberOfPixels;
					}
				}
			}
			// Set the text for the walls and colors needed.
			this.DataContext.WallsText = wallPixels.Count != 0 ? string.Join(Environment.NewLine, wallPixels.OrderBy(wp => wp.Key).Select(wp => $"{wp.Key}: {wp.Value}")) : "";
			this.DataContext.ColorsText =
				colorPixels.Count != 0 ? string.Join(Environment.NewLine, colorPixels.OrderBy(cp => ColorToWall.Colors.IndexOf(cp.Key)).Select(cp => $"{cp.Key}: {cp.Value}")) : "";
		}

		async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Initialize the cache for the Find Closest Color windows on startup, to make it so the first time we create one, it doesn't take forever then.
			this.ShowOverlay(true, "Initializing...");

			await FindClosestColor.InitializeCache();

			this.HideOverlay();
		}
	}

	/// <summary>
	/// The view model for <see cref="MainWindow" />.
	/// </summary>
	class MainWindowViewModel
	{
		/// <summary>
		/// The text for how many of each color will be needed.
		/// </summary>
		public string ColorsText { get; set; }

		/// <summary>
		/// The <see cref="ColorToWall" /> controls for each pixel color.
		/// </summary>
		public List<ColorToWall> ColorToWallControls { get; set; }

		/// <summary>
		/// The current bitmap's data.
		/// </summary>
		public BitmapData CurrentBitmapData { get; set; }

		/// <summary>
		/// The current <see cref="ColorToWall" /> control that is under the mouse cursor.
		/// </summary>
		public ColorToWall CurrentColorToWall { get; set; }

		/// <summary>
		/// The current X coordinate that is under the mouse cursor.
		/// </summary>
		public int CurrentPixelX { get; set; }

		/// <summary>
		/// The current Y coordinate that is under the mouse cursor.
		/// </summary>
		public int CurrentPixelY { get; set; }

		/// <summary>
		/// The list of grid lines to display.
		/// </summary>
		public List<GridLineData> GridLines { get; set; }

		/// <summary>
		/// The height of the current image.
		/// </summary>
		public int ImageHeight { get; set; }

		/// <summary>
		/// The width of the current image.
		/// </summary>
		public int ImageWidth { get; set; }

		/// <summary>
		/// The in-game bitmap of the image.
		/// </summary>
		public WriteableBitmap InGameBitmap { get; set; }

		/// <summary>
		/// The map bitmap of the image.
		/// </summary>
		public WriteableBitmap MapBitmap { get; set; }

		/// <summary>
		/// True to show the grid, false otherwise.
		/// </summary>
		public bool ShowGrid { get; set; } = true;

		/// <summary>
		/// True to show the image resolution on the status bar, false otherwise.
		/// </summary>
		public bool ShowImageResolution { get; set; }

		/// <summary>
		/// True to show the status of the pixel under the mouse cursor, false otherwise.
		/// </summary>
		public bool ShowPixelStatus { get; set; }

		/// <summary>
		/// The text for how many walls will be needed.
		/// </summary>
		public string WallsText { get; set; }
	}
}
