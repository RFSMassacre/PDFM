using System;
using System.Collections.Generic;
using System.Text;
using The_Please_Dont_Fail_Me_Simulator.Items;
using The_Please_Dont_Fail_Me_Simulator.Players;

namespace The_Please_Dont_Fail_Me_Simulator.Maps
{
    public sealed class Map
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private static int sizeX;
        private static int sizeY;

        public static int SizeX
        {
            get
            {
                return sizeX;
            }
            private set
            {
                sizeX = value;
            }
        }
        public static int SizeY
        {
            get
            {
                return sizeY;
            }
            private set
            {
                sizeY = value;
            }
        }

        private PlayerLocation PlayerLocation;

        private MapSlot[,] CampusMap;
        private MapSlot[,] Campus;

        public Map()
        {
            this.PlayerLocation = new PlayerLocation(Program.Player);
            GenerateMap();
        }

        private void GenerateMap()
        {
            string image = Properties.Resources.Campus;
            string[] lines = image.Split(Environment.NewLine);
            SizeX = lines[0].Length;
            SizeY = lines.Length;

            this.CampusMap = new MapSlot[SizeY, SizeX];
            for (int y = 0; y < SizeY; y++)
            {
                char[] icons = lines[y].ToCharArray();
                for (int x = 0; x < SizeX; x++)
                {
                    this.CampusMap[y, x] = ISlot.GenerateSlot(icons[x]);
                }
            }

            this.Campus = (MapSlot[,])this.CampusMap.Clone();
            this.Campus[9, 2] = this.PlayerLocation;
            this.Campus[2, 2] = new EnemyLocation(new Enemy("Ex-Lover", 10));
            this.Campus[7, 8] = new ItemLocation(new Coffee());
            this.Campus[4, 12] = new ItemLocation(new Monster());
            this.Campus[11, 19] = new ItemLocation(new BrokenBottle());
            this.Campus[2, 21] = new ItemLocation(new YardStick());
        }

        public void MoveHero(Direction direction)
        {
            for (int vertical = 0; vertical < this.Campus.GetLength(0); vertical++)
            {
                for (int horizontal = this.Campus.GetLength(1) - 1; horizontal >= 0; horizontal--)
                {
                    if (this.Campus[vertical, horizontal] is PlayerLocation)
                    {
                        int newY = vertical;
                        int newX = horizontal;

                        //Forces only one direction when moving.
                        switch (direction)
                        {
                            case Direction.Up: newY--; break;
                            case Direction.Down: newY++; break;
                            case Direction.Left: newX--; break;
                            case Direction.Right: newX++; break;
                        }

                        //Ensures there's no OutOfBounds issue.
                        if (newY < 0 || newY >= this.Campus.GetLength(0))
                        {
                            return;
                        }
                        if (newX < 0 || newX >= this.Campus.GetLength(1))
                        {
                            return;
                        }
                        if (!this.Campus[newY, newX].CanWalk)
                        {
                            return;
                        }
                        if (this.Campus[newY, newX] is EnemyLocation)
                        {
                            EnemyLocation enemyLocation = (EnemyLocation)this.Campus[newY, newX];
                            bool won = Program.StartBattle(Program.Player, enemyLocation.Enemy);

                            if (won)
                            {
                                this.Campus[vertical, horizontal] = this.CampusMap[vertical, horizontal];
                                this.Campus[newY, newX] = this.PlayerLocation;
                            }
                            return;
                        }
                        else if (this.Campus[newY, newX] is ItemLocation)
                        {
                            ItemLocation itemLocation = (ItemLocation)this.Campus[newY, newX];
                            Item item = itemLocation.Item;
                            Program.Player.Items.Add(item);
                            Console.Clear();
                            Console.Write(Properties.Resources.NewItem + "\n");
                            Console.ReadKey(true);
                        }

                        this.Campus[vertical, horizontal] = this.CampusMap[vertical, horizontal];
                        this.Campus[newY, newX] = this.PlayerLocation;
                        return;
                    }
                }
            }
        }

        public int GetHeroX()
        {
            for (int vertical = 0; vertical < this.Campus.GetLength(0); vertical++)
            {
                for (int horizontal = 0; horizontal < this.Campus.GetLength(1); horizontal++)
                {
                    if (this.Campus[vertical, horizontal] is PlayerLocation)
                    {
                        return horizontal;
                    }
                }
            }

            return -1;
        }
        public int GetHeroY()
        {
            for (int vertical = 0; vertical < this.Campus.GetLength(0); vertical++)
            {
                for (int horizontal = 0; horizontal < this.Campus.GetLength(1); horizontal++)
                {
                    if (this.Campus[vertical, horizontal] is PlayerLocation)
                    {
                        return vertical;
                    }
                }
            }

            return -1;
        }

        public int GetFormattedY()
        {
            return (this.Campus.GetLength(0) - 1) - GetHeroY();
        }

        public override string ToString()
        {
            string menu = "";
            for (int vertical = 0; vertical < SizeY; vertical++)
            {
                for (int horizontal = 0; horizontal < SizeX; horizontal++)
                {
                    menu += this.Campus[vertical, horizontal].Mark;
                }
                menu += "\n";
            }
            return menu;
        }
    }
}
