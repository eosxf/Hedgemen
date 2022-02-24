using System;

namespace Hgm.Ecs;

public class GameChangeClassEvent : GameEvent
{
	public string ClassName { get; set; } = String.Empty;
	
	public GameChangeClassEvent(string className)
	{
		ClassName = className;
	}
}