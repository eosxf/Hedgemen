namespace Hgm.Ecs;

public interface IGameEventHandler
{
    public void Propagate(GameEvent gameEvent);
}