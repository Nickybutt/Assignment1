using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentHans
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; } = 100;
        public int Damage { get; set; } = 10;
        public bool Alive { get; set; } = true;
        List<string> Inventory = new List<string>();

        public Player()
        {

        }

        public Player(string name)
        {
            Name = name;
        }

        public string MonsterAppeared()
        {
            Console.WriteLine("A Monster appeared.. you can run or you can attack..");
            Console.WriteLine("enter Run of Attack");
            string choice = Console.ReadLine().ToLower();

            return choice;
        }

        public Monster Attack(Player player, Monster monster)
        {
            Console.WriteLine("slash");
            monster.Health -= player.Damage;

            if(monster.Health < 0)
            {
                Console.WriteLine("You've just killed the monster.. Bravo");
                Alive = false;
            }
            else
            {
                monster.Attack(monster, player);
            }

            return monster;
        }
    }
}
