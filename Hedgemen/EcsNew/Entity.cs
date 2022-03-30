using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hgm.IO.Serialization;
using Hgm.Utilities;

namespace Hgm.EcsNew;

public delegate void ComponentEventWrapper(Event e);

public delegate void ComponentEvent<in TEvent>(TEvent e) where TEvent : Event;

public sealed class Entity : ISerializableState
{
	private IDictionary<Type, ComponentEntry> _componentEntries = new CachedDictionary<Type, ComponentEntry>();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool InteractsWithSystem(ISystem system) => InternalInteractsWithSystem(system.GetType());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool InteractsWithSystem<TSystem>() => InternalInteractsWithSystem(typeof(TSystem));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool InteractsWithSystem(Type systemType) => InternalInteractsWithSystem(systemType);

	private bool InternalInteractsWithSystem(Type systemType)
	{
		foreach (var componentEntry in _componentEntries)
		{
			if (componentEntry.Value.Info.SystemInteractions.Contains(systemType))
				return true;
		}

		return false;
	}

	public void AddComponent(IComponent component)
	{
		InternalAddComponent(component);
		component.Initialize(this);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AddComponent<TComponent>() where TComponent : IComponent, new()
	{
		AddComponent(new TComponent());
	}

	public bool RemoveComponent<TComponent>()
	{
		return _componentEntries.Remove(typeof(TComponent));
	}

	public TComponent GetComponent<TComponent>()
	{
		var componentType = typeof(TComponent);

		if (!_componentEntries.ContainsKey(componentType))
			return default;

		var component = _componentEntries[componentType].Component;

		if (component is not TComponent)
			throw new InvalidOperationException($"Type '{component.GetType()}' can not be cast to " +
			                                    $"type '{component}'. This should not happen.");

		return (TComponent)component;
	}
	
	public TComponent GetComponentOf<TComponent>()
	{
		foreach (var entry in _componentEntries.Values)
		{
			if (entry.Component is TComponent component)
				return component;
		}
		
		return default;
	}

	public List<TComponent> GetComponentsOf<TComponent>()
	{
		var list = new List<TComponent>();

		foreach (var entry in _componentEntries.Values)
		{
			if(entry.Component is TComponent component)
				list.Add(component);
		}

		return list;
	}

	public void PropagateEvent(Event e)
	{
		foreach (var entry in _componentEntries.Values)
		{
			var eventType = e.GetType();
			
			if (!entry.Events.ContainsKey(eventType))
				continue;

			entry.Events[eventType](e);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool WillRespondTo<TEvent>() where TEvent : Event => WillRespondTo(typeof(TEvent));

	public bool WillRespondTo(Type eventType)
	{
		foreach (var entry in _componentEntries.Values)
		{
			if (entry.Events.ContainsKey(eventType))
				return true;
		}

		return false;
	}

	public void RegisterEvent<TEvent>(IComponent component, ComponentEvent<TEvent> componentEvent) where TEvent : Event
	{
		var componentType = component.GetType();

		if (!_componentEntries.ContainsKey(componentType))
			return;

		var entry = _componentEntries[componentType];

		var eventType = typeof(TEvent);

		if (entry.Events.ContainsKey(eventType))
			return;
		
		entry.Events.Add(eventType, e => componentEvent(e as TEvent));
	}

	private void InternalAddComponent(IComponent component)
	{
		var componentType = component.GetType();

		if (_componentEntries.ContainsKey(componentType))
			return;

		var entry = new ComponentEntry
		{
			Component = component,
			Info = component.QueryComponentInfo(),
			Events = new Dictionary<Type, ComponentEventWrapper>()
		};
		
		_componentEntries.Add(componentType, entry);
	}

	public SerializationState GetObjectState()
	{
		var state = new SerializationState(this);
		var componentList = new List<SerializationState>(_componentEntries.Count);

		foreach (var entry in _componentEntries.Values)
			componentList.Add(entry.Component.GetObjectState());

		state.AddValue("components", componentList);
		
		return state;
	}

	public void SetObjectState(SerializationState state)
	{
		foreach (var entry in state.GetValue<List<SerializationState>>("components"))
		{
			var component = entry.Instantiate<IComponent>(init: false);
			InternalAddComponent(component);
			component.Initialize(this);
			component.SetObjectState(entry);
		}
	}
	
	private struct ComponentEntry
	{
		public IComponent Component;
		public ComponentInfoQuery Info;
		public Dictionary<Type, ComponentEventWrapper> Events;
	}
}