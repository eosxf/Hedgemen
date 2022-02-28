using System;
using Hgm.IO.Serialization;

namespace Hgm.Ecs;

public class Cell : IEntity
{
	public TEvent Propagate<TEvent>(TEvent e) where TEvent : GameEvent
	{
		return e;
	}

	public bool WillRespondTo(GameEvent e)
	{
		throw new NotImplementedException();
	}

	public bool WillRespondTo<TEvent>() where TEvent : GameEvent
	{
		throw new NotImplementedException();
	}

	public bool WillRespondTo(Type eventType)
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

	public bool HasPart<T>() where T : class
	{
		throw new NotImplementedException();
	}
}