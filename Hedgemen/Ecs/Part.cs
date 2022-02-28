using System;
using System.Collections.Generic;
using Hgm.Ecs.Text;
using Hgm.IO.Serialization;

namespace Hgm.Ecs;

/// <summary>
/// Component class for <see cref="Hgm.Ecs.Entity" />
/// </summary>
public abstract class Part : IComponent
{
	private readonly IDictionary<Type, PartEventWrapper> registeredEvents = new Dictionary<Type, PartEventWrapper>();
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

	public void RegisterEvent<TEvent>(PartEvent<TEvent> e) where TEvent : GameEvent
	{
		if (e == null)
			throw new Exception("Registered events cannot be null!");

		if (registeredEvents.ContainsKey(typeof(TEvent)))
			throw new Exception($"Event type {typeof(TEvent).FullName} already registered.");

		void PartEvent(GameEvent handle)
		{
			e(handle as TEvent);
		}

		registeredEvents.Add(typeof(TEvent), PartEvent);
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
			throw new InvalidOperationException($"An entity is already attached to part: {this}");
		Self = entity;
	}

	public virtual void OnEventPropagated(GameEvent gameEvent)
	{
	}
}