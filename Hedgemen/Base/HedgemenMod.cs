using Hgm.Ecs;

namespace Hgm.Base;

/// <summary>
/// Base content for Hedgemen. WIP
/// </summary>
public class HedgemenMod
{
	public void Initialize()
	{
		Hedgemen.Kaze.Registry.Components.Register("hedgemen:character", () => new Character());
		Hedgemen.Kaze.Registry.Components.Register("hedgemen:cell", () => new Cell());
		Hedgemen.Kaze.Registry.Components.Register("hedgemen:character_sheet", () => new CharacterSheet());

		Hedgemen.Kaze.NewRegistry.Register("hedgemen:class_sentient_food", () => new CharacterClassSentientFood());
	}
}