using System;
using Hgm.IO.Serialization;

namespace Hgm.Ecs;

/// <summary>
/// Debug class, not meant for actual game
/// </summary>
public class CharacterSheet : Part
{
	public int Strength { get; set; }
	public int Dexterity { get; set; }
	public int Constitution { get; set; }
	public int Intelligence { get; set; }
	public int Wisdom { get; set; }
	public int Charisma { get; set; }
	
	public CharacterClass Class { get; set; }
	
	public CharacterSheet()
	{
		Class = new CharacterClass();
		
		RegisterEvent<GameChangeClassEvent>(ChangeClass);
	}

	public void ChangeClass(GameChangeClassEvent e)
	{
		var oldClass = Class.ClassName;
		Class.ClassName = e.ClassName;
		Console.WriteLine($"Changed class from '{oldClass}' to '{e.ClassName}'");
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
		var fields = new SerializedFields
		{
			{"strength", Strength},
			{"dexterity", Dexterity},
			{"constitution", Constitution},
			{"intelligence", Intelligence},
			{"wisdom", Wisdom},
			{"charisma", Charisma},
			{"class", Class.GetSerializedInfo()}
		};

		return new SerializedInfo(this, fields);
	}

	public override void ReadSerializedInfo(SerializedInfo info)
	{
		var fields = info.Fields;
		Strength = fields.Get<int>("strength");
		Dexterity = fields.Get<int>("dexterity");
		Constitution = fields.Get<int>("constitution");
		Intelligence = fields.Get<int>("intelligence");
		Wisdom = fields.Get<int>("wisdom");
		Charisma = fields.Get<int>("charisma");
		Class = fields.Get("class").Instantiate<CharacterClass>();
	}
}