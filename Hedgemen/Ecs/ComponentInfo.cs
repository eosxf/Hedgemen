using System;
using System.Text;
using Hgm.Register;

namespace Hgm.Ecs;

/// <summary>
///     <see cref="Hgm.Ecs.Component" /> metadata that can be queried for any instantiated component.
/// </summary>
public readonly struct ComponentInfo
{
	public static ComponentInfo New(Component obj)
	{
		return new ComponentInfo
		{
			RegistryName = NamespacedString.Default,
			AccessType = obj.GetType(),
			PropagatesIgnoredEvents = false
		};
	}

	public NamespacedString RegistryName { get; init; }

	/// <summary>
	///     The type that this component will be accessed through via entity component get methods.
	/// </summary>
	public Type AccessType { get; init; }

	public bool PropagatesIgnoredEvents { get; init; }

	public override string ToString()
	{
		return new StringBuilder()
			.Append("RegistryName: ")
			.Append(RegistryName)
			.Append('\n')
			.Append("AccessType: ")
			.Append(AccessType)
			.Append('\n')
			.Append("PropagatesIgnoredEvents: ")
			.Append(PropagatesIgnoredEvents)
			.ToString();
	}
}