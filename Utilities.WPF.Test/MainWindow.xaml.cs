using System.Windows;

namespace HaFT.Utilities.WPF.Test
{
	internal partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
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
	}
}
