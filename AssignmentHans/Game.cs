using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AssignmentHans
{
    public class Game
    {
        private Room _currentRoom;
        public Player PlayerName = new Player();

        public void InitializeAndStartGame()
        {
            Setup();

            Initialize();

            Start();
        }

        // Created some rooms. All rooms are connected bij some directions
        private void Setup()
        {
            var entrance = new Room { Description = "Main entrance to the Castle", Name = "Entrance" };
            var livingRoom = new Room { Description = "Welcome to the LivingRoom", Name = "LivingRoom" };
            var diner = new Room { Description = "The diner looks nice, doesn't it?", Name = "Diner" };
            var kitchen = new Room { Description = "We have some food in the kitchen.", Name = "Kitchen", Finish = true };
            var upstairs = new Room { Description = "This is upstairs", Name = "Upstairs" };
            var yard = new Room { Description = "This is the yard", Name = "Yard" };
            var darkRoom = new Room { Description = "This is the darkRoom", Name = "DarkRoom" };
            var bedRoom = new Room { Description = "This is the bedRoom, this is where the magic happens", Name = "BedRoom" };
            var attic = new Room { Description = "This is the attic, the highest room", Name = "Attic" };
            var basement = new Room { Description = "This is the basement, the lowest room", Name = "Basement" };
            var barDancing = new Room { Description = "This is BarDancing de gieter", Name = "de Gieter" };


            entrance.ConnectedRooms.Add(EDirection.North, livingRoom);
            entrance.ConnectedRooms.Add(EDirection.South, yard);

            yard.ConnectedRooms.Add(EDirection.North, entrance);
            yard.ConnectedRooms.Add(EDirection.South, barDancing);
            yard.ConnectedRooms.Add(EDirection.East, livingRoom);
            yard.ConnectedRooms.Add(EDirection.West, attic);
            yard.ConnectedRooms.Add(EDirection.Down, diner);

            livingRoom.ConnectedRooms.Add(EDirection.Up, upstairs);
            livingRoom.ConnectedRooms.Add(EDirection.East, diner);
            livingRoom.ConnectedRooms.Add(EDirection.Down, basement);
            livingRoom.ConnectedRooms.Add(EDirection.South, entrance);

            diner.ConnectedRooms.Add(EDirection.North, kitchen);
            diner.ConnectedRooms.Add(EDirection.West, livingRoom);
            diner.ConnectedRooms.Add(EDirection.East, darkRoom);

            basement.ConnectedRooms.Add(EDirection.Up, livingRoom);

            kitchen.ConnectedRooms.Add(EDirection.South, diner);

            upstairs.ConnectedRooms.Add(EDirection.Up, attic);
            upstairs.ConnectedRooms.Add(EDirection.West, darkRoom);
            upstairs.ConnectedRooms.Add(EDirection.North, bedRoom);
            upstairs.ConnectedRooms.Add(EDirection.Down, livingRoom);

            darkRoom.ConnectedRooms.Add(EDirection.East, upstairs);

            bedRoom.ConnectedRooms.Add(EDirection.South, upstairs);

            attic.ConnectedRooms.Add(EDirection.Down, upstairs);

            _currentRoom = entrance;
        }

        public void SaveHeaven(Room room)
        {
            _currentRoom = room;
        }

        private void Initialize()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Console.WriteLine("------------------------------------------------------------------------------");
            Console.Write("------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("SUPER ZORK");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------------");
            Console.WriteLine("------------------------------------------------------------------------------");

            var str = @"                         __...---""""""---...__
           .:::::.   _.-'                      '-._
       .::::::::::::::::'   ^^      ,              '-.
  .  .:::''''::::::::'   ,        _/(_       ^^       '.
   ':::'      .'       _/(_       {\\               ,   '.
             /         {\\        /;_)            _/(_    \
            /   ,      /;_)    =='/ <===<<<       {\\      \
           /  _/(_  =='/ <===<<<  \__\       ,    /;_)      \
          /   {\\      \__\      , ``      _/(_=='/ <===<<<  \ ";

            Console.WriteLine(str);
            Console.WriteLine("------------------------------------------------------------------------------");

            SetPlayerName();
        }

        private void SetPlayerName()
        {
            PrintLambda();

            Console.WriteLine("Please enter your name");
            string name = Console.ReadLine();

            if (name == "")
            {
                Console.WriteLine("This name is incorrect.. please enter your name again");
                name = Console.ReadLine();
            }

            PlayerName.Name = name;
            Console.WriteLine($"Welcom {name}");

        }

        private static void PrintLambda()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("λ ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public bool ShouldEnd()
        {
            return _currentRoom.Finish;
        }

        public void End()
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("-----       END     ------");
            Console.WriteLine("--------------------------");
        }

        private void Start()
        {

        }

        public void DescribeLocation()
        {
            Console.WriteLine($"-- You are currently in {_currentRoom.Name}. --");
            Console.WriteLine($"-- {_currentRoom.Description}");

            if (_currentRoom.Finish)
            {
                Console.WriteLine("YOU HAVE MADE IT TO THE FINISH!");
                var str = @"╔═══╗─────────────╔╗───╔╗───╔╗
║╔═╗║────────────╔╝╚╗──║║──╔╝╚╗
║║─╚╬══╦═╗╔══╦═╦═╩╗╔╬╗╔╣║╔═╩╗╔╬╦══╦═╗╔══╗
║║─╔╣╔╗║╔╗╣╔╗║╔╣╔╗║║║║║║║║╔╗║║╠╣╔╗║╔╗╣══╣
║╚═╝║╚╝║║║║╚╝║║║╔╗║╚╣╚╝║╚╣╔╗║╚╣║╚╝║║║╠══║
╚═══╩══╩╝╚╩═╗╠╝╚╝╚╩═╩══╩═╩╝╚╩═╩╩══╩╝╚╩══╝
──────────╔═╝║
──────────╚══╝";
                Console.WriteLine(str);
                Console.WriteLine("-----------------------");
            }
        }

        public void MoveToDirection(EDirection direction)
        {
            if (_currentRoom.ConnectedRooms.TryGetValue(direction, out Room MoveFromRoom))
            {
               
                _currentRoom = MoveFromRoom;
                if (_currentRoom.Name == "de Gieter")
                {
                    MonsterAttack();
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine("you drank one lekkere bier");
                        Thread.Sleep(1000);
                    }

                    Console.WriteLine("You're too fucked up.. You can't continue..");
                    Thread.Sleep(1000);

                    End();
                }
                else if (_currentRoom.Name == "Basement")
                {
                    MonsterAttack();
                    Monster monster = new Monster();

                    Console.WriteLine("A wild monster appeared..");
                    Console.WriteLine("you attacked him.");
                    while (PlayerName.Alive)
                    {
                        Console.WriteLine("Do You want to attack the monster.. yes or no");
                        string input = Console.ReadLine().Substring(0).ToLower();

                        if (input == "y")
                        {
                            PlayerName.Attack(PlayerName, monster);
                        }
                        else
                        {
                            Console.WriteLine("run pussy..");
                        }
                    }
                }
                else
                {
                    MonsterAttack();
                }
            }
            else
            {
                Console.WriteLine("There is no door you blind fool.. You just walked into a wall.");
            }
        }

        public void MonsterAttack()
        {
            Random rand = new Random();
            Monster monster = new Monster();
            int attack = rand.Next(1, 10);

            if(attack == 3 || attack == 7 || attack == 9)
            {
                if(PlayerName.MonsterAppeared() == "attack")
                {
                    PlayerName.Attack(PlayerName, monster);
                    CheckLife(PlayerName);
                    PlayerName.MonsterAppeared();
                }
            }
            else if(attack == 2)
            {
                Console.WriteLine("You found a health potion.. you're healt is resored");
                PlayerName.Health = 100;
            }
            else
            {
                
            }

        }

        public void CheckLife(Player player)
        {
            if (!PlayerName.Alive)
            {
                End();
            }
            else
            {
                Console.WriteLine($"you're current health is {PlayerName.Health}");
            }
        }

        public void LookArround()
        {
            _currentRoom.PrintInfo();
        }
    }
}
