using System;
using System.Text.Json.Serialization;

namespace Hgm.Register;

public struct NamespacedString
{
	public static string DefaultNamespace => "any";
	public static string DefaultName => "null";

	public static NamespacedString Default => new(DefaultNamespace, DefaultName);

	private string nameSpace;
	private string name;

	[JsonConstructor]
	public NamespacedString(string fullyQualifiedString)
	{
		var fullyQualifiedStringSplit = fullyQualifiedString.Split(':');

		if (!IsValidQualifiedString(fullyQualifiedString))
			throw new ArgumentException($"String {fullyQualifiedString} is not a valid namespaced string!");

		nameSpace = fullyQualifiedStringSplit[0];
		name = fullyQualifiedStringSplit[1];
	}

	public NamespacedString(string nameSpace, string name)
	{
		this.nameSpace = nameSpace;
		this.name = name;
	}

	[JsonIgnore]
	public string Namespace
	{
		get
		{
			nameSpace ??= DefaultNamespace;
			return nameSpace;
		}

		set => nameSpace = value;
	}

	[JsonIgnore]
	public string Name
	{
		get
		{
			name ??= DefaultName;
			return name;
		}

		set => name = value;
	}

	[JsonPropertyName("name")] public string FullName => nameSpace + ':' + name;

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