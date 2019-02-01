using System;

namespace HaFT.Utilities
{
	public static class MathExt
	{
		#region Radian <=> Degree
		private const double
			Deg2Rad = Math.PI / 180,
			Rad2Deg = 180 / Math.PI;

		private const float
			Deg2RadF = (float)Deg2Rad,
			Rad2DegF = (float)Rad2Deg;

		public static double ToRadian(this double Degree) => Degree * Deg2Rad;
		public static double ToDegree(this double Radian) => Radian * Rad2Deg;

		public static float ToRadian(this float Degree) => Degree * Deg2RadF;
		public static float ToDegree(this float Radian) => Radian * Rad2DegF;
		#endregion

		public static int Clamp(this int Val, int Min, int Max) => Math.Min(Max, Math.Max(Min, Val));
		public static float Clamp(this float Val, float Min, float Max) => Math.Min(Max, Math.Max(Min, Val));
		public static double Clamp(this double Val, double Min, double Max) => Math.Min(Max, Math.Max(Min, Val));

		public static float AbsClamp(this float Val, float Magnitude) => Val.Clamp(-Magnitude, Magnitude);

		public static int Round(this float Val) => (int)Math.Round(Val);

		/// <summary>
		/// Moves <paramref name="Degree" /> into (-180, 180]
		/// </summary>
		public static float NormalizeDegree(this float Degree)
		{
			if (Degree > 180)
			{
				Degree %= 360;
				return Degree <= 180 ? Degree : Degree - 360;
			}

			if (Degree <= -180)
			{
				Degree %= 360;
				return Degree > -180 ? Degree : Degree + 360;
			}

			return Degree;
		}
	}
}
