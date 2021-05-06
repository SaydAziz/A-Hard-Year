using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    class Insect: Organism
    {
        private World Desert;   
        public Insect(string name, World world)
            : base(name, world)
        {
            Desert = world;
        }


        public override void SurvivalAction()
        {
            int i = 0;
            while (true)
            {
                try
                {
                    if (Desert.organisms[i] is Plant && Desert.organisms[i].Viable == true)
                    {
                        SurvivalStat++;
                        Desert.organisms[i].Viable = false;
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    Die();
                    Desert.consoleLog.Add(" --Insect Died: No Nectar-- ");
                    break;
                }
                
            }
        }
        public override void CheckEnvironmentTemp()
        {
            if (TemperatureSystem.currentTemp > 31)
            {
                Die();
            }
        }
    }
}
