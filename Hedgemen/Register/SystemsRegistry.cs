using System;
using System.Collections.Generic;

namespace Hgm.Register;

/// <summary>
/// this is probably a stupid idea
/// </summary>
public class SystemsRegistry
{
	private readonly IDictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();

	public bool RegisterSystem<TSystem>() where TSystem : ISystem, new()
	{
		var systemType = typeof(TSystem);
		if (_systems.ContainsKey(systemType))
			return false;

		var system = new TSystem();
		_systems.Add(systemType, system);
		return true;
	}

	public TSystem Get<TSystem>() where TSystem : ISystem
	{
		var success = _systems.TryGetValue(typeof(TSystem), out var result);
		if (!success)
			throw new InvalidOperationException($"System of type '{nameof(TSystem)}' could not be retrieved from the" +
			                                    "systems registry.");
		if (result is not TSystem tResult)
			throw new InvalidOperationException($"System of type '{nameof(TSystem)}' could not be " +
			                                    $"cast to '{result.GetType()}'. This is likely a bug.");
		return tResult;
	}
}

/// <summary>
/// prototype
/// </summary>
public class MovementSystem
{
	
}