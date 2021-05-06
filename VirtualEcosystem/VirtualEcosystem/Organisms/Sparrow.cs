using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    class Sparrow : Organism
    {
        static Random rng = new System.Random();

        private World Desert;
        public Sparrow(string name, World world)
            : base(name, world)
        {
            Desert = world;
        }


        public override void SurvivalAction()
        {
            if (rng.Next(1, 3) == 1)
            {
                try
                {
                    Desert.organisms.OfType<Plant>().First().Die();
                    Desert.consoleLog.Add(" ~~Plant Eaten by Sparrow~~ ");
                }
                catch (System.InvalidOperationException)
                {
                    Die();
                }
                               
            }
        }
    }
}
