using System;
using System.Collections.Generic;
using System.IO;

namespace dzdzdz
{
    internal class dzdz1
    {
        static void Main(string[] args)
        {
            const string CreatePlayerCommand = "1";
            const string DeletePlayerCommand = "2";
            const string BanPlayerCommand = "3";
            const string UnbanPlayerCommand = "4";

            DataBase dataBase = new DataBase();

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("База данных");
                Console.WriteLine();
                dataBase.ShowPlayersInfo();
                Console.WriteLine();
                Console.WriteLine("Варианты команд:");
                Console.WriteLine($"Команда для добавление игрока: {CreatePlayerCommand}");
                Console.WriteLine($"Команда для удаление игрока: {DeletePlayerCommand}");
                Console.WriteLine($"Команда для бана игрока: {BanPlayerCommand}");
                Console.WriteLine($"Команда для разбана игрока: {UnbanPlayerCommand}");
                Console.WriteLine("\nВведите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CreatePlayerCommand:
                        dataBase.AddPlayer();
                        break;

                    case DeletePlayerCommand:
                        dataBase.DeletePlayer();
                        break;

                    case BanPlayerCommand:
                        dataBase.BanPlayer();
                        break;

                    case UnbanPlayerCommand:
                        dataBase.UnBanPlayer();
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break; 
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class DataBase
    {
        private List<Player> players = new List<Player>();

        public void AddPlayer()
        {
            string userNick = GetUserMessage("Введите никнейм игрока:");
            string userNumber = GetUserMessage("Введите номер игрока:");
            int resultNumber;

            if (TryToConvert(userNumber, out resultNumber))
            {
                if (IsGetNumber(resultNumber))
                {
                    Console.WriteLine("Игрок с таким номером уже существует!");
                }
                else
                {
                    players.Add(new Player(resultNumber, userNick));
                    Console.WriteLine("Игрок добавлен.");
                }
            }
        }

        public void DeletePlayer()
        {
            string userNumber = GetUserMessage("Введите номер игрока: ");
            int resultNumber;

            if (TryToConvert(userNumber, out resultNumber))
            {
                if (IsGetNumber(resultNumber))
                {
                    int index = GetIndexWithNumber(resultNumber);

                    Console.WriteLine($"Игрок {players[index].Nick} удален из списка.");
                    players.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("Не найден такой номер игрока!");
                }
            }
        }

        public void UnBanPlayer()
        {
            string userNumber = GetUserMessage("Введите номер игрока: ");
            int resultNumber;

            if (TryToConvert(userNumber, out resultNumber))
            {
                if (IsGetNumber(resultNumber))
                {
                    int index = GetIndexWithNumber(resultNumber);

                    if (IsGetBanned(players[index]))
                    {
                        players[index].IsBanned = false;
                        Console.WriteLine($"Игрок: {players[index].Nick} разбанен.");
                    }
                    else
                    {
                        Console.WriteLine("Игрок не забанен!");
                    }
                }
                else
                {
                    Console.WriteLine("Не найден такой номер игрока!");
                }
            }
        }

        public void BanPlayer()
        {
            string userNumber = GetUserMessage("Введите номер игрока: ");
            int resultNumber;

            if (TryToConvert(userNumber, out resultNumber))
            {
                if (IsGetNumber(resultNumber))
                {
                    int index = GetIndexWithNumber(resultNumber);

                    if (IsGetBanned(players[index]))
                    {
                        Console.WriteLine("Игрок уже забанен!");
                    }
                    else
                    {
                        players[index].IsBanned = true;
                        Console.WriteLine($"Игрок: {players[index].Nick} забанен.");
                    }
                }
                else
                {
                    Console.WriteLine("Не найден такой номер игрока!");
                }
            }
        }

        public void ShowPlayersInfo()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].ShowInfo();
            }
        }

        private bool IsGetBanned(Player player)
        {
            if (player.IsBanned)
                return true;
            else
                return false;
        }

        private bool IsGetNumber(int userNumber)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (userNumber == players[i].Number)
                    return true;
            }

            return false;
        }

        private int GetIndexWithNumber(int userNumber)
        {
            int index = 0;

            for (int i = 0; i < players.Count; i++)
            {
                if (userNumber == players[i].Number)
                {
                    index = i;
                }
            }

            return index;
        }

        private bool TryToConvert(string userInput, out int result)
        {
            if (int.TryParse(userInput, out result))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Введите целое число!");
                return false;
            }
        }

        private string GetUserMessage(string message)
        {
            Console.WriteLine(message);
            string userInput = Console.ReadLine();

            return userInput;
        }
    }

    class Player
    {
        public Player(int number, string nick, bool isBanned = false)
        {
            Number = number;
            Nick = nick;
            IsBanned = isBanned;
        }

        public int Number { get; private set; }
        public string Nick { get; private set; }
        public bool IsBanned { get; set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Ник игрока: {Nick} номер: {Number} забанен ли: {GetWordWithBanValue()}");
        }

        public string GetWordWithBanValue()
        {
            string bannedMessage = (IsBanned) ? "Да" : "Нет";

            return bannedMessage;
        }
    }
}
