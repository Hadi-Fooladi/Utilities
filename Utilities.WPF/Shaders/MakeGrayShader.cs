using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HaFT.Utilities.WPF
{
	public class MakeGrayShader : ShaderEffect
	{
		private static PixelShader PS => LazyPS.Value;
		private static readonly Lazy<PixelShader> LazyPS = new Lazy<PixelShader>(CreateShader);

		public MakeGrayShader()
		{
			PixelShader = PS;
			UpdateShaderValue(InputProperty);
		}

		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(MakeGrayShader), 0);

		public Brush Input
		{
			get => (Brush)GetValue(InputProperty);
			set => SetValue(InputProperty, value);
		}

		private static PixelShader CreateShader() => new PixelShader { UriSource = Fn.GetLocalUri("HaFT.Utilities.WPF", "Resources/MakeGray.ps") };
	}
}
