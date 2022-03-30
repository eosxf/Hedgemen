using Hgm.IO.Serialization;
using Hgm.Utilities;

namespace Hgm.Ecs;

public class CharacterClass : IGameEventHandler, ISerializableState
{
	public string ClassName { get; set; } = "nothing";

	public void Propagate(GameEvent gameEvent)
	{
	}

	public SerializationState GetObjectState()
	{
		var state = new SerializationState(this);
		state.AddValue("class_name", ClassName);
		return state;
	}

	public void SetObjectState(SerializationState state)
	{
		ClassName = state.GetValue("class_name", "nothing");
	}
}