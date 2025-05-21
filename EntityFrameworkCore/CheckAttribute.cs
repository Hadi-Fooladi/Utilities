using System;

namespace HaFT.Utilities.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Property)]
public class CheckAttribute : Attribute
{
	public FilterCheckEnum Value { get; set; }

	/// <summary>
	/// If null, property name will be used.
	/// </summary>
	public string? Name { get; set; }

	public CheckAttribute(FilterCheckEnum value, string? name = null)
	{
		Name = name;
		Value = value;
	}
}
