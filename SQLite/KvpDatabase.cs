using System;
using SQLite;

namespace HaFT.Utilities.SQLite
{
	public class KvpDatabase<TKey> : SQLiteConnection where TKey : Enum
	{
		public KvpDatabase(string path) : base(path)
		{
			CreateTable<KVP>();
		}

		private class KVP
		{
			[PrimaryKey]
			public int Key { get; set; }

			public string Value { get; set; }
		}

		#region Private Methods
		private static int ToInt(TKey key) => ((IConvertible)key).ToInt32(null);

		private void Write(KVP kvp) => InsertOrReplace(kvp);
		private void Delete(KVP kvp) => base.Delete(kvp);

		private KVP GetPair(TKey key)
		{
			var code = ToInt(key);
			return Table<KVP>().FirstOrDefault(p => p.Key == code);
		}

		private T? GenericRead<T>(TKey key, Func<string, T> parse) where T : struct
		{
			var pair = GetPair(key);
			if (pair == null) return null;
			return parse(pair.Value);
		}
		#endregion

		#region Public Methods
		public bool Exist(TKey key) => GetPair(key) != null;
		public void Delete(TKey key) => Delete<KVP>(ToInt(key));

		#region Write Methods
		public void Write(TKey key, string value) => Write(new KVP { Key = ToInt(key), Value = value });
		public void Write(TKey key, object value) => Write(key, value.ToString());
		public void Write(TKey key, DateTime date) => Write(key, date.ToBinary());

		public void Write(TKey key, DateTime? date)
		{
			if (date == null)
				Delete(key);
			else
				Write(key, date.Value);
		}
		#endregion

		#region Read Methods
		public string Read(TKey key)
		{
			var pair = GetPair(key);
			return pair != null
				? pair.Value
				: throw new Exception("Key not found");
		}

		public string Read(TKey key, string @default)
		{
			var pair = GetPair(key);
			return pair == null ? @default : pair.Value;
		}

		/// <summary>
		/// Returns null when key doesn't exist.
		/// </summary>
		public Version ReadVersion(TKey key)
		{
			var pair = GetPair(key);
			return pair == null ? null : new Version(pair.Value);
		}

		/// <summary>
		/// Returns null when key doesn't exist.
		/// </summary>
		public int? ReadInt32(TKey key) => GenericRead(key, int.Parse);

		/// <summary>
		/// Returns null when key doesn't exist.
		/// </summary>
		public DateTime? ReadDateTime(TKey key) => GenericRead(key, s => DateTime.FromBinary(long.Parse(s)));

		/// <summary>
		/// Returns null when key doesn't exist.
		/// </summary>
		public bool? ReadBool(TKey key) => GenericRead(key, bool.Parse);
		#endregion
		#endregion
	}
}
