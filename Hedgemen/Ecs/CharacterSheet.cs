using System;
using Hgm.IO.Serialization;

namespace Hgm.Ecs;

/// <summary>
/// Debug class, not meant for actual game
/// </summary>
public class CharacterSheet : Component, ISerializableState<CharacterSheetState>
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

	public CharacterSheetState ToState()
	{
		return new CharacterSheetState
		{
			Strength = Strength,
			Dexterity = Dexterity,
			Constitution = Constitution,
			Intelligence = Intelligence,
			Wisdom = Wisdom,
			Charisma = Charisma,
			ClassName = Class.ClassName
		};
	}

	public void FromState(CharacterSheetState state)
	{
		Strength = state.Strength;
		Dexterity = state.Dexterity;
		Constitution = state.Constitution;
		Intelligence = state.Intelligence;
		Wisdom = state.Wisdom;
		Charisma = state.Charisma;
		Class = new CharacterClass { ClassName = state.ClassName };
	}
}

public struct CharacterSheetState
{
	public int Strength;
	public int Dexterity;
	public int Constitution;
	public int Intelligence;
	public int Wisdom;
	public int Charisma;
	public string ClassName;
}