using System;
using System.Collections.Generic;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            Aquarium aquarium = new Aquarium();

            aquarium.Work();
        }
    }

    class Aquarium
    {
        private List<Fish> _fisces = new List<Fish>();

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
                ShowFisces();
                Console.WriteLine();
                Console.WriteLine("Типы команд:");
                Console.WriteLine($"Добавить рыбу: {CommandAddFish}");
                Console.WriteLine($"Забрать рыбу: {CommandRemoveFish}");
                Console.WriteLine($"Выход: {CommandExit}");
                BecomeOldFisces();
                ShowDeadFisces();

                switch (Console.ReadLine())
                {
                    case CommandAddFish:
                        AddFish();
                        break;

                    case CommandRemoveFish:
                        RemoveFish();
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

        private void ShowDeadFisces()
        {
            for (int i = 0; i < _fisces.Count; i++)
            {
                if (_fisces[i].IsDie())
                {
                    Console.WriteLine($"Рыбка: {_fisces[i].Name} умерла.");
                    _fisces.RemoveAt(i);
                }
            }
        }

        private void BecomeOldFisces()
        {
            for (int i = 0; i < _fisces.Count; i++)
            {
                _fisces[i].BecomeOlder();
            }
        }

        private void ShowFisces()
        {
            for (int i = 0; i < _fisces.Count; i++)
            {
                _fisces[i].ShowInfo();
            }
        }

        private void AddFish()
        {
            string userName = GetUserMessage("Введите название рыбы:");

            if (IsContainsFish(userName))
            {
                Console.WriteLine("Такая рыба уже есть, выберите другое имя.");
            }
            else
            {
                string userAge = GetUserMessage("Введите возраст рыбы:");

                if (TryGetAge(userAge, out int resultAge))
                {
                    _fisces.Add(new Fish(resultAge, userName));
                    Console.WriteLine("Рыба добавлена.");
                }
            }
        }

        private void RemoveFish()
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

        private bool TryGetAge(string userAge, out int age)
        {
            if (int.TryParse(userAge, out age))
            {
                if (age <= 0)
                {
                    Console.WriteLine("Введите такое количество жизней чтобы рыба не умерла в первую же итерацию!");
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

        private int GetIndexWithName(string userName)
        {
            for (int i = 0; i < _fisces.Count; i++)
            {
                if (_fisces[i].Name == userName)
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

        public string Name { get; private set; }

        public void BecomeOlder()
        {
            _health--;
        }

        public bool IsDie()
        {
            return _health < 0;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя {Name}, Кол-во жизней {_health}.");
        }
    }
}