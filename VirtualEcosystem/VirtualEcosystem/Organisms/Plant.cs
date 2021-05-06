using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    class Plant: Organism
    {
        static Random rng = new System.Random();

        private World Desert;


        public Plant(string name, World world)
            :base(name, world)
        {
            //orgType = OrgType.Plant;
            Desert = world;
        }


        public override void SurvivalAction()
        {
            if (Viable == false)
            {
                SurvivalStat++;
            }
            else
            {
                Die();
                Desert.consoleLog.Add(" --Plant Died: No Pollination-- ");
            }
        }
        public override void CheckEnvironmentTemp()
        {
            if (TemperatureSystem.currentTemp < 16)  
            {
                Desert.consoleLog.Add(" --Plant Died: Too cold-- ");
                Die();
            }
        }
        public override void DropSeeds()
        {
            if (rng.Next(1, 11) < 4)
            {
                Desert.Seeds++;
            }
        }

    }
}
