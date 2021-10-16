using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
	public class MovementPart : Part, IMovement
	{
		public int Speed { get; set; }

		public MovementPart()
		{
			RegisterEvent<GameEventTest>(Test);
			RegisterEvent<ConsoleWriteMessageEvent>(WriteMessage);
		}

		public void Test(GameEventTest e)
		{
			Console.WriteLine("it worked!");
		}

		public void WriteMessage(ConsoleWriteMessageEvent e)
		{
			Console.WriteLine(e.Message);
		}

		public override ComponentInfo QueryComponentInfo()
		{
			return new ComponentInfo
			{
				RegistryName = "hedgemen:movement_part",
				AccessType = typeof(IMovement)
			};
		}
	}
}