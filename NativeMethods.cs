using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Methods that come from outside the .NET Framework using Platform Invoke (P/Invoke).
	/// </summary>
	internal static class NativeMethods
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		[SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "0",
			Justification = "Windows API POINT is defined using LONG, which is a 32-bit integer. .NET System.Drawing.Point is defined using Int32. There is no portablity issue here.")]
		public static extern IntPtr WindowFromPoint(Point Point);
	}
}
