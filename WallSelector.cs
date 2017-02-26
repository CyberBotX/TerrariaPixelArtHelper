using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// A custom ComboBox that draws a small 16x16 image of the selected wall next to the wall's name.
	/// </summary>
	public class WallSelector : ComboBox
	{
		public WallSelector() : base()
		{
			base.DrawMode = DrawMode.OwnerDrawVariable;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			e.DrawBackground();
			e.DrawFocusRectangle();

			if (e.Index >= 0 && e.Index < this.Items.Count)
			{
				var item = this.Items[e.Index] as WallItem;
				if (item.Image != null)
					e.Graphics.DrawImage(item.Image, e.Bounds.Left, e.Bounds.Top + 1);
				using (var brush = new SolidBrush(e.ForeColor))
					e.Graphics.DrawString(item.Name, e.Font, brush, e.Bounds.Left + (item.Image?.Width ?? 16), e.Bounds.Top + 1);
			}

			base.OnDrawItem(e);
		}

		protected override void OnMeasureItem(MeasureItemEventArgs e) => e.ItemHeight = 18;

		int currentIndex = -1;

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (this.SelectedIndex != this.currentIndex)
			{
				this.currentIndex = this.SelectedIndex;
				this.RefreshItem(this.SelectedIndex);
				if (!this.IsHandleCreated)
					base.OnSelectedIndexChanged(e);
			}
			else
				base.OnSelectedIndexChanged(e);
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This is overriding an existing property in the parent type and cannot be static.")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value",
			Justification = "This is overriding an existing property in the parent type and I don't want it set through here.")]
		public new DrawMode DrawMode
		{
			get { return DrawMode.OwnerDrawVariable; }
			set { }
		}
	}

	/// <summary>
	/// The item type to store within a <see cref="WallSelector" /> combo box.
	/// </summary>
	[Serializable]
	public class WallItem : IEquatable<WallItem>
	{
		string name = null;
		Image image = null;

		/// <summary>
		/// Gets or sets this wall's name.
		/// </summary>
		public string Name
		{
			get { return this.name; }
			set
			{
				this.name = value;
				if (string.IsNullOrWhiteSpace(value))
					this.Image = null;
				else
				{
					var wallImage = MainForm.GetWall(value);
					this.Image = new Bitmap(16, 16);
					if (wallImage != null)
						using (var g = Graphics.FromImage(this.Image))
							g.DrawImage(wallImage, 0, 0, new Rectangle(0, 0, 16, 16), GraphicsUnit.Pixel);
				}
			}
		}

		/// <summary>
		/// Gets or sets this wall's image, if any.
		/// </summary>
		public Image Image
		{
			get { return this.image; }
			private set
			{
				this.image?.Dispose();
				this.image = value;
			}
		}

		public WallItem() : this(null)
		{
		}

		public WallItem(string wallName)
		{
			this.Name = wallName;
		}

		public static implicit operator WallItem(string wallName) => new WallItem(wallName);

		public override string ToString() => string.IsNullOrWhiteSpace(this.Name) ? "WallItem" : this.Name;

		/// <summary>
		/// Determine if the given <see cref="WallItem" /> matches this one. They match if their names are the same.
		/// </summary>
		/// <param name="other">The other <see cref="WallItem" /> to match against.</param>
		/// <returns>true if both match, false otherwise.</returns>
		public bool Equals(WallItem other) => this.Name == other.Name;
	}
}
