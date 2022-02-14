using Hgm.Ecs.Serialization;

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
	
	public override SerializedInfo GetSerializedInfo()
	{
		return new SerializedInfo(this)
		{
			{"strength", Strength},
			{"dexterity", Dexterity},
			{"constitution", Constitution},
			{"intelligence", Intelligence},
			{"wisdom", Wisdom},
			{"charisma", Charisma}
		};
	}

	public override void ReadSerializedInfo(SerializedInfo info)
	{
		Strength = info.Get<int>("strength");
		Dexterity = info.Get<int>("dexterity");
		Constitution = info.Get<int>("constitution");
		Intelligence = info.Get<int>("intelligence");
		Wisdom = info.Get<int>("wisdom");
		Charisma = info.Get<int>("charisma");
	}
	
	public CharacterClass Class { get; set; }

	public CharacterSheet()
	{
		Class = new CharacterClass();
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
}