using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        const string CommandAddEmployee = "1";
        const string CommandDeleteEmployee = "2";
        const string CommandExit = "3";

        Dictionary<string, List<string>> dossiers = new Dictionary<string, List<string>>();

        bool isWork = true;
        string userInput;

        while (isWork)
        {
            Console.WriteLine("** Кадровый Учет **\n");
            Console.WriteLine("Сотрудники:");
            Console.WriteLine();
            PrintDossiers(dossiers);
            Console.WriteLine();
            Console.WriteLine("Доступные команды:");
            Console.WriteLine($"Добавить сотрудника: {CommandAddEmployee}");
            Console.WriteLine($"Удалить сотрудника: {CommandDeleteEmployee}");
            Console.WriteLine($"Выход: {CommandExit}");
            Console.Write("Введите нужную команду: ");
            userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandAddEmployee:
                    AddEmployee(dossiers);
                    break;

                case CommandDeleteEmployee:
                    DeleteDossier(dossiers);
                    break;

                case CommandExit:
                    isWork = false;
                    break;

                default:
                    Console.WriteLine("Нету Такой Команды");
                    break;

            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    static void AddEmployee(Dictionary<string, List<string>> dossier)
    {
        string userJob = GetWordAfterMessage("Введите должность:");
        string userFullName = GetWordAfterMessage("Введите имя сотрудника:");

        if (dossier.ContainsKey(userJob))
        {
            dossier[userJob].Add(userFullName);

            Console.WriteLine($"Добавлен сотрудник: {userFullName} к должности: {userJob}");
        }
        else
        {
            List<string> fullNames = new List<string>();
            dossier.Add(userJob, fullNames);
            dossier[userJob].Add(userFullName);

            Console.WriteLine($"Добавлен сотрудник: {userFullName} к должности: {userJob}");
        }
    }

    static void DeleteDossier(Dictionary<string, List<string>> dossiers)
    {
        string userEmployeeJob = GetWordAfterMessage("Напишите должность сотрудника: ");
        string userEmployee = GetWordAfterMessage("Напишите сотрудника: ");

        bool isGetEmployee = false;
        bool isGetJob = IsGetKeyInDictionary(dossiers, userEmployeeJob);

        if (isGetJob)
        {
            for (int i = 0; i < dossiers[userEmployeeJob].Count; i++)
            {
                if (dossiers[userEmployeeJob][i] == userEmployee)
                {
                    dossiers[userEmployeeJob].RemoveAt(i);
                    isGetEmployee = true;
                }
            }

            if (isGetEmployee == false)
            {
                Console.WriteLine("Сотрудник не найден");
            }
        }
        else
        {
            Console.WriteLine("Должность не найдена");
        }

        if (dossiers[userEmployeeJob].Count == 0)
        {
            dossiers.Remove(userEmployeeJob);
        }
    }

    static bool IsGetKeyInDictionary(Dictionary<string, List<string>> dossier, string userKey)
    {
        foreach (var key in dossier.Keys)
        {
            if (userKey == key)
            {
                return true;
            }
        }

        return false;
    }

    static void PrintDossiers(Dictionary<string, List<string>> dossiers)
    {
        foreach (var dossier in dossiers)
        {
            Console.WriteLine("Должность: " + dossier.Key + "\n Сотрудники: ");

            foreach (var name in dossier.Value)
            {
                Console.WriteLine(name);
            }
        }
    }
    static string GetWordAfterMessage(string message)
    {
        Console.WriteLine(message);
        string userInput = Console.ReadLine();

        return userInput;
    }
}