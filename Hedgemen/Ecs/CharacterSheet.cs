using System;
using Hgm.IO.Serialization;

namespace Hgm.Ecs;

/// <summary>
/// Debug class, not meant for actual game
/// </summary>
public class CharacterSheet : Part
{
	public CharacterSheet()
	{
		Class = new CharacterClass();
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
		Console.WriteLine($"Changed class from '{oldClass}' to '{e.ClassName}'");

		Console.WriteLine($"Does Self have this component (should always be true?: {Self.HasPart<CharacterSheet>()}");
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

	public override SerializedInfo GetSerializedInfo()
	{
		return new SerializedInfo(this, new SerializedFields
		{
			{"strength", Strength},
			{"dexterity", Dexterity},
			{"constitution", Constitution},
			{"intelligence", Intelligence},
			{"wisdom", Wisdom},
			{"charisma", Charisma},
			{"class", Class.GetSerializedInfo()}
		});
	}

	public override void InitializeFromFields(SerializedFields fields)
	{
		Strength = fields.Get("strength", 10);
		Dexterity = fields.Get("dexterity", 10);
		Constitution = fields.Get("constitution", 10);
		Intelligence = fields.Get("intelligence", 10);
		Wisdom = fields.Get("wisdom", 10);
		Charisma = fields.Get("charisma", 10);
		Class = fields.Get("class").Instantiate<CharacterClass>();
	}
}