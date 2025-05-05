using System;
using System.Linq;
using System.Collections.Generic;

namespace HaFT.Utilities.Razor.Models.HTML;

public class AttributeCollection
{
	#region Fields
	readonly Dictionary<string, object> _attributes = new(COMPARER);
	#endregion

	#region Properties
	public AttributeCollection? Base { get; set; }

	/// <summary>
	/// null to remove the attribute. Sets/Removes the value only for this instance, not the base.
	/// </summary>
	/// <returns>null if the <see cref="name"/> does not exist here or in the base(s)</returns>
	public object? this[string name]
	{
		get => _attributes.GetValueOrDefault(name) ?? Base?[name];
		set
		{
			if (value == null) _attributes.Remove(name);
			else _attributes[name] = value;
		}
	}

	public IEnumerable<string> Names
	{
		get
		{
			HashSet<string> names = new(COMPARER);
			update(this);
			return names;

			void update(AttributeCollection? collection)
			{
				if (collection == null) return;

				// Recursively add all names from the base(s)
				update(collection.Base);

				// Remove or add the names from this collection
				foreach (var attribute in collection._attributes)
					if (ReferenceEquals(attribute.Value, SpecialValue.Discard))
						names.Remove(attribute.Key);
					else names.Add(attribute.Key);
			}
		}
	}

	public string? Classes
	{
		get => GetString("class");
		set => this["class"] = value;
	}

	public string? Style
	{
		get => GetString("style");
		set => this["style"] = value;
	}
	#endregion

	#region Methods
	public AttributeCollection With(string name, object? value)
	{
		this[name] = value;
		return this;
	}

	public AttributeCollection Discard(string name)
	{
		this[name] = SpecialValue.Discard;
		return this;
	}

	public string? GetString(string name)
	{
		var value = this[name];
		return value is null or SpecialValue ? null : value.ToString();
	}
	#endregion

	public override string ToString()
	{
		return string.Concat(Names.Select(toString));

		string toString(string name)
		{
			var value = this[name];
			return ReferenceEquals(value, SpecialValue.Empty)
				? $" {name}"
				: $" {name}='{value}'";
		}
	}

	static readonly StringComparer COMPARER = StringComparer.OrdinalIgnoreCase;
}
