using System;

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
}