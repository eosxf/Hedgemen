using Hgm.Ecs;

namespace Hgm.Base;

/// <summary>
/// Base content for Hedgemen. WIP
/// </summary>
public class HedgemenMod
{
	public void Initialize()
	{
		Hedgemen.Kaze.Registry.Components.Register("hedgemen:character_sheet", () => new CharacterSheet());
	}
}