using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hgm.IO.Serialization;

namespace Hgm.Ecs
{
	/// <summary>
	/// Component class for <see cref="Hgm.Ecs.Entity"/>
	/// </summary>
	public abstract class Part : IComponent
	{
		private Entity self;
		public Entity Self => self;

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

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void PartEvent(GameEvent handle) => e(handle as TEvent);

			registeredEvents.Add(typeof(TEvent), PartEvent);
		}

		public virtual void OnEventPropagated(GameEvent gameEvent)
		{
        
		}

		public abstract ComponentInfo QueryComponentInfo();
		public virtual SerializedInfo GetSerializedInfo()
		{
			return new SerializedInfo(this, new SerializedFields());
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