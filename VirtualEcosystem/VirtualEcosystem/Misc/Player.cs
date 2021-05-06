using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    class Player : IPlayerInterface
    {
        public int Coin;


        public Dictionary<Item, int> Inventory;
        public List<Item> Hotbar = new List<Item>();

        public int FireWood = 0;
        public int axeLVL = 0;


        public Item Seed = new Item("Seed", 1, 15);
        public Item SparrowTrap = new Item("Sparrow Trap", 2, 15);
        public Item BirdTrap = new Item("Bird Trap", 3, 15);
        public Item ArmadilloTrap = new Item("Armadillo Trap", 4, 15);
        

        //will upgrade axe in the shop to chop denser trees
        public Item lvl1Axe = new Item("Axe (LVL1)", 10, 300);
        public Item lvl2Axe = new Item("Axe (LVL2)", 10, 100);
        public Item lvl3Axe = new Item("Axe (LVL3)", 10, 150);

        public Player() 
        {
            Coin = 0;
            Inventory = new Dictionary<Item, int>();
            Inventory.Add(Seed, 0);
            Inventory.Add(SparrowTrap, 0);
            Inventory.Add(BirdTrap, 0);
            Inventory.Add(ArmadilloTrap, 0);
        }

        int chop = 0;
        public int broke = 0;
        public bool Chop(int tree)
        {
            if (axeLVL >= tree)
            {
                chop++;
                if (chop == 5)
                {
                    FireWood++;
                    chop = 0;
                    return true;
                }
                return false;
            }

            broke++;
            return false;

        }

        public void InvToHBcheck()
        {
            foreach (KeyValuePair<Item, int> item in Inventory)
            {
                    if (item.Value < 1 && Hotbar.Contains(item.Key))
                    {
                        Hotbar.Remove(item.Key);
                    }
                    else if (item.Value < 1 && !Hotbar.Contains(item.Key))
                    {
                    }
                    else if (item.Value > 0 && !Hotbar.Contains(item.Key))
                    {
                        Hotbar.Add(item.Key);
                    }
                    else if (item.Value > 0 && Hotbar.Contains(item.Key))
                    { 
                    }
                    
                            
            }

            
        }




    }
}
