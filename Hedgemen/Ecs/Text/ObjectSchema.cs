using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hgm.Register;

namespace Hgm.Ecs.Text;

public class ObjectSchema : ISchema
{

	public NamespacedString RegistryName => RegistryNameString;
	
	[JsonInclude] [JsonPropertyName("registry_name")]
	public string RegistryNameString { get; private set; }
	
	private Dictionary<string, JsonElement> _fields = new();
	
	public T GetValue<T>(string name, T defaultReturn = default)
	{
		if (!_fields.ContainsKey(name)) return defaultReturn;
		
		var json = _fields[name];
		var obj = json.Deserialize<T>();

		return obj;
	}

	public ObjectSchema()
	{
		
	}

	public ObjectSchema(JsonView view)
	{
		Initialize(view);
	}

	private void Initialize(JsonView view)
	{
		RegistryNameString = view.RegistryName;
		_fields = view.Fields;
	}

	public class JsonView
	{
		[JsonInclude] [JsonPropertyName("registry_name")]
		public string RegistryName = NamespacedString.Default;
		
		[JsonInclude] [JsonExtensionData]
		public Dictionary<string, JsonElement> Fields = new();
	}
}