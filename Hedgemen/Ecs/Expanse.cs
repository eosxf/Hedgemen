using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
    public class Expanse : IComponent<Cell>
    {
        private Cell self;
        public Cell Self => self;

        public bool HandleEvent(GameEvent e)
        {
            return false;
        }
    }
}