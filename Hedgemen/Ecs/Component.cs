using System;
using System.Collections.Generic;
using Hgm.Ecs.Text;
using Hgm.IO.Serialization;

namespace Hgm.Ecs;

/// <summary>
/// Component class for <see cref="Hgm.Ecs.Entity" />
/// </summary>
public abstract class Component : IComponent
{
	private readonly IDictionary<Type, ComponentEventWrapper> registeredEvents = new Dictionary<Type, ComponentEventWrapper>();
	public Entity Self { get; private set; }

	public virtual void InitializeFromSchema(ComponentSchema schema)
	{
		InitializeFromFields(schema.Fields);
	}

	public virtual void InitializeFromFields(SerializedFields fields)
	{
	}

	public bool IsActive { get; set; } = true;

	public bool HandleEvent(GameEvent e)
	{
		if (!registeredEvents.ContainsKey(e.GetType())) return false;
		registeredEvents[e.GetType()](e);
		OnEventPropagated(e);
		return true;
	}

	public void RegisterEvent<TEvent>(ComponentEvent<TEvent> e) where TEvent : GameEvent
	{
		if (e == null)
			throw new Exception("Registered events cannot be null!");

		if (registeredEvents.ContainsKey(typeof(TEvent)))
			throw new Exception($"Event type {typeof(TEvent).FullName} already registered.");

		void ComponentEvent(GameEvent handle)
		{
			e(handle as TEvent);
		}

		registeredEvents.Add(typeof(TEvent), ComponentEvent);
	}

	public abstract ComponentInfo QueryComponentInfo();

	public virtual SerializedInfo GetSerializedInfo()
	{
		return new SerializedInfo(this, new SerializedFields());
	}

	public virtual void ReadSerializedInfo(SerializedInfo info)
	{
		InitializeFromFields(info.Fields);
	}

	public bool IsEventRegistered<TEvent>() where TEvent : GameEvent
	{
		return IsEventRegistered(typeof(TEvent));
	}

	public bool IsEventRegistered(Type eventType)
	{
		return registeredEvents.ContainsKey(eventType);
	}

	public void AttachEntity(Entity entity)
	{
		if (Self != null)
			throw new InvalidOperationException($"An entity is already attached to component: {this}");
		Self = entity;
	}

	public virtual void OnEventPropagated(GameEvent gameEvent)
	{
	}
}