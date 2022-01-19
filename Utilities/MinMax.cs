using System;
using System.IO;

namespace HaFT.Utilities
{
	public struct MinMax
	{
		public float Min, Max;

		public static readonly MinMax
			Normal = new MinMax(0, 1),
			SignedNormal = new MinMax(-1, 1);

		#region Constructors
		public MinMax(float Min, float Max)
		{
			this.Min = Min;
			this.Max = Max;
		}

		public MinMax(string S)
		{
			string[] SP = S.Split(',');
			Min = Convert.ToSingle(SP[0]);
			Max = Convert.ToSingle(SP[1]);
		}

		public MinMax(BinaryReader BR)
		{
			Min = BR.ReadSingle();
			Max = BR.ReadSingle();
		}

		public MinMax Scale(float Factor)
		{
			float
				C = Center,
				f = Factor * Length / 2;

			return new MinMax(C - f, C + f);
		}
		#endregion

		public float Length => Max - Min;
		public float Center => (Max + Min) / 2;

		public bool Contains(float Value) => RealMin <= Value && Value <= RealMax;

		public float Normalize(float Value) => (Value - Min) / Length;
		public float Denormalize(float NormalizedValue) => NormalizedValue * Length + Min;

		public float Clamp(float Value) => Value.Clamp(RealMin, RealMax);
		public float ClampedDenormalize(float NormalizedValue) { return Clamp(Denormalize(NormalizedValue)); }

		/// <summary>
		/// Convert the value from this range to the target range
		/// </summary>
		public float ConvertTo(float Value, MinMax Target) { return Target.Denormalize(Normalize(Value)); }

		public float ClampedConvertTo(float Value, MinMax Target) { return Target.Clamp(ConvertTo(Value, Target)); }

		/// <summary>
		/// Mirroring the value around center
		/// </summary>
		public float Mirror(float Value) => Min + (Max - Value);

		public void Write(BinaryWriter BW)
		{
			BW.Write(Min);
			BW.Write(Max);
		}

		#region Operator Overloadings
		public static MinMax operator *(MinMax MM, float Value) => Value * MM;
		public static MinMax operator *(float Value, MinMax MM) => new MinMax(MM.Min * Value, MM.Max * Value);

		public static MinMax operator +(MinMax MM1, MinMax MM2) => new MinMax(MM1.Min + MM2.Min, MM1.Max + MM2.Max);
		#endregion

		private float RealMin => Math.Min(Min, Max);
		private float RealMax => Math.Max(Min, Max);
	}
}
