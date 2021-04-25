using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Items;
using The_Please_Dont_Fail_Me_Simulator.Menus;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    [Serializable()]
    public sealed class Map
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            Ascend,
            Descend
        }

        public static int Floors = 4;
        public static int SizeX = 25;
        public static int SizeY = 13;

        private PlayerLocation PlayerLocation;

        private MapSlot[,,] CampusMap;
        private MapSlot[,,] Campus;
        private string[] Images;

        public Map()
        {
            this.Images = new string[Floors];
            this.Images[0] = Properties.Resources.Outside;
            this.Images[1] = Properties.Resources.Main;
            this.Images[2] = Properties.Resources.Second;
            this.Images[3] = Properties.Resources.Third;

            this.PlayerLocation = new PlayerLocation(Program.Player, 0, 5, 11);
            this.CampusMap = new MapSlot[Floors, SizeY, SizeX];
            this.Campus = new MapSlot[Floors, SizeY, SizeX];

            GenerateMap();
        }
        public Map(SerializationInfo info, StreamingContext context)
        {
            this.Images = new string[Floors];
            this.Images[0] = Properties.Resources.Outside;
            this.Images[1] = Properties.Resources.Main;
            this.Images[2] = Properties.Resources.Second;
            this.Images[3] = Properties.Resources.Third;

            this.CampusMap = new MapSlot[Floors, SizeY, SizeX];
            this.Campus = new MapSlot[Floors, SizeY, SizeX];

            char[,,] icons = (char[,,])info.GetValue("Map", typeof(char[,,]));
            for (int floor = 0; floor < Floors; floor++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        this.Campus[floor, y, x] = MapSlot.GenerateSlot(icons[floor, y, x], 
                            floor, y, x);
                    }
                }
            }

            int playerFloor = (int)info.GetValue("Player-Floor", typeof(int));
            int playerX = (int)info.GetValue("Player-X", typeof(int));
            int playerY = (int)info.GetValue("Player-Y", typeof(int));

            this.PlayerLocation = new PlayerLocation(Program.Player, playerFloor, playerX, playerY);
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            char[,,] icons = new char[Floors, SizeY, SizeX];
            for (int floor = 0; floor < Floors; floor++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        icons[floor, y, x] = this.CampusMap[floor, y, x].Mark;
                    }
                }
            }

            info.AddValue("Map", icons);
            info.AddValue("Player-Floor", this.PlayerLocation.Floor);
            info.AddValue("Player-X", this.PlayerLocation.X);
            info.AddValue("Player-Y", this.PlayerLocation.Y);
        }

        private void GenerateMap()
        {
            //Each floor is generated based on the order of the array.
            for (int floor = 0; floor < Floors; floor++)
            {
                string image = this.Images[floor];
                string[] lines = image.Split(Environment.NewLine);
                for (int y = 0; y < SizeY; y++)
                {
                    char[] icons = lines[y].ToCharArray();
                    for (int x = 0; x < SizeX; x++)
                    {
                        this.CampusMap[floor, y, x] = MapSlot.GenerateSlot(icons[x], floor, x, y);
                    }
                }
            }

            //Door between outside and 1st floor.
            this.CampusMap[0, 10, 13] = new Door(1, 22, 10);
            this.CampusMap[1, 10, 23] = new Door(0, 14, 10);
            //Door between 1st floor and 2nd floor.
            this.CampusMap[1, 3, 18] = new Door(2, 18, 2);
            this.CampusMap[2, 3, 18] = new Door(1, 18, 2);
            //Door between 2nd floor and 3rd floor.
            this.CampusMap[2, 11, 12] = new Door(3, 2, 2);
            this.CampusMap[3, 1, 2] = new Door(2, 12, 11);
            this.Campus = (MapSlot[,,])this.CampusMap.Clone();
            //Setting up player, mini bosses, and final boss.
            this.Campus[this.PlayerLocation.Floor, this.PlayerLocation.Y, this.PlayerLocation.X] =
                this.PlayerLocation;
            this.Campus[0, 10, 14] = new EnemyLocation(new Enemy("Evil Student X", 3), 0, 14, 10);
            this.Campus[1, 2, 18] = new EnemyLocation(new Enemy("Evil Student Y", 5), 1, 18, 2);
            this.Campus[2, 10, 12] = new EnemyLocation(new Enemy("Evil Student Z", 8), 2, 12, 10);
            this.Campus[3, 7, 18] = new EnemyLocation(new Enemy("Evil Professor John", 10), 3, 18, 7);
            //Random locations.
            RandomLocation(new ItemLocation(new Coffee().Name), 0);
            RandomLocation(new ItemLocation(new Monster().Name), 0);
            RandomLocation(new ItemLocation(new YardStick().Name), 0);
            RandomLocation(new ItemLocation(new Bottle().Name), 1);
            RandomLocation(new ItemLocation(new RedBull().Name), 1);
            RandomLocation(new ItemLocation(new Ramen().Name), 1);
            RandomLocation(new ItemLocation(new Waffles().Name), 2);
            RandomLocation(new ItemLocation(new Coffee().Name), 2);
            RandomLocation(new ItemLocation(new Monster().Name), 2);
            RandomLocation(new ItemLocation(new GarbageLid().Name), 2);
            //Lvl 1.
            RandomLocation(new EnemyLocation(new Enemy(1)), 0);
            RandomLocation(new EnemyLocation(new Enemy(1)), 0);
            RandomLocation(new EnemyLocation(new Enemy(1)), 0);
            //Really Lvl 2 to Lvl 3. (Max is number minus 1.)
            RandomLocation(new EnemyLocation(new Enemy(2, 4)), 1);
            RandomLocation(new EnemyLocation(new Enemy(2, 4)), 1);
            RandomLocation(new EnemyLocation(new Enemy(2, 4)), 1);
            RandomLocation(new EnemyLocation(new Enemy(2, 4)), 1);
            RandomLocation(new EnemyLocation(new Enemy(2, 4)), 1);
            //Lvl 4 to Lvl 7.
            RandomLocation(new EnemyLocation(new Enemy(4, 8)), 2);
            RandomLocation(new EnemyLocation(new Enemy(4, 8)), 2);
            RandomLocation(new EnemyLocation(new Enemy(4, 8)), 2);
            RandomLocation(new EnemyLocation(new Enemy(4, 8)), 2);

        }

        public void Move(Direction direction)
        {
            int floor = this.PlayerLocation.Floor;
            int newY = this.PlayerLocation.Y;
            int newX = this.PlayerLocation.X;

            //Forces only one direction when moving.
            switch (direction)
            {
                case Direction.Up: newY--; break;
                case Direction.Down: newY++; break;
                case Direction.Left: newX--; break;
                case Direction.Right: newX++; break;
            }

            //Ensures there's no OutOfBounds issue.
            MapSlot newSlot = this.Campus[floor, newY, newX];
            if (newY < 0 || newY >= this.Campus.GetLength(1))
            {
                return;
            }
            if (newX < 0 || newX >= this.Campus.GetLength(2))
            {
                return;
            }
            if (!newSlot.CanWalk)
            {
                return;
            }
            if (newSlot is EnemyLocation)
            {
                EnemyLocation enemyLocation = (EnemyLocation)this.Campus[floor, newY, newX];
                bool won = Program.StartBattle(Program.Player, enemyLocation.Enemy);

                if (won)
                {
                    Insert(this.PlayerLocation, floor, newX, newY);
                }
                return;
            }
            else if (newSlot is ItemLocation)
            {
                ItemLocation itemLocation = (ItemLocation)this.Campus[floor, newY, newX];
                Item item = Item.GenerateItem(itemLocation.ItemName);
                Program.Player.ItemNames.Add(item.Name);
                Console.Clear();
                Console.Write(new ItemPickupMenu(item));
                Console.ReadKey(true);
                Insert(this.PlayerLocation, floor, newX, newY);
                return;
            }
            else if (newSlot is Door)
            {
                Door door = (Door)this.Campus[floor, newY, newX];
                Insert(this.PlayerLocation, door.Floor, door.X, door.Y);
                return;
            }
            else
            {
                Insert(this.PlayerLocation, floor, newX, newY);
            }
            
        }
        public void Insert(MapSlot slot, int floor, int x, int y)
        {
            this.Campus[slot.Floor, slot.Y, slot.X] = this.CampusMap[slot.Floor, slot.Y, slot.X];
            slot.Floor = floor;
            slot.X = x;
            slot.Y = y;
            this.Campus[floor, y, x] = slot;
        }
        private void RandomLocation(MapSlot slot, int floor)
        {
            while (true)
            {
                int y = new Random(DateTime.Now.Millisecond).Next(SizeY);
                int x = new Random(DateTime.Now.Millisecond).Next(SizeX);
                if (!(this.Campus[floor, y, x] is Pavement))
                {
                    continue;
                }

                this.Campus[floor, y, x] = slot;
                slot.Floor = floor;
                slot.Y = y;
                slot.X = x;
                break;
            }
        }

        public string GetMap()
        {
            string menu = "";
            for (int vertical = 0; vertical < SizeY; vertical++)
            {
                for (int horizontal = 0; horizontal < SizeX; horizontal++)
                {
                    menu += this.Campus[this.PlayerLocation.Floor, vertical, horizontal].Mark;
                }
                menu += "\n";
            }
            menu += "\nControls:";
            menu += "\nWASD: Movement\tI: Inventory";
            menu += "\nESC: Save Game\tP: Stats\n";
            return menu;
        }
    }
}
