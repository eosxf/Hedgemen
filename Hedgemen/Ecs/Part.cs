using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
    public class Part : IComponent<GameObject>
    {
        private GameObject self;
        public GameObject Self => self;

        public bool HandleEvent(GameEvent e)
        {
            return false;
        }
    }
}