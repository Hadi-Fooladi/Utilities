using System.Collections.Generic;

namespace HaFT.Utilities.Razor.Models.HTML;

public class ClassBuilder
{
	const char SEPARATOR = ' ';

	string? _value;
	readonly HashSet<string> _classes = [];

	public override string ToString() => _value ??= string.Join(SEPARATOR, _classes);

	public ClassBuilder Add(params string[] classes) => Add((IEnumerable<string>)classes);
	public ClassBuilder Add(IEnumerable<string> classes)
	{
		foreach (var cls in classes)
			if (_classes.Add(cls))
				_value = null;
		
		return this;
	}

	public ClassBuilder Remove(params string[] classes) => Remove((IEnumerable<string>)classes);
	public ClassBuilder Remove(IEnumerable<string> classes)
	{
		foreach (var cls in classes)
			if (_classes.Remove(cls))
				_value = null;
		
		return this;
	}

	public static implicit operator string(ClassBuilder builder) => builder.ToString();
}
