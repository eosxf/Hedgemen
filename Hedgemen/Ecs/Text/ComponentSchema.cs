using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hgm.Register;

namespace Hgm.Ecs.Text;

public class ComponentSchema : ISchema
{
	private Dictionary<string, JsonElement> _fields = new();

	public ComponentSchema(JsonView view)
	{
		Initialize(view);
	}

	public NamespacedString RegistryName { get; private set; }

	// todo maybe put in static method for code reuse
	public T GetValue<T>(string name, T defaultReturn = default)
	{
		if (!_fields.ContainsKey(name)) return defaultReturn;

		var json = _fields[name];
		var obj = json.Deserialize<T>();

		return obj;
	}

	private void Initialize(JsonView view)
	{
		RegistryName = view.RegistryName;
		_fields = view.Fields;
	}

	public class JsonView
	{
		[JsonInclude]
		[JsonExtensionData]
		public Dictionary<string, JsonElement> Fields = new();

		[JsonInclude]
		[JsonPropertyName("registry_name")]
		public string RegistryName = NamespacedString.Default;
	}
}