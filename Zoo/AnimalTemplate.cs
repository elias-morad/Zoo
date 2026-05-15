using System;

namespace Zoo
{
    internal class AnimalTemplate
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public DietType Diet { get; set; }
        public string Environment { get; set; }

        public AnimalTemplate(string name, string species, DietType diet, string environment)
        {
            Name = name;
            Species = species;
            Diet = diet;
            Environment = environment;
        }
    }
}