using System;
using System.Linq;
using System.Collections.Generic;

namespace HaFT.Utilities.Razor.Models.HTML;

public class StyleBuilder
{
	readonly Dictionary<string, object> _valueByName = new(COMPARER);

	public StyleBuilder? Base { get; set; }

	public object? this[string name]
	{
		get => _valueByName.GetValueOrDefault(name) ?? Base?[name];
		set
		{
			if (value == null) _valueByName.Remove(name);
			else _valueByName[name] = value;
		}
	}

	public IEnumerable<string> Names
	{
		get
		{
			HashSet<string> names = new(COMPARER);
			update(this);
			return names;

			void update(StyleBuilder? builder)
			{
				if (builder == null) return;

				// Recursively add all names from the base(s)
				update(builder.Base);

				// Remove or add the names from this collection
				foreach (var (name, value) in builder._valueByName)
					if (ReferenceEquals(value, SpecialValue.Discard))
						names.Remove(name);
					else names.Add(name);
			}
		}
	}

	public override string ToString() => string.Join(SEPARATOR, Names.Select(name => $"{name}:{this[name]}"));

	public StyleBuilder Add(string name, object value)
	{
		this[name] = value;
		return this;
	}

	public StyleBuilder Remove(string name)
	{
		_valueByName.Remove(name);
		return this;
	}

	public StyleBuilder Discard(string name) => Add(name, SpecialValue.Discard);

	public static implicit operator string(StyleBuilder builder) => builder.ToString();

	const char SEPARATOR = ';';
	static readonly StringComparer COMPARER = StringComparer.OrdinalIgnoreCase;
}
