using System;
using System.Windows;
using System.Windows.Media;

namespace HaFT.Utilities.WPF
{
	public partial class GrayButton
	{
		private readonly MakeGrayShader Sh = new MakeGrayShader();

		public static readonly DependencyProperty isEnableProperty = DPH<bool, GrayButton>.Register("isEnable", true, isEnableChangedCallback);

		public GrayButton()
		{
			InitializeComponent();
		}

		public ImageSource Source
		{
			get => img.Source;
			set
			{
				img.Source = value;
				Sh.Input = new ImageBrush(value);
			}
		}

		public bool isEnable
		{
			get => (bool)GetValue(isEnableProperty);
			set => SetValue(isEnableProperty, value);
		}

		private static void isEnableChangedCallback(GrayButton B, bool Value)
		{
			B.IsEnabled = Value;
			B.img.Effect = Value ? null : B.Sh;
		}
	}
}
