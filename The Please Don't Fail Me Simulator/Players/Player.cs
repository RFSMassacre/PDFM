using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Items;

namespace The_Please_Dont_Fail_Me_Simulator.Players
{
    [Serializable()]
    public sealed class Player
    {
        private int attack;
        private int defense;

        public string Name { get; private set; }
        public string WeaponName { get; set; }
        public List<string> ItemNames { get; private set; }
        public int Level { get; private set; }
        public float Experience { get; private set; }
        public float ExperienceNeeded { get; private set; }
        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public int Attack
        {
            get
            {
                if (this.WeaponName == null)
                {
                    return this.attack;
                }

                Weapon weapon = (Weapon)Item.GenerateItem(this.WeaponName);
                return this.attack + weapon.AttackBoost;
            }
            private set
            {
                this.attack = value;
            }
        }
        public int Defense
        {
            get
            {
                if (this.WeaponName == null)
                {
                    return this.defense;
                }

                Weapon weapon = (Weapon)Item.GenerateItem(this.WeaponName);
                return this.defense + weapon.DefenseBoost;
            }
            private set
            {
                this.defense = value;
            }
        }
        public Stance Mode;
        public int CritChance { get; private set; }
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
        public int DodgeChance { get; private set; }
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

        public Player(string name)
        {
            this.Name = name;
            this.Level = 1;
            this.Experience = 0;
            this.ExperienceNeeded = this.Level * 100;
            this.MaxHealth = this.Level * 30;
            this.Health = this.MaxHealth;
            this.Attack = this.Level * 2;
            this.Defense = this.Level + 1;
            this.CritChance = (this.Level * 10) / 2;
            this.DodgeChance = (this.Level * 8) / 2;

            this.ItemNames = new List<string>(10);
        }
        public Player(string name, int level)
        {
            this.Name = name;
            if (level <= 0)
            {
                level = 1;
            }
            this.Level = level;
            this.Experience = 0;
            this.ExperienceNeeded = this.Level * 100;

            this.MaxHealth = this.Level * 30;
            this.Health = this.MaxHealth;
            this.Attack = this.Level * 2;
            this.Defense = this.Level + 1;
            this.CritChance = (this.Level * 10) / 2;
            this.DodgeChance = (this.Level * 8) / 2;

            this.ItemNames = new List<string>(10);
        }
        public Player(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue("Name", typeof(string));
            this.Level = (int)info.GetValue("Level", typeof(int));
            this.Experience = (int)info.GetValue("Experience", typeof(int));
            this.ExperienceNeeded = (int)info.GetValue("ExperienceNeeded", typeof(int));
            
            this.MaxHealth = (int)info.GetValue("MaxHealth", typeof(int));
            this.Health = (int)info.GetValue("Health", typeof(int));
            this.Attack = (int)info.GetValue("Attack", typeof(int));
            this.Defense = (int)info.GetValue("Defense", typeof(int));
            this.CritChance = (int)info.GetValue("CritChance", typeof(int));
            this.DodgeChance = (int)info.GetValue("DodgeChance", typeof(int));

            this.WeaponName = (string)info.GetValue("Weapon", typeof(string));

            this.ItemNames = (List<string>)info.GetValue("Items", typeof(List<string>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name);
            info.AddValue("Level", this.Level);
            info.AddValue("Experience", this.Experience);
            info.AddValue("ExperienceNeeded", this.ExperienceNeeded);

            info.AddValue("MaxHealth", this.MaxHealth);
            info.AddValue("Health", this.Health);
            info.AddValue("Attack", this.attack);
            info.AddValue("Defense", this.defense);
            info.AddValue("CritChance", this.CritChance);
            info.AddValue("DodgeChance", this.DodgeChance);

            if (this.WeaponName != null)
            {
                info.AddValue("Weapon", this.WeaponName);
            }

            info.AddValue("Items", this.ItemNames);
        }

        public void SetLevel(int level)
        {
            //Adjusts stats based on level.
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
            //Heals entity without going over max health.
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
            else
            {
                this.Health = healed;
            }

            return healed;
        }
        public int Damage(int damage)
        {
            //Damages entity based on entity stance.
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

        public void LevelUp(float enemyExperience)
        {
            //Keep leveling up until there is not experience points.
            this.Experience += enemyExperience;
            while (this.Experience >= this.ExperienceNeeded)
            {
                this.Experience -= this.ExperienceNeeded;
                SetLevel(this.Level + 1);
            }

            if (this.Experience < 0)
            {
                this.Experience = 0;
            }
        }
    }
}
