using System;
using System.Diagnostics;

namespace Hgm.Ecps;

public static class EcpsMain
{
	public static void Sandbox()
	{
		var entity = new Entity();
		entity.AddComponent(new CharacterSheet());
		entity.Propagate(new ChangeStrengthEvent { NewValue = 1025 });
		
		Console.WriteLine($"Entity component: {entity.GetComponent<CharacterSheet>()}");
		Console.WriteLine($"Entity property: {entity.GetProperty<CharacterStats>()}");
		
		Stopwatch stopWatch = new Stopwatch();
		stopWatch.Start();
        
		for(int i = 0; i < 256*256; ++i)
		{
			entity.GetProperty<CharacterStats>();
			var e = new ChangeStrengthEvent() {NewValue = 1025};
			entity.Propagate(e);
			//entity.Propagate(new ChangeStrengthEvent() { NewValue = 1025 });
		}

		stopWatch.Stop();
		var ts = stopWatch.Elapsed;
		Console.WriteLine($"Elapsed time: {ts.Milliseconds} ms");
	}
}