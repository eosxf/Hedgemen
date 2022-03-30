using System;

namespace Hgm.EcsNew.Systems;

public static class MovementSystem
{
	public static MovementResult ExecuteMovement(Entity entity, int x, int y)
	{
		if (!entity.InteractsWithSystem(typeof(MovementSystem)))
			return MovementResult.Failure;

		Console.WriteLine($"Moved entity to {x},{y}");

		if (entity.WillRespondTo<ModifyStatEvent>())
		{
			Console.WriteLine($"Debug changing strength to 1025");
			entity.PropagateEvent(new ModifyStatEvent
			{
				StatName = "strength",
				Value = 1025
			});
			Console.WriteLine($"Debug entity new strength: {entity.GetComponent<CharacterAttributes>().Strength}");
		}
		
		// ...
		return MovementResult.Success;
	}
}

public enum MovementResult
{
	Success,
	Failure
}