using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Items;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    [Serializable()]
    class ItemLocation : MapSlot
    {
        public readonly string ItemName;

        public ItemLocation(string item) : base()
        {
            this.ItemName = item;
        }
        public ItemLocation(string item, int floor, int x, int y) : base(floor, x, y)
        {
            this.ItemName = item;
        }

        public override string Name
        {
            get { return "Item"; }
        }
        public override char Mark
        {
            get { return 'I'; }
        }
        public override bool CanWalk
        {
            get { return true; }
        }
        public override SlotType Type
        {
            get { return SlotType.Item; }
        }
    }
}
