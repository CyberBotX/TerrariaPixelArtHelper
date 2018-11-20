using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Interaction logic for About.xaml
	/// </summary>
	public partial class About : Window
	{
		public static List<LibraryData> Libraries = new List<LibraryData>()
		{
			new LibraryData()
			{
				Library = "CalcBinding",
				Version = typeof(CalcBinding.Binding).Assembly.GetName().Version,
				Author = "Alexander Zinchenko",
				Url = new Uri("https://github.com/Alex141/CalcBinding")
			},
			new LibraryData()
			{
				Library = "FontAwesome5",
				Version = typeof(FontAwesome5.WPF.FontAwesome).Assembly.GetName().Version,
				Author = "Codinion",
				Url = new Uri("https://github.com/MartinTopfstedt/FontAwesome5")
			},
			new LibraryData()
			{
				Library = "JeremyAnsel.ColorQuant",
				Version = typeof(JeremyAnsel.ColorQuant.WuColorQuantizer).Assembly.GetName().Version,
				Author = "Jérémy Ansel",
				Url = new Uri("https://github.com/JeremyAnsel/JeremyAnsel.ColorQuant")
			},
			new LibraryData()
			{
				Library = "Math.NET Numerics",
				Version = typeof(MathNet.Numerics.Constants).Assembly.GetName().Version,
				Author = "Christoph Ruegg, Marcus Cuda, Jurgen Van Gael",
				Url = new Uri("https://numerics.mathdotnet.com/")
			},
			new LibraryData()
			{
				Library = "PostSharp",
				Version = typeof(PostSharp.Post).Assembly.GetName().Version,
				Author = "PostSharp Technologies",
				Url = new Uri("https://www.postsharp.net/")
			},
			new LibraryData()
			{
				Library = "WriteableBitmapEx",
				Version = typeof(System.Windows.Media.Imaging.BitmapFactory).Assembly.GetName().Version,
				Author = "Schulte Software Development",
				Url = new Uri("https://github.com/teichgraf/WriteableBitmapEx")
			}
		};

		public static Version Version = typeof(About).Assembly.GetName().Version;

		public About() => this.InitializeComponent();

		void OK_Click(object sender, RoutedEventArgs e) => this.Close();

		void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) => Process.Start(e.Uri.ToString());
	}

	public class LibraryData
	{
		public string Author { get; set; }

		public string Library { get; set; }

		public Version Version { get; set; }

		public Uri Url { get; set; }
	}
}
