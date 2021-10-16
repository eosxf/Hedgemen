using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hgm.Ecs.Text
{
	public class EntitySchema
	{
		private bool isInitialized = false;

		[JsonConstructor]
		public EntitySchema()
		{
			
		}

		private string registryName = string.Empty;
		private string inheritsName = string.Empty;
		private List<PartSchema> parts = new List<PartSchema>();

		[JsonInclude]
		[JsonPropertyName("registry_name")]
		public string RegistryName
		{
			get => registryName;
			private set => registryName = value;
		}

		[JsonInclude]
		[JsonPropertyName("inherits_name")]
		public string InheritsName
		{
			get => inheritsName;
			private set => inheritsName = value;
		}

		[JsonInclude]
		[JsonPropertyName("parts")]
		public IReadOnlyList<PartSchema> Parts
		{
			get => parts;
			private set
			{
				if(value is List<PartSchema>)
					parts = value as List<PartSchema>;
			}
		}

		[JsonIgnore]
		public bool IsInitialized
		{
			get => isInitialized;
			internal set => isInitialized = value;
		}
	}
}