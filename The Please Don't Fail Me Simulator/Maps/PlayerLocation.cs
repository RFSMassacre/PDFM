using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    [Serializable()]
    public class PlayerLocation : MapSlot
    {
        public readonly Player Player;

        public PlayerLocation(Player player) : base()
        {
            this.Player = player;
        }
        public PlayerLocation(Player player, int floor, int x, int y) : base(floor, x, y)
        {
            this.Player = player;
        }

        public override string Name
        {
            get { return "Player"; }
        }
        public override char Mark
        {
            get { return '☺'; }
        }
        public override bool CanWalk
        {
            get { return true; }
        }
        public override SlotType Type
        {
            get { return SlotType.PlayerLocation; }
        }
    }
}
