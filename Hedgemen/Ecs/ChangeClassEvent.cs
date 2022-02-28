namespace Hgm.Ecs;

public class ChangeClassEvent : GameEvent
{
	public ChangeClassEvent(string className)
	{
		ClassName = className;
	}

	public string ClassName { get; set; }

	public override bool IsProperlyInitialized()
	{
		return ClassName != null;
	}
}