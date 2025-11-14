using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            Oceanarium oceanarium = new Oceanarium();

            oceanarium.Work();
        }
    }

    class Oceanarium
    {
        private Aquarium _aquarium = new Aquarium();

        public void Work()
        {
            const string CommandAddFish = "1";
            const string CommandRemoveFish = "2";
            const string CommandExit = "3";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Рыбы в аквариуме: ");
                Console.WriteLine();
                _aquarium.ShowFisces();
                Console.WriteLine();
                Console.WriteLine("Типы команд:");
                Console.WriteLine($"Добавить рыбу: {CommandAddFish}");
                Console.WriteLine($"Забрать рыбу: {CommandRemoveFish}");
                Console.WriteLine($"Выход: {CommandExit}");
                _aquarium.BecomeOldFisces();
                _aquarium.ShowDeadFisces();

                switch (Console.ReadLine())
                {
                    case CommandAddFish:
                        _aquarium.AddFish();
                        break;

                    case CommandRemoveFish:
                        _aquarium.RemoveFish();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Нету такой команды.");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _fisces = new List<Fish>();

        public void ShowDeadFisces()
        {
            for (int i = _fisces.Count - 1; i >= 0; i--)
            {
                if (_fisces[i].IsDead)
                {
                    Console.WriteLine($"Рыбка: {_fisces[i].Name} умерла.");
                    _fisces.RemoveAt(i);
                }
            }
        }

        public void BecomeOldFisces()
        {
            for (int i = 0; i < _fisces.Count; i++)
            {
                _fisces[i].BecomeOlder();
            }
        }

        public void ShowFisces()
        {
            for (int i = 0; i < _fisces.Count; i++)
            {
                _fisces[i].ShowInfo();
            }
        }

        public void AddFish()
        {
            string userName = GetUserMessage("Введите название рыбы:");

            if (IsContainsFish(userName))
            {
                Console.WriteLine("Такая рыба уже есть, выберите другое имя.");
            }
            else
            {
                string userAge = GetUserMessage("Введите возраст рыбы:");

                if (UserUtils.TryGetPositiveNumber(userAge, out int resultAge))
                {
                    _fisces.Add(new Fish(resultAge, userName));
                    Console.WriteLine("Рыба добавлена.");
                }
            }
        }

        public void RemoveFish()
        {
            string userName = GetUserMessage("Введите имя рыбы которую хотите забрать:");

            if (IsContainsFish(userName))
            {
                _fisces.RemoveAt(GetIndexWithName(userName));
                Console.WriteLine("Рыба удалена.");
            }
            else
            {
                Console.WriteLine("Такой рыбы нету!");
            }
        }

        private string GetUserMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        private int GetIndexWithName(string userName)
        {
            for (int i = 0; i < _fisces.Count; i++)
            {
                if (_fisces[i].Name.ToLower() == userName.ToLower())
                {
                    return i;
                }
            }

            return -1;
        }

        private bool IsContainsFish(string userName)
        {
            for (int i = 0; i < _fisces.Count; i++)
            {
                if (_fisces[i].Name == userName)
                {
                    return true;
                }
            }

            return false;
        }
    }

    class Fish
    {
        private int _health;

        public Fish(int health, string name)
        {
            _health = health;
            Name = name;
        }

        public bool IsDead => _health < 0;

        public string Name { get; private set; }

        public void BecomeOlder()
        {
            _health--;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя {Name}, Кол-во жизней {_health}.");
        }
    }

    static class UserUtils
    {
        public static string GetUserMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        
        public static bool TryGetPositiveNumber(string userNumber, out int resultNumber)
        {
            if (int.TryParse(userNumber, out resultNumber))
            {
                if (resultNumber <= 0)
                {
                    Console.WriteLine("Введите число которое больше чем ноль");
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
    }
}