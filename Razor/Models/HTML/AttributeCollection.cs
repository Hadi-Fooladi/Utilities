using System;
using System.Linq;
using System.Collections.Generic;

namespace HaFT.Utilities.Razor.Models.HTML;

public class AttributeCollection
{
	#region Fields
	readonly Dictionary<string, string> _attributes = new(COMPARER);
	#endregion

	#region Properties
	public AttributeCollection? Base { get; set; }

	/// <summary>
	/// null to remove the attribute. Sets/Removes the value only for this instance, not the base.
	/// </summary>
	/// <returns>null if the <see cref="name"/> does not exist here or in the base(s)</returns>
	public string? this[string name]
	{
		get => _attributes.GetValueOrDefault(name) ?? Base?[name];
		set
		{
			if (value == null) _attributes.Remove(name);
			else _attributes[name] = value;
		}
	}

	public IEnumerable<string> Names
		=> Base == null
			? _attributes.Keys
			: _attributes.Keys.Concat(Base.Names).Distinct(COMPARER);

	public string? Classes
	{
		get => this["class"];
		set => this["class"] = value;
	}
	#endregion

	#region Methods
	public AttributeCollection With(string name, string? value)
	{
		this[name] = value;
		return this;
	}
	#endregion

	public override string ToString() => string.Concat(Names.Select(name => $" {name}='{this[name]}'"));

	static readonly StringComparer COMPARER = StringComparer.OrdinalIgnoreCase;
}
