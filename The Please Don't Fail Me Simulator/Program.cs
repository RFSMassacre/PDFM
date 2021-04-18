using System;
using System.Threading;
using The_Please_Dont_Fail_Me_Simulator.Players;
using The_Please_Dont_Fail_Me_Simulator.Items;
using The_Please_Dont_Fail_Me_Simulator.Maps;
using The_Please_Dont_Fail_Me_Simulator.Menus;
using System.IO;

namespace The_Please_Dont_Fail_Me_Simulator
{
    public class Program
    {
        public static bool Completed = false;
        public static readonly Map Campus = new Map();
        public static readonly Player Player = new Player("Student", 1);

        public static void Main(string[] args)
        {
            bool firstRun = true;

            Console.Write(new MainMenu());
            Console.WriteLine("\nPress SPACEBAR to start!");

            while (true)
            {
                Console.TreatControlCAsInput = true;
                ConsoleKey key = Console.ReadKey(true).Key;
                if (firstRun)
                {
                    firstRun = false;
                    Console.Clear();
                    Console.Write(Campus);
                }
                else
                {
                    switch (key)
                    {
                        case ConsoleKey.W:
                            Campus.MoveHero(Map.Direction.Up);
                            break;
                        case ConsoleKey.S:
                            Campus.MoveHero(Map.Direction.Down);
                            break;
                        case ConsoleKey.A:
                            Campus.MoveHero(Map.Direction.Left);
                            break;
                        case ConsoleKey.D:
                            Campus.MoveHero(Map.Direction.Right);
                            break;
                        case ConsoleKey.I:
                            OpenInventory();
                            break;
                    }

                    Console.Clear();
                    Console.Write(Campus);
                }
            }
        }

        public static bool StartBattle(Player player, Enemy enemy)
        {
            while (player.Health > 0 & enemy.Health > 0)
            {
                Console.Clear();
                Console.Write(new BattleMenu(player, enemy));
                Console.WriteLine("\nPick your move!");
                Console.WriteLine("1: Attack\t3: Dodge");
                Console.WriteLine("2: Block \t4: Retreat");
                ConsoleKey key = Console.ReadKey(true).Key;
                int playerChoice = 0;
                int enemyChoice = enemy.Choice;
                switch (key)
                {
                    case ConsoleKey.D1:
                        playerChoice = 1;
                        break;
                    case ConsoleKey.D2:
                        playerChoice = 2;
                        break;
                    case ConsoleKey.D3:
                        playerChoice = 3;
                        break;
                    case ConsoleKey.D4:
                        enemy.Heal(enemy.MaxHealth);
                        return false;
                    case ConsoleKey.I:
                        OpenInventory();
                        break;
                }

                Console.Clear();
                Console.Write(new BattleMenu(player, enemy));
                if (enemyChoice >= 2)
                {
                    Choice(enemy, player, enemyChoice);
                    Choice(player, enemy, playerChoice);
                }
                else
                {
                    Choice(player, enemy, playerChoice);
                    if (enemy.Health > 0)
                    {
                        Choice(enemy, player, enemyChoice);
                    }
                    
                }
                player.Mode = Entity.Stance.Attack;
                enemy.Mode = Entity.Stance.Attack;

                if (player.Health <= 0)
                {
                    Console.Clear();
                    Console.Write(new GameOverMenu());
                    Thread.Sleep(4000);
                    Environment.Exit(0);
                    return false;
                }
                else if (enemy.Health <= 0)
                {
                    Console.WriteLine("\nYou have defeated " + enemy.Name + "!");
                    Player.LevelUp(enemy.Experience);
                    Thread.Sleep(1000);
                    Console.WriteLine("You're now Level " + Player.Level + "! (" + Player.Experience 
                        + "XP / " + Player.ExperienceNeeded + "XP)");
                    Thread.Sleep(3000);
                    return true;
                }
            }

            return false;
        }

        private static void Choice(Entity attacker, Entity defender, int choice)
        {
            switch (choice)
            {
                //Attacking
                case 1:
                    attacker.Mode = Entity.Stance.Attack;
                    int attackerCrit = attacker.Crit;
                    int attackerDamage = defender.Damage(attacker.Attack * attackerCrit);
                    if (attackerDamage > 0)
                    {
                        Console.WriteLine("\n" + attacker.Name + " has dealt " + attackerDamage + " DMG.");
                        if (attackerCrit == 2)
                        {
                            Console.WriteLine(attacker.Name + " landed a critical strike!");
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        switch (defender.Mode)
                        {
                            case Entity.Stance.Block:
                                Console.WriteLine("\n" + defender.Name + " blocked all damage.");
                                break;
                            case Entity.Stance.Dodge:
                                Console.WriteLine("\n" + defender.Name + " dodged successfully!");
                                break;
                        }
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(3000);
                    break;
                //Blocking
                case 2:
                    attacker.Mode = Entity.Stance.Block;
                    Console.WriteLine("\n" + attacker.Name + " blocks...");
                    Thread.Sleep(1500);
                    break;
                //Dodging
                case 3:
                    attacker.Mode = Entity.Stance.Dodge;
                    Console.WriteLine("\n" + attacker.Name + " dodges...");
                    Thread.Sleep(1500);
                    break;
                default:
                    break;
            }
        }

        private static void OpenInventory()
        {
            int selection = 0;
            while (true)
            {
                Console.Clear();
                Console.Write(new InventoryMenu(Player, selection) + "\n");
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.W:
                        selection--;
                        break;
                    case ConsoleKey.S:
                        selection++;
                        break;
                }

                //Keep selection in range.
                if (selection < 0)
                {
                    selection = 0;
                }
                else if (selection > Player.Items.Count - 1)
                {
                    selection = Player.Items.Count - 1;
                }

                if (key == ConsoleKey.Enter)
                {
                    //Use item.
                    Item item = Player.Items[selection];
                    Console.Write(item);
                    while (true)
                    {
                        //No switch case so we can easily escape the while loop.
                        ConsoleKey use = Console.ReadKey(true).Key;
                        if (use == ConsoleKey.Y)
                        {
                            item.Use(Player);
                            selection = 0;
                            break;
                        }
                        else if (use == ConsoleKey.N)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
