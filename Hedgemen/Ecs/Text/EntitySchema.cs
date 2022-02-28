using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hgm.IO;
using Hgm.Register;

namespace Hgm.Ecs.Text;

public class EntitySchema
{
	private List<ComponentSchema> _components;

	public EntitySchema(IFile schemaFile)
	{
		var view = JsonSerializer.Deserialize<JsonView>(schemaFile.Open());
		Initialize(view);
	}

	public NamespacedString RegistryName { get; private set; }

	public NamespacedString Inherits { get; private set; }

	public IReadOnlyList<ComponentSchema> Components => _components;

	private void Initialize(JsonView view)
	{
		RegistryName = view.RegistryName;
		Inherits = view.Inherits;

		var componentsView = view.Components;
		_components = new List<ComponentSchema>(componentsView.Count);
		componentsView.ForEach(e => _components.Add(new ComponentSchema(e)));
	}

	public override string ToString()
	{
		var builder = new StringBuilder()
			.Append($"RegistryName: {RegistryName}\n")
			.Append($"Inherits: {Inherits}\n")
			.Append("Components:\n");

		foreach (var component in Components)
			builder
				.Append(component.RegistryName)
				.Append('\n');

		builder.Remove(builder.Length - 1, 1);
		return builder.ToString();
	}

	public class JsonView
	{
		[JsonInclude] [JsonPropertyName("components")]
		public List<ComponentSchema.JsonView> Components;

		[JsonInclude] [JsonPropertyName("inherits")]
		public string Inherits = string.Empty;

		[JsonInclude] [JsonPropertyName("registry_name")]
		public string RegistryName = NamespacedString.Default;
	}
}