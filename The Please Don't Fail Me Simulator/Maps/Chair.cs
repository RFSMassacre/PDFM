using System;
using System.Collections.Generic;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    [Serializable()]
    public class Chair : MapSlot
    {
        public Chair() : base()
        {

        }
        public Chair(int floor, int x, int y) : base(floor, x, y)
        {

        }

        public override string Name
        {
            get { return "Chair"; }
        }
        public override char Mark
        {
            get { return '└'; }
        }
        public override bool CanWalk
        {
            get { return false; }
        }
        public override SlotType Type
        {
            get { return SlotType.Chair; }
        }
    }
}
