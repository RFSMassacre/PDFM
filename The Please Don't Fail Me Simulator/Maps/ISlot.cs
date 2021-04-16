using System;
using System.Collections.Generic;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    public interface ISlot
    {
        public string Name { get; }
        public char Mark { get; }
        public bool CanWalk { get; }
        public SlotType SlotType { get; }

        public static MapSlot GenerateSlot(char icon)
        {
            switch (icon)
            {
                case '░':
                    return new Pavement();
                case '█':
                    return new Building();
                case '╪':
                    return new Fence();
                case ' ':
                    return new Empty();
            }

            return new Empty();
        }
    }
}
