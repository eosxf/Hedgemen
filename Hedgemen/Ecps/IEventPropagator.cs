namespace Hgm.Ecps;

public interface IEventPropagator
{
	public bool Propagate(Event gameEvent) => false;
}