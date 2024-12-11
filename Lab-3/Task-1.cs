using System;
using System.Collections.Generic;

namespace EcosystemSimulation
{
    // Базовий клас для всіх живих організмів
    public class LivingOrganism
    {
        public int Energy { get; set; }
        public int Age { get; set; }
        public int Size { get; set; }

        public LivingOrganism(int energy, int age, int size)
        {
            Energy = energy;
            Age = age;
            Size = size;
        }

        public bool IsAlive() => Energy > 0;

        public void GrowOlder()
        {
            Age++;
        }

        public void DecreaseEnergy(int amount)
        {
            Energy -= amount;
        }
    }
    public interface IReproducible
    {
        LivingOrganism Reproduce();
    }
    public interface IPredator
    {
        void Hunt(LivingOrganism prey);
    }
    public class Animal : LivingOrganism, IPredator
    {
        public int Speed { get; set; }
        public bool IsCarnivorous { get; set; }

        public Animal(int energy, int age, int size, int speed, bool isCarnivorous)
            : base(energy, age, size)
        {
            Speed = speed;
            IsCarnivorous = isCarnivorous;
        }
        public void Hunt(LivingOrganism prey)
        {
            if (IsCarnivorous && prey is Animal preyAnimal && preyAnimal.IsAlive())
            {
                Energy += 10;
                preyAnimal.DecreaseEnergy(5);
                Console.WriteLine($"{this.GetType().Name} hunted {prey.GetType().Name} and gained energy.");
            }
            else
            {
                Console.WriteLine($"{this.GetType().Name} cannot hunt.");
            }
        }
    }
    public class Plant : LivingOrganism
    {
        public int PhotosynthesisRate { get; set; }

        public Plant(int energy, int age, int size, int photosynthesisRate)
            : base(energy, age, size)
        {
            PhotosynthesisRate = photosynthesisRate;
        }

        public void Photosynthesize()
        {
            Energy += PhotosynthesisRate;
            Console.WriteLine($"{this.GetType().Name} photosynthesized and gained energy.");
        }
    }
    public class Microorganism : LivingOrganism, IReproducible
    {
        public int ReproductionRate { get; set; }
        public Microorganism(int energy, int age, int size, int reproductionRate)
            : base(energy, age, size)
        {
            ReproductionRate = reproductionRate;
        }
        public LivingOrganism Reproduce()
        {
            if (Energy >= 5)
            {
                Energy -= 2;
                var newMicroorganism = new Microorganism(3, 0, 1, ReproductionRate);
                Console.WriteLine($"{this.GetType().Name} reproduced and created a new microorganism.");
                return newMicroorganism;
            }
            else
            {
                Console.WriteLine($"{this.GetType().Name} doesn't have enough energy to reproduce.");
                return null;
            }
        }
    }
    public class Ecosystem
    {
        public List<LivingOrganism> Organisms { get; set; }

        public Ecosystem()
        {
            Organisms = new List<LivingOrganism>();
        }

        public void AddOrganism(LivingOrganism organism)
        {
            Organisms.Add(organism);
        }

        public void Simulate()
        {
            List<LivingOrganism> newOrganisms = new List<LivingOrganism>();  // Список для нових організмів

            foreach (var organism in Organisms)
            {
                if (organism is Animal animal && animal is IPredator predator)
                {
                    var prey = FindPrey(animal);
                    if (prey != null)
                    {
                        predator.Hunt(prey);
                    }
                }
                else if (organism is Plant plant)
                {
                    plant.Photosynthesize();
                }
                else if (organism is Microorganism microorganism && microorganism is IReproducible reproducible)
                {
                    var newOrganism = reproducible.Reproduce();
                    if (newOrganism != null)
                    {
                        newOrganisms.Add(newOrganism);
                    }
                }
            }
            Organisms.AddRange(newOrganisms);
        }
        private LivingOrganism FindPrey(Animal predator)
        {
            foreach (var organism in Organisms)
            {
                if (organism is Animal prey && prey != predator && prey.IsAlive())
                {
                    return prey;
                }
            }
            return null;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Данiїл Iванченко, КIб-1-23-4.0д");
            var lion = new Animal(energy: 50, age: 5, size: 100, speed: 60, isCarnivorous: true);
            var deer = new Animal(energy: 30, age: 3, size: 50, speed: 40, isCarnivorous: false);
            var tree = new Plant(energy: 20, age: 10, size: 5, photosynthesisRate: 5);
            var bacteria = new Microorganism(energy: 10, age: 1, size: 1, reproductionRate: 2);

            var ecosystem = new Ecosystem();
            ecosystem.AddOrganism(lion);
            ecosystem.AddOrganism(deer);
            ecosystem.AddOrganism(tree);
            ecosystem.AddOrganism(bacteria);
            ecosystem.Simulate();
        }
    }
}
