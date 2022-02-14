using System;
using Hgm.Ecs.Serialization;

namespace Hgm.Ecs
{
	public class Cell : IEntity
	{
		public TEvent HandleEvent<TEvent>(TEvent e) where TEvent : GameEvent
		{
			return e;
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

		public SerializedInfo GetSerializedInfo()
		{
			throw new NotImplementedException();
		}

		public void ReadSerializedInfo(SerializedInfo info)
		{
			throw new NotImplementedException();
		}
	}
}