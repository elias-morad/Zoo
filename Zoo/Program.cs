using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Animal> zoo = new List<Animal>();

            bool displayAnimals()
            {
                Console.WriteLine("\nAnimals in the Zoo:\n");

                if (zoo.Count == 0)
                {
                    Console.WriteLine("No animals in the zoo yet.");
                    return false;
                }

                foreach (Animal animal in zoo)
                {
                    animal.ShowInfo();
                }
                return true;
            }

            void addAnimal()
            {
                Console.Clear();

                string name = string.Empty;
                string species = string.Empty;
                int age = 0;
                string environment = string.Empty;
                double cost = 0;

                Console.WriteLine("Enter name:");
                name = Console.ReadLine() ?? "";

                Console.WriteLine("Enter species:");
                species = Console.ReadLine() ?? "";

                Console.WriteLine("Enter age:");
                while (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.WriteLine("Invalid age! Try again:");
                }

                Console.WriteLine("Enter cost:");
                while (!double.TryParse(Console.ReadLine(), out cost))
                {
                    Console.WriteLine("Invalid cost! Try again:");
                }

                Console.WriteLine("Enter environment:");
                environment = Console.ReadLine() ?? "";

                zoo.Add(new Animal(name, species, age, cost, environment));

                Console.WriteLine("\nAnimal added!\n");
            }

            void removeAnimal()
            {
                if (!displayAnimals()) return;

                Console.WriteLine("Enter ID of animal to remove:");
                int id;

                int.TryParse(Console.ReadLine(), out id);

                if (id <= 0)
                {
                    Console.WriteLine("Invalid ID!");
                    return;
                }

                Animal? found = zoo.Find(a => a.Id == id);

                if (found != null)
                {
                    zoo.Remove(found);
                    Console.WriteLine("Animal removed.");
                }
                else
                {
                    Console.WriteLine("Animal not found.");
                }
            }

            bool shouldContinue = true;

            while (shouldContinue)
            {
                Console.Clear();
                Console.WriteLine("\n1. Add Animal");
                Console.WriteLine("2. Remove Animal");
                Console.WriteLine("3. Show Animals");
                Console.WriteLine("4. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        addAnimal();
                        break;

                    case "2":
                        removeAnimal();
                        break;

                    case "3":
                        displayAnimals();
                        break;

                    case "4":
                        shouldContinue = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
