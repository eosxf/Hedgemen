using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
    public interface IComponent<TEntity> where TEntity : IEntity
    {
        public TEntity Self { get; }
        public bool HandleEvent(GameEvent e);
    }
}