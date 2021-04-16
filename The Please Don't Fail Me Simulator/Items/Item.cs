using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Items
{
    public abstract class Item
    {
        public string Name { get; private set; }

        public Item(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return "Use this item? (Y / N)";
        }

        public abstract void Use(Player player);
    }
}
