using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
	public interface IEntity
	{
		public TEvent HandleEvent<TEvent>(TEvent e) where TEvent : GameEvent;
		public bool WillRespondToEvent(GameEvent e);
		public bool WillRespondToEvent<TEvent>() where TEvent : GameEvent;
		public bool WillRespondToEvent(Type eventType);
	}
}