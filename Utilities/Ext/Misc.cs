﻿namespace HaFT.Utilities
{
	public static class MiscExt
	{
		public static bool isTrue(this bool? B) => B ?? false;
		public static bool isFalse(this bool? B) => !(B ?? true);
	}
}
