using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    class Organism
    {

        public string Name;
        public int SurvivalStat = 0;
        public bool Viable = true;
        public int Population = 2;
        private World Desert;

        public Organism(string name, World world)
        {
            Name = name;
            Desert = world;
        }


        //Examples: Eating, Polinating
        public virtual void SurvivalAction()
        {
            
        }
        public virtual void CheckEnvironmentTemp()
        {

        }
        public virtual void DropSeeds()
        {

        }
        public void Die()
        {
            Desert.deadList.Add(this);
        }
    }
}
