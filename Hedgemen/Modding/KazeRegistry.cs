using Hgm.Ecs;
using Hgm.Register;

namespace Hgm.Modding;

public delegate Component ComponentRegister();

public class KazeRegistry
{
	public KazeRegistry()
		: this(1337)
	{
		
	}
	
	public KazeRegistry(int initialCapacities)
	{
		Components = new ObjectRegistry<ComponentRegister>(initialCapacities);
	}
	
	public ObjectRegistry<ComponentRegister> Components { get; }
}