using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
    public interface IEntity
    {
        public bool HandleEvent(GameEvent e);
    }
}