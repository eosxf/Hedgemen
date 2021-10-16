using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
	public delegate void EventWrapper<TEvent>(TEvent e) where TEvent : GameEvent;

	public interface IComponent<TEntity> where TEntity : IEntity
	{
		public TEntity Self { get; }
		public bool IsActive { get; set; }
		public bool HandleEvent(GameEvent e);
		public void RegisterEvent<TEvent>(EventWrapper<TEvent> e) where TEvent : GameEvent;
		public bool IsEventRegistered<TEvent>() where TEvent: GameEvent;
		public bool IsEventRegistered(Type eventType);
		public ComponentInfo QueryComponentInfo();
	}
}