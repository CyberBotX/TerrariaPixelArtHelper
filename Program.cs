// Undefine the following to make the program only create the PNGs and update the project file, see below.
//#define BUILD_WALLS

using System;
#if BUILD_WALLS
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Build.Evaluation;
#endif

[assembly: PostSharp.Patterns.Model.WeakEvent]
[assembly: PostSharp.Patterns.Model.NotifyPropertyChanged(AttributeTargetTypes = "TerrariaPixelArtHelper.*ViewModel")]

/// <remarks>
/// See README.md for extra information regarding the license.
///
/// Terraria Pixel Art Helper is licensed as follows:
///
/// The MIT License (MIT)
///
/// Copyright(c) 2013-2018 Naram Qashat
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
#if BUILD_WALLS
			// This section, when enabled, will rebuild the PNG image files of all the acceptable walls.
			// It does not run the WPF application, and thus BUILD_WALLS should only be defined at the top of this file when the wall images need rebuilding.

			var walls = new Dictionary<int, string>()
			{
				[1] = "StoneWall",
				[4] = "WoodWall",
				[5] = "GrayBrickWall",
				[6] = "RedBrickWall",
				[10] = "GoldBrickWall",
				[11] = "SilverBrickWall",
				[12] = "CopperBrickWall",
				[16] = "DirtWall",
				[17] = "BlueBrickWall",
				[18] = "GreenBrickWall",
				[19] = "PinkBrickWall",
				[20] = "ObsidianBrickWall",
				[21] = "GlassWall",
				[22] = "PearlstoneBrickWall",
				[23] = "IridescentBrickWall",
				[24] = "MudstoneBrickWall",
				[25] = "CobaltBrickWall",
				[26] = "MythrilBrickWall",
				[29] = "CandyCaneWall",
				[30] = "GreenCandyCaneWall",
				[31] = "SnowBrickWall",
				[32] = "AdamantiteBeamWall",
				[33] = "DemoniteBrickWall",
				[34] = "SandstoneBrickWall",
				[35] = "EbonstoneBrickWall",
				[36] = "RedStuccoWall",
				[37] = "YellowStuccoWall",
				[38] = "GreenStuccoWall",
				[39] = "GrayStuccoWall",
				[41] = "EbonwoodWall",
				[42] = "RichMahoganyWall",
				[43] = "PearlwoodWall",
				[45] = "TinBrickWall",
				[46] = "TungstenBrickWall",
				[47] = "PlatinumBrickWall",
				[66] = "GrassWall",
				[67] = "JungleWall",
				[68] = "FlowerWall",
				[72] = "CactusWall",
				[73] = "CloudWall",
				[74] = "MushroomWall",
				[75] = "BoneBlockWall",
				[76] = "SlimeBlockWall",
				[77] = "FleshBlockWall",
				[78] = "LivingWoodWall",
				[82] = "DiscWall",
				[84] = "IceBrickWall",
				[85] = "ShadewoodWall",
				[100] = "BlueSlabWall",
				[101] = "BlueTiledWall",
				[102] = "PinkSlabWall",
				[103] = "PinkTiledWall",
				[104] = "GreenSlabWall",
				[105] = "GreenTiledWall",
				[108] = "HiveWall",
				[109] = "PalladiumColumnWall",
				[110] = "BubblegumBlockWall",
				[111] = "TitanstoneBlockWall",
				[112] = "LihzahrdBrickWall",
				[113] = "PumpkinWall",
				[114] = "HayWall",
				[115] = "SpookyWoodWall",
				[116] = "ChristmasTreeWallpaper",
				[117] = "OrnamentWallpaper",
				[118] = "CandyCaneWallpaper",
				[119] = "FestiveWallpaper",
				[120] = "StarsWallpaper",
				[121] = "SquigglesWallpaper",
				[122] = "SnowflakeWallpaper",
				[123] = "KrampusHornWallpaper",
				[124] = "BluegreenWallpaper",
				[125] = "GrinchFingerWallpaper",
				[126] = "FancyGrayWallpaper",
				[127] = "IceFloeWallpaper",
				[128] = "MusicWallpaper",
				[129] = "PurpleRainWallpaper",
				[130] = "RainbowWallpaper",
				[131] = "SparkleStoneWallpaper",
				[132] = "StarlitHeavenWallpaper",
				[133] = "BubbleWallpaper",
				[134] = "CopperPipeWallpaper",
				[135] = "DuckyWallpaper",
				[142] = "WhiteDynastyWall",
				[143] = "BlueDynastyWall",
				[146] = "CopperPlatingWall",
				[147] = "StoneSlabWall",
				[148] = "Sail",
				[149] = "BorealWoodWall",
				[151] = "PalmWoodWall",
				[153] = "AmberGemsparkWall",
				[154] = "AmethystGemsparkWall",
				[155] = "DiamondGemsparkWall",
				[156] = "EmeraldGemsparkWall",
				[157] = "OfflineAmberGemsparkWall",
				[158] = "OfflineAmethystGemsparkWall",
				[159] = "OfflineDiamondGemsparkWall",
				[160] = "OfflineEmeraldGemsparkWall",
				[161] = "OfflineRubyGemsparkWall",
				[162] = "OfflineSapphireGemsparkWall",
				[163] = "OfflineTopazGemsparkWall",
				[164] = "RubyGemsparkWall",
				[165] = "SapphireGemsparkWall",
				[166] = "TopazGemsparkWall",
				[167] = "TinPlatingWall",
				[173] = "ChlorophyteBrickWall",
				[174] = "CrimtaneBrickWall",
				[175] = "ShroomitePlatingWall",
				[176] = "MartianConduitWall",
				[177] = "HellstoneBrickWall",
				[179] = "SmoothMarbleWall",
				[181] = "SmoothGraniteWall",
				[182] = "MeteoriteBrickWall",
				[183] = "MarbleWall",
				[184] = "GraniteWall",
				[186] = "CrystalBlockWall",
				[224] = "LuminiteBrickWall",
				[228] = "SillyPinkBalloonWall",
				[229] = "SillyPurpleBalloonWall",
				[230] = "SillyGreenBalloonWall"
			};

			// Set the below to the path of where you have the PNGs of Terraria's walls, extracted from their XNBs.
			string terrariaImagesDirectory = Path.Combine("D:" + Path.DirectorySeparatorChar, "Users", "CyberBotX", "Downloads", "0_TerrariaAssets", "Images");

			string wallImagesDirectory = Path.Combine("..", "..", "..", "WallImages");
			if (Directory.Exists(wallImagesDirectory))
				foreach (var file in new DirectoryInfo(wallImagesDirectory).EnumerateFiles())
					file.Delete();
			else
				Directory.CreateDirectory(wallImagesDirectory);

			// Load the project, remove the old items for the WallImages, and create a new group to store the images.
			var project = new Project(Path.Combine("..", "..", "..", "TerrariaPixelArtHelperWPF.csproj"));
			foreach (var item in project.Xml.Items.Where(i => i.Include.StartsWith(@"WallImages\")))
				item.Parent.RemoveChild(item);
			foreach (var itemGroup in project.Xml.ItemGroups.Where(ig => ig.Count == 0))
				project.Xml.RemoveChild(itemGroup);
			var newItemGroup = project.Xml.AddItemGroup();

			// The following creates the stripped down versions of the walls.
			foreach (var kvp in walls)
			{
				var bitmap = new WriteableBitmap(new BitmapImage(new Uri(Path.Combine(terrariaImagesDirectory, $"Wall_{kvp.Key}.png"), UriKind.Absolute)));
				var newBitmap = new WriteableBitmap(App.WallPixelWidth, App.WallPixelHeight, 96, 96, PixelFormats.Bgra32, null);
				// The locations of these comes from Terraria.Framing for wall frame lookups 15-19
				// The position is the X and Y points multiplied by 36 and then 8 added to them
				// All the portions I an using are 16x16 in size
				// Point 3 of each lookup is not used
				newBitmap.Blit(new Rect(0, 0, 16, 16), bitmap, new Rect(44, 44, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 0, Point 0: 1, 1
				newBitmap.Blit(new Rect(16, 0, 16, 16), bitmap, new Rect(80, 44, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 0, Point 1: 2, 1
				newBitmap.Blit(new Rect(32, 0, 16, 16), bitmap, new Rect(116, 44, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 0, Point 2: 3, 1
				newBitmap.Blit(new Rect(0, 16, 16, 16), bitmap, new Rect(224, 44, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 1, Point 0: 6, 1
				newBitmap.Blit(new Rect(16, 16, 16, 16), bitmap, new Rect(260, 44, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 1, Point 1: 7, 1
				newBitmap.Blit(new Rect(32, 16, 16, 16), bitmap, new Rect(296, 44, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 1, Point 2: 8, 1
				newBitmap.Blit(new Rect(0, 32, 16, 16), bitmap, new Rect(224, 80, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 2, Point 0: 6, 2
				newBitmap.Blit(new Rect(16, 32, 16, 16), bitmap, new Rect(260, 80, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 2, Point 1: 7, 2
				newBitmap.Blit(new Rect(32, 32, 16, 16), bitmap, new Rect(296, 80, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 2, Point 2: 8, 2
				newBitmap.Blit(new Rect(0, 48, 16, 16), bitmap, new Rect(368, 8, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 3, Point 0: 10, 0
				newBitmap.Blit(new Rect(16, 48, 16, 16), bitmap, new Rect(368, 44, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 3, Point 1: 10, 1
				newBitmap.Blit(new Rect(32, 48, 16, 16), bitmap, new Rect(368, 80, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 3, Point 2: 10, 2
				newBitmap.Blit(new Rect(0, 64, 16, 16), bitmap, new Rect(404, 8, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 4, Point 0: 11, 0
				newBitmap.Blit(new Rect(16, 64, 16, 16), bitmap, new Rect(404, 44, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 4, Point 1: 11, 1
				newBitmap.Blit(new Rect(32, 64, 16, 16), bitmap, new Rect(404, 80, 16, 16), WriteableBitmapExtensions.BlendMode.None); // Lookup 4, Point 2: 11, 2

				using (var stream = File.Create(Path.Combine(wallImagesDirectory, $"Wall_{kvp.Value}.png")))
				{
					var encoder = new PngBitmapEncoder();
					encoder.Frames.Add(BitmapFrame.Create(newBitmap));
					encoder.Save(stream);
				}

				newItemGroup.AddItem("Resource", $@"WallImages\Wall_{kvp.Value}.png");
			}

			// Save over the project with the new references
			project.Save();
#else
			// This section, when enabled, will run the actual WPF application.

#pragma warning disable IDE0022 // Use expression body for methods
			App.Main();
#pragma warning restore IDE0022 // Use expression body for methods
#endif
		}
	}
}
