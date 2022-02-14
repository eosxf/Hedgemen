using System;
using Hgm.Register;

namespace Hgm.Ecs
{
	/// <summary>
	/// <see cref="Hgm.Ecs.IComponent"/> metadata that can be queried for any instantiated component.
	/// </summary>
	public readonly struct ComponentInfo
	{
		public NamespacedString RegistryName { get; init; }
		
		/// <summary>
		/// The type that this component will be accessed through via entity component get methods.
		/// </summary>
		public Type AccessType { get; init; }

		public bool PropagatesIgnoredEvents { get; init; }
	}
}