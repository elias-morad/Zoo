using System;
using System.Text.Json;
using System.IO;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Animal> zoo = new List<Animal>();

            void SaveToFile()
            {
                string json = JsonSerializer.Serialize(zoo, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText("zoo.json", json);

                Console.WriteLine("Zoo saved to file.");
            }

            void LoadFromFile()
            {
                if (!File.Exists("zoo.json"))
                {
                    Console.WriteLine("No save file found.");
                    return;
                }

                string json = File.ReadAllText("zoo.json");

                List<Animal>? loaded = JsonSerializer.Deserialize<List<Animal>>(json);

                if (loaded != null)
                {
                    zoo = loaded;
                    int maxId = zoo.Max(a => a.Id);
                    Animal.UpdateNextId(maxId);
                    Console.WriteLine("Zoo loaded successfully.");
                }
            }

            bool DisplayAnimals()
            {
                Console.WriteLine("\nAnimals in the Zoo:\n");

                if (zoo.Count == 0)
                {
                    Console.WriteLine("No animals in the zoo yet.");
                    return false;
                }

                foreach (Animal animal in zoo)
                {
                    Console.WriteLine(animal);
                }
                return true;
            }

            void AddAnimal()
            {
                string name = string.Empty;
                string species = string.Empty;
                int age = 0;
                string environment = string.Empty;

                Console.WriteLine("Enter name:");
                while (string.IsNullOrWhiteSpace(name))
                {
                    name = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Invalid name! Try again:");
                    }
                }

                Console.WriteLine("Enter species:");
                while (string.IsNullOrWhiteSpace(species))
                {
                    species = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(species))
                    {
                        Console.WriteLine("Invalid species! Try again:");
                    }
                }

                Console.WriteLine("Enter age:");
                while (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.WriteLine("Invalid age! Try again:");
                }

                Console.WriteLine("Enter environment:");
                while (string.IsNullOrWhiteSpace(environment))
                {
                    environment = Console.ReadLine() ?? "";

                    if (string.IsNullOrWhiteSpace(environment))
                    {
                        Console.WriteLine("Invalid environment! Try again:");
                    }
                }

                zoo.Add(new Animal(name, species, age, environment));

                Console.WriteLine("\nAnimal added!\n");
            }

            void RemoveAnimal()
            {
                if (!DisplayAnimals())
                    return;

                Console.WriteLine("Enter ID of animal to remove:");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID!");
                    return;
                }

                Animal? found = zoo.Find(a => a.Id == id);

                if (found == null)
                {
                    Console.WriteLine("Animal not found.");
                    return;
                }

                zoo.Remove(found);
                Console.WriteLine("Animal removed.");
            }

            bool shouldContinue = true;

            while (shouldContinue)
            {
                Console.Clear();
                Console.WriteLine("\n1. Add Animal");
                Console.WriteLine("2. Remove Animal");
                Console.WriteLine("3. Show Animals");
                Console.WriteLine("4. Save Zoo");
                Console.WriteLine("5. Load Zoo");
                Console.WriteLine("6. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        AddAnimal();
                        break;

                    case "2":
                        Console.Clear();
                        RemoveAnimal();
                        break;

                    case "3":
                        Console.Clear();
                        DisplayAnimals();
                        break;

                    case "4":
                        SaveToFile();
                        break;

                    case "5":
                        LoadFromFile();
                        break;

                    case "6":
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
