using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    static class WeatherModifier
    {
        public static void Heat(World world)
        {

            if (world.player.FireWood > 0)
            {
                world.player.FireWood -= 1;
                TemperatureSystem.currentTemp++;
            }
        }

        public static void Cool(World world)
        {


            if (world.player.FireWood > 0)
            {
                world.player.FireWood -= 1;
                TemperatureSystem.currentTemp--;
            }
        }
    }
}
