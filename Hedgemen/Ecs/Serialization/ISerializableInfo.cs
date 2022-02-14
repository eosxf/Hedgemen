namespace Hgm.Ecs.Serialization;

public interface ISerializableInfo
{
	public SerializedInfo GetSerializedInfo();
	public void ReadSerializedInfo(SerializedInfo info);
}