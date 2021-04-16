using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Items;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    class ItemLocation : MapSlot
    {
        public readonly Item Item;

        public ItemLocation(Item item)
        {
            this.Item = item;
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
        public override SlotType SlotType
        {
            get { return SlotType.Item; }
        }
    }
}
