using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
	public class Cell : IEntity
	{
		public bool HandleEvent(GameEvent e)
		{
			return false;
		}

		public bool HasPart<T>() where T : class
		{
			throw new NotImplementedException();
		}

		public bool WillRespondToEvent(GameEvent e)
		{
			throw new NotImplementedException();
		}

		public bool WillRespondToEvent<TEvent>() where TEvent : GameEvent
		{
			throw new NotImplementedException();
		}

		public bool WillRespondToEvent(Type eventType)
		{
			throw new NotImplementedException();
		}
	}
}