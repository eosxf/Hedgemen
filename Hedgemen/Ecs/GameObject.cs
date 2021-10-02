using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
    public delegate void PartEvent(GameEvent e);

    public class GameObject : IEntity
    {
        public PartEvent partEvent;

        public GameObject()
        {
            partEvent = delegate(GameEvent e) { Test(e as GameEventTest); };
        }

        public void Do()
        {
            partEvent(new GameEventTest());
        }

        public bool HandleEvent(GameEvent e)
        {
            

            return false;
        }

        public void Test(GameEventTest e)
        {
            Console.WriteLine("Hello!");
        }
    }
}