using System;
using System.Collections.Generic;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    [Serializable()]
    public class Empty : MapSlot
    {
        public Empty() : base()
        {

        }
        public Empty(int floor, int x, int y) : base(floor, x, y)
        {

        }

        public override string Name
        {
            get { return "Empty"; }
        }
        public override char Mark
        {
            get { return ' '; }
        }
        public override bool CanWalk
        {
            get { return false; }
        }
        public override SlotType Type
        {
            get { return SlotType.Empty; }
        }
    }
}
