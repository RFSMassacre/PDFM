using System;
using System.Collections.Generic;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    public abstract class MapSlot : ISlot
    {
        public abstract string Name { get; }
        public abstract char Mark { get; }
        public abstract bool CanWalk { get; }
        public abstract SlotType SlotType { get; }
    }
}
