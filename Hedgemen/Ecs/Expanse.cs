using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
	public abstract class Expanse : IComponent<Cell>
	{
		private Cell self;
		public Cell Self => self;

		public bool IsActive { get; set; } = true;

		public bool HandleEvent(GameEvent e)
		{
			return false;
		}

		public bool IsEventRegistered<TEvent>() where TEvent : GameEvent
		{
			throw new NotImplementedException();
		}

		public bool IsEventRegistered(Type eventType)
		{
			throw new NotImplementedException();
		}

		public abstract ComponentInfo QueryComponentInfo();

		public void RegisterEvent<TEvent>(EventWrapper<TEvent> e) where TEvent : GameEvent
		{
			
		}
	}
}