using System;
using System.Collections.Generic;
using System.Text;

namespace Zoo
{
    internal class Animal
    {
        private static int _nextId = 1; // auto-increment ID

        public int Id { get; }
        public string Name { get; }
        public string Species { get; }
        public int Age { get; }
        public double Cost { get; }
        public string Environment { get; }

        public Animal(string name, string species, int age, double cost, string environment)
        {
            Id = _nextId++;
            Name = name;
            Species = species;
            Age = age;
            Cost = cost;
            Environment = environment;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Species: {Species}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Cost: {Cost}");
            Console.WriteLine($"Environment: {Environment}");
            Console.WriteLine();
        }
    }
}
