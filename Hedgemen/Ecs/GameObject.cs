using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hgm.Utilities;

namespace Hgm.Ecs
{
	public delegate void PartEvent(GameEvent e);

	public class GameObject : IEntity
	{
		private IDictionary<Type, Part> parts = new CachedDictionary<Type, Part>();

		public GameObject()
		{
			
		}

		public void AddPart(Part part)
		{
			var infoQuery = part.QueryComponentInfo();
			parts.Add(infoQuery.AccessType, part);
		}

		public T GetPart<T>() where T : class
		{
			return parts.Get(typeof(T)) as T;
		}

		public TEvent HandleEvent<TEvent>(TEvent e) where TEvent : GameEvent
		{
			if (!WillRespondToEvent<TEvent>()) return e;
			
			foreach(var part in parts.Values)
			{
				bool result = part.HandleEvent(e);
				if (result) e.Handled = true;
			}
			
			return e;
		}

		public bool HasPart<T>() where T : class
		{
			return parts.ContainsKey(typeof(T));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool WillRespondToEvent(GameEvent e)
		{
			return WillRespondToEvent(e.GetType());
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool WillRespondToEvent<TEvent>() where TEvent : GameEvent
		{
			return WillRespondToEvent(typeof(TEvent));
		}

		public bool WillRespondToEvent(Type eventType)
		{
			foreach(var part in parts.Values)
			{
				if(part.IsEventRegistered(eventType))
					return true;
			}

			return false;
		}
	}
}