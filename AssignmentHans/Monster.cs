using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentHans
{
    public class Monster
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public bool Alive { get; set; } = true;

        private Random rand = new Random();

        public Monster()
        {
            Health = rand.Next(10, 20);
            Damage = rand.Next(3, 15);
        }

        public Player Attack(Monster monster, Player player)
        {
            
            Console.WriteLine($"Monster attacked {player.Name}");
            player.Health -= monster.Damage;
            Console.WriteLine($"The monster has {monster.Health} left but he attacked you");

            if (player.Health < 0)
            {
                Console.WriteLine("The monster killed you..");
                Alive = false;
                
                  
            }

            return player;
        }

    }
}
