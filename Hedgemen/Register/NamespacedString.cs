using System;
using System.Text.Json.Serialization;

namespace Hgm.Register;

public struct NamespacedString
{
	public static string DefaultNamespace => "any";
	public static string DefaultName => "null";

	public static NamespacedString Default => new(DefaultNamespace, DefaultName);

	private string _namespace;
	private string _name;

	[JsonConstructor]
	public NamespacedString(string fullyQualifiedString)
	{
		var fullyQualifiedStringSplit = fullyQualifiedString.Split(':');

		if (!IsValidQualifiedString(fullyQualifiedString))
			throw new ArgumentException($"String {fullyQualifiedString} is not a valid namespaced string!");

		_namespace = fullyQualifiedStringSplit[0];
		_name = fullyQualifiedStringSplit[1];
	}

	public NamespacedString(string @namespace, string name)
	{
		_namespace = @namespace;
		_name = name;
	}

	[JsonIgnore]
	public string Namespace
	{
		get
		{
			_namespace ??= DefaultNamespace;
			return _namespace;
		}

		set => _namespace = value;
	}

	[JsonIgnore]
	public string Name
	{
		get
		{
			_name ??= DefaultName;
			return _name;
		}

		set => _name = value;
	}

	[JsonPropertyName("name")] public string FullName => _namespace + ':' + _name;

	private static bool IsValidQualifiedString(string fullyQualifiedString)
	{
		var fullyQualifiedStringSplit = fullyQualifiedString.Split(':');

		var correctNumberOfSplits = fullyQualifiedStringSplit.Length == 2;
		var noSpaces = !fullyQualifiedString.Contains(' ');

		return correctNumberOfSplits && noSpaces;
	}

	public override string ToString()
	{
		return FullName;
	}

	public static implicit operator NamespacedString(string val)
	{
		return new(val);
	}

	public static implicit operator string(NamespacedString val)
	{
		return val.FullName;
	}
}