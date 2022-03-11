using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hgm.Utilities;

namespace Hgm.Ecps;

public class Entity
{
	private IDictionary<Type, IProperty> _properties = new CachedDictionary<Type, IProperty>();

	private IDictionary<Type, IComponent> _components = new CachedDictionary<Type, IComponent>();

	public Entity()
	{
		
	}

	public void AddComponent(IComponent component)
	{
		_components.Add(component.GetType(), component);
		component.Initialize(this);
	}

	public void AddProperty(IProperty property)
	{
		_properties.Add(property.GetType(), property);
	}

	public void EnsureProperty<TProperty>() where TProperty : IProperty, new()
	{
		if (_properties.ContainsKey(typeof(TProperty))) 
			return;
		var property = new TProperty();
		_properties.Add(property.GetType(), property);
	}

	public void EnsureProperty<TProperty>(ref TProperty property) where TProperty : IProperty, new()
	{
		var propertyType = typeof(TProperty);
		
		if (_properties.ContainsKey(propertyType))
		{
			property = (TProperty)_properties[propertyType];
			return;
		}
		
		property = new TProperty();
		_properties.Add(property.GetType(), property);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public TProperty GetProperty<TProperty>() where TProperty : IProperty
	{
		return (TProperty) _properties[typeof(TProperty)];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public TComponent GetComponent<TComponent>() where TComponent : IComponent
	{
		return (TComponent) _components[typeof(TComponent)];
	}

	public void Propagate(Event e)
	{
		foreach (var component in _components.Values)
		{
			component.Propagate(e);
		}
		
		foreach (var property in _properties.Values)
		{
			property.Propagate(e);
		}
	}
}