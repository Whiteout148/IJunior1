using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            Zoo zoo = new Zoo();

            zoo.Work();
        }
    }

    class Zoo
    {
        public List<Aviary> _aviaries = new List<Aviary>();

        public Zoo()
        {
            _aviaries.Add(new Aviary());
            _aviaries.Add(new Aviary());
            _aviaries.Add(new Aviary());
            _aviaries.Add(new Aviary());

            AddAnimalsToAviary();
        }

        public void Work()
        {
            const string ExitCommand = "exit";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("** Зоопарк **");
                Console.WriteLine("\nКоличество вольеров: " + _aviaries.Count);
                Console.WriteLine("Напишите номер вольера или (exit) для выхода.");
                string userInput = Console.ReadLine();

                if (userInput == ExitCommand)
                {
                    isWork = false;
                }
                else if (UserUtils.TryGetNumberInRange(userInput, out int result, _aviaries.Count))
                {
                    ShowAviary(result - 1);
                }
                else
                {
                    Console.WriteLine("Нету такой команды!");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowAviary(int number)
        {
            Console.WriteLine("Вольер номер: " + (number + 1));
            Console.WriteLine("Количество животных: " + _aviaries[number].AnimalsCount);
            Console.WriteLine("Животные: ");

            _aviaries[number].ShowAnimals();
        }

        private void AddAnimalsToAviary()
        {
            string male = "Самец";
            string female = "Самка";

            List<Animal> animalsToAdd = new List<Animal>();

            animalsToAdd.Add(new Animal("Лев", female, "Рычание"));
            animalsToAdd.Add(new Animal("Лев", male, "Рычание"));
            animalsToAdd.Add(new Animal("Тигр", female, "Рычание"));
            animalsToAdd.Add(new Animal("Тигр", male, "Рычание"));
            animalsToAdd.Add(new Animal("Свинка", male, "Хрюканье"));
            animalsToAdd.Add(new Animal("Свинка", female, "Хрюканье"));

            for (int i = 0; i < _aviaries.Count; i++)
            {
                _aviaries[i].AddAnimals(UserUtils.GetRandomAnimals(animalsToAdd));
            }
        }
    }

    class Aviary
    {
        private List<Animal> _animals = new List<Animal>();
        public int AnimalsCount => _animals.Count;

        public void AddAnimals(List<Animal> animalsToAd)
        {
            _animals.AddRange(animalsToAd);
        }

        public void ShowAnimals()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                _animals[i].ShowInfo();
                Console.WriteLine();
            }
        }
    }

    class Animal
    {
        private string _type;
        private string _gender;
        private string _sound;

        public Animal(string type, string gender, string sound)
        {
            _type = type;
            _gender = gender;
            _sound = sound;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Тип {_type} Пол {_gender} Звук {_sound}");
        }

        public Animal GetClone()
        {
            return new Animal(_type, _gender, _sound);
        }
    }

    static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }

        public static bool TryGetNumberInRange(string userNumber, out int result, int max)
        {
            if (int.TryParse(userNumber, out result))
            {
                if (result > max || result <= 0)
                {
                    Console.WriteLine("Число больше чем максимальное количество!");
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Введите число!");
            }

            return false;
        }

        public static List<Animal> GetRandomAnimals(List<Animal> animals)
        {
            List<Animal> animalsToGive = new List<Animal>();

            for (int i = 0; i < GetRandomNumber(1, animals.Count); i++)
            {
                animalsToGive.Add(animals[i].GetClone());
            }

            return animalsToGive;
        }
    }
}