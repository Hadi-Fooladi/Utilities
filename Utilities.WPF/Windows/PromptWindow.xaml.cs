using System.Windows;

namespace HaFT.Utilities.WPF
{
	public partial class PromptWindow
	{
		public PromptWindow() { InitializeComponent(); }

		public string Text
		{
			get => TB.Text;
			set => TB.Text = value;
		}

		public string Message
		{
			get => lbl.Text;
			set => lbl.Text = value;
		}

		private void bOK_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}
	}
}
