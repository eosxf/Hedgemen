namespace Hgm.Ecs;

public class Character : Component
{
	public override ComponentInfo QueryComponentInfo()
	{
		return new ComponentInfo
		{
			RegistryName = "hedgemen:character",
			AccessType = typeof(Character),
			PropagatesIgnoredEvents = true
		};
	}
}