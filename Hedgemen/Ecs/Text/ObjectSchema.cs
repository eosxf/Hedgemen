using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hgm.Register;

namespace Hgm.Ecs.Text;

public class ObjectSchema : ISchema
{
	private Dictionary<string, JsonElement> _fields = new();

	public ObjectSchema()
	{
	}

	public ObjectSchema(JsonView view)
	{
		Initialize(view);
	}

	[JsonInclude]
	[JsonPropertyName("registry_name")]
	public string RegistryNameString { get; private set; }

	public NamespacedString RegistryName => RegistryNameString;

	public T GetValue<T>(string name, T defaultReturn = default)
	{
		if (!_fields.ContainsKey(name)) return defaultReturn;

		var json = _fields[name];
		var obj = json.Deserialize<T>();

		return obj;
	}

	private void Initialize(JsonView view)
	{
		RegistryNameString = view.RegistryName;
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