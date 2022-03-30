namespace Hgm.IO.Serialization;

public interface ISerializableState
{
	public SerializationState GetObjectState();

	public void SetObjectState(SerializationState state);
}