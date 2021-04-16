using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Items;

namespace The_Please_Dont_Fail_Me_Simulator.Players
{
    public class Player : Entity
    {
        public List<Item> Items { get; private set; }

        public Player(string name) : base(name, 1, 1)
        {
            this.Level = 1;
            this.Experience = 0;
            this.ExperienceNeeded = this.Level * 100;
            this.MaxHealth = this.Level * 30;
            this.Health = this.MaxHealth;
            this.Attack = this.Level * 2;
            this.Defense = this.Level + 1;
            this.CritChance = (this.Level * 10) / 2;
            this.DodgeChance = (this.Level * 8) / 2;

            this.Items = new List<Item>();
        }
        public Player(string name, int level) : base(name, 1, 1)
        {
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

            this.Items = new List<Item>();
        }

        public void LevelUp(float enemyExperience)
        {
            if (enemyExperience <= 0)
            {
                return;
            }

            //Keep leveling up until there is not experience points.
            while (enemyExperience >= this.ExperienceNeeded)
            {
                SetLevel(this.Level + 1);
                enemyExperience -= this.ExperienceNeeded;
            }
            this.Experience = enemyExperience;
        }
    }
}
