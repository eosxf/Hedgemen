namespace Hgm.IO.Serialization;

public interface ISerializableState<TState>
{
	public TState ToState();
	public void FromState(TState state);
}