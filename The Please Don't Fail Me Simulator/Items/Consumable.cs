using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Menus;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Items
{
    [Serializable()]
    public abstract class Consumable : Item
    {
        public int HealthBoost { get; private set; }

        public Consumable(string name, int healthBoost) : base(name)
        {
            this.HealthBoost = healthBoost;
        }
        public Consumable(SerializationInfo info, StreamingContext context)
        {
            this.HealthBoost = (int)info.GetValue("Health-Boost", typeof(int));
        }
        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Health-Boost", this.HealthBoost);
        }

        public override string ToString()
        {
            return "Consume this item? (Y / N)\n";
        }

        public override void Use(Player player)
        {
            Console.WriteLine("\nHealth: " + player.Health);
            player.Heal(this.HealthBoost);
            Console.WriteLine("You gained " + this.HealthBoost + " HP!");
            Console.WriteLine("New Health: " + player.Health + " HP");
            player.ItemNames.Remove(this.Name);
            Console.ReadKey(true);
        }
    }
}
