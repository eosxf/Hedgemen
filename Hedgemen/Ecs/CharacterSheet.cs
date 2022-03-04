using System;
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

	public override SerializedInfo GetSerializedInfo()
	{
		return new SerializedInfo(this, new SerializedFields
		{
			{"hedgemen:character_sheet/strength", Strength},
			{"hedgemen:character_sheet/dexterity", Dexterity},
			{"hedgemen:character_sheet/constitution", Constitution},
			{"hedgemen:character_sheet/intelligence", Intelligence},
			{"hedgemen:character_sheet/wisdom", Wisdom},
			{"hedgemen:character_sheet/charisma", Charisma},
			{"hedgemen:character_sheet/class", Class.GetSerializedInfo()}
		});
	}

	protected override void InitializeFromFields(IHasSerializedFields handle, SerializedFields fields)
	{
		// just an example of type matching for initialization context.
		if (handle is ComponentSchema schema)
		{
			Hedgemen.Logger.Debug($"Deserializing with {typeof(ComponentSchema)} context.");
			Class = fields.GetFields<CharacterClass>("hedgemen:character_sheet/class");
		}
		
		else if (handle is SerializedInfo info)
		{
			Hedgemen.Logger.Debug($"Deserializing with {typeof(SerializedInfo)} context.");
			Class = fields.Get("hedgemen:character_sheet/class")?.Instantiate<CharacterClass>();
		}
		
		// if you don't care about the deserialization context you can just use the fields exclusively
		Strength = fields.Get("hedgemen:character_sheet/strength", 10);
		Dexterity = fields.Get("hedgemen:character_sheet/dexterity", 10);
		Constitution = fields.Get("hedgemen:character_sheet/constitution", 10);
		// we purposefully get the wrong value to test default return
		Intelligence = fields.Get("hedgemen:character_sheet/intelligencee", 55);
		Wisdom = fields.Get("hedgemen:character_sheet/wisdom", 10);
		Charisma = fields.Get("hedgemen:character_sheet/charisma", 10);
	}
}