using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hgm.Ecs.Serialization;

namespace Hgm.Ecs
{
	/// <summary>
	/// Component for Cell Entities.
	/// </summary>
	public abstract class Expanse : IComponent<Cell>
	{
		private Cell self;
		public Cell Self => self;

		public void InitializeFromDefault()
		{
			
		}

		public void InitializeFromSchema()
		{
			
		}

		public bool IsActive { get; set; } = true;

		public bool HandleEvent(GameEvent e)
		{
			return false;
		}

		public bool IsEventRegistered<TEvent>() where TEvent : GameEvent
		{
			return false;
		}

		public bool IsEventRegistered(Type eventType)
		{
			return false;
		}

		public abstract ComponentInfo QueryComponentInfo();

		public void RegisterEvent<TEvent>(EventWrapper<TEvent> e) where TEvent : GameEvent
		{
			
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