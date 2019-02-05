using System.Windows;
using static System.Windows.MessageBox;
using static System.Windows.MessageBoxButton;

namespace HaFT.Utilities.WPF
{
	internal static class Msg
	{
		public static void Error(object obj) => Error(obj.ToString());
		public static void Error(string Text, string Title = "Error") => Show(Text, Title, OK, MessageBoxImage.Error);

		public static void Info(object obj) => Info(obj.ToString());
		public static void Info(string Text, string Title = "Information") => Show(Text, Title, OK, MessageBoxImage.Information);

		public static bool Confirm(string Text, string Title = "Confirm") => Show(Text, Title, YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

		public static bool Prompt(string Title, string Message, ref string Text)
		{
			var W = new PromptWindow
			{
				Text = Text,
				Title = Title,
				Message = Message
			};

			var Result = W.ShowDialog().isTrue();

			if (Result)
				Text = W.Text;

			return Result;
		}
	}
}
