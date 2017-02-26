using System;
using System.Drawing;
using System.Windows.Forms;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Redirects mouse wheel events to the control under the cursor.
	/// </summary>
	/// <remarks>
	/// Comes from http://stackoverflow.com/a/4769961
	/// </remarks>
	public class MouseWheelRedirector : IMessageFilter
	{
		const int WM_MOUSEWHEEL = 0x020A;

		public bool PreFilterMessage(ref Message m)
		{
			if (m.Msg == WM_MOUSEWHEEL)
			{
				var hWnd = NativeMethods.WindowFromPoint(new Point(m.LParam.ToInt32() & 0xFFFF, m.LParam.ToInt32() >> 16));
				if (hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) != null)
				{
					NativeMethods.SendMessage(hWnd, (uint)m.Msg, m.WParam, m.LParam);
					return true;
				}
			}
			return false;
		}
	}
}
