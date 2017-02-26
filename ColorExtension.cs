using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Extensions to the <see cref="Color" /> struct for handling Terraria colors.
	/// </summary>
	public static class ColorExtension
	{
		// Cache of in-game colorized colors
		static Dictionary<Tuple<Color, string>, Color> cachedColorizeInGame = new Dictionary<Tuple<Color, string>, Color>();

		/// <summary>
		/// Converts the current color into the in-game color given by the argument.
		/// </summary>
		/// <remarks>
		/// The conversions are based off of the Terraria tile shaders after much analysis of the shader's assembly code.
		/// </remarks>
		/// <param name="color">The color to convert the current color to.</param>
		/// <returns>The new in-game color after conversion.</returns>
		public static Color ColorizeInGame(this Color origColor, string color)
		{
			var tuple = Tuple.Create(origColor, color);
			Color ret;
			if (!ColorExtension.cachedColorizeInGame.TryGetValue(tuple, out ret))
			{
				float R = origColor.R / 255.0f;
				float G = origColor.G / 255.0f;
				float B = origColor.B / 255.0f;

				var colors = new[] { R, G, B };
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

				ColorExtension.cachedColorizeInGame[tuple] = ret = Color.FromArgb(origColor.A, (int)(R.Clamp(0f, 1f) * 255f), (int)(G.Clamp(0f, 1f) * 255f), (int)(B.Clamp(0f, 1f) * 255f));
			}
			return ret;
		}

		// Cache of map wall colors
		static Dictionary<string, Color?> cachedMapWallColor = new Dictionary<string, Color?>();

		/// <summary>
		/// Gets the map color for the given wall.
		/// </summary>
		/// <remarks>
		/// Colors come from Terraria itself, specifically the 2-dimensional color array from Terraria.Map.MapHelper.Initialize() whose first dimension is the same size as the number of walls.
		/// </remarks>
		/// <param name="wallName">The name of the wall to get the map color of.</param>
		/// <returns>The map color for the given wall.</returns>
		public static Color? GetMapWallColor(string wallName)
		{
			Color? ret = null;
			if (!ColorExtension.cachedMapWallColor.TryGetValue(wallName, out ret))
			{
				switch (wallName)
				{
					case "Stone Wall": // 1
					case "Gray Brick Wall": // 5
						ret = Color.FromArgb(52, 52, 52);
						break;
					case "Wood Wall": // 4
						ret = Color.FromArgb(73, 51, 36);
						break;
					case "Red Brick Wall": // 6
						ret = Color.FromArgb(91, 30, 30);
						break;
					case "Gold Brick Wall": // 10
						ret = Color.FromArgb(74, 62, 12);
						break;
					case "Silver Brick Wall": // 11
						ret = Color.FromArgb(46, 56, 59);
						break;
					case "Copper Brick Wall": // 12
						ret = Color.FromArgb(75, 32, 11);
						break;
					case "Dirt Wall": // 16
						ret = Color.FromArgb(88, 61, 46);
						break;
					case "Blue Brick Wall": // 17
						ret = Color.FromArgb(27, 31, 42);
						break;
					case "Green Brick Wall": // 18
						ret = Color.FromArgb(31, 39, 26);
						break;
					case "Pink Brick Wall": // 19
						ret = Color.FromArgb(41, 28, 36);
						break;
					case "Obsidian Brick Wall": // 20
						ret = Color.FromArgb(15, 15, 15);
						break;
					case "Pearlstone Brick Wall": // 22
						ret = Color.FromArgb(113, 99, 99);
						break;
					case "Iridescent Brick Wall": // 23
						ret = Color.FromArgb(38, 38, 43);
						break;
					case "Mudstone Brick Wall": // 24
						ret = Color.FromArgb(53, 39, 41);
						break;
					case "Cobalt Brick Wall": // 25
						ret = Color.FromArgb(11, 35, 62);
						break;
					case "Mythril Brick Wall": // 26
						ret = Color.FromArgb(21, 63, 70);
						break;
					case "Candy Cane Wall": // 29
						ret = Color.FromArgb(88, 23, 23);
						break;
					case "Green Candy Cane Wall": // 30
						ret = Color.FromArgb(28, 88, 23);
						break;
					case "Snow Brick Wall": // 31
						ret = Color.FromArgb(78, 87, 99);
						break;
					case "Adamantite Beam Wall": // 32
						ret = Color.FromArgb(86, 17, 40);
						break;
					case "Demonite Brick Wall": // 33
						ret = Color.FromArgb(49, 47, 83);
						break;
					case "Sandstone Brick Wall": // 34
					case "Yellow Stucco Wall": // 37
						ret = Color.FromArgb(69, 67, 41);
						break;
					case "Ebonstone Brick Wall": // 35
						ret = Color.FromArgb(51, 51, 70);
						break;
					case "Red Stucco Wall": // 36
						ret = Color.FromArgb(87, 59, 55);
						break;
					case "Green Stucco Wall": // 38
						ret = Color.FromArgb(49, 57, 49);
						break;
					case "Gray Stucco Wall": // 39
						ret = Color.FromArgb(78, 79, 73);
						break;
					case "Ebonwood Wall": // 41
						ret = Color.FromArgb(52, 50, 62);
						break;
					case "Rich Mahogany Wall": // 42
						ret = Color.FromArgb(71, 42, 44);
						break;
					case "Pearlwood Wall": // 43
						ret = Color.FromArgb(73, 66, 50);
						break;
					case "Tin Brick Wall": // 45
						ret = Color.FromArgb(60, 59, 51);
						break;
					case "Tungsten Brick Wall": // 46
						ret = Color.FromArgb(48, 57, 47);
						break;
					case "Platinum Brick Wall": // 47
						ret = Color.FromArgb(71, 77, 85);
						break;
					case "Grass Wall": // 66
					case "Flower Wall": // 68
						ret = Color.FromArgb(30, 80, 48);
						break;
					case "Jungle Wall": // 67
						ret = Color.FromArgb(53, 80, 30);
						break;
					case "Cactus Wall": // 72
						ret = Color.FromArgb(52, 84, 12);
						break;
					case "Cloud Wall": // 73
						ret = Color.FromArgb(190, 204, 223);
						break;
					case "Mushroom Wall": // 74
						ret = Color.FromArgb(64, 62, 80);
						break;
					case "Bone Block Wall": // 75
						ret = Color.FromArgb(65, 65, 35);
						break;
					case "Slime Block Wall": // 76
						ret = Color.FromArgb(20, 46, 104);
						break;
					case "Flesh Block Wall": // 77
						ret = Color.FromArgb(61, 13, 16);
						break;
					case "Living Wood Wall": // 78
						ret = Color.FromArgb(63, 39, 26);
						break;
					case "Disc Wall": // 82
						ret = Color.FromArgb(77, 64, 34);
						break;
					case "Ice Brick Wall": // 84
						ret = Color.FromArgb(48, 78, 93);
						break;
					case "Shadewood Wall": // 85
						ret = Color.FromArgb(54, 63, 69);
						break;
					case "Blue Slab Wall": // 100
						ret = Color.FromArgb(32, 40, 45);
						break;
					case "Blue Tiled Wall": // 101
						ret = Color.FromArgb(44, 41, 50);
						break;
					case "Pink Slab Wall": // 102
						ret = Color.FromArgb(72, 50, 77);
						break;
					case "Pink Tiled Wall": // 103
						ret = Color.FromArgb(78, 50, 69);
						break;
					case "Green Slab Wall": // 104
						ret = Color.FromArgb(36, 45, 44);
						break;
					case "Green Tiled Wall": // 105
						ret = Color.FromArgb(38, 49, 50);
						break;
					case "Hive Wall": // 108
						ret = Color.FromArgb(138, 73, 38);
						break;
					case "Palladium Column Wall": // 109
						ret = Color.FromArgb(94, 25, 17);
						break;
					case "Bubblegum Block Wall": // 110
						ret = Color.FromArgb(125, 36, 122);
						break;
					case "Titanstone Block Wall": // 111
						ret = Color.FromArgb(51, 35, 27);
						break;
					case "Lihzahrd Brick Wall": // 112
						ret = Color.FromArgb(50, 15, 8);
						break;
					case "Pumpkin Wall": // 113
						ret = Color.FromArgb(135, 58, 0);
						break;
					case "Hay Wall": // 114
						ret = Color.FromArgb(65, 52, 15);
						break;
					case "Spooky Wood Wall": // 115
						ret = Color.FromArgb(39, 42, 51);
						break;
					case "Christmas Tree Wallpaper": // 116
						ret = Color.FromArgb(89, 26, 27);
						break;
					case "Ornament Wallpaper": // 117
						ret = Color.FromArgb(126, 123, 115);
						break;
					case "Candy Cane Wallpaper": // 118
						ret = Color.FromArgb(8, 50, 19);
						break;
					case "Festive Wallpaper": // 119
						ret = Color.FromArgb(95, 21, 24);
						break;
					case "Stars Wallpaper": // 120
						ret = Color.FromArgb(17, 31, 65);
						break;
					case "Squiggles Wallpaper": // 121
						ret = Color.FromArgb(192, 173, 143);
						break;
					case "Snowflake Wallpaper": // 122
						ret = Color.FromArgb(114, 114, 131);
						break;
					case "Krampus Horn Wallpaper": // 123
						ret = Color.FromArgb(136, 119, 7);
						break;
					case "Bluegreen Wallpaper": // 124
						ret = Color.FromArgb(8, 72, 3);
						break;
					case "Grinch Finger Wallpaper": // 125
						ret = Color.FromArgb(117, 132, 82);
						break;
					case "Fancy Gray Wallpaper": // 126
						ret = Color.FromArgb(100, 102, 114);
						break;
					case "Ice Floe Wallpaper": // 127
						ret = Color.FromArgb(30, 118, 226);
						break;
					case "Music Wallpaper": // 128
						ret = Color.FromArgb(93, 6, 102);
						break;
					case "Purple Rain Wallpaper": // 129
						ret = Color.FromArgb(64, 40, 169);
						break;
					case "Rainbow Wallpaper": // 130
						ret = Color.FromArgb(39, 34, 180);
						break;
					case "Sparkle Stone Wallpaper": // 131
						ret = Color.FromArgb(87, 94, 125);
						break;
					case "Starlit Heaven Wallpaper": // 132
						ret = Color.FromArgb(6, 6, 6);
						break;
					case "Bubble Wallpaper": // 133
						ret = Color.FromArgb(69, 72, 186);
						break;
					case "Copper Pipe Wallpaper": // 134
						ret = Color.FromArgb(130, 62, 16);
						break;
					case "Ducky Wallpaper": // 135
						ret = Color.FromArgb(22, 123, 163);
						break;
					case "White Dynasty Wall": // 142
						ret = Color.FromArgb(17, 172, 143);
						break;
					case "Blue Dynasty Wall": // 143
						ret = Color.FromArgb(90, 112, 105);
						break;
					case "Copper Plating Wall": // 146
						ret = Color.FromArgb(120, 59, 19);
						break;
					case "Stone Slab Wall": // 147
						ret = Color.FromArgb(59, 59, 59);
						break;
					case "Sail": // 148
						ret = Color.FromArgb(229, 218, 161);
						break;
					case "Boreal Wood Wall": // 149
						ret = Color.FromArgb(73, 59, 50);
						break;
					case "Palm Wood Wall": // 151
						ret = Color.FromArgb(102, 75, 34);
						break;
					case "Amber Gemspark Wall": // 153
						ret = Color.FromArgb(255, 145, 79);
						break;
					case "Amethyst Gemspark Wall": // 154
						ret = Color.FromArgb(221, 79, 255);
						break;
					case "Diamond Gemspark Wall": // 155
						ret = Color.FromArgb(240, 240, 247);
						break;
					case "Emerald Gemspark Wall": // 156
						ret = Color.FromArgb(79, 255, 89);
						break;
					case "Offline Amber Gemspark Wall": // 157
						ret = Color.FromArgb(154, 83, 49);
						break;
					case "Offline Amethyst Gemspark Wall": // 158
						ret = Color.FromArgb(107, 49, 154);
						break;
					case "Offline Diamond Gemspark Wall": // 159
						ret = Color.FromArgb(85, 89, 118);
						break;
					case "Offline Emerald Gemspark Wall": // 160
						ret = Color.FromArgb(49, 154, 68);
						break;
					case "Offline Ruby Gemspark Wall": // 161
						ret = Color.FromArgb(154, 49, 77);
						break;
					case "Offline Sapphire Gemspark Wall": // 162
						ret = Color.FromArgb(49, 49, 154);
						break;
					case "Offline Topaz Gemspark Wall": // 163
						ret = Color.FromArgb(154, 148, 49);
						break;
					case "Ruby Gemspark Wall": // 164
						ret = Color.FromArgb(255, 79, 79);
						break;
					case "Sapphire Gemspark Wall": // 165
						ret = Color.FromArgb(79, 102, 255);
						break;
					case "Topaz Gemspark Wall": // 166
						ret = Color.FromArgb(250, 255, 79);
						break;
					case "Tin Plating Wall": // 167
						ret = Color.FromArgb(70, 68, 51);
						break;
					case "Chlorophyte Brick Wall": // 173
						ret = Color.FromArgb(94, 163, 46);
						break;
					case "Crimtane Brick Wall": // 174
						ret = Color.FromArgb(117, 32, 59);
						break;
					case "Shroomite Plating Wall": // 175
						ret = Color.FromArgb(20, 11, 203);
						break;
					case "Martian Conduit Wall": // 176
						ret = Color.FromArgb(74, 69, 88);
						break;
					case "Hellstone Brick Wall": // 177
						ret = Color.FromArgb(60, 30, 30);
						break;
					case "Smooth Marble Wall": // 179
					case "Marble Wall": // 183
						ret = Color.FromArgb(111, 117, 135);
						break;
					case "Smooth Granite Wall": // 181
					case "Granite Wall": // 184
						ret = Color.FromArgb(25, 23, 54);
						break;
					case "Meteorite Brick Wall": // 182
						ret = Color.FromArgb(74, 71, 129);
						break;
					case "Crystal Block Wall": // 186
						ret = Color.FromArgb(38, 9, 66);
						break;
					case "Luminite Brick Wall": // 224
						ret = Color.FromArgb(57, 55, 52);
						break;
					case "Silly Pink Balloon Wall": // 228
						ret = Color.FromArgb(160, 2, 75);
						break;
					case "Silly Purple Balloon Wall": // 229
						ret = Color.FromArgb(100, 55, 164);
						break;
					case "Silly Green Balloon Wall": // 230
						ret = Color.FromArgb(0, 117, 101);
						break;
					case "Glass Wall": // 21 (special case)
						ret = Color.FromArgb(0, 0, 0, 0);
						break;
					default:
						ret = null;
						break;
				}
				ColorExtension.cachedMapWallColor[wallName] = ret;
			}
			return ret;
		}

		// Cache of map colorized colors
		static Dictionary<Tuple<Color, string>, Color> cachedColorizeMap = new Dictionary<Tuple<Color, string>, Color>();

		/// <summary>
		/// Converts the current color into the map color given by the argument.
		/// </summary>
		/// <remarks>
		/// Conversions come from Terraria itself, specifically the Terraria.Map.MapHelper.MapColor() and Terraria.WorldGen.paintColor() methods.
		/// </remarks>
		/// <param name="colorName">The color to convert the current color to.</param>
		/// <returns>The new map color after conversion.</returns>
		public static Color ColorizeMap(this Color origColor, string colorName)
		{
			var tuple = Tuple.Create(origColor, colorName);
			Color ret;
			if (!ColorExtension.cachedColorizeMap.TryGetValue(tuple, out ret))
			{
				float R = origColor.R / 255.0f;
				float G = origColor.G / 255.0f;
				float B = origColor.B / 255.0f;
				if (G > R)
					Extensions.Swap(ref R, ref G);
				if (B > R)
					Extensions.Swap(ref R, ref B);

				if (colorName == "Shadow")
					ret = Color.FromArgb(origColor.A, (int)(25 * B * 0.3f), (int)(25 * B * 0.3f), (int)(25 * B * 0.3f));
				else if (colorName == "Negative")
					ret = Color.FromArgb(origColor.A, (int)((255f - origColor.R) / 2.0f), (int)((255f - origColor.G) / 2.0f), (int)((255f - origColor.B) / 2.0f));
				else
				{
					Color? color = null;
					switch (colorName)
					{
						case "Red":
						case "Deep Red":
							color = Color.FromArgb(255, 0, 0);
							break;
						case "Orange":
						case "Deep Orange":
							color = Color.FromArgb(255, 127, 0);
							break;
						case "Yellow":
						case "Deep Yellow":
							color = Color.FromArgb(255, 255, 0);
							break;
						case "Lime":
						case "Deep Lime":
							color = Color.FromArgb(127, 255, 0);
							break;
						case "Green":
						case "Deep Green":
							color = Color.FromArgb(0, 255, 0);
							break;
						case "Teal":
						case "Deep Teal":
							color = Color.FromArgb(0, 255, 127);
							break;
						case "Cyan":
						case "Deep Cyan":
							color = Color.FromArgb(0, 255, 255);
							break;
						case "Sky Blue":
						case "Deep Sky Blue":
							color = Color.FromArgb(0, 127, 255);
							break;
						case "Blue":
						case "Deep Blue":
							color = Color.FromArgb(0, 0, 255);
							break;
						case "Purple":
						case "Deep Purple":
							color = Color.FromArgb(127, 0, 255);
							break;
						case "Violet":
						case "Deep Violet":
							color = Color.FromArgb(255, 0, 255);
							break;
						case "Pink":
						case "Deep Pink":
							color = Color.FromArgb(255, 0, 127);
							break;
						case "Black":
							color = Color.FromArgb(75, 75, 75);
							break;
						case "Gray":
							color = Color.FromArgb(175, 175, 175);
							break;
						case "White":
							color = Color.FromArgb(255, 255, 255);
							break;
						case "Brown":
							color = Color.FromArgb(255, 178, 125);
							break;
					}
					if (color.HasValue)
						ret = Color.FromArgb(origColor.A, (int)(color.Value.R * R), (int)(color.Value.G * R), (int)(color.Value.B * R));
					else
						ret = origColor;
				}
				ColorExtension.cachedColorizeMap[tuple] = ret;
			}
			return ret;
		}
	}
}
