using System;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Animal> zoo = new List<Animal>();

            List<AnimalTemplate> templates = new List<AnimalTemplate>
            {
                new AnimalTemplate("Leo", "Lion", DietType.Carnivore, "Savannah"),
                new AnimalTemplate("Bella", "Elephant", DietType.Herbivore, "Savannah"),
                new AnimalTemplate("Coco", "Parrot", DietType.Omnivore, "Rainforest"),
                new AnimalTemplate("Nemo", "Clownfish", DietType.Omnivore, "Ocean")
            };

            // Used when adding animal
            void ShowTemplates()
            {
                Console.WriteLine("\nSelect an animal to add:\n");

                for (int i = 0; i < templates.Count; i++)
                {
                    var t = templates[i];
                    Console.WriteLine($"{i + 1}. {t.Name} ({t.Species}) - {t.Diet}");
                }
            }

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
                    if (zoo.Count > 0)
                    {
                        int maxId = zoo.Max(a => a.Id);
                        Animal.UpdateNextId(maxId);
                    }
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
                ShowTemplates();

                Console.WriteLine("\nEnter choice:");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input.");
                    return;
                }

                if (choice < 1 || choice > templates.Count)
                {
                    Console.WriteLine("Invalid selection.");
                    return;
                }

                var selected = templates[choice - 1];

                Console.WriteLine("Enter custom name (or press Enter to keep default):");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                    name = selected.Name;

                zoo.Add(new Animal(
                    name,
                    selected.Species,
                    1, // default age
                    selected.Environment,
                    selected.Diet
                ));

                Console.WriteLine("Animal added from template!");
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
