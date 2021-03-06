using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    [Serializable()]
    public class EnemyLocation : MapSlot
    {
        public readonly Enemy Enemy;

        public EnemyLocation(Enemy enemy) : base()
        {
            this.Enemy = enemy;
        }
        public EnemyLocation(Enemy enemy, int floor, int x, int y) : base(floor, x, y)
        {
            this.Enemy = enemy;
        }

        public override string Name
        {
            get { return "Enemy"; }
        }
        public override char Mark
        {
            get { return 'X'; }
        }
        public override bool CanWalk
        {
            get { return true; }
        }
        public override SlotType Type
        {
            get { return SlotType.EnemyLocation; }
        }
    }
}
