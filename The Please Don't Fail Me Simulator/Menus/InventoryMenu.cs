using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Items;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Menus
{
    public class InventoryMenu : TextMenu
    {
        private Player Player;
        private int Selection;

        public InventoryMenu(Player player, int selection)
        {
            this.Player = player;
            this.Selection = selection;
        }

        public override string ToString()
        {
            string menu = "╔═══════════════════════╗";
            menu += "\n║       Inventory       ║";
            menu += "\n║                       ║";

            for (int y = 0; y < 10; y++)
            {
                try
                {
                    Item item = Item.GenerateItem(Player.ItemNames[y]);
                    if (this.Selection == y)
                    {
                        menu += "\n║ >" + AlignCenter(item.Name, 2) + "║";
                    }
                    else
                    {
                        menu += "\n║" + AlignCenter(item.Name, 0) + "║";
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    menu += "\n║" + AlignCenter("", 0) + "║";
                }
            }

            menu += "\n╚═══════════════════════╝";

            return menu;
        }
    }
}
