using System;
using System.Collections.Generic;
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

	public T Get<T>(string key, JsonSerializerOptions options = null)
	{
		var val = this[key];
		T obj = default;
		
		if (val is JsonObject jsonObject)
		{
			obj = jsonObject.Deserialize<T>(options);
		}
		
		else if (val is JsonElement jsonElement)
		{
			obj = jsonElement.Deserialize<T>(options);
		}
		
		return obj;
	}

	public SerializedInfo Get(string key, JsonSerializerOptions options = null) => Get<SerializedInfo>(key, options);
}