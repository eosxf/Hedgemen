using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
	public class ConsoleWriteMessageEvent : GameEvent
	{
		public string Message { get; }

		public ConsoleWriteMessageEvent(string message)
		{
			Message = message;
		}
	}
}