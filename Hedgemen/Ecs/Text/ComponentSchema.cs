using System.Text.Json.Serialization;
using Hgm.IO.Serialization;
using Hgm.Register;

namespace Hgm.Ecs.Text;

public class ComponentSchema
{
	[JsonInclude] [JsonPropertyName("fields")]
	public SerializedFields Fields;

	[JsonInclude] [JsonPropertyName("registry_name")]
	public string RegistryName;

	public ComponentSchema(JsonView view)
	{
		Initialize(view);
	}

	private void Initialize(JsonView view)
	{
		RegistryName = view.RegistryName;
		Fields = view.Fields;
	}

	public class JsonView
	{
		[JsonInclude] [JsonPropertyName("fields")]
		public SerializedFields Fields = new();

		[JsonInclude] [JsonPropertyName("registry_name")]
		public string RegistryName = NamespacedString.Default;
	}
}