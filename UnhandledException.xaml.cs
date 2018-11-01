using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using FontAwesome5;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Interaction logic for UnhandledException.xaml
	/// </summary>
	public partial class UnhandledException : Window
	{
		new UnhandledExceptionViewModel DataContext => base.DataContext as UnhandledExceptionViewModel;

		public UnhandledException(Exception exception)
		{
			this.InitializeComponent();

			if (exception == null)
				exception = new Exception("Exception missing???");

			string exceptionType = exception.GetType().ToString();
			string exceptionMessage = exception.Message;
			this.DataContext.ExceptionType = exceptionType;
			this.DataContext.ExceptionMessage = exceptionMessage;

			var sb = new StringBuilder();
			sb.AppendLine("************** Exception Text **************");
			sb.AppendLine($"{exceptionType}: {exceptionMessage}");
			sb.AppendLine(exception.StackTrace);
			sb.AppendLine();
			sb.AppendLine("************** Loaded Assemblies **************");
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				var sbAssembly = new StringBuilder();
				try
				{
					var assemblyName = assembly.GetName();
					sbAssembly.AppendLine(assemblyName.Name);
					sbAssembly.AppendLine($"    Assembly Version: {assemblyName.Version}");
					sbAssembly.AppendLine($"    Win32 Version: {FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion}");
					sbAssembly.AppendLine($"    CodeBase: {assembly.CodeBase}");
					sbAssembly.AppendLine("----------------------------------------");
				}
				catch
				{
					continue;
				}
				sb.Append(sbAssembly.ToString());
			}
			this.DataContext.DetailsText = sb.ToString();
		}

		void Continue_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		void Details_Click(object sender, RoutedEventArgs e) => this.DataContext.ShowDetails = !this.DataContext.ShowDetails;

		void Quit_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}
	}

	/// <summary>
	/// The view model for <see cref="UnhandledException" />.
	/// </summary>
	class UnhandledExceptionViewModel
	{
		public EFontAwesomeIcon DetailsIcon => this.ShowDetails ? EFontAwesomeIcon.Solid_CaretUp : EFontAwesomeIcon.Solid_CaretDown;

		public string DetailsText { get; set; }

		public string ExceptionMessage { get; set; }

		public string ExceptionType { get; set; }

		public bool ShowDetails { get; set; }
	}
}
