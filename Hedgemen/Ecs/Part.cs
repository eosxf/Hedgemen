using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Hgm.Ecs.Serialization;

namespace Hgm.Ecs
{
	/// <summary>
	/// Component for GameObject entities.
	/// </summary>
	public abstract class Part : IComponent<GameObject>
	{
		private GameObject self;
		public GameObject Self => self;

		public virtual void InitializeFromDefault()
		{
			
		}

		public virtual void InitializeFromSchema()
		{
			
		}

		public bool IsActive { get; set; } = true;

		private IDictionary<Type, PartEvent> registeredEvents = new Dictionary<Type, PartEvent>();

		public bool HandleEvent(GameEvent e)
		{
			if(!registeredEvents.ContainsKey(e.GetType())) return false;
			registeredEvents[e.GetType()](e);
			OnEventPropagated(e);
			return true;
		}

		public void RegisterEvent<TEvent>(EventWrapper<TEvent> e) where TEvent : GameEvent
		{
			if(e == null)
				throw new Exception("Registered events cannot be null!");

			if(registeredEvents.ContainsKey(typeof(TEvent)))
				throw new Exception($"Event type {typeof(TEvent).FullName} already registered.");

			PartEvent partEvent = [MethodImpl(MethodImplOptions.AggressiveInlining)](handle) => { e(handle as TEvent); };
			registeredEvents.Add(typeof(TEvent), partEvent);
		}

		public virtual void OnEventPropagated(GameEvent gameEvent)
		{
        
		}

		public abstract ComponentInfo QueryComponentInfo();
		public virtual SerializedInfo GetSerializedInfo()
		{
			return new SerializedInfo(this);
		}

		public virtual void ReadSerializedInfo(SerializedInfo info)
		{
			
		}

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