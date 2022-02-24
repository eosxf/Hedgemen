namespace Hgm.IO.Serialization;

public interface ISerializableInfo
{
	public SerializedInfo GetSerializedInfo();
	public void ReadSerializedInfo(SerializedInfo info);
}