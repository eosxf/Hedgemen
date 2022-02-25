using Hgm.IO.Serialization;

namespace Hgm.Ecs;

public class CharacterClass : IGameEventHandler, ISerializableInfo
{
	public string ClassName { get; set; } = "nothing";
	
	public SerializedInfo GetSerializedInfo()
	{
		var fields = new SerializedFields
		{
			{"class_name", ClassName}
		};

		return new SerializedInfo(this, fields);
	}

	public void ReadSerializedInfo(SerializedInfo info)
	{
		ClassName = info.Fields.Get<string>("class_name");
	}

	public void Propagate(GameEvent gameEvent)
	{
		
	}
}