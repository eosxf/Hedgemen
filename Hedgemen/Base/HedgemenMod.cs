using Hgm.Ecs;
using Microsoft.Xna.Framework;

namespace Hgm.Base;

/// <summary>
///     Base content for Hedgemen. WIP
/// </summary>
public class HedgemenMod
{
	public void Initialize()
	{
		Hedgemen.RegisterAssemblies(typeof(HedgemenMod), typeof(Game), typeof(object));

		Hedgemen.Kaze.NewRegistry.Register("hedgemen:character", () => new Character());
		Hedgemen.Kaze.NewRegistry.Register("hedgemen:cell", () => new Cell());
		Hedgemen.Kaze.NewRegistry.Register("hedgemen:character_sheet", () => new CharacterSheet());
		Hedgemen.Kaze.NewRegistry.Register("hedgemen:class_sentient_food", () => new CharacterClassSentientFood());
	}
}