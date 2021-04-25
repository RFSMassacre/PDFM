using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Menus
{
    public class StatsMenu : TextMenu
    {
        private Player Player;

        public StatsMenu(Player player)
        {
            this.Player = player;
        }

        public override string ToString()
        {
            string menu = "╔═══════════════════════╗";
            menu += "\n║     Player Stats      ║";
            menu += "\n║" + AlignCenter("", 0) + "║";
            menu += "\n║ " + AlignCenter(this.Player.Name, 2) + " ║";
            menu += "\n║ " + AlignCenter("LVL: " + this.Player.Level, 2) + " ║";
            menu += "\n║ " + AlignCenter("EXP: " + this.Player.Experience + " / " +
                this.Player.ExperienceNeeded, 2) + " ║";
            menu += "\n║ " + AlignCenter("HP: " + this.Player.Health + " / " + 
                this.Player.MaxHealth, 2) + " ║";
            menu += "\n║" + AlignCenter("", 0) + "║";
            menu += "\n║ " + AlignCenter("Player ATT: " + this.Player.Attack, 2) + " ║";
            menu += "\n║ " + AlignCenter("Player DEF: " + this.Player.Defense, 2) + " ║";
            menu += "\n║" + AlignCenter("", 0) + "║";
            menu += "\n╚═══════════════════════╝";

            return menu;
        }
    }
}
