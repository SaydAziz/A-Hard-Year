using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    class Shop
    {
       
        public int SelectedItem;
        public Item[] ShopContent;

        private World desert;
        public Shop(World world)
        {
            desert = world;
            ShopContent = new Item[] { desert.player.Seed, desert.player.lvl2Axe , desert.player.lvl3Axe, desert.player.SparrowTrap, desert.player.BirdTrap, desert.player.ArmadilloTrap};
        }



        public void Buy()
        {
            if (desert.player.Coin >= ShopContent[SelectedItem].Price)
            {
                desert.player.Coin -= ShopContent[SelectedItem].Price;
                desert.player.Inventory[ShopContent[SelectedItem]] += 1;

            }
            

        }

        public void Sell()
        {
            try
            {
                desert.player.Inventory.TryGetValue(ShopContent[SelectedItem], out int amount);

                if (amount > 0)
                {
                    desert.player.Coin += ShopContent[SelectedItem].Price;
                    desert.player.Inventory[ShopContent[SelectedItem]] -= 1;
                }
            }
            catch
            {

            }
            
            
        }
    }
}
