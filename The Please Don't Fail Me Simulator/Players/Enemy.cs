using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Players
{
    [Serializable()]
    public class Enemy
    {
        public static List<string> Names = new List<string>()
        {
            "Aaron", "Will", "Omar", "René", "Evin", "Addy", "Lily", "Suzie",
            "Jason", "Ashley", "Amy", "David", "Ruben", "Dan", "Stan", "Jelmer",
            "Mason", "Melissa", "Julia", "Matthew", "Luke", "Mark", "Lori",
            "Vincent", "Carly", "Miguel", "Ryan", "Keira", "Dwayne", "Joanne",
            "Mia", "Natalia", "Elise"
        };

        public string Name { get; private set; }
        public int Level { get; private set; }
        public float Experience { get; private set; }
        public float ExperienceNeeded { get; private set; }
        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
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

        public int Choice
        {
            get
            {
                int outOf100 = new Random(DateTime.Now.Millisecond).Next(100) + 1;
                int choice;
                if (outOf100 >= 1 & outOf100 <= 75)
                {
                    choice = 1;
                }
                else if (outOf100 >= 76 & outOf100 <= 90)
                {
                    choice = 2;
                }
                else
                {
                    choice = 3;
                }
                return choice;
            }
        }
        public string Dialogue { get; private set; }

        public Enemy(string dialogue, int levelMin, int levelMax)
        {
            this.Name = GenerateName();
            SetStats(GenerateLevel(levelMin, levelMax));
            this.Dialogue = this.Name + ": " + dialogue;
        }
        public Enemy(int levelMin, int levelMax)
        {
            this.Name = GenerateName();
            SetStats(GenerateLevel(levelMin, levelMax));
            this.Dialogue = "";
        }
        public Enemy(int level)
        {
            this.Name = GenerateName();
            SetStats(level);
            this.Dialogue = "";
        }
        public Enemy(string name, int level)
        {
            this.Name = name;
            SetStats(level);
            this.Dialogue = "";
        }
        public Enemy(SerializationInfo info, StreamingContext context)
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
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name);
            info.AddValue("Level", this.Level);
            info.AddValue("Experience", this.Experience);
            info.AddValue("ExperienceNeeded", this.ExperienceNeeded);

            info.AddValue("MaxHealth", this.MaxHealth);
            info.AddValue("Health", this.Health);
            info.AddValue("Attack", this.Attack);
            info.AddValue("Defense", this.Defense);
            info.AddValue("CritChance", this.CritChance);
            info.AddValue("DodgeChance", this.DodgeChance);
        }

        private void SetStats(int level)
        {
            this.Level = level;
            this.ExperienceNeeded = this.Level * 100;
            this.Experience = this.ExperienceNeeded;
            this.MaxHealth = this.Level * 30;
            this.Health = this.MaxHealth;
            this.Attack = this.Level * 2;
            this.Defense = this.Level + 1;
            this.CritChance = (this.Level * 10) / 2;
            this.DodgeChance = (this.Level * 8) / 2;
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
        private string GenerateName()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            string name = Names[random.Next(Names.Count)];
            Names.Remove(name);
            return "Student " + name;
        }
        private int GenerateLevel(int min, int max)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            return random.Next(min, max);
        }
    }
}
