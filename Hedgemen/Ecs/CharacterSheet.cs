using System;
using System.Runtime.Serialization;
using Hgm.Ecs.Text;
using Hgm.IO.Serialization;

namespace Hgm.Ecs;

/// <summary>
/// Debug class, not meant for actual game
/// </summary>
public class CharacterSheet : Component
{
	protected override void InitializeSelf()
	{
		Class = new CharacterClass { ClassName = "warrior" };
		RegisterEvent<ChangeClassEvent>(ChangeClass);
	}

	public int Strength { get; set; }
	public int Dexterity { get; set; }
	public int Constitution { get; set; }
	public int Intelligence { get; set; }
	public int Wisdom { get; set; }
	public int Charisma { get; set; }

	public CharacterClass Class { get; set; }

	public void ChangeClass(ChangeClassEvent e)
	{
		var oldClass = Class.ClassName;
		Class.ClassName = e.ClassName;
		//Console.WriteLine($"Changed class from '{oldClass}' to '{e.ClassName}'");
		Hedgemen.Logger.Debug($"Changed class from '{oldClass}' to '{e.ClassName}'");

		Console.WriteLine($"Does Self have this component (should always be true?: {Self.HasComponent<CharacterSheet>()}");
	}

	public override ComponentInfo QueryComponentInfo()
	{
		return new ComponentInfo
		{
			RegistryName = "hedgemen:character_sheet",
			AccessType = typeof(CharacterSheet),
			PropagatesIgnoredEvents = true
		};
	}

	public override SerializationState GetObjectState()
	{
		var state = new SerializationState(this);
		state.AddValue("strength", Strength);
		state.AddValue("dexterity", Dexterity);
		state.AddValue("constitution", Constitution);
		state.AddValue("intelligence", Intelligence);
		state.AddValue("wisdom", Wisdom);
		state.AddValue("charisma", Charisma);
		state.AddValue("class", Class.GetObjectState());
		return state;
	}

	public override void SetObjectState(SerializationState state)
	{
		Strength = state.GetValue("strength", 10);
		Dexterity = state.GetValue("dexterity", 10);
		Constitution = state.GetValue("constitution", 10);
		Intelligence = state.GetValue("intelligence", 10);
		Wisdom = state.GetValue("wisdom", 10);
		Charisma = state.GetValue("charisma", 10);
		Class = state.GetState("class")?.Instantiate<CharacterClass>();
	}

	protected override void InitializeSchema(ComponentSchema schema)
	{
		
	}
}