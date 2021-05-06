using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VirtualEcosystem.DaySystem;

namespace VirtualEcosystem
{

    //CREDITS
    //   - All code done by Saidazizkhon Saydaminov
    //   - April 25th 2021
    //   - Title > A Hard Year
    class World
    {

        
        

        public int canMate;
        public int canMultiply;

        private int nameNumI = 2;
        private int nameNumP = 2;
        private int nameNumA = 0;
        private int nameNumS = 0;
        private int nameNumH = 0;

        public int treeDensity = 0;


        public List<Organism> organisms = new List<Organism>();
        public List<Organism> deadList = new List<Organism>();
        public Player player = new Player();
        public Shop Store;

        public List<string> consoleLog = new List<string>();

        private Random migrateRNG = new Random();
        

        public int Seeds = 0;

        public void SetUpWorld()
        {
            
            organisms.Add(new Plant("┤ YuccaPlant", this));
            organisms.Add(new Plant("┤ YuccaPlant", this));
            organisms.Add(new Insect("╣ YuccaInsect", this));
            organisms.Add(new Insect("╣ YuccaInsect", this));
            Store = new Shop(this);

        } 
        public void NewNight()
        {
            ClearCorpse();
            EatCycle();
            OrganismTempCheck();
            ClearCorpse();




            //if survival stat is 5, reset survival stat, add new insect, add organism insect into list. 
            foreach (Organism item in organisms)
            {
                if (item is Insect)
                {
                    if (item.SurvivalStat == 2)
                    {
                        item.SurvivalStat = 0;
                        canMate++;
                    }
                }
            }

            foreach (Organism item in organisms)
            {
                if (item is Plant)
                {
                    if (item.SurvivalStat == 4)
                    {
                        item.SurvivalStat = 0;
                        canMultiply++;
                    }
                    item.DropSeeds();
                }
            }

            if (canMultiply > 0 && canMultiply % 2 == 0)
            {
                int i = 0;
                while (i < ((canMate/2)+1))
                {
                    organisms.Add(new Plant("┤ YuccaPlant", this));
                    canMultiply = 0;
                    i++;
                    nameNumP++;
                }

            }

            if (canMate > 0 && canMate % 2 == 0)
            {
                int i = 0;
                while (i < ((canMate / 2) + 1))
                {
                    organisms.Add(new Insect("╣ YuccaInsect", this));
                    canMate = 0;
                    i++;
                    nameNumI++;
                }

            }
        } //Called at the end of every day
        public void NewDay()
        {
            if (DaySystem.Migration)
            {
                MigrateSeason();
            }          
        } // Called at the end of every day

        private void EatCycle()
        {
            foreach (Organism item in organisms)
            {
                if (item is Insect)
                {
                    item.SurvivalAction();
                }
            }

            foreach (Organism item in organisms)
            {
                if (item is Plant)
                {
                    item.SurvivalAction();
                }
            }


            foreach (Organism item in organisms)
            {
                if (item is Armadillo || item is Sparrow || item is HummingBird)
                {
                    item.SurvivalAction();
                }
            }
        }
        public void ClearCorpse()
        {
            int i = 0;
            while (i < deadList.Count)
            {
                organisms.Remove(deadList[i++]);
            }
        }
        public void ResetViability()
        {
            foreach (Organism item in organisms)
            {
                if (item is Plant)
                {
                    item.Viable = true;
                }
            }
        }
        private void OrganismTempCheck()
        {
            foreach (Organism item in organisms)
            {
               
                item.CheckEnvironmentTemp();
                
            }
        }
        private void MigrateSeason()
        {
            int rng = migrateRNG.Next(1, 4);
            string name;
            switch (rng)
            {
                case 1:
                    name = "Armadillo" + (nameNumA + 1);
                    organisms.Add(new Armadillo(name, this));
                    nameNumA++;
                        break;
                case 2:
                    name = "Sparrow" + (nameNumS + 1);
                    organisms.Add(new Sparrow(name, this));
                    nameNumS++;
                    break;
                case 3:
                    name = "Hummingbird" + (nameNumH + 1);
                    organisms.Add(new HummingBird(name, this));
                    nameNumH++;
                    break;
                default:
                    break;
            }
        }
       


        //Using functions from hotbar/inventory
        public void SeedPlant(Item selectedItem)
        {
            organisms.Add(new Plant("┤ YuccaPlant", this));
            consoleLog.Add(" ++New Plant: Seed Planted++ ");
            player.Inventory[selectedItem]--;
        }
        public void TrapSparrow(Item selectedItem)
        {
            foreach (Organism item in organisms)
            {
                if (item is Sparrow)
                {
                    consoleLog.Add(" ==Sparrow Died: Killed by trap== ");
                    item.Die();
                    player.Inventory[selectedItem]--;
                    break;
                }
            }
        }
        public void TrapBird(Item selectedItem)
        {
            foreach (Organism item in organisms)
            {
                if (item is HummingBird)
                {
                    consoleLog.Add(" ==Hummingbird Died: Killed by trap== ");
                    item.Die();
                    player.Inventory[selectedItem]--;
                    break;
                }
            }
        }
        public void TrapArmadillo(Item selectedItem)
        {
            foreach (Organism item in organisms)
            {
                if (item is Armadillo)
                {
                    consoleLog.Add(" ==Armadillo Died: Killed by trap== ");
                    item.Die();
                    player.Inventory[selectedItem]--;
                    break;
                }
            }
        }


        





    }
}
