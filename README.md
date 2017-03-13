Primary repository on [GitHub](https://github.com/CyberBotX/TerrariaPixelArtHelper).

# Terraria Pixel Art Helper

Terraria Pixel Art Helper is a program designed to help determine what walls and colors in [Terraria](https://terraria.org/) can be used to represent a given image.

## Building

(You only need to worry about this if you want to build it yourself instead of using the pre-built executables.)

Terraria Pixel Art Helper uses C# 6 with the .NET Framework 4.6. You will need a minimum of Visual Studio 2015 to build the program.

Normally you can just build the program normally, as all the required files are included. However, if you decide to modify the program to include or exclude certain walls, you will need to build the program in two steps, as follows:

1. Within Program.cs, uncomment the first line so it says `#define BUILD_WALLS` and modify the path where you currently have the PNGs of the Terraria images that have been extracted from its XNB files. After that, build and run the program. It should almost immediately end and the WallImages folder will contain the required walls.
2. Within Program.cs, comment the first line so it says `//#define BUILD_WALLS`. After that, build and run the program and it will function as it should.

## Usage

This program does not actually interface with Terraria in any way. Rather, it is meant to show what you will need to use within Terraria to represent pixel art using the game's walls.

1. On loading the program, you must first load an image. This can be done from a file or from the clipboard. **NOTE:** It is recommended that you choose images with low color counts, as high color counts can slow the program down. If you try to load an image that contains more than 256 colors, you will be asked if you want to reduce the color count down to 256.
2. Once the image is loaded, you can choose the walls and colors to use for each pixel color. This can either be done manually by selecting the wall and color, or it can be done semi-automatically with the "Find Closest Color" option (see below for more information about this option). "Reset" will remove the current wall and color from the pixel. You can also click on a pixel in the actual image to determine which pixel color it corresponds to (the section for that pixel will be scrolled into view and will flash red).
3. After you have selected all the walls and colors, you will have a list of the number of walls and colors needed to represent the image in Terraria. The image's size will show up in the status bar, and when you hover a pixel in the actual image, you will see what wall and color is on that pixel in the status bar.

### Find Closest Color details

The "Find Closest Color" dialog box will show you what wall and color combinations match the original pixel color closest, with the closest matches at the top and the furthest matches on the bottom. The matches are based on the map color of the wall and color combinations.

From a technical standpoint, the original color as well as all combinations are converted from the sRGB color space to the CIE L\*a\*b\* color space (using the CIE XYZ color space as an intermediary), and then matched based on the Euclidean distance between colors, with lower values being considered more closely matching than higher values.

## Known Issues

* Walls with the Deep Sky Blue paint color show up differently in the program than they do in Terraria, but only in-game and not on the map. This is actually an issue with Terraria currently, and not Terraria Pixel Art Helper. I have sent Re-Logic a support ticket about it and they acknowledge that the issue is known but is low priority.
* The program will momentarily freeze when loading images that are high in color count or dimensions.
* Seems that sometimes walls will not be rendered as well as they should. I may need to take a closer look at Terraria's code to figure out what is wrong.

## Possible TODOs

(In no particular order)

* Save and load functionality
* Highlighting of blocks matching certain walls or colors
* Showing the currently selected color of the wall and color combination for each pixel color
* A throbber when loading images to prevent the UI from looking frozen
* Tabbed interface to allow for multiple images to be loaded together
* Some way to measure pixel distances
* Possibly some way to show the original image
* Some sort of exclusion system on walls and/or colors (so certain blocks won't be used when looking for closest color)
* A menu when right-clicking on pixels
* Possibly a tooltip instead of or in addition to the status bar for showing what the current pixel is

## Contact

If you have any questions, comments or concerns about Terraria Pixel Art Helper:

* Contact me by email: cyberbotx@cyberbotx.com (please include Terraria Pixel Art Helper in the subject line)
* Contact me on IRC: On the server jenna.cyberbotx.com (I will usually be under CyberBotX)
* Submit an issue via [GitHub's issue tracker](https://github.com/CyberBotX/TerrariaPixelArtHelper/issues)

## License

Certain portions of this program contain resources and code from Terraria itself. This includes:

* The wall images come from the game's .xnb files for the walls. They are modified to be embedded within the program by first getting them converted to PNG and then stripped down to just the portions needed by the program.
* Terraria's shader files were decompiled so I could determine how wall tiles are colorized within the game.
* Map colors for the wall tiles come from decompiling the Terraria executable.
* Colorizing the map colors of wall tiles also comes from decompiling the Terraria executable.
* The names and IDs of the walls also comes from decompiling the Terraria executable.
* The functionality to determine which wall tile to pick based on position and random number also comes from decompiling the Terraria executable.

I claim no ownership of the above items, they are copyright to [Re-Logic](https://re-logic.com/). (Any places where the above are used has been documented within the source code.)

Except where noted within the source code (mainly code that came from Stack Overflow and was not mine originally), the rest of the program is licensed as follows:

```
The MIT License (MIT)

Copyright (c) 2013-2017 Naram Qashat

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
