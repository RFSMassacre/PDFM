using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Maps;

namespace The_Please_Dont_Fail_Me_Simulator.Menus
{
    public class MainMenu : TextMenu
    {
        private int Selection;

        public MainMenu(int selection)
        {
            this.Selection = selection;
        }

        public override string ToString()
        {
            string[] selections = new string[2] { "New Game", "Load Game" };
            string menu = "╔═══════════════════════╗";
            menu += "\n║ The Please Don't Fail ║";
            menu += "\n║     Me Simulator      ║";
            menu += "\n║                       ║";
            menu += "\n║                       ║";

            for (int y = 0; y < 8; y++)
            {
                try
                {
                    if (this.Selection == y)
                    {
                        menu += "\n║ >" + AlignCenter(selections[y], 2) + "║";
                    }
                    else
                    {
                        menu += "\n║" + AlignCenter(selections[y], 0) + "║";
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    menu += "\n║" + AlignCenter("", 0) + "║";
                }
            }

            return menu += "\n╚═══════════════════════╝";
        }
    }
}
