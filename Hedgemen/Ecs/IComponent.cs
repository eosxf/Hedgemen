using System;
using Hgm.Ecs.Serialization;

namespace Hgm.Ecs;

public delegate void EventWrapper<TEvent>(TEvent e) where TEvent : GameEvent;

/// <summary>
/// Composition focused objects for <see cref="Hgm.Ecs.IEntity"/> that can handle events and contain data.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IComponent<out TEntity> :  ISerializableInfo where TEntity : IEntity
{
	public void InitializeFromDefault();
	public void InitializeFromSchema(); // todo add schema class
	public bool IsActive { get; set; }
	public bool HandleEvent(GameEvent e);
	public void RegisterEvent<TEvent>(EventWrapper<TEvent> e) where TEvent : GameEvent;
	public bool IsEventRegistered<TEvent>() where TEvent: GameEvent;
	public bool IsEventRegistered(Type eventType);
	public ComponentInfo QueryComponentInfo();
}