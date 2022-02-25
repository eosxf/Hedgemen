using System;
using Hgm.Ecs.Text;
using Hgm.IO.Serialization;

namespace Hgm.Ecs;

public delegate void EventWrapper<TEvent>(TEvent e) where TEvent : GameEvent;

/// <summary>
/// Composition focused objects for <see cref="Hgm.Ecs.IEntity"/> that can handle events and contain data.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IComponent :  ISerializableInfo
{
	public void InitializeFromSchema(ComponentSchema schema);
	public void InitializeFromFields(SerializedFields fields);
	public bool IsActive { get; set; }
	public bool HandleEvent(GameEvent e);
	public void RegisterEvent<TEvent>(EventWrapper<TEvent> e) where TEvent : GameEvent;
	public bool IsEventRegistered<TEvent>() where TEvent: GameEvent;
	public bool IsEventRegistered(Type eventType);
	public ComponentInfo QueryComponentInfo();
}