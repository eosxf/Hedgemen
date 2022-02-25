namespace Hgm.Ecs;

public abstract class GameEvent
{
	public bool Handled = false;

	public virtual bool IsProperlyInitialized()
	{
		return true;
	}
}