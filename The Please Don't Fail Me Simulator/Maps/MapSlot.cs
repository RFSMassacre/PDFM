using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    [Serializable()]
    public abstract class MapSlot
    {
        public enum SlotType
        {
            Empty,
            Pavement,
            Fence,
            Building,
            Item,
            PlayerLocation,
            EnemyLocation,
            Door,
            Chair
        }

        public abstract string Name { get; }
        public abstract char Mark { get; }
        public abstract bool CanWalk { get; }
        public abstract SlotType Type { get; }

        public int Floor;
        public int X;
        public int Y;

        public MapSlot()
        {
            this.Floor = 0;
            this.X = 0;
            this.Y = 0;
        }
        public MapSlot(int floor, int x, int y)
        {
            this.Floor = floor;
            this.X = x;
            this.Y = y;
        }
        public MapSlot(SerializationInfo info, StreamingContext context)
        {
            this.Floor = (int)info.GetValue("Floor", typeof(int));
            this.X = (int)info.GetValue("X", typeof(int));
            this.Y = (int)info.GetValue("Y", typeof(int));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Floor", this.Floor);
            info.AddValue("X", this.X);
            info.AddValue("Y", this.Y);
        }

        public static MapSlot GenerateSlot(char icon, int floor, int x, int y)
        {
            switch (icon)
            {
                case '░':
                    return new Pavement(floor, x, y);
                case '█':
                    return new Building(floor, x, y);
                case '╪':
                    return new Fence(floor, x, y);
                case '└':
                    return new Chair(floor, x, y);
            }

            return new Empty(floor, x, y);
        }
    }
}
