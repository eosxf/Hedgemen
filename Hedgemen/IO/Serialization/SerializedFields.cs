using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Hgm.IO.Serialization;

[Serializable]
public class SerializedFields : Dictionary<string, object>
{
	public SerializedFields()
	{
	}

	public SerializedFields(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
		
	}

	public T Get<T>(string key, T defaultReturn = default, JsonSerializerOptions options = null)
	{
		if (!ContainsKey(key))
			return defaultReturn;
		
		var json = this[key];
		var obj = defaultReturn;

		if (json is JsonObject jsonObject)
			obj = jsonObject.Deserialize<T>(options);

		else if (json is JsonElement jsonElement) 
			obj = jsonElement.Deserialize<T>(options);

		return obj;
	}

	public T GetFields<T>(string key, T defaultReturn = default, JsonSerializerOptions options = null,
		IReadOnlyDictionary<string, Assembly> registeredAssemblies = null,
		bool init = true)
		where T : ISerializableInfo
	{
		var fields = Get<SerializedFields>(key);
		return fields.Instantiate<T>(registeredAssemblies, init);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SerializedInfo Get(string key, JsonSerializerOptions options = null)
	{
		return Get<SerializedInfo>(key, null, options);
	}
	
	// stupid hack that might just work
	public T Instantiate<T>(IReadOnlyDictionary<string, Assembly> registeredAssemblies = null, bool init = true)
		where T : ISerializableInfo
	{
		Console.WriteLine($"Fields: {Count}");
		var serializedInfo = new SerializedInfo(typeof(T), this);
		return serializedInfo.Instantiate<T>(registeredAssemblies, init);
	}
}