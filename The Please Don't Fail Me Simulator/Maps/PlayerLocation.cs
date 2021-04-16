using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    public class PlayerLocation : MapSlot
    {
        public readonly Player Player;

        public PlayerLocation(Player player)
        {
            this.Player = player;
        }

        public override string Name
        {
            get { return "Hero"; }
        }
        public override char Mark
        {
            get { return '☺'; }
        }
        public override bool CanWalk
        {
            get { return true; }
        }
        public override SlotType SlotType
        {
            get { return SlotType.PlayerLocation; }
        }
    }
}
