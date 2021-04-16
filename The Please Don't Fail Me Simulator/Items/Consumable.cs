using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Menus;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Items
{
    public abstract class Consumable : Item
    {
        public int HealthBoost { get; private set; }

        public Consumable(string name, int healthBoost) : base(name)
        {
            this.HealthBoost = healthBoost;
        }

        public override string ToString()
        {
            return "Consume this item? (Y / N)\n";
        }

        public override void Use(Player player)
        {
            player.Heal(this.HealthBoost);
            Console.WriteLine("\nYou gained " + this.HealthBoost + " HP!");
            Console.WriteLine("\nNew Health: " + player.Health + " HP");
            player.Items.Remove(this);
            Console.ReadKey(true);
        }
    }
}
