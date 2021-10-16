using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
    public class EatFoodEvent : GameEvent
    {
        public string FoodName { get; set; }

        public EatFoodEvent(string foodName)
        {
            FoodName = foodName;
        }
    }
}