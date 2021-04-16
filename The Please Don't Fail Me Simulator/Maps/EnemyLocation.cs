using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    public class EnemyLocation : MapSlot
    {
        public readonly Enemy Enemy;
        
        public EnemyLocation(Enemy enemy)
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
        public override SlotType SlotType
        {
            get { return SlotType.EnemyLocation; }
        }
    }
}
