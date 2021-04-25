using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Maps;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Menus
{
    public class BattleMenu : TextMenu
    {
        private Player Player;
        private Enemy Enemy;

        public BattleMenu(Player player, Enemy enemy)
        {
            this.Player = player;
            this.Enemy = enemy;
        }

        public override string ToString()
        {
            string menu = "╔═══════════════════════╗";
            menu += "\n║        Battle         ║";
            menu += "\n║" + AlignCenter("", 0) + "║";
            menu += "\n║ " + AlignCenter(Enemy.Name, 2) + " ║";
            menu += "\n║ " + AlignCenter("Enemy HP: " + Enemy.Health, 2) + " ║";
            menu += "\n║ " + AlignCenter("Enemy ATT: " + Enemy.Attack, 2) + " ║";
            menu += "\n║ " + AlignCenter("Enemy DEF: " + Enemy.Defense, 2) + " ║";
            menu += "\n║" + AlignCenter("", 0) + "║";
            menu += "\n║ " + AlignCenter("Player HP: " + Player.Health, 2) + " ║";
            menu += "\n║ " + AlignCenter("Player ATT: " + Player.Attack, 2) + " ║";
            menu += "\n║ " + AlignCenter("Player DEF: " + Player.Defense, 2) + " ║";
            menu += "\n║" + AlignCenter("", 0) + "║";
            menu += "\n╚═══════════════════════╝";

            return menu;
        }
    }
}
