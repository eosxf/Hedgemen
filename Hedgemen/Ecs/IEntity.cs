using System;

namespace Hgm.Ecs;

public interface IEntity
{
	/// <summary>
	/// Handles a current Event if able to.
	/// </summary>
	/// <param name="e">
	/// Event for the entity to handle. Event contains a Handled field that is true if any Component
	/// handles the event. Should not be tweaked manually.
	/// </param>
	/// <typeparam name="TEvent">Event Type to handle.</typeparam>
	/// <returns></returns>
	public TEvent Propagate<TEvent>(TEvent e) where TEvent : GameEvent;

	/// <summary>
	/// Returns whether or not the entity will handle the given event if propagated.
	/// </summary>
	/// <param name="e">Event to be queried.</param>
	/// <returns></returns>
	public bool WillRespondTo(GameEvent e);

	/// <summary>
	/// Returns whether or not the entity will handle the given event if propagated.
	/// </summary>
	/// <typeparam name="TEvent">Event type to be queried.</typeparam>
	/// <returns></returns>
	public bool WillRespondTo<TEvent>() where TEvent : GameEvent;

	/// <summary>
	/// Returns whether or not the entity will handle the given event if propagated.
	/// </summary>
	/// <param name="eventType">Event type to be queried.</param>
	/// <returns></returns>
	public bool WillRespondTo(Type eventType);

	// todo document these
	public void AddComponent(IComponent component);
	public T GetComponent<T>() where T : class;
	public bool HasComponent<T>() where T : class;
}