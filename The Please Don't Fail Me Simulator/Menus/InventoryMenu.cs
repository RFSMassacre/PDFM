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
        private int Width;
        private int Selection;

        public InventoryMenu(Player player, int selection)
        {
            this.Player = player;
            this.Width = 23;
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
                    Item item = Player.Items[y];
                    if (this.Selection == y)
                    {
                        menu += "\n║ >" + AlignCenter(item.Name, this.Width - 2) + "║";
                    }
                    else
                    {
                        menu += "\n║" + AlignCenter(item.Name, this.Width) + "║";
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    menu += "\n║" + AlignCenter("", this.Width) + "║";
                }
            }

            menu += "\n╚═══════════════════════╝";

            return menu;
        }

        private string AlignCenter(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
