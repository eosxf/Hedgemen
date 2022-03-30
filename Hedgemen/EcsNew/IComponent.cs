using System;
using System.Collections.Generic;
using Hgm.Ecs.Text;
using Hgm.IO.Serialization;
using Hgm.Register;

namespace Hgm.EcsNew;

public interface IComponent : ISerializableState
{
	public Entity Self { get; }
	public void Initialize(Entity self);
	public void ReadComponentSchema(ComponentSchema schema);
	public ComponentInfoQuery QueryComponentInfo();
}

public class ComponentInfoQuery
{
	public NamespacedString RegistryName = NamespacedString.Default;
	public HashSet<Type> SystemInteractions = new();
}