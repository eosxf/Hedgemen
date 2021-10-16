using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
	public interface IMovement
	{
		public int Speed { get; set; }
		public void WriteMessage(ConsoleWriteMessageEvent e);
	}
}