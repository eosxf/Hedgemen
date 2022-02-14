namespace Hgm.Ecs;

public interface IGameEventHandler
{
    public void HandleEvent(GameEvent gameEvent);
}