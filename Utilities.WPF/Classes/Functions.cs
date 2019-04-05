using System;

namespace HaFT.Utilities.WPF
{
	internal static class Fn
	{
		public static Uri GetLocalUri(string AssemblyName, string ResourceRelativePath) => new Uri($"pack://application:,,,/{AssemblyName};component/{ResourceRelativePath}");
	}
}
