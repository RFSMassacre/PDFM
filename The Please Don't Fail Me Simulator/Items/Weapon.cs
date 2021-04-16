using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Menus;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Items
{
    public abstract class Weapon : Item
    {
        public int AttackBoost { get; private set; }
        public int DefenseBoost { get; private set; }

        public Weapon(string name, int attackBoost, int defenseBoost) : base(name)
        {
            this.AttackBoost = attackBoost;
            this.DefenseBoost = defenseBoost;
        }

        public override string ToString()
        {
            return "Equip this weapon? (Y / N)\n";
        }

        public override void Use(Player player)
        {
            player.Weapon = this;
            Console.WriteLine("\nYou gained " + this.AttackBoost + " ATK!");
            Console.WriteLine("You gained " + this.DefenseBoost + " DEF!");
            Console.WriteLine("\nNew Attack: " + player.Attack + " ATK!");
            Console.WriteLine("New Defense: " + player.Defense + " DEF!");
            Console.ReadKey(true);
        }
    }
}
