using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hgm.Ecs.Text;
using Hgm.IO.Serialization;
using Hgm.Utilities;

namespace Hgm.Ecs;

public delegate void ComponentEventWrapper(GameEvent e);

public delegate void ComponentEvent<in TEvent>(TEvent e) where TEvent : GameEvent;

public class Entity : ISerializableState
{
	private readonly IDictionary<Type, Component> _components = new CachedDictionary<Type, Component>();

	public Entity()
	{
		
	}

	public Entity(EntitySchema schema)
	{
		ReadEntitySchema(schema);
	}

	public Entity(SerializationState state)
	{
		SetObjectState(state);
	}
	
	public SerializationState GetObjectState()
	{
		var state = new SerializationState(this);
		foreach (var component in _components.Values)
			state.AddValue(component.QueryComponentInfo().RegistryName, component.GetObjectState());

		return state;
	}

	public void SetObjectState(SerializationState state)
	{
		foreach (var componentName in state.Fields)
		{
			var componentSerializationState = state.GetState(componentName.Key);
			var component = componentSerializationState.Instantiate<Component>(Hedgemen.RegisteredAssemblies, false);
			InternalAddComponent(component);
			component.ReadFromSerializationState(componentSerializationState);
		}
	}

	public TEvent Propagate<TEvent>(TEvent e) where TEvent : GameEvent
	{
		foreach (var component in _components.Values)
		{
			if (!component.IsActive) continue;

			var info = component.QueryComponentInfo();

			if (!e.IsProperlyInitialized())
				throw new InvalidOperationException($"Event: {e} is not properly initialized.");

			if (component.IsEventRegistered<TEvent>())
			{
				bool result = component.HandleEvent(e);
				if (result) e.Handled = true;
			}

			else if (info.PropagatesIgnoredEvents)
			{
				component.OnEventPropagated(e);
			}
		}

		return e;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool WillRespondTo(GameEvent e)
	{
		return WillRespondTo(e.GetType());
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool WillRespondTo<TEvent>() where TEvent : GameEvent
	{
		return WillRespondTo(typeof(TEvent));
	}

	public bool WillRespondTo(Type eventType)
	{
		foreach (var component in _components.Values)
			if (component.IsEventRegistered(eventType))
				return true;

		return false;
	}

	public void ReadEntitySchema(EntitySchema schema)
	{
		foreach (var componentSchema in schema.Components)
		{
			var component = Hedgemen.Kaze.Registry.Components[componentSchema.RegistryName]();
			InternalAddComponent(component);
			component.ReadFromComponentSchema(componentSchema);
		}
	}

	public void AddComponent(Component component)
	{
		InternalAddComponent(component);
		component.Initialize();
	}

	private void InternalAddComponent(Component component)
	{
		var infoQuery = component.QueryComponentInfo();

		if (_components.ContainsKey(infoQuery.AccessType))
			throw new ArgumentException($"{GetType().Name} already has a {typeof(Component)} with an AccessType of " +
			                            $"{infoQuery.AccessType.FullName}");
		component.AttachEntity(this);
		_components.Add(infoQuery.AccessType, component);
	}

	public T GetComponent<T>() where T : class
	{
		_components.TryGetValue(typeof(T), out var component);
		return component as T;
	}

	public bool HasComponent<T>() where T : class
	{
		return _components.ContainsKey(typeof(T));
	}
}