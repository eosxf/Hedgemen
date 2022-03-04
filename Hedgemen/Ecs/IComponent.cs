using System;
using Hgm.Utilities;

namespace Hgm.Ecs;

/// <summary>
/// Composition focused objects for <see cref="Hgm.Ecs.IEntity" /> that can handle events and contain data.
/// </summary>
public interface IComponent
{
	public bool IsActive { get; set; }
	public void Initialize();
	public bool HandleEvent(GameEvent e);
	public void RegisterEvent<TEvent>(ComponentEvent<TEvent> e) where TEvent : GameEvent;
	public bool IsEventRegistered<TEvent>() where TEvent : GameEvent;
	public bool IsEventRegistered(Type eventType);
	public ComponentInfo QueryComponentInfo();
	public void AttachEntity(Entity entity);
	public void OnEventPropagated(GameEvent gameEvent);
}