using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const string CommandAddDossier = "1";
        const string CommandPrintAllDossier = "2";
        const string CommandDeleteDossier = "3";
        const string CommandFindWithSurname = "4";
        const string CommandExit = "5";

        List<string> fullNames = new List<string>();
        List<string> jobs = new List<string>();

        bool isWork = true;
        string userInput;

        while (isWork)
        {
            Console.WriteLine("** Кадровый Учет **\n");
            Console.WriteLine();
            Console.WriteLine("Доступные команды:");
            Console.WriteLine($"Добавить досье: {CommandAddDossier}");
            Console.WriteLine($"Посмотреть все досье: {CommandPrintAllDossier}");
            Console.WriteLine($"Удалить досье: {CommandDeleteDossier}");
            Console.WriteLine($"Искать по фамилии: {CommandFindWithSurname}");
            Console.WriteLine($"Выход: {CommandExit}");
            Console.Write("Введите нужную команду: ");
            userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandAddDossier:
                    AddDossier(ref fullNames, ref jobs);
                    break;

                case CommandPrintAllDossier:
                    PrintAllDossier(fullNames, jobs);
                    break;

                case CommandDeleteDossier:
                    DeleteDossier(ref fullNames, ref jobs);
                    break;

                case CommandFindWithSurname:
                    FindWithSurname(fullNames, jobs);
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

    static void AddDossier(ref List<string> fullName, ref List<string> job)
    {
        string userFullName = GetUserMessage("Введите (Фамилия Имя Отчество):");
        string userJob = GetUserMessage("Введите должность:");

        string[] dividedFullName = userFullName.Split();
        int dividedFullNameLenght = 3;

        if (dividedFullName.Length == dividedFullNameLenght)
        {
            fullName.Add(userFullName);
            job.Add(userJob);

            Console.WriteLine("Досье добавлено!");
        }
        else
        {
            Console.WriteLine("Неверно введено ФИО!");
        }
    }

    static void DeleteDossier(ref List<string> fullName, ref List<string> job)
    {
        string userIndex = GetUserMessage("Введите индекс нужного досье:");
        int resultIndex;

        if (int.TryParse(userIndex, out resultIndex))
        {
            resultIndex = resultIndex - 1;

            if (resultIndex < 0 || resultIndex >= fullName.Count)
            {
                Console.WriteLine("Индекс находится за границей!");
            }
            else
            {
                fullName.RemoveAt(resultIndex);
                job.RemoveAt(resultIndex);

                Console.WriteLine("Досье удалено.");
            }
        }
        else
        {
            Console.WriteLine("Введено не целое число!");
        }
    }

    static string GetUserMessage(string message)
    {
        Console.WriteLine(message);
        string userInput = Console.ReadLine();

        return userInput;
    }

    static void PrintDossier(List<string> fullName, List<string> job, int index, char divider = ' ')
    {
        Console.Write($"ФИО: {fullName[index]} Должность: {job[index]} {divider} ");
    }

    static void PrintAllDossier(List<string> fullNames, List<string> jobs)
    {
        for (int i = 0; i < fullNames.Count; i++)
        {
            int dossierNumber = i + 1;
            char divider = '-';

            if (i == fullNames.Count - 1)
            {
                Console.Write($"Досье номер {dossierNumber} ");
                PrintDossier(fullNames, jobs, i);
            }
            else
            {
                Console.Write($"Досье номер {dossierNumber} ");
                PrintDossier(fullNames, jobs, i, divider);
            }
        }
    }

    static void FindWithSurname(List<string> fullNames, List<string> jobs)
    {
        string userSurname = GetUserMessage("Введите фамилию: ");
        bool isGetSurname = false;

        for (int i = 0; i < fullNames.Count; i++)
        {
            string[] dividedFullName = fullNames[i].Split();

            if (dividedFullName[0] == userSurname)
            {
                Console.WriteLine($"Досье с фамилией ({userSurname})");
                PrintDossier(fullNames, jobs, i);

                isGetSurname = true;
            }
        }

        if (isGetSurname == false)
        {
            Console.WriteLine("Нету сотрудника с такой фамилией!");
        }
    }
}