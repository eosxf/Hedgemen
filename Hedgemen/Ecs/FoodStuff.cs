using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hgm.Ecs
{
	public class FoodStuff : Part, IFood
	{
		public int HealingFactor { get; set; } = 1;

        public FoodStuff()
        {
            RegisterEvent<EatFoodEvent>(EatFood);
        }

		public override ComponentInfo QueryComponentInfo()
		{
			return new ComponentInfo
            {
                RegistryName = "hedgemen:foodstuff",
                AccessType = typeof(IFood)
            };
		}

        public void EatFood(EatFoodEvent e)
        {
            Console.WriteLine($"Eating " + e.FoodName + "!");
        }
	}
}