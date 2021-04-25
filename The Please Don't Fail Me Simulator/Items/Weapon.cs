using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Menus;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Items
{
    [Serializable()]
    public abstract class Weapon : Item
    {
        public int AttackBoost { get; private set; }
        public int DefenseBoost { get; private set; }

        public Weapon(string name, int attackBoost, int defenseBoost) : base(name)
        {
            this.AttackBoost = attackBoost;
            this.DefenseBoost = defenseBoost;
        }
        public Weapon(SerializationInfo info, StreamingContext context)
        {
            this.AttackBoost = (int)info.GetValue("Attack-Boost", typeof(int));
            this.DefenseBoost = (int)info.GetValue("Defense-Boost", typeof(int));
        }
        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Attack-Boost", this.AttackBoost);
            info.AddValue("Defense-Boost", this.DefenseBoost);
        }

        public override string ToString()
        {
            return "Equip this weapon? (Y / N)\n";
        }

        public override void Use(Player player)
        {
            player.WeaponName = this.Name;
            Console.WriteLine("\nYou gained " + this.AttackBoost + " ATK!");
            Console.WriteLine("You gained " + this.DefenseBoost + " DEF!");
            Console.WriteLine("\nNew Attack: " + player.Attack + " ATK!");
            Console.WriteLine("New Defense: " + player.Defense + " DEF!");
            Console.ReadKey(true);
        }
    }
}
