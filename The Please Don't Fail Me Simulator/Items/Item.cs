using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Items
{
    [Serializable()]
    public abstract class Item
    {
        public string Name { get; private set; }

        public Item()
        {
            this.Name = "";
        }
        public Item(string name)
        {
            this.Name = name;
        }
        public Item(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue("Item-Name", typeof(string));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Item-Name", this.Name);
        }

        public override string ToString()
        {
            return "Use this item? (Y / N)";
        }

        public abstract void Use(Player player);

        public static Item GenerateItem(string name)
        {
            switch (name)
            {
                case "Coffee":
                    return new Coffee();
                case "Monster":
                    return new Monster();
                case "Yard Stick":
                    return new YardStick();
                case "Bottle":
                    return new Bottle();
                case "Ramen":
                    return new Ramen();
                case "Red Bull":
                    return new RedBull();
                case "Waffles":
                    return new Waffles();
                case "Pole":
                    return new Pole();
                case "Garbage Lid":
                    return new GarbageLid();
            }

            return null;
        }
    }
}
