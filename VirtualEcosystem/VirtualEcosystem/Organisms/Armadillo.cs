using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    class Armadillo : Organism
    {
        static Random rng = new System.Random();

        private World Desert;
        public Armadillo(string name, World world)
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
                    Desert.organisms.OfType<Insect>().First().Die();
                    Desert.consoleLog.Add(" ~~Insect Eaten by Armadillo~~ ");
                }
                catch (System.InvalidOperationException)
                {
                    Die();
                }
            }
        }
    }
}
