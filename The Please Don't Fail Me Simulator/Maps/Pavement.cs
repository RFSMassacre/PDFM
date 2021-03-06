using System;
using System.Collections.Generic;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    [Serializable()]
    public class Pavement : MapSlot
    {
        public Pavement() : base()
        {

        }
        public Pavement(int floor, int x, int y) : base(floor, x, y)
        {

        }

        public override string Name
        {
            get { return "Pavement"; }
        }
        public override char Mark
        {
            get { return '░'; }
        }
        public override bool CanWalk
        {
            get { return true; }
        }
        public override SlotType Type
        {
            get { return SlotType.Pavement; }
        }
    }
}
