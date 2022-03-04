namespace Hgm.Ecs;

public class CharacterClass : IGameEventHandler
{
	public string ClassName { get; set; } = "nothing";

	public void Propagate(GameEvent gameEvent)
	{
	}
}