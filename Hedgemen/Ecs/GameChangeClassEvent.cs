namespace Hgm.Ecs;

public class GameChangeClassEvent : GameEvent
{
	public string ClassName { get; set; }
	
	public GameChangeClassEvent(string className)
	{
		ClassName = className;
	}

	public override bool IsProperlyInitialized() => ClassName != null;
}