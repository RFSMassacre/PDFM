using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Maps;

namespace The_Please_Dont_Fail_Me_Simulator.Menus
{
    public class BattleMenu : TextMenu
    {
        public BattleMenu()
        {

        }

        public override string ToString()
        {
            string menu = "";

            //Top border.
            for (int horizontal = 0; horizontal < 22; horizontal++)
            {
                if (horizontal == 0)
                {
                    menu += "╔";
                }
                else if (horizontal == 22 - 1)
                {
                    menu += "╗";
                }
                else
                {
                    menu += "═";
                }

            }

            //Middle borders.
            for (int vertical = 0; vertical < 10; vertical++)
            {
                menu += "\n";
                for (int horizontal = 0; horizontal < 22; horizontal++)
                {
                    if (horizontal == 0 || horizontal == 22 - 1)
                    {
                        menu += "║";
                    }
                    else
                    {
                        menu += " ";
                    }
                }
            }

            //Bottom border.
            for (int horizontal = 0; horizontal < 22; horizontal++)
            {
                if (horizontal == 0)
                {
                    menu += "\n╚";
                }
                else if (horizontal == 22 - 1)
                {
                    menu += "╝";
                }
                else
                {
                    menu += "═";
                }
            }

            return menu;
        }
    }
}
