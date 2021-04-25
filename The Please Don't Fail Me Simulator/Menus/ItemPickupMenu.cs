using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Items;

namespace The_Please_Dont_Fail_Me_Simulator.Menus
{
    public class ItemPickupMenu : TextMenu
    {
        private Item Item;

        public ItemPickupMenu(Item item)
        {
            this.Item = item;
        }

        public override string ToString()
        {
            string menu = "╔═══════════════════════╗";
            menu += "\n║       New Item!       ║";
            menu += "\n║                       ║";
            menu += "\n║" + AlignCenter(Item.Name, 0) + "║";
            menu += "\n║                       ║";

            if (this.Item is Consumable)
            {
                Consumable consumable = (Consumable)Item;
                menu += "\n║" + AlignCenter("Health Boost: " + consumable.HealthBoost, 0) + "║";
                menu += "\n║                       ║";
            }
            else if (this.Item is Weapon)
            {
                Weapon weapon = (Weapon)Item;
                menu += "\n║" + AlignCenter("Attack Boost: " + weapon.AttackBoost, 0) + "║";
                menu += "\n║" + AlignCenter("Defense Boost: " + weapon.DefenseBoost, 0) + "║";
            }

            menu += "\n║                       ║";
            menu += "\n║                       ║";
            menu += "\n║                       ║";
            menu += "\n║                       ║";
            menu += "\n║                       ║";
            menu += "\n╚═══════════════════════╝";

            return menu;
        }
    }

    
}
