// Undefine the following to make the program only create the PNGs and resource file, see below.
//#define BUILD_WALLS

using System;
#if BUILD_WALLS
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Linq;
#else
using System.Windows.Forms;
#endif

/// <remarks>
/// See README.md for extra information regarding the license.
///
/// Terraria Pixel Art Helper is licensed as follows:
///
/// The MIT License (MIT)
///
/// Copyright(c) 2013-2017 Naram Qashat
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
			// This section, when enabled, will rebuild the PNG image files of all the acceptable walls, as well as update the resources file to contain all the walls as well as the eyedropper cursor.
			// It does not run the WinForms application, and thus BUILD_WALLS should only be defined at the top of this file when the wall images need rebuilding.

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

			foreach (var file in new DirectoryInfo(Path.Combine("..", "..", "..", "WallImages")).EnumerateFiles())
				file.Delete();

			// The following creates the stripped down versions of the walls
			foreach (var kvp in walls)
			{
				var path = Path.Combine(terrariaImagesDirectory, FormattableString.Invariant($"Wall_{kvp.Key}.png"));
				using (var bitmap = new Bitmap(path))
					using (var newBitmap = new Bitmap(48, 80))
					{
						using (var g = Graphics.FromImage(newBitmap))
						{
							// The locations of these comes from Terraria.Framing for wall frame lookups 15-19
							// The position is the X and Y points multiplied by 36 and then 8 added to them
							// All the portions I an using are 16x16 in size
							// Point 3 of each lookup is not used
							g.DrawImage(bitmap, 0, 0, new Rectangle(44, 44, 16, 16), GraphicsUnit.Pixel); // Lookup 0, Point 0: 1, 1
							g.DrawImage(bitmap, 16, 0, new Rectangle(80, 44, 16, 16), GraphicsUnit.Pixel); // Lookup 0, Point 1: 2, 1
							g.DrawImage(bitmap, 32, 0, new Rectangle(116, 44, 16, 16), GraphicsUnit.Pixel); // Lookup 0, Point 2: 3, 1
							g.DrawImage(bitmap, 0, 16, new Rectangle(224, 44, 16, 16), GraphicsUnit.Pixel); // Lookup 1, Point 0: 6, 1
							g.DrawImage(bitmap, 16, 16, new Rectangle(260, 44, 16, 16), GraphicsUnit.Pixel); // Lookup 1, Point 1: 7, 1
							g.DrawImage(bitmap, 32, 16, new Rectangle(296, 44, 16, 16), GraphicsUnit.Pixel); // Lookup 1, Point 2: 8, 1
							g.DrawImage(bitmap, 0, 32, new Rectangle(224, 80, 16, 16), GraphicsUnit.Pixel); // Lookup 2, Point 0: 6, 2
							g.DrawImage(bitmap, 16, 32, new Rectangle(260, 80, 16, 16), GraphicsUnit.Pixel); // Lookup 2, Point 1: 7, 2
							g.DrawImage(bitmap, 32, 32, new Rectangle(296, 80, 16, 16), GraphicsUnit.Pixel); // Lookup 2, Point 2: 8, 2
							g.DrawImage(bitmap, 0, 48, new Rectangle(368, 8, 16, 16), GraphicsUnit.Pixel); // Lookup 3, Point 0: 10, 0
							g.DrawImage(bitmap, 16, 48, new Rectangle(368, 44, 16, 16), GraphicsUnit.Pixel); // Lookup 3, Point 1: 10, 1
							g.DrawImage(bitmap, 32, 48, new Rectangle(368, 80, 16, 16), GraphicsUnit.Pixel); // Lookup 3, Point 2: 10, 2
							g.DrawImage(bitmap, 0, 64, new Rectangle(404, 8, 16, 16), GraphicsUnit.Pixel); // Lookup 4, Point 0: 11, 0
							g.DrawImage(bitmap, 16, 64, new Rectangle(404, 44, 16, 16), GraphicsUnit.Pixel); // Lookup 4, Point 1: 11, 1
							g.DrawImage(bitmap, 32, 64, new Rectangle(404, 80, 16, 16), GraphicsUnit.Pixel); // Lookup 4, Point 2: 11, 2
						}
						newBitmap.Save(Path.Combine("..", "..", "..", "WallImages", FormattableString.Invariant($"Wall_{kvp.Value}.png")), ImageFormat.Png);
					}
			}

			// The following recreates the resource file for the program
			Directory.SetCurrentDirectory(Path.Combine("..", "..", "..", "Properties"));

			using (var writer = new ResXResourceWriter("Resources.resx"))
			{
				writer.AddResource(new ResXDataNode("Eyedropper", new ResXFileRef(Path.Combine("..", "Eyedropper.cur"), typeof(byte[]).AssemblyQualifiedName)));
				var BitmapAssemblyQualifiedName = typeof(Bitmap).AssemblyQualifiedName;
				foreach (var kvp in walls.OrderBy(w => w.Value))
					writer.AddResource(new ResXDataNode(FormattableString.Invariant($"Wall_{kvp.Value}"),
						new ResXFileRef(Path.Combine("..", "WallImages", FormattableString.Invariant($"Wall_{kvp.Value}.png")), BitmapAssemblyQualifiedName)));

				writer.Generate();
			}
#else
			// This section, when enabled, will run the actual WinForms application.

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.AddMessageFilter(new MouseWheelRedirector());
			Application.Run(new MainForm());
#endif
		}
	}
}
