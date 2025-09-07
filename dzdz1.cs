using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace dzdzdz
{
    internal class dzdz1
    {
        static void Main(string[] args)
        {
            Queue<string> clients = new Queue<string>();

            clients.Enqueue("Влад");
            clients.Enqueue("Алексей");
            clients.Enqueue("Иван");
            clients.Enqueue("Роман");

            int balance = 0;
            int purchaseSum = 0;

            while (clients.Count > 0)
            {
                Random random = new Random();

                purchaseSum = SetPurchaseSum(purchaseSum);
                PrintClientInfo(clients, purchaseSum);

                bool isServicedClient = IsServiced(ref balance, purchaseSum);

                if (isServicedClient)
                {
                    clients.Dequeue();
                }
                
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Смена завершена!");
        }

        static bool IsServiced(ref int balance, int purchaseSum)
        {
            const string CommandToAddBalance = "1";
            const string CommandToPrintBalance = "2";

            Console.WriteLine("Нужно перевести деньги клиента на ваш баланс, команда: " + CommandToAddBalance);
            string userCommand = Console.ReadLine();

            if (userCommand == CommandToAddBalance)
            {
                balance = AddBalance(purchaseSum, balance);
                Console.WriteLine("Введите команду для показа баланса: " + CommandToPrintBalance);
                userCommand = Console.ReadLine();

                if (userCommand == CommandToPrintBalance)
                {
                    PrintBalance(ref balance);

                    return true;
                }
                else
                {
                    Console.WriteLine("Такой команды нету.");
                }
            }
            else
            {
                Console.WriteLine("Такой команды нету.");
            }

            return false;
        }

        static void PrintBalance(ref int balance)
        {
            Console.WriteLine("Ваш баланс: " + balance);
        }

        static int AddBalance(int purchaseSum, int balance)
        {
            balance += purchaseSum;

            Console.WriteLine($"Добавлено: {purchaseSum} к балансу.");

            return balance;
        }

        static void PrintClientInfo(Queue<string> clients, int purchaseSum)
        {
            Console.WriteLine("Клиент: " + clients.Peek() + " Сумма покупок: " + purchaseSum);
        }

        static int SetPurchaseSum(int purchaseSum)
        {
            Random random = new Random();

            int minValue = 10;
            int maxValue = 50;

            return random.Next(minValue, maxValue);
        }
    }
}
