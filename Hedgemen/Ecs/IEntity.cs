using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hgm.Ecs.Serialization;

namespace Hgm.Ecs;

public interface IEntity : ISerializableInfo
{
	/// <summary>
	/// Handles a current Event if able to.
	/// </summary>
	/// <param name="e">Event for the Entity to handle. Event contains a Handled field that is true if any Part handles the event. Should not be
	/// tweaked manually.</param>
	/// <typeparam name="TEvent">Event Type to handle.</typeparam>
	/// <returns></returns>
	public TEvent HandleEvent<TEvent>(TEvent e) where TEvent : GameEvent;
	public bool WillRespondToEvent(GameEvent e);
	public bool WillRespondToEvent<TEvent>() where TEvent : GameEvent;
	public bool WillRespondToEvent(Type eventType);
}