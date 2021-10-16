using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hgm.Ecs.Text
{
	public class PartSchema
	{
		[JsonConstructor]
		public PartSchema()
		{

		}

		private string registryName = string.Empty;
		private Dictionary<string, object> fields = new();

		[JsonInclude]
		[JsonPropertyName("registry_name")]
		public string RegistryName
		{
			get => registryName;
			private set => registryName = value;
		}

		[JsonInclude]
		[JsonPropertyName("fields")]
		public IReadOnlyDictionary<string, object> Fields
		{
			get => fields;
			private set
			{
				if(value is Dictionary<string, object>)
					fields = value as Dictionary<string, object>;
			}
		}
	}
}