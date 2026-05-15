using System;

namespace Zoo
{
    internal class Animal
    {
        private static int _nextId = 1; // auto-increment ID

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Species { get; set; } = null!;
        public int Age { get; set; }
        public string Environment { get; set; } = null!;
        public DietType Diet { get; set; }

        // Parameterless constructor required for JSON
        public Animal() { }

        public Animal(string name, string species, int age, string environment, DietType diet)
        {
            Id = _nextId++;
            Name = name;
            Species = species;
            Age = age;
            Environment = environment;
            Diet = diet;
        }

        public override string ToString()
        {
            return
                $"ID: {Id}\n" +
                $"Name: {Name}\n" +
                $"Species: {Species}\n" +
                $"Age: {Age}\n" +
                $"Environment: {Environment}\n";
        }

        public static void UpdateNextId(int maxId)
        {
            _nextId = maxId + 1;
        }
    }
}
