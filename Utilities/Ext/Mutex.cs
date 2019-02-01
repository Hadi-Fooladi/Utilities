using System;
using System.Threading;

namespace HaFT.Utilities
{
	public static class MutexExt
	{
		public static void RunExclusively(this Mutex M, Action A)
		{
			try { M.WaitOne(); A(); }
			finally { M.ReleaseMutex(); }
		}

		public static T RunExclusively<T>(this Mutex M, Func<T> F)
		{
			try { M.WaitOne(); return F(); }
			finally { M.ReleaseMutex(); }
		}
	}
}
