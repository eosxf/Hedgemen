using System.Text.Json.Serialization;
using Hgm.IO.Serialization;
using Hgm.Register;

namespace Hgm.Ecs.Text;

public class ComponentSchema : ISchema
{
	[JsonInclude] [JsonPropertyName("registry_name")]
	public string RegistryName { get; private set; }

	public ComponentSchema(JsonView view)
	{
		Initialize(view);
	}

	private void Initialize(JsonView view)
	{
		RegistryName = view.RegistryName;
	}

	public class JsonView
	{
		[JsonInclude] [JsonPropertyName("registry_name")]
		public string RegistryName = NamespacedString.Default;
	}
}