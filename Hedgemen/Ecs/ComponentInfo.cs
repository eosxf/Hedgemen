using System;
using Hgm.Register;

namespace Hgm.Ecs;

/// <summary>
/// <see cref="Hgm.Ecs.IComponent" /> metadata that can be queried for any instantiated component.
/// </summary>
public readonly struct ComponentInfo
{
	public static ComponentInfo New(IComponent obj)
	{
		return new()
		{
			RegistryName = NamespacedString.Default,
			AccessType = obj.GetType(),
			PropagatesIgnoredEvents = false
		};
	}

	public NamespacedString RegistryName { get; init; }

	/// <summary>
	/// The type that this component will be accessed through via entity component get methods.
	/// </summary>
	public Type AccessType { get; init; }

	public bool PropagatesIgnoredEvents { get; init; }
}