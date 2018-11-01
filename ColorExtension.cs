using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Media;
using PostSharp.Patterns.Contracts;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Extensions to the <see cref="Color" /> struct for handling Terraria colors.
	/// </summary>
	internal static class ColorExtension
	{
		#region ColorizeInGame

		static ConcurrentDictionary<(Color originalColor, string color), Color> colorizeInGameCache = new ConcurrentDictionary<(Color originalColor, string color), Color>();

		/// <summary>
		/// Converts the current color into the in-game color given by the argument.
		/// </summary>
		/// <remarks>
		/// The conversions are based off of the Terraria tile shaders after much analysis of the shader's assembly code.
		/// </remarks>
		/// <param name="color">The color to convert the current color to.</param>
		/// <returns>The new in-game color after conversion.</returns>
		public static Color ColorizeInGame(this Color origColor, [Required] string color) => ColorExtension.colorizeInGameCache.GetOrAdd((origColor, color), key =>
		{
			float R = origColor.R / 255.0f;
			float G = origColor.G / 255.0f;
			float B = origColor.B / 255.0f;

			float[] colors = new[] { R, G, B };
			float max = colors.Max();
			float min = colors.Min();

			switch (color)
			{
				case "Red":
					R = max;
					G = B = min;
					break;
				case "Orange":
					R = max;
					G = (max + min) / 2f;
					B = min;
					break;
				case "Yellow":
					R = G = max;
					B = min;
					break;
				case "Lime":
					R = (max + min) / 2f;
					G = max;
					B = min;
					break;
				case "Green":
					R = B = min;
					G = max;
					break;
				case "Teal":
					R = min;
					G = max;
					B = (min + max) / 2f;
					break;
				case "Cyan":
					R = min;
					G = B = max;
					break;
				case "Sky Blue":
					R = min;
					G = (max + min) / 2f;
					B = max;
					break;
				case "Blue":
					R = G = min;
					B = max;
					break;
				case "Purple":
					R = (max + min) / 2f;
					G = min;
					B = max;
					break;
				case "Violet":
					R = B = max;
					G = min;
					break;
				case "Pink":
					R = max;
					G = min;
					B = (max + min) / 2f;
					break;
				case "Deep Red":
					R = max;
					G = B = min * 0.4f;
					break;
				case "Deep Orange":
					R = max;
					G = (max + min * 0.4f) / 2f;
					B = min * 0.4f;
					break;
				case "Deep Yellow":
					R = G = max;
					B = min * 0.4f;
					break;
				case "Deep Lime":
					R = (max + min * 0.4f) / 2f;
					G = max;
					B = min * 0.4f;
					break;
				case "Deep Green":
					R = B = min * 0.4f;
					G = max;
					break;
				case "Deep Teal":
					R = min * 0.4f;
					G = max;
					B = (max + min * 0.4f) / 2f;
					break;
				case "Deep Cyan":
					R = min * 0.4f;
					G = B = max;
					break;
				// NOTE: Need to add something here to make it NOT apply anything to blue and use the original value, as that is how Terraria currently does things...
				case "Deep Sky Blue":
					R = min * 0.4f;
					G = (max + min * 0.4f) / 2f;
					B = max;
					break;
				case "Deep Blue":
					R = G = min * 0.4f;
					B = max;
					break;
				case "Deep Purple":
					R = (max + min * 0.4f) / 2f;
					G = min * 0.4f;
					B = max;
					break;
				case "Deep Violet":
					R = B = max;
					G = min * 0.4f;
					break;
				case "Deep Pink":
					R = max;
					G = min * 0.4f;
					B = (max + min * 0.4f) / 2f;
					break;
				case "Black":
					R = G = B = (max + min) * 0.15f;
					break;
				case "Gray":
					R = G = B = (max + min) / 2f;
					break;
				case "White":
					R = G = B = (max * 0.7f + min * 0.3f) * (2f - (max + min) / 2f);
					break;
				case "Brown":
					R = max;
					G = max * 0.7f;
					B = max * 0.49f;
					break;
				case "Shadow":
					R = G = B = (max + min) * 0.025f;
					break;
				case "Negative":
					R = 0.75f - 2.0f * R;
					G = 0.75f - 2.0f * G;
					B = 0.75f - 2.0f * B;
					break;
			}

			return Color.FromArgb(origColor.A, (byte)(R.Clamp(0f, 1f) * 255f), (byte)(G.Clamp(0f, 1f) * 255f), (byte)(B.Clamp(0f, 1f) * 255f));
		});

		#endregion

		#region GetMapWallColor

		// Cache of map wall colors
		static ConcurrentDictionary<string, Color?> mapWallColorCache = new ConcurrentDictionary<string, Color?>();

		/// <summary>
		/// Gets the map color for the given wall.
		/// </summary>
		/// <remarks>
		/// Colors come from Terraria itself, specifically the 2-dimensional color array from Terraria.Map.MapHelper.Initialize() whose first dimension is the same size as the number of walls.
		/// </remarks>
		/// <param name="wallName">The name of the wall to get the map color of.</param>
		/// <returns>The map color for the given wall.</returns>
		public static Color? GetMapWallColor([Required] string wallName) => ColorExtension.mapWallColorCache.GetOrAdd(wallName, key =>
		{
			switch (wallName)
			{
				case "Stone Wall": // 1
				case "Gray Brick Wall": // 5
					return Color.FromRgb(52, 52, 52);
				case "Wood Wall": // 4
					return Color.FromRgb(73, 51, 36);
				case "Red Brick Wall": // 6
					return Color.FromRgb(91, 30, 30);
				case "Gold Brick Wall": // 10
					return Color.FromRgb(74, 62, 12);
				case "Silver Brick Wall": // 11
					return Color.FromRgb(46, 56, 59);
				case "Copper Brick Wall": // 12
					return Color.FromRgb(75, 32, 11);
				case "Dirt Wall": // 16
					return Color.FromRgb(88, 61, 46);
				case "Blue Brick Wall": // 17
					return Color.FromRgb(27, 31, 42);
				case "Green Brick Wall": // 18
					return Color.FromRgb(31, 39, 26);
				case "Pink Brick Wall": // 19
					return Color.FromRgb(41, 28, 36);
				case "Obsidian Brick Wall": // 20
					return Color.FromRgb(15, 15, 15);
				case "Pearlstone Brick Wall": // 22
					return Color.FromRgb(113, 99, 99);
				case "Iridescent Brick Wall": // 23
					return Color.FromRgb(38, 38, 43);
				case "Mudstone Brick Wall": // 24
					return Color.FromRgb(53, 39, 41);
				case "Cobalt Brick Wall": // 25
					return Color.FromRgb(11, 35, 62);
				case "Mythril Brick Wall": // 26
					return Color.FromRgb(21, 63, 70);
				case "Candy Cane Wall": // 29
					return Color.FromRgb(88, 23, 23);
				case "Green Candy Cane Wall": // 30
					return Color.FromRgb(28, 88, 23);
				case "Snow Brick Wall": // 31
					return Color.FromRgb(78, 87, 99);
				case "Adamantite Beam Wall": // 32
					return Color.FromRgb(86, 17, 40);
				case "Demonite Brick Wall": // 33
					return Color.FromRgb(49, 47, 83);
				case "Sandstone Brick Wall": // 34
				case "Yellow Stucco Wall": // 37
					return Color.FromRgb(69, 67, 41);
				case "Ebonstone Brick Wall": // 35
					return Color.FromRgb(51, 51, 70);
				case "Red Stucco Wall": // 36
					return Color.FromRgb(87, 59, 55);
				case "Green Stucco Wall": // 38
					return Color.FromRgb(49, 57, 49);
				case "Gray Stucco Wall": // 39
					return Color.FromRgb(78, 79, 73);
				case "Ebonwood Wall": // 41
					return Color.FromRgb(52, 50, 62);
				case "Rich Mahogany Wall": // 42
					return Color.FromRgb(71, 42, 44);
				case "Pearlwood Wall": // 43
					return Color.FromRgb(73, 66, 50);
				case "Tin Brick Wall": // 45
					return Color.FromRgb(60, 59, 51);
				case "Tungsten Brick Wall": // 46
					return Color.FromRgb(48, 57, 47);
				case "Platinum Brick Wall": // 47
					return Color.FromRgb(71, 77, 85);
				case "Grass Wall": // 66
				case "Flower Wall": // 68
					return Color.FromRgb(30, 80, 48);
				case "Jungle Wall": // 67
					return Color.FromRgb(53, 80, 30);
				case "Cactus Wall": // 72
					return Color.FromRgb(52, 84, 12);
				case "Cloud Wall": // 73
					return Color.FromRgb(190, 204, 223);
				case "Mushroom Wall": // 74
					return Color.FromRgb(64, 62, 80);
				case "Bone Block Wall": // 75
					return Color.FromRgb(65, 65, 35);
				case "Slime Block Wall": // 76
					return Color.FromRgb(20, 46, 104);
				case "Flesh Block Wall": // 77
					return Color.FromRgb(61, 13, 16);
				case "Living Wood Wall": // 78
					return Color.FromRgb(63, 39, 26);
				case "Disc Wall": // 82
					return Color.FromRgb(77, 64, 34);
				case "Ice Brick Wall": // 84
					return Color.FromRgb(48, 78, 93);
				case "Shadewood Wall": // 85
					return Color.FromRgb(54, 63, 69);
				case "Blue Slab Wall": // 100
					return Color.FromRgb(32, 40, 45);
				case "Blue Tiled Wall": // 101
					return Color.FromRgb(44, 41, 50);
				case "Pink Slab Wall": // 102
					return Color.FromRgb(72, 50, 77);
				case "Pink Tiled Wall": // 103
					return Color.FromRgb(78, 50, 69);
				case "Green Slab Wall": // 104
					return Color.FromRgb(36, 45, 44);
				case "Green Tiled Wall": // 105
					return Color.FromRgb(38, 49, 50);
				case "Hive Wall": // 108
					return Color.FromRgb(138, 73, 38);
				case "Palladium Column Wall": // 109
					return Color.FromRgb(94, 25, 17);
				case "Bubblegum Block Wall": // 110
					return Color.FromRgb(125, 36, 122);
				case "Titanstone Block Wall": // 111
					return Color.FromRgb(51, 35, 27);
				case "Lihzahrd Brick Wall": // 112
					return Color.FromRgb(50, 15, 8);
				case "Pumpkin Wall": // 113
					return Color.FromRgb(135, 58, 0);
				case "Hay Wall": // 114
					return Color.FromRgb(65, 52, 15);
				case "Spooky Wood Wall": // 115
					return Color.FromRgb(39, 42, 51);
				case "Christmas Tree Wallpaper": // 116
					return Color.FromRgb(89, 26, 27);
				case "Ornament Wallpaper": // 117
					return Color.FromRgb(126, 123, 115);
				case "Candy Cane Wallpaper": // 118
					return Color.FromRgb(8, 50, 19);
				case "Festive Wallpaper": // 119
					return Color.FromRgb(95, 21, 24);
				case "Stars Wallpaper": // 120
					return Color.FromRgb(17, 31, 65);
				case "Squiggles Wallpaper": // 121
					return Color.FromRgb(192, 173, 143);
				case "Snowflake Wallpaper": // 122
					return Color.FromRgb(114, 114, 131);
				case "Krampus Horn Wallpaper": // 123
					return Color.FromRgb(136, 119, 7);
				case "Bluegreen Wallpaper": // 124
					return Color.FromRgb(8, 72, 3);
				case "Grinch Finger Wallpaper": // 125
					return Color.FromRgb(117, 132, 82);
				case "Fancy Gray Wallpaper": // 126
					return Color.FromRgb(100, 102, 114);
				case "Ice Floe Wallpaper": // 127
					return Color.FromRgb(30, 118, 226);
				case "Music Wallpaper": // 128
					return Color.FromRgb(93, 6, 102);
				case "Purple Rain Wallpaper": // 129
					return Color.FromRgb(64, 40, 169);
				case "Rainbow Wallpaper": // 130
					return Color.FromRgb(39, 34, 180);
				case "Sparkle Stone Wallpaper": // 131
					return Color.FromRgb(87, 94, 125);
				case "Starlit Heaven Wallpaper": // 132
					return Color.FromRgb(6, 6, 6);
				case "Bubble Wallpaper": // 133
					return Color.FromRgb(69, 72, 186);
				case "Copper Pipe Wallpaper": // 134
					return Color.FromRgb(130, 62, 16);
				case "Ducky Wallpaper": // 135
					return Color.FromRgb(22, 123, 163);
				case "White Dynasty Wall": // 142
					return Color.FromRgb(17, 172, 143);
				case "Blue Dynasty Wall": // 143
					return Color.FromRgb(90, 112, 105);
				case "Copper Plating Wall": // 146
					return Color.FromRgb(120, 59, 19);
				case "Stone Slab Wall": // 147
					return Color.FromRgb(59, 59, 59);
				case "Sail": // 148
					return Color.FromRgb(229, 218, 161);
				case "Boreal Wood Wall": // 149
					return Color.FromRgb(73, 59, 50);
				case "Palm Wood Wall": // 151
					return Color.FromRgb(102, 75, 34);
				case "Amber Gemspark Wall": // 153
					return Color.FromRgb(255, 145, 79);
				case "Amethyst Gemspark Wall": // 154
					return Color.FromRgb(221, 79, 255);
				case "Diamond Gemspark Wall": // 155
					return Color.FromRgb(240, 240, 247);
				case "Emerald Gemspark Wall": // 156
					return Color.FromRgb(79, 255, 89);
				case "Offline Amber Gemspark Wall": // 157
					return Color.FromRgb(154, 83, 49);
				case "Offline Amethyst Gemspark Wall": // 158
					return Color.FromRgb(107, 49, 154);
				case "Offline Diamond Gemspark Wall": // 159
					return Color.FromRgb(85, 89, 118);
				case "Offline Emerald Gemspark Wall": // 160
					return Color.FromRgb(49, 154, 68);
				case "Offline Ruby Gemspark Wall": // 161
					return Color.FromRgb(154, 49, 77);
				case "Offline Sapphire Gemspark Wall": // 162
					return Color.FromRgb(49, 49, 154);
				case "Offline Topaz Gemspark Wall": // 163
					return Color.FromRgb(154, 148, 49);
				case "Ruby Gemspark Wall": // 164
					return Color.FromRgb(255, 79, 79);
				case "Sapphire Gemspark Wall": // 165
					return Color.FromRgb(79, 102, 255);
				case "Topaz Gemspark Wall": // 166
					return Color.FromRgb(250, 255, 79);
				case "Tin Plating Wall": // 167
					return Color.FromRgb(70, 68, 51);
				case "Chlorophyte Brick Wall": // 173
					return Color.FromRgb(94, 163, 46);
				case "Crimtane Brick Wall": // 174
					return Color.FromRgb(117, 32, 59);
				case "Shroomite Plating Wall": // 175
					return Color.FromRgb(20, 11, 203);
				case "Martian Conduit Wall": // 176
					return Color.FromRgb(74, 69, 88);
				case "Hellstone Brick Wall": // 177
					return Color.FromRgb(60, 30, 30);
				case "Smooth Marble Wall": // 179
				case "Marble Wall": // 183
					return Color.FromRgb(111, 117, 135);
				case "Smooth Granite Wall": // 181
				case "Granite Wall": // 184
					return Color.FromRgb(25, 23, 54);
				case "Meteorite Brick Wall": // 182
					return Color.FromRgb(74, 71, 129);
				case "Crystal Block Wall": // 186
					return Color.FromRgb(38, 9, 66);
				case "Luminite Brick Wall": // 224
					return Color.FromRgb(57, 55, 52);
				case "Silly Pink Balloon Wall": // 228
					return Color.FromRgb(160, 2, 75);
				case "Silly Purple Balloon Wall": // 229
					return Color.FromRgb(100, 55, 164);
				case "Silly Green Balloon Wall": // 230
					return Color.FromRgb(0, 117, 101);
				case "Glass Wall": // 21 (special case)
					return Color.FromArgb(0, 0, 0, 0);
				default:
					return null;
			}
		});

		#endregion

		#region ColorizeMap

		// Cache of map colorized colors
		static ConcurrentDictionary<(Color origColor, string colorName), Color> colorizeMapCache = new ConcurrentDictionary<(Color origColor, string colorName), Color>();

		/// <summary>
		/// Converts the current color into the map color given by the argument.
		/// </summary>
		/// <remarks>
		/// Conversions come from Terraria itself, specifically the Terraria.Map.MapHelper.MapColor() and Terraria.WorldGen.paintColor() methods.
		/// </remarks>
		/// <param name="colorName">The color to convert the current color to.</param>
		/// <returns>The new map color after conversion.</returns>
		public static Color ColorizeMap(this Color origColor, [Required] string colorName) => ColorExtension.colorizeMapCache.GetOrAdd((origColor, colorName), key =>
		{
			float R = origColor.R / 255.0f;
			float G = origColor.G / 255.0f;
			float B = origColor.B / 255.0f;
			if (G > R)
				Extensions.Swap(ref R, ref G);
			if (B > R)
				Extensions.Swap(ref R, ref B);

			if (colorName == "Shadow")
				return Color.FromArgb(origColor.A, (byte)(25 * B * 0.3f), (byte)(25 * B * 0.3f), (byte)(25 * B * 0.3f));
			else if (colorName == "Negative")
				return Color.FromArgb(origColor.A, (byte)((255f - origColor.R) / 2.0f), (byte)((255f - origColor.G) / 2.0f), (byte)((255f - origColor.B) / 2.0f));
			else
			{
				Color? color = null;
				switch (colorName)
				{
					case "Red":
					case "Deep Red":
						color = Color.FromRgb(255, 0, 0);
						break;
					case "Orange":
					case "Deep Orange":
						color = Color.FromRgb(255, 127, 0);
						break;
					case "Yellow":
					case "Deep Yellow":
						color = Color.FromRgb(255, 255, 0);
						break;
					case "Lime":
					case "Deep Lime":
						color = Color.FromRgb(127, 255, 0);
						break;
					case "Green":
					case "Deep Green":
						color = Color.FromRgb(0, 255, 0);
						break;
					case "Teal":
					case "Deep Teal":
						color = Color.FromRgb(0, 255, 127);
						break;
					case "Cyan":
					case "Deep Cyan":
						color = Color.FromRgb(0, 255, 255);
						break;
					case "Sky Blue":
					case "Deep Sky Blue":
						color = Color.FromRgb(0, 127, 255);
						break;
					case "Blue":
					case "Deep Blue":
						color = Color.FromRgb(0, 0, 255);
						break;
					case "Purple":
					case "Deep Purple":
						color = Color.FromRgb(127, 0, 255);
						break;
					case "Violet":
					case "Deep Violet":
						color = Color.FromRgb(255, 0, 255);
						break;
					case "Pink":
					case "Deep Pink":
						color = Color.FromRgb(255, 0, 127);
						break;
					case "Black":
						color = Color.FromRgb(75, 75, 75);
						break;
					case "Gray":
						color = Color.FromRgb(175, 175, 175);
						break;
					case "White":
						color = Color.FromRgb(255, 255, 255);
						break;
					case "Brown":
						color = Color.FromRgb(255, 178, 125);
						break;
				}
				return color.HasValue ? Color.FromArgb(origColor.A, (byte)(color.Value.R * R), (byte)(color.Value.G * R), (byte)(color.Value.B * R)) : origColor;
			}
		});

		#endregion
	}
}
