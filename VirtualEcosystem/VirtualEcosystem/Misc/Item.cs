using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEcosystem
{
    class Item
    {
        public string Name;
        public int ItemID;
        public int Price;

        public Item(string name, int itemID, int price)
        {
            Name = name;
            ItemID = itemID;
            Price = price;
        }


       

    }
}
