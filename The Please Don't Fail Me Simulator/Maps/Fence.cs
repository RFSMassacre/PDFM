using System;
using System.Collections.Generic;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    public class Fence : MapSlot
    {
        public override string Name
        {
            get { return "Fence"; }
        }
        public override char Mark
        {
            get { return '╪'; }
        }
        public override bool CanWalk
        {
            get { return false; }
        }
        public override SlotType SlotType
        {
            get { return SlotType.Fence; }
        }
    }
}
