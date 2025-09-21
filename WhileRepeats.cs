using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            DataBases databases = new DataBases();

            databases.Work();
        }
    }

    class DataBases
    {
        private List<Player> _players = new List<Player>();

        public void Work()
        {
            const string AddPlayerCommand = "1";
            const string BanPlayerCommand = "2";
            const string UnBanPlayerCommand = "3";
            const string RemovePlayerCommand = "4";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("База данных");
                Console.WriteLine();
                ShowPlayerInfo();
                Console.WriteLine();
                Console.WriteLine("Варианты команд:");
                Console.WriteLine($"Добавить игрока: {AddPlayerCommand}");
                Console.WriteLine($"Забанить игрока: {BanPlayerCommand}");
                Console.WriteLine($"Разбанить игрока: {UnBanPlayerCommand}");
                Console.WriteLine($"Удалить игрока: {RemovePlayerCommand}");
                string userInput = GetUserMessage("Введите команду:");

                switch (userInput)
                {
                    case AddPlayerCommand:
                        AddPlayer();
                        break;

                    case BanPlayerCommand:
                        BanPlayer();
                        break;

                    case UnBanPlayerCommand:
                        UnBanPlayer();
                        break;

                    case RemovePlayerCommand:
                        RemovePlayer();
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

        }

        private void AddPlayer()
        {
            string userInput = GetUserMessage("Введите имя игрока: ");

            _players.Add(new Player(userInput));
        }

        private void BanPlayer()
        {
            if (TryGetPlayerId(out int id))
            {
                _players[id - 1].Ban();
                Console.WriteLine("Игрок забанен");
            }
        }

        private void RemovePlayer()
        {
            if (TryGetPlayerId(out int id))
            {
                _players.RemoveAt(id - 1);
                Console.WriteLine("Игрок удален");
            }
        }

        public void UnBanPlayer()
        {
            if (TryGetPlayerId(out int id))
            {
                _players[id - 1].UnBan();
                Console.WriteLine("Игрок разбанен");
            }
        }

        private bool TryGetPlayerId(out int id)
        {
            string userId = GetUserMessage("Введите номер игрока:");

            if (int.TryParse(userId, out id))
            {
                if (id > _players.Count || id < 1)
                {
                    Console.WriteLine("Игрока с таким индексом нету в базе данных");
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Введите целое число");
            }

            return false;
        }

        private void ShowPlayerInfo()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowInfo();
            }
        }

        private string GetUserMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }

    class Player
    {
        private static int _idCounter = 1;

        public Player(string name)
        {
            Id = _idCounter++;
            Name = name;
            IsBanned = false;
        }

        public bool IsBanned { get; private set; }
        public string Name { get; private set; }
        public int Id { get; private set; }

        public void Ban()
        {
            if (IsBanned)
                Console.WriteLine("Игрок уже забанен");
            else
                IsBanned = true;
        }

        public void UnBan()
        {
            if (IsBanned)
                IsBanned = false;
            else
                Console.WriteLine("Игрок не забанен");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Игрок: {Name} номер: {Id} забанен ли: {GetWordWithBanValue()}");
        }

        private string GetWordWithBanValue()
        {
            return (IsBanned) ? "Да" : "Нет";
        }
    }
}
