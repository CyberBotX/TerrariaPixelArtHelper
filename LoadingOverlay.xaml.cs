using System.Windows.Controls;

/// <remarks>
/// See Program.cs for license.
/// </remarks>
namespace TerrariaPixelArtHelper
{
	/// <summary>
	/// Interaction logic for LoadingOverlay.xaml
	/// </summary>
	public partial class LoadingOverlay : UserControl
	{
		internal new LoadingOverlayViewModel DataContext => base.DataContext as LoadingOverlayViewModel;

		public LoadingOverlay() => this.InitializeComponent();
	}

	/// <summary>
	/// The view model for <see cref="LoadingOverlay" />.
	/// </summary>
	internal class LoadingOverlayViewModel
	{
		/// <summary>
		/// Whether there is a wait message or not.
		/// </summary>
		public bool HasWaitMessage => !string.IsNullOrEmpty(this.WaitMessage);

		/// <summary>
		/// If the Please Wait message should be showing or not.
		/// </summary>
		public bool ShowPleaseWait { get; set; } = true;

		/// <summary>
		/// The optional wait message to show below "Please Wait".
		/// </summary>
		public string WaitMessage { get; set; }
	}
}
