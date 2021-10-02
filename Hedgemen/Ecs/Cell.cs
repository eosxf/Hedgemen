using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
    public class Cell : IEntity
    {
        public bool HandleEvent(GameEvent e)
        {
            return false;
        }
    }
}