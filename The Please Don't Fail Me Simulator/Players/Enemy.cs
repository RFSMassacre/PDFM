using System;
using System.Collections.Generic;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Players
{
    public class Enemy : Entity
    {
        public int Choice
        {
            get
            {
                int outOf100 = new Random(DateTime.Now.Millisecond).Next(100) + 1;
                int choice = 1;
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

        public Enemy(string name, int level) : base(name, 1, 9)
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
    }
}
