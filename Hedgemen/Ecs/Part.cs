using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
	public abstract class Part : IComponent<GameObject>
	{
		private GameObject self;
		public GameObject Self => self;

		public bool IsActive { get; set; } = true;

		private IDictionary<Type, PartEvent> registeredEvents = new Dictionary<Type, PartEvent>();

		public bool HandleEvent(GameEvent e)
		{
			if(!registeredEvents.ContainsKey(e.GetType())) return false;

			registeredEvents[e.GetType()](e);

			return true;
		}

		public void RegisterEvent<TEvent>(EventWrapper<TEvent> e) where TEvent : GameEvent
		{
			if(e == null)
				throw new Exception("Registered events cannot be null!");

			if(registeredEvents.ContainsKey(typeof(TEvent)))
				throw new Exception($"Event type {typeof(TEvent).FullName} already registered.");
			
			registeredEvents.Add(typeof(TEvent), (handle) => { e(handle as TEvent); });
		}

		public abstract ComponentInfo QueryComponentInfo();

		public bool IsEventRegistered<TEvent>() where TEvent : GameEvent
		{
			return IsEventRegistered(typeof(TEvent));
		}

		public bool IsEventRegistered(Type eventType)
		{
			return registeredEvents.ContainsKey(eventType);
		}
	}
}