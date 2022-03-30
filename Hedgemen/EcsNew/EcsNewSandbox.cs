using System;
using System.Text.Json;
using Hgm.EcsNew.Systems;
using Hgm.IO;
using Hgm.IO.Serialization;

namespace Hgm.EcsNew;

public static class EcsNewSandbox
{
	public static void Sandbox()
	{
		var entity = new Entity();
		entity.AddComponent(new CharacterAttributes());
		entity.GetComponent<CharacterAttributes>().Strength = 15;
		Console.WriteLine($"Old entity Strength: {entity.GetComponent<CharacterAttributes>().Strength}");
		
		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		IFile file = new File("new_serialization.json");
		string json = JsonSerializer.Serialize(entity.GetObjectState(), options);
		file.WriteString(json);

		var state = JsonSerializer.Deserialize<SerializationState>(file.ReadString(), options)!;
		var newEntity = state.Instantiate<Entity>();
		Console.WriteLine($"New entity Strength: {newEntity.GetComponent<CharacterAttributes>().Strength}");
		
		var movementResult = MovementSystem.ExecuteMovement(entity, 10, 25);
		Console.WriteLine($"Succeeded in movement?: {movementResult}");
	}
}