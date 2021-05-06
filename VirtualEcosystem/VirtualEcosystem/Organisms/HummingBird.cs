using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    class HummingBird : Organism
    {

        static Random rng = new System.Random();

        private World Desert;
        public HummingBird(string name, World world)
            : base(name, world)
        {
            Desert = world;
        }


        public override void SurvivalAction()
        {
            if (rng.Next(1, 3) == 1)
            {
                if (rng.Next(1, 3) == 1)
                {
                    try 
                    { 
                        Desert.organisms.OfType<Plant>().First().Die();
                        Desert.consoleLog.Add(" ~~Plant Eaten by Hummingbird~~ ");
                    }
                    catch (System.InvalidOperationException)
                    {
                        Die();
                    }
            }
                else
                {
                    try 
                    {                    
                        Desert.organisms.OfType<Insect>().First().Die();
                        Desert.consoleLog.Add(" ~~Insect Eaten by Hummingbird~~ ");
                    }
                    catch (System.InvalidOperationException)
                    {
                        Die();
                    }
                }
            }        
        }
    }
}
