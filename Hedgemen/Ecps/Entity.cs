using System;
using System.Collections.Generic;

namespace Hgm.Ecps;

public sealed class Entity
{
	private IDictionary<Type, IProperty> _properties;
	private IDictionary<Type, Component> _components;
}