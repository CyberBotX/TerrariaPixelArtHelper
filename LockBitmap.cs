using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// A class to hold a lock on a bitmap to allow for easier manipulation of the pixels in the bitmap.
	/// </summary>
	public class LockBitmap
	{
		Bitmap source = null;
		BitmapData bitmapData = null;

		/// <summary>The byte array of all the pixels in the locked bitmap.</summary>
		byte[] Pixels { get; }

		/// <summary>The depth of the bitmap in bits per pixel.</summary>
		int Depth { get; }

		/// <summary>The stride width of the bitmap, which is how many pixels make up a single row.</summary>
		int Stride { get; set; } = -1;

		/// <summary>The width of the bitmap.</summary>
		public int Width { get; }

		/// <summary>The height of the bitmap.</summary>
		public int Height { get; }

		public LockBitmap(Bitmap source)
		{
			this.source = source;

			// Get source bitmap pixel format size
			this.Depth = Image.GetPixelFormatSize(this.source.PixelFormat);

			// Check if bpp (bits per pixel) is 8, 24, or 32
			if (this.Depth != 8 && this.Depth != 24 && this.Depth != 32)
				throw new ArgumentException("Only 8, 24, and 32 bpp images are supported.");

			// Get width and height of bitmap
			this.Width = this.source.Width;
			this.Height = this.source.Height;

			// Create byte array to copy pixel values
			this.Pixels = new byte[this.Width * this.Height * this.Depth / 8];
		}

		/// <summary>
		/// Locks the bitmap data so it can be accessed quickly.
		/// </summary>
		public void LockBits()
		{
			// Lock bitmap and return bitmap data
			this.bitmapData = this.source.LockBits(new Rectangle(0, 0, this.Width, this.Height), ImageLockMode.ReadWrite, this.source.PixelFormat);
			this.Stride = Math.Abs(this.bitmapData.Stride);

			// Copy data from pointer to array
			Marshal.Copy(this.bitmapData.Scan0, this.Pixels, 0, this.Pixels.Length);
		}

		/// <summary>
		/// Unlocks the bitmap data to write it back to the bitmap.
		/// </summary>
		public void UnlockBits()
		{
			// Copy data from byte array to pointer
			Marshal.Copy(this.Pixels, 0, this.bitmapData.Scan0, this.Pixels.Length);

			// Unlock bitmap data
			this.source.UnlockBits(this.bitmapData);
			this.Stride = -1;
		}

		/// <summary>
		/// Gets the color of the specified pixel.
		/// </summary>
		/// <param name="x">The X coordinate of the pixel to get.</param>
		/// <param name="y">The Y coordinate of the pixel to get.</param>
		/// <returns>The color at the specified pixel.</returns>
		public Color GetPixel(int x, int y)
		{
			// Get color components count
			int cCount = this.Depth / 8;

			// Get start index of the specified pixel
			int i = y * this.Stride + x * cCount;

			if (i > this.Pixels.Length - cCount)
				throw new IndexOutOfRangeException();

			if (this.Depth == 32)
				return Color.FromArgb(this.Pixels[i + 3], this.Pixels[i + 2], this.Pixels[i + 1], this.Pixels[i]);
			else if (this.Depth == 24)
				return Color.FromArgb(this.Pixels[i + 2], this.Pixels[i + 1], this.Pixels[i]);
			else
			{
				byte c = this.Pixels[i];
				return Color.FromArgb(c, c, c);
			}
		}

		/// <summary>
		/// Sets the color of the specified pixel.
		/// </summary>
		/// <param name="x">The X coordinate of the pixel to set.</param>
		/// <param name="y">The Y coordinate of the pixel to set.</param>
		/// <param name="color">The color to set at the specified pixel.</param>
		public void SetPixel(int x, int y, Color color)
		{
			// Get color components count
			int cCount = this.Depth / 8;

			// Get start index of the specified pixel
			int i = y * this.Stride + x * cCount;

			if (i > this.Pixels.Length - cCount)
				throw new IndexOutOfRangeException();

			if (this.Depth == 32 || this.Depth == 24)
			{
				this.Pixels[i] = color.B;
				this.Pixels[i + 1] = color.G;
				this.Pixels[i + 2] = color.R;
				if (this.Depth == 32)
					this.Pixels[i + 3] = color.A;
			}
			else
				this.Pixels[i] = color.B;
		}
	}
}
