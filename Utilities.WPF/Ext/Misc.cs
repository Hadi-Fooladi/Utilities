using System;
using System.Windows.Threading;

namespace HaFT.Utilities.WPF
{
	public static class MiscExt
	{
		public static void Invoke(this DispatcherObject DO, Action A) => DO.Dispatcher.InvokeAsync(A);
	}
}
