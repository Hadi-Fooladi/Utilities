using System;

namespace HaFT.Utilities.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Property)]
public class CheckAttribute : Attribute
{
	public FilterCheckEnum Value { get; set; }

	public CheckAttribute(FilterCheckEnum value) { Value = value; }
}
