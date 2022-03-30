namespace Hgm.Ecs;

public class Cell : Component
{
	protected override void InitializeComponent()
	{
	}

	public override ComponentInfo QueryComponentInfo()
	{
		return new ComponentInfo
		{
			RegistryName = "hedgemen:cell",
			AccessType = typeof(Cell),
			PropagatesIgnoredEvents = true
		};
	}
}