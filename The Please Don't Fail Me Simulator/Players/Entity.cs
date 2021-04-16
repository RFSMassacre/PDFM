using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Items;

namespace The_Please_Dont_Fail_Me_Simulator.Players
{
    public abstract class Entity
    {
        public enum Stance
        {
            Attack,
            Block,
            Dodge
        }

        public int X;
        public int Y;
        private int attack;
        private int defense;

        public string Name { get; protected set; }
        public Weapon Weapon;
        public int Level { get; protected set; }
        public float Experience { get; protected set; }
        public float ExperienceNeeded { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Health { get; protected set; }
        public int Attack
        {
            get
            {
                if (Weapon == null)
                {
                    return this.attack;
                }

                return this.attack + Weapon.AttackBoost;
            }
            protected set
            {
                this.attack = value;
            }
        }
        public int Defense
        {
            get
            {
                if (Weapon == null)
                {
                    return this.defense;
                }

                return this.defense + Weapon.DefenseBoost;
            }
            protected set
            {
                this.defense = value;
            }
        }
        public Stance Mode;
        public int CritChance { get; protected set; }
        public int Crit
        {
            get
            {
                int crit = new Random(DateTime.Now.Millisecond).Next(100);
                if (crit <= this.CritChance)
                {
                    return 2;
                }

                return 1;
            }
        }
        public int DodgeChance { get; protected set; }
        public bool Dodge
        {
            get
            {
                int dodge = new Random(DateTime.Now.Millisecond).Next(100);
                if (dodge <= this.DodgeChance)
                {
                    return true;
                }

                return false;
            }
        }

        public Entity(String name, int x, int y)
        {
            this.X = x;
            this.Y = y;

            this.Name = name;
            this.Level = 0;
            this.MaxHealth = 0;
            this.Health = 0;
            this.Attack = 0;
            this.Defense = 0;
            this.CritChance = 0;
            this.DodgeChance = 0;
        }

        public void SetLevel(int level)
        {
            if (level <= 0)
            {
                level = 1;
            }

            this.Level = level;
            this.Experience = 0;
            this.ExperienceNeeded = this.Level * 100;
            this.MaxHealth = this.Level * 30;
            this.Attack = this.Level * 2;
            this.Defense = this.Level + 1;

            Heal(this.MaxHealth / 3);
        }
        public int Heal(int heal)
        {
            if (heal < 1)
            {
                return 0;
            }

            if (this.Health == this.MaxHealth)
            {
                return 0;
            }

            int healed = this.Health + heal;
            if (healed > this.MaxHealth)
            {
                healed = this.MaxHealth - this.Health;
                this.Health = this.MaxHealth;
            }

            return healed;
        }
        public int Damage(int damage)
        {
            if (damage > 0 && this.Health > 0)
            {
                int finalDamage = damage;
                int finalDefense = this.Defense;
                if (finalDefense < 3)
                {
                    finalDefense = 3;
                }

                switch (this.Mode)
                {
                    case Stance.Attack:
                        finalDamage = damage / (finalDefense / 3);
                        break;
                    case Stance.Block:
                        finalDamage = damage / finalDefense;
                        break;
                    case Stance.Dodge:
                        if (this.Dodge)
                        {
                            finalDamage = 0;
                        }
                        break;
                }

                this.Health -= finalDamage;
                if (this.Health < 0)
                {
                    this.Health = 0;
                }

                return finalDamage;
            }

            return 0;
        }
    }
}
