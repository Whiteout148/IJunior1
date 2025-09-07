using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;

namespace dzdzdz
{
    internal class dzdz1
    {
        static void Main(string[] args)
        {
            const string CommandForServe = "1";

            Queue<int> clientsSum = new Queue<int>();

            clientsSum.Enqueue(23);
            clientsSum.Enqueue(59);
            clientsSum.Enqueue(30);

            int balance = 0;

            while (clientsSum.Count > 0)
            {
                Console.WriteLine("Баланс: " + balance);
                Console.WriteLine("\nСумма клиента: " + clientsSum.Peek());

                Console.WriteLine($"Введите команду: {CommandForServe} чтобы обслужить клиента.");
                string userInput = Console.ReadLine();

                if (userInput == CommandForServe)
                {
                    ServeClient(clientsSum, ref balance);
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        static void ServeClient(Queue<int> clientsSum, ref int balance)
        {
            Console.WriteLine("Клиент Обслужен!");
            balance = AddBalance(clientsSum, balance);
            clientsSum.Dequeue();
        }

        static int AddBalance(Queue<int> clientsSum, int balance)
        {
            balance += clientsSum.Peek();
            Console.WriteLine($"На баланс добавлено: {clientsSum.Peek()} Баланс: {balance}");

            return balance;
        }
    }
}
