using System.Windows;
using System.Windows.Interop;

namespace HaFT.Utilities.WPF.Test
{
	internal partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			BDG.ItemsSource = new[] { "Hadi", "Kevin" };
		}

		private void b1_OnClick(object sender, RoutedEventArgs e)
		{
			b1.isEnable = false;
			b2.isEnable = true;
		}

		private void b2_OnClick(object sender, RoutedEventArgs e)
		{
			b1.isEnable = true;
			b2.isEnable = false;
		}

		private void bShowPrompt_OnClick(object sender, RoutedEventArgs e)
		{
			var wnd = new PromptWindow
			{
				Title = "Title",
				Message = "Message:"
			};

			if (wnd.ShowDialog() == true)
				MessageBox.Show(wnd.Text);
		}
	}
}
